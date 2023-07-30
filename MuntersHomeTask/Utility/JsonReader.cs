using log4net;
using Newtonsoft.Json;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuntersHomeTask.Utility
{
    public class JsonReader
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JsonReader));

        public static readonly string JsonFilePath = @".\..\..\..\JsonFile\";

        public static T ExtractData<T>(string fileName, string nameObject)
        {
            var jsonString = File.ReadAllText($"{JsonFilePath}{fileName}.json");

            var data = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonString);
            if (data != null)
            {
                if (data.ContainsKey(nameObject))
                {
                    log.Info($"Get \"{nameObject}\" object from \"{fileName}\" json file");
                    return data[nameObject];
                }
                throw new NullReferenceException($"The file does not contains {nameObject} is null");
            }
            throw new NullReferenceException("The data is null");
        }
    }
}
