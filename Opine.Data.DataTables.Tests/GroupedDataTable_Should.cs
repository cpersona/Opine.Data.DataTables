using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class GroupedDataTable_Should
    {
        private IDataTable GetSourceDataTable()
        {
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetColumnIndex("Name")).Returns(2);
            t.Setup(x => x.GetColumnCount()).Returns(3);
            t.Setup(x => x.GetValue(3, 2)).Returns("test");
            t.Setup(x => x.GetRow(0)).Returns(new DataRow(t.Object, 3));
            return t.Object;
        }

        private IGroupedDataTable GetGroupedDataTable()
        {
            return new GroupedDataTable(GetSourceDataTable(), new[] { new[] { 3 } }, new[] { "Name" });
        }

        [TestMethod]
        public void GetColumnIndex()
        {
            var t = GetGroupedDataTable();
            Assert.AreEqual(0, t.GetColumnIndex("Name"));
        }

        [TestMethod]
        public void GetColumnCount()
        {
            var t = GetGroupedDataTable();
            Assert.AreEqual(1, t.GetColumnCount());
        }

        [TestMethod]
        public void GetValue()
        {
            var t = GetGroupedDataTable();
            Assert.AreEqual("test", t.GetValue(0, 0));
        }

        [TestMethod]
        public void GetRow()
        {
            var t = GetGroupedDataTable();
            Assert.AreEqual(0, t.GetRow(0).GetRowIndex());
        }

        [TestMethod]
        public void GetGroupedRows()
        {
            var t = GetGroupedDataTable();
            var gr = t.GetGroupedRows(0);
            Assert.AreEqual(1, t.Count());
            Assert.AreEqual("test", t.GetRow(0).GetValue("Name"));
        }
    }
}