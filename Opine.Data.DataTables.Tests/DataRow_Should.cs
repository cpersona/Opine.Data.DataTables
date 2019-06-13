using System;
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
            t.Setup(x => x.GetValue(1, 1)).Returns("cat");
            var r = new DataRow(t.Object, 1);
            Assert.AreEqual("cat", r.GetValue(1));
            Assert.AreEqual("cat", r[1]);
        }

        [TestMethod]
        public void GetValueByColumnName()
        {
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetColumnIndex("pet")).Returns(2);
            t.Setup(x => x.GetValue(1, 2)).Returns("cat");
            var r = new DataRow(t.Object, 1);
            Assert.AreEqual("cat", r.GetValue("pet"));
            Assert.AreEqual("cat", r["pet"]);
        }

        [TestMethod]
        public void GetGuidValue()
        {
            var value = Guid.NewGuid();
            var value2 = Guid.NewGuid();
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetValue(1, 1)).Returns(value);
            t.Setup(x => x.GetValue(1, 2)).Returns(value2.ToString());
            t.Setup(x => x.GetValue(1, 3)).Returns(null);

            var r = new DataRow(t.Object, 1);
            Assert.AreEqual(value, r.GetGuid(1));
            Assert.AreEqual(value2, r.GetGuid(2));
            Assert.AreEqual(null, r.GetGuid(3));
        }
        
        [TestMethod]
        public void GetDecimalValue()
        {
            var value = 1m;
            var value2 = 2.2m;
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetValue(1, 1)).Returns(value);
            t.Setup(x => x.GetValue(1, 2)).Returns(value2.ToString());
            t.Setup(x => x.GetValue(1, 3)).Returns(null);

            var r = new DataRow(t.Object, 1);
            Assert.AreEqual(value, r.GetDecimal(1));
            Assert.AreEqual(value2, r.GetDecimal(2));
            Assert.AreEqual(null, r.GetGuid(3));
        }

        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void FailGuidValue()
        {
            var t = new Mock<IDataTable>();
            t.Setup(x => x.GetValue(1, 1)).Returns("cat");
            var r = new DataRow(t.Object, 1);
            var neverHappens = r.GetGuid(1);
        }
    }
}
