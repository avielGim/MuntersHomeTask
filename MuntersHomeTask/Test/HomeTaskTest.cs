using NUnit.Framework;
using MuntersHomeTask.Utility;
using MuntersHomeTask.PageObject;
using OpenQA.Selenium;
using MuntersHomeTask.Enum;
using log4net;
using log4net.Config;

namespace MuntersHomeTask.Test
{
    [TestFixture]
    public class HomeTaskTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeTaskTest));
        readonly LoginPage loginPage = new();
        readonly MainPage mainPage = new();

        readonly int INDEX_ZERO = 0;

        public HomeTaskTest()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }


        [Test]
        public void Test1()
        {
            try
            {
                // open chrome drive and nevigate to "https://qa.www.trioair.net/"
                MyWebDriver.InitDriver();

                // login successfully
                string email = "Munterstal@gmail.com";
                string password = "123456Munters";
                loginPage.Login(email, password);
                loginPage.SignInButton.Click();

                // for the first farm in the menu tree
                // M: click on the first farm
                ElementAction.Click(mainPage.FarmListComp.GetFarmListElements()[INDEX_ZERO]);

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
                        // log ?
                    }
                }
                Assert.IsTrue(isConnected, "There is no controller that connected");

                // M: switch to the device screen
                MyWebDriver.SwitchToFrameDevice();

                // open controller Menu -> "Climate" -> "Temperature Curve".
                mainPage.FarmDetailsComp.FarmDetailsBarComp.MenuButton.Click();
                mainPage.FarmDetailsComp.FarmDetailsBarComp.ClickOnMenu(FarmDetailsLeftMenuType.Climate);
                mainPage.FarmDetailsComp.FarmDetailsBarComp.ClickOnMenu(FarmDetailsRightMenuType.TemperatureCurve);

                // when you are in edit mode and at a certain Field(focus) you can see in the keypad the range that you can enter to the field 
                // M: click on edit button
                mainPage.FarmDetailsComp.FarmDetailsBlueBarComp.EditButton.Click();
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
                // look for “Changes Saved Successfully” / “Failed To Save Changes” toaster as an assert condition.
                Assert.AreEqual("Changes saved", mainPage.FarmDetailsComp.GetValidationMessage());
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Type: {ex.GetType()}\n" +
                    $"Strace: {ex.StackTrace}\n" +
                    $"message: {ex.Message}");
                // TODO catch and fail logic
            }
            finally
            {
                MyWebDriver.QuitDriver();
            }
        }

        [Test]
        public void X()
        {
            log.Info("Hello");
        }
        private void SetField(TemperatureCurveTableType type)
        {
            IWebElement typeField = mainPage.FarmDetailsComp.TemperatureCurveDialog.GetFieldElements(type)[INDEX_ZERO];
            ElementAction.Click(typeField);
            Thread.Sleep(3000);
            TestContext.WriteLine($"Changing field: {type}..." );
            string value;
            switch (type)
            {
                case TemperatureCurveTableType.Day:
                    int dayMin = mainPage.FarmDetailsComp.KeypadBodyComp.GetMinRange<int>();
                    int dayMax = mainPage.FarmDetailsComp.KeypadBodyComp.GetMaxRange<int>();
                    int rand = new Random().Next(dayMin, dayMax);
                    value = rand.ToString();
                    TestContext.WriteLine($"min value: {dayMin}" );
                    TestContext.WriteLine($"max value: {dayMax}" );
                    break;
                default:
                    double minimum = mainPage.FarmDetailsComp.KeypadBodyComp.GetMinRange<double>();
                    double maximum = mainPage.FarmDetailsComp.KeypadBodyComp.GetMaxRange<double>();
                    double randDouble = new Random().NextDouble() * (maximum - minimum) + minimum;
                    value = randDouble.ToString();
                    TestContext.WriteLine($"minValue: {minimum}");
                    TestContext.WriteLine($"maxValue: {maximum}");
                    break;
            }

            TestContext.WriteLine($"Insert random value: {value}");
            ElementAction.SetText(typeField, value);
            TestContext.WriteLine($"Finish change field: {type}\n");
            Thread.Sleep(2000);
        }
    }
}
