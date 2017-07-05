using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace TestJeden
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IWebDriver driver = new ChromeDriver(@"C:\Users\Michau\Desktop\Selenium\TestJeden\packages\Selenium.Chrome.WebDriver.2.30\driver");
            driver.Navigate().GoToUrl("http://ibh-jobmanager-testui.azurewebsites.net");
            driver.Manage().Window.Maximize();
            Logowanie("MichalP", "generycznehaslo", driver);
            ZgrywanieBazy(driver, "kanapka");
            Wylogowywanie(driver);
            driver.Quit();
        }
        void ZgrywanieBazy(IWebDriver driver, string nazwaPlikuZapisu)
        {
            try
            {
                StreamWriter Stream = new StreamWriter(nazwaPlikuZapisu + ".csv");
                IWebElement WyborIlosc = driver.FindElement(By.XPath("//*[@id=\"grid_length\"]/label/select"));
                SelectElement WyborIloscDropLista = new SelectElement(WyborIlosc);
                WyborIloscDropLista.SelectByText("100");

                IWebElement Temp, Grid;
                Grid = driver.FindElement(By.Id("grid_next"));

                IJavaScriptExecutor egzekutor = (IJavaScriptExecutor)driver;
      
                do
                {
                    int j = 1;
                    do
                    {
                        for (int i = 3; i < 8; i++)
                        {
                            Temp = driver.FindElement(By.XPath("//*[@id=\"grid\"]/tbody/tr[" + j + "]/td[" + i + "]"));
                            Stream.Write(Temp.Text + ";");
                        }
                        Stream.WriteLine("");
                        j++;

                        try
                        {
                            Temp = driver.FindElement(By.XPath("//*[@id=\"grid\"]/tbody/tr[" + j + "]/td[1]"));
                        }
                        catch (NoSuchElementException) { break; }
                    } while (j <= 100);

                    Grid = driver.FindElement(By.Id("grid_next"));
                    if (Grid.GetAttribute("class") == "paginate_enabled_next") egzekutor.ExecuteScript("arguments[0].click();", Grid);
                    else break;
                } while (true);

                Stream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(System.DateTime.Now + " - Zgranie bazy do pliku nieudane. -> " + e.Message);
            }

            Console.WriteLine(System.DateTime.Now + " - Zgranie bazy do pliku udane.");
        }

        void Wylogowywanie(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath("//*[@id=\"header\"]/div[2]/span[2]")).Click();
                driver.FindElement(By.XPath("//*[@id=\"header\"]/div[2]/ul/li[3]/a")).Click();
                Console.WriteLine(System.DateTime.Now + " - Wylogowanie udane.");
            }
            catch(NoSuchElementException e)
            {
                Console.WriteLine(System.DateTime.Now + " - Wylogowanie nieudane -> " + e.Message);
                driver.Quit();
            }
        }

        void Logowanie(string login, string haslo, IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.Id("UserName")).SendKeys(login);
                driver.FindElement(By.Id("Password")).SendKeys(haslo);
                driver.FindElement(By.CssSelector("Button")).Click();
                if (driver.FindElement(By.ClassName("validation-summary-errors")).FindElement(By.CssSelector("li")).Displayed)
                {
                    throw new Exception("Błędne dane logowania!");
                }
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(System.DateTime.Now + " - Logowanie udane.");
            }
            catch (Exception e)
            {
                Console.WriteLine(System.DateTime.Now + " - Logowanie się wysypało -> " + e.Message);
                driver.Quit();
            }

        }
    }

}
