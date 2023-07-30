using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools;

namespace MuntersHomeTask.Utility
{
    public class MyWebDriver
    {
        private static readonly IWebDriver _driver = new ChromeDriver();
        private static readonly int _implicitWait_Seconds = 45;
        private static TimeSpan _implicitWait = TimeSpan.Zero;
        private static By _frameDevice = By.XPath("//iframe[@class='trio-device-iframe']");



        // TODO: log

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
                url = "https://qa.www.trioair.net/";

            SetImplicitWait();
            _driver.Navigate().GoToUrl(url);
            //_driver.Context = "NATIVE_APP";
            // log.Info("Open ChromeDriver and goto \"{url}\"...");
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                // log.Info("close driver");
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
                // log.Info($"Set implicit wait: {time}");
            }
        }

        public static void SwitchToFrameDevice()
        {
            IWebElement iframeElement = ElementAction.FindElement(_frameDevice);

            _driver.SwitchTo().Frame(iframeElement);
        }
        public static void ReturnToDefualt()
        {
            _driver.SwitchTo().DefaultContent();
        }
    }
}
