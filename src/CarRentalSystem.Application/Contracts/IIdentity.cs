namespace CarRentalSystem.Application.Contracts
{
    using System.Threading.Tasks;
    using Features.Identity;

    public interface IIdentity
    {
        Task<Result> Register(UserInputModel userInput);

        Task<Result<LoginOutputModel>> Login(UserInputModel userInput);
    }
}