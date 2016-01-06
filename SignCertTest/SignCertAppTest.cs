using System.Threading;
using NUnit.Framework;
using SignCert;
using SignCert.Common;

namespace SignCertTest
{
    public class SignCertAppTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AddTaskTest()
        {
            CommonData.WorkInUnitTestModel = true;

            var sca = new SignCertApp();

            //Test 0 task
            Assert.AreEqual(0, sca.GetWorkingItemCount());

            //Test 3 tasks
            sca.AddLoanGuaranteeContractTask(1);
            sca.AddLoanGuaranteeContractTask(2);
            Assert.AreEqual(2, sca.GetWorkingItemCount());

            //Test 3 tasks
            sca.AddLoanGuaranteeContractTask(3);
            Assert.AreEqual(3, sca.GetWorkingItemCount());

            //Start execute task
            sca.Start();

            //Wait 5 seconds, then assert
            Thread.Sleep(5 * 1000);
            Assert.AreEqual(0, sca.GetWorkingItemCount()); //Must be 0, because we have executed it (one task per second)

            sca.Stop();
        }
    }
}