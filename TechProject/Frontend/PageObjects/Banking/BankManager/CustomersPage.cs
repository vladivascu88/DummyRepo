using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Frontend.PageObjects.Banking.BankManager
{
    public class CustomersPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        public CustomersPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement searchInputField => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[placeholder='Search Customer']")));
        public IWebElement deleteBtn => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("tbody > tr > td:nth-child(5) > button")));
        
        public IWebElement firstName => driver.FindElement(By.CssSelector("tbody > tr > td:nth-child(1)"));

        //method that searches for the user if is present in the table
        public bool SearchUser(string user)
        {
            try
            {
                searchInputField.Clear();
                searchInputField.SendKeys(user);            
                if (firstName.GetAttribute("innerText") == user)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public void DeleteUser()
        {
            deleteBtn.Click();
        }
    }
}
