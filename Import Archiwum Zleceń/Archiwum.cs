using iTextSharp.text;
using iTextSharp.text.pdf;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class Archiwum
    {
        private string Zespol, OsobaKontakt, NazwaProjektu, NazwaFirmy, EmailKontaktowy, DataRozpoczecia;
        private string DataZakonczenia, DataPlanowanegoZakonczenia, Departamentm, OpisPracy, DataUtworzenia;
        private string IdProjektu, CzasPracySzef, CzasPracyZespol, TelefonKontakt;
        IList<IWebElement> PytaniaBHP;

        public void SciagnijRaportArchiwum(IWebDriver Driver, int nrZlecenia)
        {
            XPathKlik(Driver, "//*[@id=\"left-panel\"]/nav/ul/li[1]/ul/li[2]/a");

            IWebElement WyborIlosc = Driver.FindElement(By.XPath("//*[@id=\"orderArchiveGrid_length\"]/label/select"));
            SelectElement WyborIloscDropLista = new SelectElement(WyborIlosc);
            WyborIloscDropLista.SelectByText("100");

            IWebElement temp = Driver.FindElement(By.XPath("//*[@id=\"orderArchiveGrid\"]/tbody/tr[" + nrZlecenia + "]/td[8]/a"));
            IJavaScriptExecutor egzekutor = (IJavaScriptExecutor)Driver;
            egzekutor.ExecuteScript("arguments[0].click();", temp);

            //Stringi
            DataUtworzenia = Driver.FindElement(By.XPath("//*[@id=\"widget-grid\"]/div/div/div/div[1]/div/div[2]/span")).Text;
            Zespol = PobierzString(Driver, "Team");
            OsobaKontakt = PobierzString(Driver, "ContactPersonName");
            NazwaProjektu = PobierzString(Driver, "ProjectName");
            NazwaFirmy = PobierzString(Driver, "ClientCompanyName");
            EmailKontaktowy = PobierzString(Driver, "ContactPersonEmail");
            DataRozpoczecia = PobierzString(Driver, "StartDate");
            DataZakonczenia = PobierzString(Driver, "DueDate");
            DataPlanowanegoZakonczenia = PobierzString(Driver, "ScheduledDueDate");
            Departamentm = PobierzString(Driver, "Department");
            OpisPracy = PobierzString(Driver, "JobDescription");
            Departamentm = PobierzString(Driver, "Department");
            IdProjektu = PobierzString(Driver, "Project_Id");
            CzasPracySzef = PobierzString(Driver, "WorkTimeEstimationBoss");
            CzasPracyZespol = PobierzString(Driver, "WorkTimeEstimationTeam");
            TelefonKontakt = PobierzString(Driver, "ContactPersonPhone");

            Driver.FindElement(By.XPath("//*[@id=\"content\"]/div/div[2]/div/input")).Click();
        }

        public void WypiszPola()
        {
            Console.WriteLine(DataUtworzenia);
            Console.WriteLine(Zespol);
            Console.WriteLine(OsobaKontakt);
            Console.WriteLine(NazwaProjektu);
            Console.WriteLine(NazwaFirmy);
            Console.WriteLine(EmailKontaktowy);
            Console.WriteLine(DataRozpoczecia);
            Console.WriteLine(DataZakonczenia);
            Console.WriteLine(DataPlanowanegoZakonczenia);
            Console.WriteLine(Departamentm);
            Console.WriteLine(OpisPracy);
            Console.WriteLine(Departamentm);
            Console.WriteLine(IdProjektu);
            Console.WriteLine(CzasPracySzef);
            Console.WriteLine(CzasPracyZespol);
            Console.WriteLine(TelefonKontakt);
        }

        public void XPathKlik(IWebDriver Driver, string XPath)
        {
            Driver.FindElement(By.XPath(XPath)).Click();
        }

        public string PobierzString(IWebDriver Driver, string Id)
        {
            return Driver.FindElement(By.Id(Id)).GetAttribute("value");
        }

        public void WypiszDoPliku(IWebDriver Driver, string NazwaPliku)
        {
            FileStream PdfStream = new FileStream(NazwaPliku + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Rectangle A4 = new Rectangle(PageSize.A4);
            A4.BackgroundColor = new BaseColor(System.Drawing.Color.FromArgb(255, 0, 190, 240));
            Document Dokument = new Document(A4, 72, 72, 72, 72);
            PdfWriter Writer = PdfWriter.GetInstance(Dokument, PdfStream);

            Font arialDuzy = FontFactory.GetFont("Arial", 24);
            Font arialMedium = FontFactory.GetFont("Arial", 14);

            Dokument.Open();
            Dokument.Add(new Paragraph("RAPORT ARCHIWALNY", arialDuzy));
            Dokument.Add(new Paragraph("ID - " + IdProjektu, arialMedium));
            Dokument.Add(new Paragraph("Data utworzenia - " + DataUtworzenia, arialMedium));

            PdfPTable tabela = new PdfPTable(2);
            PdfPCell cell = new PdfPCell(new Phrase("Szczegóły zlecenia"));



            cell.Colspan = 2;
            cell.HorizontalAlignment = 0;

            tabela.SpacingBefore = 10f;
            tabela.WidthPercentage = 100;
            tabela.AddCell("Zespol"); tabela.AddCell(Zespol);
            tabela.AddCell("Osoba kontaktowa"); tabela.AddCell(nullString(OsobaKontakt));
            tabela.AddCell("Czas pracy (szef)"); tabela.AddCell(nullString(CzasPracySzef));
            tabela.AddCell("Czas pracy (zespol)"); tabela.AddCell(nullString(CzasPracyZespol));
            tabela.AddCell("Nazwa projektu"); tabela.AddCell(nullString(NazwaProjektu));
            tabela.AddCell("Nazwa firmy"); tabela.AddCell(nullString(NazwaFirmy));
            tabela.AddCell("Osoba kontaktowa"); tabela.AddCell(nullString(OsobaKontakt));
            tabela.AddCell("Telefon kontaktowy"); tabela.AddCell(nullString(TelefonKontakt));
            tabela.AddCell("Email kontaktowy"); tabela.AddCell(nullString(EmailKontaktowy));
            tabela.AddCell("Data rozpoczęcia"); tabela.AddCell(nullString(DataRozpoczecia));
            tabela.AddCell("Data zakończenia"); tabela.AddCell(nullString(DataZakonczenia));
            tabela.AddCell("Departament"); tabela.AddCell(nullString(Departamentm));
            tabela.AddCell("Opis pracy"); tabela.AddCell(nullString(OpisPracy));

            tabela.AddCell("Pytania BHP");
            string StringBHP = null;

            Dokument.Add(tabela);

            Dokument.Close();
            PdfStream.Close();
            Writer.Close();
        }

        private string nullString(string tekst)
        {
            if(string.IsNullOrWhiteSpace(tekst))
            {
                tekst = "Brak";
            }
            return tekst;
        }
    }
}
