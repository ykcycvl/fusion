using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Fusion.Models
{
    public class PIU
    {
        public PiuWS.Tree Tree;

        public void GetTest()
        {
            string org = "Токио";
            string date = "01.09.2017";

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            Tree = model.GetPIUData(org, date);
        }

        public bool Save(string data, string UserName)
        {
            bool res = false;

            var serializer = new JavaScriptSerializer();
            var heapdata = serializer.DeserializeObject(data);
            Tree = new PiuWS.Tree();

            PiuWS.fsn_PIU model = new PiuWS.fsn_PIU();
            List<PiuWS.Level1> levels1 = new List<PiuWS.Level1>();
            List<PiuWS.Level2> levels2 = new List<PiuWS.Level2>();
            List<PiuWS.Level3> levels3 = new List<PiuWS.Level3>();

            foreach (var undata in (Array)heapdata)
            {
                var r = (Dictionary<string, object>)undata;

                object id = null;
                r.TryGetValue("id", out id);
                object level = null;
                r.TryGetValue("level", out level);
                object code = null;
                r.TryGetValue("code", out code);
                object name = null;
                r.TryGetValue("name", out name);
                object sumplan = null;
                r.TryGetValue("sumplan", out sumplan);

                if (String.IsNullOrEmpty(sumplan.ToString()))
                    sumplan = "0";

                object organization = null;
                r.TryGetValue("organization", out organization);
                object period = null;
                r.TryGetValue("period", out period);

                if (level != null && level.ToString() == "1")
                {
                    if (Tree.Levels1 == null)
                        Tree.Levels1 = new PiuWS.Level1[0];

                    var l1 = Tree.Levels1.ToList();
                    l1.Add(new PiuWS.Level1() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), Period = DateTime.Parse(period.ToString()), SumPlan = Convert.ToDecimal(sumplan), Levels2 = new PiuWS.Level2[0] });
                    Tree.Levels1 = l1.ToArray();
                }

                if (level != null && level.ToString() == "2")
                {
                    var l2 = Tree.Levels1[Tree.Levels1.Length - 1].Levels2.ToList();
                    l2.Add(new PiuWS.Level2() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), Period = DateTime.Parse(period.ToString()), SumPlan = Convert.ToDecimal(sumplan), Levels3 = new PiuWS.Level3[0] });
                    Tree.Levels1[Tree.Levels1.Length - 1].Levels2 = l2.ToArray();
                }

                if (level != null && level.ToString() == "3")
                {
                    var l3 = Tree.Levels1[Tree.Levels1.Length - 1].Levels2[Tree.Levels1[Tree.Levels1.Length - 1].Levels2.Length - 1].Levels3.ToList();
                    l3.Add(new PiuWS.Level3() { Code = code.ToString(), Organization = organization.ToString(), Name = name.ToString(), Period = DateTime.Parse(period.ToString()), SumPlan = Convert.ToDecimal(sumplan) });
                    Tree.Levels1[Tree.Levels1.Length - 1].Levels2[Tree.Levels1[Tree.Levels1.Length - 1].Levels2.Length - 1].Levels3 = l3.ToArray();
                }
            }

            model.PutPIUData("Токио", "01.09.2017", this.Tree.Levels1);

            return res;
        }
    }
}