using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using HomePage.POM;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace HomePage.TestCases
{
    class HomePage
    {
        #region Varibales
        public static ExtentReports _extent = new ExtentReports();
        protected ExtentTest _test;
        protected ExtentHtmlReporter htmlReporter;
        IWebDriver driver;
        int x;
        int y;
        int z;

        #endregion
        [OneTimeSetUp]
        protected void Setup()
        {
            var testname = GetType().Namespace;
            var fileName = GetType().Namespace + DateTime.Now.ToString("yyMMdd") + ".html";
            String extentReportDir1 = @"C:\\ExtentReports\\";
            htmlReporter = new ExtentHtmlReporter(extentReportDir1 + fileName);
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.AppendExisting = true;
            _extent.AttachReporter(htmlReporter);
        }
        [OneTimeTearDown]
        protected void TearDown()
        {
            _extent.Flush();
            driver.Dispose();
        }
        [SetUp]
        public void Initilaize()
        {

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            _test.AssignCategory(this.GetType().ToString());
            _extent.AddSystemInfo("HostName", "AgileTeam");
            _extent.AddSystemInfo("Environment", "QA");
            _test.AssignAuthor("VenkataRamana Gangasani");
            driver = new ChromeDriver();
        }
        // [Test, Order(1)]
        [Test]
        public void TestHomepage()
        {
            var testStepName = _test.CreateNode(TestContext.CurrentContext.Test.Name);
            driver.Navigate().GoToUrl("http://paint-calc.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            PaintObjects login = new PaintObjects(driver);
            // No. of rooms hard coded now, we can reterive test data from excel sheet using ACE driver

            login.LoginRooms("1");
            
            var alert = driver.FindElement(By.XPath("/html/body/div/h1"));
            var expectedText = "Calculating Paint Required";
            Assert.AreEqual(expectedText, alert.Text);
            testStepName.Pass("Calculating Paint Required window Opened");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            // 2nd window Enter Length, width and height values

            IList<IWebElement> txtValues = driver.FindElements(By.XPath(".//table/tbody/tr/td/input"));
            int x = 10; // 3 paramaters are hard code, we can reterive test data from excel sheet using ACE driver.    
            String length = Convert.ToString(x);
            int y = 10;
            String width = Convert.ToString(x);
            int z = 10;
            String height = Convert.ToString(x);

            foreach (IWebElement txtValue in txtValues)
            {
                txtValue.SendKeys(length);
                // txtValue.SendKeys(width);
                // txtValue.SendKeys(height);
            }
            driver.FindElement(By.XPath(".//div/form/input")).Submit();

            //3rd window View Resuslts Button
            driver.FindElement(By.XPath("/html/body/div/button")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //4th Window Paint Calculation Results Window
            //Reteriving amount of feet to paint
            String amtOfPaint = driver.FindElement(By.XPath(".//table/tbody/tr/td[2]")).Text; // On GUI                  
            Double calAmtofpaint = (((x * 2) + (y * 2)) * z); // PBI Userstories     
            Assert.AreEqual(amtOfPaint, calAmtofpaint);
            testStepName.Pass("Calculating Amount of Paint: " + calAmtofpaint);

            // Calculating No. of Gallons of paint
            var calNoOfGallons = Math.Round(calAmtofpaint / 400);
            String noOfGallons = driver.FindElement(By.XPath("//*[@id='sumGallons']")).Text;
            String[] arrSplit = noOfGallons.Split(':');
            for (int i = 0; i < arrSplit.Length; i++)
            {
                noOfGallons = arrSplit[i + 1]; // on GUI
                break;
            }

            Assert.AreEqual(calNoOfGallons, noOfGallons);
            testStepName.Pass("Calculating No of Gallaons : " + calNoOfGallons);

            // Close the Results Window
            driver.FindElement(By.XPath("//*[@id='resultsModal']/div/div/div[3]/button")).Click();

        }
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            _extent.Flush();

        }
    }
}
