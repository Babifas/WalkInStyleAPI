﻿using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using WalkInStyleAPI.Data;
using WalkInStyleAPI.Models;
using WalkInStyleAPI.Models.DTOs.User;

namespace WalkInStyleAPI.Services.User_Service
{
    public class UserService:IUserService
    {
        private readonly ApDbContext _context;
        private readonly IMapper _mapper;
        public UserService(ApDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> RegisterUser(RegisterUserDto user)
        {
            var IsUserExist = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
            if (IsUserExist == null) 
            {
                var _user = _mapper.Map<User>(user);
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                var HashPassword=BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                _user.Password=HashPassword;
                _user.Role = "user";
                _user.isBlocked = false;
                _context.Users.Add(_user);
                await _context.SaveChangesAsync();
                return true; 
            }

            return false;
        }
        public async Task<List<UserViewDto>> GetAllUsers()
        {
            var users=await _context.Users.ToListAsync();
            var _user=_mapper.Map<List<UserViewDto>> (users);
            return _user;
        }
        public async Task<UserViewDto> GetUserById(int id)
        {
            var user=await _context.Users.FirstOrDefaultAsync(u=>u.UserId== id);
            if (user != null)
            {
                var _user= _mapper.Map<UserViewDto>(user);
                return _user;
            }
            return null ;
        }
        public async Task<User> Login(LoginDto user)
        {
            var IsUserExist = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
            
            if (IsUserExist != null && BCrypt.Net.BCrypt.Verify(user.Password, IsUserExist.Password))
            {
                if (IsUserExist.isBlocked == true)
                {
                    throw new Exception("Sorry,Access denied");
                }
                return IsUserExist;
            }
            return null;
        }
        public async Task BlockUser(int userid)
        {
            var user=await _context.Users.FindAsync(userid);
            if (user == null)
            {
                throw new Exception("User id not valid");
            }
            user.isBlocked=true;
            await _context.SaveChangesAsync();
        }
        public async Task UnblockUser(int userid)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null)
            {
                throw new Exception("User id not valid");
            }
            user.isBlocked = false;
            await _context.SaveChangesAsync();
        }
    }
}
