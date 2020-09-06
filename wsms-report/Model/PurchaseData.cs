using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class PurchaseData
    {
        public string CompanyName { get; set; }
        public string ReportTitle { get; set; }
        public string MonthYear { get; set; }

        public string SubTotal { get; set; }
        public string Discount { get; set; }
        public string GrandTotal { get; set; }

        public List<Purchase> PurchaseList { get; set; }

        public class Purchase
        {
            public string InvoiceNo { get; set; }
            public string ItemName { get; set; }
            public string QuantityUnitPrice { get; set; }
            public string SubTotal { get; set; }
            public string Discount { get; set; }
            public string Total { get; set; }
        }
    }
}
