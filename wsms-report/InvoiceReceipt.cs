using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using wsms.report.Model;
using System.Drawing.Printing;
using DevExpress.XtraPrinting;
using System.Linq;

namespace wsms.report
{
    public partial class InvoiceReceipt : DevExpress.XtraReports.UI.XtraReport
    {
        public ReceiptData Data { get; set; }
        public ReceiptType Type { get; set; }
        
        public delegate void PrintComplete(object o, PrintEventArgs e);
        public event PrintComplete OnPrintingReceiptComplete;

        public float DEFAULT_ROW_HEIGHT = 60f;

        public InvoiceReceipt()
        {
            InitializeComponent();
        }

        public void PopulateData()
        {
            if (Data != null)
            {
                if (Type == ReceiptType.Sales)
                {
                    lblNameTitle.Text = "Nama Pelanggan";
                    lblOrderNoTitle.Text = "Invoice No.";
                    lblOrderDateTitle.Text = "Tanggal Invoice";
                }
                else
                {
                    lblNameTitle.Text = "Supplier";
                    lblOrderNoTitle.Text = "Delivery Order No.";
                    lblOrderDateTitle.Text = "Tanggal Delivery Order";
                }

                lblCompanyName.Text = Data.CompanyName;
                lblCompanyAddress.Text = Data.CompanyAddress;
                lblCompanyPhoneNo.Text = Data.CompanyPhoneNo;

                lblName.Text = Data.Name;
                rtAddressPhoneNo.Text = Data.AddressPhoneNo;
                lblOrderNo.Text = Data.OrderNo;
                lblOrderDate.Text = Data.OrderDate;
                lblDueDate.Text = Data.DueDate;

                lblPaymentMode.Text = Data.PaymentMode;
                lblCardNo.Text = Data.CardNo;
                lblRefNo.Text = Data.RefNo;
                lblSubTotal.Text = Data.SubTotal;
                lblDiscount.Text = Data.Discount;
                lblTotalAmount.Text = Data.TotalAmount;

                if (Data.OrderList != null && Data.OrderList.Count > 0)
                {
                    var i = 1;
                    var templateRow = tblDetails.Rows[1];
                    var currRow = templateRow;

                    foreach (var item in Data.OrderList)
                    {
                        if (item.ItemName.Length > 35)
                        {
                            currRow.HeightF = DEFAULT_ROW_HEIGHT * (float)Math.Ceiling((item.ItemName.Length * 1.0) / 35);
                        }

                        currRow.Cells[0].Text = i.ToString();
                        currRow.Cells[1].Text = item.ItemName;
                        currRow.Cells[2].Text = item.Count;
                        currRow.Cells[3].Text = item.UnitPrice;
                        currRow.Cells[4].Text = item.Discount;
                        currRow.Cells[5].Text = item.Total;

                        if (!Data.OrderList.Last().Equals(item))
                        {
                            tblDetails.InsertRowBelow(currRow);
                            i++;
                            currRow = tblDetails.Rows[i];
                            
                            currRow.Cells[0].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[1].TextAlignment = TextAlignment.MiddleLeft;
                            currRow.Cells[1].Padding = templateRow.Cells[1].Padding;

                            currRow.Cells[2].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[3].TextAlignment = TextAlignment.MiddleRight;
                            currRow.Cells[3].Padding = templateRow.Cells[3].Padding;

                            currRow.Cells[4].TextAlignment = TextAlignment.MiddleCenter;

                            currRow.Cells[5].TextAlignment = TextAlignment.MiddleRight;
                            currRow.Cells[5].Padding = templateRow.Cells[5].Padding;
                        }
                    }
                }
            }
        }

        public bool ValidateForm()
        {
            return !string.IsNullOrEmpty(lblCompanyName.Text) &&
                !string.IsNullOrEmpty(lblCompanyAddress.Text) &&
                !string.IsNullOrEmpty(lblCompanyPhoneNo.Text) &&
                !string.IsNullOrEmpty(lblOrderNo.Text);
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
