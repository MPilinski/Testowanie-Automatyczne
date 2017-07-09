using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SprawdzenieFormatowania_Kwoty
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://ibh-jobmanager-testui.azurewebsites.net");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            Logowanie("MichalP", "generycznehaslo", driver);

            //Sprawdzanie okna "Zlecenia"
            #region oknoZlecenia
            Console.WriteLine(System.DateTime.Now + "Sprawdzanie kwot w oknie \"Zlecenia\"...");

            CheckRegex(driver, "//*[@id=\"grid\"]//td[8]");
            #endregion

            //Przejscie do niezaakceptowanego raportu
            #region przejscieRaport
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[8]/a/span");
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[8]/ul/li[1]/a");

            IWebElement temp;
            bool isFound = false;
            for (int i = 1; i <= 10; i++)
            {
                temp = driver.FindElement(By.XPath("//*[@id=\"grid\"]//tr[" + i + "]/td[2]/span[2]"));
                if (temp.Text.Contains("Niezaak"))
                {
                    ClickXPath(driver, "//*[@id=\"grid\"]//tr[" + i + "]/td[9]/div/a");
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
            {
                Console.WriteLine("Nie znaleziono niezaakceptowanego raportu!");
                Assert.Fail();
            }
            #endregion

            //Sprawdzenie kwot w niezaakceptowanym raporcie
            #region raportNiezaakceptowany
            Console.WriteLine(System.DateTime.Now + " - Sprawdzanie kwot w niezaakceptowanym raporcie...");

            CheckCheckbox(driver, "showPricesCheckbox");

            CheckRegex(driver, "//*[@id=\"productGrid\"]//td[position()=4 or position()=5]");

            #endregion

            //Przejscie do niezaakceptowanego raportu
            #region przejscieRaport
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[8]/a/span");
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[8]/ul/li[1]/a");

            temp = null;
            isFound = false;
            for (int i = 1; i <= 10; i++)
            {
                temp = driver.FindElement(By.XPath("//*[@id=\"grid\"]//tr[" + i + "]/td[2]/span[2]"));
                if (temp.Text.Contains("Zaakcept"))
                {
                    ClickXPath(driver, "//*[@id=\"grid\"]//tr[" + i + "]/td[9]/div/a");
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
            {
                Console.WriteLine("Nie znaleziono niezaakceptowanego raportu!");
                Assert.Fail();
            }
            #endregion

            //Sprawdzenie kwot w zaakceptowanym raporcie
            #region raportZaakceptowany
            Console.WriteLine(System.DateTime.Now + " - Sprawdzanie kwot w zaakceptowanym raporcie...");

            CheckCheckboxJE(driver, "showPricesCheckbox");
            CheckCheckbox(driver, "showWorkerPricesCheckbox");
            CheckCheckbox(driver, "showAllCostSumaryCheckbox");

            //Problem ze stopką
            CheckRegex(driver, "//*[@id=\"productGrid\"]//td[position()=4 or position()=5]");
            CheckRegex(driver, "//*[@id=\"allCostSumary\"]//td[3]");
            CheckRegex(driver, "//*[@id=\"workerGrid\"]//td[8]");
            #endregion

            //Przejscie do archiwalnego raportu
            #region przejscieArchiwumRaportow
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[8]/ul/li[2]/a");
            ClickXPath(driver, "//*[@id=\"grid\"]/tbody/tr[1]/td[8]/a");
            #endregion

            //Sprawdzenie kwot w zaarchiwizowanym raporcie
            #region raportZarchiwizowany
            Console.WriteLine(System.DateTime.Now + " - Sprawdzanie kwot w zarchiwizowanym raporcie...");
        
            CheckCheckboxJE(driver, "showPricesCheckbox");
            CheckCheckbox(driver, "showWorkerPricesCheckbox");
            CheckCheckbox(driver, "showAllCostSumaryCheckbox");

            //Problem ze stopką
            CheckRegex(driver, "//*[@id=\"productGrid\"]//td[position()=4 or position()=5]");
            CheckRegex(driver, "//*[@id=\"allCostSumary\"]//td[3]");
            CheckRegex(driver, "//*[@id=\"workerGrid\"]//td[8]");
            #endregion

            //Przejscie do faktury
            #region przejscieFaktura
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[9]/a/span");
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[9]/ul/li[1]/a");

            isFound = false;
            temp = null;
            for (int i = 1; i <= 10; i++)
            {
                temp = driver.FindElement(By.XPath("//*[@id=\"DataTables_Table_0\"]//tr[" + i +"]/td[2]/span[2]"));
                if (temp.Text.Contains("Opłacona"))
                {
                    ClickXPath(driver, "//*[@id=\"DataTables_Table_0\"]//tr[" + i + "]/td[6]/div/a[2]");
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
            {
                Console.WriteLine("Nie znaleziono opłaconej faktury!");
                Assert.Fail();
            }
            #endregion

            //Sprawdzenie kwot w opłaconej fakturze
            #region faktura
            Console.WriteLine(System.DateTime.Now + " - Sprawdzanie kwot w opłaconej fakturze...");

            CheckRegex(driver, "//*[@id=\"workerGrid\"]//td[6]");
            CheckRegex(driver, "//*[@id=\"productGrid\"]//td[4]");
            #endregion

            //Przejscie do archiwum faktur
            #region przejscieArchiwumFaktur
            ClickXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[9]/ul/li[2]/a");
            ClickXPath(driver, "//*[@id=\"DataTables_Table_0\"]/tbody/tr[1]/td[6]/div/a");
            #endregion

            //Archiwum faktur
            #region archiwumFaktura
            Console.WriteLine(System.DateTime.Now + " - Sprawdzanie kwot w zarchiwizowanej fakturze...");

            CheckRegex(driver, "//*[@id=\"workerGrid\"]//td[6]");
            CheckRegex(driver, "//*[@id=\"productGrid\"]//td[4]");
            #endregion
            
            Wylogowywanie(driver);
            
            //driver.Quit();
        }
       
        void CheckCheckbox(IWebDriver driver, string Id)
        {
            IWebElement PrizeCheckBox = driver.FindElement(By.Id(Id));
            if (!PrizeCheckBox.Selected)
            {
                PrizeCheckBox.Click();
            }
        }

        void CheckCheckboxJE(IWebDriver driver, string Id)
        {
            IWebElement PrizeCheckBox = driver.FindElement(By.Id(Id));
            IJavaScriptExecutor egzekutor = (IJavaScriptExecutor)driver;

            if (!PrizeCheckBox.Selected)
            {
                egzekutor.ExecuteScript("arguments[0].click();", PrizeCheckBox);
            }
            
        }

        void CheckRegex(IWebDriver driver, string XPath)
        {
            string RegexPattern = "(\\d){1,3}( \\d\\d\\d)*[.](\\d\\d) NOK";
            IList<IWebElement> ReportPrizeList = driver.FindElements(By.XPath(XPath));
            foreach (IWebElement Element in ReportPrizeList)
            {
                Assert.IsTrue(Regex.Match(Element.Text, RegexPattern).Success);
            }
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

        void ClickXPath(IWebDriver driver, string XPath)
        {
            driver.FindElement(By.XPath(XPath)).Click();
        }
    }
}
