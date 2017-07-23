using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMLogInPage
{
    class TMLogInPage
    {
        IWebDriver driver;
        string Url = "https://test.cigtransmanager.hostingasp.pl";

        public TMLogInPage(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        TMLogInPageElementMap Map
        {
            get
            {
                return new TMLogInPageElementMap(driver);
            }
        }

        TMLogInPageValidator Validate()
        {
            return new TMLogInPageValidator(driver);
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void LogIn(string Email, string Password)
        {
            Map.Email.SendKeys(Email);
            Map.Password.SendKeys(Password);
            Map.Button.Click();

            Validate().AsserLogIn();
        }
    }
}
