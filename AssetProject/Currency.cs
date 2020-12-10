using System;
using System.Collections.Generic;
using System.Text;

namespace EFAssets
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyToUSD { get; set; }
    }
}
