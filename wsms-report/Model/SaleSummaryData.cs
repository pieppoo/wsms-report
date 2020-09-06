﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class SaleSummaryData
    {
        public string CompanyName { get; set; }
        public string ReportTitle { get; set; }
        public string MonthYear { get; set; }

        public string TotalTrx { get; set; }
        public string TotalCashTrx { get; set; }
        public string TotalCardTrx { get; set; }

        public string SubTotal { get; set; }
        public string Discount { get; set; }
        public string GrandTotal { get; set; }

        public List<Sale> SaleList { get; set; }

        public class Sale
        {
            public string SaleDate { get; set; }
            public string CustomerName { get; set; }
            public string Level { get; set; }
            public string PaymentType { get; set; }
            public string SubTotal { get; set; }
            public string Discount { get; set; }
            public string Total { get; set; }
            public string DueDate { get; set; }
        }
    }
}
