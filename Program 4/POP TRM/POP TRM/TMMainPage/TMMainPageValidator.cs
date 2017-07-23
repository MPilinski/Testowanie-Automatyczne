using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMMainPage
{
    class TMMainPageValidator
    {
        private IWebDriver driver;

        public TMMainPageValidator(IWebDriver InDriver)
        {
            driver = InDriver;
        }

        private TMMainPageElementMap Map
        {
            get
            {
                return new TMMainPageElementMap(driver);
            }
        }

        public bool ClientTableText(int Column, int Row, string Text)
        {
            if(Map.ClientsTableField(Column, Row).Text==Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
