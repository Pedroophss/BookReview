using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration.Memory;
using MyRead.UnitTests.Cases.Api.Requests;
using MyRead.UnitTests.Core;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyRead.UnitTests.Cases.Api.v1
{
    [Trait("API", "Controllers")]
    public class UserControllerTests : IDisposable
    {
        public UserControllerTests()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<MyRead.Api.Startup>()
                .ConfigureAppConfiguration((ctx, config) =>
                {
                    config.Add(new MemoryConfigurationSource
                    {
                        InitialData = new Dictionary<string, string>
                        {
                            {"Database:Provider","InMemory"  }
                        }
                    });
                }));

            Client = Server.CreateClient();
        }

        private TestServer Server { get; }
        private HttpClient Client { get; }

        [Theory(DisplayName = "When valid user"), AutoAssert]
        internal async Task UserController_ValidUser_ShouldReturnCreated(ValidRegisterUser request)
        {
            // Act
            var response = await Client.PostAsync("users", JsonContent.Create(request.Object));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Theory(DisplayName = "When invalid user"), AutoAssert]
        internal async Task UserController_ValidUser_ShouldReturnBadRequest(InvalidRegisterUser request)
        {
            // Act
            var response = await Client.PostAsync("users", JsonContent.Create(request.Object));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        public void Dispose()
        {
            Server.Dispose();
        }
    }
}