namespace MovieManager.Services.UserServices
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
