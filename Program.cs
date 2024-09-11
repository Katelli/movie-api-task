using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); 
        //Register the Movie list with the Dependency Injection Container
        builder.Services.AddSingleton<IMovieService, MovieService>();
        var app = builder.Build();

        //Create: Creates a new movie
        app.MapPost("/movies", (Movie? movie, IMovieService movieService) =>
        {
            if (movie == null)
            {
                return Results.BadRequest();
            }
            var CreatedMovie = movieService.CreateMovie(movie);
            return Results.Created($"/movies/{CreatedMovie.Id}", CreatedMovie);
        });

        //Read: Gets all movies
         app.MapGet("/movies", (IMovieService movieService) => 
        {
            return movieService.GetAllMovies();
        });

        //Update: Updates a movie with ID
        app.MapPut("movies/{Id}", (int Id, Movie? UpdateMovieInfo, IMovieService movieService) =>
        {
            if (UpdateMovieInfo == null)
            {
                return Results.BadRequest();
            }
            var movie = movieService.UpdateMovieWithId(Id, UpdateMovieInfo);
            if (movie == null)
            {
                return Results.NotFound();
            }
            return Results.Ok();
        });

        //Delete: Deletes a movie with ID
        app.MapDelete("movies/{Id}", (int Id, IMovieService movieService) => 
        {
            movieService.DeleteMovieWithId(Id);
            return Results.Ok();
        });

        app.MapGet("/status", () => "System healthy");

        app.Run();
    }
}
  