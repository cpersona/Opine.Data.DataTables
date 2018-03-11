using System.Collections;
using System.Collections.Generic;

namespace Opine.Data.DataTables
{
    public class FilteredDataTable : IDataTable
    {
        private IDataTable parentDataTable;
        private int[] rowIndices;

        protected FilteredDataTable() { }

        public FilteredDataTable(IDataTable parentDataTable, int[] rowIndices)
        {
            this.parentDataTable = parentDataTable;
            this.rowIndices = rowIndices;
        }

        public IDataTable FilterBy(IRowFilter[] filters, bool matchAll)
        {
            return DataTableHelper.Filter(this, filters, matchAll);
        }

        public virtual int GetColumnCount()
        {
            return parentDataTable.GetColumnCount();
        }

        public virtual int GetColumnIndex(string columnName)
        {
            return parentDataTable.GetColumnIndex(columnName);
        }

        public virtual IEnumerator<IDataRow> GetEnumerator()
        {
            for (int i = 0; i < rowIndices.Length; i++)
            {
                yield return new DataRow(this, i);
            }
        }

        public IDataRow GetRow(int rowIndex)
        {
            return new DataRow(this, rowIndex);
        }

        public virtual object GetValue(int rowIndex, int columnIndex)
        {
            return parentDataTable.GetValue(rowIndices[rowIndex], columnIndex);
        }

        public IGroupedDataTable GroupBy(string[] columnNames)
        {
            return DataTableHelper.Group(this, columnNames);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
