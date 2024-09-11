class Movie
{
    //Set up variables(fields) and methods. Use modifiers (private/public) to determine how it can be accessed
    private static int _id = 0;
    //Use the properties get; set; to gain access to and modify the values of private fields
    public int Id {get; set;}
    public string Title {get; set;}

    public Movie(string title)
    {
        Title = title;
        Id = _id++;
    }
}
// Methods needed for CRUD
interface IMovieService
{
    //Create
    public Movie CreateMovie(Movie movie);
    //Read
    public IEnumerable<Movie> GetAllMovies();
    //Update
    public Movie? UpdateMovieWithId(int id, Movie UpdateMovieInfo);
    //Delete
    public void DeleteMovieWithId(int id);
}

// Implements IMovieService and contains the logic needed
class MovieService : IMovieService
{
    private List<Movie> movies;
    public MovieService()
    {
        movies = new List<Movie>();
    }
    //Create
    public Movie CreateMovie(Movie movie)
    {
        //Add movie to the list
        movies.Add(movie);
        return movie;
    }
    //Read
    public IEnumerable<Movie>GetAllMovies()
    {
        //Return list of all the movies
        return movies;
    }
    //Update
    public Movie? UpdateMovieWithId(int id, Movie UpdateMovieInfo)
    {
        //Find the movie with the ID
        var movie = movies.Find((movie) => movie.Id == id);
        //Return nothing if there is no movie with that ID
        if(movie == null)
        {
            return null;
        }
        //Update the title of the movie with that ID
        movie.Title = UpdateMovieInfo.Title;
        return movie;
    }
    //Delete
    public void DeleteMovieWithId(int id)
    {
        //Find the movie with the ID
        var movie = movies.Find((movie) => movie.Id == id);
        //Do nothing if there is noe movie with that ID
        if(movie == null)
        {
            return;
        }
        //Remove the movie with that ID
        movies.Remove(movie);
    }
}