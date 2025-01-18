using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers; 

namespace Frontend.PageObjects.Banking
{
    public class BankHomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public BankHomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement homeBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Home']")));

        public IWebElement bankManagerLoginBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Bank Manager Login']")));


    }
}
