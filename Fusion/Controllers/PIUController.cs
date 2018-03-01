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
        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult Budgeting()
        {
            PIU model = new PIU();
            model.UserName = User.Identity.Name;
            model.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult View()
        {
            PIU model = new PIU();
            model.UserName = User.Identity.Name;
            model.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD")]
        public ActionResult Report()
        {
            PIU model = new PIU();
            model.UserName = User.Identity.Name;
            model.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            model.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).ToString("dd.MM.yyyy");
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult GetTest()
        {
            PIU model = new PIU();
            //model.GetConsolidatedReport();

            /*ContentResult cr = new ContentResult();
            cr.ContentType = "application/json";
            cr.ContentEncoding = System.Text.Encoding.UTF8;
            cr.Content = data;*/
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult EditPlan(string organization, byte quarter)
        {
            if (String.IsNullOrEmpty(organization))
                return RedirectToAction("Index");

            PIU model = new PIU();
            model.Organization = organization;
            model.Quarter = quarter;
            model.UserName = User.Identity.Name;
            model.ToConfirm = true;
            model.GetPlan();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult ViewPlan(string organization, string startDate, string endDate)
        {
            if (String.IsNullOrEmpty(organization))
                return RedirectToAction("Index");

            PIU model = new PIU();
            model.Organization = organization;
            model.UserName = User.Identity.Name;

            DateTime dt = DateTime.Parse(startDate);

            model.StartDate = startDate;
            model.EndDate = endDate;
            model.Get();
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        [HttpPost]
        public ContentResult Save(string data, string organization, bool? toConfirm, byte Quarter)
        {
            if (toConfirm == null)
                toConfirm = false;

            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();
            model.Quarter = Quarter;

            try
            {
                if (model.Save(data, User.Identity.Name, (bool)toConfirm, false, organization, model.StartDate))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно сохранено"" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка сохранения"" }";
            }

            return result;
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        [HttpPost]
        public ContentResult Confirm(string data, string date, string organization)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();

            try
            {
                if (model.Save(data, User.Identity.Name, false, true, organization, date))
                    result.Content = @"{ ""result"": ""success"",""message"": ""Успешно согласовано"" }";
                else
                    result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка"" }";
            }
            catch (Exception ex)
            {
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка: " + ex.Message + @""" }";
            }

            return result;
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
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

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        [HttpPost]
        public ActionResult ExportToExcelConsolidated(string data, string date)
        {
            Dictionary<string, object> orgs = new Dictionary<string, object>();

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
                    var c = r.Where(p => p.Key.StartsWith("org_"));

                    foreach (var v in c.ToList())
                    {
                        object orgCode = null;
                        orgCode = v.Key.Split('_')[1].ToString();
                        object orgName = null;
                        orgName = v.Value;

                        list.Columns.Add(orgName.ToString() + " план", typeof(string));
                        list.Columns.Add(orgName.ToString() + " факт", typeof(string));
                        list.Columns.Add(orgName.ToString() + " откл.", typeof(string));
                        orgs.Add(orgCode.ToString(), orgName);
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
                    object orgCode = null;
                    orgCode = c.Key.Split('_')[1].ToString();
                    object valObj = null;
                    valObj = c.Value;

                    if (String.IsNullOrEmpty(valObj.ToString()) || valObj.ToString().ToLower() == "infinity")
                        valObj = "0";

                    var o = orgs.FirstOrDefault(p => p.Key == orgCode.ToString());
                    Decimal.TryParse(valObj.ToString(), out val);
                    row[o.Value.ToString() + " план"] = val.ToString("N", numberFormatInfo);
                }

                foreach (var c in sf)
                {
                    Decimal val = 0;
                    object orgCode = null;
                    orgCode = c.Key.Split('_')[1].ToString();
                    object valObj = null;
                    valObj = c.Value;

                    if (String.IsNullOrEmpty(valObj.ToString()) || valObj.ToString().ToLower() == "infinity")
                        valObj = "0";

                    var o = orgs.FirstOrDefault(p => p.Key == orgCode.ToString());
                    Decimal.TryParse(valObj.ToString(), out val);
                    row[o.Value.ToString() + " факт"] = val.ToString("N", numberFormatInfo);
                }

                foreach (var c in dv)
                {
                    Decimal val = 0;
                    object orgCode = null;
                    orgCode = c.Key.Split('_')[1].ToString();
                    object valObj = null;
                    valObj = c.Value;

                    if (String.IsNullOrEmpty(valObj.ToString()) || valObj.ToString().ToLower() == "infinity")
                        valObj = "0";

                    var o = orgs.FirstOrDefault(p => p.Key == orgCode.ToString());
                    Decimal.TryParse(valObj.ToString(), out val);
                    row[o.Value.ToString() + " откл."] = val.ToString("N", numberFormatInfo);
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
            Response.AddHeader("content-disposition", "attachment; filename=СводныйОтчет_" + date + ".xls");
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

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult Norms(string Organization, string Date)
        {
            PIU model = new PIU();
            model.Organization = "Токио";
            model.GetNorms(User.Identity.Name, Organization, DateTime.Parse(Date));
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
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
                result.Content = @"{ ""result"": ""error"",""message"": ""Ошибка сохранения!"" }";
            }

            return result;
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult CR(string startDate, string OrgPool)
        {
            DateTime myDate;

            if (!DateTime.TryParse(startDate, out myDate))
            {
                string s = "pizda kosyak";
            }

            PIU model = new PIU();
            model.StartDate = startDate;
            model.UserName = User.Identity.Name;
            model.GetConsolidatedReport(OrgPool);
            return View(model);
        }

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ContentResult GetByOrganization(string organization, string startDate, string endDate)
        {
            PIU model = new PIU();
            string data = "";

            ContentResult cr = new ContentResult();
            cr.ContentType = "application/json";
            cr.ContentEncoding = System.Text.Encoding.UTF8;
            cr.Content = data;
            return cr;
        }

        public ActionResult GetRoles()
        {
            PIU model = new PIU();
            model.GetRoles(User.Identity.Name, "Токио");
            return View();
        }
    }
}