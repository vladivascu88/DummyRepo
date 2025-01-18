using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using OpenQA.Selenium.Support.UI;
using Frontend.Setup;
using Frontend.PageObjects.Webtables;

namespace Frontend.Tests
{
    [TestClass]
    public class WebTablesTest:TestSetup
    {      

        [TestMethod]
        public void CheckUpateOfUserDetails()
        {
            //Navigate to the webpage
            string url = "https://www.way2automation.com/angularjs-protractor/webtables/";
            driver.Navigate().GoToUrl(url);

            //Maximize the window
            driver.Manage().Window.Maximize();

            //Create an instance of the WebTablesPage
            WebTablesPage webTablesPage = new WebTablesPage(driver);          

            //Click the Edit button for the row with First Name "Mark"
            webTablesPage.ClickEditButtonForFirstName("Mark");

            //Modify the Role to Sales Team
            EditUserPopUp popUpPage = new EditUserPopUp(driver);
            popUpPage.SelectRole(1);
            popUpPage.ClickSave();     

            //Check if the Role is updated to Sales Team
            Assert.IsTrue(webTablesPage.GetRoleCellValueForFirstName("Mark").Contains("Sales Team"), "Role is updated to Sales Team");

            //Click the Edit button for the row with First Name "test"
            webTablesPage.ClickEditButtonForFirstName("test");

            //Modify the Customer to Company AAA
            popUpPage.radioBtnCustomerAAA.Click(); 
            popUpPage.ClickSave();

            //Check if the Customer is updated to Company AAA
            Assert.IsTrue(webTablesPage.GetCustomerCellValueForFirstName("test").Contains("Company AAA"), "No update was done for Customer column, current value is: "+ (webTablesPage.GetCustomerCellValueForFirstName("test").ToString()));

        }
      
    }
}
