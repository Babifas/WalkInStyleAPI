namespace WalkInStyleAPI.JWTVerification
{
    public interface IJWTService
    {
        int GetUserIdFromToken(string token);
    }
}
