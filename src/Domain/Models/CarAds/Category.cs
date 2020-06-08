namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;

    using static ModelConstants.Category;
    using static ModelConstants.Common;

    public class Category : Entity<int>
    {
        internal Category(string name, string description)
        {
            this.Validate(name, description);

            this.Name = name;
            this.Description = description;
        }

        public string Name { get; }

        public string Description { get; }

        private void Validate(string name, string description)
        {
            Guard.ForStringLength<InvalidCategoryException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.ForStringLength<InvalidCategoryException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
        }
    }
}