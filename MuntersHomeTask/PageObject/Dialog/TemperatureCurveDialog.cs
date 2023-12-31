﻿using MuntersHomeTask.Enum;
using MuntersHomeTask.Utility;
using OpenQA.Selenium;

namespace MuntersHomeTask.PageObject.Dialog
{
    public class TemperatureCurveDialog
    {
        private readonly By _dayField = By.XPath("//input[contains(@id,'c1_')]");
        private readonly By _targetField = By.XPath("//input[contains(@id,'c2_')]");
        private readonly By _heatField = By.XPath("//input[contains(@id,'c3_')]");
        private readonly By _coolField = By.XPath("//input[contains(@id,'c4_')]");
        private readonly By _lowTAlarmField = By.XPath("//input[contains(@id,'c6_')]");
        private readonly By _highTAlarmField = By.XPath("//input[contains(@id,'c7_')]");


        public IList<IWebElement> GetFieldElements(TemperatureCurveTableType type)
        {
            By fieldsBy = type switch
            {
                TemperatureCurveTableType.Day => _dayField,
                TemperatureCurveTableType.Target => _targetField,
                TemperatureCurveTableType.Heat => _heatField,
                TemperatureCurveTableType.Cool => _coolField,
                TemperatureCurveTableType.LowTAlarm => _lowTAlarmField,
                TemperatureCurveTableType.HighTAlarm => _highTAlarmField,
                _ => throw new NotImplementedException("The field is unrecognized"),
            };
            return ElementAction.FindElements(fieldsBy);
        }
        // TOCHEKC
        public IList<string> GetFieldText(TemperatureCurveTableType type)
        {
            IList<string> result = new List<string>();
            IList<IWebElement> elementList = GetFieldElements(type);
            foreach (IWebElement element in elementList)
            {
                result.Add(element.Text);
            }
            return result;
        }
    }
}
