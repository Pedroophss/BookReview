using AutoFixture.Xunit2;
using MyRead.Domain.Entities;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace MyRead.UnitTests.Exemplo
{
    [Trait("Example", "Autofixture")]
    public class AutoFixture
    {
        public AutoFixture(ITestOutputHelper output)
        {
            Output = output;
        }

        public ITestOutputHelper Output { get; }

        [AutoData]
        [Theory(DisplayName = "AutoFixture Tests")]
        public void Example(int numero, string palavra, UserEntity user)
        {
            Output.WriteLine(numero.ToString());
            Output.WriteLine(palavra);
            Output.WriteLine(JsonSerializer.Serialize(user));
        }
    }
}