using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Fusion.Models
{
    public class PIU
    {
        [Display(Name = "Начало периода")]
        public string StartDate { get; set; }
        [Display(Name = "Конец периода")]
        public string EndDate { get; set; }
        [Display(Name = "Подразделение")]
        public string Organization { get; set; }
        public string OrganizationCode { get; set; }
        public string UserName { get; set; }
        public string Data { get; set; }
        public string ColumnsRub { get; set; }
        public string ColumnsPrc { get; set; }
        public bool ToConfirm { get; set; }
        public bool Confirmation { get; set; }
        public bool Confirmed { get; set; }
        public bool CanConfirm { get; set; }
        private byte _quarter { get; set; }
        private int _year { get; set; }
        public string OrgPool { get; set; }
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                Period = _quarter;
            }
        }
        public byte Period
        {
            get
            {
                return _quarter;
            }
            set
            {
                _quarter = value;

                switch (value)
                {
                    case 0:
                        StartDate = new DateTime(_year, 1, 1).ToString("dd.MM.yyyy");
                        EndDate = new DateTime(_year, 12, 31).ToString("dd.MM.yyyy");
                        break;
                    case 1:
                        StartDate = new DateTime(_year, 1, 1).ToString("dd.MM.yyyy");
                        EndDate = new DateTime(_year, 3, 31).ToString("dd.MM.yyyy");
                        break;
                    case 2:
                        StartDate = new DateTime(_year, 4, 1).ToString("dd.MM.yyyy");
                        EndDate = new DateTime(_year, 6, 30).ToString("dd.MM.yyyy");
                        break;
                    case 3:
                        StartDate = new DateTime(_year, 7, 1).ToString("dd.MM.yyyy");
                        EndDate = new DateTime(_year, 9, 30).ToString("dd.MM.yyyy");
                        break;
                    case 4:
                        StartDate = new DateTime(_year, 10, 1).ToString("dd.MM.yyyy");
                        EndDate = new DateTime(_year, 12, 31).ToString("dd.MM.yyyy");
                        break;
                }
            }
        }

        public class PIUViewModel
        {
            //Наименование статьи
            public string Name { get; set; }
            public Decimal SumPlan { get; set; }
            public Decimal SumFact { get; set; }
            public Decimal SumNorm { get; set; }
            public string Level { get; set; }
            public List<PIUViewModel> ChildItems { get; set; }
        }

        public IEnumerable<SelectListItem> Periods
        {
            get
            {
                List<SelectListItem> q = new List<SelectListItem>();
                q.Add(new SelectListItem() { Text = "1 квартал", Value = "1" });
                q.Add(new SelectListItem() { Text = "2 квартал", Value = "2" });
                q.Add(new SelectListItem() { Text = "3 квартал", Value = "3" });
                q.Add(new SelectListItem() { Text = "4 квартал", Value = "4" });
                q.Add(new SelectListItem() { Text = "Год", Value = "0" });
                return q;
            }
        }
        public IEnumerable<SelectListItem> Years
        {
            get
            {
                int initYear = DateTime.Today.Year;
                List<SelectListItem> q = new List<SelectListItem>();

                for (int i = 0; i < 5; i++)
                {
                    q.Add(new SelectListItem() { Text = initYear.ToString(), Value = initYear.ToString() });
                    initYear++;
                }

                return q;
            }
        }

        public IEnumerable<SelectListItem> OrgPoolSelectList
        {
            get
            {
                List<SelectListItem> op = new List<SelectListItem>();
                op.Add(new SelectListItem() { Text = "Все рестораны", Value = "0" });
                op.Add(new SelectListItem() { Text = "Рестораны Токио", Value = "1" });
                return op;
            }
        }

        public IEnumerable<SelectListItem> OrganizationsSelectList
        {
            get
            {
                List<SelectListItem> Orgs = new List<SelectListItem>();

                using (PiuWS.fsn_PIU service = new PiuWS.fsn_PIU())
                {
                    service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                    var Organizations = service.GetOrganizations(UserName, "0").ToList();

                    foreach (PiuWS.Organization org in Organizations)
                        Orgs.Add(new SelectListItem() { Text = org.Name, Value = org.Name });
                }

                return Orgs;
            }
        }

        public void GetTest()
        {
            string org = "Токио";
            string date1 = "01.09.2017";
            string date2 = "30.11.2017";
            string userName = "tv";

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            PiuWS.PIU t = model.GetPIUData2(userName, org, DateTime.Parse(date1), DateTime.Parse(date2), "0");
            string s = "";

            foreach (var entry in t.Entries)
            {
                foreach (var a in entry.Articles)
                {
                    //s += ProcessArticle(a, userName);
                }
            }

            this.Data = s;
        }

        public void GetConsolidatedReport(string OrgPool)
        {
            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            model.Timeout = 600000;
            PiuWS.PIU t = model.GetPIUData2(UserName, "", DateTime.Parse(StartDate), DateTime.Parse(StartDate), OrgPool);

            string s = "";

            int i = 0;

            this.ColumnsRub = "{id: \"id\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"name\", header: \"Наименование\", width: 400 },\r\n";
            this.ColumnsRub += "{id: \"level\", hidden: \"true\" },\r\n";

            this.ColumnsPrc = "{id: \"id\", hidden: \"true\" },\r\n";
            this.ColumnsPrc += "{id: \"name\", header: \"Наименование\", width: 400 },\r\n";
            this.ColumnsPrc += "{id: \"maxDiv\", hidden: \"true\"},\r\n";

            string planItogFormula = "";
            string factItogFormula = "";

            foreach (var entry in t.Entries)
            {
                i++;
                entry.Organization.Code = entry.Organization.Code.Replace("-", "");

                this.ColumnsRub += "{ id: \"" + entry.Organization.Code + "\", hidden: \"true\" },\r\n";
                this.ColumnsRub += "{ id: \"sumplan_" + entry.Organization.Code + "\", header: \"" + entry.Organization.Name + "<br/>План\", css: \"planColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";
                this.ColumnsRub += "{ id: \"sumfact_" + entry.Organization.Code + "\", header: \"" + entry.Organization.Name + "<br/>Факт\", css: \"factColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";
                this.ColumnsRub += "{ id: \"div_" + entry.Organization.Code + "\", header: \"" + entry.Organization.Name + "<br/>Откл.\", css: \"divColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";

                this.ColumnsPrc += "{ id: \"sumplan_" + entry.Organization.Code + "\", hidden: \"true\", header: \"" + entry.Organization.Name + "<br/>план\", css: \"planColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), cssFormat:markRO },\r\n";
                this.ColumnsPrc += "{ id: \"div_" + entry.Organization.Code + "\", hidden: \"true\", header: \"Откл, %\", editor: '', css: \"divColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }) },\r\n";
                this.ColumnsPrc += "{ id: \"prcNorm\", header: \"Норма\", css: \"normColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }) },\r\n";
                this.ColumnsPrc += "{ id: \"prc_" + entry.Organization.Code + "\", header: \"Факт<br/>" + entry.Organization.Name + "\", css: \"factColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }), },\r\n";
                this.ColumnsPrc += "{ id: \"prcDiv_" + entry.Organization.Code + "\", header: \"Откл.<br/>" + entry.Organization.Name + "\", math:\"[$r, prcNorm] - [$r, prc_" + entry.Organization.Code + "]\", css: \"factColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }), cssFormat:markDiv },\r\n";

                s += "{" + String.Format("id:\"{0}\", org_{1}:\"{2}\"", "01.000.000", entry.Organization.Code, entry.Organization.Name) + "},\r\n";

                foreach (var a in entry.Articles)
                    s += ProcessArticleConsolidated(a, UserName, entry.Organization.Code);

                planItogFormula += " + [$r, sumplan_" + entry.Organization.Code + "]";
                factItogFormula += " + [$r, sumfact_" + entry.Organization.Code + "]";
            }

            this.ColumnsRub += "{id: \"itogPlan\", header: \"Итог<br/>План\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math: \"" + planItogFormula + "\"},";
            this.ColumnsRub += "{id: \"itogFact\", header: \"Итог<br/>Факт\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math: \"" + factItogFormula + "\"},";
            this.ColumnsRub += "{id: \"itogDiv\", header: \"Итог<br/>Откл.\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math:\"[$r, itogPlan] - [$r, itogFact]\" },";
            this.Data = s;
        }

        private string ProcessArticle(PiuWS.Article article, string userName, string parent)
        {
            if (!article.ToConfirm)
                ToConfirm = false;

            string articleData = "{" + String.Format("id:\"{0}\", name:\"{1}\", criticalDev:\"{2}\", parent:\"{3}\", toConfirm:\"{4}\"", article.Code, article.Name, article.SumNormMax, parent, article.ToConfirm) + "},\r\n";
            articleData += "{" + String.Format("id:\"{0}\", maxDiv:\"{1}\"", article.Code, article.SumNormMax) + "},\r\n";
            articleData += "{" + String.Format("id:\"{0}\", level:\"{1}\"", article.Code, article.Level) + "},\r\n";

            if (article.Allowed != null)
            {
                var a = article.Allowed.ToList().FirstOrDefault(p => p.NameCFR.ToLower() == userName.ToLower());

                if (a != null && article.ToConfirm)
                    Confirmation = true;

                if(a == null || !a.WriteRole || article.ToConfirm)
                    articleData += "{" + String.Format("id:\"{0}\", readonly:\"true\", allow:\"true\"", article.Code) + "},\r\n";
            }

            this.ColumnsRub = "{id: \"id\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"code\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"parent\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"organization\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"name\", header: \"Наименование\", width: 400 },\r\n";
            this.ColumnsRub += "{id: \"level:\", hidden: \"true\" },\r\n";

            this.ColumnsPrc = "{id: \"id\", hidden: \"true\" },\r\n";
            this.ColumnsPrc += "{id: \"parent\", hidden: \"true\" },\r\n";
            this.ColumnsPrc += "{id: \"code\", hidden: \"true\" },\r\n";
            this.ColumnsPrc += "{id: \"organization\", hidden: \"true\" },\r\n";
            this.ColumnsPrc += "{id: \"name\", header: \"Наименование\", width: 400 },\r\n";
            this.ColumnsPrc += "{id: \"maxDiv\", hidden: \"true\"},\r\n";

            string factItogFormula = "";
            string planItogFormula = "";

            foreach (var adata in article.DataForPeriod)
            {
                this.ColumnsRub += "{ id: \"sumplan_" + adata.Period.ToString("MMyy") + "\", header: \"" + adata.Period.ToString("MMM. yy") + "<br/>План\", editor: 'text', css: \"planColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";
                this.ColumnsRub += "{ id: \"sumfact_" + adata.Period.ToString("MMyy") + "\", header: \"" + adata.Period.ToString("MMM. yy") + "<br/>Факт\", css: \"factColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";
                this.ColumnsRub += "{ id: \"div_" + adata.Period.ToString("MMyy") + "\", header: \"" + adata.Period.ToString("MMM. yy") + "<br/>Откл.\", css: \"divColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";

                this.ColumnsPrc += "{ id: \"sumplan_" + adata.Period.ToString("MMyy") + "\", hidden: \"true\", header: \"" + adata.Period.ToString("MMM yy") + "<br/>план\", editor: 'text', css: \"planColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), cssFormat:markRO },\r\n";
                this.ColumnsPrc += "{ id: \"div_" + adata.Period.ToString("MMyy") + "\", hidden: \"true\", header: \"Откл, %\", editor: '', css: \"divColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }) },\r\n";
                this.ColumnsPrc += "{ id: \"prcNorm\", header: \"Норма\", css: \"normColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }) },\r\n";
                this.ColumnsPrc += "{ id: \"prc_" + adata.Period.ToString("MMyy") + "\", header: \"Факт<br/>" + adata.Period.ToString("MMM yy") + "\", editor: '', css: \"factColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }), },\r\n";
                this.ColumnsPrc += "{ id: \"prcDiv_" + adata.Period.ToString("MMyy") + "\", header: \"Откл.<br/>" + adata.Period.ToString("MMM yy") + "\", editor: '', math:\"[$r, prcNorm] - [$r, prc_" + adata.Period.ToString("MMyy") + "]\", css: \"factColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }), cssFormat:markDiv },\r\n";

                articleData += "{" + String.Format("id:\"{0}\", prcNorm:\"{1}\"", article.Code, adata.SumNorm) + "},\r\n";
                articleData += "{" + String.Format("id:\"{0}\", prc_{1}:\"=GetVal({2})\"", article.Code, adata.Period.ToString("MMyy"), "([" + article.Code + ", sumfact_" + adata.Period.ToString("MMyy") + "] / [01.000.000, sumfact_" + adata.Period.ToString("MMyy") + "]) * 100") + "},\r\n";

                // Формулы расчета статей, если есть.
                string formula = "";

                // Суммируемые ячейки
                if (article.Additional != null)
                    foreach (var add in article.Additional)
                        if (!String.IsNullOrEmpty(add.Code))
                            formula += " + [" + add.Code + ", sumplan_" + adata.Period.ToString("MMyy") + "]";

                // Вычитаемые ячейки
                if (article.Exception != null)
                    foreach (var div in article.Exception)
                        if (!String.IsNullOrEmpty(div.Code))
                            formula += " + [" + div.Code + ", sumplan_" + adata.Period.ToString("MMyy") + "]";

                string css = "";

                if (article.Level == 1)
                    css = "level1css";

                if (article.Level == 2)
                    css = "level2css";

                if (article.Level == 3)
                    css = "level3css";

                if (article.ToConfirm)
                    css += " toAllow";

                if (!String.IsNullOrEmpty(formula))
                    articleData += "{" + string.Format("id:\"{0}\", sumfact_{2}:\"{3}\", sumplan_{2}:\"= {4}\", div_{2}:\"=[{0}, sumplan_{2}] - [{0}, sumfact_{2}]\", $css:\"{5}\"", article.Code, article.Name, adata.Period.ToString("MMyy"), adata.SumFact, formula, css) + "},\r\n";
                else
                    articleData += "{" + string.Format("id:\"{0}\", sumfact_{2}:\"{3}\", sumplan_{2}:\"{4}\", div_{2}:\"=[{0}, sumplan_{2}] - [{0}, sumfact_{2}]\", $css:\"{5}\"", article.Code, article.Name, adata.Period.ToString("MMyy"), adata.SumFact, adata.SumPlan, css) + "},\r\n";

                factItogFormula += " + [$r, sumfact_" + adata.Period.ToString("MMyy") + "]";
                planItogFormula += " + [$r, sumplan_" + adata.Period.ToString("MMyy") + "]";
            }

            foreach (var child in article.Child)
                articleData += ProcessArticle(child, userName, article.Code);


            this.ColumnsRub += "{id: \"itogPlan\", header: \"Итог<br/>План\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math: \"" + planItogFormula + "\"},";
            this.ColumnsRub += "{id: \"itogFact\", header: \"Итог<br/>Факт\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math: \"" + factItogFormula + "\"},";
            this.ColumnsRub += "{id: \"itogDiv\", header: \"Итог<br/>Откл.\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math:\"[$r, itogPlan] - [$r, itogFact]\" },";
            return articleData;
        }

        private string ProcessArticlePlan(PiuWS.Article article, string userName, string parent)
        {
            if (!article.ToConfirm)
                ToConfirm = false;

            string articleData = "{" + String.Format("id:\"{0}\", name:\"{1}\", parent:\"{2}\", toConfirm:\"{3}\"", article.Code, article.Name, parent, article.ToConfirm) + "},\r\n";
            articleData += "{" + String.Format("id:\"{0}\", level:\"{1}\"", article.Code, article.Level) + "},\r\n";

            if (article.Allowed != null)
            {
                var a = article.Allowed.ToList().FirstOrDefault(p => p.NameCFR.ToLower() == userName.ToLower());

                if(a != null)
                    articleData += "{" + String.Format("id:\"{0}\", allow:\"true\"", article.Code) + "},\r\n";
                else
                    articleData += "{" + String.Format("id:\"{0}\", allow:\"false\"", article.Code) + "},\r\n";

                if(a == null && article.ToConfirm)
                    articleData += "{" + String.Format("id:\"{0}\", readonly:\"true\"", article.Code) + "},\r\n";
            }

            this.ColumnsRub = "{id: \"id\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"code\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"parent\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"organization\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"name\", header: \"Наименование\", width: 400 },\r\n";
            this.ColumnsRub += "{id: \"level:\", hidden: \"true\" },\r\n";

            string planItogFormula = "";

            foreach (var adata in article.DataForPeriod)
            {
                this.ColumnsRub += "{ id: \"sumplan_" + adata.Period.ToString("MMyy") + "\", header: \"" + adata.Period.ToString("MMM. yy") + "<br/>План\", editor: 'text', css: \"planColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }) },\r\n";

                // Формулы расчета статей, если есть.
                string formula = "";

                // Суммируемые ячейки
                if (article.Additional != null)
                    foreach (var add in article.Additional)
                        if (!String.IsNullOrEmpty(add.Code))
                            formula += " + [" + add.Code + ", sumplan_" + adata.Period.ToString("MMyy") + "]";

                // Вычитаемые ячейки
                if (article.Exception != null)
                    foreach (var div in article.Exception)
                        if (!String.IsNullOrEmpty(div.Code))
                            formula += " + [" + div.Code + ", sumplan_" + adata.Period.ToString("MMyy") + "]";

                string css = "";

                if (article.Level == 1)
                    css = "level1css";

                if (article.Level == 2)
                    css = "level2css";

                if (article.Level == 3)
                    css = "level3css";

                if (article.ToConfirm)
                    css += " toAllow";

                if (!String.IsNullOrEmpty(formula))
                    articleData += "{" + string.Format("id:\"{0}\", sumplan_{1}:\"= {2}\", $css:\"{3}\"", article.Code, adata.Period.ToString("MMyy"), formula, css) + "},\r\n";
                else
                    articleData += "{" + string.Format("id:\"{0}\", sumplan_{1}:\"{2}\", $css:\"{3}\"", article.Code, adata.Period.ToString("MMyy"), adata.SumPlan, css) + "},\r\n";

                planItogFormula += " + [$r, sumplan_" + adata.Period.ToString("MMyy") + "]";
            }

            foreach (var child in article.Child)
                articleData += ProcessArticlePlan(child, userName, article.Code);


            this.ColumnsRub += "{id: \"itogPlan\", header: \"Итог<br/>План\", css: \"itogColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 0 }), math: \"" + planItogFormula + "\"},";
            return articleData;
        }

        private string ProcessArticleConsolidated(PiuWS.Article article, string userName, string organizationCode)
        {
            string articleData = "{" + String.Format("id:\"{0}\", name:\"{1}\", maxDiv:\"{2}\", level:\"{3}\"", article.Code, article.Name, article.SumNormMax, article.Level);

            foreach (var adata in article.DataForPeriod)
            {
                articleData += String.Format(", prcNorm:\"{0}\"", adata.SumNorm);
                articleData += String.Format(", prc_{0}:\"=GetVal({1})\"", organizationCode, "([" + article.Code + ", sumfact_" + organizationCode + "] / [01.000.000, sumfact_" + organizationCode + "]) * 100");

                string css = "";

                if (article.Level == 1)
                    css = "level1css";

                if (article.Level == 2)
                    css = "level2css";

                if (article.Level == 3)
                    css = "level3css";

                articleData += String.Format(", sumfact_{1}:\"{2}\", sumplan_{1}:\"{3}\", div_{1}:\"=[{0}, sumplan_{1}] - [{0}, sumfact_{1}]\", $css:\"{4}\"", article.Code, organizationCode, adata.SumFact, adata.SumPlan, css);
            }

            articleData += "},\r\n";

            foreach (var child in article.Child)
                articleData += ProcessArticleConsolidated(child, userName, organizationCode);

            return articleData;
        }
        private string ProcessArticleForNorms(PiuWS.Article article, string userName, string parent)
        {
            if (!article.ToConfirm)
                ToConfirm = false;

            string articleData = "{" + String.Format("id:\"{0}\", name:\"{1}\", criticalDev:\"{2}\", parent:\"{3}\"", article.Code, article.Name, article.SumNormMax, parent) + "},\r\n";
            articleData += "{" + String.Format("id:\"{0}\", maxDiv:\"{1}\"", article.Code, article.SumNormMax) + "},\r\n";
            articleData += "{" + String.Format("id:\"{0}\", level:\"{1}\"", article.Code, article.Level) + "},\r\n";

            this.ColumnsRub = "{id: \"id\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"code\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"parent\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"organization\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{id: \"name\", header: \"Наименование\", width: 400 },\r\n";
            this.ColumnsRub += "{id: \"level:\", hidden: \"true\" },\r\n";
            this.ColumnsRub += "{ id: \"prcNorm\", header: \"Норма\", editor: 'text', css: \"normColumn\", format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }) },\r\n";
            this.ColumnsRub += "{id: \"maxDiv:\", header: \"Макс. откл.\", editor: 'text', format: webix.Number.numToStr({ groupDelimiter: \" \", groupSize: 3, decimalDelimiter: \".\", decimalSize: 2 }) },\r\n";

            foreach (var adata in article.DataForPeriod)
            {
                string css = "";

                if (article.Level == 1)
                    css = "level1css";

                if (article.Level == 2)
                    css = "level2css";

                if (article.Level == 3)
                    css = "level3css";

                articleData += "{" + string.Format("id:\"{0}\", name:\"{1}\", prcNorm:\"{2}\", maxDiv:\"{3}\", $css:\"{4}\"", article.Code, article.Name, adata.SumNorm, article.SumNormMax, css) + "},\r\n";
            }

            foreach (var child in article.Child)
                articleData += ProcessArticleForNorms(child, userName, article.Code);

            return articleData;
        }

        public bool Save(string data, string UserName, bool ToConfirm, string organization)
        {
            bool res = false;

            try
            {
                var serializer = new JavaScriptSerializer();
                var heapdata = serializer.DeserializeObject(data);
                PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();

                PiuWS.PIU piu = new PiuWS.PIU();
                piu.DateStart = DateTime.Parse(StartDate);
                piu.DateEnd = DateTime.Parse(EndDate);
                piu.Confirmed = false;

                List<PiuWS.Entry> entries = new List<PiuWS.Entry>();
                entries.Add(new PiuWS.Entry() { Organization = new PiuWS.Organization() { Code = "", FullName = organization, Name = organization, ShortName = organization, Deleted = false }, Articles = new PiuWS.Article[0] });
                piu.Entries = entries.ToArray();

                foreach (var undata in (Array)heapdata)
                {
                    bool toConfirm = false;
                    var r = (Dictionary<string, object>)undata;

                    object id = null;
                    r.TryGetValue("id", out id);
                    object level = null;
                    r.TryGetValue("level", out level);
                    object name = null;
                    r.TryGetValue("name", out name);
                    object allow = null;
                    r.TryGetValue("allow", out allow);
                    object confirmed = null;
                    r.TryGetValue("confirmed", out confirmed);
                    object comment = null;
                    r.TryGetValue("comment", out comment);

                    if (comment == null)
                        comment = "";

                    if (confirmed == null)
                        confirmed = false;

                    //Если нет разрешения для редактирования статьи
                    if (allow == null)
                        allow = false;

                    //Если есть разрешение на редактирование статьи и стоит отметка "на согласование", выставляем флаг согласования для статьи
                    if (Convert.ToBoolean(allow) && ToConfirm)
                        toConfirm = true;

                    piu.Reconciliations = new PiuWS.Reconciliation[0];

                    if (level != null && level.ToString() == "1")
                    {
                        List<PiuWS.Article> articles = piu.Entries[0].Articles.ToList();
                        articles.Add(new PiuWS.Article()
                        {
                            Additional = new PiuWS.Rated[0],
                            Exception = new PiuWS.Rated[0],
                            Allowed = new PiuWS.GroupCFR[0],
                            Child = new List<PiuWS.Article>().ToArray(),
                            Code = id.ToString(),
                            Level = Convert.ToInt32(level),
                            Name = name.ToString(),
                            ToConfirm = Convert.ToBoolean(toConfirm),
                            DataForPeriod = new List<PiuWS.DataArticle>().ToArray(),
                            Comment = comment.ToString(),
                            Confirmed = Convert.ToBoolean(confirmed)
                        });

                        piu.Entries[0].Articles = articles.ToArray();

                        var t = r.Where(p => p.Key.StartsWith("sumplan"));

                        if (t != null)
                        {
                            foreach (var v in t.ToList())
                            {
                                object period = null;
                                period = v.Key.Split('_')[1].ToString();
                                period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                                object sumplan = null;
                                sumplan = v.Value;

                                if (String.IsNullOrEmpty(sumplan.ToString()))
                                    sumplan = "0";

                                var dfp = piu.Entries[0].Articles.Last().DataForPeriod.ToList();
                                dfp.Add(new PiuWS.DataArticle()
                                {
                                    Period = Convert.ToDateTime(period),
                                    SumPlan = Convert.ToDecimal(sumplan)
                                });
                                piu.Entries.Last().Articles.Last().DataForPeriod = dfp.ToArray();
                            }
                        }
                    }

                    if (level != null && level.ToString() == "2")
                    {
                        List<PiuWS.Article> articles = piu.Entries[0].Articles.Last().Child.ToList();
                        articles.Add(new PiuWS.Article()
                        {
                            Additional = new PiuWS.Rated[0],
                            Exception = new PiuWS.Rated[0],
                            Allowed = new PiuWS.GroupCFR[0],
                            Child = new List<PiuWS.Article>().ToArray(),
                            Code = id.ToString(),
                            Level = Convert.ToInt32(level),
                            Name = name.ToString(),
                            ToConfirm = Convert.ToBoolean(toConfirm),
                            DataForPeriod = new List<PiuWS.DataArticle>().ToArray(),
                            Comment = comment.ToString(),
                            Confirmed = Convert.ToBoolean(confirmed)
                        });

                        piu.Entries[0].Articles.Last().Child = articles.ToArray();

                        var t = r.Where(p => p.Key.StartsWith("sumplan"));

                        if (t != null)
                        {
                            foreach (var v in t.ToList())
                            {
                                object period = null;
                                period = v.Key.Split('_')[1].ToString();
                                period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                                object sumplan = null;
                                sumplan = v.Value;

                                if (String.IsNullOrEmpty(sumplan.ToString()))
                                    sumplan = "0";

                                var dfp = piu.Entries[0].Articles.Last().Child.Last().DataForPeriod.ToList();
                                dfp.Add(new PiuWS.DataArticle()
                                {
                                    Period = Convert.ToDateTime(period),
                                    SumPlan = Convert.ToDecimal(sumplan)
                                });
                                piu.Entries[0].Articles.Last().Child.Last().DataForPeriod = dfp.ToArray();
                            }
                        }
                    }

                    if (level != null && level.ToString() == "3")
                    {
                        List<PiuWS.Article> articles = piu.Entries[0].Articles.Last().Child.Last().Child.ToList();
                        articles.Add(new PiuWS.Article()
                        {
                            Additional = new PiuWS.Rated[0],
                            Exception = new PiuWS.Rated[0],
                            Allowed = new PiuWS.GroupCFR[0],
                            Child = new List<PiuWS.Article>().ToArray(),
                            Code = id.ToString(),
                            Level = Convert.ToInt32(level),
                            Name = name.ToString(),
                            ToConfirm = Convert.ToBoolean(toConfirm),
                            DataForPeriod = new List<PiuWS.DataArticle>().ToArray(),
                            Comment = comment.ToString(),
                            Confirmed = Convert.ToBoolean(confirmed)
                        });

                        piu.Entries[0].Articles.Last().Child.Last().Child = articles.ToArray();

                        var t = r.Where(p => p.Key.StartsWith("sumplan"));

                        if (t != null)
                        {
                            foreach (var v in t.ToList())
                            {
                                object period = null;
                                period = v.Key.Split('_')[1].ToString();
                                period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                                object sumplan = null;
                                sumplan = v.Value;

                                if (String.IsNullOrEmpty(sumplan.ToString()))
                                    sumplan = "0";

                                var dfp = piu.Entries[0].Articles.Last().Child.Last().Child.Last().DataForPeriod.ToList();
                                dfp.Add(new PiuWS.DataArticle()
                                {
                                    Period = Convert.ToDateTime(period),
                                    SumPlan = Convert.ToDecimal(sumplan)
                                });
                                piu.Entries[0].Articles.Last().Child.Last().Child.Last().DataForPeriod = dfp.ToArray();
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(organization))
                {
                    model.PutPIUData2(UserName, DateTime.Parse(StartDate), piu);
                    res = true;
                }
                else
                    res = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public void Get()
        {
            ToConfirm = true;

            if (Organization == null)
                Organization = "";

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            model.Timeout = 300000;
            PiuWS.PIU t = model.GetPIUData2(UserName, Organization, DateTime.Parse(StartDate), DateTime.Parse(EndDate), "0");

            var r = t.Reconciliations.FirstOrDefault(p => p.UserName == UserName);

            if (r == null)
                CanConfirm = false;

            if (r != null && r.Agreed)
            {
                CanConfirm = true;
                ToConfirm = false;
            }

            string s = "";

            foreach (var entry in t.Entries)
            {
                foreach (var a in entry.Articles)
                {
                    s += ProcessArticle(a, UserName, a.Code);
                }
            }

            this.Data = s;
        }

        public void GetPlan()
        {
            ToConfirm = true;

            if (Organization == null)
                Organization = "";

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            model.Timeout = 300000;

            //bool b = model.Test(ar.ToArray());

            PiuWS.PIU t = model.GetPIUData2(UserName, Organization, DateTime.Parse(StartDate), DateTime.Parse(EndDate), "0");
            //model.PutPIUData2("tv", "2018-01-01", t);

            var r = t.Reconciliations.FirstOrDefault(p => p.UserName == UserName);

            if (r == null)
                CanConfirm = false;

            //if (r != null && r.Agreed)
            if (r != null)
            {
                CanConfirm = true;
                ToConfirm = false;
            }

            string s = "";

            foreach (var entry in t.Entries)
            {
                foreach (var a in entry.Articles)
                {
                    s += ProcessArticlePlan(a, UserName, a.Code);
                }
            }

            if (CanConfirm)
            {
                this.ColumnsRub += "{ id:\"confirmed\", header:{ content:\"masterCheckbox\" }, checkValue:'true', uncheckValue:'false', template:\"{common.checkbox()}\", width:40, css: \"checkColumn\" }, ";
                this.ColumnsRub += "{ id:\"comment\", header:\"Примечание\", editor: 'text', width: 300, css: \"commentColumn\" }";
            }
            else
            {
                this.ColumnsRub += "{ id:\"comment\", header:\"Примечание\", width: 300, css: \"commentColumn\" }";
            }
            
            this.Data = s;
        }

        public void GetNorms(string UserName, string Organization)
        {
            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            PiuWS.PIU piu = model.GetNorms2(UserName, Organization, DateTime.Parse(StartDate));

            string s = "";

            foreach (var entry in piu.Entries)
            {
                foreach (var a in entry.Articles)
                {
                    s += ProcessArticleForNorms(a, UserName, a.Code);
                }
            }

            this.Data = s;
        }

        public bool SaveNorms(string data, string UserName, string date, string Organization)
        {
            bool res = false;

            try
            {
                var serializer = new JavaScriptSerializer();
                var heapdata = serializer.DeserializeObject(data);
                PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();

                PiuWS.PIU piu = new PiuWS.PIU();
                piu.DateStart = DateTime.Parse(date);
                piu.DateEnd = DateTime.Parse(date);
                piu.Confirmed = false;

                List<PiuWS.Entry> entries = new List<PiuWS.Entry>();
                entries.Add(new PiuWS.Entry() { Organization = new PiuWS.Organization() { Code = "", FullName = "", Name = "", ShortName = "", Deleted = false }, Articles = new PiuWS.Article[0] });
                piu.Entries = entries.ToArray();
                piu.ToConfirm = true;

                foreach (var undata in (Array)heapdata)
                {
                    var r = (Dictionary<string, object>)undata;

                    object id = null;
                    r.TryGetValue("id", out id);
                    object name = null;
                    r.TryGetValue("name", out name);
                    object criticalDev = null;
                    r.TryGetValue("criticalDev", out criticalDev);
                    object parent = null;
                    r.TryGetValue("parent", out parent);
                    object maxDiv = null;
                    r.TryGetValue("maxDiv", out maxDiv);
                    object level = null;
                    r.TryGetValue("level", out level);
                    object prcNorm = null;
                    r.TryGetValue("prcNorm", out prcNorm);

                    if (criticalDev == null)
                        criticalDev = 0;

                    if (maxDiv == null)
                        maxDiv = 0;

                    if (prcNorm == null)
                        prcNorm = 0;

                    piu.Reconciliations = new PiuWS.Reconciliation[0];

                    if (level != null && level.ToString() == "1")
                    {
                        List<PiuWS.Article> articles = piu.Entries[0].Articles.ToList();
                        articles.Add(new PiuWS.Article()
                        {
                            Additional = new PiuWS.Rated[0],
                            Exception = new PiuWS.Rated[0],
                            Allowed = new PiuWS.GroupCFR[0],
                            Child = new List<PiuWS.Article>().ToArray(),
                            Code = id.ToString(),
                            Level = Convert.ToInt32(level),
                            Name = name.ToString(),
                            ToConfirm = false,
                            SumNormMax = Convert.ToDecimal(maxDiv),
                            DataForPeriod = new List<PiuWS.DataArticle>().ToArray(),
                            Comment = "",
                            Confirmed = false
                        });

                        piu.Entries[0].Articles = articles.ToArray();

                        var dfp = piu.Entries[0].Articles.Last().DataForPeriod.ToList();
                        dfp.Add(new PiuWS.DataArticle()
                        {
                            Period = DateTime.Parse(date),
                            SumNorm = Convert.ToDecimal(prcNorm)
                        });
                        piu.Entries.Last().Articles.Last().DataForPeriod = dfp.ToArray();
                    }

                    if (level != null && level.ToString() == "2")
                    {
                        List<PiuWS.Article> articles = piu.Entries[0].Articles.Last().Child.ToList();
                        articles.Add(new PiuWS.Article()
                        {
                            Additional = new PiuWS.Rated[0],
                            Exception = new PiuWS.Rated[0],
                            Allowed = new PiuWS.GroupCFR[0],
                            Child = new List<PiuWS.Article>().ToArray(),
                            Code = id.ToString(),
                            Level = Convert.ToInt32(level),
                            Name = name.ToString(),
                            ToConfirm = false,
                            SumNormMax = Convert.ToDecimal(maxDiv),
                            DataForPeriod = new List<PiuWS.DataArticle>().ToArray(),
                            Comment = "",
                            Confirmed = false
                        });

                        piu.Entries[0].Articles.Last().Child = articles.ToArray();

                        var dfp = piu.Entries[0].Articles.Last().Child.Last().DataForPeriod.ToList();
                        dfp.Add(new PiuWS.DataArticle()
                        {
                            Period = DateTime.Parse(date),
                            SumNorm = Convert.ToDecimal(prcNorm)
                        });
                        piu.Entries[0].Articles.Last().Child.Last().DataForPeriod = dfp.ToArray();
                    }

                    if (level != null && level.ToString() == "3")
                    {
                        List<PiuWS.Article> articles = piu.Entries[0].Articles.Last().Child.Last().Child.ToList();
                        articles.Add(new PiuWS.Article()
                        {
                            Additional = new PiuWS.Rated[0],
                            Exception = new PiuWS.Rated[0],
                            Allowed = new PiuWS.GroupCFR[0],
                            Child = new List<PiuWS.Article>().ToArray(),
                            Code = id.ToString(),
                            Level = Convert.ToInt32(level),
                            Name = name.ToString(),
                            ToConfirm = false,
                            SumNormMax = Convert.ToDecimal(maxDiv),
                            DataForPeriod = new List<PiuWS.DataArticle>().ToArray(),
                            Comment = "",
                            Confirmed = false
                        });

                        piu.Entries[0].Articles.Last().Child.Last().Child = articles.ToArray();

                        var dfp = piu.Entries[0].Articles.Last().Child.Last().Child.Last().DataForPeriod.ToList();
                        dfp.Add(new PiuWS.DataArticle()
                        {
                            Period = DateTime.Parse(date),
                            SumNorm = Convert.ToDecimal(prcNorm)
                        });
                        piu.Entries[0].Articles.Last().Child.Last().Child.Last().DataForPeriod = dfp.ToArray();
                    }
                }

                model.PutNorms2(UserName, Organization, DateTime.Parse(date), piu);
                res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public void GetRoles(string userName, string organization)
        {
            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            var t = model.GetRoles(userName, organization, DateTime.Today);
        }
    }
}