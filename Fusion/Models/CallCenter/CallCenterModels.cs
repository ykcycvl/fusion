using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Data.Common;
using System.Web.Configuration;
using FirebirdSql.Data.FirebirdClient;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Fusion.Models.CallCenter
{
    public class CallCenterModels
    {
        public class Orders : List<Order>
        {
            public DateTime start_dt { get; set; }
            public DateTime end_dt { get; set; }
            public void GetOrders()
            {
                CultureInfo culture = new CultureInfo("ru-RU");
                string connectionString = WebConfigurationManager.ConnectionStrings["DLVConnectionString"].ConnectionString;
                FbConnection fbcon = new FbConnection(connectionString);

                fbcon.Open();

                FbCommand fbcmd = new FbCommand(String.Format(@"select o.order_id, o.work_time, o.status, o.dlv_type, o.order_num, c.l_name, c.f_name, c.m_name, p.phone_number,
                                                    s.street_name, a.house, a.building, a.flat, r.restaurant_name, o.adv_info
                                                FROM
                                                dlv_orders o
                                                INNER JOIN dlv_restaurants r ON o.restaurant_id = r.restaurant_id
                                                INNER JOIN dlv_clients c ON o.guest_id = c.client_id
                                                INNER JOIN dlv_addresses a ON o.address_id = a.address_id
                                                INNER JOIN dlv_streets s ON a.street_id = s.street_id
                                                INNER JOIN dlv_client_phones p ON o.guest_id = p.client_id
                                                WHERE o.work_time >= '{0}' and o.work_time <= '{1}' ORDER BY o.status, o.order_id", this.start_dt.ToShortDateString(), this.end_dt.AddDays(1).ToShortDateString()), fbcon);

                string ttt = fbcmd.CommandText;
                FbDataReader rdr = fbcmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Order order = new Order();
                        order.order_num = rdr["operator_name"].ToString();
                        order.work_time = rdr["work_time"].ToString();
                        order.status = rdr["status"].ToString();
                        order.full_name = rdr["l_name"].ToString() + " " + rdr["f_name"].ToString() + " " + rdr["m_name"].ToString();
                        order.address = rdr["street_name"].ToString() + ", " + rdr["house"].ToString() + ", " + rdr["building"] + ", " + rdr["flat"].ToString();
                        order.restaurant = rdr["restaurant_name"].ToString();
                        order.dlv_type = rdr["dlv_type"].ToString();
                        order.phone = rdr["phone_number"].ToString();

                        if (rdr["adv_info"] != DBNull.Value)
                            order.adv_info = Encoding.GetEncoding(1251).GetString((byte[])rdr["adv_info"]);

                        this.Add(order);
                    }
                }

                fbcon.Close();
            }
        }
        public class Order
        {
            public DateTime start_dt { get; set; }
            public DateTime end_dt { get; set; }
            [DisplayName("Номер заказа")]
            public string order_num { get; set; }
            [DisplayName("Имя гостя")]
            public string full_name { get; set; }
            [DisplayName("Ресторан")]
            public string restaurant { get; set; }
            [DisplayName("Адрес")]
            public string address { get; set; }
            [DisplayName("Время выполнения")]
            public string work_time { get; set; }
            [DisplayName("Доп. инфо")]
            public string adv_info { get; set; }
            private string _status;
            [DisplayName("Состояние")]
            public string status 
            {
                get
                {
                    switch (_status)
                    { 
                        case "1" :
                            return "Ожидание";
                        case "2":
                            return "Производство";
                        case "3":
                            return "3";
                        case "4":
                            return "4";
                        case "5":
                            return "В архиве";
                        default:
                            return "хз";
                    }
                }
                set
                {
                    _status = value;
                }
            }
            private string _dlv_type;
            [DisplayName("Тип доставки")]
            public string dlv_type 
            {
                get
                {
                    if (_dlv_type == "0")
                        return "Доставка";
                    else
                        return "Вынос";
                }
                set
                {
                    _dlv_type = value;
                }
            }
            [DisplayName("Телефон")]
            public string phone { get; set; }
            public List<Order> Orders = new List<Order>();
            public void GetOrders()
            {
                CultureInfo culture = new CultureInfo("ru-RU");
                string connectionString = WebConfigurationManager.ConnectionStrings["DLVConnectionString"].ConnectionString;
                FbConnection fbcon = new FbConnection(connectionString);

                fbcon.Open();

                FbCommand fbcmd = new FbCommand(String.Format(@"select o.order_id, o.work_time, o.status, o.dlv_type, o.order_num, c.l_name, c.f_name, c.m_name, p.phone_number,
                                                    s.street_name, a.house, a.building, a.flat, r.restaurant_name, o.adv_info
                                                FROM
                                                dlv_orders o
                                                INNER JOIN dlv_restaurants r ON o.restaurant_id = r.restaurant_id
                                                INNER JOIN dlv_clients c ON o.guest_id = c.client_id
                                                LEFT JOIN dlv_addresses a ON o.address_id = a.address_id
                                                LEFT JOIN dlv_streets s ON a.street_id = s.street_id
                                                LEFT JOIN dlv_client_phones p ON o.guest_id = p.client_id
                                                WHERE o.work_time >= '{0}' and o.work_time <= '{1}' ORDER BY o.status DESC, o.work_time ASC", this.start_dt.ToShortDateString(), this.end_dt.AddDays(1).ToShortDateString()), fbcon);

                string ttt = fbcmd.CommandText;
                FbDataReader rdr = fbcmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Order order = new Order();
                        order.order_num = rdr["order_num"].ToString();
                        order.work_time = rdr["work_time"].ToString();
                        order.status = rdr["status"].ToString();
                        order.full_name = rdr["l_name"].ToString() + " " + rdr["f_name"].ToString() + " " + rdr["m_name"].ToString();
                        order.address = rdr["street_name"].ToString() + ", " + rdr["house"].ToString() + ", " + rdr["building"] + ", " + rdr["flat"].ToString();
                        order.restaurant = rdr["restaurant_name"].ToString();
                        order.dlv_type = rdr["dlv_type"].ToString();
                        order.phone = rdr["phone_number"].ToString();

                        if (rdr["adv_info"] != DBNull.Value)
                            order.adv_info = Encoding.GetEncoding(1251).GetString((byte[])rdr["adv_info"]);

                        Orders.Add(order);
                    }
                }

                fbcon.Close();
            }
        }
        public class OperatorData
        {
            [Display(Name = "Оператор")]
            public string operatorName { get; set; }
            [Display(Name = "Дата")]
            public string applyDate { get; set; }
            [Display(Name = "Сумма заказов")]
            public string orderSum { get; set; }
            [Display(Name = "Оплачено")]
            public string payedSum { get; set; }
            [Display(Name = "Количество")]
            public string ordersCount { get; set; }
        }
        public class RestReport
        {
            public class ReportString
            {
                public string DlvType;
                public string vremya;
                public string day;
                public string order_summ;
                public string payed_summ;
                public string order_cnt;
            }

            public List<ReportString> rs = new List<ReportString>();
            public void GetReport()
            {
                CultureInfo culture = new CultureInfo("ru-RU");

                //string connectionString = WebConfigurationManager.ConnectionStrings["DLVConnectionString"].ConnectionString;
                string connectionString = "User=SYSDBA;Password=masterkey;Database=10.1.0.109:C:\\DB\\DELIVERYRK7.FDB;Dialect=3;Charset=WIN1251;";
                FbConnection fbcon = new FbConnection(connectionString);

                fbcon.Open();

                FbCommand fbcmd = new FbCommand(String.Format(@"SELECT
                                            DLV_TYPE,
                                            EXTRACT(HOUR from APPLY_TIME) as vremya,
                                            EXTRACT(DAY from APPLY_TIME) as d,
                                            SUM(dlo.order_summ) as order_summ,
                                            SUM(dlo.payed_summ) as payed_summ,
                                            COUNT(dlo.ORDER_ID) as order_cnt
                                        FROM
                                            dlv_orders as dlo
                                        WHERE
                                            deleted = 0
                                            AND
                                            apply_time >= '01.03.2016'
                                            and
                                            apply_time < '01.04.2016'
                                            AND (EXTRACT(HOUR from APPLY_TIME) >= 22 or EXTRACT(HOUR from APPLY_TIME) <= 1)
                                            AND RESTAURANT_ID = 1009190
                                        GROUP BY 1, 2, 3
                                        order BY
                                        3, 2"), fbcon);

                //fbcmd = new FbCommand(String.Format(@"SELECT DLV_TYPE, order_num, apply_time FROM DLV_ORDERS WHERE APPLY_TIME >= '21.04.2016' AND ORDER_NUM = 135"), fbcon);


                string ttt = fbcmd.CommandText;
                FbDataReader rdr = fbcmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReportString r = new ReportString();
                        r.DlvType = rdr["DLV_TYPE"].ToString();
                        r.vremya = rdr["vremya"].ToString();
                        r.day = rdr["d"].ToString();
                        r.order_summ = rdr["order_summ"].ToString();
                        r.payed_summ = rdr["payed_summ"].ToString();
                        r.order_cnt = rdr["order_cnt"].ToString();
                        rs.Add(r);
                    }
                }

                fbcon.Close();
            }
        }
        public class OperatorReport
        {
            [Display(Name = "с")]
            public DateTime start_dt { get; set; }
            [Display(Name = "по")]
            public DateTime end_dt { get; set; }
            public List<OperatorData> CCOperatorsReportData = new List<OperatorData>();
            public void GetOperatorOrdersSum()
            {
                CultureInfo culture = new CultureInfo("ru-RU");

                string connectionString = WebConfigurationManager.ConnectionStrings["DLVConnectionString"].ConnectionString;
                //string connectionString = "User=SYSDBA;Password=masterkey;Database=192.168.0.112:C:\\db\\deliveryrk7.fdb;Dialect=3;Charset=WIN1251;";
                FbConnection fbcon = new FbConnection(connectionString);

                fbcon.Open();

                FbCommand fbcmd = new FbCommand(String.Format(@"SELECT
                                            dlo.operator_name,
                                            CAST(dlo.apply_time as DATE) as apply_time,
                                        --    dlo.apply_time,
                                            SUM(dlo.order_summ) as order_summ,
                                            SUM(dlo.payed_summ) as payed_summ,
                                            COUNT(dlo.ORDER_ID) as order_cnt
                                        FROM
                                            dlv_orders as dlo
                                        --    INNER JOIN DLV_PERSONALS as dlp ON dlo.
                                        WHERE
                                            deleted = 0
                                            AND
                                            apply_time >= '{0}'
                                            and
                                            apply_time < '{1}'
                                        GROUP BY dlo.operator_name, 2
                                        order BY
                                        apply_time,
										dlo.operator_name", start_dt, end_dt.AddDays(1).ToShortDateString()), fbcon);

                string ttt = fbcmd.CommandText;
                FbDataReader rdr = fbcmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        OperatorData od = new OperatorData();
                        od.operatorName = rdr["operator_name"].ToString();
                        //od.applyDate = Convert.ToDateTime(rdr["apply_time"]).Day.ToString() + "." + Convert.ToDateTime(rdr["apply_time"]).Month.ToString() + "." + Convert.ToDateTime(rdr["apply_time"]).Year.ToString();
                        od.applyDate = String.Format("{0: dd.MM.yyyy}", Convert.ToDateTime(rdr["apply_time"]));
                        od.orderSum = rdr["order_summ"].ToString();
                        od.payedSum = rdr["payed_summ"].ToString();
                        od.ordersCount = rdr["order_cnt"].ToString();
                        CCOperatorsReportData.Add(od);
                    }
                }

                fbcon.Close();
            }
        }
    }
}