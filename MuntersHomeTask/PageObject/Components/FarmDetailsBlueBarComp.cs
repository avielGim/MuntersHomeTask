using MuntersHomeTask.Entities;
using OpenQA.Selenium;

namespace MuntersHomeTask.PageObject.Components
{
    public class FarmDetailsBlueBarComp
    {
        #region Property
        public Button EditButton = new(By.XPath("//div[@id='button-edit']")); // need to change the div to button
        public Button SaveButton = new(By.XPath("//div[contains(text(),'Save') and @class='toolbar-button label-button ng-star-inserted']"));
        public Button CancelButton = new(By.XPath("//div[contains(text(),'Cancel') and @class='toolbar-button label-button ng-star-inserted']"));
        #endregion
    }
}
