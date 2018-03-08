using System;
using System.Collections;
using System.Collections.Generic;

namespace Opine.Data.DataTables
{
    public class DataTable : IDataTable
    {
        private object[] values;
        private Dictionary<string, int> columnIndices;

        public object[] Values { get => values; }

        public virtual int GetColumnIndex(string columnName)
        {
            return columnIndices[columnName];
        }

        public virtual int GetColumnCount() 
        {
            return columnIndices.Keys.Count;
        }

        public virtual object GetValue(int rowIndex, int columnIndex)
        {
            return values[(rowIndex * columnIndices.Keys.Count) + columnIndex];
        }

        public virtual IEnumerator<IDataRow> GetEnumerator()
        {
            var colCount = GetColumnCount();
            for (int i = 0; i < values.Length; i += colCount)
            {
                yield return new DataRow(this, i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IGroupedDataTable GroupBy(string[] columnNames)  
        {
            return DataTableHelper.Group(this, columnNames);
        }

        public IDataTable FilterBy(IRowFilter[] filters, bool matchAll)
        {
            return DataTableHelper.Filter(this, filters, matchAll);
        }

        public IDataRow GetRow(int rowIndex)
        {
            return new DataRow(this, rowIndex);
        }
    }
}
