using Frontend.PageObjects.Webtables.Grid;
using OpenQA.Selenium;

namespace Frontend.PageObjects.Webtables
{
    public class WebTablesPage
    {
        private IWebDriver driver;

        public WebTablesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement tableElement => driver.FindElement(By.CssSelector(".smart-table"));

        public By tableRows = By.CssSelector(".smart-table-data-row");

        public TResult ProcessRowByFirstName<TResult>(string firstName, Func<TableRowControl, TResult> processRow)
        {
            var rows = driver.FindElements(tableRows);
            foreach (var rowElement in rows)
            {
                var rowControl = new TableRowControl(driver, rowElement);
                if (rowControl.firstName.GetAttribute("innerText").Equals(firstName))
                {
                    return processRow(rowControl);
                }
            }
            return default;
        }
        
        public void ClickEditButtonForFirstName(string firstName)
        {
            ProcessRowByFirstName(firstName, rowControl =>
            {
                rowControl.editBtn.Click();
                return true;
            });
        }

        public string GetCustomerCellValueForFirstName(string firstName)
        {
            return ProcessRowByFirstName(firstName, rowControl =>
                rowControl.customerName.GetAttribute("innerText"));
        }

        public string GetRoleCellValueForFirstName(string firstName)
        {
            return ProcessRowByFirstName(firstName, rowControl =>
                rowControl.roleName.GetAttribute("innerText"));
        }

    }
}