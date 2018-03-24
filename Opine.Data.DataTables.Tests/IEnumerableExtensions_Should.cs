using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class IEnumerableExtensions_Should
    {
        private DataTable GetDataTable()
        {
            var columns = new string[] 
            {
                "Name", "Age"
            };
            var values = new object[] 
            { 
                "Bob", 55,
                "Tom", 32,
                "Jim", 43
            };
            return new DataTable(columns, values);
        }
        
        [TestMethod]
        public void ToTable()
        {
            var rows = GetDataTable().Where(x => (int)x.GetValue("Age") > 40);
            var table = rows.ToDataTable();
            Assert.AreEqual(2, table.Count());
            Assert.AreEqual(55, table.GetValue(0, 1));
            Assert.AreEqual(43, table.GetValue(1, 1));
        }
    }
}