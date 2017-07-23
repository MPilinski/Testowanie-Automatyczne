using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMMainPage
{
    class TMMainPage
    {
        private IWebDriver driver;

        public TMMainPage(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        public TMMainPageElementMap Map
        {
            get
            {
                return new TMMainPageElementMap(driver);
            }
        }

        public TMMainPageValidator Validate()
        {
            return new TMMainPageValidator(driver);
        }

        public void ClickNewClient()
        {
            Map.AddClient.Click();
        }

        public void Navigate(string Where)
        {
            switch (Where)
            {
                case "Klienci":
                    Map.MainMenu[0].Click();
                    break;
                case "Pojazdy":
                    Map.MainMenu[1].Click();
                    break;
                case "Gminy":
                    Map.MainMenu[2].Click();
                    break;
                case "Akcje":
                    Map.MainMenu[3].Click();
                    break;
                case "Admin":
                    Map.MainMenu[4].Click();
                    break;
                default:
                    Console.WriteLine(System.DateTime.Now + " - Nie znaleziono opcji " + Where + " w menu glownym");
                    break;
            }
        }

        public void Search(string WhatToSearch)
        {
            Map.Search.SendKeys(WhatToSearch);

        }
    }
}
