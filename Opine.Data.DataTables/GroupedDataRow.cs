namespace Opine.Data.DataTables
{
    public class GroupedDataRow : DataRow, IGroupedDataRow
    {
        private GroupedDataRow() { }
        
        public GroupedDataRow(IGroupedDataTable table, int rowIndex) : base(table, rowIndex)
        {
        }

        public IDataTable GetGroupedRows()
        {
            var table = GetTable() as IGroupedDataTable;
            return table.GetGroupedRows(GetRowIndex());
        }
    }
}
