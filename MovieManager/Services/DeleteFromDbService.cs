﻿using MovieManager.Services.ServicesContracts;
using MovieManager.Data;
using MovieManager.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace MovieManager.Services
{
    public class DeleteFromDbService : IDeleteFromDbService
    {
        private readonly MovieContext dataContext;

        public DeleteFromDbService(MovieContext data)
        {
            this.dataContext = data;
        }


        public void DeleteMovieFromUserPlaylist(int movieId, string playlistName, string userName)
        {
            var movie = dataContext.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();

            var targetPlaylist = dataContext.Playlists
                .Include(p => p.Movies)
                .Where(u => u.User.UserName == userName && u.PlaylistName == playlistName)
                .FirstOrDefault();

            targetPlaylist.Movies.Remove(movie);

            dataContext.SaveChanges();

            Console.WriteLine($"Removed Movie {movieId} from user - {userName}'s list: {playlistName}");
        }


        public async Task DeleteActorFromUserList(int actorId, string userName)
        {
            var actor = await dataContext.Actors.Where(m => m.ActorId == actorId).FirstOrDefaultAsync();

            var targetUser = await dataContext.Users
                .Where(u => u.UserName == userName)
                .FirstOrDefaultAsync();

            if (targetUser.Actors.Contains(actor))
            {
                targetUser.Actors.Remove(actor);
            }

            await dataContext.SaveChangesAsync();

            Console.WriteLine($"Removed Actor {actor.FullName} from user - {userName}'s favorite actors");
        }


        public async Task<string> DeleteFromDbUsingName(string movieTitle)
        {
            if (await dataContext.Movies.AnyAsync(i => i.Title == movieTitle))
            {
                Console.WriteLine($"Found movie: {movieTitle}. Deleting from Db.");

                var movie = await dataContext.Movies.Where(x => x.Title == movieTitle).FirstOrDefaultAsync();

                if (movie != null) 
                {
                    dataContext.Movies.Remove(movie);
                    await dataContext.SaveChangesAsync();
                }
                return $"Deleted {movieTitle} from Movies DB.";
            }
            else
            {
                return $"{movieTitle} doesn't exist in the DB!";
            }

        }


        public async Task<string> DeleteFromDbUsingId(int Id)
        {
            if (await dataContext.Movies.AnyAsync(i => i.MovieId == Id))
            {
				Movie movie = await dataContext.Movies.Where(x => x.MovieId == Id).FirstOrDefaultAsync();
				Console.WriteLine($"Found movie with ID: {movie.MovieId} and Name: {movie.Title}. Deleting from Db.");

                dataContext.Movies.Remove(movie);
                await dataContext.SaveChangesAsync();
                return $"Deleted Movie with ID - {Id} from Movies DB.";
            }
            else
            {
                return $"{Id} doesn't exist in the DB!";
            }
        }
    }
}
