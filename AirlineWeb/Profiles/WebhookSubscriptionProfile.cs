
using AutoMapper;
using AirlineWeb.Dtos;
using AirlineWeb.Models;

namespace AirlineWeb.Profiles
{
    public class WebhookSubscriptionProfile : Profile
    {
        public WebhookSubscriptionProfile()
        {
            //Source -> Target
            CreateMap<WebhookSubscriptionCreateDto, WebhookSubscription>();
            CreateMap<WebhookSubscription, WebhookSubscriptionReadDto>();
            
        }
    }
}