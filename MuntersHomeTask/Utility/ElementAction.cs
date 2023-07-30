using log4net;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace MuntersHomeTask.Utility
{
    public class ElementAction
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ElementAction));

        static WebDriverWait _wait { get => new(_driver, MyWebDriver.GetImplicitWait()); }
        static IWebDriver _driver => MyWebDriver.GetDriver();

        // TODO: log

        public static IWebElement FindElement(By by)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        public static IList<IWebElement> FindElements(By by)
        {
            return _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        public static void SetText(By by, string value)
        {
            SetText(FindElement(by), value);
        }

        public static void SetText(IWebElement element, string value)
        {
            PaintElement(element, "blue", "red");
            Thread.Sleep(1500);
            element.SendKeys(value);
            log.Info($"Send text to an element: {value}");
            ReturnElementColorToDefault(element);
        }

        // click
        public static void Click(By by)
        {
            IWebElement element = _wait.Until(ExpectedConditions.ElementToBeClickable(by));
            Click(element);
        }
        public static void Click(IWebElement element)
        {
            try
            {
                element.Click();
                log.Info($"Element clicked by selenium");
            }
            catch (ElementClickInterceptedException)
            {
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)_driver;
                jsExecutor.ExecuteScript("arguments[0].click();", element);
                log.Info("Element clicked by java-script");
            }
        }

        // JSAction
        public static void ReturnElementColorToDefault(IWebElement element)
        {
            PaintElement(element, "null", "null");
        }
        public static void PaintElement(IWebElement element, string background, string border)
        {
            string command = $"arguments[0].setAttribute('style', 'background: {background}; border: 2px solid {border};');";
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(command, element);
        }
    }
}
