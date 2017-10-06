using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Fusion.Models
{
    public class RK7APIModels
    {
        public static string HttpPost(string PostData)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://10.1.0.22:8095/Delivery/SendOrder");

            var data = Encoding.UTF8.GetBytes(PostData);

            request.Method = "POST";
            request.ContentType = "application/text";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        [XmlRoot("RK7QueryResult")]
        public class RespGetRefsDataMenuItems
        {
            [XmlRoot("RK7Reference")]
            public class RK7Reference
            {
                public class MenuItem
                {
                    [XmlAttribute]
                    public int Ident { get; set; }
                    [XmlAttribute]
                    public int SourceIdent { get; set; }
                    [XmlAttribute]
                    public string GUIDString { get; set; }
                    [XmlAttribute]
                    public int Code { get; set; }
                    [XmlAttribute]
                    public string Name { get; set; }
                    [XmlAttribute]
                    public string Status { get; set; }
                    [XmlAttribute]
                    public string ShortName { get; set; }
                    [XmlAttribute]
                    public string CategPath { get; set; }
                }
                [XmlArray("Items"), XmlArrayItem("Item")]
                public List<MenuItem> MenuItems { get; set; }
            }

            [XmlElement("RK7Reference")]
            public RK7Reference rk7ref { get; set; }

            public string Serialize()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(RespGetRefsDataMenuItems));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }
            public RespGetRefsDataMenuItems Deserialize(string xml)
            {
                var xmlSerializer = new XmlSerializer(typeof(RespGetRefsDataMenuItems));
                var stringReader = new StringReader(xml);
                return (RespGetRefsDataMenuItems)xmlSerializer.Deserialize(stringReader);
            }
            public string GetMenuItemsFromRK()
            { 
                string query = @"<?xml version=""1.0"" encoding=""utf-8""?>
                    <RK7Query>
                    <RK7CMD CMD=""GetRefDataFiltered"" RefName = ""MenuItems"">
   
                    </RK7CMD>
                    </RK7Query>";
                return HttpPost(query);
            }
        }

        [XmlRoot("RK7Query")]
        public class RK7Query
        {
            [XmlElement("RK7CMD")]
            public CreateOrder asef { get; set; }
            public class CreateOrder
            {
                [XmlAttribute("CMD")]
                public string CMD = "CreateOrder";
                [XmlElement("Order")]
                public OrderInfo Order { get; set; }
                public class OrderInfo
                {
                    [XmlAttribute("persistentComment")]
                    public string persistentComment { get; set; }
                    [XmlAttribute("nonPersistentComment")]
                    public string nonPersistentComment { get; set; }
                    [XmlAttribute("extSource")]
                    public string extSource { get; set; }
                    [XmlAttribute("extID")]
                    public string extID { get; set; }
                    [XmlElement("Table")]
                    public OrderAttribute Table { get; set; }
                    [XmlElement("OrderType")]
                    public OrderAttribute OrderType { get; set; }
                    [XmlElement("GuestType")]
                    public OrderAttribute GuestType { get; set; }
                    [XmlElement("OrderCategory")]
                    public OrderAttribute OrderCategory { get; set; }

                    public class OrderAttribute 
                    {
                        [XmlAttribute("code")]
                        public string Code { get; set; }
                        [XmlAttribute("id")]
                        public string id { get; set; }
                    }

                    public class Guest
                    { 

                    }
                }
            }
        }
    }

    [XmlRoot("RK7QueryResult")]
    public class RK7QueryResult
    {
        public static string HttpPost(string PostData, string ip, string port)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://10.1.0.22:8095/Delivery/Query");

            var data = Encoding.UTF8.GetBytes(PostData);

            request.Method = "POST";
            request.ContentType = "application/text";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        [XmlRoot("LayoutResult")]
        public class SystemBalanceReport
        {
            public class Item
            {
                public class Rhrow
                {
                    [XmlElement("rhcol")]
                    public List<string> rhcol { get; set; }
                }
                [XmlElement("rhrow")]
                public Rhrow rhrow { get; set; }

                public class Rbrow
                {
                    [XmlElement("SystemBalance")]
                    public Item SystemBalance { get; set; }

                    [XmlElement("Discounts")]
                    public Item Discounts { get; set; }

                    [XmlElement("MonWaiterBalance")]
                    public Item MonWaiterBalance { get; set; }

                    [XmlElement("rbcol")]
                    public List<string> rbcol { get; set; }
                }

                [XmlElement("rbrow")]
                public List<Rbrow> rbrow { get; set; }

                public class Rfrow
                {
                    [XmlElement("rfcol")]
                    public List<string> rfcol { get; set; }
                }
                [XmlElement("rfrow")]
                public Rfrow rfrow { get; set; }
            }
            [XmlElement("BandRepTitle")]
            public Item BandRepTitle { get; set; }
            [XmlElement("SystemBalance")]
            public Item SystemBalance { get; set; }
            [XmlElement("Discounts")]
            public Item Discounts { get; set; }
            [XmlElement("DishCategory")]
            public Item DishCategory { get; set; }
            [XmlElement("MonWaiterBalance")]
            public Item MonWaiterBalance { get; set; }
        }

        [XmlElement("LayoutResult")]
        public SystemBalanceReport systemBalanceReport { get; set; }
        public void serialize()
        {
        }

        public RK7QueryResult Deserialize(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(RK7QueryResult));
            var stringReader = new StringReader(xml);
            return (RK7QueryResult)xmlSerializer.Deserialize(stringReader);
        }

        public string GetSystemBalance(string ip, int port)
        {
            return HttpPost(@"<?xml version=""1.0"" encoding=""utf-8""?> 
<RK7Query>
  <RK7CMD CMD=""GetDocByLayout"" TextReport=""0"" LayoutFilters="""" DataSourceParams="""">
    <Layout id=""10771"" />
  </RK7CMD>
</RK7Query>", ip, port.ToString());
        }
    }
}