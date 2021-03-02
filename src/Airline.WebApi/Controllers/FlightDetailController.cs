using System;
using System.Threading.Tasks;
using Airline.WebApi.Data;
using Airline.WebApi.Dtos;
using Airline.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public FlightDetailController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("{flightCode}", Name = "GetFlightDetailBySecret")]
        public async Task<ActionResult<FlightDetailReadDto>> GetFlightDetailBySecret(string flightCode)
        {
            var flight = await _context.FlightDetails.FirstOrDefaultAsync(f => f.FlightCode == flightCode);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightDetailReadDto>(flight));
        }
        
        [HttpPost]
        public async Task<ActionResult<FlightDetailReadDto>> CreateFlightDetail(FlightDetailCreateDto model)
        {
            var flight = await _context.FlightDetails
                .FirstOrDefaultAsync(f => f.FlightCode == model.FlightCode);

            if (flight != null) return NoContent();
            
            flight = _mapper.Map<FlightDetail>(model);

            try
            {
                await _context.FlightDetails.AddAsync(flight);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                
            var flightDetail = _mapper.Map<FlightDetailReadDto>(flight);

            return RedirectToAction(nameof(GetFlightDetailBySecret), new { flightCode = flightDetail.FlightCode });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FlightDetailReadDto>> UpdateFlightDetail(int id, FlightDetailUpdateDto model)
        {
            var flight = await _context.FlightDetails.FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null) return NoContent();
            
            _mapper.Map(model, flight);
            
            try
            {
                _context.FlightDetails.Update(flight);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}