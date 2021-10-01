using System;

namespace MyRead.Bind.ValueObjects
{
    // TODO: Refatorar para uma implementação performatica
    public ref struct CsvRow
    {
        string[] Columns { get; }

        public CsvRow(string row)
        {
            Columns = row.Split(',');
        }

        public T GetColumn<T>(ushort column) =>
            (T)Convert.ChangeType(Columns[column], typeof(T));
    }
}