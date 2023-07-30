using OpenQA.Selenium;
using MuntersHomeTask.Utility;

namespace MuntersHomeTask.PageObject.Components
{
    public class ControllerListComp
    {
        // TODO: log
        private readonly By _comparison = By.XPath("//div[@class='icon-comparison']");
        private readonly By _controller = By.XPath("//div[contains(@id,'titleMenuItem')]/..");

        public IList<IWebElement> GetControllerElements()
        {
            return ElementAction.FindElements(_controller);
        }

        public bool IsControllerConnected_ByIndex(int index)
        {
            IWebElement element = GetControllerElements()[index];
            bool isDisconnected = !element.GetAttribute("class").Contains("disconnected");
            // log
            return isDisconnected;
        }
    }
}
