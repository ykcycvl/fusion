using System;
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

namespace Fusion.Models
{
    public class RKCRM
    {
        static IPAddress ip = IPAddress.Parse("10.1.0.108");
        static string Terminal_Type = "239487KJT3asf";
        static string Global_Type = "kN3uF2TTVtmpp1Gb25Mj";
        public class CRMResponse
        {
            public string Message = "";
            public string MessageType = "";
        }
        public static CRMResponse Query(string query)
        {
            CRMResponse response = new CRMResponse();

            query = String.Format(@"Message-ID: 1
                        Message-Type: Request
                        Time: '2016-10-04 13:42:12'
                        Terminal-Type: 25
                        Content-Length: {0}

                        {1}
                        ", query.Length, query);

            TcpClient client = new TcpClient();
            client.SendTimeout = 120000;
            client.ReceiveTimeout = 120000;
            client.ReceiveBufferSize = 5242880;

            client.Connect(ip, 9191);
            NetworkStream tcpStream = client.GetStream();
            byte[] sendBytes = Encoding.GetEncoding(1251).GetBytes(query);
            tcpStream.Write(sendBytes, 0, sendBytes.Length);
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int bytesRead = tcpStream.Read(bytes, 0, client.ReceiveBufferSize);
            client.Close();

            string returnData = Encoding.GetEncoding(1251).GetString(bytes).Replace("\0", "");

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
                Global_Type = "kN3uF2TTVtmpp1Gb25Mj";
            }
        }
        public static List<Holder> SearchHoldersByPhone(string phone)
        {
            string query = String.Format(@"<?xml version=""1.0"" standalone=""yes"" ?>
<Message Action=""Search holders"" Terminal_Type=""{0}"" Global_Type=""{1}"">
	<Include>Account, Holder_Card, Holder_Contact</Include>
	<Count>25</Count>
	<Index>1</Index>
	<Item Mode=""Clear""/>
	<Item Mode=""Add"">
	<Contacts>
		<Phone Value=""{2}"" IsNumber=""True""/>
	</Contacts>
	</Item>
</Message>", Terminal_Type, Global_Type, phone);
            CRMResponse r = Query(query);
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
	<Include>Account, Holder_Card, Holder_Contact</Include>
	<Count>25</Count>
	<Index>1</Index>
	<Item Mode=""Clear""/>
	<Item Mode=""Add"">
	<Contacts>
		<EMail Value=""{2}"" />
	</Contacts>
	</Item>
</Message>", Terminal_Type, Global_Type, email);
            CRMResponse r = Query(query);
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

            [XmlElement("Holders_Cards")]
            public CRMHolderCardsInfo Holders_Cards { get; set; }

            [XmlElement("Holders_Contacts")]
            public CRMHolderContactsInfo Holders_Contacts { get; set; }
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

                    return RKCRM.Query(cardXML);
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
            [XmlIgnore]
            public List<RKCRM.Holder> Holders;
            public string Serialize()
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(AddHoldersModel));
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

                if (contact != null)
                {
                    Action = "Add holders";

                    Holders = RKCRM.SearchHoldersByPhone(contact.Value);

                    if (Holders.Count > 0)
                    {
                        ///
                        /// Вот тут я решил закоментить, потому что не помню, что делал до этого и это, что закоменчено, может быть пригодится мне в будущем. Хотя очень надеюсь что нет ¯\_(ツ)_/¯
                        /// В общем, если через месяц мне эта херь не понадобится, я ее удалю :) Сегодня 14/12/2016))

                        /*Holders[0].L_Name = Holder.L_Name;
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
                                    RKCRM.Query("<?xml version=\"1.0\" encoding=\"Windows-1251\" standalone=\"yes\" ?><Message Action=\"Account block\" Terminal_Type=\"2\" Global_Type=\"kN3uF2TTVtmpp1Gb25Mj\"><Account_Number>" + Holder.Accounts[i].Account_Number + "</Account_Number><Remarks>Блокировка счета по причине перехода на бонусную систему</Remarks> </Message>");
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

                        Action = "Edit holders";*/
                    }

                    string s = Serialize();
                    RKCRM.CRMResponse r = RKCRM.Query(s);

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
                Include = "Account, Holder_Contact, Holder_Card,  Holder_Property";
                string s = Serialize();
                s = Regex.Replace(s, "<Birth>.*?</Birth>", "");
                s = s.Replace("<Holder>", "");
                s = s.Replace("</Holder>", "");
                RKCRM.CRMResponse r = RKCRM.Query(s);

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
                    for (int i = Holder.Accounts.Count - 1; i > 0; i--)
                    {
                        if (Holder.Accounts[i].Account_Type_ID != null && Holder.Accounts[i].Account_Type_ID == "16")
                            Holder.Accounts[i].Account_Level_ID = "15";

                        if (Holder.Accounts[i].Account_Type_ID != null && Holder.Accounts[i].Account_Type_ID == "18")
                            Holder.Accounts[i].Account_Level_ID = "22";

                        if (Holder.Accounts[i].Status == "Blocked")
                            Holder.Accounts.Remove(Holder.Accounts[i]);
                    }

                Action = "Edit holders";
                Include = "Account, Holder_Contact, Holder_Card";
                Holder.Holders_Cards = null;
                Holder.Holders_Contacts = null;
                Holder.Group_ID = "45";

                if (Holder.M_Name == null)
                    Holder.M_Name = "";

                if (Holder.L_Name == null)
                    Holder.L_Name = "";

                string query = Serialize();
                CRMResponse response = Query(query);

                if (response.MessageType == "Error")
                {
                    LastResult = response.Message;

                    /*Holder = Holder.Deserialize(response.Message);
                    Holder.Cards = Holder.Holders_Cards.Cards;
                    Holder.Contacts = Holder.Holders_Contacts.Contacts;*/
                }

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
                CRMResponse r = Query(query);

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
                            RKCRM.Query("<?xml version=\"1.0\" encoding=\"Windows-1251\" standalone=\"yes\" ?><Message Action=\"Account block\" Terminal_Type=\"2\" Global_Type=\"kN3uF2TTVtmpp1Gb25Mj\"><Account_Number>" + Holder.Accounts[i].Account_Number + "</Account_Number><Remarks>Блокировка счета по причине перехода на бонусную систему</Remarks> </Message>");
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
                RKCRM.CRMResponse r = RKCRM.Query(s);

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
}