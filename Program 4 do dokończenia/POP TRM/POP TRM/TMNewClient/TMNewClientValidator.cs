using OpenQA.Selenium;
using System;

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
        public bool VerifieForm()
        {
            IWebElement Temp;
            try
            {
                Temp = Map.FullForm.FindElement(By.XPath("//input[class=\"form-control validation-error\""));
            }
            catch
            {
                return true;
            }
            Console.WriteLine(Temp.Text);
            return false;
        }
    }
}
