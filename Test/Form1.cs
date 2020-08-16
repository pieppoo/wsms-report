using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wsms.report.Model;
using wsms.report;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = new PaymentData();

            data.Name = "ANTHONY ARIFIN";
            data.PaymentMode = "TUNAI";
            data.PayDate = "2020";
            data.InvoiceNo = "123123123123";
            data.DueDate = "2020";

            var report = new InvoicePayment();
            report.Data = data;
            report.PopulateData();
            report.ShowPreviewDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = new ReceiptData();

            data.CompanyName = "PUDDING POP";
            data.CompanyAddress = "NULL";
            data.CompanyPhoneNo = "0778-423004";
            data.AddressPhoneNo = "alamat asd asd" + Environment.NewLine + "012313123123";
            data.OrderNo = "123123123123";

            data.OrderList = new List<ReceiptData.OrderDetails>
            {
                new ReceiptData.OrderDetails
                {
                    ItemName = "Sabun Cuci",
                    Count = "5",
                    UnitPrice = "40.000",
                    Discount = "10",
                    Total = "200.000"
                },
                new ReceiptData.OrderDetails
                {
                    ItemName = "If you need to do something different with the last element then you'd need something like",
                    Count = "1",
                    UnitPrice = "550.000",
                    Discount = "0",
                    Total = "550.000"
                },
            };

            var report = new InvoiceReceipt();
            report.Data = data;
            report.Type = ReceiptType.Purchase;
            report.PopulateData();
            report.ShowPreviewDialog();
        }
    }
}
