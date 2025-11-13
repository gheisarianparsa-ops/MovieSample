using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Data;
using MovieWebApi.Interfaces;
using MovieWebApi.Models;

namespace MovieWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMovieApp<GenreModel> _GenreRepositoy;
        public GenreController(IMovieApp<GenreModel> GenreRepositoy)
        {
            _GenreRepositoy = GenreRepositoy;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreModel>>> GetAllGenre()
        {
            return Ok(await _GenreRepositoy.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GenreModel>>> GetGenre(int id)
        {
            return Ok(await _GenreRepositoy.GetByIdAsync(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var res = await _GenreRepositoy.DeleteAsync(id);

            if (!res)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GenreModel>> UpdateGenre(int id, GenreModel genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }
            var Updatedgenre = await _GenreRepositoy.UpdateAsync(id, genre);
            if (Updatedgenre == null)
            {
                return NotFound();
            }
            return Ok(Updatedgenre);
        }
        [HttpPost]
        public async Task<ActionResult<GenreModel>> InsertGenre([FromBody] GenreModel genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _GenreRepositoy.CreateAsync(genre);
            return Ok(genre);
        }
    }
}
