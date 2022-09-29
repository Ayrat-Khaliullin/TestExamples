using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Threading;
using UITests.Pages;
using UITests.Utilities;
using UITests.Utilities.Bases;
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace UITests.Suites
{
    
    [AllureFeature("Google tests")]
    [TestFixture]
    [AllureNUnit]
    public class GoogleTests : BaseTest
    {
        BaseDriver driverInstance; 
        GoogleMainPage mainpage;

        [SetUp]
        public void Setup()
        {
            driverInstance = new BaseDriver();
            mainpage = new GoogleMainPage(driverInstance);
        }
        [TearDown]
        public void Teardown()
        {
            driverInstance.AttachScreenToReport();
            driverInstance.Dispose();
        }

        [TestCase(TestName = "Search form is displayed", Author = "Ayrat Khaliullin")]
        public void SearchFormTest()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(mainpage.FindSearchForm(), "Search form wasn't find at the page");
            });
        }

        [TestCase(TestName = "Logo is displayed", Author = "Ayrat Khaliullin")]
        public void LogoExistance()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(mainpage.FindLogo(), "Search form wasn't find at the page");
            });
        }

        [TestCase("What is Linkedin",TestName = "Search a phrase", Author = "Ayrat Khaliullin")]
        public void Search(string phrase)
        {
            mainpage.Search(phrase);
            GoogleSearchResultPage googleSearchResultPage = new GoogleSearchResultPage(driverInstance);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(googleSearchResultPage.FindSearchPhrase(phrase), "There's no phrase in search input");
            });
        }

        [TestCase("What is Linkedin", TestName = "Find search results", Author = "Ayrat Khaliullin")] 
        public void FindResults(string phrase)
        {
            mainpage.Search(phrase);
            GoogleSearchResultPage googleSearchResultPage = new GoogleSearchResultPage(driverInstance);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(googleSearchResultPage.FindResults(), "There's no results after search the phrase: {0}",phrase);
            });
        }
    }
}