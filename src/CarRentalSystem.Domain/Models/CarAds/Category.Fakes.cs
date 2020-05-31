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
                => new Category("Valid category", "Valid description text");

            public Priority Priority => Priority.Default;
        }
    }
}
