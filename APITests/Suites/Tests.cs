using APITests.Pages;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace APITests.Suites
{
    [TestFixture]   
    public class Tests
    {
        ConvertToPdf con = new ConvertToPdf();

        [TestCase(TestName = "Upload DOCX file", Author = "Ayrat Khaliullin")]
        [Order(1)]
        public void UploadDoc()
        {
            var response = con.UploadDocument();

            if (response.ContainsKey("server_filename"))
            {
                con.serverFilename = response["server_filename"].Value<string>();
                Assert.Pass();
            }
            else
            {
                con.stopTest = true;
                Assert.Fail("Document was not upload");
            }
        }

        [TestCase(TestName = "Convert file to PDF", Author = "Ayrat Khaliullin")]
        [Order(2)]
        public void Converting()
        {
            if (con.stopTest) Assert.Ignore("TestIgnored due previous errors");
            var response = con.ConvertToPdfProcess();

            if (response["status"].Value<string>()=="TaskSuccess")
            {
                Assert.Pass();
            }
            else
            {
                con.stopTest = true;
                Assert.Fail("Document was not converted to pdf");
            }
        }

        [TestCase(TestName = "Download PDF file from server",Author ="Ayrat Khaliullin")]
        [Order(3)]
        public void DownloadPdf()
        {
            if (con.stopTest) Assert.Ignore("TestIgnored due previous errors");
            var response = con.DownloadPdf();

            Assert.That(response.ContentType, Is.EqualTo("application/pdf"));
        }
    }
}