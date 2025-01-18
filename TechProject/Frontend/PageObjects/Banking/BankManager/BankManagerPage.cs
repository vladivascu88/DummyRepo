using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Frontend.PageObjects.Banking.BankManager
{
    public class BankManagerPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public BankManagerPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement addCustomerBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[ng-click=\"addCust()\"]")));

        public IWebElement customersBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[ng-click=\"showCust()\"]")));

    }
}
