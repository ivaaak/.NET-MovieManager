using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;

namespace MovieManager.Test.Data
{
    public class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<MovieContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<MovieContext>()
                .UseSqlServer(connection)
                .Options;

            using var context = new MovieContext(dbContextOptions);

            context.Database.EnsureCreated();
        }

        public MovieContext CreateContext() => new MovieContext(dbContextOptions);

        public void Dispose() => connection.Dispose();
    }
}
