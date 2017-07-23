using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace POP_TRM
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PustePola()
        {
        ExecuteTestClientCreating("PustePola.csv", 0, "login", "haslo");
        }

        [TestMethod]
        public void PrawidlowePola()
        {
            for(int i=0;i<4;i++)
            {
                ExecuteTestClientCreating("PrawidlowePola.csv", i, "login", "haslo");
            }
        }

        [TestMethod]
        public void NieprawidlowePola()
        {
            for (int i = 0; i < 4; i++)
            {
                ExecuteTestClientCreating("PrawidlowePola.csv", i, "login", "haslo");
            }
        }



        public bool ExecuteTestClientCreating(string DataSetPath, int Row, string Login, string Password)
        {
            Regex RegexPath = new Regex(@"^[\w]*[.]csv");
            Match Match = RegexPath.Match(DataSetPath);

            //Sprawdzenie danych wejściowych
            if (!Match.Success || Row<0)
            {
                Console.WriteLine("Błąd danych wejściowych funkcj ExecuteTestClientCreating");
                Assert.Fail();
                return false;
            }

            //Przekierowanie ścieżki na folder "DaneTestowe"
            String Temp = @"C:\Users\Michau\Documents\GitHub\Testowanie-Automatyczne\Program 4 do dokończenia\POP TRM\POP TRM\DaneTestowe\";
            Temp += DataSetPath;
            DataSetPath = Temp;

            //Stworzenie drivera, instancji obiektów stron
            IWebDriver Driver = new ChromeDriver();
            TMLogInPage.TMLogInPage LogInPage = new TMLogInPage.TMLogInPage(Driver);
            TMMainPage.TMMainPage MainPage = new TMMainPage.TMMainPage(Driver);
            TMNewClient.TMNewClient ClientCreationPage = new TMNewClient.TMNewClient(Driver);

            //Logowanie
            LogInPage.Navigate();
            LogInPage.LogIn(Login, Password);

            //Przejście do zakładki "Klienci"
            MainPage.Navigate("Klienci");

            //Przejście do dodawania nowego klienta
            MainPage.ClickNewClient();
            ClientCreationPage.WindowOpenWait();

            //Załadowanie danych z kliku "DataSetPath" (musi być csv) do pól tekstowych i list w oknie dodawaniea klienta.
            ClientCreationPage.LoadFieldsFile(DataSetPath, Row);
            //Werifikacja czy nie wyświetla się żadek komunikat przy jakimkolwiek z pól
            ClientCreationPage.Valide().VerifyForm();
            ClientCreationPage.Add();

            //Sprawdzenie czy klient o danym Id został dodany, TODO: sprawdzić czy dodał się z odpowiednimi polami.
            ClientCreationPage.Valide().AssertClientExist(DataSetPath, Row);

            //Zamknięcie Drivera
            Driver.Quit();

            return true;
        }
    }
}
