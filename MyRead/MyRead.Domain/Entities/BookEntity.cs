using MyRead.Domain.Models;
using MyRead.Domain.Resources;
using System;
using System.Collections.Generic;

namespace MyRead.Domain.Entities
{
    public class BookEntity : Entity
    {
        public BookEntity(string title, string authors, string language, int pages)
            : this(Guid.NewGuid(), title, authors, language, pages) { }

        public BookEntity(Guid id, string title, string authors, string language, int pages)
            : base(id)
        {
            Pages = pages;
            Title = title;
            Authors = authors;
            Language = language;
        }

        public int Pages { get; }
        public string Title { get; }
        public string Authors { get; }
        public string Language { get; }

        public override IEnumerable<ValidationItem> Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                yield return new(nameof(BookEntity), nameof(DomainStrs.BookNameRequired));

            if (string.IsNullOrWhiteSpace(Authors))
                yield return new(nameof(BookEntity), nameof(DomainStrs.BookAuthorRequired));

            if (Pages == 0)
                yield return new(nameof(BookEntity), nameof(DomainStrs.BookPagesRequired));
        }
    }
}