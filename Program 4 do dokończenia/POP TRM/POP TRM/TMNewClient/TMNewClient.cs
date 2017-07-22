using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void Add()
        {
            Map.SaveButton.Click();
        }

        public void LoadFieldsFile(string FileName, int Row)
        {
            string Line = null;
            string[] Fields = new string[12];
            int[] TextFields = new int[]{ 0, 1, 2, 4, 5, 8, 9};
            int[] ListFields = new int[] { 3, 6, 7, 10, 11 };

            StreamReader Reader = new StreamReader(FileName);
            for (int i = Row; i >= 0; i--)
            {
                Line = Reader.ReadLine();
            }
            Fields = Line.Split(';');

            for (int i = 0; i < TextFields.Length; i++)
            {
                if (Fields[TextFields[i]] != "null")
                {
                    Map.FindElementClientField(TextFields[i] + 1).SendKeys(Fields[TextFields[i]]);
                }
            }

            for(int i=0;i<ListFields.Length;i++)
            {
                if (Fields[TextFields[i]] != "null")
                {
                    new SelectElement(driver.FindElement(By.XPath("(//div[@class=\"form-group\"])[" + (ListFields[i] + 1) + "]/div/select"))).SelectByText(Fields[ListFields[i]]); 
                }
            }

            Reader.Close();
        }
    }
}
