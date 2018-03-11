using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Opine.Data.DataTables.Tests
{
    [TestClass]
    public class DataTableHelper_Should
    {
        private DataTable GetTable()
        {
            var columns = new[] { "Name", "Age", "State" };
            var values = new object[] 
            {
                "Bob", 33, "NY",
                "Tim", 22, "CA",
                "Jim", 22, "CA",
                "Sam", 33, "CA",
            };
            return new DataTable(columns, values);
        }


        [TestMethod]
        public void Group()
        {
            var t = GetTable();
            var g = DataTableHelper.Group(t, new[] { "State", "Age" });
            Assert.AreEqual(3, g.Count());
            Assert.AreEqual("NY", g.GetRow(0).GetValue("State"));
            Assert.AreEqual(33, g.GetRow(0).GetValue("Age"));
            Assert.AreEqual("CA", g.GetRow(1).GetValue("State"));
            Assert.AreEqual(22, g.GetRow(1).GetValue("Age"));
            Assert.AreEqual("CA", g.GetRow(2).GetValue("State"));
            Assert.AreEqual(33, g.GetRow(2).GetValue("Age"));
        }

        [TestMethod]
        public void Filter()
        {
            {
                var filter = new Mock<IRowFilter>();
                filter.Setup(x => x.IsMatch(It.Is<IDataRow>(y => y.GetValue("State").ToString() == "CA"))).Returns(true);
                var t = GetTable();
                var f = DataTableHelper.Filter(t, new[] { filter.Object }, true);
                Assert.AreEqual(3, f.Count());
            }

            {
                var filter = new Mock<IRowFilter>();
                filter.Setup(x => x.IsMatch(It.Is<IDataRow>(y => Convert.ToInt32(y.GetValue("Age")) == 33))).Returns(true);
                var t = GetTable();
                var f = DataTableHelper.Filter(t, new[] { filter.Object }, true);
                Assert.AreEqual(2, f.Count());
            }

            {
                var filter = new Mock<IRowFilter>();
                filter.Setup(x => x.IsMatch(It.Is<IDataRow>(y => y.GetValue("State").ToString() == "AZ"))).Returns(true);
                var t = GetTable();
                var f = DataTableHelper.Filter(t, new[] { filter.Object }, true);
                Assert.AreEqual(0, f.Count());
            }
        }
    }
}