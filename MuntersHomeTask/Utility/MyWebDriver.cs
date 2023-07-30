using log4net;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MuntersHomeTask.Utility
{
    public class MyWebDriver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MyWebDriver));

        private static readonly IWebDriver _driver = new ChromeDriver();
        private static readonly int _implicitWait_Seconds = 45;
        private static TimeSpan _implicitWait = TimeSpan.Zero;
        private static By _frameDevice = By.XPath("//iframe[@class='trio-device-iframe']");

        protected MyWebDriver() { }

        public static IWebDriver GetDriver()
        {
            return _driver;
        }
        public static TimeSpan GetImplicitWait()
        {
            return _implicitWait;
        }

        public static void InitDriver(string? url = null)
        {
            if (string.IsNullOrEmpty(url))
                url = JsonReader.ExtractData<string>("appsettings", "MainUrl");

            SetImplicitWait();
            _driver.Navigate().GoToUrl(url);
            log.Info($"Open ChromeDriver and goto \"{url}\"...");
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                log.Info("Close driver");
            }
        }

        public static void SetImplicitWait(int time = -1)
        {

            int tempTime = (time > -1) ? time : _implicitWait_Seconds;
            SetImplicitWait(TimeSpan.FromSeconds(tempTime));
        }
        public static void SetImplicitWait(TimeSpan timeSpan)
        {
            if (_driver != null)
            {
                _driver.Manage().Timeouts().ImplicitWait = timeSpan;
                _implicitWait = timeSpan;
                log.Info($"Set implicit wait: {timeSpan}");
            }
        }

        public static void SwitchToFrameDevice()
        {
            IWebElement iframeElement = ElementAction.FindElement(_frameDevice);

            _driver.SwitchTo().Frame(iframeElement);
            log.Info("Driver switch window to iframe");
        }
        public static void ReturnToDefualt()
        {
            _driver.SwitchTo().DefaultContent();
            log.Info("Driver return default content");
        }

        //take a screen shot
        public static void Screenshot(string testName)
        {
            string folderName = JsonReader.ExtractData<string>("appsettings", "ScreenshotPath");
            string fileName = $"{testName}.png";
            string path = $"{folderName}/{fileName}";
            try
            {
                ((ITakesScreenshot)GetDriver())
                    .GetScreenshot()
                    .SaveAsFile(path, ScreenshotImageFormat.Png);
            }
            catch (DirectoryNotFoundException)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(folderName);
                    log.Info($"creating {folderName} directory");
                }
                else
                {
                    log.Info($"the directory {folderName} already exist");
                }

                ((ITakesScreenshot)GetDriver())
                    .GetScreenshot()
                    .SaveAsFile(path, ScreenshotImageFormat.Png);
            }
            log.Info($"Take a screenshot: {testName}");
        }
    }
}
