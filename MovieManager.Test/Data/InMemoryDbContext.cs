using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.DataModels;

namespace MovieManager.Test.Data
{
    public class InMemoryDbContext : DbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<MovieContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<MovieContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new MovieContext(dbContextOptions);

            context.Database.EnsureCreated();
           
        }
        public MovieContext CreateContext() => new MovieContext(dbContextOptions);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlite(connection);
            }
        }

        public DbSet<User> Users { get; set; } //users hashset as the test context has no identity
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<QRCodeObject> QRCodes { get; set; }
    }
}
