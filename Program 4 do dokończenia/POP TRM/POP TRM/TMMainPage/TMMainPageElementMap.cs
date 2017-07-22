using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_TRM.TMMainPage
{
    public class TMMainPageElementMap
    {
        //Constructor
        public TMMainPageElementMap(IWebDriver InDriver)
        {
            driver = InDriver;
        }


        //Fields
        public IWebDriver driver;

        public IWebElement[] MainMenu
        {
            get
            {
                IWebElement[] Temp = new IWebElement[5];
                for (int i = 2; i <= 6; i++)
                {
                    Temp[i-2] = AssertElementFind("//*[@class=\"nav\"]//li[" + i + "]");
                }
                return Temp;
            }
        }

        public IWebElement Search
        {
            get
            {
                return AssertElementFind("//*[@id=\"datatable2_filter\"]/label/input");
            }
        }

        public IWebElement DropDownProfil
        {
            get
            {
                return AssertElementFind("//*[@class=\"media-box\"]//p[@class=\"p\"]");
            }
        }

        public IWebElement AddClient
        {
            get
            {
                return AssertElementFind("//*[@id=\"clients\"]//button");
            }
        }

        public IWebElement ClientsTable
        {
            get
            {
                return AssertElementFind("//*[@id=\"datatable2\"]/tbody");
            }
        }

        public IWebElement PageElementCount
        {
            get
            {
                return AssertElementFind("//*[@id=\"datatable2_length\"]//select");
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

        public IWebElement ClientsTableField(int Column, int Row)
        {
            if (1 < Column || Column > 17 || Row < 1)
            {
                return null;
            }
            return AssertElementFind("//*[@id=\"datatable2\"]//tr[" + Column + "]/td[" + Row + "]");
        }
    }
}
