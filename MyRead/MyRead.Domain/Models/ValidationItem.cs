using Microsoft.Extensions.Logging;

namespace MyRead.Domain.Models
{
    public record ValidationItem
        (string Source, string MessageKey, LogLevel LogLevel = LogLevel.Error)
    {
        public static implicit operator ValidationItem((string, string) value) =>
            new (value.Item1, value.Item2);
    }
}