using System;
using System.Collections.Generic;
using System.Linq;

namespace Opine.Data.DataTables
{
    public static class IEnumerableExtensions
    {
        public static IDataTable ToDataTable(this IEnumerable<IDataRow> @this)
        {
            if (!@this.Any())
            {
                throw new ArgumentException();
            }

            return new FilteredDataTable(@this.First().GetTable(), @this.Select(x => x.GetRowIndex()).ToArray());
        }
    }
}