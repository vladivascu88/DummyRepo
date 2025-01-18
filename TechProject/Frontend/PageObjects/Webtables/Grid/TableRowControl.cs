using OpenQA.Selenium;


namespace Frontend.PageObjects.Webtables.Grid
{
    public class TableRowControl
    {
        private IWebDriver driver;
        private IWebElement row;

        public TableRowControl(IWebDriver driver, IWebElement row)
        {
            this.driver = driver;
            this.row = row;
        }

        public IWebElement editBtn => row.FindElement(By.CssSelector(".btn.btn-link"));
        public IWebElement firstName => row.FindElement(By.CssSelector(".smart-table-data-row td"));
        public IWebElement customerName => row.FindElement(By.CssSelector(".smart-table-data-row td:nth-child(5)"));
        public IWebElement roleName => row.FindElement(By.CssSelector(".smart-table-data-row td:nth-child(6)"));





    }
}
