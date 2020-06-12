﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Repositiories;
using System.Threading.Tasks;

namespace MovieApp.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("api/movies")]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _movieRepository.GetAllMovies());
        }

        [HttpGet("api/movies/{id}")]
        public async Task<IActionResult> GetMovies(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie != null)
            {
                return Ok(movie);
            }

            return NotFound(new { Message = $"Movie with id {id} is not available." });
        }

        [HttpGet("api/directors")] //directors
        public async Task<IActionResult> GetDirectors()
        {
            return Ok(await _movieRepository.GetAllDirectors());
        }

        [HttpPost("api/movies")]
        public async Task<IActionResult> AddMovie(CreateMovieModel model)
        {
            var result = await _movieRepository.AddMovie(model);
            if (result >= 1)
            {
                return Ok(new { Message = "Movie added succesfully." });
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }
    }
}