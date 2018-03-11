using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Opine.Data.DataTables
{
    public class DataTable : IDataTable
    {
        private object[] values;
        private Dictionary<string, int> columnIndices;

        protected DataTable() { }

        public DataTable(string[] columnNames, object[] values)
        {
            this.values = values;
            this.columnIndices = columnNames
                .Select((x, i) => new { x, i })
                .ToDictionary(x => x.x, x => x.i);
        }

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
            var i = (rowIndex * columnIndices.Keys.Count) + columnIndex;
            return values[i];
        }

        public virtual IGroupedDataTable GroupBy(string[] columnNames)  
        {
            return DataTableHelper.Group(this, columnNames);
        }

        public virtual IDataTable FilterBy(IRowFilter[] filters, bool matchAll)
        {
            return DataTableHelper.Filter(this, filters, matchAll);
        }

        public IDataRow GetRow(int rowIndex)
        {
            return new DataRow(this, rowIndex);
        }

        public virtual IEnumerator<IDataRow> GetEnumerator()
        {
            var colCount = GetColumnCount();
            for (int i = 0; i < values.Length; i += colCount)
            {
                yield return new DataRow(this, i / colCount);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
