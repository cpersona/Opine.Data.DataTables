using System;

namespace Opine.Data.DataTables
{
    public class DataRow : IDataRow
    {
        private IDataTable table;
        private int rowIndex;

        public object this[string columnName] => this.GetValue(columnName);
        
        public object this[int index] => this.GetValue(index);

        protected DataRow() { }

        public DataRow(IDataTable table, int rowIndex)
        {
            this.table = table;
            this.rowIndex = rowIndex;
        }

        public int GetRowIndex()
        {
            return rowIndex;
        }

        public IDataTable GetTable()
        {
            return table;
        }

        public object GetValue(int index)
        {
            return table.GetValue(rowIndex, index);
        }

        public object GetValue(string columnName)
        {
            var i = table.GetColumnIndex(columnName);
            return GetValue(i);
        }

        public Guid? GetGuid(int index)
        {
            var value = GetValue(index);
            return ToGuid(value);
        }

        public Guid? GetGuid(string columnName)
        {
            var value = GetValue(columnName);
            return ToGuid(value);
        }

        public decimal? GetDecimal(int index)
        {
            return ToDecimal(GetValue(index));
        }

        public decimal? GetDecimal(string columnName)
        {
            return ToDecimal(GetValue(columnName));
        }

        private Guid? ToGuid(object value)
        {
            if (value is null) return null;
            if (value is Guid) return (Guid)value;
            return Guid.Parse(value.ToString());
        }

        private decimal? ToDecimal(object value)
        {
            if (value is null) return null;
            if (value is decimal) return (decimal)value;
            return decimal.Parse(value.ToString());
        }
    }
}
