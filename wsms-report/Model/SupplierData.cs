using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsms.report.Model
{
    public class SupplierData
    {
        public string CompanyName { get; set; }
        public string ReportTitle { get; set; }
        public List<Supplier> SupplierList { get; set; }

        public class Supplier
        {
            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public string Address { get; set; }
            public string Debt { get; set; }
        }
    }
}
