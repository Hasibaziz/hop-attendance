using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Text;
using OfficeOpenXml.Drawing;
using OfficeOpenXml;

namespace ERP.Utility
{
    public class Excelimport
    {
        public sealed class ExcelFileResult : FileResult
        {
            private DataTable dt;
            private TableStyle tableStyle;
            private TableItemStyle headerStyle;
            private TableItemStyle itemStyle;
            //private ExcelPackage EXPackage;
           

            /// <summary>
            ///Z.Bsp. "Export date: {0}" (default initialization) - if empty string, export date
            /// not specified.
            /// </summary>
            public string TitleExportDate { get; set; }
            /// <summary>
            /// Title of the export is displayed in the upper left sheet
            /// </summary>
            public string Title { get; set; }

            ///<summary>
            ///Footer of the Export Excel
            ///</summary>
            public string Footer { get; set; }

             
            /// <summary>
            /// Konstruktor
            /// </summary>
            /// <param name="dt">To export DataTable</param>
            public ExcelFileResult(DataTable dt)
                : this(dt, null, null, null)
            { }

            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="dt">To export DataTable</param>
            /// <param name="tableStyle">Styling for entire table</param>
            /// <param name="headerStyle">Styling for header</param>
            /// <param name="itemStyle">Styling for the individual cells</param>
            public ExcelFileResult(DataTable dt, TableStyle tableStyle, TableItemStyle headerStyle, TableItemStyle itemStyle)
                : base("application/ms-excel")
            {
                this.dt = dt;
                Title = "Attendance Report ";
                Footer = "Powered By: Hasib, IT Department";
                TitleExportDate = "Export Date: {0}";
                this.tableStyle = tableStyle;
                this.headerStyle = headerStyle;               
                this.itemStyle = itemStyle;
                ExcelPackage EXPackage = new ExcelPackage();
                

                // provide defaults
                if (this.tableStyle == null)
                {
                    this.tableStyle = new TableStyle();
                    this.tableStyle.BorderStyle = BorderStyle.Solid;
                    this.tableStyle.BorderColor = Color.Black;
                    this.tableStyle.BorderWidth = Unit.Parse("2px");                    
                    //this.tableStyle.BackColor = Color.LightGray;
                    this.tableStyle.BackColor = Color.Azure;                  
                    //this.tableStyle.BackImageUrl = Path.GetFullPath("D:/HOP/BOK.jpg");
                    //exPackage.Workbook.Properties.Author = "Hasib";
                    //exPackage.Workbook.Properties.Comments = "HopLunIT";
                    //exPackage.Workbook.Properties.Title = "HopLun (Bangladesh) Ltd. Reports";                    
                }
                if (this.headerStyle == null)
                {
                    this.headerStyle = new TableItemStyle();
                    this.headerStyle.BackColor = Color.LightGray;
                }
            }

            private int Pixel2MTU(int p)
            {
                throw new NotImplementedException();
            }


            protected override void WriteFile(HttpResponseBase response)
            {
                // Create HtmlTextWriter
                StringWriter sw = new StringWriter();
                HtmlTextWriter tw = new HtmlTextWriter(sw);               

                // Build HTML Table from Items
                if (tableStyle != null)
                    tableStyle.AddAttributesToRender(tw);
                tw.RenderBeginTag(HtmlTextWriterTag.Table);

                // Create Title Row
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                tw.AddAttribute(HtmlTextWriterAttribute.Colspan, (dt.Columns.Count - 2).ToString());
                tw.RenderBeginTag(HtmlTextWriterTag.Td);                
                tw.Write(Title);
                tw.RenderEndTag();
                tw.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                tw.RenderBeginTag(HtmlTextWriterTag.Td);
                if (TitleExportDate != string.Empty)
                    tw.WriteLineNoTabs(string.Format(TitleExportDate, DateTime.Now.ToString("dd.MM.yyyy")));
                tw.RenderEndTag();

                

                // Create Header Row
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                DataColumn col = null;
                for (Int32 i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    col = dt.Columns[i];
                    if (headerStyle != null)
                        headerStyle.AddAttributesToRender(tw);
                    tw.RenderBeginTag(HtmlTextWriterTag.Th);
                    tw.RenderBeginTag(HtmlTextWriterTag.Strong);
                    tw.WriteLineNoTabs(col.ColumnName);
                    tw.RenderEndTag();
                    tw.RenderEndTag();
                }
                tw.RenderEndTag();
                

                // Create Data Rows
                foreach (DataRow row in dt.Rows)
                {
                    tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                    for (Int32 i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        if (itemStyle != null)
                            itemStyle.AddAttributesToRender(tw);                        
                        tw.RenderBeginTag(HtmlTextWriterTag.Td);
                        tw.WriteLineNoTabs(HttpUtility.HtmlEncode(row[i]));
                        tw.RenderEndTag();
                    }
                    tw.RenderEndTag(); //  /tr
                }
                tw.RenderEndTag(); //  /table


                ///<summary>
                ///The Footer Parts of the Excel Reports
                ///<!summary>
                tw.RenderBeginTag(HtmlTextWriterTag.Tr);
                tw.AddAttribute(HtmlTextWriterAttribute.Colspan, (dt.Columns.Count - 2).ToString());
                tw.RenderBeginTag(HtmlTextWriterTag.Td);
                //tw.WriteLineNoTabs(Footer);
                tw.RenderEndTag();
                tw.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
                tw.RenderBeginTag(HtmlTextWriterTag.Td);
                if (TitleExportDate != string.Empty)
                    tw.WriteLineNoTabs(Footer);
                tw.RenderEndTag();
                              
                               

                // Write result to output-stream
                Stream outputStream = response.OutputStream;
                byte[] byteArray = Encoding.Default.GetBytes(sw.ToString());
                response.OutputStream.Write(byteArray, 0, byteArray.GetLength(0));
            }
        }
    }
}