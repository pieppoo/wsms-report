using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class CustomerData
    {
        public string CompanyName { get; set; }
        public string ReportTitle { get; set; }
        public List<Customer> CustomerList { get; set; }

        public class Customer
        {
            public string Name { get; set; }
            public string Level { get; set; }
            public string PhoneNo { get; set; }
            public string Address { get; set; }
            public string Debt { get; set; }
        }
    }
}
