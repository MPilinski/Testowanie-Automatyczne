﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMNewClient
{
    class TMNewClientElementMap
    {
        private IWebDriver driver;
        
        //Constructor
        public TMNewClientElementMap(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        //Fields
        public IWebElement FullForm
        {
            get
            {
                return driver.FindElement(By.ClassName("form-horizontal"));
            }
        }

            //Text fields
        public IWebElement Id
        {
            get
            {
                return FindElementClientField(1);
            }
        }

        public IWebElement IdSk
        {
            get
            {
                return FindElementClientField(2);
            }
        }

        public IWebElement Name
        {
            get
            {
                return FindElementClientField(3);
            }
        }

        public IWebElement NIP
        {
            get
            {
                return FindElementClientField(5);
            }
        }

        public IWebElement WWW
        {
            get
            {
                return FindElementClientField(8);
            }
        }

        public IWebElement AcquisitionDate
        {
            get
            {
                return FindElementClientField(9);
            }
        }

            //Lists
        public IWebElement Character
        {
            get
            {
                return FindElementClientField(4);
            }
        }

        public IWebElement Mentor
        {
            get
            {
                return FindElementClientField(6);
            }
        }

        public IWebElement Status
        {
            get
            {
                return FindElementClientField(7);
            }
        }

        public IWebElement ClientAcquisitionDate
        {
            get
            {
                return FindElementClientField(10);
            }
        }

            //Buttons
        public IWebElement SaveButton
        {
            get
            {
                return AssertElementFind("/html/body/div[2]/div/div/div[3]/button[1]");
            }
        }

        public IWebElement CancelButton
        {
            get
            {
                return AssertElementFind("//*[@class=\"modal-footer\"]//button[2]");
            }
        }


        //Methods
        private IWebElement AssertElementFind(string XPath)
        {
            try
            {
                return driver.FindElement(By.XPath(XPath));
            }
            catch
            {
                Console.WriteLine(System.DateTime.Now + " - Nie udało się znaleść elementu \" " + XPath + " \".");
                return null;
            }
        }

        private IWebElement FindElementClientField(int Order)
        {
            return AssertElementFind("/html/body/div[2]/div/div/div[2]/div/div[1]/div");
        }
    }
}
