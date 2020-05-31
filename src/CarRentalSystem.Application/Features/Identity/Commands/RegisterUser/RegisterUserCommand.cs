namespace CarRentalSystem.Application.Features.Identity.Commands.RegisterUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class RegisterUserCommand : UserInputModel, IRequest<Result>
    {
        public RegisterUserCommand(string email, string password)
            : base(email, password)
        {
        }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
        {
            private readonly IIdentity identity;

            public RegisterUserCommandHandler(IIdentity identity) => this.identity = identity;

            public async Task<Result> Handle(
                RegisterUserCommand request,
                CancellationToken cancellationToken)
                => await this.identity.Register(request);
        }
    }
}