using MyRead.Domain.Models;
using MyRead.Domain.Resources;
using System;
using System.Collections.Generic;

namespace MyRead.Domain.Entities
{
    public class UserEntity : Entity
    {
        public UserEntity(string name, int age, bool isProfessional)
            : this(Guid.NewGuid(), name, age, isProfessional) { }

        public UserEntity(Guid id, string name, int age, bool isProfessional)
            : base(id)
        {
            Age = age;
            Name = name;
            IsProfessional = isProfessional;
        }

        public int Age { get; }
        public string Name { get; }
        public bool IsProfessional { get; }



        public override IEnumerable<ValidationItem> Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new(nameof(UserEntity), nameof(DomainStrs.UserNameRequired));
        }
    }
}