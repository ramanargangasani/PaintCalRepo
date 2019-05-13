using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace HomePage.POM
{
    class PaintObjects
    {
        IWebDriver driver;
        public IWebElement txtNoofRooms => driver.FindElement(By.Name("rooms"));
        public IWebElement btnSubmit => driver.FindElement(By.XPath("/html/body/div/form/input[2]"));

        public PaintObjects(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void LoginRooms(string txtNoofRooms)
        {

            driver.FindElement(By.Name("rooms")).SendKeys(txtNoofRooms);
            btnSubmit.Click();

        }
    }
}
