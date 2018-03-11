using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class GroupedDataRow_Should
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

        private GroupedDataRow GroupedDataRow()
        {
            return new GroupedDataRow(GetGroupedDataTable(), 0);
        }

        [TestMethod]
        public void GetTable()
        {
            var t = GetGroupedDataTable();
            var r = new GroupedDataRow(t, 0);
            Assert.AreEqual(t, r.GetTable());
        }

        [TestMethod]
        public void GetRowIndex()
        {
            var r = GroupedDataRow();
            Assert.AreEqual(0, r.GetRowIndex());
        }

        [TestMethod]
        public void GetValue()
        {
            var r = GroupedDataRow();
            Assert.AreEqual("test", r.GetValue("Name"));
            Assert.AreEqual("test", r.GetValue(0));
        }

        [TestMethod]
        public void GetGroupedRows()
        {
            var r = GroupedDataRow();
            var t = r.GetGroupedRows();
            Assert.AreEqual(1, t.Count());
            Assert.AreEqual("test", t.GetRow(0).GetValue(2));
        }
    }
}