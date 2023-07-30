using MuntersHomeTask.PageObject.Dialog;
using MuntersHomeTask.Utility;
using OpenQA.Selenium;

namespace MuntersHomeTask.PageObject.Components
{
    public class FarmDetailsComp
    {
        #region Property
        private By _validationMessage = By.XPath("//div[@role='alertdialog' and @aria-live='polite']");

        public FarmDetailsUpperBarComp FarmDetailsBarComp = new();
        public FarmDetailsBlueBarComp FarmDetailsBlueBarComp = new();
        public TemperatureCurveDialog TemperatureCurveDialog = new();
        public KeypadBodyComp KeypadBodyComp = new();
        #endregion


        #region Method
        public string GetValidationMessage()
        {
            return ElementAction.FindElement(_validationMessage).Text;
        }
        #endregion
    }
}
