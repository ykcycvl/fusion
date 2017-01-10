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
    public class CRM
    {
        static string Terminal_Type = WebConfigurationManager.AppSettings["Terminal_Type"].ToString();
        static string Global_Type = WebConfigurationManager.AppSettings["Global_Type"].ToString();
        static IPAddress ip = IPAddress.Parse(WebConfigurationManager.AppSettings["CSIP"].ToString());

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
            client.Connect(ip, 9191);
            NetworkStream tcpStream = client.GetStream();
            byte[] sendBytes = Encoding.GetEncoding(1251).GetBytes(query);
            tcpStream.Write(sendBytes, 0, sendBytes.Length);
            byte[] bytes = new byte[client.ReceiveBufferSize];
            int bytesRead = tcpStream.Read(bytes, 0, client.ReceiveBufferSize);
            string returnData = Encoding.UTF8.GetString(bytes).Replace("\0", "");
            client.Close();

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
    }
    public class CRMViewModels
    {
        [XmlRoot("CHECK")]
        public class CheckInfo
        {
            private CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();
            private RK7Entities dbRK = new RK7Entities();
            public class EXTINFO
            {
                public class INTERFACE
                {
                    [XmlRoot("ITEM")]
                    public class Item
                    {
                        [XmlAttribute("cardcode")]
                        public string cardcode { get; set; }
                    }

                    [XmlArray("HOLDERS"), XmlArrayItem("ITEM")]
                    public List<Item> Items { get; set; }
                }
                [XmlArray("INTERFACES"), XmlArrayItem("INTERFACE")]
                public List<INTERFACE> Interfaces { get; set; }
            }
            [XmlElement("EXTINFO")]
            public EXTINFO Extinfo { get; set; }
            public class CHECKDATA
            {
                [XmlAttribute("checknum")]
                public int checknum { get; set; }
                [XmlAttribute("tablename")]
                public string tablename { get; set; }
                [XmlAttribute("startservice")]
                public string startservice { get; set; }
                [XmlAttribute("closedatetime")]
                public string closedatetime { get; set; }
                [XmlAttribute("ordernum")]
                public string ordernum { get; set; }
                [XmlAttribute("guests")]
                public int guests { get; set; }

                public class PERSON
                {
                    [XmlAttribute("name")]
                    public string Name { get; set; }
                }
                [XmlArray("CHECKPERSONS"), XmlArrayItem("PERSON")]
                public List<PERSON> Checkpersons { get; set; }

                public class Checkline
                {
                    [XmlAttribute("id")]
                    public string id { get; set; }
                    [XmlAttribute("code")]
                    public int code { get; set; }
                    [XmlAttribute("name")]
                    public string name { get; set; }
                    [XmlAttribute("uni")]
                    public int uni { get; set; }
                    [XmlAttribute("type")]
                    public string type { get; set; }
                    [XmlAttribute("price")]
                    public Decimal price { get; set; }
                    [XmlIgnore]
                    [XmlAttribute("categ_id")]
                    public Nullable<int> categ_id { get; set; }
                    [XmlAttribute("servprint_id")]
                    public string servprint_id { get; set; }
                    [XmlAttribute("servprint")]
                    public string servprint { get; set; }
                    [XmlAttribute("quantity")]
                    public Decimal quantity { get; set; }
                    [XmlAttribute("sum")]
                    public Decimal sum { get; set; }
                    [XmlAttribute("parent")]
                    public int parent { get; set; }
                }
                [XmlArray("CHECKLINES"), XmlArrayItem("LINE")]
                public List<Checkline> Checklines { get; set; }

                public class CheckDiscount
                {
                    [XmlAttribute("id")]
                    public string id { get; set; }
                    [XmlAttribute("code")]
                    public int code { get; set; }
                    [XmlAttribute("name")]
                    public string name { get; set; }
                    [XmlAttribute("cardcode")]
                    public string cardcode { get; set; }
                }
                [XmlArray("CHECKDISCOUNTS"), XmlArrayItem("DISCOUNT")]
                public List<CheckDiscount> Discounts { get; set; }

                public class PAYMENT
                {
                    [XmlAttribute("id")]
                    public string id { get; set; }
                    [XmlAttribute("code")]
                    public int code { get; set; }
                    [XmlAttribute("name")]
                    public string name { get; set; }
                    [XmlAttribute("uni")]
                    public int uni { get; set; }
                    [XmlAttribute("paytype")]
                    public int paytype { get; set; }
                    [XmlAttribute("bsum")]
                    public Decimal bsum { get; set; }
                    [XmlAttribute("sum")]
                    public Decimal sum { get; set; }
                    [XmlAttribute("cardcode")]
                    public string cardcode { get; set; }
                    [XmlAttribute("ownerinfo")]
                    public string ownerinfo { get; set; }
                }
                [XmlArray("CHECKPAYMENTS"), XmlArrayItem("PAYMENT")]
                public List<PAYMENT> Payments { get; set; }
            }
            [XmlElement("CHECKDATA")]
            public CHECKDATA CheckData { get; set; }
            [XmlAttribute("stationcode")]
            public int stationcode { get; set; }
            [XmlAttribute("restaurantcode")]
            public int restaurantcode { get; set; }
            [XmlAttribute("cashservername")]
            public string cashservername { get; set; }
            [XmlAttribute("generateddatetime")]
            public string generateddatetime { get; set; }
            [XmlAttribute("chmode")]
            public string chmode { get; set; }
            [XmlAttribute("shiftdate")]
            public string shiftdate { get; set; }
            [XmlAttribute("shiftnum")]
            public int shiftnum { get; set; }
            public void Deserialize(string xml)
            {
                Encoding utf8 = Encoding.GetEncoding("UTF-8");
                Encoding win1251 = Encoding.GetEncoding(1251);

                byte[] utf8Bytes = utf8.GetBytes(xml);
                byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
                xml = utf8.GetString(win1251Bytes);

                var xmlSerializer = new XmlSerializer(typeof(CheckInfo));
                var stringReader = new StringReader(xml);
                CheckInfo c = (CheckInfo)xmlSerializer.Deserialize(stringReader);
                this.cashservername = c.cashservername;
                this.Extinfo = c.Extinfo;
                this.CheckData = c.CheckData;
                this.chmode = c.chmode;
                this.generateddatetime = c.generateddatetime;
                this.restaurantcode = c.restaurantcode;
                this.shiftdate = c.shiftdate;
                this.shiftnum = c.shiftnum;
                this.stationcode = c.stationcode;
            }
            [XmlIgnore]
            public CASHGROUPS midserver;
            [XmlIgnore]
            public CASHES cashstation;
            [XmlIgnore]
            public Decimal bp_plus = 0;
            [XmlIgnore]
            public Decimal bp_minus = 0;

            public bool GetCheck(Guid TransactionGuid)
            {
                CARD_TRANSACTION_NOTES checkinfo = db.CARD_TRANSACTION_NOTES.FirstOrDefault(p => p.TRANSACT_GUID == TransactionGuid);

                if (checkinfo != null && checkinfo.DOP_INFO != null)
                {
                    CARD_TRANSACTIONS transaction = db.CARD_TRANSACTIONS.FirstOrDefault(p => p.TRANSACT_GUID == TransactionGuid && p.TRANSACTION_TYPE == 162);

                    if (transaction != null && transaction.SUMM != null)
                        bp_plus = (decimal)transaction.SUMM;

                    transaction = db.CARD_TRANSACTIONS.FirstOrDefault(p => p.TRANSACT_GUID == TransactionGuid && p.TRANSACTION_TYPE == 161);

                    if (transaction != null && transaction.SUMM != null)
                        bp_minus = (decimal)transaction.SUMM;

                    this.Deserialize(checkinfo.DOP_INFO);

                    midserver = dbRK.CASHGROUPS.FirstOrDefault(p => p.NETNAME == cashservername && p.STATUS == 3);

                    if (midserver != null)
                    {
                        cashstation = dbRK.CASHES.FirstOrDefault(p => p.CASHGROUP == midserver.SIFR && p.STATUS == 3 && p.CODE == stationcode);
                    }

                    return true;
                }
                else
                    return false;
            }
        }
        public class OverallViewModel
        {
            private CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();

            public class InvalidHoldersAccounts
            {
                private static SqlConnection GetConnection()
                {
                    string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                    SqlConnection dbConnection = new SqlConnection(connectionString);
                    dbConnection.Open();
                    return dbConnection;
                }

                public int count { get; set; }
                public void GetIHA()
                {
                    SqlConnection con = GetConnection();
                    SqlCommand command = new SqlCommand("SELECT COUNT(t1.PEOPLE_ID) FROM (select cp.PEOPLE_ID, COUNT(cpa.PEOPLE_ACCOUNT_ID) as accountcount from CARD_PEOPLES cp INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON cp.PEOPLE_ID = cpa.PEOPLE_ID where cp.GROUP_ID = 45 and cp.DELETED = 0 and cpa.DELETED = 0 GROUP BY cp.PEOPLE_ID HAVING COUNT(cpa.PEOPLE_ACCOUNT_ID) <= 2) t1", con);

                    SqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            count = Convert.ToInt32(record[0]);
                        }
                    }

                    rdr.Close();
                    con.Close();
                }

                public void AddAccounts()
                {
                    List<long> people_ids = new List<long>();

                    SqlConnection con = GetConnection();
                    SqlCommand command = new SqlCommand("select cp.PEOPLE_ID, COUNT(cpa.PEOPLE_ACCOUNT_ID) as accountcount from CARD_PEOPLES cp INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON cp.PEOPLE_ID = cpa.PEOPLE_ID where cp.GROUP_ID = 45 and cp.DELETED = 0 and cpa.DELETED = 0 GROUP BY cp.PEOPLE_ID HAVING COUNT(cpa.PEOPLE_ACCOUNT_ID) <= 2", con);

                    SqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            people_ids.Add(Convert.ToInt64(record["PEOPLE_ID"]));
                        }
                    }

                    rdr.Close();
                    con.Close();

                    for (int i = 0; i < people_ids.Count; i++)
                    {
                        string query = String.Format(@"<?xml version=""1.0"" encoding=""Windows-1251"" standalone=""yes"" ?>
							<Message Action=""Edit holders"" Terminal_Type=""15"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"">
								<Holder_ID>{0}</Holder_ID>
								<Include></Include>
								<Holder>
									<Holder_ID>{0}</Holder_ID>
									<Group_ID>45</Group_ID>
									<Accounts>
										<Account>
											<Account_Type_ID>16</Account_Type_ID>
											<Auto_Change_Levels>false</Auto_Change_Levels>
											<Account_Level_ID>15</Account_Level_ID>
										</Account>
										<Account>
											<Account_Type_ID>18</Account_Type_ID>
											<Auto_Change_Levels>false</Auto_Change_Levels>
											<Account_Level_ID>22</Account_Level_ID>
										</Account>
                                        <Account>
											<Account_Type_ID>11</Account_Type_ID>
											<Auto_Change_Levels>false</Auto_Change_Levels>
										</Account>
									</Accounts>
									<Addresses>
									</Addresses>
								</Holder>
							</Message>", people_ids[i]);
                        RKCRM.Query(query);
                    }
                }
            }

            public InvalidHoldersAccounts IHA;
            public class OverallCardInfo
            {
                private CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();
                public string CardsGroup { get; set; }
                public int TotalCardsCount { get; set; }
                public int ActiveCardsCount { get; set; }
                public int VoidedCardsAmount { get; set; }
                public int BlockedCardsAmount { get; set; }
                public int LinkedCardsAmount { get; set; }
                public int VacantCardsAmount { get; set; }

                public OverallCardInfo(int group_id)
                {
                    TotalCardsCount = db.CARD_CARDS.Count(p => p.GROUP_ID == group_id && p.DELETED == 0);
                    ActiveCardsCount = db.CARD_CARDS.Count(p => p.GROUP_ID == group_id && p.DELETED == 0 && p.STATUS == 1);
                    VoidedCardsAmount = db.CARD_CARDS.Count(p => p.GROUP_ID == group_id && p.DELETED == 0 && p.STATUS == 3);
                    BlockedCardsAmount = db.CARD_CARDS.Count(p => p.GROUP_ID == group_id && p.DELETED == 0 && p.STATUS == 4);
                    LinkedCardsAmount = db.CARD_CARDS.Count(p => p.GROUP_ID == group_id && p.DELETED == 0 && p.STATUS == 1 && p.PEOPLE_ID != null);
                    VacantCardsAmount = db.CARD_CARDS.Count(p => p.GROUP_ID == group_id && p.DELETED == 0 && p.STATUS == 1 && p.PEOPLE_ID == null);
                }
            }
            public List<OverallCardInfo> CardsInfo = new List<OverallCardInfo>();
            public Decimal? TotalBP { get; set; }
            public int TotalPersons { get; set; }
            public int VacantCardsAmount { get; set; }
            public OverallViewModel()
            {
                TotalBP = db.CARD_PEOPLE_ACCOUNTS.Where(p => p.ACCOUNT_TYPE_ID == 16).Sum(p => p.BALANCE);
                TotalPersons = db.CARD_PEOPLES.Where(p => p.GROUP_ID == 45).Count();

                if (TotalBP == null)
                    TotalBP = 0;

                List<CARD_GROUPS> groups = db.CARD_GROUPS.Where(p => p.DELETED == 0 && p.PARENT_ID == 20).OrderByDescending(p => p.NAME).ToList();

                foreach (var g in groups)
                {
                    OverallCardInfo oci = new OverallCardInfo(g.GROUP_ID);
                    oci.CardsGroup = g.NAME;
                    CardsInfo.Add(oci);

                    if (g.GROUP_ID == 44)
                        VacantCardsAmount = oci.VacantCardsAmount;
                }
            }
        }
        public class GuestViewModel
        {
            private CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();
            public long PEOPLE_ID { get; set; }
            public string F_NAME { get; set; }
            public string M_NAME { get; set; }
            public string L_NAME { get; set; }
            public string FULL_NAME { get; set; }
            public Nullable<short> DELETED { get; set; }
            public string GroupName { get; set; }
            public Nullable<System.DateTime> BIRTHDAY { get; set; }
            public Decimal Balance { get; set; }
            public Decimal BPRecieved { get; set; }
            public Decimal BPSpented { get; set; }
            public List<CARD_CARDS> Cards { get; set; }
            public List<CARD_CONTACTS> Phones { get; set; }
            public List<CARD_CONTACTS> Emails { get; set; }
        }
        public class GuestListViewModel
        {
            private CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();

            public List<GuestViewModel> Guests = new List<GuestViewModel>();
        }
        public class TransactionsViewModel
        {
            private string searchString = "";
            private long? _people_id;
            public long? people_id
            {
                get
                {
                    return _people_id;
                }
                set
                {
                    if (value != null)
                    {
                        _people_id = value;

                        searchString = string.Format(@"where 
	                            cpa.PEOPLE_ID = {0} 
                                and
                                ct.TRANSACTION_TIME >= '{1}' and ct.TRANSACTION_TIME <= '{2}'
	                            and
	                            ca.ACCOUNT_TYPE_ID = 16
                                and
								ct.TRANSACTION_TYPE IN (161, 162)", _people_id, _StartDateTime.ToString("yyyy-MM-dd"), _EndDateTime.ToString("yyyy-MM-dd"));

                        person = db.CARD_PEOPLES.FirstOrDefault(p => p.PEOPLE_ID == (long)people_id);
                    }
                }
            }
            private DateTime _StartDateTime;
            public DateTime StartDateTime 
            {
                get
                {
                    return _StartDateTime;
                }
                set
                {
                    _StartDateTime = value;

                    if (_people_id != null)
                    {
                        searchString = string.Format(@"where 
	                            cpa.PEOPLE_ID = {0} 
                                and
                                ct.TRANSACTION_TIME >= '{1}' and ct.TRANSACTION_TIME <= '{2}'
	                            and
	                            ca.ACCOUNT_TYPE_ID = 16
                                and
								ct.TRANSACTION_TYPE IN (161, 162)", _people_id, _StartDateTime.ToString("yyyy-MM-dd"), _EndDateTime.ToString("yyyy-MM-dd 23:59:59"));
                    }
                    else
                    {
                        searchString = string.Format(@"where 
                                ct.TRANSACTION_TIME >= '{0}' and ct.TRANSACTION_TIME <= '{1}'
	                            and
	                            ca.ACCOUNT_TYPE_ID = 16
                                and
                                ct.TRANSACTION_LINK IS NULL
                                and
								ct.TRANSACTION_TYPE IN (161, 162)", _StartDateTime.ToString("yyyy-MM-dd"), _EndDateTime.ToString("yyyy-MM-dd 23:59:59"));
                    }
                }
            }
            private DateTime _EndDateTime;
            public DateTime EndDateTime
            {
                get
                {
                    return _EndDateTime;
                }
                set
                {
                    _EndDateTime = value;

                    if (_people_id != null)
                    {
                        searchString = string.Format(@"where 
	                            cpa.PEOPLE_ID = {0} 
                                and
                                ct.TRANSACTION_TIME >= '{1}' and ct.TRANSACTION_TIME <= '{2}'
	                            and
	                            ca.ACCOUNT_TYPE_ID = 16
								and
								ct.TRANSACTION_TYPE IN (161, 162)", _people_id, _StartDateTime.ToString("yyyy-MM-dd"), _EndDateTime.ToString("yyyy-MM-dd 23:59:59"));
                    }
                    else
                    {
                        searchString = string.Format(@"where 
                                ct.TRANSACTION_TIME >= '{0}' and ct.TRANSACTION_TIME <= '{1}'
	                            and
	                            ca.ACCOUNT_TYPE_ID = 16
								and
								ct.TRANSACTION_TYPE IN (161, 162)", _StartDateTime.ToString("yyyy-MM-dd"), _EndDateTime.ToString("yyyy-MM-dd 23:59:59"));
                    }
                }
            }
            public CARD_PEOPLES person;
            private CARD_SYSTEMEntities db = new CARD_SYSTEMEntities();
            public class TransactionInfo
            {
                public long transaction_id { get; set; }
                public long? transaction_link { get; set; }
                public Guid TransactionGuid { get; set; }
                public DateTime transaction_time { get; set; }
                public int transaction_type { get; set; }
                public long card_code { get; set; }
                public string account_name { get; set; }
                public string dop_info { get; set; }
                public string REASON { get; set; }
                public string NOTES { get; set; }
                public string full_name { get; set; }
                public Decimal bpAccrued = 0;
                public Decimal bpSpented = 0;
            }
            public List<TransactionInfo> Transactions = new List<TransactionInfo>();
            public void Search()
            {

                string query = String.Format(@"select 
	                                DISTINCT
                                    ct.TRANSACTION_LINK,
	                                ct.TRANSACTION_ID,
	                                ct.TRANSACTION_TIME,
	                                ct.TRANSACTION_TYPE,
									ct.TRANSACT_GUID,
	                                ct.SUMM,
	                                ca.NAME,
	                                ctn.DOP_INFO,
									cp.L_NAME,
									cp.F_NAME,
									cp.M_NAME,
									cp.FULL_NAME,
									ctr.NAME AS REASON,
									ctn.NOTES,
									ct.CARD_CODE
                                from CARD_TRANSACTIONS ct
	                                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                                INNER JOIN CARD_ACCOUNT_TYPES ca ON cpa.ACCOUNT_TYPE_ID = ca.ACCOUNT_TYPE_ID
	                                LEFT JOIN CARD_TRANSACTION_NOTES ctn ON ct.TRANSACT_GUID = ctn.TRANSACT_GUID
                                    LEFT JOIN CARD_TRANSFER_REASONS ctr ON ctn.TRANSFER_REASON_ID = ctr.TRANSFER_REASON_ID
									LEFT JOIN CARD_PEOPLES cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
                                {0}
                                ORDER BY ct.TRANSACTION_LINK, ct.TRANSACTION_TIME DESC", searchString);
                string connectionstring = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ToString();
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 120000;
                SqlDataReader rdr = cmd.ExecuteReader();

                foreach (DbDataRecord record in rdr)
                {
                    TransactionInfo ti = new TransactionInfo();
                    ti.account_name = record["NAME"].ToString();

                    if (record["CARD_CODE"] != DBNull.Value)
                        ti.card_code = Convert.ToInt64(record["CARD_CODE"]);

                    if (record["TRANSACTION_LINK"] != DBNull.Value)
                        ti.transaction_link = Convert.ToInt64(record["TRANSACTION_LINK"]);

                    if (record["DOP_INFO"] != DBNull.Value)
                        ti.dop_info = record["DOP_INFO"].ToString();

                    if (record["FULL_NAME"] != DBNull.Value)
                        ti.full_name = record["FULL_NAME"].ToString();

                    if (record["TRANSACTION_ID"] != DBNull.Value)
                        ti.transaction_id = Convert.ToInt64(record["TRANSACTION_ID"]);

                    if (record["TRANSACTION_TIME"] != DBNull.Value)
                        ti.transaction_time = Convert.ToDateTime(record["TRANSACTION_TIME"]);

                    if (record["TRANSACTION_TYPE"] != DBNull.Value)
                        ti.transaction_type = Convert.ToInt32(record["TRANSACTION_TYPE"]);

                    if (record["SUMM"] != DBNull.Value)
                    {
                        if(ti.transaction_type == 161)
                            ti.bpSpented = Convert.ToDecimal(record["SUMM"]);
                        else
                            if(ti.transaction_type == 162)
                                ti.bpAccrued = Convert.ToDecimal(record["SUMM"]);
                    }

                    ti.TransactionGuid = Guid.Parse(record["TRANSACT_GUID"].ToString());

                    if (record["REASON"] != DBNull.Value)
                        ti.REASON = record["REASON"].ToString();

                    if (record["NOTES"] != DBNull.Value)
                        ti.NOTES = record["NOTES"].ToString();

                    if (ti.transaction_link == null)
                    {
                        TransactionInfo tiSearch = Transactions.FirstOrDefault(p => p.TransactionGuid == ti.TransactionGuid);

                        if (tiSearch == null)
                            Transactions.Add(ti);
                        else
                            if (ti.transaction_type == 161)
                                tiSearch.bpSpented = ti.bpSpented;
                            else
                                if (ti.transaction_type == 162)
                                    tiSearch.bpAccrued = ti.bpAccrued;
                    }
                    else
                    {
                        TransactionInfo tiSearch = Transactions.FirstOrDefault(p => p.transaction_id == ti.transaction_link);

                        if (tiSearch == null)
                        {
                            Transactions.Add(ti);
                            ti.NOTES = "Отмена транзакции " + ti.transaction_link.ToString();
                        }
                        else
                        {
                            tiSearch.transaction_link = ti.transaction_link;
                            tiSearch.NOTES += "\r\n" + "Отмена транзакции " + ti.transaction_link.ToString();
                        }
                    }
                }

                con.Close();
            }
        }
        public class PersonViewModel
        {
            public ulong PEOPLE_ID { get; set; }
            public string L_NAME { get; set; }
            public string F_NAME { get; set; }
            public string M_NAME { get; set; }
            public string FULL_NAME { get; set; }
            public ulong PEOPLE_ACCOUNT_ID { get; set; }
            public string CARD_CODE { get; set; }
            public DateTime BIRTHDAY { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public decimal BALANCE { get; set; }
            public DateTime EXP_DATE { get; set; }
            public string PIN { get; set; }
            public ulong phoneid { get; set; }
            public ulong emailid { get; set; }
            public decimal accrued { get; set; }
            public decimal spented { get; set; }
            public string CityAttr { get; set; }
        }
        public class PersonsViewModel
        {
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }

            private string searchString = "WHERE cp.DELETED = 0";
            private string orderString = " ORDER BY FULL_NAME ASC";
            private string _order = "";
            public string order
            {
                get
                {
                    return _order;
                }
                set
                {
                    if (value != null && value != "")
                    {
                        orderString = string.Format("ORDER BY {0} {1}", value, _OrderDirection);
                        _order = value;
                    }
                }
            }
            private string _OrderDirection = "ASC";
            public string OrderDirection 
            {
                get { return _OrderDirection; }
                set
                {
                    if (value != null && value != "" && _order != null && _order != "")
                    {
                        orderString = string.Format("ORDER BY {0} {1}", _order, value);
                        _OrderDirection = value;
                    }
                }
            }
            private string _L_NAME;
            public string L_NAME 
            {
                get
                {
                    return _L_NAME;
                }
                set
                {
                    _L_NAME = value;
                }
            }
            private string _F_NAME;
            public string F_NAME
            {
                get
                {
                    return _F_NAME;
                }
                set
                {
                    _F_NAME = value;
                }
            }
            private string _M_NAME;
            public string M_NAME
            {
                get
                {
                    return _M_NAME;
                }
                set
                {
                    _M_NAME = value;
                }
            }
            private string _FULL_NAME;
            public string FULL_NAME 
            {
                get
                {
                    return _FULL_NAME;
                }
                set 
                {
                    if(value != null && value != "")
                        searchString += string.Format(" AND cp.FULL_NAME LIKE('%{0}%')", value);

                    _FULL_NAME = value;
                }
            }
            private string _CardCode;
            public string CardCode
            {
                get
                {
                    return _CardCode;
                }
                set
                {
                    if (value != null && value != "")
                        searchString += string.Format(" AND cc.CARD_CODE = {0}", value);

                    _CardCode = value;
                }
            }
            private string _BDATE;
            public string BDATE
            {
                get
                {
                    return _BDATE;
                }
                set
                {
                    _BDATE = value;
                }
            }
            private string _phone;
            public string phone 
            {
                get
                {
                    return _phone;
                }
                set 
                {
                    if (value != null && value != "")
                        searchString += string.Format(" AND cphone.CONTACT_VALUE LIKE('%{0}%')", value);

                    _phone = value;
                }
            }
            private string _email;
            public string email 
            {
                get
                {
                    return _email;
                }
                set
                {
                    if (value != null && value != "")
                        searchString += string.Format(" AND cemail.CONTACT_VALUE LIKE('%{0}%')", value);

                    _email = value;
                }
            }
            private decimal _BALANCE;
            public decimal BALANCE
            {
                get
                {
                    return _BALANCE;
                }
                set
                {
                    if (value > 0)
                    {
                        searchString += string.Format(" AND cpa.BALANCE >= {0}", value);
                    }

                    _BALANCE = value;
                }
            }
            private decimal _TotalAccrued;
            public decimal TotalAccrued
            {
                get
                {
                    return _TotalAccrued;
                }
                set
                {
                    _TotalAccrued = value;
                }
            }
            private decimal _TotalSpented;
            public decimal TotalSpented
            {
                get
                {
                    return _TotalSpented;
                }
                set
                {
                    _TotalSpented = value;
                }
            }
            private string _CityAttr;
            public string CityAttr
            {
                get
                {
                    return _CityAttr;
                }
                set
                {
                    if (value != null && value != "")
                        searchString += string.Format(" AND cpv.NAME = '{0}'", value);

                    _CityAttr = value;
                }
            }

            public List<PersonViewModel> persons = new List<PersonViewModel>();
            public void Search()
            {
                SqlConnection con = GetConnection();
                SqlCommand command = new SqlCommand(String.Format(@"
                        select TOP 20
	                        cp.PEOPLE_ID,
	                        cp.BIRTHDAY,
	                        cp.L_NAME,
	                        cp.F_NAME,
	                        cp.M_NAME,
	                        cp.FULL_NAME,
	                        MAX(Case when cpa.ACCOUNT_TYPE_ID = 16 then cpa.BALANCE End) as BALANCE,
							ltrim(STUFF(
							 (SELECT ', ' + CAST (b.CARD_CODE AS varchar(20))
							  FROM CARD_PEOPLES a 
									LEFT JOIN CARD_CARDS b ON a.PEOPLE_ID = b.PEOPLE_ID
							  WHERE a.PEOPLE_ID = cp.PEOPLE_ID
							  FOR XML PATH ('')), 1, 1, ''))  AS CARDS,
							STUFF(
							 (SELECT ', ' + b.CONTACT_VALUE
							  FROM CARD_PEOPLES a 
									LEFT JOIN CARD_CONTACTS b ON a.PEOPLE_ID = b.PEOPLE_ID
							  WHERE a.PEOPLE_ID = cp.PEOPLE_ID and b.CONTACT_TYPE IN (254, 250)
							  FOR XML PATH ('')), 1, 1, '')  AS phones,
	                        STUFF(
							 (SELECT ', ' + b.CONTACT_VALUE
							  FROM CARD_PEOPLES a 
									LEFT JOIN CARD_CONTACTS b ON a.PEOPLE_ID = b.PEOPLE_ID
							  WHERE a.PEOPLE_ID = cp.PEOPLE_ID and b.CONTACT_TYPE IN (252)
							  FOR XML PATH ('')), 1, 1, '')  AS emails,
	                        Sum(Case when ct.transaction_type = 162 then ct.SUMM End) as accrued,
	                        Sum(Case when ct.transaction_type = 161 then ct.SUMM End) as spented,
                            cpv.NAME as CityAttr
                        from 
	                        CARD_PEOPLES cp
	                        LEFT JOIN CARD_PEOPLE_ACCOUNTS cpa ON cp.PEOPLE_ID = cpa.PEOPLE_ID
	                        LEFT JOIN CARD_CARDS cc ON CP.PEOPLE_ID = cc.PEOPLE_ID
	                        LEFT JOIN CARD_CONTACTS cphone ON cp.PEOPLE_ID = cphone.PEOPLE_ID and cphone.CONTACT_TYPE IN (254, 250) and cphone.DELETED = 0
	                        LEFT JOIN CARD_CONTACTS cemail ON cp.PEOPLE_ID = cemail.PEOPLE_ID and cemail.CONTACT_TYPE = 252 and cemail.DELETED = 0
                            LEFT JOIN CARD_TRANSACTIONS ct ON cpa.PEOPLE_ACCOUNT_ID = ct.ACCOUNT_ID
							LEFT JOIN CARD_PEOPLE_PROPERTY_LINKS cppl ON (cp.PEOPLE_ID = cppl.PEOPLE_ID AND cppl.PROPERTY_ID = 2)
							LEFT JOIN CARD_PROPERTY_VALUES cpv ON cppl.PROPERTY_VALUE_ID = cpv.PROPERTY_VALUE_ID
                        {0}
                        GROUP BY 
	                        cp.PEOPLE_ID,
	                        cp.BIRTHDAY,
	                        cp.L_NAME,
	                        cp.F_NAME,
	                        cp.M_NAME,
	                        cp.FULL_NAME,
                            cpv.NAME
                        {1}
                    ", searchString, orderString), con);

                
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        PersonViewModel p = new PersonViewModel();

                        if (record["PEOPLE_ID"] != DBNull.Value)
                            p.PEOPLE_ID = Convert.ToUInt64(record["PEOPLE_ID"]);
                        if (record["BIRTHDAY"] != DBNull.Value)
                            p.BIRTHDAY = Convert.ToDateTime(record["BIRTHDAY"]);
                        if (record["L_NAME"] != DBNull.Value)
                            p.L_NAME = Convert.ToString(record["L_NAME"]);
                        if (record["F_NAME"] != DBNull.Value)
                            p.F_NAME = Convert.ToString(record["F_NAME"]);
                        if (record["M_NAME"] != DBNull.Value)
                            p.M_NAME = Convert.ToString(record["M_NAME"]);
                        if (record["FULL_NAME"] != DBNull.Value)
                            p.FULL_NAME = Convert.ToString(record["FULL_NAME"]);
                        if (record["BALANCE"] != DBNull.Value)
                            p.BALANCE = Convert.ToDecimal(record["BALANCE"]);
                        if (record["CARDS"] != DBNull.Value)
                            p.CARD_CODE = Convert.ToString(record["CARDS"]);
                        if (record["phones"] != DBNull.Value)
                            p.phone = Convert.ToString(record["phones"]);
                        if (record["emails"] != DBNull.Value)
                            p.email = Convert.ToString(record["emails"]);
                        if (record["accrued"] != DBNull.Value)
                            p.accrued = Convert.ToDecimal(record["accrued"]);
                        if (record["spented"] != DBNull.Value)
                            p.spented = Convert.ToDecimal(record["spented"]);
                        if (record["CityAttr"] != DBNull.Value)
                            p.CityAttr = Convert.ToString(record["CityAttr"]);
                        persons.Add(p);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
    }
    public class ReportViewModels
    {
        public class OPDViewModel
        {
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }
            private string whereString = "";
            private string havingString = "";

            public class OPDInfo
            {
                public long PEOPLE_ID { get; set; }
                public int OPD { get; set; }
                public string FULL_NAME { get; set; }
                public long CARD_CODE { get; set; }
                public DateTime DT { get; set; }
            }
            public int count { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public List<OPDInfo> OPDInfoList = new List<OPDInfo>();
            public void Search()
            {
                whereString = String.Format("WHERE ct.transaction_type IN (162, 161) and ct.TRANSACTION_TIME >= '{0}' and ct.TRANSACTION_TIME <= '{1}'", StartDateTime.ToString("yyyy-MM-dd"), EndDateTime.ToString("yyyy-MM-dd"));
                havingString = String.Format("HAVING COUNT(ct.TRANSACTION_ID) >= {0}", count);

                SqlConnection con = GetConnection();
                SqlCommand command = new SqlCommand(String.Format(@"select
	                COUNT(ct.TRANSACTION_ID) as OPD,
                    cp.PEOPLE_ID,
	                cp.FULL_NAME,
	                ct.CARD_CODE,
	                convert(varchar, ct.TRANSACTION_TIME, 104) as DT
                FROM
	                CARD_TRANSACTIONS ct
	                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                INNER JOIN CARD_PEOPLES cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
                {0}
                GROUP BY
                    cp.PEOPLE_ID,
	                cp.FULL_NAME,
	                ct.CARD_CODE,
	                convert(varchar, ct.TRANSACTION_TIME, 104)
                {1}
                ORDER BY OPD DESC", whereString, havingString), con);

                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        OPDInfo opd = new OPDInfo();

                        if (record["PEOPLE_ID"] != DBNull.Value)
                            opd.PEOPLE_ID = Convert.ToInt64(record["PEOPLE_ID"]);
                        if(record["OPD"] != DBNull.Value)
                            opd.OPD = Convert.ToInt32(record["OPD"]);
                        if (record["FULL_NAME"] != DBNull.Value)
                            opd.FULL_NAME = Convert.ToString(record["FULL_NAME"]);
                        if (record["CARD_CODE"] != DBNull.Value)
                            opd.CARD_CODE = Convert.ToInt32(record["CARD_CODE"]);
                        if (record["DT"] != DBNull.Value)
                            opd.DT = Convert.ToDateTime(record["DT"]);

                        OPDInfoList.Add(opd);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        //Начисления и потраты баллов в общем по дням
        public class FPDViewModel
        {
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }
            public class FPDInfo
            {
                public Decimal accrued { get; set; }
                public Decimal spented { get; set; }
                public DateTime DT { get; set; }
            }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public List<FPDInfo> FPDList = new List<FPDInfo>();
            public void Search()
            {
                SqlConnection con = GetConnection();
                SqlCommand command = new SqlCommand(String.Format(@"select
	                Sum(Case when ct.transaction_type = 162 then ct.SUMM End) as accrued,
	                Sum(Case when ct.transaction_type = 161 then ct.SUMM End) as spented,
	                convert(varchar, ct.TRANSACTION_TIME, 104) as DT
                FROM
	                CARD_TRANSACTIONS ct
	                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                INNER JOIN CARD_PEOPLES cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
                WHERE
	                ct.transaction_type IN (162, 161)
	                and 
	                ct.TRANSACTION_TIME >= '{0}' 
	                and 
	                ct.TRANSACTION_TIME <= '{1}'
                GROUP BY
	                convert(varchar, ct.TRANSACTION_TIME, 104)
                ORDER BY DT DESC", StartDateTime.ToString("yyyy-MM-dd"), EndDateTime.ToString("yyyy-MM-dd")), con);

                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        FPDInfo fpd = new FPDInfo();

                        if (record["accrued"] != DBNull.Value)
                            fpd.accrued = Convert.ToDecimal(record["accrued"]);
                        if (record["spented"] != DBNull.Value)
                            fpd.spented = Convert.ToDecimal(record["spented"]);
                        if (record["DT"] != DBNull.Value)
                            fpd.DT = Convert.ToDateTime(record["DT"]);

                        FPDList.Add(fpd);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        //Начисления и потраты баллов в общем по месяцам
        public class FPMViewModel
        {
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }
            public class FPMInfo
            {
                public Decimal accrued { get; set; }
                public Decimal spented { get; set; }
                public int year { get; set; }
                public int month { get; set; }
            }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public List<FPMInfo> FPMList = new List<FPMInfo>();
            public void Search()
            {
                SqlConnection con = GetConnection();
                SqlCommand command = new SqlCommand(String.Format(@"select
	                Sum(Case when ct.transaction_type = 162 then ct.SUMM End) as accrued,
	                Sum(Case when ct.transaction_type = 161 then ct.SUMM End) as spented,
	                DATEPART(year, ct.TRANSACTION_TIME) as y,
	                DATEPART(month, ct.TRANSACTION_TIME) as m
                FROM
	                CARD_TRANSACTIONS ct
	                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                INNER JOIN CARD_PEOPLES cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
                WHERE
	                ct.transaction_type IN (162, 161)
	                and 
	                ct.TRANSACTION_TIME >= '{0}' 
	                and 
	                ct.TRANSACTION_TIME <= '{1}'
                GROUP BY
	                DATEPART(year, ct.TRANSACTION_TIME),
	                DATEPART(month, ct.TRANSACTION_TIME)
                ORDER BY y, m DESC", StartDateTime.ToString("yyyy-MM-dd"), EndDateTime.ToString("yyyy-MM-dd")), con);

                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        FPMInfo fpm = new FPMInfo();

                        if (record["accrued"] != DBNull.Value)
                            fpm.accrued = Convert.ToDecimal(record["accrued"]);
                        if (record["spented"] != DBNull.Value)
                            fpm.spented = Convert.ToDecimal(record["spented"]);
                        if (record["m"] != DBNull.Value)
                            fpm.year = Convert.ToInt32(record["m"]);
                        if (record["y"] != DBNull.Value)
                            fpm.month = Convert.ToInt32(record["y"]);

                        FPMList.Add(fpm);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class MiscReports
        {
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public int Summ { get; set; }
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }

            public List<List<string>> Data = new List<List<string>>();
            //Отчет по потратам за месяц на указанную сумму
            public void SPM()
            {
                int sum = Summ;

                if (sum > 0)
                    sum = -sum;

                string query = String.Format(@"select 
	                -1 * SUM(ct.SUMM) as 'Сумма потрат',
					count(ct.TRANSACTION_ID) as 'Количество операций',
	                ct.CARD_CODE as 'Карта',
	                cp.L_NAME as 'Фамилия',
	                cp.F_NAME as 'Имя',
	                cp.M_NAME as 'Отчество',
	                DATEPART(year, ct.TRANSACTION_TIME) as 'Год',
	                DATEPART(month, ct.TRANSACTION_TIME) as 'Месяц'
                FROM
	                CARD_TRANSACTIONS ct
	                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa on ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                INNER JOIN CARD_PEOPLES cp ON cp.PEOPLE_ID = cpa.PEOPLE_ID
                WHERE
	                ct.TRANSACTION_TIME >= '{0}' and ct.TRANSACTION_TIME <= '{1}'
	                and
	                ct.transaction_type = 161
	                and
	                ct.CARD_CODE IS NOT NULL
                GROUP BY
	                ct.CARD_CODE,
	                cp.L_NAME,
	                cp.F_NAME,
	                cp.M_NAME,
	                DATEPART(year, ct.TRANSACTION_TIME),
	                DATEPART(month, ct.TRANSACTION_TIME)
                HAVING
                    SUM(ct.SUMM) <= {2}", StartDateTime.ToString("yyyy-MM-dd"), EndDateTime.ToString("yyyy-MM-dd"), sum);
                Execute(query);
            }
            public void ASIOD()
            {
                string query = String.Format(@"SELECT 
	                COUNT(t1.ctt) as 'Количество списание-начислений',
	                t1.PEOPLE_ID as 'ID гостя в CRM',
	                t1.FULL_NAME as 'Имя',
	                t1.DT as 'Дата'
                FROM 
                (select
		                ct.TRANSACTION_TYPE as ctt,
		                ct1.TRANSACTION_TYPE as ct1t,
                        cp.PEOPLE_ID,
	                    cp.FULL_NAME,
	                    convert(varchar, ct.TRANSACTION_TIME, 104) as DT
                    FROM
	                    CARD_TRANSACTIONS ct
	                    INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                    INNER JOIN CARD_PEOPLES cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
		                LEFT JOIN CARD_TRANSACTIONS ct1 ON (ct.ACCOUNT_ID = ct1.ACCOUNT_ID and ct1.TRANSACTION_TYPE = 161 and convert(varchar, ct.TRANSACTION_TIME, 104) = convert(varchar, ct1.TRANSACTION_TIME, 104))
                    WHERE 
		                ct.transaction_type = 162
		                and 
		                ct.TRANSACTION_TIME >= '{0}' and ct.TRANSACTION_TIME <= '{1}'
                ) as t1
                WHERE
	                t1.ct1t IS NOT NULL
                GROUP BY 
	                t1.PEOPLE_ID,
	                t1.FULL_NAME,
	                t1.DT
                ORDER BY DT", StartDateTime.ToString("yyyy-MM-dd"), EndDateTime.ToString("yyyy-MM-dd"));
                Execute(query);
            }
            public void CAO()
            {
                string query = String.Format(@"SELECT * 
                FROM (select
	                ct.CARD_CODE as 'Номер карты',
	                cp.FULL_NAME as 'Ф.И.О.',
	                min(ct.SUMM) as minimum
                FROM 
	                CARD_TRANSACTIONS ct
	                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
	                INNER JOIN CARD_PEOPLES cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
	                INNER JOIN CARD_PEOPLE_ACCOUNTS cpa1 ON cp.PEOPLE_ID = cpa1.PEOPLE_ID
                WHERE
	                ct.TRANSACTION_TIME >= '{0}' and ct.TRANSACTION_TIME <= '{1}'
	                and
	                ct.TRANSACTION_TYPE = 112
	                and
	                cpa1.ACCOUNT_TYPE_ID = 16
                GROUP BY 
	                convert(varchar, ct.TRANSACTION_TIME, 104),
	                ct.CARD_CODE,
	                cp.FULL_NAME) as t1
                WHERE t1.minimum >= {2}", StartDateTime.ToString("yyyy-MM-dd"), EndDateTime.ToString("yyyy-MM-dd"), Summ);
                Execute(query);
            }
            private void Execute(string query)
            {
                SqlConnection con = GetConnection();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    List<string> row = new List<string>();

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i));
                    }

                    Data.Add(row);

                    foreach (DbDataRecord record in rdr)
                    {
                        row = new List<string>();

                        for (int i = 0; i < record.FieldCount; i++)
                        {
                            row.Add(record[i].ToString());
                        }

                        Data.Add(row);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
    }
    public class CRMAPIModels
    {
        #region DRC
        public class CRMHolderCardsInfo
        {
            [XmlArray("Holder_Card"), XmlArrayItem("Card")]
            public List<CRMCardInfo> Cards { get; set; }
        }
        public class CRMCardInfo
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

                    SelectListItem sli = Statuses.First(p => p.Value == Status);

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
            public CRM.CRMResponse Save(string UserName)
            {
                string cardXML = "";

                if (Status == "Active")
                {
                    cardXML = String.Format(@"<?xml version=""1.0"" encoding=""Windows-1251"" standalone=""yes"" ?>
                        <Message Action=""Card active"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"" Unit_ID=""1"" User_ID=""{2}"">
                        <Card_Code>{0}</Card_Code>
                        <Expired>{1}</Expired>
                        </Message>", Card_Code, Expired, 1);
                }
                else
                    if (Status == "Inactive")
                    {
                        cardXML = String.Format(@"<?xml version=""1.0"" encoding=""Windows-1251"" standalone=""yes"" ?>
                            <Message Action=""Card inactive"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"" Unit_ID=""1"" User_ID=""{1}"">
                            <Card_Code>{0}</Card_Code>
                            </Message>", Card_Code, 1);
                    }
                    else
                        if (Status == "Blocked")
                        {
                            cardXML = String.Format(@"<?xml version=""1.0"" encoding=""Windows-1251"" standalone=""yes"" ?>
                                <Message Action=""Card block"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj"" Unit_ID=""1"" User_ID=""{2}"">
                                <Card_Code>{0}</Card_Code>
                                <Remarks>{1}</Remarks>
                                </Message>", Card_Code, "Заблокирована пользователем " + UserName, 1);
                        }

                return CRM.Query(cardXML);
            }
        }
        public class CRMHolderContactsInfo
        {
            [XmlArray("Holder_Contact"), XmlArrayItem("Contact")]
            public List<CRMContactInfo> Contacts { get; set; }
        }
        public class CRMContactInfo
        {
            [Display(Name = "ID")]
            [XmlElement("Contact_ID")]
            public string Contact_ID { get; set; }

            [Display(Name = "Тип")]
            [XmlElement("Type_ID")]
            public string Type_ID { get; set; }

            [Display(Name = "Значение")]
            [XmlElement("Value")]
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
        }
        public class CRMAccountInfo
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
        [XmlRoot("Holder")]
        public class DRCCRMHolderInfo
        {
            #region SelectLists
            [XmlIgnore]
            public IEnumerable<SelectListItem> GenderSelectList { get; set; }
            [XmlIgnore]
            public IEnumerable<SelectListItem> MaritalStatusSelectList { get; set; }
            #endregion
            #region SelectedValues
            [XmlIgnore]
            public string SelectedGender { get; set; }
            [XmlIgnore]
            public string SelectedMaritalStatus { get; set; }
            #endregion

            [Display(Name = "ID")]
            [XmlElement("Holder_ID")]
            public string Holder_ID { get; set; }

            [Display(Name = "Код группы")]
            [XmlElement("Group_ID")]
            public string Group_ID { get; set; }

            [Display(Name = "Фамилия")]
            [XmlElement("L_Name")]
            public string L_Name { get; set; }

            [Display(Name = "Имя")]
            [XmlElement("F_Name")]
            public string F_Name { get; set; }

            [Display(Name = "Отчество")]
            [XmlElement("M_Name")]
            public string M_Name { get; set; }

            [Display(Name = "Полное имя")]
            [XmlElement("Full_Name")]
            public string Full_Name { get; set; }

            [Display(Name = "Внешний код")]
            [XmlElement("External_Code")]
            public string External_Code { get; set; }

            [Display(Name = "Код неплательщика")]
            [XmlElement("Unpay_Type_ID")]
            public string Unpay_Type_ID { get; set; }

            [XmlIgnore]
            [Display(Name = "Дата рождения")]
            public DateTime BirthD { get; set; }
            [XmlElement("Birth")]
            [Display(Name = "Дата рождения")]
            public string Birth
            {
                get
                {
                    return BirthD.ToString("yyyy-MM-dd");
                }
                set
                {
                    BirthD = DateTime.Parse(value);
                }
            }

            [Display(Name = "Фотография")]
            [XmlElement("Photo")]
            public string Photo { get; set; }

            [Display(Name = "Пол")]
            [XmlElement("Gender")]
            public string Gender { get; set; }

            [Display(Name = "Семейное положение")]
            [XmlElement("Marrital")]
            public string Marrital { get; set; }

            [Display(Name = "Код языка")]
            [XmlElement("Language_ID")]
            public string Language_ID { get; set; }

            [Display(Name = "Курящий")]
            [XmlElement("Smoke")]
            public string Smoke { get; set; }

            [Display(Name = "Верифицирован")]
            [XmlElement("Verification")]
            public string Verification { get; set; }
            [XmlElement("Deleted")]
            [Display(Name = "Удален")]
            public string Deleted { get; set; }
            [XmlElement("Group_Name")]
            [Display(Name = "Название группы")]
            public string Group_Name { get; set; }
            [XmlElement("Division_ID")]
            [Display(Name = "ID подразделения")]
            public string Division_ID { get; set; }
            [XmlElement("Division_Name")]
            [Display(Name = "Название подразделения")]
            public string Division_Name { get; set; }
            [XmlElement("Remarks")]
            [Display(Name = "Примечание")]
            public string Remarks { get; set; }
            [XmlElement("Cards_Count")]
            [Display(Name = "Количество карт")]
            public string Cards_Count { get; set; }
            [XmlElement("Accounts_Count")]
            [Display(Name = "Количество счетов")]
            public string Accounts_Count { get; set; }
            [XmlElement("Contacts_Count")]
            [Display(Name = "Количество контактов")]
            public string Contacts_Count { get; set; }

            [XmlArray("Accounts"), XmlArrayItem("Account")]
            public List<CRMAccountInfo> Accounts { get; set; }
            [XmlElement("Holders_Contacts")]
            public CRMHolderContactsInfo Holders_Contacts { get; set; }
            [XmlElement("Holders_Cards")]
            public CRMHolderCardsInfo Holders_Cards { get; set; }

            public DRCCRMHolderInfo Deserialize(string xml)
            {
                var xmlSerializer = new XmlSerializer(typeof(DRCCRMHolderInfo));
                var stringReader = new StringReader(xml);
                return (DRCCRMHolderInfo)xmlSerializer.Deserialize(stringReader);
            }
            public DRCCRMHolderInfo GetHolderInfo(Int64 people_id)
            {
                string query = String.Format(@"<?xml version=""1.0"" encoding=""Windows-1251"" standalone=""yes"" ?> <Message Action=""Get holder info"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj""><Include>Holder_Card, Holder_Contact, Account, Holder_NoCount</Include><Holder_ID>{0}</Holder_ID> </Message>", people_id);
                CRM.CRMResponse r = CRM.Query(query);

                if (r.MessageType.ToUpper() != "ERROR")
                {
                    return Deserialize(r.Message);
                }
                else
                    return null;
            }
            public DMCCRMHolderInfo ToHolderInfo()
            {
                DMCCRMHolderInfo h = new DMCCRMHolderInfo();
                h.Holder_ID = Holder_ID;
                h.Group_ID = Group_ID;
                h.L_Name = L_Name;
                h.F_Name = F_Name;
                h.M_Name = M_Name;
                h.External_Code = External_Code;
                h.Unpay_Type_ID = Unpay_Type_ID;
                h.Birth = Birth;
                h.Gender = Gender;
                h.Marrital = Marrital;
                h.Language_ID = Language_ID;
                h.Smoke = Smoke;
                h.Verification = Verification;
                h.Cards = Holders_Cards.Cards;
                h.Contacts = Holders_Contacts.Contacts;
                h.Accounts = Accounts;
                return h;
            }
        }
        #endregion
        #region DMC
        public class DMCCRMRelativesInfo
        {
            [Display(Name = "ID родственника")]
            [XmlElement("Relative_ID")]
            public string Relative_ID { get; set; }

            [Display(Name = "Код типа родства")]
            [XmlElement("Relative_Type_ID")]
            public string Relative_Type_ID { get; set; }
        }
        public class DMCCRMHolderInfo
        {
            [Display(Name = "ID")]
            [XmlElement("Holder_ID")]
            public string Holder_ID { get; set; }

            [Display(Name = "Код группы")]
            [XmlElement("Group_ID")]
            public string Group_ID { get; set; }

            [Display(Name = "Фамилия")]
            [XmlElement("L_Name")]
            public string L_Name { get; set; }

            [Display(Name = "Имя")]
            [XmlElement("F_Name")]
            public string F_Name { get; set; }

            [Display(Name = "Отчество")]
            [XmlElement("M_Name")]
            public string M_Name { get; set; }

            [Display(Name = "Полное имя")]
            [XmlElement("Full_Name")]
            public string Full_Name { get; set; }

            [Display(Name = "Внешний код")]
            [XmlElement("External_Code")]
            public string External_Code { get; set; }

            [Display(Name = "Код неплательщика")]
            [XmlElement("Unpay_Type_ID")]
            public string Unpay_Type_ID { get; set; }

            [XmlIgnore]
            private DateTime _Birth { get; set; }
            [XmlIgnore]
            [Display(Name = "Дата рождения")]
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
                    _Birth = DateTime.Parse(value);
                }
            }

            [Display(Name = "Фотография")]
            [XmlElement("Photo")]
            public string Photo { get; set; }

            [Display(Name = "Пол")]
            [XmlElement("Gender")]
            public string Gender { get; set; }

            [Display(Name = "Семейное положение")]
            [XmlElement("Marrital")]
            public string Marrital { get; set; }

            [Display(Name = "Код языка")]
            [XmlElement("Language_ID")]
            public string Language_ID { get; set; }

            [Display(Name = "Курящий")]
            [XmlElement("Smoke")]
            public string Smoke { get; set; }

            [Display(Name = "Верифицирован")]
            [XmlElement("Verification")]
            public string Verification { get; set; }

            [XmlArray("Cards"), XmlArrayItem("Card")]
            public List<CRMCardInfo> Cards { get; set; }
            [XmlIgnore]
            public List<CRMAccountInfo> Accounts { get; set; }
            [XmlArray("Contacts"), XmlArrayItem("Contact")]
            public List<CRMContactInfo> Contacts { get; set; }
        }
        [XmlRoot("Message")]
        public class CRMHolderInfo : APIMessage
        {
            [XmlIgnore]
            public List<string> ErrorList = new List<string>();
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }
            [XmlElement("Holder")]
            public DMCCRMHolderInfo Holder { get; set; }

            private string Serialize()
            {
                Action = "Edit holders";
                Terminal_Type = "239487KJT3asf";
                Global_Type = "kN3uF2TTVtmpp1Gb25Mj";

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(CRMHolderInfo));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }

            public void EditHolders(string UserName)
            {
                //Сначала сохраним карты
                for (int i = 0; i < Holder.Cards.Count; i++)
                {
                    if (Holder.Cards[i].Holder_ID != null)
                    {
                        CRM.CRMResponse r = Holder.Cards[i].Save(UserName);

                        if (r.MessageType.ToUpper() == "ERROR")
                            ErrorList.Add(r.Message);
                    }
                }

                //Потом сохраним самого чувака или чувиху
                string s = Serialize();

                CRM.CRMResponse r1 = CRM.Query(s);

                if (r1.MessageType.ToUpper() == "ERROR")
                {
                    ErrorList.Add(r1.Message);
                }

                CRMAPIModels.DRCCRMHolderInfo h = new CRMAPIModels.DRCCRMHolderInfo();
                Holder = h.GetHolderInfo(Convert.ToInt64(Holder.Holder_ID)).ToHolderInfo();
            }
            public void AddTransaction(string Account_Number, Decimal bp)
            {
                string query = "";
            }
        }
        #endregion
        public class APIMessage
        {
            [XmlAttribute("Action")]
            public string Action { get; set; }

            [XmlAttribute("Terminal_Type")]
            public string Terminal_Type { get; set; }

            [XmlAttribute("Global_Type")]
            public string Global_Type { get; set; }
            [XmlIgnore]
            public string LastResult { get; set; }
        }
        /*public class AccountInfo
        {
            [XmlElement("Account_Type_ID")]
            public int Account_Type_ID { get; set; }

            [XmlElement("Auto_Change_Levels")]
            public bool Auto_Change_Levels { get; set; }

            [XmlElement("Credit_Depth")]
            public Decimal Credit_Depth { get; set; }

            [XmlElement("Account_Level_ID")]
            public string Account_Level_ID { get; set; }
        }
         * */
        public class ContactInfo
        {
            [XmlElement("Type_ID")]
            public int Type_ID { get; set; }

            [XmlElement("Value")]
            public string Value { get; set; }

            [XmlElement("Notes")]
            public string Notes { get; set; }

            [XmlElement("Dispatch")]
            public bool Dispatch { get; set; }

            [XmlElement("Deleted")]
            public bool Deleted { get; set; }
        }
        public class HolderInfo
        {
            [XmlIgnore]
            public Int64 people_id { get; set; }
            [XmlElement("Group_ID")]
            public int Group_ID { get; set; }

            [XmlElement("L_Name")]
            [Display(Name = "Фамилия")]
            public string L_Name { get; set; }

            [XmlElement("F_Name")]
            [Display(Name = "Имя")]
            public string F_Name { get; set; }

            [XmlElement("M_Name")]
            [Display(Name = "Отчество")]
            public string M_Name { get; set; }

            private DateTime _Birth;
            [XmlElement("Birth")]
            [Display(Name = "Дата рождения")]
            public string Birth
            {
                get
                {
                    return _Birth.ToString("yyyy-MM-dd");
                }
                set
                {
                    _Birth = DateTime.Parse(value);
                }
            }

            [XmlIgnore]
            [Display(Name = "Дата рождения")]
            public DateTime Birth_
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

            [XmlArray("Accounts"), XmlArrayItem("Account")]
            public List<CRMAccountInfo> Accounts { get; set; }

            [XmlArray("Contacts"), XmlArrayItem("Contact")]
            public List<ContactInfo> Contacts { get; set; }
            [XmlArray("Cards"), XmlArrayItem("Card")]
            public List<CRMCardInfo> Cards { get; set; }
        }
        #region CRMAPICLASSES
        public class CRMEditHolder : APIMessage
        {
            [XmlElement("Holder")]
            public DMCCRMHolderInfo Holder { get; set; }
            private string Serialize()
            {
                Action = "Edit holders";
                Terminal_Type = "239487KJT3asf";
                Global_Type = "kN3uF2TTVtmpp1Gb25Mj";

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(CRMEditHolder));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }
            public string EditHolders()
            {
                string s = Serialize();
                return s;
            }
        }
        #endregion
        [XmlRoot("Message")]
        public class HolderViewModel: APIMessage
        {
            [XmlElement("Holder")]
            public HolderInfo Holder { get; set; }

            [XmlIgnore]
            [Display(Name = "Сотовый телефон")]
            public string phone { get; set; }

            [XmlIgnore]
            [Display(Name = "Email")]
            public string email { get; set; }

            [XmlIgnore]
            [Display(Name = "Номер карты")]
            public string Card_Code { get; set; }

            //private List<RKCRM.Holder> Holders = new List<RKCRM.Holder>();

            private HolderViewModel Deserialize(string xml)
            {
                Encoding utf8 = Encoding.GetEncoding("UTF-8");
                Encoding win1251 = Encoding.GetEncoding(1251);

                byte[] utf8Bytes = utf8.GetBytes(xml);
                byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
                xml = utf8.GetString(win1251Bytes);

                var xmlSerializer = new XmlSerializer(typeof(HolderViewModel));
                var stringReader = new StringReader(xml);
                return (HolderViewModel)xmlSerializer.Deserialize(stringReader);
            }
            private string Serialize()
            {
                Terminal_Type = "239487KJT3asf";
                Global_Type = "kN3uF2TTVtmpp1Gb25Mj";

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(typeof(HolderViewModel));
                    xmlSerializer.Serialize(memoryStream, this);
                    memoryStream.Position = 0;
                    TextReader reader = new StreamReader(memoryStream);

                    return reader.ReadToEnd();
                }
            }
            private static SqlConnection GetConnection()
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["crmConnectionString"].ConnectionString;
                SqlConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            }
            public bool AddHolder()
            {
                // 1. Проверяем, есть ли такой владелец у нас в CRM
                

                if (false)
                {
                    return true;
                }
                else
                {
                    LastResult = "Операция завершена успешно";
                    return false;
                }

                return false;
            }
            public void EditHolder()
            { 

            }
            public bool Execute()
            {
                bool result = false;
                bool CardIsOk = true;
                string MessageType = "";
                string Response = "";

                /*if (Card_Code != null && Card_Code != "")
                {
                    SqlConnection con = GetConnection();
                    SqlCommand command = new SqlCommand(String.Format(@"select * from CARD_CARDS 
                    where 
	                    CARD_CODE = {0}
	                    and
	                    PEOPLE_ID is null
	                    and
	                    STATUS = 1
	                    and
	                    deleted = 0
	                    and
	                    GROUP_ID = 44", Card_Code), con);

                    SqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        CardIsOk = true;
                    }

                    rdr.Close();
                    con.Close();
                }
                else
                {
                    CardIsOk = true;

                    if (Holder != null && Holder.people_id != 0)
                    {
                        SqlConnection con = GetConnection();
                        SqlCommand command = new SqlCommand(String.Format(@"UPDATE card_cards SET status = 4 WHERE people_id = {0}", Holder.people_id), con);
                        int cnt = command.ExecuteNonQuery();
                        con.Close();
                    }
                }*/

                if (CardIsOk)
                {
                    string Query = Serialize();

                    Query = String.Format(@"Message-ID: 1
                        Message-Type: Request
                        Time: '2016-10-04 13:42:12'
                        Terminal-Type: 25
                        Content-Length: {0}

                        {1}
                        ", Query.Length, Query);

                    TcpClient client = new TcpClient();
                    IPAddress ip = IPAddress.Parse("10.1.0.108");
                    client.Connect(ip, 9191);
                    NetworkStream tcpStream = client.GetStream();
                    byte[] sendBytes = Encoding.GetEncoding(1251).GetBytes(Query);
                    tcpStream.Write(sendBytes, 0, sendBytes.Length);
                    byte[] bytes = new byte[client.ReceiveBufferSize];
                    int bytesRead = tcpStream.Read(bytes, 0, client.ReceiveBufferSize);
                    string returnData = Encoding.GetEncoding(1251).GetString(bytes);
                    client.Close();

                    Match m = Regex.Match(returnData, "^Message-Type:(?<val>.*?)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                    if (m.Success)
                    {
                        MessageType = m.Groups["val"].ToString().Trim();
                    }

                    m = Regex.Match(returnData, "Content-Length:.*?\r\n(?<val>.*)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (m.Success)
                    {
                        Response = m.Groups["val"].ToString().Trim();
                    }

                    if (MessageType.ToUpper() != "ERROR")
                    {
                        m = Regex.Match(returnData, "<Holder_ID>(?<val>.*?)</Holder_ID>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                        if (m.Success)
                        {
                            string people_id = m.Groups["val"].ToString();
                            Holder.people_id = Convert.ToInt64(people_id);

                            if (Card_Code != null && Card_Code != "")
                            {
                                SqlConnection con = GetConnection();
                                SqlCommand command = new SqlCommand(String.Format(@"UPDATE card_cards SET people_id = {0} WHERE CARD_CODE = {1}", people_id, Card_Code), con);
                                int cnt = command.ExecuteNonQuery();
                                con.Close();

                                if (cnt > 0)
                                {
                                    LastResult = "Операция завершена успешно";
                                    result = true;
                                }
                            }
                            else
                            {
                                LastResult = "Операция завершена успешно";
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        LastResult = Response;
                        result = false;
                    }
                }
                else
                {
                    LastResult = "Карта не найдена, либо невозможно привязать указанную карту.";
                    result = false;
                }

                return result;
            }
            public bool Search(Int64 people_id)
            {
                string MessageType = "";
                string Response = "";
                string query = String.Format(@"<?xml version=""1.0"" encoding=""Windows-1251"" standalone=""yes"" ?> <Message Action=""Get holder info"" Terminal_Type=""239487KJT3asf"" Global_Type=""kN3uF2TTVtmpp1Gb25Mj""><Include>Holder_Card, Account, Holder_Contact</Include><Holder_ID>{0}</Holder_ID> </Message>", people_id);

                CRM.CRMResponse r = CRM.Query(query);

                if (r.MessageType == "ERROR")
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}