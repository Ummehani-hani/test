using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;

namespace FinalProject
{
    [TestClass]
    public class UnitTest1
    {
        public static IWebDriver driver;
        public TestContext instance;
        public TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }
        }
        public static ExtentReports extentReports;
        public static ExtentTest Test;
        public static ExtentTest Step;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            string ResultFilePath = @"C:\ExtentReports\TestExecLog_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            var sparkReporter = new ExtentSparkReporter(ResultFilePath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(sparkReporter);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            extentReports.Flush();
        }

        [TestInitialize]
        public void TestInit()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            BasePage.driver = driver;
            Test = extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TestClean()
        {
            try
            {
                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    string path = TakeScreenshot("Failure Screenshot");
                    if (!string.IsNullOrEmpty(path))
                        Test.Fail("Test Failed ").AddScreenCaptureFromPath(path);
                    else
                        Test.Fail("Test Failed  (No screenshot captured)");
                }
                else
                {
                    Test.Pass("Test Passed ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in cleanup: " + ex.Message);
            }
            finally
            {
                driver?.Quit();
            }
        }

        private string TakeScreenshot(string stepDetail, By locatorToWait = null)
        {
            try
            {
                if (locatorToWait != null)
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.ElementIsVisible(locatorToWait));
                }

                string folderPath = @"C:\ExtentReports\images\";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string path = Path.Combine(folderPath, fileName);

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                File.WriteAllBytes(path, screenshot.AsByteArray);

                return Path.GetFullPath(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Screenshot error: " + ex.Message);
                return string.Empty;
            }
        }

        public void AddStepScreenshot(Status status, string stepDetail, By locator)
        {
            string path = TakeScreenshot(stepDetail, locator);

            if (!string.IsNullOrEmpty(path))
            {
                Step.Log(status, stepDetail,
                    MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build());
            }
            else
            {
                Step.Log(status, stepDetail + " (No screenshot captured)");
            }
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                "|DataDirectory|\\TestData.xml",
                "Test",
                DataAccessMethod.Sequential)]
        [TestMethod]
        public void TestMethod1()
        {
            string url = "https://adactinhotelapp.com/";
            string user = TestContext.DataRow["Username"].ToString();
            string pass = TestContext.DataRow["Password"].ToString();

            
            Step = Test.CreateNode("Login Page");
            driver.Url = url;
            AddStepScreenshot(Status.Info, "Login Page Opened", By.Id("username"));

            LoginPage loginPage = new LoginPage();
            loginPage.Login(url, user, pass);
            AddStepScreenshot(Status.Pass, "Login Done", By.Id("location"));

            
            Step = Test.CreateNode("Search Page");
            SearchPage searchPage = new SearchPage();
            searchPage.SearchHotel();
            AddStepScreenshot(Status.Pass, "Search Completed", By.Id("radiobutton_0"));

            
            Step = Test.CreateNode("Select Hotel Page");
            SelectPage selectPage = new SelectPage();
            selectPage.SelectHotel();
            AddStepScreenshot(Status.Pass, "Hotel Selected", By.Id("continue"));

            
            Step = Test.CreateNode("Booking Confirmation");
            ConfirmationPage confirmationPage = new ConfirmationPage();
            confirmationPage.ConfirmBooking();
            AddStepScreenshot(Status.Pass, "Booking Confirmed", By.Id("logout"));
        }
    }
}
