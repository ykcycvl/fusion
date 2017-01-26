using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sh4Ole;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fusion.Models.SH4
{
    public class SH4
    {
        Sh4Ole.SH4App sh4 = new SH4App();
        public int Open()
        {
            sh4.SetServerName("10.1.0.14:pTa10002t60000");
            return sh4.DBLoginEx("Admin", "2707");
        }
        public void Close()
        {
            sh4.DBLogout();
        }

        /*Здесь будут описания классов*/
        public class GoodsTreeItem
        {
            [Display(Name="ID")]
            public int ID { get; set; }
            [Display(Name = "Родитель")]
            public int? ParentID { get; set; }
            [Display(Name = "Наименование")]
            public string Name { get; set; }
            [Display(Name = "Код")]
            public string Code { get; set; }
            [Display(Name = "Внешний код")]
            public string ExternalCode { get; set; }
        }
        public class Good
        {
            public dynamic id { get; set; }
            public dynamic GroupID { get; set; }
            public dynamic GroupName { get; set; }
            public dynamic Name { get; set; }
            public dynamic CodePrefix { get; set; }
            public dynamic CodeNumber { get; set; }
            public dynamic Unit { get; set; }
            public dynamic ComplectID { get; set; }
            public dynamic ComplectName { get; set; }
        }
        public class GoodsBalance
        {
            public Good good { get; set; }
            public dynamic balance { get; set; }
            public double cost { get; set; }
            public double cost_bal { get; set; }
        }
        /*Здесь будут переменные*/
        public List<GoodsTreeItem> GoodsTree = new List<GoodsTreeItem>();
        public List<Good> Goods = new List<Good>();
        public List<GoodsBalance> GoodsBalances = new List<GoodsBalance>();
        /*Здесь будут методы*/
        public void GetGoodsTree(int? id)
        {
            int res = sh4.GoodsTree();

            if (res >= 0)
            {
                while (sh4.EOF(res) != 1)
                {
                    GoodsTreeItem gti = new GoodsTreeItem();
                    gti.ID = sh4.ValByName(res, "1.209.1.0");

                    if (sh4.ValByName(res, "1.209.2.0").GetType() != typeof(DBNull))
                        gti.ParentID = sh4.ValByName(res, "1.209.2.0");

                    Encoding srcEncodingFormat = Encoding.GetEncoding(1251);
                    Encoding dstEncodingFormat = Encoding.UTF8;
                    byte[] originalByteString = srcEncodingFormat.GetBytes(sh4.ValByName(res, "1.209.3.0").ToString());
                    byte[] convertedByteString = Encoding.Convert(srcEncodingFormat, dstEncodingFormat, originalByteString);
                    gti.Name = dstEncodingFormat.GetString(convertedByteString);
                    gti.Code = sh4.ValByName(res, "1.209.4.0").ToString();
                    gti.ExternalCode = sh4.ValByName(res, "1.209.5.0").ToString();

                    GoodsTree.Add(gti);
                    sh4.Next(res);
                }

                sh4.CloseQuery(res);
            }
        }
        public void GetGoods(int? id)
        {
            GetGoodsTree(id);

            if (id == null)
            {
                for (int i = 0; i < GoodsTree.Count; i++)
                {
                    int res = sh4.Goods(GoodsTree[i].ID);

                    if (res >= 0)
                    {
                        while (sh4.EOF(res) != 1)
                        {
                            Good g = new Good();
                            g.id = sh4.ValByName(res, "1.210.1.0");
                            g.GroupID = sh4.ValByName(res, "1.209.1.0");
                            g.Name = sh4.ValByName(res, "1.210.2.0");
                            g.CodePrefix = sh4.ValByName(res, "1.210.3.0");
                            g.CodeNumber = sh4.ValByName(res, "1.210.4.0");
                            g.Unit = sh4.ValByName(res, "1.206.2.0");
                            g.ComplectID = sh4.ValByName(res, "1.200.1.1");
                            g.ComplectName = sh4.ValByName(res, "1.200.2.1");

                            Goods.Add(g);
                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);
                    }
                }
            }
            else
            {
                int res = sh4.Goods((int)id);

                if (res >= 0)
                {
                    while (sh4.EOF(res) != 1)
                    {
                        Good g = new Good();
                        g.id = sh4.ValByName(res, "1.210.1.0");
                        g.GroupID = sh4.ValByName(res, "1.209.1.0");
                        g.Name = sh4.ValByName(res, "1.210.2.0");
                        g.CodePrefix = sh4.ValByName(res, "1.210.3.0");
                        g.CodeNumber = sh4.ValByName(res, "1.210.4.0");
                        g.Unit = sh4.ValByName(res, "1.206.2.0");
                        g.ComplectID = sh4.ValByName(res, "1.200.1.1");
                        g.ComplectName = sh4.ValByName(res, "1.200.2.1");

                        Goods.Add(g);
                        sh4.Next(res);
                    }

                    sh4.CloseQuery(res);
                }
            }

            for (int i = 0; i < Goods.Count; i++)
            {
                Goods[i].GroupName = GoodsTree.FirstOrDefault(p => p.ID == Goods[i].GroupID).Name;
            }
        }
        public void GetBalances(int? GroupID, int? GoodID)
        {
            if (GroupID == null && GoodID == null)
            {
                GetGoods(null);

                //Остатки
                for (int i = 0; i < Goods.Count; i++)
                {
                    int res = sh4.GsFifo(Goods[i].id, 0, DateTime.Today.ToOADate(), DateTime.Today.ToOADate());

                    if (res >= 0)
                    {
                        while (sh4.EOF(res) != 1)
                        {
                            if (sh4.ValByName(res, "1.105.1.1").GetType() == typeof(DBNull))
                            {
                                GoodsBalance gb = new GoodsBalance();
                                gb.good = Goods[i];
                                gb.balance = sh4.ValByName(res, "1.105.3.0");
                                gb.cost_bal = sh4.ValByName(res, "1.105.4.0");
                                GoodsBalances.Add(gb);
                                break;
                            }

                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);
                    }
                }
            }
            else
                if (GroupID != null)
                {
                    GetGoods(GroupID);

                    for (int i = 0; i < Goods.Count; i++)
                    {
                        int res = sh4.GsFifo(Goods[i].id, 0, DateTime.Today.ToOADate(), DateTime.Today.ToOADate());

                        if (res >= 0)
                        {
                            while (sh4.EOF(res) != 1)
                            {
                                if (sh4.ValByName(res, "1.105.1.1").GetType() == typeof(DBNull))
                                {
                                    GoodsBalance gb = new GoodsBalance();
                                    gb.good = Goods[i];
                                    gb.balance = sh4.ValByName(res, "1.105.3.0");

                                    dynamic sum = sh4.ValByName(res, "1.105.4.0");

                                    if (gb.balance > 0)
                                        gb.cost_bal = (double)sum / (double)gb.balance;
                                    else
                                        gb.cost_bal = 0;

                                    GoodsBalances.Add(gb);
                                    break;
                                }

                                sh4.Next(res);
                            }

                            sh4.CloseQuery(res);
                        }
                    }
                }
                else
                {
                    GetGoodsTree(null);
                    int res = sh4.GoodByRID((int)GoodID);

                    if (res >= 0)
                    {
                        Good g = new Good();

                        while (sh4.EOF(res) != 1)
                        {
                            g.id = sh4.ValByName(res, "1.210.1.0");
                            g.GroupID = sh4.ValByName(res, "1.209.1.0");
                            g.Name = sh4.ValByName(res, "1.210.2.0");
                            g.CodePrefix = sh4.ValByName(res, "1.210.3.0");
                            g.CodeNumber = sh4.ValByName(res, "1.210.4.0");
                            g.Unit = sh4.ValByName(res, "1.206.2.0");
                            g.ComplectID = sh4.ValByName(res, "1.200.1.1");
                            g.ComplectName = sh4.ValByName(res, "1.200.2.1");
                            g.GroupName = GoodsTree.FirstOrDefault(p => p.ID == g.GroupID).Name;

                            Goods.Add(g);
                            sh4.Next(res);
                        }

                        sh4.CloseQuery(res);

                        res = sh4.GsFifo((int)GoodID, 0, DateTime.Today.ToOADate(), DateTime.Today.ToOADate());

                        if (res >= 0)
                        {
                            while (sh4.EOF(res) != 1)
                            {
                                if (sh4.ValByName(res, "1.105.1.1").GetType() == typeof(DBNull))
                                {
                                    GoodsBalance gb = new GoodsBalance();
                                    gb.good = g;
                                    gb.balance = sh4.ValByName(res, "1.105.3.0");
                                    dynamic sum = sh4.ValByName(res, "1.105.4.0");

                                    if (gb.balance > 0)
                                        gb.cost_bal = (double)sum / (double)gb.balance;
                                    else
                                        gb.cost_bal = 0;

                                    GoodsBalances.Add(gb);
                                    break;
                                }

                                sh4.Next(res);
                            }

                            sh4.CloseQuery(res);
                        }
                    }
                }

            if (GoodsBalances.Count > 0)
            {
                DateTime startdate = DateTime.Parse("01." + DateTime.Today.AddMonths(-1).Month + "." + DateTime.Today.AddMonths(-1).Year);
                DateTime enddate = DateTime.Parse(DateTime.DaysInMonth(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month) + "." + DateTime.Today.AddMonths(-1).Month + "." + DateTime.Today.AddMonths(-1).Year);

                //Себестоимость
                for (int i = 0; i < GoodsBalances.Count; i++)
                {
                    GoodsBalances[i].cost = GetCost(GoodsBalances[i].good.id, startdate, enddate);
                }
            }
        }
        public double GetCost(int GoodsID, DateTime startdate, DateTime enddate)
        {
            dynamic sum = 0;
            dynamic cnt = 0;
            int res = sh4.GsFifo(GoodsID, 0, startdate.ToOADate(), enddate.ToOADate());

            if (res >= 0)
            {
                while (sh4.EOF(res) != 1)
                {
                    if (sh4.ValByName(res, "2.103.10.1").GetType() != typeof(DBNull))
                        if (sh4.ValByName(res, "2.103.10.1") == 12 || sh4.ValByName(res, "2.103.10.1") == 12)
                        {
                            sum += sh4.ValByName(res, "2.105.4.0");
                            cnt += sh4.ValByName(res, "2.105.3.0");
                        }

                    sh4.Next(res);
                }

                sh4.CloseQuery(res);
            }

            if (cnt != 0)
                return (double)sum / (double)cnt;
            else
                return 0;
        }
    }
}