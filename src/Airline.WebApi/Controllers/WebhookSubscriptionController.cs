using System;
using System.Linq;
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
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{secret}", Name = "GetSubscriptionBySecret")]
        public async Task<ActionResult<WebhookSubscriptionReadDto>> GetSubscriptionBySecret(string secret)
        {
            var subscription = await _context.WebhookSubscriptions.FirstOrDefaultAsync(s => s.Secret == secret);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription));
        }
        
        [HttpPost]
        public async Task<ActionResult<WebhookSubscriptionReadDto>> CreateSubscription(WebhookSubscriptionCreateDto model)
        {
            var subscription = await _context.WebhookSubscriptions
                .FirstOrDefaultAsync(s => s.WebhookUri == model.WebhookUri);

            if (subscription != null) return NoContent();
            
            subscription = _mapper.Map<WebhookSubscription>(model);
            subscription.Secret = Guid.NewGuid().ToString();
            subscription.WebhookPublisher = "PanAus";

            try
            {
                await _context.WebhookSubscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                
            var webhookSubscription = _mapper.Map<WebhookSubscriptionReadDto>(subscription);

            return RedirectToAction(nameof(GetSubscriptionBySecret), new { secret = webhookSubscription.Secret });
        }
    }
}