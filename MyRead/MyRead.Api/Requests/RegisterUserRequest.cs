using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Application.Services;
using MyRead.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Api.Requests
{
    public class RegisterUserRequest : IRequest<UserEntity>
    {
        public int Age { get; init; }
        public string Name { get; init; }
        public bool IsProfessional { get; init; }
        public ReadBook[] ReadBooks { get; init; }

        public class ReadBook
        {
            public Guid BookId { get; init; }
            public byte Rating { get; init; }
        }
    }

    internal class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, UserEntity>
    {
        public RegisterUserRequestHandler(IUserService users, IRatingService ratings, IUnitOfWork uow)
        {
            Uow = uow;
            Users = users;
            Ratings = ratings;
        }

        IUnitOfWork Uow { get; }
        IUserService Users { get; }
        IRatingService Ratings { get; }

        public async Task<UserEntity> Handle(RegisterUserRequest r, CancellationToken token)
        {
            var user = new UserEntity(r.Name, r.Age, r.IsProfessional);
            await Users.NewUser(user, token);

            if (r.ReadBooks is not null)
                foreach (var rating in r.ReadBooks.Select(s => new BookRatingEntity(user.Id, s.BookId, s.Rating)))
                    await Ratings.SaveRating(rating, token);            

            await Uow.Commit(token);

            return user;
        }
    }
}