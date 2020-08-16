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
            report.PrintReceiptDialog();
        }
    }
}
