using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class InventoryData
    {
        public string CompanyName { get; set; }
        public string ReportTitle { get; set; }
        public List<Item> ItemList { get; set; }

        public class Item
        {
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Barcode { get; set; }
            public string StockCount { get; set; }
            public string PurchasePrice { get; set; }
        }
    }
}
