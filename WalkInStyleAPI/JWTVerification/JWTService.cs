using System.IdentityModel.Tokens.Jwt;

namespace WalkInStyleAPI.JWTVerification
{
    public class JWTService:IJWTService
    {
        public void CheckUser(string token)
        {
            token = token.Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "UserId");
                Console.WriteLine(userIdClaim);
            }

        }
    }
}
