using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerCollectionApi.Models;

namespace BeerCollectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly BeerCollectionContext _context;

        public BeersController(BeerCollectionContext context)
        {
            _context = context;
        }

        // GET: api/Beers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers()
        {
            if (_context.Beers == null)
            {
                return NotFound();
            }
            return await _context.Beers.ToListAsync();
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            if (_context.Beers == null)
            {
                return NotFound();
            }
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        //GET: Search beers by name
        [HttpGet]
        [Route("searchbyname")]
        public IActionResult GetBeers(string beerName)
        {
            var beers = from m in _context.Beers
                        select m;

            if (!String.IsNullOrEmpty(beerName))
            {
                beers = beers.Where(s => s.BeerName!.Contains(beerName));
            }
            return Ok(beers);
        }

        //PUT change beer rating
        [HttpPut]
        public async Task<IActionResult> UpdateBeerRating(BeerRatingChangeRequest request)
        {
            if (request.Rating > 5 || request.Rating < 1)
            {
                return Problem("Beer rating is incorrect.");
            }
            var item = await _context.Beers.FindAsync(request.BeerId);
            if (item == null)
                return BadRequest("Beer not found");

            if (item.Rating == null)
            {
                item.Rating = request.Rating;
            }
            else
            {
                item.Rating = (item.Rating + request.Rating) / 2;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Beers
        [HttpPost]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            if (_context.Beers == null)
            {
                return Problem("Entity set 'BeerCollectionContext.Beers'  is null.");
            }
            if (beer.Rating != null && (beer.Rating > 5 || beer.Rating < 1))
            {
                return Problem("Beer rating is incorrect.");
            }
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBeer), new { id = beer.Id }, beer);
        }

        // DELETE: api/Beers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            if (_context.Beers == null)
            {
                return NotFound();
            }
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
