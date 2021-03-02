using System.ComponentModel.DataAnnotations;

namespace Airline.WebApi.Dtos
{
    public class WebhookSubscriptionCreateDto
    {
        [Required]
        public string WebhookUri { get; set; }
        
        [Required]
        public string WebhookType { get; set; }
    }
}