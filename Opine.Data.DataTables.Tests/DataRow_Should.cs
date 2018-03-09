using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class DataRow_Should
    {
        [TestMethod]
        public void GetRowIndex()
        {
            var r = new DataRow(null, 1);
            Assert.AreEqual(1, r.GetRowIndex());
        }

        [TestMethod]
        public void GetTable()
        {
            var t = new Mock<IDataTable>();
            var r = new DataRow(t.Object, 1);
            Assert.AreEqual(t.Object, r.GetTable());
        }

        [TestMethod]
        public void GetValueByIndex()
        {
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetValue(It.Is<int>(y => y == 1), It.Is<int>(y => y == 1))).Returns("cat");
            var r = new DataRow(t.Object, 1);
            Assert.AreEqual("cat", r.GetValue(1));
        }

        [TestMethod]
        public void GetValueByColumnName()
        {
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetColumnIndex(It.Is<string>(y => y == "pet"))).Returns(2);
            t.Setup(x => x.GetValue(It.Is<int>(y => y == 1), It.Is<int>(y => y == 2))).Returns("cat");
            var r = new DataRow(t.Object, 1);
            Assert.AreEqual("cat", r.GetValue("pet"));
        }
    }
}
