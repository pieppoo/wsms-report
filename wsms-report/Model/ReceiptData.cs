using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class ReceiptData
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhoneNo { get; set; }

        public string Name { get; set; }
        public string AddressPhoneNo { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string DueDate { get; set; }

        public string PaymentMode { get; set; }
        public string CardNo { get; set; }
        public string RefNo { get; set; }
        public string SubTotal { get; set; }
        public string Discount { get; set; }
        public string TotalAmount { get; set; }

        public List<OrderDetails> OrderList { get; set; }

        public class OrderDetails
        {
            public string ItemName { get; set; }
            public string Count { get; set; }
            public string UnitPrice { get; set; }
            public string Discount { get; set; }
            public string Total { get; set; }
        }
    }
}
