namespace CarRentalSystem.Web.Features
{
    using System.Linq;
    using Application;
    using Application.Contracts;
    using Domain.Models.CarAds;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class CarAdsController : ControllerBase
    {
        private readonly IRepository<CarAd> carAds;
        private readonly IOptions<ApplicationSettings> settings;

        public CarAdsController(
            IRepository<CarAd> carAds,
            IOptions<ApplicationSettings> settings)
        {
            this.carAds = carAds;
            this.settings = settings;
        }

        [HttpGet]
        public object Get() => new
        {
            Settings = this.settings,
            CarAds = this.carAds
                .All()
                .Where(c => c.IsAvailable)
        };
    }
}
