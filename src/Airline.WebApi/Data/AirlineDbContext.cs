using Airline.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Airline.WebApi.Data
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> opt) : base(opt)
        {
        }
        
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
    }
}