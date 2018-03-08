namespace Opine.Data.DataTables
{
    public interface IDataRow
    {
        IDataTable GetTable();
        int GetRowIndex();
        object GetValue(int index);
        object GetValue(string columnName);
    }
}
