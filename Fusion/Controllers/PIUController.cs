using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fusion.Models;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web.UI;

namespace Fusion.Controllers
{
    public class PIUController : Controller
    {
        // GET: PIU
        public ActionResult Index()
        {
            PIU model = new PIU();
            model.userName = User.Identity.Name;
            model.StartDate = new DateTime(DateTime.Today.AddMonths(-3).Year, DateTime.Today.AddMonths(-3).Month, 1).ToString("dd.MM.yyyy");
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            return View(model);
        }

        public ActionResult GetTest()
        {
            PIU model = new PIU();
            model.GetTest();
            return View(model);
        }

        public ActionResult Get(string organization, string startDate, string endDate)
        {
            PIU model = new PIU();
            model.Organization = organization;
            model.StartDate = startDate;
            model.EndDate = endDate;
            model.Get(User.Identity.Name);
            return View("Edit", model);
        }

        [HttpPost]
        public ContentResult Save(string data)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();

            try
            {
                if (model.Save(data, User.Identity.Name))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка: " + ex.Message + @""" }";
            }

            return result;
        }

        [HttpPost]
        public ActionResult ExportToExcel(string data)
        {
            var list = new System.Data.DataTable("teste");
            list.Columns.Add("Уровень", typeof(string));
            list.Columns.Add("Наименование", typeof(string));
            var numberFormatInfo = new System.Globalization.CultureInfo("en-Us", false).NumberFormat;
            numberFormatInfo.NumberGroupSeparator = " ";
            numberFormatInfo.NumberDecimalSeparator = ",";

            var serializer = new JavaScriptSerializer();
            var heapdata = serializer.DeserializeObject(data);

            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;
                var t = r.FirstOrDefault(p => p.Key == "id" && p.Value.ToString() == "01.000.000");

                if (t.Key != null && t.Value != null)
                {
                    var c = r.Where(p => p.Key.StartsWith("sumplan"));

                    foreach (var v in c.ToList())
                    {
                        object period = null;
                        period = v.Key.Split('_')[1].ToString();
                        period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                        object sumplan = null;
                        sumplan = v.Value;

                        if (String.IsNullOrEmpty(sumplan.ToString()) || sumplan.ToString().ToLower() == "infinity")
                            sumplan = "0";

                        list.Columns.Add(DateTime.Parse(period.ToString()).ToString("MMMM yy") + " план", typeof(string));
                        list.Columns.Add(DateTime.Parse(period.ToString()).ToString("MMMM yy") + " факт", typeof(string));
                        list.Columns.Add(DateTime.Parse(period.ToString()).ToString("MMMM yy") + " откл.", typeof(string));
                    }

                    break;
                }
            }

            list.Columns.Add("Итого план", typeof(string));
            list.Columns.Add("Итого факт", typeof(string));
            list.Columns.Add("Итого откл.", typeof(string));

            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;

                object id = null;
                r.TryGetValue("id", out id);
                object parent = null;
                r.TryGetValue("parent", out parent);
                object level = null;
                r.TryGetValue("level", out level);
                object code = null;
                r.TryGetValue("code", out code);
                object name = null;
                r.TryGetValue("name", out name);
                object organization = null;
                r.TryGetValue("organization", out organization);
                object itogPlan = null;
                r.TryGetValue("itogPlan", out itogPlan);
                object itogFact = null;
                r.TryGetValue("itogFact", out itogFact);
                object itogDiv = null;
                r.TryGetValue("itogDiv", out itogDiv);

                System.Data.DataRow row;
                row = list.NewRow();
                row["Уровень"] = level.ToString();
                row["Наименование"] = name.ToString();
                var sp = r.Where(p => p.Key.StartsWith("sumplan_"));
                var sf = r.Where(p => p.Key.StartsWith("sumfact_"));
                var dv = r.Where(p => p.Key.StartsWith("div_"));

                if (itogPlan == null || String.IsNullOrEmpty(itogPlan.ToString()) || itogPlan.ToString().ToLower() == "infinity")
                    itogPlan = "0";

                if (itogFact == null || String.IsNullOrEmpty(itogFact.ToString()) || itogFact.ToString().ToLower() == "infinity")
                    itogFact = "0";

                if (itogDiv == null || String.IsNullOrEmpty(itogDiv.ToString()) || itogDiv.ToString().ToLower() == "infinity")
                    itogDiv = "0";

                Decimal itogPlanD = 0;
                Decimal itogFactD = 0;
                Decimal itogDivD = 0;

                Decimal.TryParse(itogPlan.ToString(), out itogPlanD);
                Decimal.TryParse(itogFact.ToString(), out itogFactD);
                Decimal.TryParse(itogDiv.ToString(), out itogDivD);

                row["Итого план"] = itogPlanD.ToString("N", numberFormatInfo); ;
                row["Итого факт"] = itogFactD.ToString("N", numberFormatInfo); ;
                row["Итого откл."] = itogDivD.ToString("N", numberFormatInfo); ;

                foreach (var c in sp)
                {
                    Decimal val = 0;
                    object period = null;
                    period = c.Key.Split('_')[1].ToString();
                    period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                    object valObj = null;
                    valObj = c.Value;

                    if (String.IsNullOrEmpty(valObj.ToString()) || valObj.ToString().ToLower() == "infinity")
                        valObj = "0";

                    Decimal.TryParse(valObj.ToString(), out val);

                    //list.Rows.Add(Subdiv, position, FullName, Rate, Hours, totalaccsum.ToString("N", numberFormatInfo), totaldetsum.ToString("N", numberFormatInfo), aadsum.ToString("N", numberFormatInfo), "");
                    row[DateTime.Parse(period.ToString()).ToString("MMMM yy") + " план"] = val.ToString("N", numberFormatInfo);
                }

                foreach (var c in sf)
                {
                    Decimal val = 0;
                    object period = null;
                    period = c.Key.Split('_')[1].ToString();
                    period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                    object valObj = null;
                    valObj = c.Value;

                    if (String.IsNullOrEmpty(valObj.ToString()) || valObj.ToString().ToLower() == "infinity")
                        valObj = "0";

                    Decimal.TryParse(valObj.ToString(), out val);

                    row[DateTime.Parse(period.ToString()).ToString("MMMM yy") + " факт"] = val.ToString("N", numberFormatInfo); ;
                }

                foreach (var c in dv)
                {
                    Decimal val = 0;
                    object period = null;
                    period = c.Key.Split('_')[1].ToString();
                    period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                    object valObj = null;
                    valObj = c.Value;

                    if (String.IsNullOrEmpty(valObj.ToString()) || valObj.ToString().ToLower() == "infinity")
                        valObj = "0";

                    Decimal.TryParse(valObj.ToString(), out val);

                    row[DateTime.Parse(period.ToString()).ToString("MMMM yy") + " откл."] = val.ToString("N", numberFormatInfo); ;
                }

                list.Rows.Add(row);
            }

            var grid = new GridView();
            grid.DataSource = list;
            grid.DataBind();

            for (int i = 0; i < list.Columns.Count; i++)
            {
                if (list.Columns[i].ColumnName.Contains("факт") || list.Columns[i].ColumnName.Contains("откл."))
                    for (int j = 0; j < grid.Rows.Count; j++)
                    {
                        grid.Rows[j].Cells[i].BackColor = Color.FromArgb(221, 221, 221);
                    }

                if (list.Columns[i].ColumnName.Contains("Итого план") || list.Columns[i].ColumnName.Contains("Итого факт") || list.Columns[i].ColumnName.Contains("Итого откл."))
                    for (int j = 0; j < grid.Rows.Count; j++)
                    {
                        grid.Rows[j].Cells[i].BackColor = Color.FromArgb(221, 235, 247);
                    }
            }

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].Cells[0].Text.Trim() == "1")
                {
                    for (int j = 0; j < grid.Rows[i].Cells.Count; j++)
                    {
                        grid.Rows[i].Cells[j].BackColor = Color.FromArgb(183, 222, 232);
                        grid.Rows[i].Cells[j].Font.Bold = true;
                    }
                }

                if (grid.Rows[i].Cells[0].Text.Trim() == "3")
                {
                    for (int j = 0; j < grid.Rows[i].Cells.Count; j++)
                    {
                        grid.Rows[i].Cells[j].Font.Italic = true;
                        grid.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ПИУ_.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }

        public ActionResult Norms()
        {
            PIU model = new PIU();
            model.Organization = "Токио";
            model.GetNorms(User.Identity.Name, new DateTime(2017, 1, 1));
            return View(model);
        }

        [HttpPost]
        public ContentResult SaveNorms(string data, string date)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();

            try
            {
                if (model.SaveNorms(data, User.Identity.Name, date))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка: " + ex.Message + @""" }";
            }

            return result;
        }
    }
}