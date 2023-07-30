using log4net;
using OpenQA.Selenium;
using MuntersHomeTask.Utility;
using MuntersHomeTask.PageObject.Dialog;

namespace MuntersHomeTask.PageObject.Components
{
    public class FarmDetailsComp
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FarmDetailsComp));

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
            string validationTxt = ElementAction.FindElement(_validationMessage).Text;
            log.Info($"Validation message: \"{validationTxt}\"");
            return validationTxt;
        }
        #endregion
    }
}
