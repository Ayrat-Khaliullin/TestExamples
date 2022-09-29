using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace UITests.Utilities.Bases
{
    /// <summary>
    /// Parent class,inherited by all test classes
    /// Includes executed test counter
    /// </summary>
    public abstract class BaseTest
    {
        public static int loginRetries = 3;
        #region Public Methods
        [SetUp]
        public void SetUpBaseTest()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            TestContext.Progress.WriteLine($"Started test: {testName}");
        }

        [TearDown]
        public void TearDownBaseTest()
        {
            CountTest();
            var testName = TestContext.CurrentContext.Test.Name;
            TestContext.Progress.WriteLine($"TestFinished: {testName}");
            if (_executedTests % 10 == 0)
            {
                TestContext.Progress.WriteLine(
                    $"\r\n[{System.DateTime.Now.ToString("HH:mm:ss")}] Total test executed: {_executedTests}; Passed: {_passedTests}; Failed: {_failedTests}; Ignored: {_skippedTests}\r\n");
            }
        }
        #endregion

        #region Private Properties
        private static int _executedTests;
        private static int _passedTests;
        private static int _failedTests;
        private static int _skippedTests;
        #endregion

        #region Private Methods
        private static void IncrementTests()
        {
            _executedTests++;
        }
        private static void IncrementPassedTests()
        {
            _passedTests++;
        }
        private static void IncrementFailedTests()
        {
            _failedTests++;
        }
        private static void IncrementSkippedTests()
        {
            _skippedTests++;
        }
        private static void CountTest()
        {
            IncrementTests();
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                IncrementPassedTests();
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                IncrementFailedTests();
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped ||
                     TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Inconclusive)
            {
                IncrementSkippedTests();
            }
        }
        #endregion
    }
}
