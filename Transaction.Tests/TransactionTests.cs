using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Transactions;

namespace TransactionTests
{
    [TestFixture]
    public class TransactionTests
    {

        [Test]
        public void CreateBasicTransaction()
        {
            var a = 0;
            var testTransaction = new Transaction(() => { a = 3; });
            Assert.AreEqual(0, a);
        }

        [Test]
        public void ExecuteBasicTransaction()
        {
            var a = 0;
            var testTransaction = new Transaction(() => { a = 3; });
            testTransaction.Commit();
            Assert.AreEqual(3, a);
        }
    }
}
