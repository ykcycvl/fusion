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
using System.Data;
using System.Text;

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

        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, RDR")]
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
        public ActionResult EditPlan(string organization, byte Period, int Year)
        {
            if (String.IsNullOrEmpty(organization))
                return RedirectToAction("Index");

            PIU model = new PIU();
            model.Organization = organization;
            model.Year = Year;
            model.Period = Period;
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
        public ContentResult Save(string data, string organization, bool? toConfirm, byte Period, int Year)
        {
            if (toConfirm == null)
                toConfirm = false;

            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();
            model.Year = Year;
            model.Period = Period;

            try
            {
                if (model.Save(data, User.Identity.Name, (bool)toConfirm,  organization))
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
        public ContentResult Confirm(string data, string organization, byte Period, int Year)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();
            model.Period = Period;
            model.Year = Year;

            try
            {
                if (model.Save(data, User.Identity.Name, false, organization))
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
        public ActionResult Norms()
        {
            PIU model = new PIU() { UserName = User.Identity.Name };
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        public ActionResult EditNorms(string Organization, string StartDate)
        {
            PIU model = new PIU() { UserName = User.Identity.Name };
            model.Organization = Organization;
            model.StartDate = StartDate;
            model.GetNorms(User.Identity.Name, Organization);
            return View(model);
        }
        [MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        [HttpPost]
        public ContentResult SaveNorms(string data, string date, string Organization)
        {
            ContentResult result = new ContentResult();
            result.ContentType = "json";

            PIU model = new PIU();

            try
            {
                if (model.SaveNorms(data, User.Identity.Name, date, Organization))
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

        //[MyAuthorize(Roles = "FusionAdmin, BB, BS, DD, DM, DSK, FD, GD, HRD, ITD, NZ, OD, SP, TD, UPR, RDR")]
        [HttpPost]
        public ActionResult ExportToExcelConsolidated(string tableDataRUB, string date)
        {
            Dictionary<string, object> orgs = new Dictionary<string, object>();

            var rubDT = new System.Data.DataTable("ПиУ(руб)");
            var prcDT = new System.Data.DataTable("ПиУ(%)");
            rubDT.Columns.Add("Уровень", typeof(string));
            rubDT.Columns.Add("Наименование", typeof(string));
            prcDT.Columns.Add("Уровень", typeof(string));
            prcDT.Columns.Add("Наименование", typeof(string));
            var numberFormatInfo = new System.Globalization.CultureInfo("en-Us", false).NumberFormat;
            numberFormatInfo.NumberDecimalSeparator = ".";

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var heapdata = serializer.DeserializeObject(tableDataRUB);

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

                        rubDT.Columns.Add(orgName.ToString() + " план", typeof(string));
                        rubDT.Columns.Add(orgName.ToString() + " факт", typeof(string));
                        rubDT.Columns.Add(orgName.ToString() + " откл.", typeof(string));

                        prcDT.Columns.Add("Норма " + orgName.ToString(), typeof(string));
                        prcDT.Columns.Add("Факт " + orgName.ToString(), typeof(string));
                        prcDT.Columns.Add("Откл. " + orgName.ToString(), typeof(string));

                        orgs.Add(orgCode.ToString(), orgName);
                    }

                    break;
                }
            }

            rubDT.Columns.Add("Итого план", typeof(string));
            rubDT.Columns.Add("Итого факт", typeof(string));
            rubDT.Columns.Add("Итого откл.", typeof(string));

            prcDT.Columns.Add("Ср. факт", typeof(string));
            prcDT.Columns.Add("Ср. откл.", typeof(string));

            Dictionary<string, Decimal> MainSums = new Dictionary<string, decimal>();

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
                object prcNorm = null;
                r.TryGetValue("prcNorm", out prcNorm);

                System.Data.DataRow row;
                row = rubDT.NewRow();

                System.Data.DataRow rowPrc;
                rowPrc = prcDT.NewRow();

                row["Уровень"] = level.ToString();
                row["Наименование"] = name.ToString();
                rowPrc["Уровень"] = level.ToString();
                rowPrc["Наименование"] = name.ToString();
                var sp = r.Where(p => p.Key.StartsWith("sumplan_"));
                var sf = r.Where(p => p.Key.StartsWith("sumfact_"));
                var dv = r.Where(p => p.Key.StartsWith("div_"));

                if (itogPlan == null || String.IsNullOrEmpty(itogPlan.ToString()) || itogPlan.ToString().ToLower() == "infinity")
                    itogPlan = "0";

                if (itogFact == null || String.IsNullOrEmpty(itogFact.ToString()) || itogFact.ToString().ToLower() == "infinity")
                    itogFact = "0";

                if (itogDiv == null || String.IsNullOrEmpty(itogDiv.ToString()) || itogDiv.ToString().ToLower() == "infinity")
                    itogDiv = "0";

                if (prcNorm == null || String.IsNullOrEmpty(prcNorm.ToString()) || prcNorm.ToString().ToLower() == "infinity")
                    prcNorm = "0";

                Decimal itogPlanD = 0;
                Decimal itogFactD = 0;
                Decimal itogDivD = 0;

                Decimal.TryParse(itogPlan.ToString(), out itogPlanD);
                Decimal.TryParse(itogFact.ToString(), out itogFactD);
                Decimal.TryParse(itogDiv.ToString(), out itogDivD);

                Decimal itogDivPrcD = 0;

                row["Итого план"] = itogPlanD;
                row["Итого факт"] = itogFactD;
                row["Итого откл."] = itogDivD;

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

                Decimal prcFactAvg = 0;
                int cnt = 0;

                Decimal prcNrm = 0;
                Decimal.TryParse(prcNorm.ToString(), out prcNrm);

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

                    if (id.ToString() == "01.000.000")
                        MainSums.Add(orgCode.ToString(), val);

                    Decimal prc = 0;

                    if (MainSums[orgCode.ToString()] != 0)
                        prc = (val / MainSums[orgCode.ToString()]);

                    rowPrc["Норма " + o.Value.ToString()] = prcNrm.ToString("N", numberFormatInfo);
                    rowPrc["Факт " + o.Value.ToString()] = prc.ToString("N", numberFormatInfo);
                    rowPrc["Откл. " + o.Value.ToString()] = (prcNrm - prc).ToString("N", numberFormatInfo);
                    cnt++;
                    prcFactAvg += prc;
                }

                if (cnt == 0)
                    cnt = 1;

                prcFactAvg = prcFactAvg / cnt;

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

                rowPrc["Ср. факт"] = prcFactAvg;
                rowPrc["Ср. откл."] = prcNrm - prcFactAvg;

                rubDT.Rows.Add(row);
                prcDT.Rows.Add(rowPrc);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(rubDT);
            ds.Tables.Add(prcDT);

            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=СводныйОтчет_" + date + ".xls");

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