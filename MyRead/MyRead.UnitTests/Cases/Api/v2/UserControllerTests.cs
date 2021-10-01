using MyRead.UnitTests.Cases.Api.Requests;
using MyRead.UnitTests.Core;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyRead.UnitTests.Cases.Api.v2
{
    [Collection("InMemoryApi")]
    [Trait("API", "Controllers V2")]
    public class UserControllerTests
    {
        public InMemoryApiFixture Fixture { get; }

        public UserControllerTests(InMemoryApiFixture fixture) =>
            Fixture = fixture;

        [Theory(DisplayName = "When valid user"), AutoAssert]
        internal async Task UserController_ValidUser_ShouldReturnCreated(ValidRegisterUser request)
        {
            // Act
            var response = await Fixture.Client.PostAsync("users", JsonContent.Create(request.Object));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Theory(DisplayName = "When invalid user"), AutoAssert]
        internal async Task UserController_ValidUser_ShouldReturnBadRequest(InvalidRegisterUser request)
        {
            // Act
            var response = await Fixture.Client.PostAsync("users", JsonContent.Create(request.Object));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}