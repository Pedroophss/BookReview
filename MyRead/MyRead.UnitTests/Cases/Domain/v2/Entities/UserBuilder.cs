using Bogus;
using MyRead.Domain.Entities;

namespace MyRead.UnitTests.Cases.Domain.v2.Entities
{
    public class UserBuilder
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public bool IsProfessional { get; set; }

        public UserBuilder()
        {
            var bogus = new Faker();

            Name = bogus.Name.FullName();
            Age = bogus.Random.Int(0, 100);
            IsProfessional = bogus.Random.Bool();
        }

        public UserBuilder WithEmptyName()
        {
            Name = string.Empty;
            return this;
        }

        public UserEntity Build() =>
            new(Name, Age, IsProfessional);
    }
}