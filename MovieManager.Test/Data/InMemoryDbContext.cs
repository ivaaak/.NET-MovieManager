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

        //The entity type 'PlaylistMovie' requires a primary key to be defined.
        //If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PlaylistMovie>().HasNoKey();
            //playlist <-> movie many to many
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Playlists)
                .WithMany(m => m.Movies)
                .UsingEntity<PlaylistMovie>(
                     j => j
                        .HasOne(pt => pt.Playlist)
                        .WithMany(t => t.PlaylistMovies)
                        .HasForeignKey(pt => pt.PlaylistId),
                    j => j
                        .HasOne(pt => pt.Movie)
                        .WithMany(p => p.PlaylistMovies)
                        .HasForeignKey(pt => pt.MovieId),
                    j =>
                    {
                        j.HasKey(t => new { t.MovieId, t.PlaylistId });
                    });

            //user has many playlists
            modelBuilder.Entity<User>()
                .HasMany(c => c.Playlists)
                .WithOne(e => e.User);

            //playlist has a qrcode
            modelBuilder.Entity<Playlist>()
                .HasOne(c => c.QrCode)
                .WithOne(e => e.Playlist);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; } //users hashset as the test context has no identity
        public DbSet<User> AspNetUsers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<QRCodeObject> QRCodes { get; set; }
    }
}
