namespace CarRentalSystem.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;

    internal class CarRentalDbInitializer : IInitializer
    {
        private readonly CarRentalDbContext db;

        public CarRentalDbInitializer(CarRentalDbContext db) => this.db = db;

        public void Initialize() => this.db.Database.Migrate();
    }
}
