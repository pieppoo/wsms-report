using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class ReceiptData
    {
        public string SupplierName { get; set; }
        public string CustomerName { get; set; }
        public string paymode { get; set; }
        public string paydate { get; set; }
        public string invoiceno { get; set; }
        public string duedate { get; set; }
        public string payamt { get; set; }
        public string payamtword { get; set; }
        public string renark { get; set; }
        public string totaldebt { get; set; }
        public string totalpaid { get; set; }
        public string debtbalance { get; set; }
        public string cardno { get; set; }
        public string cardref { get; set; }
    }
}
