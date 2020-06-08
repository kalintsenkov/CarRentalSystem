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
        private readonly HashSet<CarAd> carAds;

        internal Dealer(string name, PhoneNumber phoneNumber)
        {
            this.Validate(name);

            this.Name = name;
            this.PhoneNumber = phoneNumber;

            this.carAds = new HashSet<CarAd>();
        }

        private Dealer(string name)
        {
            this.Name = name;
            this.PhoneNumber = null!;

            this.carAds = new HashSet<CarAd>();
        }

        public string Name { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public IReadOnlyCollection<CarAd> CarAds => this.carAds.ToList().AsReadOnly();

        public void AddCarAd(CarAd carAd) => this.carAds.Add(carAd);

        public Dealer EditName(string name)
        {
            this.Name = name;
            return this;
        }

        public Dealer EditPhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
            return this;
        }

        private void Validate(string name)
            => Guard.ForStringLength<InvalidDealerException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}
