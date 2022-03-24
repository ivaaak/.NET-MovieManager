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

        //public DbSet<PlaylistMovie> PlaylistMovies { get; set; }
        

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Platform> Platforms { get; set; }


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

            /*
            modelBuilder.Entity<PlaylistMovie>()
                .HasKey(e => new { e.MovieId, e.PlaylistId });

            modelBuilder.Entity<PlaylistMovie>()
                .HasOne(e => e.Movie)
                .WithMany(e => e.PlaylistMovies)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlaylistMovie>()
                .HasOne(e => e.Playlist)
                .WithMany(e => e.PlaylistMovies)
                .OnDelete(DeleteBehavior.Restrict);
            */

            /*playlist has many movies
            modelBuilder.Entity<Playlist>()
                .HasMany(c => c.Movies)
                .WithOne();

            modelBuilder.Entity<Movie>()
               .HasMany(c => c.Playlists)
               .WithOne();
            */

            //user has many playlists
            modelBuilder.Entity<User>()
                .HasMany(c => c.Playlists)
                .WithOne(e => e.User);

            base.OnModelCreating(modelBuilder);
            //initializes Identity?
        }
    }
}