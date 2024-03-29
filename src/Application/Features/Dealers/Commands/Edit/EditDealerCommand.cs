﻿namespace CarRentalSystem.Application.Features.Dealers.Commands.Edit;

using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MediatR;

public class EditDealerCommand : EntityCommand<int>, IRequest<Result>
{
    public string Name { get; init; } = default!;

    public string PhoneNumber { get; init; } = default!;

    public class EditDealerCommandHandler : IRequestHandler<EditDealerCommand, Result>
    {
        private readonly ICurrentUser currentUser;
        private readonly IDealerRepository dealerRepository;

        public EditDealerCommandHandler(
            ICurrentUser currentUser, 
            IDealerRepository dealerRepository)
        {
            this.currentUser = currentUser;
            this.dealerRepository = dealerRepository;
        }

        public async Task<Result> Handle(
            EditDealerCommand request, 
            CancellationToken cancellationToken)
        {
            var dealer = await this.dealerRepository.FindByUser(
                this.currentUser.UserId,
                cancellationToken);

            if (request.Id != dealer.Id)
            {
                return "You cannot edit this dealer.";
            }

            dealer
                .EditName(request.Name)
                .EditPhoneNumber(request.PhoneNumber);

            await this.dealerRepository.Save(dealer, cancellationToken);

            return Result.Success;
        }
    }
}