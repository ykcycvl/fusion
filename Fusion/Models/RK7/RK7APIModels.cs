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
    }
}