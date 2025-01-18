using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Frontend.PageObjects.Webtables
{
    public class EditUserPopUp
    {
        private IWebDriver driver;
        public EditUserPopUp(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement roleDropDown => driver.FindElement(By.CssSelector("select[name=\"RoleId\"]"));
        public IWebElement btnSave => driver.FindElement(By.CssSelector(".btn-success"));
        public IWebElement tableElement => driver.FindElement(By.CssSelector(".smart-table"));
        public IWebElement radioBtnCustomerAAA => driver.FindElement(By.CssSelector("label:nth-child(1) > input"));


        public void SelectRole(int index)
        {
            var selectElement = new SelectElement(roleDropDown);
            selectElement.SelectByIndex(index);
        }
        public void ClickSave()
        {
            btnSave.Click();
        }      

    }
}
