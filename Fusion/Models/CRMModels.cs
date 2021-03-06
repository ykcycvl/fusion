﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Net.Mail;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Transactions;

namespace Fusion.Models
{
    public class RKCRM
    {
        static IPAddress ip = IPAddress.Parse("10.1.0.90");
        static string Terminal_Type = "239487KJT3asf";
        static string Global_Type = "";
        public class CRMResponse
        {
            public string Message = "";
            public string MessageType = "";
        }
        public static CRMResponse Query(string query, string MessageID)
        {
            query = query.Trim();
            byte[] queryBytes = Encoding.UTF8.GetBytes(query);
            CRMResponse response = new CRMResponse();

            query = String.Format(@"Message-ID: {4}
Message-Type: Request
Time: {2}
Terminal-Type: {3}
Content-Length: {0}

{1}", queryBytes.Length, query, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Terminal_Type, MessageID);

            TcpClient client = new TcpClient();
            client.SendTimeout = 120000;
            client.ReceiveTimeout = 240000;
            client.ReceiveBufferSize = 5242880;

            client.Connect(ip, 9191);
            NetworkStream tcpStream = client.GetStream();
            byte[] sendBytes = Encoding.UTF8.GetBytes(query);
            tcpStream.Write(sendBytes, 0, sendBytes.Length);
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int bytesRead = tcpStream.Read(bytes, 0, client.ReceiveBufferSize);
            client.Close();

            string returnData = Encoding.UTF8.GetString(bytes).Replace("\0", "");

            Match m = Regex.Match(returnData, "^Message-Type:(?<val>.*?)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            if (m.Success)
            {
                response.MessageType = m.Groups["val"].ToString().Trim();
            }

            m = Regex.Match(returnData, "Content-Length:.*?\r\n(?<val>.*)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (m.Success)
            {
                response.Message = m.Groups["val"].ToString().Trim();
            }

            return response;
        }
        public class APIMessage
        {
            [XmlAttribute("Reserved_A")]
            public string Reserved_A { get; set; }
            [XmlAttribute("Action")]
            public string Action { get; set; }

            [XmlAttribute("Terminal_Type")]
            public string Terminal_Type { get; set; }

            [XmlAttribute("Global_Type")]
            public string Global_Type { get; set; }
            [XmlElement("Include")]
            public string Include { get; set; }
            [XmlIgnore]
            public string LastResult = "";
            public APIMessage()
            {
                Terminal_Type = "239487KJT3asf";
                Global_Type = "";
            }
        }
        public static List<Holder> SearchHoldersByPhone(string phone)
        {
            string query = String.Format(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<Message Action=""Search holders"" Terminal_Type=""{0}"" Global_Type=""{1}"">
<Include>Account, Holder_Card, Holder_Contact, Holder_Address</Include>
<Count>25</Count>
<Index>1</Index>
<Item Mode=""Clear""/>
<Item Mode=""Add"">
<Contacts>
	<Phone Value=""{2}"" IsNumber=""True""/>
</Contacts>
</Item>
<Item Mode=""Clear""/>
</Message>", Terminal_Type, Global_Type, phone);
            CRMResponse r = Query(query, "1");
            Holder m = new Holder();

            XmlSerializer deserializer = new XmlSerializer(typeof(List<Holder>), new XmlRootAttribute("Holders"));
            var stringReader = new StringReader(r.Message);
            List<Holder> Holders = (List<Holder>)deserializer.Deserialize(stringReader);

            return (Holders);
        }

        public static List<Holder> SearchHoldersByMail(string email)
        {
            string query = String.Format(@"<?xml version=""1.0"" standalone=""yes"" ?>
<Message Action=""Search holders"" Terminal_Type=""{0}"" Global_Type=""{1}"">
	<Include>Account, Holder_Card, Holder_Contact, Holder_Address</Include>
	<Count>25</Count>
	<Index>1</Index>
	<Item Mode=""Clear""/>
	<Item Mode=""Add"">
	<Contacts>
		<EMail Value=""{2}"" />
	</Contacts>
	</Item>
</Message>", Terminal_Type, Global_Type, email);
            CRMResponse r = Query(query, "1");
            Holder m = new Holder();

            XmlSerializer deserializer = new XmlSerializer(typeof(List<Holder>), new XmlRootAttribute("Holders"));
            var stringReader = new StringReader(r.Message);
            List<Holder> Holders = (List<Holder>)deserializer.Deserialize(stringReader);

            return (Holders);
        }
        [XmlRoot("Holder")]
        public class Holder
        {
            [Display(Name = "Удален")]
            [XmlElement("Deleted")]
            public string Deleted { get; set; }

            [Display(Name = "ID группы")]
            [XmlElement("Group_ID")]
            public string Group_ID { get; set; }
            public IEnumerable<SelectListItem> GroupSelectList
            {
                get
                {
                    List<SelectListItem> Groups = new List<SelectListItem>();
                    Groups.Add(new SelectListItem() { Text = "Море плюсов", Value = "45" });
                    Groups.Add(new SelectListItem() { Text = "SaintFish", Value = "99" });
                    return Groups;
                }
            }

            [Display(Name = "Группа")]
            [XmlElement("Group_Name")]
            public string Group_Name { get; set; }

            [Display(Name = "ID подразделения")]
            [XmlElement("Division_ID")]
            public string Division_ID { get; set; }

            [Display(Name = "Подразделение")]
            [XmlElement("Division_Name")]
            public string Division_Name { get; set; }

            [Display(Name = "ID владельца")]
            [XmlElement("Holder_ID")]
            public string Holder_ID { get; set; }

            [Display(Name = "ИНН")]
            [XmlElement("INN")]
            public string INN { get; set; }

            [Display(Name = "Внешний код")]
            [XmlElement("External_Code")]
            public string External_Code { get; set; }

            [XmlElement("Unpay_Type_ID")]
            public string Unpay_Type_ID { get; set; }

            [XmlElement("Unpay_Type_Name")]
            public string Unpay_Type_Name { get; set; }

            [Display(Name = "Фамилия")]
            [XmlElement("L_Name")]
            public string L_Name { get; set; }

            [Display(Name = "Имя")]
            [XmlElement("F_Name")]
            [Required]
            public string F_Name { get; set; }

            [Display(Name = "Отчество")]
            [XmlElement("M_Name")]
            public string M_Name { get; set; }

            [Display(Name = "Ф.И.О.")]
            [XmlElement("Full_Name")]
            public string Full_Name { get; set; }

            [XmlIgnore]
            private DateTime _Birth { get; set; }
            [XmlIgnore]
            [Display(Name = "Дата рождения")]
            [Required]
            public DateTime BirthD
            {
                get
                {
                    return _Birth;
                }
                set
                {
                    _Birth = value;
                }
            }
            [XmlElement("Birth")]
            public string Birth
            {
                get
                {
                    return _Birth.ToString("yyyy-MM-dd");
                }
                set
                {
                    try
                    {
                        _Birth = DateTime.Parse(value);
                    }
                    catch
                    {
                        _Birth = DateTime.Parse("01.01.1980");
                    }
                }
            }

            [Display(Name = "Доп. инфо")]
            [XmlElement("Remarks")]
            public string Remarks { get; set; }

            [Display(Name = "Карты")]
            [XmlArray("Cards"), XmlArrayItem("Card")]
            public List<CardInfo> Cards { get; set; }

            [Display(Name = "Счета")]
            [XmlArray("Accounts"), XmlArrayItem("Account")]
            public List<AccountInfo> Accounts { get; set; }

            [Display(Name = "Контакты")]
            [XmlArray("Contacts"), XmlArrayItem("Contact")]
            public List<ContactInfo> Contacts { get; set; }

            [Display(Name = "Адреса")]
            [XmlArray("Addresses"), XmlArrayItem("Address")]
            public List<AddressInfo> Addresses { get; set; }

            [XmlElement("Holders_Cards")]
            public CRMHolderCardsInfo Holders_Cards { get; set; }

            [XmlElement("Holders_Contacts")]
            public CRMHolderContactsInfo Holders_Contacts { get; set; }

            [XmlElement("Holders_Addresses")]
            public CRMHolderAddressesInfo Holders_Addresses { get; set; }

            [XmlArray("Holders_Properties"), XmlArrayItem("Holder_Property")]
            public List<HolderProperty> Holders_Properties { get; set; }

            public class CardInfo
            {
                #region SelectLists
                [XmlIgnore]
                public IEnumerable<SelectListItem> StatusSelectList
                {
                    get
                    {
                        List<SelectListItem> Statuses = new List<SelectListItem>();
                        Statuses.Add(new SelectListItem() { Text = "Активна", Value = "Active" });
                        Statuses.Add(new SelectListItem() { Text = "Заблокирована", Value = "Blocked" });
                        Statuses.Add(new SelectListItem() { Text = "Неактивна", Value = "Inactive" });

                        if (Status == null)
                            Status = "";

                        SelectListItem sli = Statuses.FirstOrDefault(p => p.Value == Status);

                        if (sli != null)
                            sli.Selected = true;

                        return Statuses;
                    }
                }
                #endregion
                [Display(Name = "ID группы")]
                [XmlElement("Group_ID")]
                public string Group_ID { get; set; }

                [Display(Name = "Код карты")]
                [XmlElement("Card_Code")]
                public string Card_Code { get; set; }

                [Display(Name = "Информация о магнитной ленте")]
                [XmlElement("Carrier_Data")]
                public string Carrier_Data { get; set; }

                [Display(Name = "Пароль")]
                [XmlElement("Password")]
                public string Password { get; set; }

                [XmlIgnore]
                private DateTime _Offered;
                [XmlIgnore]
                public DateTime OfferedD
                {
                    get
                    {
                        return _Offered;
                    }
                    set
                    {
                        _Offered = value;
                    }
                }

                [Display(Name = "Дата выдачи")]
                [XmlElement("Offered")]
                public string Offered
                {
                    get
                    {
                        return _Offered.ToString("yyyy-MM-dd");
                    }
                    set
                    {
                        _Offered = DateTime.Parse(value);
                    }
                }

                [XmlIgnore]
                private DateTime _Expired;
                [XmlIgnore]
                public DateTime ExpiredD
                {
                    get
                    {
                        return _Expired;
                    }
                    set
                    {
                        _Expired = value;
                    }
                }
                [Display(Name = "Дата окончания")]
                [XmlElement("Expired")]
                public string Expired
                {
                    get
                    {
                        return _Expired.ToString("yyyy-MM-dd");
                    }
                    set
                    {
                        _Expired = DateTime.Parse(value);
                    }
                }

                [Display(Name = "Подтверждения менеджера")]
                [XmlElement("Is_Confirm_Manager")]
                public string Is_Confirm_Manager { get; set; }
                [XmlElement("Is_Virtual_Card")]
                public string Is_Virtual_Card { get; set; }
                [XmlElement("Status")]
                public string Status { get; set; }
                [XmlElement("Group_Name")]
                public string Group_Name { get; set; }
                [XmlElement("Holder_ID")]
                public string Holder_ID { get; set; }
                [XmlElement("Owner_ID")]
                public string Owner_ID { get; set; }
                [XmlElement("Verification")]
                public string Verification { get; set; }
                public CRMResponse Save(string UserName)
                {
                    string cardXML = "";

                    if (Status == "Active")
                    {
                        cardXML = String.Format(@"<?xml version=""1.0"" standalone=""yes"" ?>
                        <Message Action=""Card active"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"" Unit_ID=""1"" User_ID=""{2}"">
                        <Card_Code>{0}</Card_Code>
                        <Expired>{1}</Expired>
                        </Message>", Card_Code, Expired, 1);
                    }
                    else
                        if (Status == "Inactive")
                        {
                            cardXML = String.Format(@"<?xml version=""1.0"" standalone=""yes"" ?>
                            <Message Action=""Card inactive"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"" Unit_ID=""1"" User_ID=""{1}"">
                            <Card_Code>{0}</Card_Code>
                            </Message>", Card_Code, 1);
                        }
                        else
                            if (Status == "Blocked")
                            {
                                cardXML = String.Format(@"<?xml version=""1.0"" standalone=""yes"" ?>
                                <Message Action=""Card block"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"" Unit_ID=""1"" User_ID=""{2}"">
                                <Card_Code>{0}</Card_Code>
                                <Remarks>{1}</Remarks>
                                </Message>", Card_Code, "Заблокирована пользователем " + UserName, 1);
                            }

                    return RKCRM.Query(cardXML, "1");
                }
            }
            public class AccountInfo
            {
                [Display(Name = "Номер счета")]
                [XmlElement("Account_Number")]
                public string Account_Number { get; set; }

                [Display(Name = "Код типа счета")]
                [XmlElement("Account_Type_ID")]
                public string Account_Type_ID { get; set; }

                [Display(Name = "Автоматический межуровневый переход")]
                [XmlElement("Auto_Change_Levels")]
                public string Auto_Change_Levels { get; set; }

                [Display(Name = "Глубина кредита")]
                [XmlElement("Credit_Depth")]
                public string Credit_Depth { get; set; }

                [Display(Name = "Код уровня счета")]
                [XmlElement("Account_Level_ID")]
                public string Account_Level_ID { get; set; }
                [XmlElement("Holder_ID")]
                public string Holder_ID { get; set; }
                [XmlElement("Status")]
                public string Status { get; set; }
                [XmlElement("Account_Class")]
                public string Account_Class { get; set; }
                [XmlElement("Account_Type_Name")]
                public string Account_Type_Name { get; set; }
                [XmlElement("Base_Rate")]
                public string Base_Rate { get; set; }
                [XmlElement("Create")]
                public string Create { get; set; }
                [XmlElement("Balance")]
                public string Balance { get; set; }
            }
            public class ContactInfo
            {
                [Display(Name = "ID")]
                [XmlElement("Contact_ID")]
                public string Contact_ID { get; set; }

                [Display(Name = "Тип")]
                [XmlElement("Type_ID")]
                [Required]
                public string Type_ID { get; set; }

                [Display(Name = "Значение")]
                [XmlElement("Value")]
                [Required]
                public string Value { get; set; }

                [Display(Name = "Внешний идентификатор")]
                [XmlElement("Value_Ext")]
                public string Value_Ext { get; set; }

                [Display(Name = "Примечание")]
                [XmlElement("Notes")]
                public string Notes { get; set; }

                [Display(Name = "Разрешение на рассылку")]
                [XmlElement("Dispatch")]
                public string Dispatch { get; set; }

                [Display(Name = "Удален")]
                [XmlElement("Deleted")]
                public string Deleted { get; set; }
                [XmlElement("Is_Virtual_Card")]
                public string Is_Virtual_Card { get; set; }
                [XmlElement("Type_Name")]
                public string Type_Name { get; set; }
                [XmlIgnore]
                public IEnumerable<SelectListItem> ContactTypesSelectList
                {
                    get
                    {
                        List<SelectListItem> Statuses = new List<SelectListItem>();
                        Statuses.Add(new SelectListItem() { Text = "Мобильный", Value = "254" });
                        Statuses.Add(new SelectListItem() { Text = "Электронная почта", Value = "252" });

                        if (Type_ID == null)
                            Type_ID = "";

                        SelectListItem sli = Statuses.FirstOrDefault(p => p.Value == Type_ID.ToString());

                        if (sli != null)
                            sli.Selected = true;

                        return Statuses;
                    }
                }
            }
            public class AddressInfo
            {
                [Display(Name = "ID")]
                [XmlElement("Address_ID")]
                public string Address_ID { get; set; }

                [Display(Name = "Тип")]
                [XmlElement("Type_ID")]
                [Required]
                public string Type_ID { get; set; }

                [Display(Name = "Страна")]
                [XmlElement("Country_ID")]
                [Required]
                public string Country_ID { get; set; }

                [Display(Name = "Почтовый индекс")]
                [XmlElement("ZIP")]
                public string ZIP { get; set; }

                [Display(Name = "Регион")]
                [XmlElement("Region")]
                public string Region { get; set; }

                [Display(Name = "Город")]
                [XmlElement("City_ID")]
                [Required]
                public string City_ID { get; set; }

                [Display(Name = "Улица")]
                [XmlElement("Street_ID")]
                [Required]
                public string Street_ID { get; set; }

                [Display(Name = "Дом")]
                [XmlElement("House")]
                [Required]
                public string House { get; set; }

                [Display(Name = "Строение")]
                [XmlElement("Building")]
                public string Building { get; set; }

                [Display(Name = "Подъезд")]
                [XmlElement("Entry")]
                public string Entry { get; set; }

                [Display(Name = "Этаж")]
                [XmlElement("Floor")]
                public string Floor { get; set; }

                [Display(Name = "Квартира")]
                [XmlElement("Apartments")]
                public string Apartments { get; set; }

                [Display(Name = "Домофон")]
                [XmlElement("Entry_Code")]
                public string Entry_Code { get; set; }

                [Display(Name = "Доп. инфо")]
                [XmlElement("DopInfo")]
                public string DopInfo { get; set; }

                [Display(Name = "Удален")]
                [XmlElement("Deleted")]
                public string Deleted { get; set; }

                [Display(Name = "Широта")]
                [XmlElement("LAT")]
                public string LAT { get; set; }
                [Display(Name = "Долгота")]
                [XmlElement("LNG")]
                public string LNG { get; set; }

            }
            public class CRMHolderContactsInfo
            {
                [XmlArray("Holder_Contact"), XmlArrayItem("Contact")]
                public List<ContactInfo> Contacts { get; set; }
            }
            public class CRMHolderCardsInfo
            {
                [XmlArray("Holder_Card"), XmlArrayItem("Card")]
                public List<CardInfo> Cards { get; set; }
            }
            public class CRMHolderAddressesInfo
            {
                [XmlArray("Holder_Address"), XmlArrayItem("Address")]
                public List<AddressInfo> Addresses { get; set; }
            }
            public Holder Deserialize(string xml)
            {
                var xmlSerializer = new XmlSerializer(typeof(Holder));
                var stringReader = new StringReader(xml);
                return (Holder)xmlSerializer.Deserialize(stringReader);
            }
        }
        [XmlRoot("Holder_Property")]
        public class HolderProperty
        {
            [XmlElement("Property_ID")]
            public int property_id { get; set; }
            [XmlElement("Value_ID")]
            public int Value_ID { get; set; }
        }
        [XmlRoot("Message")]
        public class AddHoldersModel : APIMessage
        {
            [XmlElement("Holder")]
            public Holder Holder { get; set; }
            [XmlElement("Reserved_A")]
            public string Reserved_A_EL { get; set; }
            [XmlIgnore]
            public List<RKCRM.Holder> Holders;
            public string Serialize()
            {
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    OmitXmlDeclaration = false,
                    NewLineHandling = NewLineHandling.None
                };

                var stream = new MemoryStream();
                using (XmlWriter xw = XmlWriter.Create(stream, settings))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                    XmlSerializer x = new XmlSerializer(GetType(), "");
                    x.Serialize(xw, this, ns);
                }

                string s = Encoding.UTF8.GetString(stream.ToArray()).Substring(1);
                return s;
            }
            public bool AddHolder()
            {
                //Поиск гостя по номеру телефона перед добавлением. Если гость есть, то нужно отправить на редактирование, а не добавлять нового.
                //Избегаем дублей!!!
                Holder.ContactInfo contact = Holder.Contacts.FirstOrDefault(p => p.Type_ID == "254");

                if (contact != null)
                {
                    Action = "Add holders";
                    Holders = RKCRM.SearchHoldersByPhone(contact.Value);
                    this.Include = "Holder_Contact, Holder_Address";

                    if (string.IsNullOrEmpty(this.Holder.F_Name))
                        this.Holder.F_Name = "";

                    if (string.IsNullOrEmpty(this.Holder.L_Name))
                        this.Holder.L_Name = "";

                    if (string.IsNullOrEmpty(this.Holder.M_Name))
                        this.Holder.M_Name = "";

                    this.Holder.Full_Name = (this.Holder.L_Name + " " + this.Holder.F_Name + " " + this.Holder.M_Name).Trim();

                    string s = Serialize();
                    RKCRM.CRMResponse r = RKCRM.Query(s, "1");

                    if (r.MessageType == "Error")
                    {
                        LastResult = r.Message;
                        return false;
                    }
                    else
                    {
                        Holder = Holder.Deserialize(r.Message);
                        return true;
                    }
                }
                else
                {
                    LastResult = "Необходимо указать телефон.";
                    return false;
                }
            }
        }
        [XmlRoot("Message")]
        public class EditHoldersModel : APIMessage
        {
            [XmlElement("Holder")]
            public Holder Holder { get; set; }
            [XmlIgnore]
            public IEnumerable<SelectListItem> AccountTypesSelectList
            {
                get
                {
                    List<SelectListItem> AccountTypes = new List<SelectListItem>();
                    AccountTypes.Add(new SelectListItem() { Text = "Бонусный", Value = "16" });
                    AccountTypes.Add(new SelectListItem() { Text = "Потраты", Value = "11" });
                    AccountTypes.Add(new SelectListItem() { Text = "Дисконтный", Value = "18" });
                    return AccountTypes;
                }
            }
            public string Serialize()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(EditHoldersModel));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }
            public EditHoldersModel Deserialize(string xml)
            {
                var xmlSerializer = new XmlSerializer(typeof(EditHoldersModel));
                var stringReader = new StringReader(xml);
                return (EditHoldersModel)xmlSerializer.Deserialize(stringReader);
            }
            public Holder GetHolderInfo(string holder_id)
            {
                Holder = new RKCRM.Holder();
                Holder.Holder_ID = holder_id;
                Action = "Get holder info";
                Include = "Account, Holder_Contact, Holder_Card,  Holder_Property, Holder_Address";
                string s = Serialize();
                s = Regex.Replace(s, "<Birth>.*?</Birth>", "");
                s = s.Replace("<Holder>", "");
                s = s.Replace("</Holder>", "");
                RKCRM.CRMResponse r = RKCRM.Query(s, "1");

                if (r.MessageType == "Response")
                {
                    Holder = Holder.Deserialize(r.Message);
                    Holder.Cards = Holder.Holders_Cards.Cards;
                    Holder.Contacts = Holder.Holders_Contacts.Contacts;
                }

                return Holder;
            }
            public Holder EditHolder(string UserName)
            {
                //Сначала сохраним карты
                if (Holder.Cards != null)
                    for (int i = 0; i < Holder.Cards.Count; i++)
                    {
                        if (Holder.Cards[i].Holder_ID != null)
                        {
                            CRMResponse r = Holder.Cards[i].Save(UserName);

                            if (r.MessageType.ToUpper() == "ERROR")
                            {
                                LastResult = r.Message;
                                return Holder;
                            }
                        }
                    }

                if (Holder.Accounts != null)
                    for (int i = Holder.Accounts.Count - 1; i >= 0; i--)
                    {
                        if (Holder.Accounts[i].Account_Type_ID != null && Holder.Accounts[i].Account_Type_ID == "16")
                            Holder.Accounts[i].Account_Level_ID = "15";

                        if (Holder.Accounts[i].Account_Type_ID != null && Holder.Accounts[i].Account_Type_ID == "18")
                            Holder.Accounts[i].Account_Level_ID = "22";

                        if (Holder.Accounts[i].Status == "Blocked")
                            Holder.Accounts.Remove(Holder.Accounts[i]);
                    }

                Action = "Edit holders";
                Include = "Account, Holder_Contact, Holder_Card, Holder_Address";
                Holder.Holders_Cards = null;
                Holder.Holders_Contacts = null;
                Holder.Group_ID = "45";

                if (Holder.M_Name == null)
                    Holder.M_Name = "";

                if (Holder.L_Name == null)
                    Holder.L_Name = "";

                string query = Serialize();
                CRMResponse response = Query(query, "1");

                if (response.MessageType == "Error")
                    LastResult = response.Message;

                Holder = GetHolderInfo(Holder.Holder_ID);

                return Holder;
            }
        }
        [XmlRoot("Message")]
        public class Transaction : APIMessage
        {
            [XmlRoot("Transaction")]
            public class TransactionInfo
            {
                public string Account_Number { get; set; }
                public string External_ID { get; set; }
                public string External_Index { get; set; }
                public string External_Date { get; set; }
                public string Amount { get; set; }
                public string Transaction_Time { get; set; }
                public string Transaction_Life { get; set; }
                public string Transaction_Delay { get; set; }
                public string Remarks { get; set; }
                public string Card_Code { get; set; }
                public string Account_Type_ID { get; set; }
            }

            [XmlElement("Transaction")]
            public TransactionInfo transaction { get; set; }
            public string Serialize()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(Transaction));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }
            public bool AddTransaction()
            {
                Action = "Transaction";
                string query = Serialize();
                CRMResponse r = Query(query, "1");

                if (r.MessageType == "Error")
                {
                    LastResult = r.Message;
                    return false;
                }
                else
                    return true;
            }
        }

        [XmlRoot("Message")]
        public class ImportHoldersModel : APIMessage
        {
            [XmlElement("Holder")]
            public Holder Holder { get; set; }
            [XmlIgnore]
            public List<RKCRM.Holder> Holders;
            public string Serialize()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(ImportHoldersModel));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }
            public bool AddHolder()
            {
                //Поиск гостя по номеру телефона перед добавлением. Если гость есть, то нужно отправить на редактирование, а не добавлять нового.
                //Избегаем дублей!!!
                Holder.ContactInfo contact = Holder.Contacts.FirstOrDefault(p => p.Type_ID == "254");
                Action = "Add holders";

                if (contact != null)
                {
                    Holders = RKCRM.SearchHoldersByPhone(contact.Value);
                }

                if (Holders != null && Holders.Count > 0)
                {
                    Action = "Edit holders";
                    Holders[0].L_Name = Holder.L_Name;
                    Holders[0].F_Name = Holder.F_Name;
                    Holders[0].M_Name = Holder.M_Name;
                    Holders[0].Full_Name = null;
                    Holders[0].Birth = Holder.Birth;
                    Holders[0].Cards = Holder.Cards;
                    Holder = Holders[0];

                    if (Holder.Contacts != null)
                    {
                        for (int i = Holder.Contacts.Count - 1; i >= 0; i--)
                        {
                            if (Holder.Contacts[i].Type_ID == "254")
                                Holder.Contacts.Remove(Holder.Contacts[i]);
                        }
                    }

                    if (Holders[0].Holders_Contacts != null)
                    {
                        for (int i = 0; i < Holders[0].Holders_Contacts.Contacts.Count; i++)
                        {
                            Holder.Contacts.Add(Holders[0].Holders_Contacts.Contacts[i]);
                        }
                    }
                }

                Holder.Group_ID = "45";
                Holder.Division_ID = "1";

                bool bonus = false;
                bool discont = false;
                bool potr = false;

                if (Holder.Accounts != null)
                {
                    for (int i = Holder.Accounts.Count - 1; i >= 0; i--)
                    {
                        if (Holder.Accounts[i].Account_Type_ID == "16")
                            bonus = true;

                        if (Holder.Accounts[i].Account_Type_ID == "18")
                            discont = true;

                        if (Holder.Accounts[i].Account_Type_ID == "11")
                            potr = true;

                        if (Holder.Accounts[i].Account_Type_ID != "16" && Holder.Accounts[i].Account_Type_ID != "18" && Holder.Accounts[i].Account_Type_ID != "11")
                        {
                            RKCRM.Query("<?xml version=\"1.0\" encoding=\"Windows-1251\" standalone=\"yes\" ?><Message Action=\"Account block\" Terminal_Type=\"2\" Global_Type=\"kN3uF2TTVtmpp1Gb25Mj\"><Account_Number>" + Holder.Accounts[i].Account_Number + "</Account_Number><Remarks>Блокировка счета по причине перехода на бонусную систему</Remarks> </Message>", "1");
                            Holder.Accounts.Remove(Holder.Accounts[i]);
                        }
                    }
                }

                if (Holder.Accounts == null)
                {
                    Holder.Accounts = new List<Holder.AccountInfo>();
                }

                if (!bonus)
                    Holder.Accounts.Add(new Holder.AccountInfo() { Account_Type_ID = "16", Auto_Change_Levels = "false", Account_Level_ID = "15" });

                if (!discont)
                    Holder.Accounts.Add(new Holder.AccountInfo() { Account_Type_ID = "18", Auto_Change_Levels = "false", Account_Level_ID = "22" });

                if (!potr)
                    Holder.Accounts.Add(new Holder.AccountInfo() { Account_Type_ID = "11", Auto_Change_Levels = "false" });

                Holder.Holders_Cards = null;
                Holder.Holders_Contacts = null;
                Holder.Holders_Properties = null;

                string s = Serialize();
                RKCRM.CRMResponse r = RKCRM.Query(s, "1");

                if (r.MessageType == "Error")
                {
                    LastResult = r.Message;
                    return false;
                }
                else
                {
                    //Holder = Holder.Deserialize(r.Message);
                    return true;
                }
            }
        }
    }
    public class CRMTools
    {
        private static SqlConnection GetConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
            SqlConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }

        private Entities db = new Entities();
        public List<CRMSegment> Segments { get; set; }
        public List<SelectListItem> Templates
        {
            get
            {
                List<SelectListItem> result = new List<SelectListItem>();
                var et = db.EmailTemplate.ToList();

                foreach (var e in et)
                {
                    result.Add(new SelectListItem() { Value = e.id.ToString(), Text = e.Title });
                }

                return result;
            }
        }
        public string Exception { get; set; }
        public CRMSegment Segment { get; set; }
        [Required]
        [Display(Name="Тема письма")]
        public string MailTitle { get; set; }
        [Required]
        [Display(Name = "Текст письма")]
        public string MailText { get; set; }
        public List<CRMSegment> GetSegments()
        {
            return db.CRMSegment.Where(p => p.IsActive == true).ToList();
        }
        public CRMSegment GetSegment(int id)
        {
            return db.CRMSegment.FirstOrDefault(p => p.id == id);
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                //MessageBox.Show("Canceled");
            }
            if (e.Error != null)
            {
                //textBox1.Text += String.Format("[{0}] {1}\r\n", token, e.Error.ToString());
            }
            else
            {
                //textBox1.Text += "Message sent\r\n";
            }

            //mailSent = true;
        }

        private class Email
        {
            public string id { get; set; }
            public string email { get; set; }
        }

        public bool Prepare()
        {
            try
            {
                if (this.Segment != null && !String.IsNullOrEmpty(this.Segment.SegmentQuery))
                {
                    SqlConnection con = GetConnection();
                    SqlCommand cmd = new SqlCommand(this.Segment.SegmentQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    List<Email> mailList = new List<Email>();

                    Mail mail = new Mail() { MailSubject = this.MailTitle, MailTemplateID = (int)this.Segment.EmailTemplateID, CreateDateTime = DateTime.Now };

                    db.Mail.Add(mail);
                    db.SaveChanges();

                    while (rdr.Read())
                    {
                        if (rdr["CONTACT_VALUE"] != DBNull.Value && rdr["CONTACT_VALUE"].ToString().Trim() != "")
                        {
                            mailList.Add(new Email() { id = rdr["CONTACT_ID"].ToString(), email = rdr["CONTACT_VALUE"].ToString() });
                        }
                    }

                    rdr.Close();
                    con.Close();
                    cmd.Dispose();

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, new TimeSpan(2, 00, 0)))
                    {
                        Entities context = null;

                        context = new Entities();
                        context.Configuration.AutoDetectChangesEnabled = false;

                        int count = 0;

                        foreach (var m in mailList)
                        {
                            string hash = "";

                            using (MD5 md5 = MD5.Create())
                            {
                                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(m.id + "_" + m.email));

                                StringBuilder sBuilder = new StringBuilder();

                                for (int i = 0; i < data.Length; i++)
                                    sBuilder.Append(data[i].ToString("x2"));


                                hash = sBuilder.ToString();
                            }

                            CRMToSend row = new CRMToSend() { MailID = mail.id, Paprika = hash, RecipientEmail = m.email, StatusID = 1, CreateDateTime = DateTime.Now };

                            ++count;
                            context = AddToContext(context, row, count, 1000, true);

                            /*SmtpClient client = new SmtpClient();
                            client.Host = "srv-ex00.fg.local";
                            client.Port = 2525;
                            client.EnableSsl = false;
                            client.Credentials = new NetworkCredential("noreply", "123456zZ");
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;

                            MailAddress from = new MailAddress("noreply@tokyo-bar.ru", "Кристина из Токио",
                            System.Text.Encoding.UTF8);

                            MailAddress to = new MailAddress(m.email);

                            MailMessage message = new MailMessage(from, to);
                            message.IsBodyHtml = true;
                            message.Body = template.Content.Replace("{content}", this.MailText).Replace("{unsubscribe_link}", "https://tokyo-bar.ru/personal/email-d.php?num_email=" + m.email + "&paprika=" + hash);
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.Subject = this.MailTitle;
                            message.SubjectEncoding = System.Text.Encoding.UTF8;
                            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                            string userState = "Тестовая рассылка! " + m;
                            client.SendAsync(message, userState);*/
                        }

                        context.SaveChanges();

                        try
                        {
                            
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            if (context != null)
                                context.Dispose();
                        }

                        scope.Complete();
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        private Entities AddToContext(Entities context, CRMToSend entity, int count, int commitCount, bool recreateContext)
        {
            context.CRMToSend.Add(entity);

            if (count % commitCount == 0)
            {
                context.SaveChanges();
                if (recreateContext)
                {
                    context.Dispose();
                    context = new Entities();
                    context.Configuration.AutoDetectChangesEnabled = false;
                }
            }

            return context;
        }
    }
}