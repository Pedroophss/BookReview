using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace MyRead.UnitTests.Core
{
    public class AutoAssertAttribute : AutoDataAttribute
    {
        private static Fixture CreateCustomFixture()
        {
            var fixture = new Fixture();

            // Injetando em todos os testes com o [AutoAssert]:
            // - Faker do bogus
            // - Geração automatica de mocks usando NSubstitute
            fixture.Customize(new AutoNSubstituteCustomization())
                   .Inject(new Bogus.Faker());

            return fixture;
        }

        public AutoAssertAttribute()
            : base(() => CreateCustomFixture()) { }
    }
}