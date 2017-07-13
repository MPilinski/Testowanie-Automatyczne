using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMLogInPage
{
    class TMLogInPageValidator
    {
        IWebDriver driver;

        public TMLogInPageValidator(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        public bool AsserLogIn()
        {
            try
            {
                driver.FindElement(By.ClassName("validation-summary-errors"));
                Console.WriteLine(System.DateTime.Now + " - Nie udało się zalogować");
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
