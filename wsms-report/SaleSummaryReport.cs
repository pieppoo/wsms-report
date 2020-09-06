using System;
using System.Drawing.Printing;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using wsms.report.Model;

namespace wsms.report
{
    public partial class SaleSummaryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SaleSummaryData Data { get; set; }

        public delegate void PrintComplete(object o, PrintEventArgs e);
        public event PrintComplete OnPrintingReceiptComplete;

        public SaleSummaryReport()
        {
            InitializeComponent();
        }

        public void PopulateData()
        {
            if (Data != null)
            {
                lblCompanyName.Text = Data.CompanyName;
                lblTitle.Text       = Data.ReportTitle;
                lblMonthYear.Text   = Data.MonthYear;

                lblTotalTrx.Text    = Data.TotalTrx;
                lblTotalCash.Text   = Data.TotalCashTrx;
                lblTotalCard.Text   = Data.TotalCardTrx;

                lblSubtotal.Text    = Data.SubTotal;
                lblDiscount.Text    = Data.Discount;
                lblGrandTotal.Text  = Data.GrandTotal;

                lblPrintDate.Text   = DateTime.Now.ToString("dd/MM/yyyy");

                if (Data.SaleList != null && Data.SaleList.Count > 0)
                {
                    var i = 1;
                    var templateRow = tblDetails.Rows[1];
                    var currRow = templateRow;

                    foreach (var item in Data.SaleList)
                    {
                        currRow.Cells[0].Text = i.ToString();
                        currRow.Cells[1].Text = item.SaleDate;
                        currRow.Cells[2].Text = item.CustomerName;
                        currRow.Cells[3].Text = item.Level;
                        currRow.Cells[4].Text = item.PaymentType;
                        currRow.Cells[5].Text = item.SubTotal;
                        currRow.Cells[6].Text = item.Discount;
                        currRow.Cells[7].Text = item.Total;
                        currRow.Cells[8].Text = item.DueDate;

                        if (!Data.SaleList.Last().Equals(item))
                        {
                            tblDetails.InsertRowBelow(currRow);
                            i++;
                            currRow = tblDetails.Rows[i];

                            currRow.Cells[0].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[1].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[2].TextAlignment = TextAlignment.MiddleLeft;
                            currRow.Cells[2].Padding = templateRow.Cells[2].Padding;

                            currRow.Cells[3].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[4].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[5].TextAlignment = TextAlignment.MiddleRight;
                            currRow.Cells[5].Padding = templateRow.Cells[5].Padding;

                            currRow.Cells[6].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[7].TextAlignment = TextAlignment.MiddleRight;
                            currRow.Cells[7].Padding = templateRow.Cells[7].Padding;

                            currRow.Cells[8].TextAlignment = TextAlignment.MiddleCenter;
                        }
                    }
                }
            }
        }

        public bool ValidateForm()
        {
            return !string.IsNullOrEmpty(lblCompanyName.Text) &&
                !string.IsNullOrEmpty(lblTotalTrx.Text);
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
                throw new NullReferenceException("Report data hasn't populated");
            }
        }

        public void PrintReceipt()
        {
            if (ValidateForm())
            {
                using (ReportPrintTool printTool = new ReportPrintTool(this))
                {
                    // Invoke the Print dialog.
                    printTool.Print();
                    printTool.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
                }
            }
            else
            {
                throw new NullReferenceException("Report data hasn't populated");
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
