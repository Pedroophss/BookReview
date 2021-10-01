using System;

namespace MyRead.Application.Stories
{
    public class UserRegister
    {
        public byte Age { get; init; }
        public string Name { get; init; }
        public bool IsProfessional { get; init; }
        public ReadBook[] ReadBooks { get; init; }

        public class ReadBook
        {
            public Guid BookId { get; init; }
            public byte? Rating { get; init; }
        }
    }
}