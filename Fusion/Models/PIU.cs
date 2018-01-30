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
        public PiuWS.Tree Tree;
        [Display(Name="Начало периода")]
        public string StartDate { get; set; }
        [Display(Name = "Конец периода")]
        public string EndDate { get; set; }
        [Display(Name = "Подразделение")]
        public string Organization { get; set; }
        public string userName { get; set; }

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

        public IEnumerable<SelectListItem> OrganizationsSelectList
        {
            get
            {
                List<SelectListItem> Orgs = new List<SelectListItem>();

                using (PiuWS.fsn_PIU service = new PiuWS.fsn_PIU())
                {
                    service.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                    var Organizations = service.GetOrganizations(userName).ToList();

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

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            Tree = model.GetPIUData("tv", org, date1, date2);
        }

        public bool Save(string data, string UserName, bool Confirm)
        {
            bool res = false;

            try
            {
                var serializer = new JavaScriptSerializer();
                var heapdata = serializer.DeserializeObject(data);
                Tree = new PiuWS.Tree();

                if (Confirm)
                {
                    Tree.Reconciliations = new PiuWS.Reconciliation[1];
                    Tree.Reconciliations[0].UserName = UserName;
                    Tree.Reconciliations[0].Agreed = true;
                }

                string org = "";

                PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
                List<PiuWS.Level1> levels1 = new List<PiuWS.Level1>();
                List<PiuWS.Level2> levels2 = new List<PiuWS.Level2>();
                List<PiuWS.Level3> levels3 = new List<PiuWS.Level3>();

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

                    if (org == "" && organization != null)
                        org = organization.ToString();

                    if (level != null && level.ToString() == "1")
                    {
                        if (Tree.Levels1 == null)
                            Tree.Levels1 = new PiuWS.Level1[0];

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

                                var l1 = Tree.Levels1.ToList();
                                l1.Add(new PiuWS.Level1() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), Period = DateTime.Parse(period.ToString()), SumPlan = Convert.ToDecimal(sumplan), Levels2 = new PiuWS.Level2[0] });
                                Tree.Levels1 = l1.ToArray();
                            }
                        }
                    }

                    if (level != null && level.ToString() == "2")
                    {
                        if (parent != null)
                        {
                            var t = r.Where(p => p.Key.StartsWith("sumplan"));

                            foreach (var v in t.ToList())
                            {
                                object period = null;
                                period = v.Key.Split('_')[1].ToString();
                                period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                                object sumplan = null;
                                sumplan = v.Value;

                                var l1 = Tree.Levels1.FirstOrDefault(p => p.Code == parent.ToString() && p.Period == DateTime.Parse(period.ToString()));
                                var l2 = l1.Levels2.ToList();
                                l2.Add(new PiuWS.Level2() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), Period = DateTime.Parse(period.ToString()), SumPlan = Convert.ToDecimal(sumplan), Levels3 = new PiuWS.Level3[0] });
                                l1.Levels2 = l2.ToArray();
                            }
                        }
                    }

                    if (level != null && level.ToString() == "3")
                    {
                        if (parent != null)
                        {
                            var t = r.Where(p => p.Key.StartsWith("sumplan"));

                            foreach (var v in t.ToList())
                            {
                                object period = null;
                                period = v.Key.Split('_')[1].ToString();
                                period = "01." + period.ToString().Substring(0, 2) + ".20" + period.ToString().Substring(2, 2);
                                object sumplan = null;
                                sumplan = v.Value;

                                var lvl1 = Tree.Levels1.Where(p => p.Period == DateTime.Parse(period.ToString()));

                                foreach (var l in lvl1.ToList())
                                {
                                    var l2 = l.Levels2.FirstOrDefault(p => p.Code == parent.ToString() && p.Period == DateTime.Parse(period.ToString()));

                                    if (l2 == null)
                                        continue;

                                    var l3 = l2.Levels3.ToList();

                                    l3.Add(new PiuWS.Level3() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), Period = DateTime.Parse(period.ToString()), SumPlan = Convert.ToDecimal(sumplan) });
                                    l2.Levels3 = l3.ToArray();
                                }                                
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(org))
                {
                    model.PutPIUData(UserName, "Токио", "01.09.2017", this.Tree);
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

        public void Get(string UserName)
        {
            if (Organization == null)
                Organization = "";

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            Tree = model.GetPIUData(UserName, Organization, StartDate, EndDate);
        }

        public void GetNorms(string UserName, DateTime dt)
        {
            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            Tree = model.GetNorms(UserName, Organization, dt.ToString("dd.MM.yyyy"));
        }

        public bool SaveNorms(string data, string UserName, string year)
        {
            bool res = false;

            try
            {
                var serializer = new JavaScriptSerializer();
                var heapdata = serializer.DeserializeObject(data);
                Tree = new PiuWS.Tree();
                string org = "";

                PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
                List<PiuWS.Level1> levels1 = new List<PiuWS.Level1>();
                List<PiuWS.Level2> levels2 = new List<PiuWS.Level2>();
                List<PiuWS.Level3> levels3 = new List<PiuWS.Level3>();
                string lastL1Code = "";

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
                    object sumNorm = null;
                    r.TryGetValue("sumNorm", out sumNorm);
                    object maxDev = null;
                    r.TryGetValue("maxDev", out maxDev);

                    if (sumNorm == null || sumNorm.ToString() == "")
                        sumNorm = "0";

                    if (maxDev == null || maxDev.ToString() == "")
                        maxDev = "0";

                    sumNorm = sumNorm.ToString().Replace(",", ".");
                    maxDev = maxDev.ToString().Replace(",", ".");

                    if (org == "" && organization != null)
                        org = organization.ToString();

                    if (level != null && level.ToString() == "1")
                    {
                        if (Tree.Levels1 == null)
                            Tree.Levels1 = new PiuWS.Level1[0];

                        var l1 = Tree.Levels1.ToList();
                        l1.Add(new PiuWS.Level1() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), SumNorm = Convert.ToDecimal(sumNorm), SumNormMax = Convert.ToDecimal(maxDev), Levels2 = new PiuWS.Level2[0] });
                        Tree.Levels1 = l1.ToArray();
                    }

                    if (level != null && level.ToString() == "2")
                    {
                        if (parent != null)
                        {
                            var l1 = Tree.Levels1.FirstOrDefault(p => p.Code == parent.ToString());
                            lastL1Code = parent.ToString();
                            var l2 = l1.Levels2.ToList();
                            l2.Add(new PiuWS.Level2() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), SumNorm = Convert.ToDecimal(sumNorm), SumNormMax = Convert.ToDecimal(maxDev), Levels3 = new PiuWS.Level3[0] });
                            l1.Levels2 = l2.ToArray();
                        }
                    }

                    if (level != null && level.ToString() == "3")
                    {
                        if (parent != null)
                        {
                            var l1 = Tree.Levels1.FirstOrDefault(p => p.Code == lastL1Code);
                            var l2 = l1.Levels2.FirstOrDefault(x => x.Code == parent.ToString());
                            var l3 = l2.Levels3.ToList();
                            l3.Add(new PiuWS.Level3() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), SumNorm = Convert.ToDecimal(sumNorm), SumNormMax = Convert.ToDecimal(maxDev) });
                            l2.Levels3 = l3.ToArray();
                        }
                    }
                }

                if (!String.IsNullOrEmpty(org))
                {
                    res = true;
                    model.PutNorms(UserName, org, "01.01." + year, this.Tree);
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
    }
}