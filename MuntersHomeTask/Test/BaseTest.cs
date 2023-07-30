using log4net;
using System.Text;
using log4net.Config;
using NUnit.Framework;
using MuntersHomeTask.Utility;
using NUnit.Framework.Interfaces;

// [assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace MuntersHomeTask.Test
{
    public abstract class BaseTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeTaskTest));

        private int _screenshotIndex = 0;
        private string _currentTestName = "";


        public BaseTest()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }

        [SetUp]
        public void BeforeTest()
        {
            _currentTestName = GetNameOfCurrentTest();
            log.Info($"START TEST - {_currentTestName}");
        }

        [TearDown]
        public void AfterTest_GetStatusTest()
        {
            MyWebDriver.QuitDriver();
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            switch (status)
            {
                case TestStatus.Failed:
                    log.Error(_currentTestName);
                    break;
                case TestStatus.Inconclusive:
                    log.Error(_currentTestName);
                    break;
                case TestStatus.Skipped:
                    log.Info(_currentTestName);
                    break;
                case TestStatus.Passed:
                    log.Info(_currentTestName);
                    break;
                default:
                    log.Info(_currentTestName);
                    break;
            }
            log.Info($"FINISH TEST - {status}\n");
        }

        public void CatchAndFail(Exception ex)
        {
            MyWebDriver.Screenshot($"{_currentTestName}_({_screenshotIndex++})");

            Type t = ex.GetType();
            if (typeof(AssertionException).Equals(t))
            {
                log.Error("result is not as expected");
                log.Error(ex.Message);
            }
            else
            {
                log.Error("\nError");
                log.Error($"Message: {ex.Message}");
                log.Error($"Exeption type: {ex.GetType()}");
                log.Error(ex.StackTrace);
                Assert.Fail(ex.Message);
            }
        }
        private string GetNameOfCurrentTest()
        {
            string fullName = TestContext.CurrentContext.Test.FullName;
            string[] arrayFullName = fullName.Split('.');
            int count = arrayFullName.Length;
            string className = arrayFullName[^2];

            StringBuilder testName = new();
            testName.Append(className);
            testName.Append('_');
            testName.Append(arrayFullName[^1]);
            return testName.ToString();
        }
    }
}
