namespace Opine.Data.DataTables
{
    public interface IGroupedDataRow : IDataRow
    {
        IDataTable GetGroupedRows();
    }
}
