namespace CarRentalSystem.Domain.Models.Dealers
{
    using System.Collections.Generic;
    using System.Linq;
    using CarAds;
    using Common;
    using Exceptions;

    using static ModelConstants.Common;

    public class Dealer : Entity<int>, IAggregateRoot
    {
        private readonly ICollection<CarAd> carAds;

        public Dealer(string name, PhoneNumber phoneNumber)
        {
            this.Validate(name);

            this.Name = name;
            this.PhoneNumber = phoneNumber;

            this.carAds = new HashSet<CarAd>();
        }

        public string Name { get; }

        public PhoneNumber PhoneNumber { get; }

        public IReadOnlyCollection<CarAd> CarAds => this.carAds.ToList().AsReadOnly();

        public void AddCarAd(CarAd carAd) => this.carAds.Add(carAd);

        private void Validate(string name)
            => Guard.ForStringLength<InvalidDealerException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}
