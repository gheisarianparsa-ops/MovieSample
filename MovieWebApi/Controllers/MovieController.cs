using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Data;
using MovieWebApi.Interfaces;
using MovieWebApi.Models;

namespace MovieWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieApp<MovieModel> _movieRepositoy;
        public MovieController(IMovieApp<MovieModel> movieRepositoy)
        {
            _movieRepositoy = movieRepositoy;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetAllMovie()
        {
            return Ok(await _movieRepositoy.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovie(int id)
        {
            return Ok(await _movieRepositoy.GetByIdAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var res = await _movieRepositoy.DeleteAsync(id);

            if (!res)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieModel>> UpdateMove(int id, MovieModel movie)
        {
            if (id!=movie.Id)
            {
                return BadRequest();
            }
            var UpdatedMovie = await _movieRepositoy.UpdateAsync(id, movie);
            if (UpdatedMovie == null)
            {
                return NotFound();
            }
            return Ok(UpdatedMovie);
        }
        [HttpPost]
        public async Task<ActionResult<MovieModel>> InsertMovie(MovieModel movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _movieRepositoy.CreateAsync(movie);
            return Ok(movie);
        }
    }
}
