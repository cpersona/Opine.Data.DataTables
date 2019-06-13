using System;

namespace Opine.Data.DataTables
{
    public interface IDataRow
    {
        IDataTable GetTable();
        int GetRowIndex();
        object this[int index] { get; }
        object this[string columnName] { get; }
        object GetValue(int index);
        object GetValue(string columnName);
        Guid? GetGuid(int index);
        Guid? GetGuid(string columnName);
        decimal? GetDecimal(int index);
        decimal? GetDecimal(string columnName);
    }
}
