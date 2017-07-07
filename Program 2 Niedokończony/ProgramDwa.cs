using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using System.Collections;
using OpenQA.Selenium.Interactions;

namespace JmTestyDropbox
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://ibh-jobmanager-testui.azurewebsites.net");
            Logowanie(driver, "MichalP", "generycznehaslo");

            KlikXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[12]/a");

            //Dodanie nieistniejącej nazwy
            UsunIstniejacaRole(driver, "DowolnaNazwa");
            DodajRole(driver, "DowolnaNazwa");
            SprawdzCzyIstnieje(driver, "DowolnaNazwa");
            EdytujIstniejacaRole(driver, "DowolnaNazwa");
            SprawdzCzyIstnieje(driver, "DowolnaNazwa");
            

            ////Dodanie istniejącej nazwy
            //DodajRole(driver, "DowolnaNazwa");
            //SprawdzCzyIstnieje(driver, "DowolnaNazwa");

            ////Dodanie roli o pustej nazwie
            //DodajRole(driver, "");
            //SprawdzCzyIstnieje(driver, "");



            //Wylogowywanie(Driver);
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

        void Logowanie(IWebDriver driver, string login, string haslo)
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

        void KlikXPath(IWebDriver driver, string XPath)
        {
            driver.FindElement(By.XPath(XPath)).Click();
        }

        void DodajRole(IWebDriver driver, string nazwaRoli)
        {
            Thread.Sleep(1000);
            KlikXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[12]/ul/li[2]/a");

            KlikXPath(driver, "//*[@id=\"content\"]/div/div[2]/div/a");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            driver.FindElement(By.Id("Name")).SendKeys(nazwaRoli);
            KlikXPath(driver, "//*[@id=\"form0\"]/div[2]/button[1]");

            if (CzyIstnieje(driver, "field-validation-error"))
            {
                Console.WriteLine(System.DateTime.Now + " - " + nazwaRoli + " - nie udało się utworzyć roli!");
                Console.WriteLine("         Błąd: " + driver.FindElement(By.ClassName("field-validation-error")).Text);
                KlikXPath(driver, "//*[@id=\"form0\"]/div[2]/button[2]");
            }
            else
            {
                KlikXPath(driver, "/html/body/div[9]/div/div/div[3]/button[2]");
                Console.WriteLine(System.DateTime.Now + " - " + nazwaRoli + " - rola utworzona.");
            }
        }

        bool SprawdzCzyIstnieje(IWebDriver driver, string nazwaRoli)
        {
            Thread.Sleep(1000);
            KlikXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[12]/ul/li[2]/a");
            try
            {
                driver.FindElement(By.XPath("//*[contains(.,'" + nazwaRoli + "')]"));
                Console.WriteLine("         Sprawdzenie: Rola " + nazwaRoli + " istnieje.");
                return true;
            }
            catch
            {
                Console.WriteLine("         Sprawdzenie: Rola " + nazwaRoli + " nie istnieje.");
                return false;
            }
        }

        bool CzyIstnieje(IWebDriver driver, string Klasa)
        {
            try
            {
                driver.FindElement(By.ClassName(Klasa));
                return true;
            }
            catch
            {
                return false;
            }
        }

        void UsunIstniejacaRole(IWebDriver driver, string nazwaRoli)
        {
            Thread.Sleep(1000);
            KlikXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[12]/ul/li[2]/a");
            bool znaleziono = false;
            for (int i=1;i<6;i++)
            {
                if (driver.FindElement(By.XPath("//*[@id=\"DataTables_Table_0\"]/tbody/tr[" + i + "]/td[1]")).Text.Contains(nazwaRoli))
                {
                    driver.FindElement(By.XPath("//*[@id=\"DataTables_Table_0\"]/tbody/tr[" + i + "]/td[2]/div/a[2]/span")).Click();
                    Thread.Sleep(1000);
                    driver.FindElement(By.XPath("/html/body/div[9]/div/div/div[3]/button[1]")).Click();

                    Console.WriteLine(System.DateTime.Now + " - " + nazwaRoli + " - element usuniety.");
                    znaleziono = true;
                    break;
                }
            }
            if(!znaleziono)Console.WriteLine(System.DateTime.Now + " Nie udało się usunąć elementu! - " + nazwaRoli);
        }

        void EdytujIstniejacaRole(IWebDriver driver, string nazwaRoli)
        {
            int[] IloscOpcji = new int[12] { 7, 6, 3, 0, 5, 6, 5, 2, 3, 7, 0, 0};
            bool[,] ZaznaczaneOpcje = new bool[12,7]{{true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {false, true, true, true, true, true, true},
                                                     {false, true, true, true, true, true, true},
                                                     {false, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true},
                                                     {true, true, true, true, true, true, true}};
            Thread.Sleep(1000);
            IJavaScriptExecutor egzekutor = (IJavaScriptExecutor)driver;

            KlikXPath(driver, "//*[@id=\"left-panel\"]/nav/ul/li[12]/ul/li[2]/a");
            bool znaleziono = false;
            for (int i = 1; i < 6; i++)
            {
                if (driver.FindElement(By.XPath("//*[@id=\"DataTables_Table_0\"]/tbody/tr[" + i + "]/td[1]")).Text.Contains(nazwaRoli))
                {
                    driver.FindElement(By.XPath("//*[@id=\"DataTables_Table_0\"]/tbody/tr[" + i + "]/td[2]/div/a[1]")).Click();
                    Console.WriteLine(nazwaRoli + " - edycja - znaleziono element");
                    znaleziono = true;
                    break;
                }
            }
            if (!znaleziono) Console.WriteLine(nazwaRoli + "- nie udało się znaleść elementu!");

            Actions akcja = new Actions(driver);
            
            IWebElement lista = driver.FindElement(By.Id("tree"));
            IWebElement temp = null;


            Thread.Sleep(1000);
            for(int i=0; i<12; i++)
            {
                if (IloscOpcji[i] == 0 && ZaznaczaneOpcje[i,0])
                {
                    temp = driver.FindElement(By.XPath("//*[@id=\"tree\"]/li[" + (i+1) + "]/ol/li/label"));
                    egzekutor.ExecuteScript("arguments[0].click();", temp);
                    Console.WriteLine("Stało się");
                }

                for (int j=0;j<IloscOpcji[i];j++)
                {
                    if (ZaznaczaneOpcje[i, j])
                    {
                        temp = driver.FindElement(By.Id("Permissions[" + i + "].Children[" + j + "].Allow"));
                        egzekutor.ExecuteScript("arguments[0].click();", temp);
                    }
                }
            }

            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/input[1]")).Click();
        }
    }
}
