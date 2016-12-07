using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Npgsql;
using NpgsqlTypes;
using System.Data.Common;
using System.Web.Configuration;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Fusion.Models.BC
{
    public class BCModels
    {
        private static NpgsqlConnection GetConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BCConnectionString"].ConnectionString;
            NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }
        public class BCSecurity
        {
            public class OPDData
            {
                public int count { get; set; }
                public string card_no { get; set; }
                [DataType(DataType.Date)]
                [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
                public DateTime dt { get; set; }
                public string fullname { get; set; }
                public int party_id { get; set; }
                public List<OPDData> GetOPD(int opd, string order_by, string order_direction, int limit, int offset, DateTime start_dt, DateTime end_dt)
                {
                    List<OPDData> opddataList = new List<OPDData>();
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand com = new NpgsqlCommand(String.Format("select count(rr.receipt_no) as cnt, dc_card_no, date(rr.creation_date) as dt, pp.name, pp.id as party_id from retail_receipt rr INNER JOIN retail_discountcard rdc ON rr.dc_card_no = rdc.card_no INNER JOIN organization_counterparty ocp ON rdc.owner_id = ocp.id INNER JOIN party_party pp ON ocp.party_id = pp.id WHERE rr.status = 'fixed' and date(rr.creation_date) >= '{0}' and date(rr.creation_date) <= '{1}' group by dc_card_no, pp.name, date(creation_date), pp.id having count(rr.id) >= {2} ORDER BY date(creation_date)", start_dt, end_dt, opd), con);
                    NpgsqlDataReader rdr = com.ExecuteReader();

                    if(rdr.HasRows)
                        foreach (DbDataRecord record in rdr)
                        {
                            OPDData opddata = new OPDData();
                            opddata.count = Convert.ToInt32(record["cnt"]);
                            opddata.card_no = record["dc_card_no"].ToString();
                            opddata.dt = Convert.ToDateTime(record["dt"]);
                            opddata.fullname = record["name"].ToString();
                            opddata.party_id = Convert.ToInt32(record["party_id"]);
                            opddataList.Add(opddata);
                        }

                    rdr.Close();
                    con.Close();
                    return opddataList;
                }
            }
        }
        public class SavingAccounts : List<Retail.TSavingAccount>
        {
            public void GetListByPartyID(int _party_id)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_savingaccount WHERE owner_id IN (SELECT id FROM organization_counterparty WHERE party_id = {0}) ORDER BY id", _party_id), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TSavingAccount sa = new Retail.TSavingAccount();
                        sa.id = Convert.ToInt32(record["id"]);
                        sa.account_no = record["account_no"].ToString();
                        sa.creation_date = Convert.ToDateTime(record["creation_date"]);
                        sa.bp = Convert.ToInt32(record["bp"]);
                        sa.owner_id = Convert.ToInt32(record["owner_id"]);
                        sa.domain_id = Convert.ToInt32(record["domain_id"]);
                        this.Add(sa);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class Log
        {

        }
        public class Misc
        {
            //Общая сумма бонусных баллов на счетах
            public int total_bp { get; set; }
            //Общая сумма начисленных баллов
            public Decimal total_bp_accrued { get; set; }
            //Общая сумма потраченных баллов
            public Decimal total_bp_spent { get; set; }
            //Общая сумма полученных баллов участником
            public Decimal party_total_accrued_pb { get; set; }
            //Общая сумма потраченных баллов участником
            public Decimal party_total_paid_bp { get; set; }
            public Decimal party_total { get; set; }
            public Decimal party_sub_total { get; set; }
            //Общее количество персон
            public int total_party_count { get; set; }
            //Общее количество свободных (не привязанных) карт
            public int total_free_cards_count { get; set; }
            //Количество владельцев карт без счета
            public int WithoutSavingAccountTotal { get; set; }
            public void GetTotalBP()
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(bp) FROM retail_savingaccount"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.total_bp = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetTotalBPAccrued()
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(total_accrued_bp) FROM retail_receipt WHERE status = 'fixed' and solution = 'success'"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.total_bp_accrued = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetTotalBPSpent()
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(total_paid_bp) FROM retail_receipt WHERE status = 'fixed' and solution = 'success'"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.total_bp_spent = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetSpentAndAccrualsBPForParty(int party_id)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(total_accrued_bp), SUM(total_paid_bp), SUM(total), SUM(sub_total) FROM retail_receipt WHERE status = 'fixed' and dc_card_no IN (SELECT card_no FROM retail_discountcard WHERE owner_id IN (SELECT id FROM organization_counterparty WHERE party_id = {0}))", party_id), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.party_total_accrued_pb = Convert.ToDecimal(record[0]);
                        this.party_total_paid_bp = Convert.ToDecimal(record[1]);
                        this.party_total = Convert.ToDecimal(record[2]);
                        this.party_sub_total = Convert.ToDecimal(record[3]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetTotalPersonCount()
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT COUNT(id) FROM party_party"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.total_party_count = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetFreeCardsCount()
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT COUNT(id) FROM retail_discountcard WHERE owner_id IS NULL"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        this.total_free_cards_count = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void CreateSavingAccountsForEmpty()
            {
                try
                {
                    List<int> cpids = new List<int>();
                    //Создание пати
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("select DISTINCT id from organization_counterparty ocp WHERE id NOT IN (SELECT DISTINCT owner_id FROM retail_savingaccount WHERE owner_id IS NOT NULL)"), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord row in rdr)
                        {
                            int cpid = Convert.ToInt32(row["id"]);
                            cpids.Add(cpid);
                        }
                    }

                    rdr.Close();

                    for (int i = 0; i < cpids.Count; i++)
                    {
                        //Создание и привязка накопительного счета
                        //Чтение последовательности и префикса!
                        command.CommandText = String.Format("SELECT * FROM retail_sequence WHERE id = 1");
                        rdr = command.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            string prefix = "";
                            ulong number = 0;
                            int initial_bp = 0;

                            foreach (DbDataRecord record in rdr)
                            {
                                prefix = record["prefix"].ToString();
                                number = Convert.ToUInt32(record["number"]);
                            }

                            rdr.Close();

                            //Проверка наличия счета у владельца
                            command.CommandText = String.Format("SELECT * FROM retail_savingaccount WHERE owner_id = {0}", cpids[i]);
                            rdr = command.ExecuteReader();

                            bool isHaveSavingAccount = false;

                            if (rdr.HasRows)
                            {
                                foreach (DbDataRecord record in rdr)
                                {
                                    isHaveSavingAccount = true;
                                }
                            }

                            rdr.Close();

                            if (!isHaveSavingAccount)
                            {
                                command.CommandText = String.Format("UPDATE retail_sequence SET number = {0} WHERE id = 1", number + 1);
                                command.ExecuteNonQuery();
                                //Начальное количество баллов
                                command.CommandText = String.Format("SELECT value FROM retail_domainsetting WHERE domain_id = 2 AND name = 'initial_bp'");
                                rdr = command.ExecuteReader();

                                if (rdr.HasRows)
                                {
                                    foreach (DbDataRecord record in rdr)
                                    {
                                        initial_bp = Convert.ToInt32(record["value"]);
                                    }
                                }

                                rdr.Close();
                                command.CommandText = String.Format("INSERT INTO retail_savingaccount (account_no, creation_date, bp, owner_id, domain_id) VALUES('{0}', now(), {1}, {2}, 2)", prefix + number.ToString(), initial_bp, cpids[i]);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public void GetWithoutSavingAccountTotalCount()
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("select COUNT(id) as cnt from organization_counterparty ocp WHERE id NOT IN (SELECT DISTINCT owner_id FROM retail_savingaccount WHERE owner_id IS NOT NULL)"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord row in rdr)
                    {
                        this.WithoutSavingAccountTotal = Convert.ToInt32(row["cnt"]);
                    }
                }

                rdr.Close();
                con.Close();
            }
            
        }
        public class Report
        { 
            //Отчет по количеству потраченных/начисленных баллов в разбивке по дням
            public List<List<string>> GetDailySpentAccruedBP(DateTime start_dt, DateTime end_dt)
            {
                BC.BCViewModels.TableReportViewModel reportData = new BCViewModels.TableReportViewModel();
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(total_paid_bp) as \"Списано\", SUM(total_accrued_bp) as \"Начислено\", date(creation_date) as \"Дата\" FROM retail_receipt WHERE creation_date >= '{0}' and creation_date <= '{1}' and status = 'fixed' and solution = 'success' GROUP BY 3 ORDER BY 3", start_dt.ToShortDateString(), end_dt.ToShortDateString()), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    reportData.viewData = new List<List<string>>();
                    List<string> row = new List<string>();

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i));
                    }

                    reportData.viewData.Add(row);

                    foreach (DbDataRecord record in rdr)
                    {
                        row = new List<string>();

                        for (int i = 0; i < record.FieldCount; i++)
                        {
                            row.Add(record[i].ToString());
                        }

                        reportData.viewData.Add(row);
                    }
                }

                rdr.Close();
                con.Close();
                return reportData.viewData;
            }
            //Отчет по количеству потраченных/начисленных баллов в разбивке по месяцам
            public List<List<string>> GetMonthlySpentAccruedBP(DateTime start_dt, DateTime end_dt)
            {
                BC.BCViewModels.TableReportViewModel reportData = new BCViewModels.TableReportViewModel();
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(total_paid_bp) as \"Списано\", SUM(total_accrued_bp) as \"Начислено\", extract(MONTH FROM creation_date) as \"Месяц\", extract(YEAR FROM creation_date) as \"Год\", to_char(creation_date, 'TMMonth yyyy') as \"Месяц/год\" FROM retail_receipt WHERE creation_date >= '{0}' and creation_date <= '{1}' and status = 'fixed' and solution = 'success' GROUP BY 3,4,5 ORDER BY 4, 3", start_dt.ToShortDateString(), end_dt.ToShortDateString()), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    reportData.viewData = new List<List<string>>();
                    List<string> row = new List<string>();

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i));
                    }

                    reportData.viewData.Add(row);

                    foreach (DbDataRecord record in rdr)
                    {
                        row = new List<string>();

                        for (int i = 0; i < record.FieldCount; i++)
                        {
                            row.Add(record[i].ToString());
                        }

                        reportData.viewData.Add(row);
                    }
                }

                rdr.Close();
                con.Close();
                return reportData.viewData;
            }
            //Топ активных пользователей (количество операций)
            //Топ активных пользователей (начисленные бонусы)
            //Топ активных пользователей (потраченные бонусы)
            //Топ активных пользователей (потраченные средства)
            //Топ самых "богатых" пользователей (наибольший остаток баллов)
        }
        public class DiscountCards : List<BCViewModels.DiscountCardViewModel>
        {
            public void GetListByPartyID(int _party_id)
            {
                string OrderByStmt = String.Format("ORDER BY card_no ASC");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_discountcard WHERE owner_id IN (SELECT id FROM organization_counterparty WHERE party_id = {0}) {1}", _party_id, OrderByStmt), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        BCViewModels.DiscountCardViewModel dc = new BCViewModels.DiscountCardViewModel();
                        dc.id = Convert.ToInt32(record["id"]);
                        dc.card_no = record["card_no"].ToString();
                        dc.pin = Convert.ToInt32(record["pin"]);
                        dc.status = record["status"].ToString();
                        dc.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);
                        this.Add(dc);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetList(int offset, int limit)
            {
                string OrderByStmt = String.Format("ORDER BY card_no ASC");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_discountcard {0} OFFSET {1} LIMIT {2}", OrderByStmt, offset, limit), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        BCViewModels.DiscountCardViewModel dc = new BCViewModels.DiscountCardViewModel();
                        dc.id = Convert.ToInt32(record["id"]);
                        dc.card_no = record["card_no"].ToString();
                        dc.pin = Convert.ToInt32(record["pin"]);
                        dc.status = record["status"].ToString();
                        dc.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);
                        this.Add(dc);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public int GetTotalCount()
            {
                int cnt = 0;
                string OrderByStmt = String.Format("ORDER BY card_no ASC");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT COUNT(id) FROM retail_discountcard"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        cnt = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
                return cnt;
            }
        }
        public class ReceiptList : List<Retail.TReceipt>
        {
            /*public void GetReceiptsRangeByPartyID(int? offset, int? limit, int? _party_id)
            {
                if (_party_id == null)
                    return;

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_receipt WHERE status = 'fixed' and dc_card_no IN (SELECT card_no FROM retail_discountcard WHERE owner_id IN (SELECT id FROM organization_counterparty WHERE party_id = {0})) ORDER BY creation_date DESC OFFSET {1} LIMIT {2}", _party_id, offset, limit), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TReceipt r = new Retail.TReceipt();
                        r.id = Convert.ToInt32(record["id"]);
                        r.receipt_no = record["receipt_no"].ToString();
                        r.creation_date = Convert.ToDateTime(record["creation_date"]);
                        r.transaction_no = record["transaction_no"].ToString();
                        r.reason = record["reason"].ToString();
                        r.receipt_no = record["receipt_no"].ToString();
                        r.dc_card_no = record["dc_card_no"].ToString();
                        r.auth_code = record["auth_code"].ToString();
                        r.available_bp = Convert.ToInt32(record["available_bp"]);
                        r.total_accrued_bp = Convert.ToInt32(record["total_accrued_bp"]);
                        r.total_paid_bp = Convert.ToInt32(record["total_paid_bp"]);
                        r.sub_total = Convert.ToDecimal(record["sub_total"]);
                        r.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                        r.discount = Convert.ToDecimal(record["discount"]);
                        r.promo_code = record["promo_code"].ToString();
                        r.total = Convert.ToDecimal(record["total"]);
                        r.activation_code = record["activation_code"].ToString();
                        r.status = record["status"].ToString();
                        r.solution = record["solution"].ToString();
                        r.pos_id = Convert.ToInt32(record["pos_id"]);
                        r.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);
                        this.Add(r);
                    }
                }

                rdr.Close();
                con.Close();
            }*/
            public void GetReceiptsRangeByCardNo(int? offset, int? limit, string _card_no)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT rcpt.*, rvt.creation_date as rvt_date, rvt.act_no, rvt.total_bp_to_return, rvt.total_to_return FROM retail_receipt rcpt LEFT JOIN retail_revertcashact rvt ON rcpt.receipt_no = rvt.receipt_no WHERE dc_card_no = '{0}' ORDER BY rcpt.creation_date DESC OFFSET {1} LIMIT {2}", _card_no, offset, limit), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TReceipt r = new Retail.TReceipt();
                        r.id = Convert.ToInt32(record["id"]);
                        r.receipt_no = record["receipt_no"].ToString();
                        r.creation_date = Convert.ToDateTime(record["creation_date"]);
                        r.transaction_no = record["transaction_no"].ToString();
                        r.reason = record["reason"].ToString();
                        r.receipt_no = record["receipt_no"].ToString();
                        r.dc_card_no = record["dc_card_no"].ToString();
                        r.auth_code = record["auth_code"].ToString();
                        r.available_bp = Convert.ToInt32(record["available_bp"]);
                        r.total_accrued_bp = Convert.ToInt32(record["total_accrued_bp"]);
                        r.total_paid_bp = Convert.ToInt32(record["total_paid_bp"]);
                        r.sub_total = Convert.ToDecimal(record["sub_total"]);
                        r.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                        r.discount = Convert.ToDecimal(record["discount"]);
                        r.promo_code = record["promo_code"].ToString();
                        r.total = Convert.ToDecimal(record["total"]);
                        r.activation_code = record["activation_code"].ToString();
                        r.status = record["status"].ToString();
                        r.solution = record["solution"].ToString();
                        r.pos_id = Convert.ToInt32(record["pos_id"]);
                        r.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);

                        if (record["rvt_date"] != DBNull.Value)
                            r.rvt_date = Convert.ToDateTime(record["rvt_date"]);

                        r.act_no = record["act_no"].ToString();

                        if (record["total_to_return"] != DBNull.Value)
                            r.total_to_return = Convert.ToDecimal(record["total_to_return"]);

                        if (record["total_bp_to_return"] != DBNull.Value)
                            r.total_bp_to_return = Convert.ToDecimal(record["total_bp_to_return"]);
                        this.Add(r);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetPartyReceiptsRangeForPeriod(DateTime start_dt, DateTime end_dt, int _party_id)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT rcpt.*, rvt.creation_date as rvt_date, rvt.act_no, rvt.total_bp_to_return, rvt.total_to_return FROM retail_receipt rcpt LEFT JOIN retail_revertcashact rvt ON rcpt.receipt_no = rvt.receipt_no WHERE status = 'fixed' and date(rcpt.creation_date) >= '{0}' and date(rcpt.creation_date) <= '{1}' and dc_card_no IN (SELECT card_no FROM retail_discountcard WHERE owner_id IN (SELECT id FROM organization_counterparty WHERE party_id = {2})) ORDER BY rcpt.creation_date DESC", start_dt.ToShortDateString(), end_dt.ToShortDateString(), _party_id), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TReceipt r = new Retail.TReceipt();
                        r.id = Convert.ToInt32(record["id"]);
                        r.receipt_no = record["receipt_no"].ToString();
                        r.creation_date = Convert.ToDateTime(record["creation_date"]);
                        r.transaction_no = record["transaction_no"].ToString();
                        r.reason = record["reason"].ToString();
                        r.receipt_no = record["receipt_no"].ToString();
                        r.dc_card_no = record["dc_card_no"].ToString();
                        r.auth_code = record["auth_code"].ToString();
                        r.available_bp = Convert.ToInt32(record["available_bp"]);
                        r.total_accrued_bp = Convert.ToInt32(record["total_accrued_bp"]);
                        r.total_paid_bp = Convert.ToInt32(record["total_paid_bp"]);
                        r.sub_total = Convert.ToDecimal(record["sub_total"]);
                        r.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                        r.discount = Convert.ToDecimal(record["discount"]);
                        r.promo_code = record["promo_code"].ToString();
                        r.total = Convert.ToDecimal(record["total"]);
                        r.activation_code = record["activation_code"].ToString();
                        r.status = record["status"].ToString();
                        r.solution = record["solution"].ToString();
                        r.pos_id = Convert.ToInt32(record["pos_id"]);
                        r.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);

                        if (record["rvt_date"] != DBNull.Value)
                            r.rvt_date = Convert.ToDateTime(record["rvt_date"]);

                        r.act_no = record["act_no"].ToString();

                        if (record["total_to_return"] != DBNull.Value)
                            r.total_to_return = Convert.ToDecimal(record["total_to_return"]);

                        if (record["total_bp_to_return"] != DBNull.Value)
                            r.total_bp_to_return = Convert.ToDecimal(record["total_bp_to_return"]);
                        this.Add(r);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetReceiptsRangeForPeriod(DateTime start_dt, DateTime end_dt)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT rcpt.*, rvt.creation_date as rvt_date, rvt.act_no, rvt.total_bp_to_return, rvt.total_to_return FROM retail_receipt rcpt LEFT JOIN retail_revertcashact rvt ON rcpt.receipt_no = rvt.receipt_no WHERE date(rcpt.creation_date) >= '{0}' and date(rcpt.creation_date) <= '{1}' ORDER BY rcpt.creation_date DESC", start_dt.ToShortDateString(), end_dt.ToShortDateString()), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TReceipt r = new Retail.TReceipt();
                        r.id = Convert.ToInt32(record["id"]);
                        r.receipt_no = record["receipt_no"].ToString();
                        r.creation_date = Convert.ToDateTime(record["creation_date"]);
                        r.transaction_no = record["transaction_no"].ToString();
                        r.reason = record["reason"].ToString();
                        r.receipt_no = record["receipt_no"].ToString();
                        r.dc_card_no = record["dc_card_no"].ToString();
                        r.auth_code = record["auth_code"].ToString();
                        r.available_bp = Convert.ToInt32(record["available_bp"]);
                        r.total_accrued_bp = Convert.ToInt32(record["total_accrued_bp"]);
                        r.total_paid_bp = Convert.ToInt32(record["total_paid_bp"]);
                        r.sub_total = Convert.ToDecimal(record["sub_total"]);
                        r.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                        r.discount = Convert.ToDecimal(record["discount"]);
                        r.promo_code = record["promo_code"].ToString();
                        r.total = Convert.ToDecimal(record["total"]);
                        r.activation_code = record["activation_code"].ToString();
                        r.status = record["status"].ToString();
                        r.solution = record["solution"].ToString();
                        r.pos_id = Convert.ToInt32(record["pos_id"]);
                        r.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);

                        if (record["rvt_date"] != DBNull.Value)
                            r.rvt_date = Convert.ToDateTime(record["rvt_date"]);

                        r.act_no = record["act_no"].ToString();

                        if (record["total_to_return"] != DBNull.Value)
                            r.total_to_return = Convert.ToDecimal(record["total_to_return"]);

                        if (record["total_bp_to_return"] != DBNull.Value)
                            r.total_bp_to_return = Convert.ToDecimal(record["total_bp_to_return"]);
                        this.Add(r);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void GetReceiptList(int offset, int limit, DateTime start_dt, DateTime end_dt)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_receipt WHERE date(creation_date) >= '{2}' and date(creation_date) <= '{3}' ORDER BY creation_date DESC OFFSET {0} LIMIT {1}", offset, limit, start_dt, end_dt), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TReceipt r = new Retail.TReceipt();
                        r.id = Convert.ToInt32(record["id"]);
                        r.receipt_no = record["receipt_no"].ToString();
                        r.creation_date = Convert.ToDateTime(record["creation_date"]);
                        r.transaction_no = record["transaction_no"].ToString();
                        r.reason = record["reason"].ToString();
                        r.receipt_no = record["receipt_no"].ToString();
                        r.dc_card_no = record["dc_card_no"].ToString();
                        r.auth_code = record["auth_code"].ToString();
                        r.available_bp = Convert.ToInt32(record["available_bp"]);
                        r.total_accrued_bp = Convert.ToInt32(record["total_accrued_bp"]);
                        r.total_paid_bp = Convert.ToInt32(record["total_paid_bp"]);
                        r.sub_total = Convert.ToDecimal(record["sub_total"]);
                        r.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                        r.discount = Convert.ToDecimal(record["discount"]);
                        r.promo_code = record["promo_code"].ToString();
                        r.total = Convert.ToDecimal(record["total"]);
                        r.activation_code = record["activation_code"].ToString();
                        r.status = record["status"].ToString();
                        r.solution = record["solution"].ToString();
                        r.pos_id = Convert.ToInt32(record["pos_id"]);
                        r.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);
                        this.Add(r);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public int GetTotalCount()
            {
                int cnt = 0;
                string OrderByStmt = String.Format("ORDER BY card_no ASC");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT COUNT(id) FROM retail_receipt"), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        cnt = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
                return cnt;
            }
            public int GetCount(DateTime start_dt, DateTime end_dt)
            {
                int cnt = 0;
                string OrderByStmt = String.Format("ORDER BY creation_date DESC");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT COUNT(id) FROM retail_receipt WHERE date(creation_date) >= '{0}' and date(creation_date) <= '{1}'", start_dt, end_dt), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        cnt = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
                return cnt;
            }
        }
        public class ReceiptRowList : List<Retail.TReceiptRow>
        {
            public void GetReceiptRows(int _receipt_id)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT id FROM retail_receiptrow WHERE receipt_id = {0}", _receipt_id), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Retail.TReceiptRow rr = new Retail.TReceiptRow();
                        rr.GetReceiptRowByID(Convert.ToInt32(rdr["id"]));
                        this.Add(rr);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class LinkChannelsList : List<Party.TLinkChannel>
        {
            public void GetList(int _party_id)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM party_linkchannel WHERE party_id = {0} ORDER BY id", _party_id), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Party.TLinkChannel lc = new Party.TLinkChannel();
                        lc.id = Convert.ToInt32(record["id"]);
                        lc.name = record["name"].ToString();
                        lc.value = record["value"].ToString();
                        lc.is_verified = Convert.ToBoolean(record["is_verified"]);
                        lc.party_id = Convert.ToInt32(record["party_id"]);
                        this.Add(lc);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class HolidayList : List<Party.THoliday>
        {
            public void GetList(int _party_id)
            {
                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM party_holiday WHERE party_id = {0} ORDER BY holiday", _party_id), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Party.THoliday h = new Party.THoliday();
                        h.id = Convert.ToInt32(record["id"]);
                        h.name = record["name"].ToString();
                        h.holiday = Convert.ToDateTime(record["holiday"]);
                        h.party_id = Convert.ToInt32(record["party_id"]);
                        this.Add(h);
                    }
                }

                rdr.Close();
                con.Close();
            }
        }
        public class PartyList : List<Party.TParty>
        {
            public void GetList(int offset, int limit, string order, string OrderDirection, int bplimit, int opdlimit)
            {
                string whereStmt = String.Format("WHERE rsa.bp >= {0}", bplimit);
                string havingStmt = String.Format("HAVING count() >= {0}", opdlimit);

                if (order == null || order == string.Empty)
                    order = "id";

                if (OrderDirection == null || OrderDirection == "")
                    OrderDirection = "ASC";

                string o = "OFFSET " + offset.ToString(), l = "LIMIT " + limit.ToString();
                string OrderByStmt = String.Format("ORDER BY {0} {1}", order, OrderDirection);

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT party_party.*, organization_counterparty.id as CPID, CASE WHEN rsa.bp is null THEN 0 ELSE rsa.bp END, CASE WHEN tt.accrued is null THEN 0 ELSE tt.accrued END, CASE WHEN tt.spent is null THEN 0 ELSE tt.spent END FROM party_party INNER JOIN organization_counterparty ON party_party.id = organization_counterparty.party_id LEFT JOIN retail_savingaccount rsa ON organization_counterparty.id = rsa.owner_id LEFT JOIN (SELECT SUM(rr.total_accrued_bp) as accrued, SUM(rr.total_paid_bp) as spent, rdc.owner_id FROM retail_receipt rr INNER JOIN retail_discountcard rdc ON rr.dc_card_no = rdc.card_no WHERE rr.receipt_no NOT IN (SELECT rrca.receipt_no FROM retail_revertcashact rrca) AND rr.status = 'fixed' GROUP BY rdc.owner_id) tt ON organization_counterparty.id = tt.owner_id {0} {1} {2} {3}", whereStmt, OrderByStmt, l, o), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Party.TParty p = new Party.TParty();
                        p.id = Convert.ToInt32(record["id"]);
                        p.cpid = Convert.ToInt32(record["CPID"]);
                        p.name = record["name"].ToString();
                        p.last_name = record["last_name"].ToString();
                        p.middle_name = record["middle_name"].ToString();
                        p.first_name = record["first_name"].ToString();
                        p.marital_status = record["marital_status"].ToString();
                        p.gender = record["gender"].ToString();
                        this.Add(p);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void FindByName(string name)
            {
                string OrderByStmt = String.Format("ORDER BY {0} ASC", "name");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT party_party.*, organization_counterparty.id as CPID  FROM party_party INNER JOIN organization_counterparty ON party_party.id = organization_counterparty.party_id WHERE UPPER(party_party.name) LIKE UPPER('%{0}%') {1}", name, OrderByStmt), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Party.TParty p = new Party.TParty();
                        p.id = Convert.ToInt32(record["id"]);
                        p.cpid = Convert.ToInt32(record["CPID"]);
                        p.name = record["name"].ToString();
                        p.last_name = record["last_name"].ToString();
                        p.middle_name = record["middle_name"].ToString();
                        p.first_name = record["first_name"].ToString();
                        p.marital_status = record["marital_status"].ToString();
                        p.gender = record["gender"].ToString();
                        this.Add(p);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void FindByCardNumber(string card_no)
            {
                string OrderByStmt = String.Format("ORDER BY {0} ASC", "name");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT party_party.*, organization_counterparty.id as CPID  FROM party_party INNER JOIN organization_counterparty ON party_party.id = organization_counterparty.party_id WHERE organization_counterparty.id IN (SELECT owner_id FROM retail_discountcard WHERE card_no LIKE '%{0}%') {1}", card_no, OrderByStmt), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Party.TParty p = new Party.TParty();
                        p.id = Convert.ToInt32(record["id"]);
                        p.cpid = Convert.ToInt32(record["CPID"]);
                        p.name = record["name"].ToString();
                        p.last_name = record["last_name"].ToString();
                        p.middle_name = record["middle_name"].ToString();
                        p.first_name = record["first_name"].ToString();
                        p.marital_status = record["marital_status"].ToString();
                        p.gender = record["gender"].ToString();
                        this.Add(p);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public void FindByPhone(string phone)
            {
                string OrderByStmt = String.Format("ORDER BY {0} ASC", "name");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT party_party.*, organization_counterparty.id as CPID  FROM party_party INNER JOIN organization_counterparty ON party_party.id = organization_counterparty.party_id WHERE party_party.id IN (SELECT party_id FROM party_linkchannel WHERE name = 'Phone' and value LIKE '%{0}%') {1}", phone, OrderByStmt), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        Party.TParty p = new Party.TParty();
                        p.id = Convert.ToInt32(record["id"]);
                        p.cpid = Convert.ToInt32(record["CPID"]);
                        p.name = record["name"].ToString();
                        p.last_name = record["last_name"].ToString();
                        p.middle_name = record["middle_name"].ToString();
                        p.first_name = record["first_name"].ToString();
                        p.marital_status = record["marital_status"].ToString();
                        p.gender = record["gender"].ToString();
                        this.Add(p);
                    }
                }

                rdr.Close();
                con.Close();
            }
            public int GetTotalCount(int bplimit, int opdlimit)
            {
                int cnt = 0;
                string OrderByStmt = String.Format("ORDER BY card_no ASC");

                NpgsqlConnection con = GetConnection();
                NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT COUNT(pp.id) FROM party_party pp INNER JOIN organization_counterparty ocp ON pp.id = ocp.party_id INNER JOIN retail_savingaccount rsa ON ocp.id = rsa.owner_id WHERE rsa.bp >= {0}", bplimit), con);
                NpgsqlDataReader rdr = command.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        cnt = Convert.ToInt32(record[0]);
                    }
                }

                rdr.Close();
                con.Close();
                return cnt;
            }
        }
        public class TCountry
        {
            public int id { get; set; }
            public string name { get; set; }
            public string full_name { get; set; }
            public string symbol { get; set; }
            public int code { get; set; }
        }
        public class Address
        {
            public class TAddress
            {
                public int id { get; set; }
                public int country_id { get; set; }
                public string country { get; set; }
                public string city { get; set; }
                public string district { get; set; }
                public string street { get; set; }
                public string house { get; set; }
                public string flat { get; set; }
                public string postal_index { get; set; }
            }
        }
        public class Organization
        {
            public class AdditionalParam
            {
                public string DisplayName { get; set; }
                public string value { get; set; }
            }
            public class TCounterParty
            {
                public int id { get; set; }
                public int user_id { get; set; }
                public int party_id { get; set; }
                public int organization_id { get; set; }
            }
            public class TOrganization
            {
                public int id { get; set; }
                [DisplayName("Название")]
                public string name { get; set; }
                [DisplayName("Регистрационный номер")]
                public string reg_no { get; set; }
            }
            public class TCounterPartyAdditionalParam
            {
                public int id { get; set; }
                [DisplayName("Атрибут")]
                public string name { get; set; }
                [DisplayName("Значение")]
                public string value { get; set; }
                public int counterparty_id { get; set; }
            }
        }
        public class Party
        {
            public class TLinkChannel
            {
                public int id { get; set; }
                private string _name;
                public string name 
                {
                    get
                    {
                        return _name;
                    }
                    set
                    {
                        _name = value;

                        switch (_name)
                        {
                            case "Phone": 
                                DisplayName = "Телефон";
                                break;
                            case "Mobile":
                                DisplayName = "Мобильный телефон";
                                break;
                            case "E-Mail":
                                DisplayName = "Эл. почта";
                                break;
                            default:
                                DisplayName = _name;
                                break;
                        }
                    }
                }
                public string DisplayName { get; set; }
                public string value { get; set; }
                public bool is_verified { get; set; }
                public int party_id { get; set; }
                public bool Add()
                {
                    try 
                    {
                        NpgsqlConnection con = GetConnection();
                        NpgsqlCommand command = new NpgsqlCommand(String.Format("INSERT INTO party_linkchannel (name, value, is_verified, party_id) VALUES('{0}', '{1}', false, {2})", this.name, this.value, this.party_id), con);
                        command.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            public class THoliday
            {
                public int id { get; set; }
                public string name { get; set; }
                public DateTime holiday { get; set; }
                public int party_id { get; set; }
            }
            public class TParty
            {
                public int id { get; set; }
                public int cpid { get; set; }
                [DisplayName("Ф.И.О.")]
                public string name { get; set; }
                [DisplayName("Фамилия")]
                public string last_name { get; set; }
                [DisplayName("Отчество")]
                public string middle_name { get; set; }
                [DisplayName("Имя")]
                public string first_name { get; set; }
                [DisplayName("Семейное положение")]
                public string marital_status { get; set; }
                [DisplayName("Пол")]
                public string gender { get; set; }
                [DisplayName("Дата рождения")]
                [DataType(DataType.Date)]
                [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
                public DateTime? birthday { get; set; }
                public void GetPartyByID(int? _id)
                {
                    if (_id == null)
                        return;

                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT party_party.*, organization_counterparty.id as CPID FROM party_party INNER JOIN organization_counterparty ON party_party.id = organization_counterparty.party_id WHERE party_party.id = {0}", _id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            this.id = Convert.ToInt32(record["id"]);
                            this.cpid = Convert.ToInt32(record["cpid"]);
                            this.name = record["name"].ToString();
                            this.last_name = record["last_name"].ToString();
                            this.middle_name = record["middle_name"].ToString();
                            this.first_name = record["first_name"].ToString();
                            this.marital_status = record["marital_status"].ToString();
                            this.gender = record["gender"].ToString();
                        }
                    }

                    rdr.Close();
                    con.Close();
                }
                public string Save()
                {
                    string res = "OK";

                    if (this.id == 0)
                    {
                        try
                        {
                            //Создание пати
                            NpgsqlConnection con = GetConnection();
                            NpgsqlCommand command = new NpgsqlCommand(String.Format("INSERT INTO party_party (name, last_name, middle_name, first_name, marital_status, gender) VALUES('{0}','{1}','{2}','{3}','{4}','{5}') RETURNING id", this.name, this.last_name, this.middle_name, this.first_name, this.marital_status, this.gender), con);
                            NpgsqlDataReader rdr = command.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                foreach (DbDataRecord row in rdr)
                                {
                                    this.id = Convert.ToInt32(row[0]);
                                }
                            }
                            rdr.Close();

                            //Создание контерпати
                            command.CommandText = String.Format("INSERT INTO organization_counterparty (user_id, party_id, organization_id) VALUES({0},'{1}',{2}) RETURNING id", "null", this.id, "null");
                            rdr = command.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                foreach (DbDataRecord row in rdr)
                                {
                                    this.cpid = Convert.ToInt32(row[0]);
                                }
                            }
                            rdr.Close();
                            //Добавление дня рождения, если указано
                            if (this.birthday != null)
                            {
                                command.CommandText = String.Format("INSERT INTO party_holiday (name, holiday, party_id) VALUES('{0}','{1}',{2})", "Birthday", this.birthday, this.id);
                                command.ExecuteNonQuery();
                            }
                            //Создание и привязка накопительного счета
                            //Чтение последовательности и префикса!
                            command.CommandText = String.Format("SELECT * FROM retail_sequence WHERE id = 1");
                            rdr = command.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                string prefix = "";
                                ulong number = 0;
                                int initial_bp = 0;

                                foreach (DbDataRecord record in rdr)
                                { 
                                    prefix = record["prefix"].ToString();
                                    number = Convert.ToUInt32(record["number"]);
                                }

                                rdr.Close();

                                //Проверка наличия счета у владельца
                                command.CommandText = String.Format("SELECT * FROM retail_savingaccount WHERE owner_id = {0}", this.cpid);
                                rdr = command.ExecuteReader();

                                bool isHaveSavingAccount = false;

                                if (rdr.HasRows)
                                {
                                    foreach (DbDataRecord record in rdr)
                                    {
                                        isHaveSavingAccount = true;
                                    }
                                }

                                rdr.Close();

                                if (!isHaveSavingAccount)
                                {
                                    command.CommandText = String.Format("UPDATE retail_sequence SET number = {0} WHERE id = 1", number + 1);
                                    command.ExecuteNonQuery();
                                    //Начальное количество баллов
                                    command.CommandText = String.Format("SELECT value FROM retail_domainsetting WHERE domain_id = 2 AND name = 'initial_bp'");
                                    rdr = command.ExecuteReader();

                                    if (rdr.HasRows)
                                    {
                                        foreach (DbDataRecord record in rdr)
                                        {
                                            initial_bp = Convert.ToInt32(record["value"]);
                                        }
                                    }

                                    rdr.Close();
                                    command.CommandText = String.Format("INSERT INTO retail_savingaccount (account_no, creation_date, bp, owner_id, domain_id) VALUES('{0}', now(), {1}, {2}, 2)", prefix + number.ToString(), initial_bp, this.cpid);
                                    command.ExecuteNonQuery();
                                }
                            }

                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            res = ex.Message;
                        }
                    }
                    else
                    {
                        try
                        {
                            NpgsqlConnection con = GetConnection();
                            NpgsqlCommand command = new NpgsqlCommand(String.Format("UPDATE party_party SET name = '{0}', last_name = '{1}', middle_name = '{2}', first_name = '{3}', marital_status = '{4}', gender = '{5}' WHERE id = {6} ", this.name, this.last_name, this.middle_name, this.first_name, this.marital_status, this.gender, this.id), con);
                            command.ExecuteNonQuery();

                            if (this.birthday != null)
                            {
                                command.CommandText = String.Format("SELECT * FROM party_holiday WHERE name = '{0}' AND party_id = {1}", "Birthday", this.id);
                                NpgsqlDataReader rdr = command.ExecuteReader();

                                if (rdr.HasRows)
                                {
                                    int holiday_id = 0;

                                    foreach (DbDataRecord record in rdr)
                                    {
                                        holiday_id = Convert.ToInt32(record[0]);
                                    }

                                    rdr.Close();
                                    command.CommandText = String.Format("UPDATE party_holiday SET holiday = '{0}' WHERE id = {1}", this.birthday, holiday_id);
                                    command.ExecuteNonQuery();
                                }
                                else
                                {
                                    rdr.Close();
                                    command.CommandText = String.Format("INSERT INTO party_holiday (name, holiday, party_id) VALUES('{0}','{1}',{2})", "Birthday", this.birthday, this.id);
                                    command.ExecuteNonQuery();
                                }
                            }

                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            res = ex.Message;
                        }
                    }

                    return res;
                }
                public int GetTotalAccruedBP(int? _id)
                {
                    int res = 0;

                    if (_id == null)
                        return 0;

                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(rr.total_accrued_bp) FROM retail_receipt rr WHERE rr.dc_card_no IN (SELECT rdc.card_no FROM retail_discountcard rdc WHERE rdc.owner_id IN (SELECT oc.id FROM organization_counterparty oc WHERE oc.party_id = {0})) and rr.receipt_no NOT IN (SELECT DISTINCT(rrca.receipt_no) FROM retail_revertcashact rrca) and rr.status = 'fixed'", _id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            if(record[0] != DBNull.Value)
                                res = Convert.ToInt32(record[0]);
                            else
                                res = 0;
                        }
                    }

                    rdr.Close();
                    con.Close();

                    return res;
                }
                public int GetTotalSpentBP(int? _id)
                {
                    int res = 0;

                    if (_id == null)
                        return 0;

                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT SUM(rr.total_paid_bp) FROM retail_receipt rr WHERE rr.dc_card_no IN (SELECT rdc.card_no FROM retail_discountcard rdc WHERE rdc.owner_id IN (SELECT oc.id FROM organization_counterparty oc WHERE oc.party_id = {0})) and rr.receipt_no NOT IN (SELECT DISTINCT(rrca.receipt_no) FROM retail_revertcashact rrca) and rr.status = 'fixed'", _id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            if (record[0] != DBNull.Value)
                                res = Convert.ToInt32(record[0]);
                            else
                                res = 0;
                        }
                    }

                    rdr.Close();
                    con.Close();

                    return res;
                }
            }
            public class TPartyAddresses : List<Address.TAddress>
            {
                public int id { get; set; }
                public int party_id { get; set; }
                public int address_id { get; set; }

                public void GetAddressByPartyID(int _party_id)
                {
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT address_address.*, country_country.full_name FROM address_address INNER JOIN country_country ON address_address.country_id = country_country.id WHERE address_address.id IN (SELECT address_id FROM party_party_addresses WHERE party_id = {0}) ORDER BY id", _party_id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            Address.TAddress a = new Address.TAddress();
                            a.id = Convert.ToInt32(record["id"]);
                            a.country_id = Convert.ToInt32(record["country_id"]);
                            a.country = record["full_name"].ToString();
                            a.city = record["city"].ToString();
                            a.district = record["district"].ToString();
                            a.street = record["street"].ToString();
                            a.house = record["house"].ToString();
                            a.flat = record["flat"].ToString();
                            a.postal_index = record["postal_index"].ToString();
                            this.Add(a);
                        }
                    }

                    rdr.Close();
                    con.Close();
                }
            }
        }
        public class Retail
        {
            public class DCStatus
            {
                public string DisplayName { get; set; }
                public string name { get; set; }
            }
            public class TDiscountCard
            {
                public int id { get; set; }
                public string card_no { get; set; }
                public int owner_id { get; set; }
                public int pin { get; set; }
                public string status { get; set; }
                public bool is_synchronized { get; set; }
                public string Save()
                {
                    string res = "OK";

                    try
                    {
                        NpgsqlConnection con = GetConnection();
                        NpgsqlCommand command = new NpgsqlCommand(String.Format("UPDATE retail_discountcard SET status = '{0}' WHERE id = {1} ", this.status, this.id), con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        res = ex.Message;
                    }

                    return res;
                }
                public bool FindCard(string _card_no)
                {
                    bool result = false;
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_discountcard WHERE card_no = '{0}'", _card_no), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            this.id = Convert.ToInt32(record["id"]);
                            this.card_no = record["card_no"].ToString();

                            if(record["owner_id"] != DBNull.Value)
                                this.owner_id = Convert.ToInt32(record["owner_id"]);

                            this.pin = Convert.ToInt32(record["pin"]);
                            this.status = record["status"].ToString();
                            this.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);
                        }

                        result = true;
                    }

                    rdr.Close();
                    con.Close();
                    return result;
                }
                public string LinkCard(int _id, int _party_id)
                {
                    string res = "OK";

                    try
                    {
                        NpgsqlConnection con = GetConnection();
                        NpgsqlCommand command = new NpgsqlCommand(String.Format("UPDATE retail_discountcard SET owner_id = (SELECT id FROM organization_counterparty WHERE party_id = {0}) WHERE id = {1} ", _party_id, _id), con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        res = ex.Message;
                    }

                    return res;
                }
                private string POST(string Url, string Data)
                {
                    //Data = "{ \"objects\": [ {} ] }";
                    //Data = "";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
                    req.Credentials = new NetworkCredential("tv", "123456");
                    req.Method = "POST";
                    req.Timeout = 10000;
                    req.ContentType = "application/json";
                    byte[] sentData = Encoding.GetEncoding("utf-8").GetBytes(Data);
                    req.ContentLength = sentData.Length;
                    System.IO.Stream sendStream = req.GetRequestStream();
                    sendStream.Write(sentData, 0, sentData.Length);
                    sendStream.Close();
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    System.IO.Stream ReceiveStream = res.GetResponseStream();
                    System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
                    //Кодировка указывается в зависимости от кодировки ответа сервера
                    Char[] read = new Char[256]; ;
                    int count = sr.Read(read, 0, 256);
                    string Out = String.Empty;

                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        Out += str;
                        count = sr.Read(read, 0, 256);
                    }
                    return Out;
                }
                private string PATCH(string Url, string Data)
                {
                    //Data = "{ \"objects\": [ {} ] }";
                    //Data = "";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
                    req.Credentials = new NetworkCredential("tv", "123456");
                    req.Method = "PATCH";
                    req.Timeout = 10000;
                    req.ContentType = "application/json";
                    byte[] sentData = Encoding.GetEncoding("utf-8").GetBytes(Data);
                    req.ContentLength = sentData.Length;
                    System.IO.Stream sendStream = req.GetRequestStream();
                    sendStream.Write(sentData, 0, sentData.Length);
                    sendStream.Close();
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    System.IO.Stream ReceiveStream = res.GetResponseStream();
                    System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
                    //Кодировка указывается в зависимости от кодировки ответа сервера
                    Char[] read = new Char[256]; ;
                    int count = sr.Read(read, 0, 256);
                    string Out = String.Empty;

                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        Out += str;
                        count = sr.Read(read, 0, 256);
                    }
                    return Out;
                }
                public int AddBP(int bp, string username)
                {
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("select rdc.card_no, rac.code, rdc.owner_id from retail_discountcard rdc INNER JOIN retail_authcode rac ON rdc.id = rac.dc_card_id where rdc.id = {0}", this.id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    int res = 0;

                    string card_no = "";
                    string auth_code = "";
                    int owner_id = 0;
                    string receipt_no = "";

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            card_no = record["card_no"].ToString();
                            auth_code = record["code"].ToString();

                            if (record["owner_id"] != DBNull.Value)
                                owner_id = Convert.ToInt32(record["owner_id"]);
                        }

                        Guid g = Guid.NewGuid();

                        try
                        {
                            string data = "{\"receipt_no\" : \"\", \"promo_code\" : \"\", \"discount\" : 0, \"discount_percentage\" : 0, \"paid_bp\" : 0, \"sub_total\" : 0, \"total\" : 0, \"additional_params\" : [{\"name\" : \"tablename\",\"value\" : \"0\"},{\"name\" : \"guests\",\"value\" : \"1\"}],\"rows\" : [{\"code\" : \"2159\",\"product_name\" : \"Ручное пополнение\",\"quantity\": \"1\",\"retail_cost\": \"" + bp + "\",\"sku\": \"8388613\",\"uom\": 796, \"paid_bp\" : 0}, {\"code\" : \"1\",\"product_name\" : \"" + username + "\",\"quantity\": \"1\",\"retail_cost\": \"0\",\"sku\": \"8388623\",\"uom\": 796, \"paid_bp\" : 0}],\"pos\": \"1001\",\"dc_card_no\": \"" + card_no + "\",\"auth_code\": \"" + auth_code + "\",\"transaction_no\": \"" + g.ToString() + "\"}";
                            string postresult = POST("http://10.1.0.36:8002/elastic/retail/api/v1/receipt/", data);

                            data = "{\"status\": \"fixed\"}";
                            PATCH("http://10.1.0.36:8002/elastic/retail/api/v1/receipt/" + g.ToString() + "/", data);

                            Match m = Regex.Match(postresult, "receipt_no\":(?<val>.*?),");

                            if (m.Success)
                            {
                                res = bp;
                                receipt_no = m.Groups["val"].ToString();
                            }
                            else
                            {
                                res = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            res = 0;
                        }
                    }

                    rdr.Close();

                    if (owner_id != 0 && bp == res)
                    {
                        command = new NpgsqlCommand(String.Format("UPDATE retail_savingaccount SET bp = bp + {0} WHERE owner_id = {1} ", bp, owner_id), con);
                        command.ExecuteNonQuery();

                        if (receipt_no != "")
                        {
                            receipt_no = receipt_no.Replace("\"", "");
                            receipt_no = receipt_no.Replace(" ", "");
                            command = new NpgsqlCommand(String.Format("UPDATE retail_receipt SET total_accrued_bp = {0} WHERE receipt_no = '{1}' AND dc_card_no = '{2}'", bp, receipt_no, card_no), con);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                        res = 0;

                    con.Close();
                    return res;
                }
                public int SubBP(int bp, string username)
                {
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("select rdc.card_no, rac.code, rdc.owner_id FROM retail_discountcard rdc INNER JOIN retail_authcode rac ON rdc.id = rac.dc_card_id where rdc.id = {0}", this.id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    int res = 0;

                    string card_no = "";
                    string auth_code = "";
                    int owner_id = 0;

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            card_no = record["card_no"].ToString();
                            auth_code = record["code"].ToString();

                            if (record["owner_id"] != DBNull.Value)
                                owner_id = Convert.ToInt32(record["owner_id"]);
                        }

                        Guid g = Guid.NewGuid();

                        try
                        {
                            string data = "{\"receipt_no\" : \"\", \"promo_code\" : \"\", \"discount\" : 0, \"discount_percentage\" : 0, \"paid_bp\" : 0, \"sub_total\" : 0, \"total\" : 0, \"additional_params\" : [{\"name\" : \"tablename\",\"value\" : \"0\"},{\"name\" : \"guests\",\"value\" : \"1\"}],\"rows\" : [{\"code\" : \"99999\",\"product_name\" : \"Ручное списание\",\"quantity\": \"1\",\"retail_cost\": \"" + bp + "\",\"sku\": \"8388613\",\"uom\": 796, \"paid_bp\" : 0}, {\"code\" : \"1\",\"product_name\" : \"" + username + "\",\"quantity\": \"1\",\"retail_cost\": \"0\",\"sku\": \"8388623\",\"uom\": 796, \"paid_bp\" : 0}],\"pos\": \"1001\",\"dc_card_no\": \"" + card_no + "\",\"auth_code\": \"" + auth_code + "\",\"transaction_no\": \"" + g.ToString() + "\"}";
                            string postresult = POST("http://10.1.0.36:8002/elastic/retail/api/v1/receipt/", data);

                            data = "{\"status\": \"fixed\"}";
                            PATCH("http://10.1.0.36:8002/elastic/retail/api/v1/receipt/" + g.ToString() + "/", data);

                            Match m = Regex.Match(postresult, "receipt_no\":(?<val>.*?),");

                            if (m.Success)
                            {
                                res = bp;
                            }
                            else
                            {
                                res = 0;
                            }
                        }
                        catch
                        {
                            res = 0;
                        }
                    }

                    rdr.Close();

                    if (owner_id != 0 && bp == res)
                    {
                        command = new NpgsqlCommand(String.Format("UPDATE retail_savingaccount SET bp = bp - {0} WHERE owner_id = {1}", bp, owner_id), con);
                        command.ExecuteNonQuery();
                    }
                    else
                        res = 0;

                    con.Close();
                    return res;
                }
            }
            public class TDomain
            {
                public int id { get; set; }
                public string name { get; set; }
                public string code_name { get; set; }
                public int admin_id { get; set; }
            }
            public class TPos
            {
                public int id { get; set; }
                public string code { get; set; }
                public string name { get; set; }
                public int ou_id { get; set; }
                public int retail_warehouse_id { get; set; }
                public void GetPOSByID(int _id)
                {
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_pos WHERE id = {0}", _id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            this.id = Convert.ToInt32(record["id"]);
                            this.code = record["code"].ToString();
                            this.name = record["name"].ToString();
                            this.ou_id = Convert.ToInt32(record["ou_id"]);
                            this.retail_warehouse_id = Convert.ToInt32(record["retail_warehouse_id"]);
                        }
                    }

                    rdr.Close();
                    con.Close();
                }
            }
            public class TReceipt
            {
                public int id { get; set; }
                public string receipt_no { get; set; }
                public DateTime creation_date { get; set; }
                public string transaction_no { get; set; }
                public string reason { get; set; }
                public string dc_card_no { get; set; }
                public string auth_code { get; set; }
                public int available_bp { get; set; }
                public int total_accrued_bp { get; set; }
                public int total_paid_bp { get; set; }
                public Decimal sub_total { get; set; }
                public Decimal discount_percentage { get; set; }
                public Decimal discount { get; set; }
                public string promo_code { get; set; }
                public Decimal total { get; set; }
                public string activation_code { get; set; }
                private string _status;
                public string status 
                {
                    get
                    {
                        switch (_status)
                        { 
                            case "new":
                                return "Новый";
                            case "fixed":
                                return "Проведен";
                            default:
                                return "Неизвестно";
                        }
                    }
                    set
                    {
                        _status = value;
                    }
                }
                public string solution { get; set; }
                public int pos_id { get; set; }
                public bool is_synchronized { get; set; }
                public DateTime rvt_date { get; set; }
                public string act_no { get; set; }
                public Decimal total_bp_to_return { get; set; }
                public Decimal total_to_return { get; set; }
                public void GetReceiptByID(int _id)
                {
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_receipt WHERE id = {0}", _id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            this.id = Convert.ToInt32(record["id"]);
                            this.receipt_no = record["receipt_no"].ToString();
                            this.creation_date = Convert.ToDateTime(record["creation_date"]);
                            this.transaction_no = record["transaction_no"].ToString();
                            this.reason = record["reason"].ToString();
                            this.dc_card_no = record["dc_card_no"].ToString();
                            this.auth_code = record["auth_code"].ToString();
                            this.available_bp = Convert.ToInt32(record["available_bp"]);
                            this.total_accrued_bp = Convert.ToInt32(record["total_accrued_bp"]);
                            this.total_paid_bp = Convert.ToInt32(record["total_paid_bp"]);
                            this.sub_total = Convert.ToDecimal(record["sub_total"]);
                            this.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                            this.discount = Convert.ToDecimal(record["discount"]);
                            this.promo_code = record["promo_code"].ToString();
                            this.total = Convert.ToDecimal(record["total"]);
                            this.activation_code = record["activation_code"].ToString();
                            this.status = record["status"].ToString();
                            this.solution = record["solution"].ToString();
                            this.pos_id = Convert.ToInt32(record["pos_id"]);
                            this.is_synchronized = Convert.ToBoolean(record["is_synchronized"]);
                        }
                    }

                    rdr.Close();
                    con.Close();
                }
            }
            public class TReceiptAdditionalParam
            {
                public int id { get; set; }
                public string name { get; set; }
                public string value { get; set; }
                public int receipt_id { get; set; }
            }
            public class TReceiptRow
            {
                public int id { get; set; }
                public string sku { get; set; }
                [DisplayName("Код R-Keeper")]
                public string code { get; set; }
                [DisplayName("Наименование R-Keeper")]
                public string product_name { get; set; }
                [DisplayName("Цена R-Keeper")]
                public Decimal retail_cost { get; set; }
                public string uom { get; set; }
                [DisplayName("Количество")]
                public Decimal quantity { get; set; }
                [DisplayName("Общая стоимость")]
                public Decimal total_cost { get; set; }
                [DisplayName("Процент скидки")]
                public Decimal discount_percentage { get; set; }
                [DisplayName("Скидка")]
                public Decimal discount { get; set; }
                [DisplayName("Начисленно баллов")]
                public int accrued_bp { get; set; }
                [DisplayName("Оплачено баллами")]
                public int paid_bp { get; set; }
                public string product_group_name { get; set; }
                public string product_group_code { get; set; }
                public string product_category_name { get; set; }
                public string product_category_code { get; set; }
                public int receipt_id { get; set; }

                public void GetReceiptRowByID(int _id)
                {
                    NpgsqlConnection con = GetConnection();
                    NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT * FROM retail_receiptrow WHERE id = {0}", _id), con);
                    NpgsqlDataReader rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        foreach (DbDataRecord record in rdr)
                        {
                            this.id = Convert.ToInt32(record["id"]);
                            this.sku = record["sku"].ToString();
                            this.product_name = record["product_name"].ToString();
                            this.retail_cost = Convert.ToDecimal(record["retail_cost"]);
                            this.uom = record["uom"].ToString();
                            this.quantity = Convert.ToDecimal(record["quantity"]);
                            this.total_cost = Convert.ToDecimal(record["total_cost"]);
                            this.discount_percentage = Convert.ToDecimal(record["discount_percentage"]);
                            this.discount = Convert.ToDecimal(record["discount"]);
                            this.accrued_bp = Convert.ToInt32(record["accrued_bp"]);
                            this.paid_bp = Convert.ToInt32(record["paid_bp"]);
                            this.product_group_name = record["product_group_name"].ToString();
                            this.product_group_code = record["product_group_code"].ToString();
                            this.product_category_name = record["product_category_name"].ToString();
                            this.product_category_code = record["product_category_code"].ToString();
                            this.receipt_id = Convert.ToInt32(record["receipt_id"]);
                        }
                    }

                    rdr.Close();
                    con.Close();
                }
            }
            public class TSavingAccount
            {
                public int id { get; set; }
                public string account_no { get; set; }
                public DateTime creation_date { get; set; }
                public int bp { get; set; }
                public int owner_id { get; set; }
                public int domain_id { get; set; }
                public string Save()
                {
                    string res = "OK";

                    try
                    {
                        NpgsqlConnection con = GetConnection();
                        NpgsqlCommand command = new NpgsqlCommand(String.Format("UPDATE retail_savingaccount SET bp = '{0}' WHERE id = {1} ", this.bp, this.id), con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        res = ex.Message;
                    }

                    return res;
                }
                public void Create(int party_id)
                {
                    int initial_bp = 0;
                    NpgsqlConnection con = GetConnection();

                    try
                    {
                        int cpid = 0;
                        //Создание пати
                        NpgsqlCommand command = new NpgsqlCommand(String.Format("SELECT id FROM organization_counterparty WHERE party_id = {0}", party_id), con);
                        NpgsqlDataReader rdr = command.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            foreach (DbDataRecord record in rdr)
                            {
                                cpid = Convert.ToInt32(record[0]);
                            }

                            rdr.Close();
                            //Создание и привязка накопительного счета
                            //Чтение последовательности и префикса!
                            command.CommandText = String.Format("SELECT * FROM retail_sequence WHERE id = 1");
                            rdr = command.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                string prefix = "";
                                ulong number = 0;

                                foreach (DbDataRecord record in rdr)
                                {
                                    prefix = record["prefix"].ToString();
                                    number = Convert.ToUInt32(record["number"]);
                                }

                                rdr.Close();
                                command.CommandText = String.Format("SELECT * FROM retail_savingaccount WHERE owner_id = {0}", cpid);
                                rdr = command.ExecuteReader();

                                if (rdr.HasRows)
                                {
                                    rdr.Close();
                                    return;
                                }

                                rdr.Close();
                                command.CommandText = String.Format("UPDATE retail_sequence SET number = {0} WHERE id = 1", number + 1);
                                command.ExecuteNonQuery();
                                //Начальное количество баллов
                                command.CommandText = String.Format("SELECT value FROM retail_domainsetting WHERE domain_id = 2 AND name = 'initial_bp'");
                                rdr = command.ExecuteReader();

                                if (rdr.HasRows)
                                {
                                    foreach (DbDataRecord record in rdr)
                                    {
                                        initial_bp = Convert.ToInt32(record["value"]);
                                    }
                                }

                                rdr.Close();
                                command.CommandText = String.Format("INSERT INTO retail_savingaccount (account_no, creation_date, bp, owner_id, domain_id) VALUES('{0}', now(), {1}, {2}, 2)", prefix + number.ToString(), initial_bp, cpid);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                public void Unlink(int id)
                {
                    NpgsqlConnection con = GetConnection();

                    try
                    {
                        NpgsqlCommand command = new NpgsqlCommand(String.Format("UPDATE retail_savingaccount SET owner_id = null WHERE id = {0}", id), con);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}