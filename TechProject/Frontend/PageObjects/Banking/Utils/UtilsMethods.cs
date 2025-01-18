using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Frontend.PageObjects.Banking.Utils
{
    public static class UtilsMethods
    {
        public static void WaitForElementToBeDisplayed(IWebDriver driver, IWebElement element, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
    }
}
