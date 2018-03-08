namespace Opine.Data.DataTables
{
    public class DataRow : IDataRow
    {
        private IDataTable table;
        private int rowIndex;

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
    }
}
