namespace CarRentalSystem.Web.Features
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Contracts;
    using Domain.Models.CarAds;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class CarAdsController : ControllerBase
    {
        private readonly IRepository<CarAd> carAds;

        public CarAdsController(IRepository<CarAd> carAds) 
            => this.carAds = carAds;

        [HttpGet]
        public IEnumerable<CarAd> Get() 
            => this.carAds
            .All()
            .Where(c => c.IsAvailable);
    }
}
