# ASP.NET- Verus MovieManager
A full stack web project built with ASP.NET, EF Core and the TMDB API. 
It can be used for browsing and logging movies and tv shows. 

## Features:

**Guests**
- Have access to the welcome page for guests and can register or log in.
- Can search for movies/shows/actors
- Can browse the discover section - popular/top rated/trending/newly released movies.

**Users**
- Can browse all sections like the guests.
- Can create custom playlists. 
- Can also make playlists public and generate QR Codes for each list.
- Can save movies/shows to a playlist(including a user-specific favorites list)
- Can save a list of favorite actors.
- Can rate and review movies.

**Admin**
- Can edit and delete playlists.
- Can edit and delete movies/shows.
- Can delete user accounts.

**Tests/Api**
- All services are tested
- Some controller routes are tested
- The API can return information about user playlists, users and movies/shows

**All data is pulled from the TMDB Api and only what is required is saved to the DB.**

## Built With:
- ASP.NET Core 6
- Entity Framework Core 6.0.9
- Microsoft SQL Server Express
- TMDB Api and a TMDB Api Wrapper
- ASP.NET Identity System
- MVC Areas (Admin / User-Guest)
- Razor Pages + Partial Views
- Customized Log in and Register pages (scaffolded)
- Dependency Injection and IoC
- Data Validation, both Client-side, and Server-side
- Responsive Design (custom css, bootstrap and js)
- Bootstrap
- Toastr
- QRCoder 
- jQuery
- MyTested.AspNetCore.Mvc 
- xunit
