using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.Common;

namespace Fusion.Models.RK7
{
    public class RK7HybridModels
    {
        private static SqlConnection GetConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["RKConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        public class PrintCheck
        {
            public Decimal NationalSum { get; set; }
            public Decimal BindedSum { get; set; }
            public int CheckNum { get; set; }
            public string CloseStation { get; set; }
            public string Cashier { get; set; }
            public int GuestCnt { get; set; }
            public string Waiter { get; set; }
            public string tableName { get; set; }
            public DateTime Close { get; set; }
            public int Deleted { get; set; }
            public DateTime Open { get; set; }
            public void GetPrintCheck(string guidstring)
            {
                SqlConnection con = GetConnection();
                SqlCommand command = new SqlCommand(String.Format("SELECT PrintChecks.NATIONALSUM AS 'NATIONALSUM', PrintChecks.BINDEDSUM AS 'BINDEDSUM', PrintChecks.CHECKNUM AS 'CHECKNUM', CASHES00.NAME AS 'CLOSESTATION', EMPLOYEES00.NAME AS 'CASHIER', PrintChecks.GUESTCNT AS 'GUESTCNT', EMPLOYEES01.NAME AS 'WAITER', TABLES00.NAME AS 'TABLE', PrintChecks.STATE AS 'CHECKSTATE', Orders00.ORDERNAME AS 'ORDERNAME', PrintChecks.CLOSEDATETIME AS 'Close', PrintChecks.DELETED AS 'DELETED', PrintChecks.OPENVOIDNAME AS 'OPENVOIDNAME', Orders00.OPENTIME AS 'Open' FROM PRINTCHECKS LEFT JOIN CASHES CASHES00 ON (CASHES00.SIFR = PrintChecks.iCloseStation) LEFT JOIN EMPLOYEES EMPLOYEES00 ON (EMPLOYEES00.SIFR = PrintChecks.iAuthor) JOIN Orders Orders00 ON (Orders00.Visit = PrintChecks.Visit) AND (Orders00.MidServer = PrintChecks.MidServer) AND (Orders00.IdentInVisit = PrintChecks.OrderIdent) LEFT JOIN EMPLOYEES EMPLOYEES01 ON (EMPLOYEES01.SIFR = Orders00.MainWaiter) LEFT JOIN TABLES TABLES00 ON (TABLES00.SIFR = Orders00.TableID) WHERE (PrintChecks.IGNOREINREP = 0) AND ((PrintChecks.STATE = 6) OR (PrintChecks.STATE = 7)) AND printchecks.guidstring = '{0}'", guidstring), con);
                SqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.NationalSum = Convert.ToDecimal(record["NATIONALSUM"]);
                        this.BindedSum = Convert.ToDecimal(record["BINDEDSUM"]);
                        this.CheckNum = Convert.ToInt32(record["CHECKNUM"]);
                        this.CloseStation = record["CLOSESTATION"].ToString();
                        this.Cashier = record["CASHIER"].ToString();
                        this.GuestCnt = Convert.ToInt32(record["GUESTCNT"]);
                        this.Waiter = record["WAITER"].ToString();
                        this.tableName = record["TABLE"].ToString();
                        this.Close = Convert.ToDateTime(record["Close"]);
                        this.Deleted = Convert.ToInt32(record["DELETED"]);
                        this.Open = Convert.ToDateTime(record["Open"]);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class Order 
        {
        }
        public class Bonustypes
        { 

        }
    }
}