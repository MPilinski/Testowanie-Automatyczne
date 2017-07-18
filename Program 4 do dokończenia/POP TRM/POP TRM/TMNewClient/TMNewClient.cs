using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMNewClient
{
    class TMNewClient
    {
        public TMNewClient(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        IWebDriver driver;

        public TMNewClientElementMap Map
        {
            get
            {
                return new TMNewClientElementMap(driver);
            }
        }

        public TMNewClientValidator Valide()
        {
            return new TMNewClientValidator(driver);
        }
    }
}
