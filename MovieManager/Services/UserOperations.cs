using MovieManager.Models.DataModels;
using MovieManager.Data.DBConfig;
using MovieManager.Data.DataModels;

namespace MovieManager.Services
{
    public class UserOperations
    {
        public static void CreateUser(
            string username)
        {
            MovieContext context = new MovieContext();
            if (context.Users.Any(u => u.UserName == username))
            {
                Console.WriteLine("User already exists");
            }
            else
            {
                User u = new User
                {
                    //UserName = username,
                    //CountryCode = countryCode,
                    //LanguageId = languageId,
                    //Email = "testc@test.com",
                    //PasswordHash = "1234",
                };

                context.Users.Add(u);
                context.SaveChanges();
                context.Dispose();
                Console.WriteLine($"Created user {username}");
            }
        }
        // TRANSFERRED TO ASP.NET IDENTITY 
    }
}
