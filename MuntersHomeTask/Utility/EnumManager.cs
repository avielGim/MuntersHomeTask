using System;
using System.Linq;
using OpenQA.Selenium;
using System.ComponentModel;

namespace MuntersHomeTask.Utility
{
    public class EnumManager
    {
        public static string GetDescriptionFromEnum<T>(T value)
        {
            DescriptionAttribute attribute = value.GetType()
                                                  .GetField(value.ToString())
                                                  .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                  .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static T GetEnumFromDescription<T>(string value)
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field
                                                , typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == value)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(value));
        }
    }
}
