using MovieManager.Models.DataModels;
using MovieManager.Data.DBConfig;
using MovieManager.Data.DataModels;

namespace MovieManager.Services
{
    public class UserOperations
    {
        public static void CreateUser()
        {
            MovieContext context = new MovieContext();

            /*
            if (context.Users.Any(u => u.UserName == username))
            {
                Console.WriteLine("User already exists");
            }
            else
            {
            */
                User u = new User()
                {
                    Email = "abc@abc.com",
                    UserName = "testUser",
                };

                context.Users.Add(u);
                context.SaveChanges();
                context.Dispose();
                Console.WriteLine($"Created user {u.UserName} with ID {u.Id}");
            //}
        }
        // TRANSFERRED TO ASP.NET IDENTITY 
    }
}
