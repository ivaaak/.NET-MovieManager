# ASP.NET- Verus MovieManager
A full stack web project built with ASP.NET, EF Core and the TMDB API. 
The Application can be used for browsing and logging movies and tv shows. 

## Screenshots: 

Main page:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/mainMenu.png" width="80%"></img> 

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/mainMenuBottom.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/mainMenuNavbar.png" width="45%"></img> 

Login / Register / Logout:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/2.%20loginView.png" width="30%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/2.%20register.png" width="30%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/2.%20logout.png" width="30%"></img> 

Profile Page:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/profilePageNew.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/profilePageNew2.png" width="45%"></img> 

Discover:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/3.%20discover.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/discoverStart.png" width="45%"></img> 

Profile Statistics:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/profile.png" width="80%"></img> 

Playlist (grid/list):

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/playlistGrid.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/playlistList.png" width="45%"></img> 

Playlists /with QR Codes/:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/playlists.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/playlistsQrCode.png" width="45%"></img> 

Trailer Partial Views / Modals

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/trailerModal.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/trailerModal2.png" width="45%"></img> 

Movie/Show page:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/movieCard.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/movieCard2.png" width="45%"></img> 

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/showCard.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/4.%20show-movieCard.png" width="45%"></img> 

Actors page:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/actors.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/actorsDetail.png" width="45%"></img> 

Search:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/4.%20search.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/4.%20searchResults.png" width="45%"></img> 

Reviews/ User Reviews Page: 

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/reviewPage.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/reviews.png" width="45%"></img> 

Admin Area:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/admin.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/admin%20users.png" width="45%"></img> 

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/adminMovies.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/adminPlaylists.png" width="45%"></img> 

ToastR notifications:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/toastCombined.png" width="40%"></img> 

API - Endpoints and Results:

<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/ApiAllEndpoints.png" width="45%"></img> 
<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/ApiAllPlaylistsResponse.png" width="45%"></img> 

<img src="https://github.com/ivaaak/ASP.NET-MovieManager/blob/master/design/ApiMovieResult.png" width="45%"></img> 
<img src="https://github.com/ivaaak/ASP.NET-MovieManager/blob/master/design/ApiMovieResult2.png" width="45%"></img> 

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
- Docker
- TMDB API
- ASP.NET Identity System
- MVC Areas (Admin / User-Guest)
- Razor Pages + Partial Views
- Customized Log in and Register pages (scaffolded)
- Dependency Injection and IoC
- Data Validation, both Client-side, and Server-side
- Responsive Design (Custom CSS, Bootstrap and JS animations/transitions/DOM)
- Bootstrap 5
- Redis Caching
- Fluent Validation

(Libraries)
- TMDB Api Wrapper
- Toastr
- QRCoder 
- jQuery
- Swagger/Swashbuckle

(Testing)
- MyTested.AspNetCore.Mvc 
- Moq (incl inMemory DB)
- xunit and NUnit
- coverlet
- CodeCov


<img src="https://raw.githubusercontent.com/ivaaak/ASP.NET-MovieManager/master/design/testing2.png" width="60%"></img> 


🏅 SoftUni Web Advanced/ASP.NET - 104/105 pts (6.00)
