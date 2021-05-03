using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var retVal = _context.CelestialObjects.First(x => x.Id == id);

            if (retVal == null)
            {
                return NotFound();
            }

            retVal.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == retVal.Id).ToList();
            
            return Ok(retVal);
        }
        
        
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var retVal = _context.CelestialObjects.First(x => x.Name == name);

            if (retVal == null)
            {
                return NotFound();
            }

            retVal.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == retVal.Id).ToList();
            
            return Ok(retVal);
        }
        
        [HttpGet()]
        public IActionResult GetAll()
        {
            var retVal = _context.CelestialObjects;

            if (retVal == null)
            {
                return NotFound();
            }

            foreach (var cObject in retVal)
            {
                cObject.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == cObject.Id).ToList();
            }
            
            return Ok(retVal);
        }
    }
}
