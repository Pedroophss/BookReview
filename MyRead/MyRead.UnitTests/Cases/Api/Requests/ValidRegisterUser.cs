using MyRead.Api.Requests;
using MyRead.UnitTests.Cases.Domain.v3.Entities.User;
using MyRead.UnitTests.Core;

namespace MyRead.UnitTests.Cases.Api.Requests
{
    internal class ValidRegisterUser : RuleModel<RegisterUserRequest>
    {
        public ValidRegisterUser(ValidUser user)
            : base(new RegisterUserRequest()
            {
                Age = user.Object.Age,
                Name = user.Object.Name,
                IsProfessional = user.Object.IsProfessional
            })
        { }
    }
}