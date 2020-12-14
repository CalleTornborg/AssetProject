using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFAssets
{
    public class Report
    {
        //public int OfficeId { get; set; }
        static AssetContext _context = new AssetContext();

        public void ShowInventoryPerOffice()
        {
            Console.WriteLine("");
            Console.WriteLine("\tInventory of active devices for each office:");
            

            DateTime today = DateTime.Now;
            int date_dif;
            double totalSummaryOffice;
            double totalSummaryCorp;
            totalSummaryOffice = 0; 
            totalSummaryCorp = 0;

            var officeList = _context.Offices.ToList();
            var categoryList = _context.Category.ToList();
            var assetList = _context.Assets.ToList();
            var curList = _context.Currency.ToList();

            foreach (var office in officeList)
            {
                totalSummaryOffice = 0;
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("\t" + office.OfficeName.ToUpper() + ", " + office.OfficeCountry.ToUpper());
                Console.WriteLine("");
                Console.WriteLine("\t\tDevice name".PadRight(25) + "Purchased".PadRight(15) + "Expires".PadRight(15) + "Replace date".PadRight(15));
                
                foreach (var cat in categoryList)
                {
                    Console.WriteLine(""); 
                    Console.WriteLine("\t" + cat.CategoryName);
                    foreach (var asset in assetList)
                    {
                        if (asset.OfficeId == office.OfficeId && asset.CategoryId == cat.CategoryId)
                        {
                            date_dif = DateFunctions.GetMonthDifference(today, asset.AssetExpirationDate);

                            totalSummaryOffice += asset.AssetPrice;

                            if (date_dif <= 3 || asset.AssetExpirationDate < today)
                            {
                                //WriteBlue("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15));
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\t\t" + asset.AssetName.ToString().PadRight(25) + asset.AssetPurchaseDate.ToString("d").PadRight(15) + asset.AssetExpirationDate.ToString("d").PadRight(15) + asset.AssetWarningDate.ToString("d").PadRight(15));
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (date_dif <= 6)
                            {
                                //WriteBlue("\t\t" + a.AssetName.ToString().PadRight(25) + a.AssetPurchaseDate.ToString("d").PadRight(15));
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\t\t" + asset.AssetName.ToString().PadRight(25) + asset.AssetPurchaseDate.ToString("d").PadRight(15) + asset.AssetExpirationDate.ToString("d").PadRight(15) + asset.AssetWarningDate.ToString("d").PadRight(15));
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                                Console.WriteLine("\t\t" + asset.AssetName.ToString().PadRight(25) + asset.AssetPurchaseDate.ToString("d").PadRight(15) + asset.AssetExpirationDate.ToString("d").PadRight(15) + asset.AssetWarningDate.ToString("d").PadRight(15));
                        }

                        
                    }


                }
                Console.WriteLine("");
                Console.WriteLine("\tTotal value of office inventory: " + totalSummaryOffice + " USD");
            }
        } 
    }
}
