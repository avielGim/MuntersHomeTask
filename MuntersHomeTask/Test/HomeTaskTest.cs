using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using MuntersHomeTask.Enum;
using MuntersHomeTask.Utility;
using MuntersHomeTask.PageObject;
using MuntersHomeTask.JsonObject;

namespace MuntersHomeTask.Test
{
    [TestFixture]
    public class HomeTaskTest : BaseTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeTaskTest));
        readonly LoginPage loginPage = new();
        readonly MainPage mainPage = new();

        readonly int INDEX_ZERO = 0;

        private string _successMessage = "Changes saved";


        [Test]
        public void Test1()
        {
            try
            {
                // open chrome drive and nevigate to "https://qa.www.trioair.net/"
                MyWebDriver.InitDriver();

                // login successfully
                User user = JsonReader.ExtractData<User>(User.JsonFile, "Success");
                loginPage.Login(user);
                loginPage.SignInButton.Click();
                log.Info("Login");

                // for the first farm in the menu tree
                ElementAction.Click(mainPage.FarmListComp.GetFarmListElements()[INDEX_ZERO]);
                log.Info("Click on the first farm");

                // under the sub-menu there are the actual controllers, "1" appears as disconnected, and the other is active.
                // M: click only on the connected one
                bool isConnected = false;
                Thread.Sleep(3000);
                int size = mainPage.ControllerListComp.GetControllerElements().Count;
                IList<IWebElement> controllerList = mainPage.ControllerListComp.GetControllerElements();
                for (int i = INDEX_ZERO; i < controllerList.Count && !isConnected; i++)
                {
                    if (mainPage.ControllerListComp.IsControllerConnected_ByIndex(i))
                    {
                        isConnected = true;
                        ElementAction.Click(mainPage.ControllerListComp.GetControllerElements()[i]);
                        log.Info($"Click on the connected controller at index={i}");
                    }
                }
                Assert.IsTrue(isConnected, "There is no controller that connected");

                // M: switch to the device screen
                MyWebDriver.SwitchToFrameDevice();

                // open controller Menu -> "Climate" -> "Temperature Curve".
                mainPage.FarmDetailsComp.FarmDetailsBarComp.MenuButton.Click();
                mainPage.FarmDetailsComp.FarmDetailsBarComp.ClickOnMenu(FarmDetailsLeftMenuType.Climate);
                mainPage.FarmDetailsComp.FarmDetailsBarComp.ClickOnMenu(FarmDetailsRightMenuType.TemperatureCurve);
                log.Info("open controller Menu -> \"Climate\" -> \"Temperature Curve\" successfully");

                // when you are in edit mode and at a certain Field(focus) you can see in the keypad the range that you can enter to the field 
                // M: click on edit button
                mainPage.FarmDetailsComp.FarmDetailsBlueBarComp.EditButton.Click();
                log.Info("Click edit button");

                // M: edit each field by random number between the givven range
                IList<TemperatureCurveTableType> list = new List<TemperatureCurveTableType>()
                {
                    TemperatureCurveTableType.Day, TemperatureCurveTableType.Target,
                    TemperatureCurveTableType.LowTAlarm, TemperatureCurveTableType.HighTAlarm,
                };
                foreach (TemperatureCurveTableType type in list)
                {
                    SetField(type);
                }

                // save changes
                mainPage.FarmDetailsComp.FarmDetailsBlueBarComp.SaveButton.Click();
                log.Info("Click save button");
                // look for “Changes Saved Successfully” / “Failed To Save Changes” toaster as an assert condition.
                Assert.AreEqual(_successMessage, mainPage.FarmDetailsComp.GetValidationMessage(), "The message is not as expected");

                MyWebDriver.Screenshot("Success!");
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
               CatchAndFail(ex);
            }
        }


        private void SetField(TemperatureCurveTableType type)
        {
            IWebElement typeField = mainPage.FarmDetailsComp.TemperatureCurveDialog.GetFieldElements(type)[INDEX_ZERO];
            ElementAction.Click(typeField);
            Thread.Sleep(3000); // wait for the Range to be modified
            log.Info($"Changing field: {type}..." );
            
            string value = GetRandomValue(type);

            log.Info($"Insert random value: {value}");
            ElementAction.SetText(typeField, value);
            Thread.Sleep(2000);
            string currValue = mainPage.FarmDetailsComp.TemperatureCurveDialog.GetFieldText(type)[INDEX_ZERO];

            // TOCHECK the try and catch logic
            try
            {
                if (currValue.Contains(value))
                    Assert.Fail($"The value at {type} should be contains ={value}, but actual={currValue}");
            }
            catch (Exception ex)
            {
                log.Error($"The insert value is not as expected={value} actual={currValue}");
                CatchAndFail(ex);
                value = GetRandomValue(type);
                ElementAction.SetText(typeField, value);            
            }
            log.Info($"Finish change field: {type}\n");
            Thread.Sleep(8000); // wait for the Range to be modified
        }
        public string GetRandomValue(TemperatureCurveTableType type)
        {            
            switch (type)
            {
                case TemperatureCurveTableType.Day:
                    int dayMin = mainPage.FarmDetailsComp.KeypadBodyComp.GetMinRange<int>();
                    int dayMax = mainPage.FarmDetailsComp.KeypadBodyComp.GetMaxRange<int>();
                    int rand = new Random().Next(dayMin, dayMax);
                    return rand.ToString();
                default:
                    double minimum = mainPage.FarmDetailsComp.KeypadBodyComp.GetMinRange<double>();
                    double maximum = mainPage.FarmDetailsComp.KeypadBodyComp.GetMaxRange<double>();
                    double randDouble = new Random().NextDouble() * (maximum - minimum) + minimum;
                    return randDouble.ToString();
            }
        }
    }
}
