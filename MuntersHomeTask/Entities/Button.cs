using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using MuntersHomeTask.Utility;

namespace MuntersHomeTask.Entities
{
    public class Button
    {
        private By _by;

        public Button(By by)
        {
            _by = by;
        }

        public bool IsClickable()
        {
            WebDriverWait wait = new(MyWebDriver.GetDriver(), MyWebDriver.GetImplicitWait());
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(_by));

            bool isDisplay = element.Enabled;
            // log
            return isDisplay;
        }
        public void Click()
        {
            ElementAction.Click(_by);
        }
    }
}
