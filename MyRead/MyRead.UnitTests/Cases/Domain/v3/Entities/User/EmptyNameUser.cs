using Bogus;

namespace MyRead.UnitTests.Cases.Domain.v3.Entities.User
{
    public class EmptyNameUser : ValidUser
    {
        public EmptyNameUser(Faker bogus)
            : base(bogus, _name: string.Empty) { }
    }
}