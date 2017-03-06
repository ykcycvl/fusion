using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Web.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace Fusion.Models
{
    public class InternetOrders
    {
        public int BitrixUserID = 0;
        private static MySqlConnection GetConnection()
        {
            MySqlConnection dbConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["SiteConnectionString"].ConnectionString);
            dbConnection.Open();
            return dbConnection;
        }

        private static SqlConnection GetVegaConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["VegaConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public class OrderInfo
        {
            public class GuestInfo
            {
                public int id { get; set; }
                [Display(Name="Логин")]
                public string Login { get; set; }
                [Display(Name="Имя")]
                public string Name { get; set; }
                [Display(Name = "email")]
                public string email { get; set; }
                [Display(Name = "Телефон")]
                public string phone { get; set; }
            }
            public class OrderItemInfo
            {
                public int id { get; set; }
                public int productId { get; set; }
                [Display(Name="Цена")]
                public Decimal Price { get; set; }
                [Display(Name="Валюта")]
                public string CurrencyName { get; set; }
                [Display(Name="Количество")]
                public Decimal Quantity { get; set; }
                [Display(Name="Название")]
                public string ProductName { get; set; }

            }
            public class EmployeeInfo
            {
                public int id { get; set; }
                public string Login { get; set; }
                public string LastName { get; set; }
                public string Name { get; set; }
            }
            public class OrderPropertyInfo
            {
                public int id { get; set; }
                public int OrderId { get; set; }
                public int OrderPropsId { get; set; }
                public string Name { get; set; }
                public string Value { get; set; }
            }
            public class PaySystemInfo
            {
                public int id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
            }

            public GuestInfo Guest { get; set; }
            public List<OrderItemInfo> Items { get; set; }
            public EmployeeInfo Employee { get; set; }
            public List<OrderPropertyInfo> Properties { get; set; }

            [Display(Name = "Номер заказа")]
            public int id { get; set; }

            public bool Canceled { get; set; }
            public DateTime DateCanceled { get; set; }
            public EmployeeInfo EmployeeCanceled { get; set; }
            public string ReasonCanceled { get; set; }
            public string Status { get; set; }
            [Display(Name = "Дата изменения статуса")]
            public DateTime DateStatus { get; set; }
            public EmployeeInfo EmployeeStatus { get; set; }
            public Decimal PriceDelivery { get; set; }
            public Decimal Price { get; set; }
            public string Currency { get; set; }
            public Decimal Discount { get; set; }
            public PaySystemInfo PaySystem { get; set; }
            public EmployeeInfo EmployeeLocked { get; set; }
            public DateTime DateLocked { get; set; }
            [Display(Name = "Создан")]
            public DateTime DateInsert { get; set; }
            [Display(Name = "Последнее изменение")]
            public DateTime DateUpdate { get; set; }
            [Display(Name = "Комментарий")]
            public string Comment { get; set; }
            [Display(Name = "Оплачен")]
            public bool Payed { get; set; }
            [Display(Name = "Время оплаты")]
            public DateTime DatePayed { get; set; }
            [Display(Name = "Сообщение ПС")]
            public string PSMessage { get; set; }
            public string DeliveryName { get; set; }
            public int BitrixUserID { get; set; }
            public string Address { get; set; }

            public void SetStatus(char StatusId)
            {
                if (id == 0)
                    return;

                if (BitrixUserID != 0)
                    if (EmployeeLocked == null || EmployeeLocked.id == BitrixUserID || DateLocked < DateTime.Now.AddMinutes(-30))
                    {
                        MySqlConnection con = GetConnection();
                        MySqlCommand com = new MySqlCommand(String.Format("UPDATE b_sale_order SET STATUS_ID = '{3}', DATE_STATUS = '{1}', EMP_STATUS_ID = {2} WHERE ID = {0}", id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BitrixUserID, StatusId), con);

                        try
                        {
                            com.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            throw ex;
                        }

                        con.Close();
                    }
            }

            public void GetOrder(int id, string UserName)
            {
                BitrixUserID = 0;
                MySqlConnection con = GetConnection();
                SqlConnection vegaCon = GetVegaConnection();

                SqlCommand cmd = new SqlCommand(String.Format(@"SELECT * FROM bitrixUser WHERE adlogin = '{0}'", UserName), vegaCon);
                SqlDataReader vegaRdr = cmd.ExecuteReader();

                if (vegaRdr.HasRows)
                {
                    while (vegaRdr.Read())
                    {
                        BitrixUserID = Convert.ToInt32(vegaRdr["bitrixId"]);
                    }
                }

                vegaRdr.Close();
                vegaCon.Close();

                #region query
                MySqlCommand com = new MySqlCommand(String.Format(@"
SELECT 
  bso.ID as OrderID,
  bso.DATE_INSERT AS OrderDateInsert,
  guest.ID AS GuestID,
  guest.NAME AS GuestName,
  guest.LAST_NAME AS GuestLastName,
  bssl.NAME AS OrderStatus,
  bso.PAYED AS Payed,
  bso.PRICE AS OrderPrice,
  bso.DATE_UPDATE AS OrderDateUpdate,
  bso.PRICE_DELIVERY AS OrderPriceDelivery,
  guest.EMAIL AS GuestEmail,
  guest.PERSONAL_PHONE AS GuestPhone,
  bsps.ID AS PaySystemID,
  bsps.NAME AS PayTypeName,
  bsps.DESCRIPTION AS PaySystemDescription,
  bso.PS_STATUS_MESSAGE AS PSMessage,
  bso.DATE_PAYED AS OrderDatePayed,
  bso.USER_DESCRIPTION AS OrderGuestComment,
  empLocked.ID AS LockEmpID,
  empLocked.LOGIN AS LockEmpLogin,
  empLocked.LAST_NAME AS LockEmpLastName,
  empLocked.NAME AS LockEmpName,
  bso.DATE_LOCK AS DateLock,
  bsd.NAME AS DeliveryName,
  bso.DISCOUNT_VALUE AS Discount
FROM 
  b_sale_order bso
  INNER JOIN b_sale_delivery bsd ON bso.DELIVERY_ID = bsd.ID
  LEFT JOIN b_user guest ON bso.USER_ID = guest.ID
  LEFT JOIN b_sale_pay_system bsps ON bso.PAY_SYSTEM_ID = bsps.ID
  LEFT JOIN b_user empLocked ON bso.LOCKED_BY = empLocked.ID
  LEFT JOIN b_sale_status_lang bssl ON bso.STATUS_ID = bssl.STATUS_ID AND bssl.LID = 'ru'
WHERE bso.ID = {0}", id), con);
                #endregion

                #region FillMainInfo
                MySqlDataReader rdr = com.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        this.id = Convert.ToInt32(rdr["OrderID"]);
                        DateInsert = Convert.ToDateTime(rdr["OrderDateInsert"]);

                        if (rdr["GuestID"] != DBNull.Value)
                        {
                            Guest = new OrderInfo.GuestInfo();
                            Guest.id = Convert.ToInt32(rdr["GuestID"]);

                            if (rdr["GuestName"] != DBNull.Value)
                                Guest.Name = rdr["GuestName"].ToString();

                            if (rdr["GuestEmail"] != DBNull.Value)
                                Guest.email = Convert.ToString(rdr["GuestEmail"]);

                            if (rdr["GuestPhone"] != DBNull.Value)
                                Guest.phone = Convert.ToString(rdr["GuestPhone"]);
                        }

                        Discount = Convert.ToDecimal(rdr["Discount"]);

                        if (rdr["OrderStatus"] != DBNull.Value)
                            Status = Convert.ToString(rdr["OrderStatus"]);

                        if (rdr["Payed"] != DBNull.Value)
                            if (rdr["Payed"].ToString() == "N")
                                Payed = false;
                            else
                                Payed = true;

                        if (rdr["OrderPrice"] != DBNull.Value)
                            Price = Convert.ToDecimal(rdr["OrderPrice"]);

                        if (rdr["OrderDateUpdate"] != DBNull.Value)
                            DateUpdate = Convert.ToDateTime(rdr["OrderDateUpdate"]);

                        if (rdr["OrderPriceDelivery"] != DBNull.Value)
                            PriceDelivery = Convert.ToDecimal(rdr["OrderPriceDelivery"]);

                        if (rdr["PaySystemID"] != DBNull.Value)
                        {
                            PaySystem = new OrderInfo.PaySystemInfo();
                            PaySystem.id = Convert.ToInt32(rdr["PaySystemID"]);

                            if (rdr["PayTypeName"] != DBNull.Value)
                                PaySystem.Name = Convert.ToString(rdr["PayTypeName"]);

                            if (rdr["PaySystemDescription"] != DBNull.Value)
                                PaySystem.Description = Convert.ToString(rdr["PaySystemDescription"]);
                        }

                        if (rdr["PSMessage"] != DBNull.Value)
                            PSMessage = Convert.ToString(rdr["PSMessage"]);

                        if (rdr["OrderDatePayed"] != DBNull.Value)
                            DatePayed = Convert.ToDateTime(rdr["OrderDatePayed"]);

                        if (rdr["OrderGuestComment"] != DBNull.Value)
                            Comment = Convert.ToString(rdr["OrderGuestComment"]);


                        if (rdr["LockEmpID"] != DBNull.Value)
                        {
                            EmployeeLocked = new OrderInfo.EmployeeInfo();
                            EmployeeLocked.id = Convert.ToInt32(rdr["LockEmpID"]);

                            if (rdr["LockEmpLogin"] != DBNull.Value)
                            {
                                EmployeeLocked.Login = Convert.ToString(rdr["LockEmpLogin"]);
                            }

                            if (rdr["LockEmpLastName"] != DBNull.Value)
                            {
                                EmployeeLocked.LastName = Convert.ToString(rdr["LockEmpLastName"]);
                            }

                            if (rdr["LockEmpName"] != DBNull.Value)
                            {
                                EmployeeLocked.Name = Convert.ToString(rdr["LockEmpName"]);
                            }
                        }

                        if (rdr["DateLock"] != DBNull.Value)
                        {
                            DateLocked = Convert.ToDateTime(rdr["DateLock"]);
                        }

                        if (rdr["DeliveryName"] != DBNull.Value)
                        {
                            DeliveryName = Convert.ToString(rdr["DeliveryName"]);
                        }
                    }
                }

                rdr.Close();

                if (BitrixUserID != 0)
                    if (EmployeeLocked == null || EmployeeLocked.id == BitrixUserID || DateLocked < DateTime.Now.AddMinutes(-30))
                    {
                        com.CommandText = String.Format(@"UPDATE b_sale_order SET LOCKED_BY = {0}, DATE_LOCK = '{1}' WHERE ID = {2}", BitrixUserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id);
                        com.ExecuteNonQuery();
                    }

                #endregion

                #region FillOrderProps
                com.CommandText = String.Format(@"SELECT * FROM b_sale_order_props_value WHERE order_id = {0}", id);
                rdr = com.ExecuteReader();

                if (rdr.HasRows)
                {
                    this.Properties = new List<OrderPropertyInfo>();

                    while (rdr.Read())
                    {
                        OrderInfo.OrderPropertyInfo opi = new OrderPropertyInfo();
                        opi.id = Convert.ToInt32(rdr["id"]);
                        opi.OrderId = Convert.ToInt32(rdr["ORDER_ID"]);
                        opi.OrderPropsId = Convert.ToInt32(rdr["ORDER_PROPS_ID"]);
                        opi.Name = Convert.ToString(rdr["NAME"]);

                        if (rdr["VALUE"] != DBNull.Value)
                            opi.Value = Convert.ToString(rdr["VALUE"]);

                        this.Properties.Add(opi);
                    }
                }

                rdr.Close();
                #endregion

                #region FillItems
                com.CommandText = String.Format(@"select * FROM b_sale_basket bsb where bsb.ORDER_ID = {0}", id);
                rdr = com.ExecuteReader();

                if (rdr.HasRows)
                {
                    this.Items = new List<OrderItemInfo>();

                    while (rdr.Read())
                    {
                        OrderInfo.OrderItemInfo oii = new OrderItemInfo();
                        oii.id = Convert.ToInt32(rdr["id"]);
                        oii.productId = Convert.ToInt32(rdr["PRODUCT_ID"]);
                        oii.Price = Convert.ToDecimal(rdr["PRICE"]);
                        oii.Quantity = Convert.ToDecimal(rdr["QUANTITY"]);
                        oii.ProductName = Convert.ToString(rdr["NAME"]);
                        this.Items.Add(oii);
                    }
                }

                rdr.Close();
                #endregion

                con.Close();
            }
        }

        public List<OrderInfo> Orders = new List<OrderInfo>();

        public void GetOrderList(string UserName, string[] filter_status)
        {
            string statuses = "'C','F','N','O'";

            if(filter_status != null)
            {
                statuses = "";

                for (int i = 0; i < filter_status.Length; i++)
                    filter_status[i] = "'" + filter_status[i] + "'";

                statuses = string.Join(",", filter_status);
            }

            BitrixUserID = 0;
            SqlConnection vegaCon = GetVegaConnection();

            SqlCommand cmd = new SqlCommand(String.Format(@"SELECT * FROM bitrixUser WHERE adlogin = '{0}'", UserName), vegaCon);
            SqlDataReader vegaRdr = cmd.ExecuteReader();

            if (vegaRdr.HasRows)
            {
                while (vegaRdr.Read())
                {
                    BitrixUserID = Convert.ToInt32(vegaRdr["bitrixId"]);
                }
            }

            vegaRdr.Close();
            vegaCon.Close();

            MySqlConnection con = GetConnection();
            #region query
            MySqlCommand com = new MySqlCommand(String.Format(@"
SELECT 
  bso.ID as OrderID,
  bso.DATE_INSERT AS OrderDateInsert,
  bso.USER_ID AS GuestID,
  GuestInfo.VALUE AS GuestName,
  bssl.NAME AS OrderStatus,
  bso.PAYED AS Payed,
  bso.PRICE AS OrderPrice,
  bso.DATE_UPDATE AS OrderDateUpdate,
  bso.PRICE_DELIVERY AS OrderPriceDelivery,
  GuestEmail.VALUE AS GuestEmail,
  bsps.ID AS PaySystemID,
  bsps.NAME AS PayTypeName,
  bsps.DESCRIPTION AS PaySystemDescription,
  bso.PS_STATUS_MESSAGE AS PSMessage,
  bso.DATE_PAYED AS OrderDatePayed,
  bso.USER_DESCRIPTION AS OrderGuestComment,
  empLocked.ID AS LockEmpID,
  empLocked.LOGIN AS LockEmpLogin,
  empLocked.LAST_NAME AS LockEmpLastName,
  empLocked.NAME AS LockEmpName,
  bso.DATE_LOCK AS DateLock,
  addrInfo.VALUE AS Address,
  contactInfo.VALUE AS GuestPhone
FROM 
  b_sale_order bso
  LEFT JOIN b_sale_pay_system bsps ON bso.PAY_SYSTEM_ID = bsps.ID
  LEFT JOIN b_user empLocked ON bso.LOCKED_BY = empLocked.ID
  LEFT JOIN b_sale_status_lang bssl ON bso.STATUS_ID = bssl.STATUS_ID AND bssl.LID = 'ru'
  LEFT JOIN b_sale_order_props_value as addrInfo ON bso.ID = addrInfo.ORDER_ID AND addrInfo.ORDER_PROPS_ID = 1
  LEFT JOIN b_sale_order_props_value as contactInfo ON bso.ID = contactInfo.ORDER_ID AND contactInfo.ORDER_PROPS_ID = 2
  LEFT JOIN b_sale_order_props_value as GuestInfo ON bso.ID = GuestInfo.ORDER_ID AND GuestInfo.ORDER_PROPS_ID = 7
  LEFT JOIN b_sale_order_props_value as GuestEmail ON bso.ID = GuestEmail.ORDER_ID AND GuestEmail.ORDER_PROPS_ID = 3
WHERE bso.DATE_INSERT > '{0}' AND bso.STATUS_ID IN ({1})
ORDER BY bso.DATE_INSERT DESC", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:SS"), statuses), con);
            #endregion

            MySqlDataReader rdr = com.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    OrderInfo oi = new OrderInfo();
                    oi.id = Convert.ToInt32(rdr["OrderID"]);
                    oi.DateInsert = Convert.ToDateTime(rdr["OrderDateInsert"]);

                    if (rdr["GuestID"] != DBNull.Value)
                    {
                        oi.Guest = new OrderInfo.GuestInfo();
                        oi.Guest.id = Convert.ToInt32(rdr["GuestID"]);

                        if (rdr["GuestName"] != DBNull.Value)
                            oi.Guest.Name = rdr["GuestName"].ToString();

                        if (rdr["GuestEmail"] != DBNull.Value)
                            oi.Guest.email = Convert.ToString(rdr["GuestEmail"]);

                        if (rdr["GuestPhone"] != DBNull.Value)
                        {
                            oi.Guest.phone = Convert.ToString(rdr["GuestPhone"]);
                            oi.Guest.phone = oi.Guest.phone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                        }
                    }

                    if (rdr["OrderStatus"] != DBNull.Value)
                        oi.Status = Convert.ToString(rdr["OrderStatus"]);

                    if (rdr["Payed"] != DBNull.Value)
                        if (rdr["Payed"].ToString() == "N")
                            oi.Payed = false;
                        else
                            oi.Payed = true;

                    if (rdr["OrderPrice"] != DBNull.Value)
                        oi.Price = Convert.ToDecimal(rdr["OrderPrice"]);

                    if (rdr["OrderDateUpdate"] != DBNull.Value)
                        oi.DateUpdate = Convert.ToDateTime(rdr["OrderDateUpdate"]);

                    if (rdr["OrderPriceDelivery"] != DBNull.Value)
                        oi.PriceDelivery = Convert.ToDecimal(rdr["OrderPriceDelivery"]);

                    if (rdr["PaySystemID"] != DBNull.Value)
                    {
                        oi.PaySystem = new OrderInfo.PaySystemInfo();
                        oi.PaySystem.id = Convert.ToInt32(rdr["PaySystemID"]);

                        if (rdr["PayTypeName"] != DBNull.Value)
                            oi.PaySystem.Name = Convert.ToString(rdr["PayTypeName"]);

                        if (rdr["PaySystemDescription"] != DBNull.Value)
                            oi.PaySystem.Description = Convert.ToString(rdr["PaySystemDescription"]);
                    }

                    if (rdr["PSMessage"] != DBNull.Value)
                        oi.PSMessage = Convert.ToString(rdr["PSMessage"]);

                    if (rdr["OrderDatePayed"] != DBNull.Value)
                        oi.DatePayed = Convert.ToDateTime(rdr["OrderDatePayed"]);

                    if (rdr["OrderGuestComment"] != DBNull.Value)
                        oi.Comment = Convert.ToString(rdr["OrderGuestComment"]);


                    if (rdr["LockEmpID"] != DBNull.Value)
                    { 
                        oi.EmployeeLocked = new OrderInfo.EmployeeInfo();
                        oi.EmployeeLocked.id = Convert.ToInt32(rdr["LockEmpID"]);

                        if (rdr["LockEmpLogin"] != DBNull.Value)
                        {
                            oi.EmployeeLocked.Login = Convert.ToString(rdr["LockEmpLogin"]);
                        }

                        if (rdr["LockEmpLastName"] != DBNull.Value)
                        {
                            oi.EmployeeLocked.LastName = Convert.ToString(rdr["LockEmpLastName"]);
                        }

                        if (rdr["LockEmpName"] != DBNull.Value)
                        {
                            oi.EmployeeLocked.Name = Convert.ToString(rdr["LockEmpName"]);
                        }
                    }

                    if (rdr["DateLock"] != DBNull.Value)
                    {
                        oi.DateLocked = Convert.ToDateTime(rdr["DateLock"]);
                    }

                    if (rdr["Address"] != DBNull.Value)
                        oi.Address = Convert.ToString(rdr["Address"]);

                    Orders.Add(oi);
                }
            }

            rdr.Close();
            con.Close();
        }
    }
}