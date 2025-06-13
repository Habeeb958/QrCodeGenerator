using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static MoviesController;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private static readonly List<Movie> Movies = new()
    {
        new Movie { Title = "Inception", ImageUrl = "/images/INCEPTION.jpeg" },
        new Movie { Title = "The Matrix", ImageUrl = "/images/THE_MATRIX.jpeg" },
        new Movie { Title = "Interstellar", ImageUrl = "/images/INTERSTELLAR.jpeg" },
        new Movie { Title = "Avatar", ImageUrl = "/images/AVATAR.jpeg" },
        new Movie { Title = "Titanic", ImageUrl = "/images/TITANIC.jpeg" },
        new Movie { Title = "Gladiator", ImageUrl = "/images/GLADIATOR.jpeg" },
        new Movie { Title = "The Dark Knight", ImageUrl = "/images/THE_DARK_KNIGHT.jpeg" },
        new Movie { Title = "Pulp Fiction", ImageUrl = "/images/PULP_FICTION.jpeg" },
        new Movie { Title = "Forrest Gump", ImageUrl = "/images/FOREST_GUMP.jpeg" },
        new Movie { Title = "The Shawshank Redemption", ImageUrl = "/images/THE_SHAWSHANK_REDEMPTION.jpeg" }
    };

    [HttpGet("random")]
    public IActionResult GetRandomMovies()
    {
        var random = new Random();
        var randomMovies = Movies.OrderBy(x => random.Next()).Take(10).ToList();

        return Ok(randomMovies);
    }

    public class Movie
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
