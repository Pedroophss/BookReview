using System.Threading.Tasks;

namespace MyRead.Domain.Delegates
{
    public record PersistedEventArgs(object Entity);

    public delegate Task PersistedHandler(PersistedEventArgs args);
}