using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using Npgsql;
using System.Web.Configuration;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;

namespace Fusion.Models
{
    public class ReportModels
    {
        private static NpgsqlConnection PGConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BCConnectionString"].ConnectionString;
            NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }
        private static SqlConnection SqlServerConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        public class TableReportModel
        {
            public DateTime start_dt { get; set; }
            public DateTime end_dt { get; set; }
            public List<List<string>> viewData { get; set; }
            public void PTReport()
            {
                SqlConnection con = SqlServerConnection();
                SqlCommand command = new SqlCommand(String.Format("SELECT t1.rest as 'Ресторан', YEAR(t1.shiftdate) as 'Год', Month(t1.shiftdate) as 'Месяц', SUM(t1.GUESTCNT) as 'Общее гости', SUM(CASE WHEN t1.tablename <> '61' AND t1.tablename <> '62' AND t1.tablename <> '63' AND t1.tablename <> '60' THEN t1.GUESTCNT ELSE 0 END) as 'Зал гостей', SUM(CASE WHEN (t1.tablename = '61' OR t1.tablename = '62' OR t1.tablename = '60') AND (t1.code = 9001 OR t1.code = 3 OR t1.code = 1) THEN t1.GUESTCNT ELSE 0 END) as 'Доставка гостей', SUM(CASE WHEN (t1.tablename = '61' OR t1.tablename = '63' OR t1.tablename = '60') AND (t1.code = 9001 OR t1.code = 2 OR t1.code = 1) THEN t1.GUESTCNT ELSE 0 END) as 'Вынос гостей', SUM(t1.PRLISTSUM) as 'Общее сумма', SUM(CASE WHEN t1.tablename <> '61' AND t1.tablename <> '62' AND t1.tablename <> '63' AND t1.tablename <> '60' THEN t1.PRLISTSUM ELSE 0 END) as 'Зал сумма', SUM(CASE WHEN (t1.tablename = '61' OR t1.tablename = '62' OR t1.tablename = '60') AND (t1.code = 9001 OR t1.code = 3 OR t1.code = 1) THEN t1.PRLISTSUM ELSE 0 END) as 'Доставка сумма', SUM(CASE WHEN (t1.tablename = '61' OR t1.tablename = '63' OR t1.tablename = '60') AND (t1.code = 9001 OR t1.code = 2 OR t1.code = 1) THEN t1.PRLISTSUM ELSE 0 END) as 'Вынос сумма' FROM (SELECT DISTINCT PrintChecks00.CHECKNUM, RESTAURANTS00.NAME as 'rest', GLOBALSHIFTS00.SHIFTDATE as 'shiftdate', Tables00.Name as 'tablename', v.GUESTCNT, PrintChecks00.PRLISTSUM, CHANGEABLEORDERTYPES00.code FROM PAYBINDINGS JOIN CurrLines CurrLines00 ON (CurrLines00.Visit = PayBindings.Visit) AND (CurrLines00.MidServer = PayBindings.MidServer) AND (CurrLines00.UNI = PayBindings.CurrUNI) JOIN VISITS v ON (CurrLines00.VISIT = v.SIFR) AND (v.MIDSERVER = CurrLines00.MIDSERVER) JOIN PrintChecks PrintChecks00 ON (PrintChecks00.Visit = CurrLines00.Visit) AND (PrintChecks00.MidServer = CurrLines00.MidServer) AND (PrintChecks00.UNI = CurrLines00.CheckUNI) JOIN Orders Orders00 ON (Orders00.Visit = PayBindings.Visit) AND (Orders00.MidServer = PayBindings.MidServer) AND (Orders00.IdentInVisit = PayBindings.OrderIdent) LEFT JOIN EMPLOYEES EMPLOYEES00 ON (EMPLOYEES00.SIFR = Orders00.MainWaiter) LEFT JOIN SaleObjects SaleObjects00 ON (SaleObjects00.Visit = PayBindings.Visit) AND (SaleObjects00.MidServer = PayBindings.MidServer) AND (SaleObjects00.DishUNI = PayBindings.DishUNI) AND (SaleObjects00.ChargeUNI = PayBindings.ChargeUNI) LEFT JOIN DishDiscounts DishDiscounts00 ON (DishDiscounts00.Visit = SaleObjects00.Visit) AND (DishDiscounts00.MidServer = SaleObjects00.MidServer) AND (DishDiscounts00.UNI = SaleObjects00.ChargeUNI) JOIN GLOBALSHIFTS GLOBALSHIFTS00 ON (GLOBALSHIFTS00.MidServer = Orders00.MidServer) AND (GLOBALSHIFTS00.ShiftNum = Orders00.iCommonShift) LEFT JOIN OrderSessions OrderSessions00 ON (OrderSessions00.Visit = SaleObjects00.Visit) AND (OrderSessions00.MidServer = SaleObjects00.MidServer) AND (OrderSessions00.UNI = SaleObjects00.SessionUNI) LEFT JOIN SessionDishes SessionDishes00 ON (SessionDishes00.Visit = SaleObjects00.Visit) AND (SessionDishes00.MidServer = SaleObjects00.MidServer) AND (SessionDishes00.UNI = SaleObjects00.DishUNI) LEFT JOIN MENUITEMS MENUITEMS00 ON (MENUITEMS00.SIFR = SessionDishes00.Sifr) LEFT JOIN CASHES CASHES00 ON (CASHES00.SIFR = PrintChecks00.iCloseStation) LEFT JOIN CASHGROUPS CASHGROUPS00 ON (CASHGROUPS00.SIFR = PayBindings.Midserver) LEFT JOIN CURRENCYTYPES CURRENCYTYPES00 ON (CURRENCYTYPES00.SIFR = CurrLines00.iHighLevelType) LEFT JOIN CURRENCIES CURRENCIES00 ON (CURRENCIES00.SIFR = CurrLines00.Sifr) LEFT JOIN DISHGROUPS DISHGROUPS0000 ON (DISHGROUPS0000.CHILD = MENUITEMS00.SIFR) AND (DISHGROUPS0000.CLASSIFICATION = 512) LEFT JOIN CLASSIFICATORGROUPS CLASSIFICATORGROUPS0000 ON CLASSIFICATORGROUPS0000.SIFR * 256 + CLASSIFICATORGROUPS0000.NumInGroup = DISHGROUPS0000.PARENT LEFT JOIN Shifts Shifts00 ON (Shifts00.MidServer = PrintChecks00.MidServer) AND (Shifts00.iStation = PrintChecks00.iCloseStation) AND (Shifts00.ShiftNum = PrintChecks00.iShift) LEFT JOIN TABLES TABLES00 ON (TABLES00.SIFR = Orders00.TableID) LEFT JOIN PaymentsExtra PaymentsExtra00 ON (PaymentsExtra00.Visit = CurrLines00.Visit) AND (PaymentsExtra00.MidServer = CurrLines00.MidServer) AND (PaymentsExtra00.PayUNI = CurrLines00.PayUNIForOwnerInfo) LEFT JOIN trk7EnumsValues trk7EnumsValues1E00 ON (trk7EnumsValues1E00.EnumData = SaleObjects00.OBJKIND) AND (trk7EnumsValues1E00.EnumName = 'tSaleObjectKind') LEFT JOIN RESTAURANTS RESTAURANTS00 ON (RESTAURANTS00.SIFR = CASHGROUPS00.Restaurant) LEFT JOIN EMPLOYEES EMPLOYEES01 ON (EMPLOYEES01.SIFR = Orders00.iCreator) LEFT JOIN SessionDishes SessionDishes01 ON (SessionDishes01.Visit = SessionDishes00.Visit) AND (SessionDishes01.Midserver = SessionDishes00.Midserver) AND (SessionDishes01.UNI = SessionDishes00.ComboDishUNI) LEFT JOIN MENUITEMS MENUITEMS01 ON (MENUITEMS01.SIFR = SessionDishes01.Sifr) LEFT JOIN trk7EnumsValues trk7EnumsValues3600 ON (trk7EnumsValues3600.EnumData = GLOBALSHIFTS00.STATUS) AND (trk7EnumsValues3600.EnumName = 'TRecordStatus') LEFT JOIN CATEGLIST CATEGLIST00 ON (CATEGLIST00.SIFR = MENUITEMS00.PARENT) LEFT JOIN CHANGEABLEORDERTYPES CHANGEABLEORDERTYPES00 ON (CHANGEABLEORDERTYPES00.SIFR = Orders00.COT) LEFT JOIN UNCHANGEABLEORDERTYPES UNCHANGEABLEORDERTYPES00 ON (UNCHANGEABLEORDERTYPES00.SIFR = Orders00.UOT) LEFT JOIN DISHGROUPS DISHGROUPS0001 ON (DISHGROUPS0001.CHILD = MENUITEMS00.SIFR) AND (DISHGROUPS0001.CLASSIFICATION = 2560) LEFT JOIN CLASSIFICATORGROUPS CLASSIFICATORGROUPS0001 ON CLASSIFICATORGROUPS0001.SIFR * 256 + CLASSIFICATORGROUPS0001.NumInGroup = DISHGROUPS0001.PARENT WHERE ((PrintChecks00.STATE = 6)) AND (GLOBALSHIFTS00.SHIFTDATE BETWEEN {0} AND {2}) AND ((GLOBALSHIFTS00.STATUS = 3))) t1 GROUP BY t1.rest, YEAR(t1.shiftdate), Month(t1.shiftdate) ORDER BY 1, 2, 3", start_dt, end_dt), con);
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    this.viewData = new List<List<string>>();
                    List<string> row = new List<string>();

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i));
                    }

                    this.viewData.Add(row);

                    foreach (DbDataRecord record in rdr)
                    {
                        row = new List<string>();

                        for (int i = 0; i < record.FieldCount; i++)
                        {
                            row.Add(record[i].ToString());
                        }

                        this.viewData.Add(row);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class CRMAnalyticsModel
        {
            public class Item
            {
                public string Restaurant { get; set; }
                public int GuestCnt { get; set; }
                public int VisitCount { get; set; }
                public string FullName { get; set; }
                public long PeopleID { get; set; }
                public int Day { get; set; }
                public int Month { get; set; }
                public int Year { get; set; }
                public Decimal RealMoney { get; set; }
                public Decimal UnrealMoney { get; set; }
                public Decimal AvgCheck { get; set; }
                public Decimal AvgCheckPG { get; set; }
            }

            public List<Item> Items = new List<Item>();

            public void GetGuestDynamics(DateTime startdDT, DateTime endDT)
            {
                SqlConnection con = SqlServerConnection();
                SqlCommand cmd = new SqlCommand(String.Format(@"SELECT
	COUNT(*) AS GuestCNT,
	MIDSERVER,
	Restaurant,
	SUM(REALSUM) AS REALSUM,
	SUM(UNREALSUM) AS UNREALSUM,
	AVG(AVGSUMPP) AS AVGSUMPP,
	AVG(AVGSUM) AS AVGSUM,
	DT
FROM
(SELECT 
	DISTINCT
	T1.ACCOUNT_ID,
	T1.MIDSERVER,
	T1.Restaurant,
	T1.TABLEID,
	T1.PEOPLE_ID,
	T1.FULL_NAME,
	SUM(T1.REALSUM) AS REALSUM,
	SUM(T1.UNREALSUM) AS UNREALSUM,
	AVG(AVGSUMPP) AS AVGSUMPP,
	AVG(AVGSUM) AS AVGSUM,
	DT
FROM
	(select 
		DISTINCT
		CT.ACCOUNT_ID,
		PC.MIDSERVER,
		CG.NAME AS Restaurant,
		O.TABLEID,
		cp.PEOPLE_ID,
		cp.FULL_NAME,
		IIF (C.USEBONUSPERCENT > 0, CL.BINDEDSUM, 0) AS REALSUM,
		IIF (C.USEBONUSPERCENT = 0, CL.BINDEDSUM, 0) AS UNREALSUM,
		AVG(PC.BINDEDSUM / IIF (O.GUESTSCOUNT = 0, 1, O.GUESTSCOUNT)) AS AVGSUMPP,
		AVG(PC.BINDEDSUM) as AVGSUM,
		convert(varchar, ct.TRANSACTION_TIME, 104) as DT
	from [CRM7U].[dbo].[CARD_TRANSACTIONS] CT
		INNER JOIN [CRM7U].[dbo].[CARD_PEOPLE_ACCOUNTS] cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
		INNER JOIN [CRM7U].[dbo].[CARD_PEOPLES] cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
		INNER JOIN [RK7].[dbo].[PRINTCHECKS] PC ON PC.CHECKNUM = CT.EXTERNAL_ID AND 
			DATEDIFF(DAY, '1899-12-30 00:00:00', ToDateTimeOffset(PC.CLOSEDATETIME, '+10:00')) = DATEDIFF(DAY, '1899-12-30 00:00:00', CT.TRANSACTION_TIME) AND 
			PC.CLOSEDATETIME BETWEEN '{0} 09:00' and '{1} 04:00'
		INNER JOIN [RK7].[dbo].[ORDERS] O ON PC.VISIT = O.VISIT AND PC.MIDSERVER = O.MIDSERVER AND O.PRICELISTSUM <> 0
		INNER JOIN [RK7].[dbo].[CURRLINES] CL ON PC.VISIT = CL.VISIT AND PC.MIDSERVER = CL.MIDSERVER
		INNER JOIN [RK7].[dbo].[CURRENCIES] C ON CL.SIFR = C.SIFR
		INNER JOIN [RK7].[dbo].[CASHGROUPS] CG ON PC.MIDSERVER = CG.SIFR
	WHERE CT.transaction_type IN (162, 161)
	GROUP BY 
		CT.ACCOUNT_ID,
		PC.MIDSERVER,
		O.TABLEID,
		CG.NAME,
		cp.PEOPLE_ID,
		cp.FULL_NAME,
		IIF (C.USEBONUSPERCENT > 0, CL.BINDEDSUM, 0),
		IIF (C.USEBONUSPERCENT = 0, CL.BINDEDSUM, 0),
		convert(varchar, ct.TRANSACTION_TIME, 104)) T1
	GROUP BY
		T1.ACCOUNT_ID,
		T1.MIDSERVER,
		T1.Restaurant,
		T1.TABLEID,
		T1.PEOPLE_ID,
		T1.FULL_NAME,
		DT) T2
GROUP BY MIDSERVER, Restaurant, DT", startdDT.ToString("yyyy-MM-dd"), endDT.AddDays(1).ToString("yyyy-MM-dd")), con);

                SqlDataReader rdr = cmd.ExecuteReader();

                foreach (var row in rdr)
                {
                    Item i = new Item()
                    {
                        Restaurant = rdr["Restaurant"].ToString(),
                        GuestCnt = Convert.ToInt32(rdr["GuestCNT"]),
                        AvgCheck = Convert.ToDecimal(rdr["AVGSUM"]),
                        AvgCheckPG = Convert.ToDecimal(rdr["AVGSUMPP"]),
                        Day = Convert.ToDateTime(rdr["DT"]).Day,
                        Month = Convert.ToDateTime(rdr["DT"]).Month,
                        Year = Convert.ToDateTime(rdr["DT"]).Year,
                        RealMoney = Convert.ToDecimal(rdr["REALSUM"]),
                        UnrealMoney = Convert.ToDecimal(rdr["UNREALSUM"])
                    };
                    Items.Add(i);
                }
            }

            public void GetGuestVisitsDynamics(DateTime startdDT, DateTime endDT)
            {
                SqlConnection con = SqlServerConnection();
                SqlCommand cmd = new SqlCommand(String.Format(@"SELECT 
	COUNT(PEOPLE_ID) AS VISIT,
	PEOPLE_ID,
	FULL_NAME,
	SUM(REALSUM) AS REALSUM,
	SUM(UNREALSUM) AS UNREALSUM,
	AVG(AVGSUMPP) AS AVGSUMPP,
	AVG(AVGSUM) AS AVGSUM,
	DATEPART(MONTH, DT) AS MON,
	DATEPART(YEAR, DT) AS Y
FROM
(SELECT 
	DISTINCT
	T1.ACCOUNT_ID,
	T1.PEOPLE_ID,
	T1.FULL_NAME,
	SUM(T1.REALSUM) AS REALSUM,
	SUM(T1.UNREALSUM) AS UNREALSUM,
	AVG(AVGSUMPP) AS AVGSUMPP,
	AVG(AVGSUM) AS AVGSUM,
	DT
FROM
	(select 
		DISTINCT
		CT.ACCOUNT_ID,
		PC.MIDSERVER,
		CG.NAME AS Restaurant,
		O.TABLEID,
		cp.PEOPLE_ID,
		cp.FULL_NAME,
		IIF (C.USEBONUSPERCENT > 0, CL.BINDEDSUM, 0) AS REALSUM,
		IIF (C.USEBONUSPERCENT = 0, CL.BINDEDSUM, 0) AS UNREALSUM,
		AVG(PC.BINDEDSUM / IIF (O.GUESTSCOUNT = 0, 1, O.GUESTSCOUNT)) AS AVGSUMPP,
		AVG(PC.BINDEDSUM) as AVGSUM,
		ct.TRANSACTION_TIME as DT
	from [CRM7U].[dbo].[CARD_TRANSACTIONS] CT
		INNER JOIN [CRM7U].[dbo].[CARD_PEOPLE_ACCOUNTS] cpa ON ct.ACCOUNT_ID = cpa.PEOPLE_ACCOUNT_ID
		INNER JOIN [CRM7U].[dbo].[CARD_PEOPLES] cp ON cpa.PEOPLE_ID = cp.PEOPLE_ID
		INNER JOIN [RK7].[dbo].[PRINTCHECKS] PC ON PC.CHECKNUM = CT.EXTERNAL_ID AND 
			DATEDIFF(DAY, '1899-12-30 00:00:00', ToDateTimeOffset(PC.CLOSEDATETIME, '+10:00')) = DATEDIFF(DAY, '1899-12-30 00:00:00', CT.TRANSACTION_TIME) AND 
			PC.CLOSEDATETIME BETWEEN '{0} 09:00' and '{1} 04:00'
		INNER JOIN [RK7].[dbo].[ORDERS] O ON PC.VISIT = O.VISIT AND PC.MIDSERVER = O.MIDSERVER AND O.PRICELISTSUM <> 0
		INNER JOIN [RK7].[dbo].[CURRLINES] CL ON PC.VISIT = CL.VISIT AND PC.MIDSERVER = CL.MIDSERVER
		INNER JOIN [RK7].[dbo].[CURRENCIES] C ON CL.SIFR = C.SIFR
		INNER JOIN [RK7].[dbo].[CASHGROUPS] CG ON PC.MIDSERVER = CG.SIFR
	WHERE CT.transaction_type IN (162, 161)
	GROUP BY 
		CT.ACCOUNT_ID,
		PC.MIDSERVER,
		O.TABLEID,
		CG.NAME,
		cp.PEOPLE_ID,
		cp.FULL_NAME,
		IIF (C.USEBONUSPERCENT > 0, CL.BINDEDSUM, 0),
		IIF (C.USEBONUSPERCENT = 0, CL.BINDEDSUM, 0),
		ct.TRANSACTION_TIME) T1
	GROUP BY
		T1.ACCOUNT_ID,
		T1.PEOPLE_ID,
		T1.FULL_NAME,
		DT) T2
GROUP BY 
	PEOPLE_ID, FULL_NAME, DATEPART(MONTH, DT), DATEPART(YEAR, DT)
ORDER BY 1", startdDT.ToString("yyyy-MM-dd"), endDT.AddDays(1).ToString("yyyy-MM-dd")), con);

                SqlDataReader rdr = cmd.ExecuteReader();

                foreach (var row in rdr)
                {
                    Item i = new Item()
                    {
                        FullName = rdr["FULL_NAME"].ToString(),
                        VisitCount = Convert.ToInt32(rdr["VISIT"]),
                        PeopleID = Convert.ToInt64(rdr["PEOPLE_ID"]),
                        AvgCheck = Convert.ToDecimal(rdr["AVGSUM"]),
                        AvgCheckPG = Convert.ToDecimal(rdr["AVGSUMPP"]),
                        Month = Convert.ToInt32(rdr["MON"]),
                        Year = Convert.ToInt32(rdr["Y"]),
                        RealMoney = Convert.ToDecimal(rdr["REALSUM"]),
                        UnrealMoney = Convert.ToDecimal(rdr["UNREALSUM"])
                    };
                    Items.Add(i);
                }
            }
        }
    }
    public class RSS
    {
        public string uri { get; set; }
        public string rssTitle = "";
        public class rssNewsItem
        {
            public string newsLink { get; set; }
            public string newsTitle { get; set; }
            public string newsPhoto { get; set; }
            public string newsDescription { get; set; }
            public string pubDate { get; set; }
            public string category { get; set; }
        }
        public List<rssNewsItem> rssNews = new List<rssNewsItem>();
        public List<RSS> channels = new List<RSS>();
        public void getRss(string url)
        {
            XmlReader rdr = XmlReader.Create(url);
            SyndicationFeed news = SyndicationFeed.Load(rdr);
            rdr.Close();

            RSS channel = channels.FirstOrDefault(p => p.uri == url);

            if (channel == null)
            {
                channel = new RSS();
                channel.uri = url;
                channel.rssTitle = news.Title.Text;

                foreach (SyndicationItem newsItem in news.Items)
                {
                    rssNewsItem n = new rssNewsItem();
                    n.newsLink = newsItem.Links[0].Uri.ToString();
                    n.newsTitle = newsItem.Title.Text;
                    n.newsDescription = Regex.Replace(newsItem.Summary.Text, @"<div>.*?</div>", "", RegexOptions.Singleline);
                    n.newsDescription = Regex.Replace(n.newsDescription, @"<.*?>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline).Trim();
                    n.pubDate = newsItem.PublishDate.ToString("dd.MM.yyyy");
                    n.category = "";

                    channel.rssNews.Add(n);
                }

                channels.Add(channel);
            }
            else
            {
                foreach (SyndicationItem newsItem in news.Items)
                {
                    rssNewsItem n = new rssNewsItem();
                    n.newsLink = newsItem.Links[0].Uri.ToString();
                    n.newsTitle = newsItem.Title.Text;
                    n.newsPhoto = newsItem.Links[1].Uri.ToString();
                    n.newsDescription = Regex.Replace(newsItem.Summary.Text, @"<div>.*?</div>", "", RegexOptions.Singleline);
                    n.newsDescription = Regex.Replace(n.newsDescription, @"<.*?>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline).Trim();
                    n.pubDate = newsItem.PublishDate.ToString("dd.MM.yyyy");
                    n.category = "";

                    channel.rssNews.Add(n);
                }
            }

        }
    }
}