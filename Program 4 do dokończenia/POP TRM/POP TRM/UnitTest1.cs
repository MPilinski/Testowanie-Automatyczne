using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace POP_TRM
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            TMLogInPage.TMLogInPage StronaLogowania = new TMLogInPage.TMLogInPage(driver);
            TMMainPage.TMMainPage StronaGlowna = new TMMainPage.TMMainPage(driver);
            TMNewClient.TMNewClient StronaTworzeniaKlienta = new TMNewClient.TMNewClient(driver);

            StronaLogowania.Navigate();
            StronaLogowania.LogIn("mpilinski@it-project.net.pl", "5U3qauq6");

            StronaGlowna.Navigate("Klienci");
            StronaGlowna.Map.AddClient.Click();

            StronaTworzeniaKlienta.Map.Id.Click();
        }
    }
}
