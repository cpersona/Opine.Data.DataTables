using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Opine.Data.DataTables
{
    public class GroupedDataTable : IGroupedDataTable
    {
        private IDataTable parentDataTable;
        private int[][] rowIndices;
        private string[] groupingFields;
        private Dictionary<string, int> groupingFieldIndices;

        public GroupedDataTable(IDataTable parentDataTable, int[][] rowIndices, string[] groupingFields)
        {
            this.parentDataTable = parentDataTable;
            this.rowIndices = rowIndices;
            this.groupingFields = groupingFields;
            groupingFieldIndices = groupingFields
                .Select((f, i) => new { f, i })
                .ToDictionary(
                    x => x.f, 
                    x => x.i);
        }

        public virtual int GetColumnCount()
        {
            return groupingFields.Length;
        }

        public virtual int GetColumnIndex(string columnName)
        {
            return groupingFieldIndices[columnName];
        }

        public virtual object GetValue(int rowIndex, int columnIndex)
        {
            // Get the value at the parent's location of the column
            // Using the first row in the group as indicative
            var i = parentDataTable.GetColumnIndex(groupingFields[columnIndex]);
            return parentDataTable.GetValue(rowIndices[rowIndex][0], i);
        }

        public virtual IEnumerator<IDataRow> GetEnumerator()
        {
            for (int i = 0; i < rowIndices.Length; i++)
            {
                yield return new GroupedDataRow(this, i);
            }
        }

        public IDataTable GetGroupedRows(int rowIndex)
        {
            return new FilteredDataTable(parentDataTable, rowIndices[rowIndex]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IDataRow GetRow(int rowIndex)
        {
            return new GroupedDataRow(this, rowIndex);
        }

        public IGroupedDataTable GroupBy(string[] columnNames)  
        {
            return DataTableHelper.Group(this, columnNames);
        }

        public IDataTable FilterBy(IRowFilter[] filters, bool matchAll)
        {
            return DataTableHelper.Filter(this, filters, matchAll);
        }
    }
}
