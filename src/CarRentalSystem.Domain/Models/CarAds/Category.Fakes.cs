namespace CarRentalSystem.Domain.Models.CarAds
{
    using System;
    using FakeItEasy;

    public class CategoryFakes
    {
        public class CategoryDummyFactory : IDummyFactory
        {
            public bool CanCreate(Type type) => type == typeof(Category);

            public object? Create(Type type) 
                => new Category("Economy", "Economy cars are extremely comfortable for urban and non-urban condition because of its sizes and maneuverability as well as the best rental prices. We offer huge choice of economy cars available for rent at affordable prices. If our offices are not convenient for you, we will deliver a car directly to you address. Economy cars for rent are from the leading world manufacturers as: Toyota, Renault, Ford, Nissan and others. If you need economy car for rent in Bulgaria, you will find the best offers below. Choose the best economy car hire for your holiday!");

            public Priority Priority => Priority.Default;
        }
    }
}
