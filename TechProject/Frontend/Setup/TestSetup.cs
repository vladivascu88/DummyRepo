using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using WebDriverManager.Helpers;

namespace Frontend.Setup
{
    public class TestSetup
    {
        public IWebDriver driver;

        [TestInitialize]
        public void TestInit()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
