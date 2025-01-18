using Frontend.PageObjects.Webtables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Setup;
using Frontend.PageObjects.Banking;
using Frontend.PageObjects.Banking.BankManager;
using OpenQA.Selenium.Support.UI;
using Frontend.PageObjects.Banking.Utils;
using OpenQA.Selenium;
using System.Reflection.Metadata;


namespace Frontend.Tests
{
    [TestClass]
    public class BankingTests:TestSetup
    {

        [TestMethod]
        public void CheckCustomerInBankManager()
        {
            //Navigate to the webpage
            string url = "https://www.way2automation.com/angularjs-protractor/banking/#/login";
            driver.Navigate().GoToUrl(url);

            //Maximize the window
            driver.Manage().Window.Maximize();
      
            //Create an instance of the BankHomePage
            BankHomePage bankHomePage = new BankHomePage(driver);

            //Click on the bank manager login button
            bankHomePage.bankManagerLoginBtn.Click();

            //Create an instance of the BankManagerPage
            BankManagerPage bankManagerPage = new BankManagerPage(driver);

            //Click on the add customer button
            bankManagerPage.addCustomerBtn.Click();

            //Create an instance of the AddCustomerPage
            AddCustomerPage addCustomerPage = new AddCustomerPage(driver);

            //Enter the details for the person
            addCustomerPage.firstNameInput.SendKeys("John");
            addCustomerPage.lastNameInput.SendKeys("Wick");
            addCustomerPage.postCodeInput.SendKeys("888300");

            //Add customer
            addCustomerPage.addCustomerBtn.Click();

            //Accept the browser alert
            driver.SwitchTo().Alert().Accept(); 

            //Click on the customers button
            bankManagerPage.customersBtn.Click();

            //Create an instance of the CustomersPage
            CustomersPage customersPage = new CustomersPage(driver);

            //Search the name "John" 
            Assert.IsTrue(customersPage.SearchUser("John"),"user is not found");

            //Delete the record
            customersPage.DeleteUser();
            Assert.IsFalse(customersPage.SearchUser("John"), "user is not deleted");

        

        }
    }
}
