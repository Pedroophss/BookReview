using MyRead.Domain.Abstractions;
using MyRead.Domain.Delegates;
using MyRead.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRead.Domain.Entities
{
    public abstract class Entity : IValidatable
    {
        public Guid Id { get; }

        protected Entity(Guid id) =>
            Id = id;

        public abstract IEnumerable<ValidationItem> Validate();

        public event PersistedHandler AfterCommited;

        internal async Task TriggerAfterCommited(PersistedEventArgs args) =>
            await AfterCommited?.Invoke(args);
    }
}