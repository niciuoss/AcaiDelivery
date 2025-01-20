using Microsoft.AspNetCore.Mvc;
using AcaiDeliveryAPI.Models;
using AcaiDeliveryAPI.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AcaiDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcaiController : ControllerBase
    {
        private readonly AcaiContext _context;

        public AcaiController(AcaiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acai>>> GetAcais()
        {
            return await _context.Acais.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Acai>> CreateAcai(Acai acai)
        {
            _context.Acais.Add(acai);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAcais), new { id = acai.Id }, acai);
        }
    }
}
