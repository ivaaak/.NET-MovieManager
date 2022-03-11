using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;

namespace MovieManager.Data
{
    public class MovieContext : IdentityDbContext<User> 
        //this overwrites the default Asp.Net Identity model
    {
        public MovieContext()
        {
        }

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options) 
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<UserPlaylist> UserPlaylists { get; set; }

        //will be adding these one by one to the DB
        //public DbSet<Actor> Actors { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        //public DbSet<Platform> Platforms { get; set; }
        //public DbSet<Actor> Actors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //playlist has many movies
            modelBuilder.Entity<Playlist>()
                .HasMany(c => c.Movies)
                .WithOne(e => e.Playlist);

            //user has many playlists
            modelBuilder.Entity<User>()
                .HasMany(c => c.Playlists)
                .WithOne(e => e.User);

            base.OnModelCreating(modelBuilder);
            //initializes Identity?
        }
    }
}
