namespace Opine.Data.DataTables
{
    public interface IRowFilter
    {
        bool IsMatch(IDataRow row);
    }
}
