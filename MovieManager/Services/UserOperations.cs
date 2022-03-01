using MovieManagerMVC.Models.DataModels;
using MovieManagerMVC.Data.DBConfig;
using MovieManagerMVC.Data.DataModels;

namespace MovieManagerMVC.Services
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
                UserNotAsp u = new UserNotAsp
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
