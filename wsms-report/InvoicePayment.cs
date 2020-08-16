using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using wsms.report.Model;
using System.Drawing.Printing;
using DevExpress.XtraPrinting;

namespace wsms.report
{
    public partial class InvoicePayment : DevExpress.XtraReports.UI.XtraReport
    {
        public PaymentData Data { get; set; }
        public PaymentType Type { get; set; }

        public delegate void PrintComplete(object o, PrintEventArgs e);
        public event PrintComplete OnPrintingReceiptComplete;


        public InvoicePayment()
        {
            InitializeComponent();
        }

        public void PopulateData() 
        {
            if (Data != null)
            {
                if (Type == PaymentType.Sales)
                {
                    lblNameTitle.Text = "Dibayarkan Oleh";
                }
                else
                {
                    lblNameTitle.Text = "Dibayarkan Ke";
                }


                lblName.Text = Data.Name;
                lblPaymentMode.Text = Data.PaymentMode;
                lblPaymentDate.Text = Data.PayDate;
                lblInvoiceNo.Text = Data.InvoiceNo;
                lblDueDate.Text = Data.DueDate;

                lblAmount.Text = Data.PaymentAmount;
                lblAmountWritten.Text = Data.PaymentWritten;
                lblRemarks.Text = Data.Remarks;

                lblCardNo.Text = Data.CardNo;
                lblRefNo.Text = Data.CardRef;

                lblTotalAmount.Text = Data.TotalAmount;
                lblPaidAmount.Text = Data.TotalPaid;
                lblBalanceAmount.Text = Data.BalanceAmount;

                PrintingSystem.Document.Name = "Faktur Pembayaran - " + Data.InvoiceNo;
            }
        }

        public bool ValidateForm()
        {
            return !string.IsNullOrEmpty(lblName.Text) &&
                !string.IsNullOrEmpty(lblPaymentMode.Text) &&
                !string.IsNullOrEmpty(lblPaymentDate.Text) &&
                !string.IsNullOrEmpty(lblInvoiceNo.Text) &&
                !string.IsNullOrEmpty(lblDueDate.Text);
        }

        public void PrintReceiptDialog()
        {
            if (ValidateForm())
            {
                using (ReportPrintTool printTool = new ReportPrintTool(this))
                {
                    // Invoke the Print dialog.
                    printTool.PrintDialog();
                    printTool.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
                }
            }
            else
            {
                throw new NullReferenceException("Receipt data hasn't populated");
            }
        }

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.EndPrint += new PrintEventHandler(PrintDocument_EndPrint);
        }

        void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            if (OnPrintingReceiptComplete != null)
                OnPrintingReceiptComplete(sender, e);
        }

        public void ShowPrintPreview()
        {
            if (ValidateForm())
            {
                using (ReportPrintTool printTool = new ReportPrintTool(this))
                {
                    var ps = printTool.PrintingSystem;
                    ps.SetCommandVisibility(PrintingSystemCommand.Background, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.Watermark, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.Save, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.FillBackground, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.ClosePreview, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.Find, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None);

                    ps.SetCommandVisibility(PrintingSystemCommand.SendCsv, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendFile, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendGraphic, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendMht, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendPdf, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendRtf, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendTxt, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendXls, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendXlsx, CommandVisibility.None);
                    ps.SetCommandVisibility(PrintingSystemCommand.SendXps, CommandVisibility.None);

                    printTool.ShowPreviewDialog();
                }
            }
        }
    }
}
