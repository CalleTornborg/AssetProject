using System;
using System.Collections.Generic;
using System.Text;

namespace EFAssets
{
    
    public class App
    {
        static AssetContext _context = new AssetContext();

        public void Run()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            Header("Main Menu");

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("a) Assets menu");
            Console.WriteLine("b) Category menu");
            Console.WriteLine("c) Offices");
            Console.WriteLine("d) Currencies");
            Console.WriteLine("e) Reports");
            Console.WriteLine("\nq) Exit program");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    AssetMenu();
                    break;
                case ConsoleKey.B:
                    CategoryMenu();
                    break;
                case ConsoleKey.C:
                    OfficeMenu();
                    break;
                case ConsoleKey.D:
                    CurrencyMenu();
                    break;
                case ConsoleKey.E:
                    ReportMenu();
                    break;
               
                case ConsoleKey.Q:
                    break;
               
                default:
                    MainMenu();
                    break;
            }

            //if (command == ConsoleKey.Enter)
            //    MainMenu();

            //if(command == ConsoleKey.A)
            //    AssetMenu();
            //if (command == ConsoleKey.B)
            //    CategoryMenu();
            //if (command == ConsoleKey.C)
            //    OfficeMenu();
            //if (command == ConsoleKey.D)
            //    CurrencyMenu();
            //if (command == ConsoleKey.E)
            //    ReportMenu();
            //if (command == ConsoleKey.Q)
            //    return;
        }

        private void AssetMenu()
        {
            Header("Asset Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new asset");
            Console.WriteLine("b) Update existing asset");
            Console.WriteLine("c) Delete existing asset");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    AssetMenu();
                    break;
                case ConsoleKey.B:
                    CategoryMenu();
                    break;
                case ConsoleKey.C:
                    OfficeMenu();
                    break;
                case ConsoleKey.D:
                    CurrencyMenu();
                    break;
                case ConsoleKey.E:
                    ReportMenu();
                    break;

                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    AssetMenu();
                    break;
            }
        }

        private void CategoryMenu()
        {
            Header("Category Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new category");
            Console.WriteLine("b) Update existing category");
            Console.WriteLine("c) Delete existing category");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                CategoryMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void OfficeMenu()
        {
            Header("Office Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new office");
            Console.WriteLine("b) Update existing office");
            Console.WriteLine("c) Delete existing office");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                OfficeMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void CurrencyMenu()
        {
            Header("Currency Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new currency");
            Console.WriteLine("b) Update existing currency");
            Console.WriteLine("c) Delete existing currency");
            Console.WriteLine("d) Show existing currencies");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewCurrency();
                    break;
                case ConsoleKey.B:
                    PageUpdateCurrency();
                    break;
                case ConsoleKey.C:
                    PageDeleteCurrency();
                    break;
                case ConsoleKey.D:
                    {
                        Header("Existing currencies");
                        ShowCurrencies();
                        Console.ReadKey();
                        CurrencyMenu();
                        break;
                    }
               
                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    CurrencyMenu();
                    break;
            }
        }

        private void ReportMenu()
        {
            Header("Reports");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Show categories");
            Console.WriteLine("b) Show all assets");
            Console.WriteLine("c) Show assets due to be replaced");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            if (command == ConsoleKey.Enter)
                ReportMenu();

            if (command == ConsoleKey.Q)
                MainMenu();
        }

        private void PageAddNewCurrency()
        {
            Header("Add new currency");
            ShowCurrencies();

            var currency =  new Currency();
            Write("Currency name: ");
            string curName = Console.ReadLine();
            currency.CurrencyName = curName;

            double xRate = 0;
            Write("Exchange rate vs USD: ");
            try
            {
                xRate = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Write("Numerical values only...");
                Console.ReadKey();
                PageAddNewCurrency();
            }
            currency.CurrencyToUSD = xRate;

            _context.Currency.Add(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " added...");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void PageUpdateCurrency()
        {
            Header("Update currency");
            ShowCurrencies();
            Write("Which currency do you want to update? ");
            int currencyId = int.Parse(Console.ReadLine());
            var currency = _context.Currency.Find(currencyId);

            WriteLine("Current name: " + currency.CurrencyName);
            Write("New name: ");
            string newName = Console.ReadLine();
            currency.CurrencyName = newName;

            WriteLine("Old exchange rate to USD: " + currency.CurrencyToUSD.ToString());
            Write("New exchange rate: ");
            double newRate = 0;
            try
            {
                newRate = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Write("Numerical values only...");
                Console.ReadKey();
                PageUpdateCurrency();
            }
            currency.CurrencyToUSD = newRate;

            _context.Currency.Update(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " updated");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void PageDeleteCurrency()
        {
            Header("Delete currency");
            ShowCurrencies();

            Write("Which currency do you want to delete? ");
            int currencyId = int.Parse(Console.ReadLine());

            var currency = _context.Currency.Find(currencyId);
            _context.Currency.Remove(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " deleted...");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void ShowCurrencies()
        {
            WriteLine("id".PadRight(5) + "Name".PadRight(20) + "Exchange rate".PadRight(20));
            foreach(var x in _context.Currency)
            {
                WriteLineBlue(x.CurrencyId.ToString().PadRight(5) + x.CurrencyName.ToString().PadRight(20) + x.CurrencyToUSD.ToString().PadRight(20));
            }
        }

        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine("------------------------------------");
        }

        private void WriteLine(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        private void WriteLineBlue(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
        }

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }
}
