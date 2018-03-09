using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class DataTable_Should
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
        public void GetValues()
        {
            var table = GetDataTable();
            Assert.AreEqual(6, table.Values.Length);
            Assert.AreEqual("Bob", table.Values[0]);
            Assert.AreEqual(43, table.Values[5]);
        }

        [TestMethod]
        public void GetColumnIndex()
        {
            var table = GetDataTable();
            Assert.AreEqual(0, table.GetColumnIndex("Name"));
            Assert.AreEqual(1, table.GetColumnIndex("Age"));
        }

        [TestMethod]
        public void GetColumnCount()
        {
            var table = GetDataTable();
            Assert.AreEqual(2, table.GetColumnCount());
        }

        [TestMethod]
        public void GetValueByIndex()
        {
            var table = GetDataTable();
            Assert.AreEqual(43, table.GetValue(2, 2));
        }

        [TestMethod]
        public void GetRow()
        {
            var table = GetDataTable();
            var r = table.GetRow(1);
            Assert.IsNotNull(r);
            Assert.AreEqual(1, r.GetRowIndex());
        }

        [TestMethod]
        public void GetEnumerator()
        {
            var table = GetDataTable();
            var e = table.GetEnumerator();
            int i = 0;
            while (e.MoveNext())
            {
                i++;
            }
            Assert.AreEqual(3, i);
        }
    }
}