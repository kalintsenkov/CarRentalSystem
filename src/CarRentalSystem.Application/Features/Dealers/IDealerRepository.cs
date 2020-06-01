namespace CarRentalSystem.Application.Features.Dealers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Domain.Models.Dealers;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default);
    }
}