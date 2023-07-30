using log4net;
using MuntersHomeTask.Entities;
using MuntersHomeTask.Enum;
using MuntersHomeTask.Utility;
using OpenQA.Selenium;

namespace MuntersHomeTask.PageObject.Components
{
    public class FarmDetailsUpperBarComp
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FarmDetailsUpperBarComp));


        public Button HomeButton = new(By.XPath("//button[@class='header-item header-button k-button']"));

        public Button MenuButton = new(By.XPath("//button[@class='header-item header-button']"));

        public void ClickOnMenu(FarmDetailsLeftMenuType type)
        {
            By by = By.XPath($"//div[@class='menu-left']//div[@id='{type}']");
            ElementAction.Click(by);
            log.Info($"Click on left-menu option: {type}");
        }
        public void ClickOnMenu(FarmDetailsRightMenuType type)
        {
            string typeStr = EnumManager.GetDescriptionFromEnum(type);
            By by = By.XPath($"//div[@class='menu-table-name' and text()='{typeStr}']");
            ElementAction.Click(by);
            log.Info($"Click on right-menu option: {typeStr}");
        }
    }
}
