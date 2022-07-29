namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser;

public class LoginOutputModel
{
    public LoginOutputModel(int dealerId, string token)
    {
        this.DealerId = dealerId;
        this.Token = token;
    }

    public int DealerId { get; }

    public string Token { get; }
}