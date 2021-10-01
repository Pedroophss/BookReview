using MyRead.Domain.Models;
using System.Collections.Generic;

namespace MyRead.Domain.Abstractions
{
    public interface IValidatable
    {
        IEnumerable<ValidationItem> Validate();
    }
}