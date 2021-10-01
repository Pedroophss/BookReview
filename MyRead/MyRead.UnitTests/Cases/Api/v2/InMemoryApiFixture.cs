using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace MyRead.UnitTests.Cases.Api.v2
{
    public class InMemoryApiFixture : IDisposable
    {
        public TestServer Server { get; }
        public HttpClient Client { get; }

        public InMemoryApiFixture()
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

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }
    }

    [CollectionDefinition("InMemoryApi")]
    public class ApiCollection : ICollectionFixture<InMemoryApiFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}