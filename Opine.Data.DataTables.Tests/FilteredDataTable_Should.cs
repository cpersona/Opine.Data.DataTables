using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class FilteredDataTable_Should
    {
        private IDataTable GetSourceDataTable()
        {
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetColumnIndex("Name")).Returns(2);
            t.Setup(x => x.GetColumnCount()).Returns(3);
            t.Setup(x => x.GetValue(3, 2)).Returns("test");
            t.Setup(x => x.GetRow(3)).Returns(new DataRow(t.Object, 3));
            return t.Object;
        }

        public FilteredDataTable GetTable()
        {
            return new FilteredDataTable(GetSourceDataTable(), new int[] { 3 });
        }

        [TestMethod]
        public void GetColumnIndex() 
        {
            var t = GetTable();
            Assert.AreEqual(2, t.GetColumnIndex("Name"));
        }

        [TestMethod]
        public void GetColumnCount()
        {
            var t = GetTable();
            Assert.AreEqual(3, t.GetColumnCount());
        }

        [TestMethod]
        public void GetValue()
        {
            var t = GetTable();
            Assert.AreEqual("test", t.GetRow(0).GetValue("Name"));
            Assert.AreEqual("test", t.GetRow(0).GetValue(2));
        }

        [TestMethod]
        public void GetRow()
        {
            var t = GetTable();
            var r = t.GetRow(0);
            Assert.AreEqual(0, r.GetRowIndex());
        }
    }
}