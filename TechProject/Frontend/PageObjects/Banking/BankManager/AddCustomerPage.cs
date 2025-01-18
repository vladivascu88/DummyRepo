using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.PageObjects.Banking.BankManager
{
    public class AddCustomerPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public AddCustomerPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }       

        public IWebElement firstNameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[placeholder='First Name']")));
        public IWebElement lastNameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[placeholder='Last Name']")));
        public IWebElement postCodeInput => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[placeholder='Post Code']")));
        public IWebElement addCustomerBtn => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='submit']")));


    }
}
