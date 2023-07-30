using log4net;
using OpenQA.Selenium;
using MuntersHomeTask.Utility;
using System.Text.RegularExpressions;

namespace MuntersHomeTask.PageObject.Components
{
    public class KeypadBodyComp
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KeypadBodyComp));

        private By _rangeNumbers = By.XPath("//span[@class='range-info']");

        public T GetMinRange<T>()
        {
            Type tType = typeof(T);
            (string min, _) = GetRangeNumbersAsString();

            if (tType.Equals(typeof(int)) && !min.Contains('.'))
            {
                log.Info($"Minimum int value: {(T)(object)int.Parse(min)}");
                return (T)(object)int.Parse(min);
            }
            else if (tType.Equals(typeof(double)))
            {
                log.Info($"Minimum double value: {(T)(object)double.Parse(min)}");
                return (T)(object)double.Parse(min);
            }
            throw new ArgumentException("Invalid range format.");
        }
        public T? GetMaxRange<T>()
        {
            Type tType = typeof(T);
            (_, string max) = GetRangeNumbersAsString();
            
            if (tType.Equals(typeof(int)) && !max.Contains('.'))
            {
                log.Info($"Maximum int value: {(T)(object)int.Parse(max)}");
                return (T)(object)int.Parse(max);
            }
            else if (tType.Equals(typeof(double)))
            {
                log.Info($"Maximum double value: {(T)(object)double.Parse(max)}");
                return (T)(object)double.Parse(max);
            }

            throw new ArgumentException("Invalid range format.");
        }

        public (string, string) GetRangeNumbersAsString()
        {
            string text = ElementAction.FindElement(_rangeNumbers).Text;
            string pattern = @"([-+]?\d*\.?\d+)\s*–\s*([-+]?\d*\.?\d+)";

            Match match = new Regex(pattern).Match(text);
            if (match.Success)
            {
                string min = match.Groups[1].Value;
                string max = match.Groups[2].Value;

                return (min, max);
            }
            else
            {
                throw new ArgumentException("Invalid range format.");
            }
        }
    }
}
