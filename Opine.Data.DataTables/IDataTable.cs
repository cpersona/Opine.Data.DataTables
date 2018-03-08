using System.Collections.Generic;

namespace Opine.Data.DataTables
{
    public interface IDataTable : IEnumerable<IDataRow>
    {
        int GetColumnIndex(string columnName);
        int GetColumnCount();
        object GetValue(int rowIndex, int columnIndex);
        IGroupedDataTable GroupBy(string[] columnNames);
        IDataTable FilterBy(IRowFilter[] filters, bool matchAll);
        IDataRow GetRow(int rowIndex);
    }
}
