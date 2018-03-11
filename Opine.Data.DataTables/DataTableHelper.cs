using System.Collections.Generic;
using System.Linq;

namespace Opine.Data.DataTables
{
    public static class DataTableHelper
    {
        public static GroupedDataTable Group(IDataTable parentDataTable, 
            string[] columnNames)
        {
            var groups = new Dictionary<int, List<int>>();
            var columnIndices = columnNames.Select(parentDataTable.GetColumnIndex).ToArray();
            int ri = 0;
            foreach (var r in parentDataTable)
            {
                int hash = 0;
                foreach (var i in columnIndices)
                {
                    hash |= r.GetValue(i).GetHashCode();
                }
                List<int> groupIndices = null;
                if (!groups.TryGetValue(hash, out groupIndices))
                {
                    groupIndices = new List<int>();
                    groups.Add(hash, groupIndices);
                }
                groupIndices.Add(ri);
                ri++;
            }
            var rowIndices = groups.Select(d => d.Value.ToArray()).ToArray();
            return new GroupedDataTable(parentDataTable, rowIndices, columnNames);
        }

        public static FilteredDataTable Filter(IDataTable parentDataTable, 
            IRowFilter[] filters, bool matchAll)
        {
            var indices = new List<int>();
            int i = 0;
            foreach (var r in parentDataTable)
            {
                if (FilterInternal(filters, matchAll, indices, i, r))
                {
                    indices.Add(i);
                }
                i++;
            }
            return new FilteredDataTable(parentDataTable, indices.ToArray());
        }

        private static bool FilterInternal(IRowFilter[] filters, bool matchAll, List<int> indices, int i, IDataRow r)
        {
            var match = true;
            foreach (var f in filters)
            {
                if (f.IsMatch(r))
                {
                    if (!matchAll)
                    {
                        return true;
                    }
                }
                else
                {
                    if (matchAll)
                    {
                        return false;
                    }
                    else
                    {
                        match = false;
                    }
                }
            }
            return true;
        }
    }
}
