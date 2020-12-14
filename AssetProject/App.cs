using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

                case ConsoleKey.F12:
                    var egg = new EasterEgg1();
                    Console.Clear();
                    egg.ShowEasterEgg();
                    Console.ReadKey();
                    MainMenu();
                    break;

                case ConsoleKey.Q:
                    break;

                default:
                    MainMenu();
                    break;
            }

        }

        private void AssetMenu()
        {
            Header("Asset Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new asset");
            Console.WriteLine("b) Update existing asset");
            Console.WriteLine("c) Delete existing asset");
            Console.WriteLine("d) Show existing assets");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewAsset();
                    break;
                case ConsoleKey.B:
                    PageUpdateAsset();
                    break;
                case ConsoleKey.C:
                    PageDeleteAsset();
                    break;
                case ConsoleKey.D:
                    Header("Existing assets");
                    ShowAssets(0);
                    Console.ReadKey();
                    AssetMenu();
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
            Console.WriteLine("d) Show existing categories");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;
            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewCategory();
                    break;
                case ConsoleKey.B:
                    PageUpdateCategory();
                    break;
                case ConsoleKey.C:
                    PageDeleteCategory();
                    break;
                case ConsoleKey.D:
                    {
                        Header("Existing categories");
                        ShowCategories(0);
                        Console.ReadKey();
                        CategoryMenu();
                        break;
                    }

                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    OfficeMenu();
                    break;
            }
        }

        private void OfficeMenu()
        {
            Header("Office Menu");

            Console.WriteLine("What do you want to do");
            Console.WriteLine("a) Add new office");
            Console.WriteLine("b) Update existing office");
            Console.WriteLine("c) Delete existing office");
            Console.WriteLine("d) Show existing offices");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    PageAddNewOffice();
                    break;
                case ConsoleKey.B:
                    PageUpdateOffice();
                    break;
                case ConsoleKey.C:
                    PageDeleteOffice();
                    break;
                case ConsoleKey.D:
                    {
                        Header("Existing offices");
                        ShowOffices(0);
                        Console.ReadKey();
                        OfficeMenu();
                        break;
                    }

                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    OfficeMenu();
                    break;
            }
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
            Console.WriteLine("a) Assets per office");
            Console.WriteLine("b) Cost of replacing expiring assets");
            Console.WriteLine("c) Assets per office, other version");
            Console.WriteLine("\nq) Return to Main menu");

            ConsoleKey command = Console.ReadKey(true).Key;

            // Using switch eliminates all other keypresses as invalid
            switch (command)
            {
                case ConsoleKey.A:
                    ReportAssetsPerOffice();
                    //WriteBlue("Report 1 placeholder");
                    //Console.ReadKey();
                    //ReportMenu();
                    break;
                case ConsoleKey.B:
                    ReportAssetsCosts();
                    //WriteBlue("Report 2 placeholder");
                    //Console.ReadKey();
                    //ReportMenu();
                    break;
                case ConsoleKey.C:
                    Header("Report");
                    Report report = new Report();
                    report.ShowInventoryPerOffice();
                    Console.WriteLine("");
                    WriteBlue("Press any key to cotinue..");
                    Console.ReadKey();
                    ReportMenu();
                    break;



                case ConsoleKey.Q:
                    MainMenu();
                    break;

                default:
                    ReportMenu();
                    break;
            }
        }

        /// <summary>
        /// Add new asset to database
        /// Connects to Category and Office
        /// Calculates Replacement date based on Life span from Category
        /// Calculates a Warning date (3 months before Replacemant date)
        /// </summary>
        private void PageAddNewAsset()
        {
            // Date calculations
            var DF = new DateFunctions();

            // Check if any categories exist in category table. Assets MUST have a category.
            // If no category exist, redirect user to Category menu to first create categories
            if (!_context.Category.Any())
            {
                WriteLine("No categories exist.. Asset must have a category.");
                Write("Press any key to go to Category menu.");
                Console.ReadKey();
                CategoryMenu();
                return;
            }
            else
            {
                Header("Add a new asset");
                ShowCategories(1);
                ShowOffices(1);
                ShowAssets(1);

                var newAsset = new Asset();

                Write("\nAsset name: ");
                string newAssetName = Console.ReadLine();

                Write("Purchase date (yyyy-mm-dd): ");
                DateTime newPurchaseDate;
                try
                {
                    newPurchaseDate = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Invalid date format, should be yyyy-mm-dd...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }
                Write("Purchase price in USD: ");
                double newPurchasePrice;
                try
                {
                    newPurchasePrice = double.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Must be numerical value...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                Write("Select category for asset: ");
                int newCategoryID;
                try
                {
                    newCategoryID = int.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Must be numeric...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                Write("Select office asset belongs to: ");
                int newOfficeID;
                try
                {
                    newOfficeID = int.Parse(Console.ReadLine());
                }
                catch
                {

                    Write("Numerical values only...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                // Find the category to determine LifeSpan of asset
                var category = _context.Category.Find(newCategoryID);

                // Find the officeName
                var office = _context.Offices.Find(newOfficeID);

                // Calculate ExpirationDate and a WarningDate
                DateTime newExpirationDate = DateFunctions.CalculateEolDate(newPurchaseDate, category.CategoryEOLMonths);
                DateTime newWarningDate = DateFunctions.CalculateWarningDate(newPurchaseDate, category.CategoryEOLMonths - 3);

                // All done, load user input for creation of asset
                newAsset.AssetName = newAssetName;
                newAsset.AssetPrice = newPurchasePrice;
                newAsset.AssetPurchaseDate = newPurchaseDate;
                newAsset.AssetExpirationDate = newExpirationDate;
                newAsset.AssetWarningDate = newWarningDate;
                newAsset.CategoryId = newCategoryID;
                newAsset.OfficeId = newOfficeID;
                // We assume all assets are active, for convinience...
                newAsset.AssetActive = true;

                // Do it!
                _context.Assets.Add(newAsset);
                _context.SaveChanges();

                Write("Asset: ");
                WriteBlue(newAssetName);
                Write(" with replacement date: ");
                WriteBlue(newExpirationDate.ToString("d"));
                Write(" added to category: ");
                WriteBlue(category.CategoryName);
                Write(" and office: ");
                WriteBlue(office.OfficeName);

                Console.ReadKey();
                AssetMenu();
                return;
            }

        }

        /// <summary>
        /// Updates an asset
        /// If purchase date changed, then recalculates new End Of Life date and Warning Date
        /// </summary>
        private void PageUpdateAsset()
        {
            Header("Update existing asset");
            ShowOffices(1);
            ShowCategories(1);
            ShowAssets(1);

            Write("\nWhich asset would you like to update? (ID = 0 to abort): ");
            int assetID;
            try
            {
                assetID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                AssetMenu();
                return;
            }

            // User aborts operation
            if (assetID == 0)
            {
                AssetMenu();
                return;
            }

            // Check if selected asset exists in database
            var asset = _context.Assets.Find(assetID);

            // User input variables
            string newName = "";
            DateTime newPurchaseDate;
            int newCategoryID;
            int newOfficeID;
            double newPurchasePrice;

            if (asset == null)
            {
                Write("That asset does not exist...");
                AssetMenu();
                return;
            }
            else
            {
                WriteLine("Existing asset name: " + asset.AssetName);
                Write("Enter new name (Enter to keep old name): ");
                newName = Console.ReadLine();
                if (newName == "")
                    newName = asset.AssetName;

                WriteLine("Existing purchase date: " + asset.AssetPurchaseDate.ToString("d"));
                Write("Enter new purchase date (yyyy-mm-dd): ");
                try
                {
                    newPurchaseDate = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    Write("Invalid date format. Must be like yyyy-mm-dd...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                WriteLine("Existing purchase price: " + asset.AssetPrice);
                Write("Enter new purchase price: ");
                try
                {
                    newPurchasePrice = double.Parse(Console.ReadLine());
                }
                catch
                {
                    Write("Numerical values only...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                WriteLine("Existing category ID: " + asset.CategoryId);
                Write("Enter new category ID: ");
                try
                {
                    newCategoryID = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Write("Numerical values only...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                // Check if valid category selected
                var category = _context.Category.Find(newCategoryID);
                if (category == null)
                {
                    Write("That category does not exist...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                WriteLine("Existing office ID: " + asset.OfficeId);
                Write("Enter new office ID: ");
                try
                {
                    newOfficeID = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Write("Numerical values only...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }

                var office = _context.Offices.Find(newOfficeID);
                if (office == null)
                {
                    Write("That office does not exist...");
                    Console.ReadKey();
                    AssetMenu();
                    return;
                }
                // Calculate ExpirationDate and a WarningDate
                DateTime newExpirationDate = DateFunctions.CalculateEolDate(newPurchaseDate, category.CategoryEOLMonths);
                DateTime newWarningDate = DateFunctions.CalculateWarningDate(newPurchaseDate, category.CategoryEOLMonths - 3);

                // All done for update
                asset.AssetName = newName;
                asset.AssetPurchaseDate = newPurchaseDate;
                asset.AssetPrice = newPurchasePrice;
                asset.CategoryId = newCategoryID;
                asset.OfficeId = newOfficeID;
                asset.AssetExpirationDate = newExpirationDate;
                asset.AssetWarningDate = newWarningDate;

                _context.Assets.Update(asset);
                _context.SaveChanges();

                Write("Asset: " + asset.AssetName + "updated");
                Console.ReadKey();
                AssetMenu();
            }
        }

        /// <summary>
        /// Deletes an asset from database
        /// </summary>
        private void PageDeleteAsset()
        {
            // Stuff happening
            Header("Delete existing asset");
            ShowAssets(1);

            Write("Which asset do you want to delete (ID = 0 to abort): ");
            int assetID;
            try
            {
                assetID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                AssetMenu();
                return;
            }
            // User aborts deletion with ID = 0
            if (assetID == 0)
            {
                AssetMenu();
                return;
            }

            // Check if assetID exists in database
            var asset = _context.Assets.Find(assetID);
            if (asset == null)
            {
                Write("That asset does not exist...");
                Console.ReadKey();
                AssetMenu();
                return;
            }
            else
            {
                _context.Assets.Remove(asset);
                _context.SaveChanges();

                Write("Asset: " + asset.AssetName + " deleted...");
                Console.ReadKey();
            }
            AssetMenu();

        }

        /// <summary>
        /// Shows basic asset info. parameter: int displayHeader if neader display is showed or not
        /// </summary>
        /// <param name="displayHeader"></param>
        private void ShowAssets(int displayHeader)
        {
            if (displayHeader == 1)
                WriteLine("Existing assets:");
            WriteLine("id".PadRight(5) + "Name".PadRight(25) + "Purchase price USD".PadRight(20) + "Purchase date".PadRight(15) + "Replacement date".PadRight(20));
            foreach (var x in _context.Assets)
            {

                WriteLineBlue(x.AssetId.ToString().PadRight(5) + x.AssetName.ToString().PadRight(25) + x.AssetPrice.ToString().PadRight(20) + x.AssetPurchaseDate.ToString("d").PadRight(15) + x.AssetExpirationDate.ToString("d").PadRight(20));
            }

        }

        /// <summary>
        /// Add new Category with LifeSpan in months for assets
        /// </summary>
        private void PageAddNewCategory()
        {
            Header("Add new category");
            // If no prevous categories in database, notify user
            if (!_context.Category.Any())
            {
                WriteLine("No prevous categories exist...");
            }
            else
            {
                ShowCategories(1);
            }

            var newCategory = new Category();

            Write("New category name: ");
            string newCatName = Console.ReadLine();

            Write("Lifespan of assets in category (months): ");
            int newLifespan;
            try
            {
                newLifespan = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

            // All done load user input for creation
            newCategory.CategoryName = newCatName;
            newCategory.CategoryEOLMonths = newLifespan;

            _context.Category.Add(newCategory);
            _context.SaveChanges();

            Write("New category: " + newCategory.CategoryName + " with asset lifespan of " + newLifespan + " months added.");
            Console.ReadKey();
            CategoryMenu();
        }

        /// <summary>
        /// Update Category with new info
        /// </summary>
        private void PageUpdateCategory()
        {
            Header("Update existing category");
            ShowCategories(1);

            Write("Which category would like to update? (ID = 0 to abort): ");
            int categoryID;
            try
            {
                categoryID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

            // User aborts operation
            if (categoryID == 0)
            {
                CategoryMenu();
                return;
            }

            // Check if selected category exists in database
            var category = _context.Category.Find(categoryID);
            if (category == null)
            {
                Write("That category does not exist...");
                CategoryMenu();
                return;
            }
            else
            {
                WriteLine("Existing category name is: " + category.CategoryName);
                Write("Enter new name: ");
                string newName = Console.ReadLine();

                WriteLine("Existing lifespan of attached assets (in months) is: " + category.CategoryEOLMonths);
                Write("Enter new lifespan of assets in category (months)");
                int newLifespan;
                try
                {
                    newLifespan = int.Parse(Console.ReadLine());
                }
                catch
                {
                    WriteLine("Numerical values only...");
                    Console.ReadKey();
                    CategoryMenu();
                    return;
                }

                // Save Old EOL-months for comparing with new
                int oldLifespan = category.CategoryEOLMonths;

                // All done load user input for update to database
                category.CategoryName = newName;
                category.CategoryEOLMonths = newLifespan;

                _context.Category.Update(category);
                _context.SaveChanges();


                // If LifeSpan for category has changed, recalculate new EOL dates and warning dates 
                // for all assets in that category.
                if (oldLifespan != newLifespan)
                {
                    var asset = _context.Assets.ToList();
                    foreach (var x in asset)
                    {
                        if (x.CategoryId == categoryID)
                        {
                            // Recalculate ExpirationDate and a WarningDate
                            DateTime newExpirationDate = DateFunctions.CalculateEolDate(x.AssetPurchaseDate, newLifespan);
                            DateTime newWarningDate = DateFunctions.CalculateWarningDate(x.AssetPurchaseDate, newLifespan - 3);

                            x.AssetExpirationDate = newExpirationDate;
                            x.AssetWarningDate = newWarningDate;
                            _context.Assets.Update(x);
                            _context.SaveChanges();
                        }
                    }
                }
                Write("Category is updated...");
                Console.ReadKey();
                CategoryMenu();
            }
        }

        /// <summary>
        /// Deletes a category
        /// **Note: Categories will not be deleted if any assets are attached to it...
        /// </summary>
        private void PageDeleteCategory()
        {
            // *** MUST check if any assets are connected to category before deletion
            // *** Category cannot be deleted if ANY assets are connected.

            Header("Delete existing category");
            ShowCategories(1);

            Write("Which category would you like to delete? (ID = 0 to abort): ");
            int categoryID;
            try
            {
                categoryID = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Numerical values only...");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

            // User aborts operation
            if (categoryID == 0)
            {
                CategoryMenu();
                return;
            }

            // Check if selected category exists in database
            var category = _context.Category.Find(categoryID);
            var asset = _context.Assets.Find(category.CategoryId);
            if (category == null)
            {
                Write("That category does not exist...");
                CategoryMenu();
                return;
            }

            // Check if any assets are connected to the category
            if (asset != null)
            {
                WriteLineRed(" ** There are assets attached to the category: " + category.CategoryName);
                WriteLineRed(" Update assets and attach them to a different category,");
                WriteLineRed(" before trying to delete this category.");
                Console.ReadKey();
                CategoryMenu();
                return;
            }
            else
            {
                _context.Category.Remove(category);
                _context.SaveChanges();

                Write("Category: " + category.CategoryName + " is deleted");
                Console.ReadKey();
                CategoryMenu();
                return;
            }

        }

        /// <summary>
        /// Displays basic Category info
        /// </summary>
        /// <param name="displayHeader"></param>
        private void ShowCategories(int displayHeader)
        {
            // If category table empty, don't bother trying to read from db
            if (!_context.Category.Any())
            {
                WriteLine("No prevous categories exist... Nothing to show..");
                Console.ReadKey();
                //CategoryMenu();
                //return;
            }
            else
            {
                // To avoid unnecessary info
                if (displayHeader == 1)
                    WriteLine("Existing categories:");
                WriteLine("ID".PadRight(5) + "Name".PadRight(25) + "Lifespan (months)".PadRight(20));
                foreach (var x in _context.Category)
                {
                    WriteLineBlue(x.CategoryId.ToString().PadRight(5) + x.CategoryName.ToString().PadRight(25) + x.CategoryEOLMonths.ToString().PadRight(20));
                }
            }
        }

        /// <summary>
        /// Adds a new regonal office.
        /// Must have currencies in the database, since an office must have a currency
        /// </summary>
        private void PageAddNewOffice()
        {
            Header("Add new office");
            ShowCurrencies();
            ShowOffices(1);

            // Check if any currencies exist in currency table. Offices MUST have a currency.
            // If no currency exist, redirect user to Currency menu
            if (!_context.Currency.Any())
            {
                WriteLine("No currencies exist.. Office must have a currency.");
                Write("Press any key to go to Currency menu.");
                Console.ReadKey();
                CurrencyMenu();
            }


            Write("Office name: ");
            string newName = Console.ReadLine();


            Write("Country (ex USA, GER, SWE): ");
            string newCountry = Console.ReadLine();


            Write("Select currencyID from above: ");
            int currencyId = 0;
            try
            {
                currencyId = int.Parse(Console.ReadLine());

                // Check if selected CurrencyID exists in table
                // Otherwise start again
                var chkCurrencyId = _context.Currency.Find(currencyId);
                if (chkCurrencyId == null)
                {
                    Write("Only existing currency IDs are valid..");
                    Console.ReadKey();
                    OfficeMenu();
                    return;
                }
            }
            catch
            {
                Write("Only numerical values ...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // All done, load object with user entries
            var office = new Office();
            office.OfficeName = newName;
            office.OfficeCountry = newCountry;
            office.CurrencyId = currencyId;

            _context.Offices.Add(office);
            _context.SaveChanges();

            Write("Office " + office.OfficeName + " " + office.OfficeCountry + " added...");
            Console.ReadKey();
            OfficeMenu();
        }

        private void PageDeleteOffice()
        {
            Header("Delete existing office");
            ShowOffices(0);

            Write("Which office do you want to delete (ID= 0 to abort): ");
            int officeId = 0;
            try
            {
                officeId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // User aborts deletion with ID = 0
            if (officeId == 0)
            {
                OfficeMenu();
                return;
            }
            // Check if officeId exists in database
            var office = _context.Offices.Find(officeId);
            if (office == null)
            {
                Write("That office does not exist...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }
            else
            {
                _context.Offices.Remove(office);
                _context.SaveChanges();

                Write("Office " + office.OfficeName + ", " + office.OfficeCountry + " deleted...");
                Console.ReadKey();
            }
            OfficeMenu();
        }

        private void PageUpdateOffice()
        {
            Header("Update existing office");
            ShowOffices(0);
            ShowCurrencies();
            int officeId;

            Write("Which office do you want to update (ID = 0 to abort): ");
            try
            {
                officeId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // User aborts update with ID = 0
            if (officeId == 0)
            {
                OfficeMenu();
                return;
            }
            // Check if officeId exists in database
            var office = _context.Offices.Find(officeId);
            var curr = _context.Currency.Find(office.CurrencyId);
            if (office == null)
            {
                Write("That office does not exist...");
                OfficeMenu();
                return;
            }
            string currName = curr.CurrencyName;
            WriteLine("Current office name: " + office.OfficeName);
            Write("Enter new name: ");
            string newName = Console.ReadLine();
            WriteLine("Current country is " + office.OfficeCountry);
            Write("Enter new country (ex USA, GER, SWE): ");
            string newCountry = Console.ReadLine();
            WriteLine("Current currency is: " + office.CurrencyId + " " + currName);
            Write("Enter new currency ID: ");
            int newCurrencyId;
            try
            {
                newCurrencyId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Write("Only numerical values...");
                Console.ReadKey();
                OfficeMenu();
                return;
            }

            // All done load user input for update
            office.OfficeName = newName;
            office.OfficeCountry = newCountry;
            office.CurrencyId = newCurrencyId;

            _context.Offices.Update(office);
            _context.SaveChanges();

            Write("Office " + office.OfficeName + ", " + office.OfficeCountry + " updated...");
            Console.ReadKey();
            OfficeMenu();
            return;

        }

        private void ShowOffices(int showHeader)
        {
            if (showHeader == 1)
                WriteLine("Existing offices:");
            WriteLine("id".PadRight(5) + "Name".PadRight(20) + "Country".PadRight(20) + "Currency".PadRight(10));
            foreach (var x in _context.Offices)
            {

                WriteLineBlue(x.OfficeId.ToString().PadRight(5) + x.OfficeName.ToString().PadRight(20) + x.OfficeCountry.ToString().PadRight(20) + x.CurrencyId.ToString().PadRight(10));
            }
        }

        private void PageAddNewCurrency()
        {
            Header("Add new currency");
            ShowCurrencies();

            var currency = new Currency();
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

            Write("Which currency do you want to delete? (0) to abort ");
            int currencyId = int.Parse(Console.ReadLine());
            if (currencyId == 0)
                return;

            var currency = _context.Currency.Find(currencyId);
            _context.Currency.Remove(currency);
            _context.SaveChanges();

            Write("Currency " + currency.CurrencyName + " deleted...");
            Console.ReadKey();
            CurrencyMenu();
        }

        private void ShowCurrencies()
        {
            WriteLine("Existing currencies:");
            WriteLine("id".PadRight(5) + "Name".PadRight(20) + "Exchange rate".PadRight(20));
            foreach (var x in _context.Currency)
            {
                WriteLineBlue(x.CurrencyId.ToString().PadRight(5) + x.CurrencyName.ToString().PadRight(20) + x.CurrencyToUSD.ToString().PadRight(20));
            }
        }

        /// <summary>
        /// Simple list of assets by offices and categories
        /// color codes assets yellow 6 months before exp date
        /// and red 3 months before exp date
        /// </summary>
        private void ReportAssetsPerOffice()
        {
            Header("Assets per office");

            DateTime today = DateTime.Now;
            int date_dif;

            var office = _context.Offices.ToList();
            var category = _context.Category.ToList();
            var asset = _context.Assets.ToList();
            var cur = _context.Currency.ToList();

            foreach (var ofc in office)
            {
                WriteLine(ofc.OfficeName + ", " + ofc.OfficeCountry);
                WriteLine("\t\tName".PadRight(25) + "Purchased".PadRight(15) + "Expires".PadRight(15) + "Replace date".PadRight(15));
                foreach (var cat in category)
                {
                    WriteLineBlue("\t" + cat.CategoryName);
                    foreach (var a in asset)
                    {
                        if (a.OfficeId == ofc.OfficeId && a.CategoryId == cat.CategoryId)
                        {
                            date_dif = DateFunctions.GetMonthDifference(today, a.AssetExpirationDate);
                            if (date_dif <= 3 || a.AssetExpirationDate < today)
                            {
                                //WriteBlue("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15));

                                WriteLineRed("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15) + a.AssetExpirationDate.ToString("d").PadRight(15) + a.AssetWarningDate.ToString("d").PadRight(15));

                            }
                            else if (date_dif <= 6)
                            {
                                //WriteBlue("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15));
                                WriteLineYellow("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15) + a.AssetExpirationDate.ToString("d").PadRight(15) + a.AssetWarningDate.ToString("d").PadRight(15));
                            }
                            else
                                WriteLineBlue("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15) + a.AssetExpirationDate.ToString("d").PadRight(15) + a.AssetWarningDate.ToString("d").PadRight(15));
                        }
                    }
                }
            }
            Console.ReadKey();
            ReportMenu();
        }

        /// <summary>
        /// Report on costs of replacing expiring assets
        /// per office and category in local currency
        /// </summary>
        private void ReportAssetsCosts()
        {
            Header("Cost of replacing expiring assets");
            DateTime today = DateTime.Now;
            int date_dif;


            var office = _context.Offices.ToList();
            var category = _context.Category.ToList();
            var asset = _context.Assets.ToList();
            var cur = _context.Currency.ToList();

            foreach (var ofc in office)
            {
                double sumOfExpenses = 0;
                double totalOfficeExpenses = 0;
                // Get currency and exchange rate
                var currency = _context.Currency.Find(ofc.CurrencyId);
                double exchangeRate = currency.CurrencyToUSD;

                WriteLine(ofc.OfficeName + ", " + ofc.OfficeCountry);
                WriteLine("\t\tName".PadRight(25) + "Purchased".PadRight(15) + "Expires".PadRight(15) + "Replace date".PadRight(15));
                foreach (var cat in category)
                {
                    WriteLineBlue("\t" + cat.CategoryName);
                    foreach (var a in asset)
                    {
                        double localValue = 0;
                        if (a.OfficeId == ofc.OfficeId && a.CategoryId == cat.CategoryId)
                        {
                            date_dif = DateFunctions.GetMonthDifference(today, a.AssetExpirationDate);
                            if (date_dif <= 3 || a.AssetExpirationDate < today)
                            {
                                // Convert to local currency and summarize costs
                                localValue = a.AssetPrice / exchangeRate;
                                sumOfExpenses += localValue;
                                WriteLineBlue("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15) + a.AssetExpirationDate.ToString("d").PadRight(15) + localValue.ToString("F").PadRight(15) + currency.CurrencyName.ToString().PadRight(5));

                            }

                        }
                    }
                    WriteLineBlue("Total replacement expenses for: " + cat.CategoryName.ToString() + " " + currency.CurrencyName + " " + sumOfExpenses.ToString("F"));
                    totalOfficeExpenses += sumOfExpenses;
                    sumOfExpenses = 0;
                }
                WriteLineYellow("Total replacement expenses for office: " + ofc.OfficeName.ToString() + " are: " + currency.CurrencyName + " " + totalOfficeExpenses.ToString("F"));
                totalOfficeExpenses = 0;
                sumOfExpenses = 0;
            }
            Console.ReadKey();
            ReportMenu();
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

        private void WriteLineRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
        }
        private void WriteLineYellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
        }
        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }

        private void WriteBlue(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
        }

        private void WriteRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
        }

        private void WriteYellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
        }
    }
}