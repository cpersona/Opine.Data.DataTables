using System;

namespace Opine.Data.DataTables.Filters
{
    public class EqualsFilter : IRowFilter
    {
        private string fieldName;
        private string value;

        public EqualsFilter(string fieldName, string value)
        {
            this.fieldName = fieldName;
            this.value = value;
        }

        public bool IsMatch(IDataRow row)
        {
            var fieldValue = row.GetValue(fieldName);
            if (fieldValue == null && value == null) return true;
            if (fieldValue == null) return false;

            return fieldValue.ToString().Equals(value, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}