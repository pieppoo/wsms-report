using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace wsms.report
{
    public class ReportCore
    {
        public delegate void PrintComplete(object o, PrintEventArgs e);
        public event PrintComplete OnPrintingReceiptComplete;

        private XtraReport _report;


        public ReportCore()
        {
        }

        //public ReportCore(MemoSerahanItem data)
        //{
        //    _report = new MemoSerahan();
        //    (_report as MemoSerahan).PopulateData(data);
        //    _report.PrintingSystem.Document.Name = "MemoSerahan_" + DateTime.Now.ToFileTimeUtc();
        //}

        //public ReportCore(ReceiptType type, ReceiptItem data)
        //{
        //    if (type == ReceiptType.OriginalReceipt)
        //    {
        //        _report = new OriginalReceipt();
        //        (_report as OriginalReceipt).PopulateData(data);
        //    }
        //    else
        //    {
        //        _report = new Receipt();
        //        var receipt = _report as Receipt;

        //        if (receipt != null)
        //        {
        //            receipt.ReceiptType = type;
        //            receipt.PopulateData(data);
        //        }

        //    }

        //    _report.PrintingSystem.Document.Name = data.ReceiptNo;
        //}

        public void PrintReceiptDialog()
        {
            if (_report != null)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(_report))
                {
                    // Invoke the Print dialog.
                    printTool.PrintDialog();
                    printTool.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
                }
            }
            else
            {
                throw new NullReferenceException("Receipt object hasn't initialized");
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
            if (_report != null)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(_report))
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
