using NUnit.Framework;
using SignCert.Business.CertSignHelper;
using SignCert.Common;
using iTextSharp.text;

namespace SignCertTest.Business
{
    /// <summary>
    /// 标准的测试模板
    /// </summary>
    [TestFixture]
    public class WordsCertSignTest
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
        public void SignTest()
        {
            CommonData.WorkInUnitTestModel = true;

            var wcs = new WordsCertSign();
            var document = new Document();
            const string keyword = "甲方同意：20150512";
            document.Open();

            wcs.Init(document, 1, "andy", keyword, "甲方");

            var result = wcs.Sign();

            Assert.IsTrue(result);
        }
    }
}
