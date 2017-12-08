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
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Net.Sockets;
using System.Threading;

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

        public class SODresponse
        {
            public int OrderID { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public class Message
        {
            public string To { get; set; }
            public string Phone { get; set; }
        }

        public class OrderInfo
        {
            public class GuestInfo
            {
                public int id { get; set; }
                [Display(Name = "Логин")]
                public string Login { get; set; }
                [Display(Name = "Имя")]
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
                [Display(Name = "Цена")]
                public Decimal Price { get; set; }
                [Display(Name = "Валюта")]
                public string CurrencyName { get; set; }
                [Display(Name = "Количество")]
                public Decimal Quantity { get; set; }
                [Display(Name = "Название")]
                public string ProductName { get; set; }
                [Display(Name = "Код RK7")]
                public string RKCode { get; set; }
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
            public int DiscountPrc { get; set; }

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

            CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();
            RK7Entities dbRK7 = new RK7Entities();

            public void GetOrder(int id, string UserName)
            {
                DiscountPrc = 0;
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

                #region GetPromocode
                var coupon = this.Properties.FirstOrDefault(p => p.OrderPropsId == 19);

                if (coupon != null)
                {
                    com.CommandText = String.Format(@"select biebp.value as discount from b_iblock_element bieb INNER JOIN b_iblock_element_property biebp ON bieb.ID = biebp.IBLOCK_ELEMENT_ID AND biebp.IBLOCK_PROPERTY_ID = 37 where bieb.IBLOCK_ID = 11 and bieb.name = '{0}';", coupon.Value);
                    rdr = com.ExecuteReader();

                    if (rdr.HasRows)
                        while (rdr.Read())
                            if (rdr["discount"] != DBNull.Value)
                                DiscountPrc = Convert.ToInt32(rdr["discount"]);

                    rdr.Close();
                }
                #endregion

                #region FillItems
                com.CommandText = String.Format(@"select bsb.*, CAST(bie.XML_ID as SIGNED), bieb.VALUE as RKCODE from b_sale_basket bsb LEFT JOIN b_iblock_element bie ON bsb.PRODUCT_ID = bie.ID 
LEFT JOIN b_iblock_element_property bieb ON cast(bie.XML_ID AS SIGNED) = bieb.IBLOCK_ELEMENT_ID and bieb.IBLOCK_PROPERTY_ID IN (76, 162, 161, 160, 159, 158, 153, 155, 151)
where bsb.ORDER_ID = {0}", id);
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

                        if (rdr["RKCODE"] != DBNull.Value)
                            oii.RKCode = Convert.ToString(rdr["RKCODE"]);
                        else
                            oii.RKCode = "";

                        this.Items.Add(oii);
                    }
                }

                rdr.Close();
                #endregion

                con.Close();
            }
            public SODresponse SendOrderToDelivery(string UserName, string extSourceID)
            {
                SODresponse result = new SODresponse() { OrderID = this.id };
                List<RKCRM.Holder.AddressInfo> addressInfo = null;

                #region Prepare
                string comment = "", f_name = "", l_name = "", phone = "";
                int guestCount = 0, orderType = 3;
                DateTime deliveryTime = DateTime.Now;

                int defaultCookTime = Convert.ToInt32(WebConfigurationManager.AppSettings["DefaultCookTime"]);
                int defaultWayTime = Convert.ToInt32(WebConfigurationManager.AppSettings["DefaultWayTime"]);

                if (this.DeliveryName == "Забрать самостоятельно")
                    orderType = 2;

                string[] nameParts = this.Guest.Name.Trim().Split(' ');
                f_name = nameParts[0];

                var propsAddress = this.Properties.FirstOrDefault(p => p.OrderPropsId == 1);
                var propsPhone = this.Properties.FirstOrDefault(p => p.OrderPropsId == 2);
                var propsEmail = this.Properties.FirstOrDefault(p => p.OrderPropsId == 3);
                var propsDeliveryTime = this.Properties.FirstOrDefault(p => p.OrderPropsId == 4);
                var propsName = this.Properties.FirstOrDefault(p => p.OrderPropsId == 7);
                var propsGuestCount = this.Properties.FirstOrDefault(p => p.OrderPropsId == 8);
                var propsSdacha = this.Properties.FirstOrDefault(p => p.OrderPropsId == 9);
                var propsCity = this.Properties.FirstOrDefault(p => p.OrderPropsId == 17);
                var propsPromoCode = this.Properties.FirstOrDefault(p => p.OrderPropsId == 19);
                var propsRestId = this.Properties.FirstOrDefault(p => p.OrderPropsId == 22);
                var propsCityId = this.Properties.FirstOrDefault(p => p.OrderPropsId == 24);
                var propsStreetId = this.Properties.FirstOrDefault(p => p.OrderPropsId == 25);
                var propsHouse = this.Properties.FirstOrDefault(p => p.OrderPropsId == 26);
                var propsBuilding = this.Properties.FirstOrDefault(p => p.OrderPropsId == 27);
                var propsEntry = this.Properties.FirstOrDefault(p => p.OrderPropsId == 28);
                var propsFloor = this.Properties.FirstOrDefault(p => p.OrderPropsId == 29);
                var propsAppt = this.Properties.FirstOrDefault(p => p.OrderPropsId == 30);
                var propsEntryCode = this.Properties.FirstOrDefault(p => p.OrderPropsId == 31);

                //Для определения, откуда заказ, с ебучего приложения или с сайта
                var propsSource = this.Properties.FirstOrDefault(p => p.OrderPropsId == 18);

                phone = propsPhone.Value.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");

                string ordercategoryID = "0", restID = "0";

                if (orderType == 2)
                {
                    switch (propsRestId.Value)
                    {
                        case "1002128":
                            // Гайдамак
                            ordercategoryID = "1002069";
                            restID = "1002128";
                            break;
                        case "1003172":
                            // Киото
                            ordercategoryID = "1002070";
                            restID = "1003172";
                            break;
                        case "1":
                            // Хоум
                            ordercategoryID = "1002068";
                            restID = "1";
                            break;
                        case "1002257":
                            // Каваи
                            ordercategoryID = "1003468";
                            restID = "1002257";
                            break;
                        case "1008008":
                            // Мауро
                            ordercategoryID = "1010099";
                            restID = "1008008";
                            break;
                        case "1005896":
                            // Уссурийск
                            ordercategoryID = "1005921";
                            restID = "1005896";
                            break;
                        case "1009190":
                            // Находка 1 этаж
                            ordercategoryID = "1010100";
                            restID = "1009190";
                            break;
                        default:
                            // Хуй пойми чо
                            ordercategoryID = "1002069";
                            restID = "1002128";
                            break;
                    }
                }

                if (orderType == 3)
                {
                    addressInfo = new List<RKCRM.Holder.AddressInfo>();

                    if (propsAddress == null)
                    {
                        result.Success = false;
                        result.Message = "Не удалось определить адрес доставки. Пожалуйста, проверьте корректность данных.";
                        return result;
                    }

                    if (propsCityId == null && propsCity == null)
                    {
                        result.Success = false;
                        result.Message = "Не удалось определить город доставки. Пожалуйста, проверьте корректность данных.";
                        return result;
                    }
                    else
                    {
                        if (propsCityId == null && propsCity != null)
                        {
                            var cityInfo = db.CARD_CITIES.FirstOrDefault(p => p.NAME.ToLower() == propsCity.Value && p.DELETED == 0 && p.COUNTRY_ID == 1);

                            if (cityInfo == null)
                            {
                                result.Success = false;
                                result.Message = "Не удалось определить город доставки. Пожалуйста, проверьте корректность данных.";
                                return result;
                            }

                            propsCityId = new OrderPropertyInfo() { Value = cityInfo.CITY_ID.ToString() };
                        }
                    }

                    //Определение улицы
                    if (propsStreetId == null || String.IsNullOrEmpty(propsStreetId.Value))
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"улица:(?<val>.*?),");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString().Trim();
                            long cityId = Convert.ToInt64(propsCityId.Value);
                            var streetInfo = db.CARD_STREETS.FirstOrDefault(p => p.NAME.ToLower() == sv.ToLower() && p.CITY_ID == cityId && p.DELETED == 0);

                            if (streetInfo == null)
                            {
                                result.Success = false;
                                result.Message = "Не удалось определить улицу. Пожалуйста, проверьте корректность данных.";
                                return result;
                            }

                            propsStreetId = new OrderPropertyInfo() { Value = streetInfo.STREET_ID.ToString() };
                        }
                    }

                    //Определение дома
                    if (propsHouse == null)
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"дом:(?<val>.*?),");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString();

                            propsHouse = new OrderPropertyInfo() { Value = sv.Trim() };
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Не удалось определить номер дома. Пожалуйста, проверьте корректность данных.";
                            return result;
                        }
                    }

                    //Определение строения
                    if (propsBuilding == null || String.IsNullOrEmpty(propsBuilding.Value))
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"строение:(?<val>.*?),");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString();

                            propsBuilding = new OrderPropertyInfo() { Value = sv.Trim() };
                        }
                        else
                        {
                            propsBuilding = new OrderPropertyInfo() { Value = "" };
                        }
                    }

                    //Определение подъезда
                    if (propsEntry == null || String.IsNullOrEmpty(propsEntry.Value))
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"подъезд:(?<val>.*?),");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString();

                            propsEntry = new OrderPropertyInfo() { Value = sv.Trim() };
                        }
                        else
                        {
                            propsEntry = new OrderPropertyInfo() { Value = "" };
                        }
                    }

                    //Определение домофона
                    if (propsEntryCode == null || String.IsNullOrEmpty(propsEntryCode.Value))
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"домофон:(?<val>.*?),");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString();

                            propsEntryCode = new OrderPropertyInfo() { Value = sv.Trim() };
                        }
                        else
                        {
                            propsEntryCode = new OrderPropertyInfo() { Value = "" };
                        }
                    }

                    //Определение этажа
                    if (propsFloor == null || String.IsNullOrEmpty(propsFloor.Value))
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"этаж:(?<val>.*?),");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString();

                            propsFloor = new OrderPropertyInfo() { Value = sv.Trim() };
                        }
                        else
                        {
                            propsFloor = new OrderPropertyInfo() { Value = "" };
                        }
                    }

                    //Определение квартиры
                    if (propsAppt == null || String.IsNullOrEmpty(propsAppt.Value))
                    {
                        Match mt = Regex.Match(propsAddress.Value.ToLower(), @"квартира\/офис:(?<val>.*?)$");

                        if (mt.Success)
                        {
                            string sv = mt.Groups["val"].ToString();

                            propsAppt = new OrderPropertyInfo() { Value = sv.Trim() };
                        }
                        else
                        {
                            propsAppt = new OrderPropertyInfo() { Value = "" };
                        }
                    }

                    addressInfo.Add(new RKCRM.Holder.AddressInfo()
                    {
                        Type_ID = "250",
                        Country_ID = "1",
                        ZIP = "",
                        Region = "",
                        City_ID = propsCityId.Value,
                        Street_ID = propsStreetId.Value,
                        House = propsHouse.Value,
                        Building = propsBuilding.Value,
                        Entry = propsEntry.Value,
                        Floor = propsFloor.Value,
                        Apartments = propsAppt.Value,
                        Entry_Code = propsEntryCode.Value,
                        DopInfo = "",
                        Deleted = "False",
                        LAT = "000.000000000",
                        LNG = "000.000000000",
                    });
                }

                if (propsGuestCount != null)
                {
                    Int32.TryParse(propsGuestCount.Value, out guestCount);

                    if (guestCount == 0)
                        guestCount = 1;
                }

                if (propsName != null)
                {
                    nameParts = propsName.Value.Trim().Split(' ');

                    if (propsSource == null)
                    {
                        f_name = nameParts[0].Trim();

                        if (nameParts.Length > 1)
                            l_name = nameParts[1].Trim();
                    }
                    else
                    {
                        f_name = nameParts[0].Trim();

                        if (nameParts.Length > 1)
                        {
                            l_name = nameParts[0].Trim();
                            f_name = nameParts[1].Trim();
                        }
                    }
                }

                if (this.Payed)
                    comment += String.Format("{0} ОПЛАЧЕН ", this.id);

                if (this.PaySystem.id == 5)
                    comment += "ТЕРМИНАЛ ";

                if (propsDeliveryTime == null || propsDeliveryTime.Value.Trim() == "Ближайшее")
                {
                    if (orderType == 2)
                    {
                        DateTime dt = DateTime.Now;
                        deliveryTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).AddMinutes(defaultCookTime);
                    }
                    else
                    {
                        if (orderType == 3)
                        {
                            DateTime dt = DateTime.Now;
                            deliveryTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second).AddMinutes(defaultCookTime + defaultWayTime);
                        }
                    }
                }
                else
                {
                    deliveryTime = DateTime.Parse(propsDeliveryTime.Value.Trim().Replace(" в ", " "));
                    comment += deliveryTime.ToString("dd.MM.yyyy HH:mm") + " ";
                }

                if (propsSdacha != null && propsSdacha.Value.Trim() != "")
                    comment += String.Format("{0}р ", propsSdacha.Value);

                comment += this.Comment.Trim();

                string contactPhone = String.Format(@"<Contact><Type_ID>254</Type_ID><Value>{0}</Value><Dispatch>True</Dispatch></Contact>", phone);

                List<RKCRM.Holder> holders = RKCRM.SearchHoldersByPhone(phone);

                if (holders != null && holders.Count > 0)
                {
                    if (addressInfo == null)
                        addressInfo = new List<RKCRM.Holder.AddressInfo>();

                    holders[0].Addresses = addressInfo;

                    if (orderType == 2)
                    {
                        addressInfo.AddRange(holders[0].Holders_Addresses.Addresses);

                        for (int i = 0; i < addressInfo.Count; i++)
                            addressInfo[i].Deleted = "True";
                    }

                    holders[0].Holders_Addresses = null;

                    RKCRM.EditHoldersModel editHolders = new RKCRM.EditHoldersModel();
                    holders[0].F_Name = f_name;
                    holders[0].L_Name = l_name;
                    holders[0].M_Name = "";
                    holders[0].Full_Name = "";
                    editHolders.Holder = holders[0];
                    holders[0] = editHolders.EditHolder(UserName);
                }
                else
                {
                    RKCRM.AddHoldersModel addHolders = new RKCRM.AddHoldersModel();
                    addHolders.Holder = new RKCRM.Holder()
                    {
                        Group_ID = "4",
                        F_Name = f_name,
                        L_Name = l_name,
                        BirthD = new DateTime(1900, 1, 1),
                        Contacts = new List<RKCRM.Holder.ContactInfo>(),
                        Addresses = addressInfo
                    };

                    addHolders.Holder.Contacts.Add(new RKCRM.Holder.ContactInfo()
                    {
                        Type_ID = "254",
                        Value = phone,
                        Dispatch = "True"
                    });

                    if (addHolders.AddHolder())
                        holders = RKCRM.SearchHoldersByPhone(phone);
                    else
                    {
                        result.Message = "[AddHolders] " + addHolders.LastResult;
                        result.Success = false;
                        return result;
                    }
                }

                #endregion

                #region RK7Block
                string addressId = "0";
                bool addressSpecified = false;

                if (addressInfo != null && addressInfo.Count > 0)
                {
                    RKCRM.Holder.AddressInfo ai = addressInfo.FirstOrDefault(p => String.IsNullOrEmpty(p.Address_ID));

                    if(ai != null)
                    {
                        ai = holders[0].Holders_Addresses.Addresses.FirstOrDefault(p =>
                            p.Country_ID == ai.Country_ID
                            && p.City_ID == ai.City_ID
                            && p.Street_ID == ai.Street_ID
                            && p.House == ai.House
                            );

                        addressId = ai.Address_ID;
                        addressSpecified = true;
                    }
                }

                var empid = dbRK7.EMPLOYEES.FirstOrDefault(p => p.AUTHUSERNAME == UserName);

                if (empid == null)
                    empid = new EMPLOYEES() { SIFR = 9001 };


                RK7_qryCreateOrder.RK7Query createOrder = new RK7_qryCreateOrder.RK7Query();
                createOrder.RK7CMD = new RK7_qryCreateOrder.RK7QueryRK7CMD();
                RK7_qryCreateOrder.RK7QueryRK7CMDOrder order = new RK7_qryCreateOrder.RK7QueryRK7CMDOrder()
                {
                    Table = new RK7_qryCreateOrder.refItem() { code = "60" },
                    OrderType = new RK7_qryCreateOrder.refItem() { code = orderType.ToString() },
                    GuestType = new RK7_qryCreateOrder.refItem() { id = "1" },
                    OrderCategory = new RK7_qryCreateOrder.refItem() { id = ordercategoryID },
                    persistentComment = comment,
                    Creator = new RK7_qryCreateOrder.refItem() { id = empid.SIFR.ToString() },
                    Waiter = new RK7_qryCreateOrder.refItem() { id = empid.SIFR.ToString() },
                    nonPersistentComment = "Internet",
                    extSource = extSourceID,
                    extID = "256"
                };

                List<RK7_qryCreateOrder.guest_item> guests = new List<RK7_qryCreateOrder.guest_item>();


                guests.Add(new RK7_qryCreateOrder.guest_item()
                {
                    clientIDSpecified = true,
                    clientID = Convert.ToInt64(holders[0].Holder_ID),
                    addressIDSpecified = addressSpecified,
                    addressID = Convert.ToInt64(addressId)
                });

                for (int i = 1; i < guestCount; i++)
                    guests.Add(new RK7_qryCreateOrder.guest_item()
                    {
                        guestLabel = (i + 1).ToString()
                    });

                order.Guests = new RK7_qryCreateOrder.Guests_Item()
                {
                    count = guestCount,
                    Guest = guests.ToArray()
                };

                createOrder.RK7CMD.Order = new RK7_qryCreateOrder.RK7QueryRK7CMDOrder[1] { order };

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(RK7_qryCreateOrder.RK7Query));
                StringWriter stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, createOrder);
                string rkQuery = stringWriter.ToString();

                string s = RK7APIModels.HttpPost(rkQuery);

                string status = "";
                string guid = "";

                Match m = Regex.Match(s, @"guid=\""(?<val>.*?)\""");

                if (m.Success)
                    guid = m.Groups["val"].Value;

                m = Regex.Match(s, @"Status=\""(?<val>.*?)\""");

                if (m.Success)
                    status = m.Groups["val"].Value;

                if (status != "Ok")
                {
                    result.Message = "[CreateOrder] " + s;
                    result.Success = false;
                    return result;
                }

                Dlv_qryDeliveryUpdateStatus.RK7Query updateStatus = new Dlv_qryDeliveryUpdateStatus.RK7Query()
                {
                    RK7CMD = new Dlv_qryDeliveryUpdateStatus.RK7QueryRK7CMD()
                    {
                        Order = new Dlv_qryDeliveryUpdateStatus.orderElement() { guid = guid }
                    }
                };

                if (!String.IsNullOrEmpty(restID) && restID != "0")
                    updateStatus.RK7CMD.Restaurant = new Dlv_qryDeliveryUpdateStatus.refItem() { id = restID };

                updateStatus.RK7CMD.DeliveryBlock = new Dlv_qryDeliveryUpdateStatus.deliveryBlock()
                {
                    deliveryTimeSpecified = true,
                    deliveryTime = deliveryTime,
                    minCookTimeSpecified = true,
                    minCookTime = (new DateTime(1899, 12, 30)).AddMinutes(defaultCookTime)
                };

                xmlSerializer = new XmlSerializer(typeof(Dlv_qryDeliveryUpdateStatus.RK7Query));
                stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, updateStatus);
                rkQuery = stringWriter.ToString();

                s = RK7APIModels.HttpPost(rkQuery);

                m = Regex.Match(s, @"Status=\""(?<val>.*?)\""");

                if (m.Success)
                    status = m.Groups["val"].Value;

                if (status != "Ok")
                {
                    result.Message = "[SaveOrder] " + s;
                    result.Success = false;
                    return result;
                }

                RK7_qrySaveOrder.RK7Query saveOrder = new RK7_qrySaveOrder.RK7Query()
                {
                    RK7CMD = new RK7_qrySaveOrder.RK7QueryRK7CMD()
                    {
                        dontcheckLicense = true,
                        deferred = true,
                        dontcheckLicenseSpecified = true
                    }
                };
                saveOrder.RK7CMD.Order = new RK7_qrySaveOrder.orderElement() { guid = guid };

                List<RK7_qrySaveOrder.checkItem> items = new List<RK7_qrySaveOrder.checkItem>();

                foreach (var dish in this.Items)
                    if (dish.RKCode != "")
                        items.Add(new RK7_qrySaveOrder.dishItem() { code = dish.RKCode, quantity = Convert.ToInt32(dish.Quantity * 1000) });

                if (this.PriceDelivery > 0 && orderType == 3)
                    items.Add(new RK7_qrySaveOrder.dishItem() { code = "322", quantity = 1000 });

                if (this.DiscountPrc == 5 && orderType != 2)
                    items.Add(new RK7_qrySaveOrder.discountItem() { id = "1011218", code = "1000012" });

                if (this.DiscountPrc == 10 && orderType != 2)
                    items.Add(new RK7_qrySaveOrder.discountItem() { id = "1011251", code = "1000014" });


                saveOrder.RK7CMD.Session = new RK7_qrySaveOrder.SessionItem[1] { new RK7_qrySaveOrder.SessionItem() { Items = items.ToArray() } };

                xmlSerializer = new XmlSerializer(typeof(RK7_qrySaveOrder.RK7Query));
                stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, saveOrder);
                rkQuery = stringWriter.ToString();

                s = RK7APIModels.HttpPost(rkQuery);

                m = Regex.Match(s, @"Status=\""(?<val>.*?)\""");

                if (m.Success)
                    status = m.Groups["val"].Value;

                if (status != "Ok")
                {
                    result.Message = "[SaveOrder] " + s;
                    result.Success = false;
                    return result;
                }
                #endregion

                result.Success = true;
                result.Message = "Заказ успешно сохранен";
                return result;
            }
        }

        public List<OrderInfo> Orders = new List<OrderInfo>();

        public void GetOrderList(string UserName, string[] filter_status)
        {
            string statuses = "'C','F','N','O'";

            if (filter_status != null)
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
    public class QuasiPhone
    {
        private StreamWriter swSender;
        private TcpClient tcpServer;
        private IPAddress ipAddr;

        public void Send(string UserName, string Message)
        {
            InitializeConnection(UserName, Message);
        }

        private void InitializeConnection(string UserName, string Message)
        {
            ipAddr = IPAddress.Parse(WebConfigurationManager.AppSettings["QuasiServer"].ToString());
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddr, 1986);

            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine("vega [" + UserName + "]");
            swSender.Flush();

            swSender.WriteLine(Message);
            swSender.Flush();

            swSender.Close();
            tcpServer.Close();
        }
    }
}