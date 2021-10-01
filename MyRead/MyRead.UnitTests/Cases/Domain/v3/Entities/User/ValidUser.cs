using Bogus;
using MyRead.Domain.Entities;
using MyRead.UnitTests.Core;

namespace MyRead.UnitTests.Cases.Domain.v3.Entities.User
{
    public class ValidUser : RuleModel<UserEntity>
    {
        // Construtor usado pelo autofixture:
        public ValidUser(Faker bogus)
            : this(bogus, _name: null) { }

        // Construtor usado por outros estados de user
        protected ValidUser(Faker bogus, string _name = null, int? _age = null, bool? _isProfessional = null)
            : base(new UserEntity
            (
                name: _name ?? bogus.Name.FullName(),
                age: _age ?? bogus.Random.Int(0, 100),
                isProfessional: _isProfessional ?? bogus.Random.Bool()
            ))
        { }
    }
}