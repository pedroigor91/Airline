using AirlineWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Data
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> opt) :base(opt)
        {
            
        }

        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
    }
}