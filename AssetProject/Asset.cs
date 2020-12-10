using System;
using System.Collections.Generic;
using System.Text;

namespace EFAssets
{
    public class Asset
    {
        public int CategoryId { get; set; }
        public int OfficeId { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime AssetPurchaseDate { get; set; }
        public DateTime AssetExpirationDate { get; set; }
        public DateTime AssetWarningDate { get; set; }
        public double AssetPrice { get; set; }
        public bool AssetActive { get; set; }
    }
}
