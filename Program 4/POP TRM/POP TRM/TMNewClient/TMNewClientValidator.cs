using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.IO;

namespace POP_TRM.TMNewClient
{
    class TMNewClientValidator
    {
        //Constructor
        public TMNewClientValidator(IWebDriver TempDriver)
        {
            driver = TempDriver;
        }

        //Fields
        IWebDriver driver;
        TMNewClientElementMap Map
        {
            get
            {
                return new TMNewClientElementMap(driver);
            }
        }

        //Methods
        public void VerifyForm()
        {
            IWebElement Temp=null;
            try
            {
                Temp = Map.FullForm.FindElement(By.XPath("//*[@class=\"validationMessage\"]"));
            }
            catch { }
            Console.WriteLine(Temp.Text);
            Assert.Fail(); 
        }

        private bool FindClient(string FileName, int Row)
        {
            string Line = null;
            string[] Fields = new string[12];
            TMMainPage.TMMainPage MainPage = new TMMainPage.TMMainPage(driver);

            StreamReader Reader = new StreamReader(FileName);
            for (int i = Row; i >= 0; i--)
            {
                Line = Reader.ReadLine();
            }
            Fields = Line.Split(';');

            MainPage.Search(Fields[2]);

            IWebElement Temp = null;

            for(int i = 1; i<10; i++)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                try
                {
                    Temp = driver.FindElement(By.XPath("//*[@id=\"datatable2\"]/tbody/tr[" + i + "]/td[4]"));
                    if(Temp.Text.Contains(Fields[1]))
                    {
                        return true;
                    }
                }
                catch{}
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            return false;
        }

        public void AssertClientExist(string FileName, int Row)
        {
            if(!FindClient(FileName,Row))
            {
                Assert.Fail();
            }
        }
    }
}
