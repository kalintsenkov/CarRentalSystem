namespace CarRentalSystem.Application.Features.Identity.Commands.RegisterUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dealers;
    using Domain.Factories.Dealers;
    using MediatR;

    public class RegisterUserCommand : UserInputModel, IRequest<Result>
    {
        public RegisterUserCommand(string email, string password, string name, string phoneNumber)
            : base(email, password)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
        }

        public string Name { get; }

        public string PhoneNumber { get; }


        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
        {
            private readonly IIdentity identity;
            private readonly IDealerFactory dealerFactory;
            private readonly IDealerRepository dealerRepository;

            public RegisterUserCommandHandler(
                IIdentity identity, 
                IDealerFactory dealerFactory, 
                IDealerRepository dealerRepository)
            {
                this.identity = identity;
                this.dealerFactory = dealerFactory;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(
                RegisterUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Register(request);
                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                var dealer = this.dealerFactory
                    .WithName(request.Name)
                    .WithPhoneNumber(request.PhoneNumber)
                    .Build();
                
                user.BecomeDealer(dealer);

                await this.dealerRepository.Save(dealer, cancellationToken);

                return result;
            }
        }
    }
}