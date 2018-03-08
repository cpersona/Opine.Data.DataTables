namespace Opine.Data.DataTables
{
    public interface IGroupedDataTable : IDataTable
    {
        IDataTable GetGroupedRows(int rowIndex);
    }
}
