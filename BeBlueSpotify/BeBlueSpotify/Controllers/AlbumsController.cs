using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeBlueSpotify.Model;
using BeBlueSpotify.Arquitetura.Paginacao;

namespace BeBlueSpotify.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly BeBlueSpotifyContext _context;

        public AlbumsController(BeBlueSpotifyContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbum()
        {
            return await _context.Album.ToListAsync();
        }      

        // PUT: api/Albums/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
            if (id != album.Idalbum)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Albums
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
            _context.Album.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbum", new { id = album.Idalbum }, album);
        }

        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Album>> DeleteAlbum(int id)
        {
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();

            return album;
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.Idalbum == id);
        }

        // GET: api/Albums
        /// <summary>
        /// Consultar o catálogo de discos de forma paginada, filtrando por gênero e
        /// ordenando de forma crescente pelo nome do disco;
        /// </summary>
        /// <param name="idGenero"></param>
        /// <param name="paginaAtual"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Album> GetAlbumPorGenero(int idGenero, int paginaAtual)
        {
            return _context.Album.Where(r => r.Idgenero == idGenero).OrderBy(r => r.Nome).GetPaged(paginaAtual, 20).Results;
        }

        // GET: api/Albums/5
        /// <summary>
        /// Consultar o disco pelo seu identificador;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _context.Album.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }
    }
}
