using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models.Fabrika;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace Fusion.Controllers
{
    public class TSPController : Controller
    {
        // GET: TSP
        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ActionResult Index()
        {
            TSPModels model = new TSPModels();
            model.GetList();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ActionResult Add()
        {
            TSPModels model = new TSPModels();
            model.Initialize(User.Identity.Name.ToLower());
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ActionResult Edit(int id)
        {
            TSPModels model = new TSPModels(id, User.Identity.Name.ToLower());
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, TSPAdmin")]
        public ActionResult Nomenclature()
        {
            TSPModels model = new TSPModels();
            model.GetNomenclature();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, TSPAdmin")]
        public ContentResult SaveNomenclature(string data)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "json";

            try
            {
                TSPModels model = new TSPModels();
                model.SaveNomenclature(data);
                cr.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
            }
            catch (Exception ex)
            {
                cr.Content = @"{ ""result"": ""error"",""message"": """ + ex.Message + @""" }";
            }

            return cr;
        }

        [HttpPost]
        [MyAuthorize(Roles = "FusionAdmin, TSP")]
        public ContentResult Save(string data)
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "json";

            try
            {
                TSPModels model = new TSPModels();
                model.Save(data, User.Identity.Name);

                cr.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
            }
            catch (Exception ex)
            {
                cr.Content = @"{ ""result"": ""error"",""message"": """ + ex.Message + @""" }";
            }
            
            return cr;
        }

        [HttpPost]
        public ActionResult ExportToExcel(string JSON)
        {
            Dictionary<string, object> orgs = new Dictionary<string, object>();

            var TSPRequestDT = new System.Data.DataTable("Заявка на ТСП");
            TSPRequestDT.Columns.Add("Уровень", typeof(string));
            TSPRequestDT.Columns.Add("Наименование", typeof(string));
            TSPRequestDT.Columns.Add("Ед. изм.", typeof(string));
            var numberFormatInfo = new System.Globalization.CultureInfo("en-Us", false).NumberFormat;
            numberFormatInfo.NumberDecimalSeparator = ".";

            var serializer = new JavaScriptSerializer();
            var heapdata = serializer.DeserializeObject(JSON);

            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;

                object RequestID = null;
                r.TryGetValue("RequestID", out RequestID);
                object ProductId = null;
                r.TryGetValue("ProductId", out ProductId);

                int reqId = Convert.ToInt32(RequestID);

                foreach (var el in r)
                {
                    if (el.Key.StartsWith("_r"))
                    {
                        TSPRequestDT.Columns.Add("Наименование", typeof(string));

                        if (el.Value.ToString() != "")
                        { }
                    }
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(TSPRequestDT);

            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=Заявка.xls");

            Response.Charset = string.Empty;
            System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
            myTextWriter = ExportToExcelXML(ds);
            Response.Write(myTextWriter.ToString());
            Response.End();

            return View("MyView");
        }

        public System.IO.StringWriter ExportToExcelXML(DataSet source)
        {
            System.IO.StringWriter excelDoc;
            excelDoc = new System.IO.StringWriter();
            StringBuilder ExcelXML = new StringBuilder();

            ExcelXML.Append("<xml version>\r\n<Workbook ");
            ExcelXML.Append("xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");
            ExcelXML.Append("xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n ");
            ExcelXML.Append("xmlns:x=\"urn:schemas- microsoft-com:office:");
            ExcelXML.Append("excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:");
            ExcelXML.Append("office:spreadsheet\">\r\n ");

            #region Styles
            ExcelXML.Append(@"<Styles><Style ss:ID=""Default"" ss:Name=""Normal"">
<Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss""/>
<Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
</Style>\r\n");

            ExcelXML.Append(@"<Style ss:ID=""Plan"">");
            ExcelXML.Append(@"<Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>");
            ExcelXML.Append(@"<Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""/>");
            ExcelXML.Append(@"<NumberFormat ss:Format=""Standard""/>");
            ExcelXML.Append(@"</Style>");

            ExcelXML.Append(@"<Style ss:ID=""NonPlan"">");
            ExcelXML.Append(@"<Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""/>
   <Interior ss:Color=""#DDDDDD"" ss:Pattern=""Solid""/>");
            ExcelXML.Append(@"<NumberFormat ss:Format=""Standard""/>");
            ExcelXML.Append(@"</Style>");

            ExcelXML.Append(@"<Style ss:ID=""SubHeaderRegular"">");
            ExcelXML.Append(@"<Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""
    ss:Bold=""1""/>
   <Interior ss:Color=""#B7DEE8"" ss:Pattern=""Solid""/>");
            ExcelXML.Append(@"</Style>");

            ExcelXML.Append(@"<Style ss:ID=""ThirdLevel"">");
            ExcelXML.Append(@"<Alignment ss:Horizontal=""Right"" ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""
    ss:Italic=""1""/>");
            ExcelXML.Append(@"</Style>");

            ExcelXML.Append(@"<Style ss:ID=""SubHeaderNumber"">");
            ExcelXML.Append(@"<Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""
    ss:Bold=""1""/>
   <Interior ss:Color=""#B7DEE8"" ss:Pattern=""Solid""/>");
            ExcelXML.Append(@"<NumberFormat ss:Format=""Standard""/>");
            ExcelXML.Append(@"</Style>");
            ExcelXML.Append(@"<Style ss:ID=""Itog"">
   <Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""/>
   <Interior ss:Color=""#DDEBF7"" ss:Pattern=""Solid""/>
   <NumberFormat ss:Format=""Standard""/>
  </Style>");
            ExcelXML.Append(@"<Style ss:ID=""NormPrc"">
   <Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" ss:Color=""#000000"" ss:Bold=""1""/>
   <Interior ss:Color=""#FFFFFF"" ss:Pattern=""Solid""/>
   <NumberFormat ss:Format=""Percent""/>
  </Style>");

            ExcelXML.Append(@"<Style ss:ID=""NonPlanPrc"">
<Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""/>
   <Interior ss:Color=""#DDDDDD"" ss:Pattern=""Solid""/>
<NumberFormat ss:Format=""Percent""/>
</Style>");

            ExcelXML.Append(@"<Style ss:ID=""SubHeaderPrc"">
<Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""
     ss:Color=""#000000""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:CharSet=""204"" x:Family=""Swiss"" ss:Color=""#000000""
    ss:Bold=""1""/>
   <Interior ss:Color=""#B7DEE8"" ss:Pattern=""Solid""/>
<NumberFormat ss:Format=""Percent""/>
</Style>");

            ExcelXML.Append(@"</Styles>");
            #endregion

            string startExcelXML = ExcelXML.ToString();
            const string endExcelXML = "</Workbook>";
            excelDoc.Write(startExcelXML);

            #region PIURUB
            DataTable dt = source.Tables[0];
            excelDoc.Write("<Worksheet ss:Name=\"" + dt.TableName + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>\r\n");

            for (int x = 0; x < dt.Columns.Count; x++)
            {
                excelDoc.Write("<Cell>\r\n<Data ss:Type=\"String\">");
                excelDoc.Write(dt.Columns[x].ColumnName);
                excelDoc.Write("</Data>\r\n</Cell>");
            }

            excelDoc.Write("</Row>\r\n");

            foreach (DataRow x in dt.Rows)
            {
                excelDoc.Write("<Row>\r\n");

                string styleID = "Default";

                for (int y = 0; y < dt.Columns.Count; y++)
                {
                    if (dt.Columns[y].ColumnName.Contains("план"))
                        styleID = "Plan";
                    else
                    {
                        if (dt.Columns[y].ColumnName.Contains("факт") || dt.Columns[y].ColumnName.Contains("откл"))
                            styleID = "NonPlan";
                    }

                    if (dt.Columns[y].ColumnName.Contains("Итого"))
                        styleID = "Itog";

                    if (x[0].ToString().Trim() == "1")
                        styleID = "SubHeaderRegular";

                    if (x[0].ToString().Trim() == "1" && !dt.Columns[y].ColumnName.Contains("Наименование") && !dt.Columns[y].ColumnName.Contains("Уровень"))
                        styleID = "SubHeaderNumber";

                    if (x[0].ToString().Trim() == "3" && dt.Columns[y].ColumnName.Contains("Наименование"))
                        styleID = "ThirdLevel";

                    string XMLstring = x[y].ToString();

                    XMLstring = XMLstring.Trim();

                    excelDoc.Write("<Cell ss:StyleID=\"" + styleID + "\">");

                    if (dt.Columns[y].ColumnName == "Наименование")
                        excelDoc.Write("<Data ss:Type=\"String\">");
                    else
                        excelDoc.Write("<Data ss:Type=\"Number\">");

                    excelDoc.Write(XMLstring);
                    excelDoc.Write("</Data></Cell>");
                }

                excelDoc.Write("</Row>\r\n");
            }

            excelDoc.Write("</Table>");
            excelDoc.Write("</Worksheet>");
            #endregion

            #region PIUPRC
            dt = source.Tables[1];
            excelDoc.Write("<Worksheet ss:Name=\"" + dt.TableName + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row>\r\n");

            for (int x = 0; x < dt.Columns.Count; x++)
            {
                excelDoc.Write("<Cell>\r\n<Data ss:Type=\"String\">");
                excelDoc.Write(dt.Columns[x].ColumnName);
                excelDoc.Write("</Data>\r\n</Cell>");
            }

            excelDoc.Write("</Row>\r\n");

            foreach (DataRow x in dt.Rows)
            {
                excelDoc.Write("<Row>\r\n");

                string styleID = "Default";

                for (int y = 0; y < dt.Columns.Count; y++)
                {
                    if (dt.Columns[y].ColumnName.Contains("Норма"))
                        styleID = "NormPrc";
                    else
                    {
                        if (dt.Columns[y].ColumnName.Contains("Факт") || dt.Columns[y].ColumnName.Contains("Откл."))
                            styleID = "NonPlanPrc";
                    }

                    if (x[0].ToString().Trim() == "1")
                        styleID = "SubHeaderRegular";

                    if (x[0].ToString().Trim() == "1" && !dt.Columns[y].ColumnName.Contains("Наименование") && !dt.Columns[y].ColumnName.Contains("Уровень"))
                        styleID = "SubHeaderPrc";

                    if (x[0].ToString().Trim() == "3" && dt.Columns[y].ColumnName.Contains("Наименование"))
                        styleID = "ThirdLevel";

                    string XMLstring = x[y].ToString();

                    XMLstring = XMLstring.Trim();

                    excelDoc.Write("<Cell ss:StyleID=\"" + styleID + "\">");

                    if (dt.Columns[y].ColumnName == "Наименование")
                        excelDoc.Write("<Data ss:Type=\"String\">");
                    else
                        excelDoc.Write("<Data ss:Type=\"Number\">");

                    excelDoc.Write(XMLstring);
                    excelDoc.Write("</Data></Cell>");
                }

                excelDoc.Write("</Row>\r\n");
            }

            excelDoc.Write("</Table>");
            excelDoc.Write("</Worksheet>");
            #endregion

            excelDoc.Write(endExcelXML);

            return excelDoc;
        }
    }
}