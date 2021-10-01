using MyRead.Domain.Models;
using System.Collections.Generic;
using System.Globalization;

namespace MyRead.Application.Abstractions
{
    public interface INotifier
    {
        bool HasError { get; }

        void Notify(params ValidationItem[] validationItems);

        void Notify(IEnumerable<ValidationItem> validationItems);

        IEnumerable<string> GetErros(CultureInfo culture = null);

        void LogMessages(CultureInfo culture = null);
    }
}