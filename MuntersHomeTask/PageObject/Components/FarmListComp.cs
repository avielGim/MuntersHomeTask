using MuntersHomeTask.Utility;
using OpenQA.Selenium;

namespace MuntersHomeTask.PageObject.Components
{
    public class FarmListComp
    {
        private readonly By _farm = By.XPath("//div[contains(@id,'site')]");

        public IList<IWebElement> GetFarmListElements()
        {
            return ElementAction.FindElements(_farm);
        }
    }
}
