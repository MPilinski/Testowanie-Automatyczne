//#undef DEBUG
#define DEBUG

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
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
            
            SciagnijArchiwum(driver);

            #if(!DEBUG)
                Wylogowywanie(driver);
                driver.Quit();
            #endif
        }

        void Wylogowywanie(IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath("//*[@id=\"header\"]/div[2]/span[2]")).Click();
                driver.FindElement(By.XPath("//*[@id=\"header\"]/div[2]/ul/li[3]/a")).Click();
                Console.WriteLine(System.DateTime.Now + " - Wylogowanie udane.");
            }
            catch (NoSuchElementException e)
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
            catch (NoSuchElementException)
            {
                Console.WriteLine(System.DateTime.Now + " - Logowanie udane.");
            }
            catch (Exception e)
            {
                Console.WriteLine(System.DateTime.Now + " - Logowanie się wysypało -> " + e.Message);
                driver.Quit();
            }

        }

        void SciagnijArchiwum(IWebDriver driver)
        {
            for (int i = 1; i <= 100; i++)
            {
                Archiwum test = new Archiwum();
                try
                {
                    test.SciagnijRaportArchiwum(driver, i);
                    test.WypiszDoPliku(driver, "Raport" + i.ToString());
                    Console.WriteLine(i);
                }
                catch
                {
                    break;
                }
            }
        }
    }
}

