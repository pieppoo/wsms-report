using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class PaymentData
    {
        public string Name { get; set; }
        public string PaymentMode { get; set; }
        public string PayDate { get; set; }
        public string InvoiceNo { get; set; }
        public string DueDate { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentWritten { get; set; }
        public string Remarks { get; set; }
        public string TotalAmount { get; set; }
        public string TotalPaid { get; set; }
        public string BalanceAmount { get; set; }
        public string CardNo { get; set; }
        public string CardRef { get; set; }
    }
}
