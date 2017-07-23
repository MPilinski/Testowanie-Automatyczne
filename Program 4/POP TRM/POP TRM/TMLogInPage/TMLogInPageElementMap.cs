using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMLogInPage
{
    class TMLogInPageElementMap
    {
        IWebDriver driver;

        public TMLogInPageElementMap(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        public IWebElement Email
        {
            get
            {
                return AssertElementFind("Email");
            }
        }

        public IWebElement Password
        {
            get
            {
                return AssertElementFind("Password");
            }
        }

        public IWebElement RemeberMe
        {
            get
            {
                return AssertElementFind("RememberMe");
            }
        }

        public IWebElement Button
        {
            get
            {
                return driver.FindElement(By.XPath("//button"));
            }
        }
        
        private IWebElement AssertElementFind(string Id)
        {
            try
            {
                return driver.FindElement(By.Id(Id));
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine(System.DateTime.Now + " - Nie udało się znaleść elementu \" " + Id + " \".");
                return null;
            }

        }
    }
}
