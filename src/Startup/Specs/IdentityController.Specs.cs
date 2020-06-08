namespace CarRentalSystem.Startup.Specs
{
    using Application.Features.Identity.Commands.LoginUser;
    using Application.Features.Identity.Commands.RegisterUser;
    using FluentAssertions;
    using Infrastructure.Identity;
    using MyTested.AspNetCore.Mvc;
    using Web.Features;
    using Xunit;

    public class IdentityControllerSpecs
    {
        [Theory]
        [InlineData(
            IdentityFakes.TestEmail,
            IdentityFakes.ValidPassword,
            JwtTokenGeneratorFakes.ValidToken)]
        public void LoginShouldReturnTokenWhenUserEntersValidCredentials(string email, string password, string token)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Identity/Login")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Email = email,
                        Password = password
                    }))
                .To<IdentityController>(c => c
                    .Login(new LoginUserCommand(email, password)))
                .Which()
                .ShouldReturn()
                .ActionResult<LoginOutputModel>(result => result
                    .Passing(model => model.Token.Should().Be(token)));
        [Theory]
        [InlineData(
            IdentityFakes.TestEmail,
            IdentityFakes.ValidPassword,
            IdentityFakes.ValidName,
            IdentityFakes.ValidPhoneNumber)]
        public void RegisterShouldHaveCorrectAttributes(string email, string password, string name, string phoneNumber)
            => MyController<IdentityController>
                .Calling(c => c
                    .Register(new RegisterUserCommand(email, password, name, phoneNumber)))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .SpecifyingRoute(nameof(IdentityController.Register)));

        [Fact]
        public void LoginShouldHaveCorrectAttributes()
            => MyController<IdentityController>
                .Calling(c => c
                    .Login(new LoginUserCommand(With.No<string>(), With.No<string>())))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .SpecifyingRoute(nameof(IdentityController.Login)));
    }
}
