using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.Http.Headers;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Net;
using System.Web.Configuration;

namespace Fusion.Models.Bringo
{
    public class Bringo
    {
        enum PayType { Account = 1, Sender = 2, Recipient = 3 };
        enum TdsPayType { Sender = 1, Recipient = 2, Payed = 3 };
        enum ContentPayType { Payed = 1, Recipient = 3 };

        public class Restaurant
        {
            public int id { get; set; }
            public string name { get; set; }
            public string Address { get; set; }
            public string phone { get; set; }
        }
        public List<Restaurant> rests = new List<Restaurant>();
        public class OrderInfo
        {
            public _DeliveryOrder Order;
            public _DeliveryOrderResponse Response;
        }
        public List<OrderInfo> Orders = new List<OrderInfo>();

        private string serviceUrl = "";
        private string connectionString;
        private string Login;
        private string Password;
        private int GMTtimeShift = 0;
        private string RestIDs;
        enum Statuses { Draft = 10, New = 20, Taken = 30, CourSender = 40, CourTookDelivery = 50, CourRecipient = 60, SubmittedRecipient = 70, Closed = 80 };

        #region OutInterface
        [DataContract]
        public class _DeliveryOrder
        {
            [DataContract]
            public class Delivery
            {
                public Int64 order_id { get; set; }
                [DataMember]
                public string external_id { get; set; }
                [DataMember]
                public string title { get; set; }
                [DataMember]
                public string description { get; set; }
                [DataMember]
                public Decimal value { get; set; }
                [DataMember]
                public float weight { get; set; }
                [DataMember]
                public int tds_pay_type { get; set; }
                [DataMember]
                public byte partial_buy_out { get; set; }
                //[DataMember]
                //public Decimal goods_price_transit { get; set; }
                [DataMember]
                public Decimal client_price { get; set; }
                [DataMember]
                public int content_pay_type { get; set; }
                [DataMember]
                public int pay_type { get; set; }
                [DataMember]
                public bool is_draft { get; set; }
                [DataMember]
                public int calculate_pup { get; set; }
                //private Decimal _Price;
                //[DataMember]
                //public Decimal price 
                //{
                //    get { return 150; }
                //    set { _Price = value; }
                //}
            }
            [DataContract]
            public class _Point
            {
                [DataContract]
                public class Address
                {
                    [DataMember]
                    public string address_string { get; set; }
                }
                [DataContract]
                public class Time
                {
                    [DataMember]
                    public int start_utc { get; set; }
                    [DataMember]
                    public int end_utc { get; set; }
                }

                [DataMember]
                public string name { get; set; }
                [DataMember]
                public string phone_number { get; set; }
                [DataMember]
                public string description { get; set; }
                [DataMember]
                public int order { get; set; }
                [DataMember]
                public Address address { get; set; }
                [DataMember]
                public Time time { get; set; }

                public _Point()
                {
                    address = new Address();
                    time = new Time();
                }
            }
            [DataMember]
            public Delivery delivery { get; set; }
            [DataMember]
            public _Point[] points { get; set; }
            public _DeliveryOrder()
            {
                points = new _Point[2];
                points[0] = new _Point();
                points[1] = new _Point();
                delivery = new Delivery();
            }
        }
        [DataContract]
        public class _DeliveryPrice
        {
            [DataContract]
            public class Delivery
            {
                [DataMember]
                public float weight { get; set; }
            }
            [DataContract]
            public class _Point
            {
                [DataContract]
                public class Address
                {
                    [DataMember]
                    public string address_string { get; set; }
                }
                [DataContract]
                public class Time
                {
                    [DataMember]
                    public int start_utc { get; set; }
                    [DataMember]
                    public int end_utc { get; set; }
                }

                [DataMember]
                public string name { get; set; }
                public int order { get; set; }
                [DataMember]
                public Address address { get; set; }
                [DataMember]
                public Time time { get; set; }
                public _Point()
                {
                    address = new Address();
                    time = new Time();
                }
            }
            [DataMember]
            public Delivery delivery { get; set; }
            [DataMember]
            public _Point[] points { get; set; }
            public _DeliveryPrice()
            {
                points = new _Point[2];
                points[0] = new _Point();
                points[1] = new _Point();
                delivery = new Delivery();
            }
        }
        [DataContract]
        public class _NearestEnd
        {
            [DataMember]
            public string from_address { get; set; }
            [DataMember]
            public string to_address { get; set; }
            [DataMember]
            public int start_utc { get; set; }

        }
        #endregion

        #region InInterface
        [DataContract]
        public class _AuthData
        {
            public bool IsAuthorized { get; set; }
            private string _sid;
            [DataMember]
            public string sid
            {
                get
                {
                    return _sid;
                }
                set
                {
                    _sid = value;
                    IsAuthorized = true;
                }
            }
            [DataMember]
            public string user_id { get; set; }
        }
        public _AuthData aData = new _AuthData();
        [DataContract]
        public class _PriceResponse
        {
            [DataMember]
            public Decimal price { get; set; }
        }
        [DataContract]
        public class _DeliveryOrderResponse
        {
            [DataMember]
            public string external_id { get; set; }
            [DataContract]
            public class Success
            {
                [DataMember]
                public int id { get; set; }
                [DataMember]
                public int status_id { get; set; }
                [DataMember]
                public Decimal price { get; set; }
                [DataContract]
                public class Item
                {
                    [DataMember]
                    public int external_id { get; set; }
                    [DataMember]
                    public int internal_id { get; set; }

                }
                [DataMember]
                public Item[] items { get; set; }
            }
            [DataMember]
            public Success success { get; set; }
            [DataContract]
            public class Error
            {
                [DataMember]
                public string name { get; set; }
                [DataMember]
                public int code { get; set; }
                [DataMember]
                public string message { get; set; }
                [DataMember]
                public int status { get; set; }
            }
            [DataMember]
            public Error errors { get; set; }
        }
        [DataContract]
        public class _DeliveryInfoResponse
        {
            [DataContract]
            public class Delivery
            {
                [DataMember]
                public int id { get; set; }
                [DataMember]
                public string title { get; set; }
                [DataMember]
                public string description { get; set; }
                [DataMember]
                public string createdate { get; set; }
                [DataMember]
                public int current_status { get; set; }
                [DataMember]
                public int? courier_id { get; set; }
                [DataMember]
                public string courier_name { get; set; }
                [DataMember]
                public string courier_phone_number { get; set; }
                [DataMember]
                public int dlv_code { get; set; }
                [DataMember]
                public Decimal value { get; set; }
                [DataMember]
                public float weight { get; set; }
                [DataMember]
                public int cargo_type { get; set; }
                [DataMember]
                public int pay_type { get; set; }
                [DataMember]
                public Decimal transit_sum { get; set; }
                [DataMember]
                public int transit_type { get; set; }
                [DataMember]
                public int? group_id { get; set; }
                [DataMember]
                public int? client_id { get; set; }
                [DataMember]
                public string client_name { get; set; }
                [DataMember]
                public string client_phone_number { get; set; }
                [DataMember]
                public bool requires_photo { get; set; }
                [DataMember]
                public bool appointed { get; set; }
                [DataMember]
                public int? close_code_id { get; set; }
            }
            [DataContract]
            public class _Point
            {
                [DataMember]
                public string name { get; set; }
                [DataMember]
                public string phone_number { get; set; }
                [DataMember]
                public string description { get; set; }
                [DataMember]
                public int order { get; set; }
                [DataContract]
                public class Address
                {
                    [DataMember]
                    public string address_string { get; set; }
                    [DataMember]
                    public string metro { get; set; }
                }
                [DataMember]
                public Address address { get; set; }
                [DataContract]
                public class Time
                {
                    [DataMember]
                    public int start_utc { get; set; }
                    [DataMember]
                    public int start_local { get; set; }
                    [DataMember]
                    public int end_utc { get; set; }
                    [DataMember]
                    public int end_local { get; set; }
                }
                [DataMember]
                public Time time { get; set; }
            }
            [DataMember]
            public Delivery delivery { get; set; }
            [DataMember]
            public _Point[] points { get; set; }
        }
        [DataContract]
        public class _NearestEndResponse
        {
            [DataMember]
            public int end_utc { get; set; }
        }
        #endregion

        public void GetDeliveryOrders()
        {
            string query = String.Format(@"SELECT FIRST 1 SKIP 0 DISTINCT * 
                FROM DLV_ORDERS o 
                INNER JOIN DLV_RESTAURANTS rest ON o.RESTAURANT_ID = rest.RESTAURANT_ID
                INNER JOIN DLV_ADDRESSES addr ON o.ADDRESS_ID = addr.ADDRESS_ID
                INNER JOIN DLV_STREETS s ON addr.STREET_ID = s.STREET_ID
                INNER JOIN DLV_TOWNS t ON s.TOWN_ID = t.TOWN_ID
                INNER JOIN dlv_clients c ON o.guest_id = c.client_id
                INNER JOIN dlv_client_phones p ON o.guest_id = p.client_id
                WHERE o.WORK_TIME >= '{0}' AND o.DLV_TYPE = 0 AND o.STATUS IN (1, 2) AND o.ORDER_ID NOT IN (SELECT DISTINCT ORDER_ID FROM BRINGO_DLV) AND o.RESTAURANT_ID IN ({1})", DateTime.Today.ToShortDateString(), RestIDs);

            FbConnection con = new FbConnection(connectionString);
            FbCommand cmd = new FbCommand(query, con);

            con.Open();

            FbDataReader rdr = cmd.ExecuteReader();

            foreach (var r in rdr)
            {
                long order_id = Convert.ToInt64(rdr["ORDER_ID"]);
                string RK7GUID = rdr["RK7GUID"].ToString();
                string adv_info = "";

                if (rdr["adv_info"] != DBNull.Value)
                    adv_info = Encoding.GetEncoding(1251).GetString((byte[])rdr["adv_info"]);

                Restaurant rest = rests.Find(p => p.id == Convert.ToInt64(rdr["RESTAURANT_ID"]));

                _DeliveryOrder delivery = new _DeliveryOrder();
                delivery.delivery.order_id = order_id;
                delivery.delivery.external_id = RK7GUID;
                delivery.delivery.title = "Заказ " + order_id.ToString() + ". Токио Доставка. (" + rdr["ORDER_NUM"] + ")";
                delivery.delivery.description = adv_info;
                delivery.delivery.value = Convert.ToDecimal(rdr["SUMM_WCD"]);
                delivery.delivery.weight = 1;
                delivery.delivery.tds_pay_type = (int)TdsPayType.Payed;
                delivery.delivery.partial_buy_out = 0;
                //delivery.delivery.goods_price_transit = Convert.ToDecimal(rdr["SUMM_WCD"]);
                delivery.delivery.client_price = 0;
                delivery.delivery.content_pay_type = (int)ContentPayType.Recipient;
                delivery.delivery.pay_type = (int)PayType.Account;
                delivery.delivery.is_draft = false;
                delivery.delivery.calculate_pup = 0;

                //Отправитель
                delivery.points[0].name = rdr["RESTAURANT_NAME"].ToString();
                delivery.points[0].order = 1;
                delivery.points[0].phone_number = rest.phone;
                delivery.points[0].description = rdr["RESTAURANT_NAME"].ToString();
                delivery.points[0].address.address_string = rest.Address;
                delivery.points[0].time.start_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(15).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                delivery.points[0].time.end_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(60).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                //Получатель
                delivery.points[1].name = rdr["F_NAME"].ToString();
                delivery.points[1].order = 2;
                //Номер телефона полный!
                delivery.points[1].phone_number = "+7" + rdr["phone_number"].ToString();
                delivery.points[1].description = adv_info + " \r\n, кв " + rdr["FLAT"];
                delivery.points[1].address.address_string = "000000, г " + rdr["TOWN_NAME"].ToString() + ", " + rdr["STREET_NAME"].ToString() + ", д " + rdr["HOUSE"].ToString();
                delivery.points[1].time.start_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(60).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                delivery.points[1].time.end_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(90).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                Orders.Add(new OrderInfo() { Order = delivery });
            }

            rdr.Close();
            con.Close();
        }
        public void SendOrder(long id)
        {
            string query = String.Format(@"SELECT FIRST 1 SKIP 0 DISTINCT * 
                FROM DLV_ORDERS o 
                INNER JOIN DLV_RESTAURANTS rest ON o.RESTAURANT_ID = rest.RESTAURANT_ID
                INNER JOIN DLV_ADDRESSES addr ON o.ADDRESS_ID = addr.ADDRESS_ID
                INNER JOIN DLV_STREETS s ON addr.STREET_ID = s.STREET_ID
                INNER JOIN DLV_TOWNS t ON s.TOWN_ID = t.TOWN_ID
                INNER JOIN dlv_clients c ON o.guest_id = c.client_id
                INNER JOIN dlv_client_phones p ON o.guest_id = p.client_id
                WHERE o.WORK_TIME >= '{0}' AND o.DLV_TYPE = 0 AND o.STATUS IN (1, 2) AND o.ORDER_ID NOT IN (SELECT DISTINCT ORDER_ID FROM BRINGO_DLV) AND o.RESTAURANT_ID IN ({1}) AND o.order_id = {2}", DateTime.Today.ToShortDateString(), RestIDs, id);

            FbConnection con = new FbConnection(connectionString);
            FbCommand cmd = new FbCommand(query, con);

            con.Open();

            FbDataReader rdr = cmd.ExecuteReader();

            foreach (var r in rdr)
            {
                long order_id = Convert.ToInt64(rdr["ORDER_ID"]);
                string RK7GUID = rdr["RK7GUID"].ToString();
                string adv_info = "";

                if (rdr["adv_info"] != DBNull.Value)
                    adv_info = Encoding.GetEncoding(1251).GetString((byte[])rdr["adv_info"]);

                Restaurant rest = rests.Find(p => p.id == Convert.ToInt64(rdr["RESTAURANT_ID"]));

                _DeliveryOrder delivery = new _DeliveryOrder();
                delivery.delivery.order_id = order_id;
                delivery.delivery.external_id = RK7GUID;
                delivery.delivery.title = "Заказ " + order_id.ToString() + ". Токио Доставка. (" + rdr["ORDER_NUM"] + ")";
                delivery.delivery.description = adv_info;
                delivery.delivery.value = Convert.ToDecimal(rdr["SUMM_WCD"]);
                delivery.delivery.weight = 1;
                delivery.delivery.tds_pay_type = (int)TdsPayType.Payed;
                delivery.delivery.partial_buy_out = 0;
                //delivery.delivery.goods_price_transit = Convert.ToDecimal(rdr["SUMM_WCD"]);
                delivery.delivery.client_price = 0;
                delivery.delivery.content_pay_type = (int)ContentPayType.Recipient;
                delivery.delivery.pay_type = (int)PayType.Account;
                delivery.delivery.is_draft = false;
                delivery.delivery.calculate_pup = 0;

                //Отправитель
                delivery.points[0].name = rdr["RESTAURANT_NAME"].ToString();
                delivery.points[0].order = 1;
                delivery.points[0].phone_number = rest.phone;
                delivery.points[0].description = rdr["RESTAURANT_NAME"].ToString();
                delivery.points[0].address.address_string = rest.Address;
                delivery.points[0].time.start_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(15).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                delivery.points[0].time.end_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(60).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                //Получатель
                delivery.points[1].name = rdr["F_NAME"].ToString();
                delivery.points[1].order = 2;
                //Номер телефона полный!
                delivery.points[1].phone_number = "+7" + rdr["phone_number"].ToString();
                delivery.points[1].description = adv_info + " \r\n, кв " + rdr["FLAT"];
                delivery.points[1].address.address_string = "000000, г " + rdr["TOWN_NAME"].ToString() + ", " + rdr["STREET_NAME"].ToString() + ", д " + rdr["HOUSE"].ToString();
                delivery.points[1].time.start_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(60).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                delivery.points[1].time.end_utc = (Int32)(((DateTime)rdr["work_time"]).AddHours(-10).AddMinutes(90).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                //_DeliveryOrderResponse response = BatchCreate(delivery);
                _DeliveryOrderResponse response = new _DeliveryOrderResponse();
                response.success = new _DeliveryOrderResponse.Success();
                response.success.id = 1234568;
                response.success.status_id = 20;
                Orders.Add(new OrderInfo() { Order = delivery, Response = response });
            }

            rdr.Close();
            con.Close();

            /*for (int i = 0; i < Orders.Count; i++)
            {
                if (Orders[i].Response != null && Orders[i].Response.success != null)
                {
                    string q = "INSERT INTO BRINGO_DLV VALUES(" + Orders[i].Order.delivery.order_id + ", " + Orders[i].Response.success.id.ToString() + ", '" + Orders[i].Response.external_id + "', null)";
                    FbCommand com = new FbCommand(q, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string q = "INSERT INTO BRINGO_DLV VALUES(" + Orders[i].Order.delivery.order_id + ", null, '" + Orders[i].Order.delivery.external_id + "', '" + Orders[i].Response.errors.message + "')";
                    FbCommand com = new FbCommand(q, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }*/
        }
        public bool Authorize()
        {
            string response = "";

            try
            {
                HttpContent content = new StringContent("{\"login\":\"" + Login + "\", \"password\":\"" + Password + "\"}", Encoding.UTF8);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-auth-token", aData.sid);
                var result = client.PostAsync(serviceUrl + "login", content).Result;

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    response = result.Content.ReadAsStringAsync().Result;

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(_AuthData));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(response));
                    aData = (_AuthData)serializer.ReadObject(stream);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public _DeliveryOrderResponse BatchCreate(_DeliveryOrder delivery)
        {
            _DeliveryOrderResponse[] response = new _DeliveryOrderResponse[1];
            string action = WebConfigurationManager.AppSettings["CreateAction"].ToString();
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(_DeliveryOrder));
            ser.WriteObject(stream1, delivery);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string s = sr.ReadToEnd();

            if (action == "delivery/batch-create")
            {
                s = "[" + s + "]";
            }

            var result = POST(s, action);
            s = result.Content.ReadAsStringAsync().Result;

            if (action == "delivery/batch-create")
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(_DeliveryOrderResponse[]));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
                response = (_DeliveryOrderResponse[])serializer.ReadObject(stream);
            }
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(_DeliveryOrderResponse));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
                response[0] = (_DeliveryOrderResponse)serializer.ReadObject(stream);
            }

            return response[0];
        }
        public void GetPrice(_DeliveryPrice deliveryPrice)
        {
            string action = "delivery/get-price";
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(_DeliveryPrice));
            ser.WriteObject(stream1, deliveryPrice);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string s = sr.ReadToEnd();
            var result = POST("[" + s + "]", action);
        }
        public _DeliveryInfoResponse GetInfo(int id)
        {
            _DeliveryInfoResponse response;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-auth-token", aData.sid);
            var result = client.GetAsync(serviceUrl + "/delivery/get-info?id=" + id.ToString()).Result;
            string s = result.Content.ReadAsStringAsync().Result;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(_DeliveryInfoResponse));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(s));
            response = (_DeliveryInfoResponse)serializer.ReadObject(stream);
            return response;
        }
        public void GetList(Int32? createtime_from, Int32? createtime_to, int? status_id, int? close_code_id, int? courier_id, string sort_field, string sort_order, int? limit, int? offset)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-auth-token", aData.sid);
            var result = client.GetAsync(serviceUrl + "/delivery/get-list/?limit=100").Result;
            string response = result.Content.ReadAsStringAsync().Result;
        }
        public void NearestEnd(_NearestEnd nearestEnd)
        {
            string action = "delivery/nearest-end";
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(_NearestEnd));
            ser.WriteObject(stream1, nearestEnd);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string s = sr.ReadToEnd();
            var result = POST("[" + s + "]", action);
        }
        private HttpResponseMessage POST(string data, string path)
        {
            try
            {
                HttpContent content = new StringContent(data, Encoding.UTF8);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-auth-token", aData.sid);
                HttpResponseMessage result = client.PostAsync(serviceUrl + path, content).Result;
                return result;
                //response = result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Bringo()
        {
            Restaurant r = new Restaurant();
            r.id = 1;
            r.name = "Токио Хоум";
            r.Address = "000000, г Владивосток, проспект 100 лет Владивостоку, д 50А";
            r.phone = "+74232707707";
            rests.Add(r);

            r = new Restaurant();
            r.id = 1002128;
            r.name = "Токио (ост. Гайдамак)";
            r.Address = "000000, г Владивосток, ул Светланская, 183В";
            r.phone = "+74232750750";
            rests.Add(r);

            r = new Restaurant();
            r.id = 1002257;
            r.name = "Токио Kawaii";
            r.Address = "000000, г Владивосток, ул Семеновская, 7В";
            r.phone = "+74232447777";
            rests.Add(r);

            r = new Restaurant();
            r.id = 1003172;
            r.name = "Токио (Первая речка)";
            r.Address = "000000, г Владивосток, проспект Острякова, 8";
            r.phone = "+74232227777";
            rests.Add(r);

            r = new Restaurant();
            r.id = 1005896;
            r.name = "Токио в г. Уссурийск";
            r.Address = "000000, г Уссурийск, ул Комсомольская, 28";
            r.phone = "+74234346464";
            rests.Add(r);

            r = new Restaurant();
            r.id = 1008008;
            r.name = "Токио (ост. Дальзавод)";
            r.Address = "000000, г Владивосток, ул Светланская, 121";
            r.phone = "+74232367777";
            rests.Add(r);

            r = new Restaurant();
            r.id = 1009190;
            r.name = "Токио в г. Находка";
            r.Address = "000000, г Находка, проспект Мира, 2";
            r.phone = "+74236617777";
            rests.Add(r);

            serviceUrl = WebConfigurationManager.AppSettings["ServiceURL"].ToString();
            connectionString = WebConfigurationManager.ConnectionStrings["DLVConnectionString"].ToString();
            Login = WebConfigurationManager.AppSettings["BringoLogin"].ToString();
            Password = WebConfigurationManager.AppSettings["BringoPassword"].ToString();
            GMTtimeShift = Convert.ToInt32(WebConfigurationManager.AppSettings["GMTtimeShift"]);
            RestIDs = WebConfigurationManager.AppSettings["RestIDs"].ToString();
        }
    }
}