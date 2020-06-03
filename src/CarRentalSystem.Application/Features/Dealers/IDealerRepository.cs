namespace CarRentalSystem.Application.Features.Dealers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Domain.Models.Dealers;
    using Queries.Details;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<DealerDetailsOutputModel> GetDetails(
            int id, 
            CancellationToken cancellationToken = default);

        Task<Dealer> FindByUser(
            string userId, 
            CancellationToken cancellationToken = default);
    }
}