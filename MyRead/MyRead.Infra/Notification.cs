using Microsoft.Extensions.Logging;
using MyRead.Application.Abstractions;
using MyRead.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyRead.Infra
{
    internal class Notification : INotifier
    {
        private ILogger Logger { get; }
        private Lazy<List<ValidationItem>> Messages { get; }

        public bool HasError =>
            Messages.IsValueCreated
            && Messages.Value.Any(a => a.LogLevel == LogLevel.Error || a.LogLevel == LogLevel.Critical);

        public Notification(ILoggerFactory log)
        {
            Logger = log.CreateLogger(nameof(INotifier));
            Messages = new Lazy<List<ValidationItem>>();
        }

        public void Notify(params ValidationItem[] validationItems)
        {
            if (validationItems.Length != 0)
                Messages.Value.AddRange(validationItems);
        }

        public void Notify(IEnumerable<ValidationItem> validationItems)
        {
            if (validationItems.Any())
                Messages.Value.AddRange(validationItems);
        }

        public IEnumerable<string> GetErros(CultureInfo culture = null)
        {
            if (!Messages.IsValueCreated)
                yield break;

            var _culture = culture ?? CultureInfo.CurrentCulture;
            foreach (var item in Messages.Value)
                yield return $"{item.Source} : {GetCorrectString(item.MessageKey, _culture)}";
        }

        public void LogMessages(CultureInfo culture = null)
        {
            if (Messages.IsValueCreated)
            {
                var _culture = culture ?? CultureInfo.CurrentCulture;
                foreach (var item in Messages.Value)
                    Logger.Log(item.LogLevel, $"{item.Source} : {GetCorrectString(item.MessageKey, _culture)}");
            }
        }

        private static string GetCorrectString(string key, CultureInfo culture) =>
            Domain.Resources.DomainStrs.ResourceManager.GetString(key, culture) ?? key;
    }
}