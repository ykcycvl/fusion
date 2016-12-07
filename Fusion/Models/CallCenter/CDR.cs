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
using AsterNET.Manager;
using AsterNET.Manager.Event;

namespace Fusion.Models.CallCenter
{
    public class CDR
    {
        public static MySqlConnection GetConnection()
        {
            MySqlConnection dbConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["AsterCDRDB"].ConnectionString);
            dbConnection.Open();
            return dbConnection;
        }
        public static MySqlConnection GetACDRDBConnection()
        {
            MySqlConnection dbConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["AsteriskCDRDB"].ConnectionString);
            dbConnection.Open();
            return dbConnection;
        }
        public static MySqlConnection GetAsteriskConnection()
        {
            MySqlConnection dbConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["asterisk"].ConnectionString);
            dbConnection.Open();
            return dbConnection;
        }
        public static MySqlConnection GetQuasiConnection()
        {
            MySqlConnection dbConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Quasi"].ConnectionString);
            dbConnection.Open();
            return dbConnection;
        }
        public class HHPstat
        {
            public DateTime TimePeriod { get; set; }
            public int IncomingTotal { get; set; }
            public int Answered { get; set; }
            public int MAX_QUEUED { get; set; }
            public TimeSpan AvgAnswerTime { get; set; }
            public TimeSpan MaxAnswerTime { get; set; }
            public TimeSpan AvgAnswerTimeOperator { get; set; }
            public TimeSpan MaxAnswerTimeOperator { get; set; }
            public int AbandonTotal { get; set; }
            public int Abandonafter10 { get; set; }
            public TimeSpan AbandonAvgWaitTime { get; set; }
            public TimeSpan AbandonMaxWaitTime { get; set; }
            public Double AnsweredPrc { get; set; }
            public Double SL { get; set; }
            public int AnsweredFor10Sec { get; set; }
            public int OutgoingTotal { get; set; }
            public TimeSpan AvgIncomingTalkTime { get; set; }
            public TimeSpan AvgOutgoingTalkTime { get; set; }
            public int Total { get; set; }
            public Double ACR { get; set; }
            public Double AvgOperatorsCount { get; set; }
            public Double AvgOnlineOperatorsCount { get; set; }
            public TimeSpan RestTime { get; set; }
            public TimeSpan InternetTime { get; set; }
            public TimeSpan OnlineTime { get; set; }
            public TimeSpan RegisteredTime { get; set; }
        }
        public class Agent
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Queue
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Event
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class ServiceLevel
        {
            public DateTime dt { get; set; }
            public int hour { get; set; }
            public bool fpoh { get; set; }
            public int Sec10 { get; set; }
            public int Sec20 { get; set; }
            public int Sec30 { get; set; }
            public int Sec40 { get; set; }
            public int Sec50 { get; set; }
            public int Sec60 { get; set; }
            public int Sec70 { get; set; }
            public int Sec80 { get; set; }
            public int Sec90 { get; set; }
            public int SecOver90 { get; set; }

        }
        public class MissedCall
        {
            public string uniqueid { get; set; }
            public DateTime EnterQueueDt { get; set; }
            public DateTime AbandonDt { get; set; }
            public int WaitTime { get; set; }
            public string phonenumber { get; set; }
            public string Queue { get; set; }
            public int start_pos { get; set; }
            public int end_pos { get; set; }
            public string agent{get;set;}
            public TimeSpan WaitTimeForOperator { get; set; }
        }
        public class AgentCDRInfo
        {
            public string AgentName { get; set; }
            public string AgentNumber { get; set; }
            public int AnsweredTotal { get; set; }
            public TimeSpan AvgAnswerTime { get; set; }
            public TimeSpan MaxAnswerTime { get; set; }
            public TimeSpan TotalTalkTime { get; set; }
            public int AbandonTotal { get; set; }
            public int Abandonafter10 { get; set; }
            public TimeSpan AbandonAvgWaitTime { get; set; }
            public TimeSpan AbandonMaxWaitTime { get; set; }
            public int OutgoingTotal { get; set; }
            public TimeSpan AvgIncomingTalk { get; set; }
            public TimeSpan AvgOutgoingTalk { get; set; }
            public TimeSpan RestTime { get; set; }
            public TimeSpan InternetTime { get; set; }
            public TimeSpan OnlineTime { get; set; }
            public TimeSpan RegisteredTime { get; set; }
            public TimeSpan FreeTime { get; set; }

            public ServiceLevel AgentServiceLevel = new ServiceLevel();
            public List<ServiceLevel> SLPHH = new List<ServiceLevel>();
            public List<ServiceLevel> SLPD = new List<ServiceLevel>();
            public List<ServiceLevel> SLPW = new List<ServiceLevel>();
            public List<ServiceLevel> SLPM = new List<ServiceLevel>();
            public List<HHPstat> _HHPStat = new List<HHPstat>();
        }
        public class RecordInfo
        {
            public DateTime calldate { get; set; }
            public string src { get; set; }
            public string dst { get; set; }
            public string recordingfile { get; set; }
        }
        public class CDRInfo
        {
            public List<HHPstat> _overallHHPStat = new List<HHPstat>();
            public List<ServiceLevel> SLPHH = new List<ServiceLevel>();
            public List<ServiceLevel> SLPD = new List<ServiceLevel>();
            public List<ServiceLevel> SLPW = new List<ServiceLevel>();
            public List<ServiceLevel> SLPM = new List<ServiceLevel>();
            public List<MissedCall> MissedCalls = new List<MissedCall>();
            public List<RecordInfo> RecordsList = new List<RecordInfo>();
            public virtual ICollection<Agent> Agents { get; set; }
            public virtual ICollection<Queue> Queues { get; set; }
            public virtual ICollection<Event> Events { get; set; }
            public int IncomingTotal { get; set; }
            public int Answered { get; set; }
            public int MAX_QUEUED { get; set; }
            public TimeSpan AvgAnswerTime { get; set; }
            public TimeSpan MaxAnswerTime { get; set; }
            public TimeSpan AvgAnswerTimeOperator { get; set; }
            public TimeSpan MaxAnswerTimeOperator { get; set; }
            public int AbandonTotal { get; set; }
            public int Abandonafter10 { get; set; }
            public TimeSpan AbandonAvgWaitTime { get; set; }
            public TimeSpan AbandonMaxWaitTime { get; set; }
            public Double AnsweredPrc { get; set; }
            public Double SL { get; set; }
            public int AnsweredFor10Sec { get; set; }
            public int OutgoingTotal { get; set; }
            public TimeSpan AvgIncomingTalkTime { get; set; }
            public TimeSpan AvgOutgoingTalkTime { get; set; }
            public int Total { get; set; }
            public Double ACR { get; set; }
            public Double AvgOperatorsCount { get; set; }
            public Double AvgOnlineOperatorsCount { get; set; }
            
            public int COMPLETECALLER { get; set; }
            public int COMPLETEAGENT { get; set; }
            public int TRANSFER { get; set; }
            public int EXITWITHTIMEOUT { get; set; }
            public int ABANDON { get; set; }
            public int ABANDONMORE10 { get; set; }
            public int ABANDONLESS10 { get; set; }
            public int ABANDONMAXWAITTIME { get; set; }
            public int ENTERQUEUE { get; set; }
            public int CONNECTLESS10 { get; set; }
            public int CONNECTMORE10 { get; set; }
            public int TOTAL { get; set; }
            public int TOTALDURATION { get; set; }
            public int AvgDuration { get; set; }
            public ServiceLevel OverallServiceLevel = new ServiceLevel();
            //Список статистики по агентам
            public List<AgentCDRInfo> AgentsCDR = new List<AgentCDRInfo>();

            public CDRInfo()
            {
                MySqlConnection con = GetConnection();
                MySqlCommand com = new MySqlCommand("SELECT qname_id, queue FROM qname WHERE queue <> 'NONE' and queue IN ('2000', '2003') ORDER BY queue", con);
                MySqlDataReader rdr = com.ExecuteReader();

                if (rdr.HasRows)
                {
                    Queues = new List<Queue>();
                    foreach (DbDataRecord record in rdr)
                    {
                        Queue queue = new Queue();
                        queue.id = Convert.ToInt32(record["qname_id"]);
                        queue.name = record["queue"].ToString();
                        Queues.Add(queue);
                    }
                }

                rdr.Close();

                com.CommandText = "SELECT agent_id, agent FROM qagent ORDER BY agent"; ;
                rdr = com.ExecuteReader();

                if (rdr.HasRows)
                {
                    Agents = new List<Agent>();

                    foreach (DbDataRecord record in rdr)
                    {
                        Agent agent = new Agent();
                        agent.id = Convert.ToInt32(record["agent_id"]);
                        agent.name = record["agent"].ToString();
                        Agents.Add(agent);
                    }
                }

                rdr.Close();

                con.Close();
            }
            /*
             * Причины разъединения
             * COMPLETECALLER: гость положил трубку
             * COMPLETEAGENT: оператор положил трубку
             */
            
            public void GetOverallStatistic(DateTime start_dt, DateTime end_dt, string queues, string agents, bool forPeriod)
            {
                /*
                 * Список операторов
                 */
                #region GetOPerators
                MySqlConnection acon = GetAsteriskConnection();
                MySqlCommand acom = new MySqlCommand("SELECT * FROM AgentListView", acon);
                MySqlDataReader ardr = acom.ExecuteReader();

                TimeSpan tspan = end_dt - start_dt;
                int Days = tspan.Days;
                DateTime dt = start_dt;

                if (Days > 1)
                    dt = new DateTime(start_dt.Year, start_dt.Month, start_dt.Day);

                /*while (dt <= end_dt)
                {
                    HHPstat hs = new HHPstat();
                    hs.TimePeriod = dt;
                    _overallHHPStat.Add(hs);

                    if (Days == 1)
                        dt = dt.AddMinutes(30);
                    else
                        dt = dt.AddDays(1);
                }*/

                foreach (DbDataRecord r in ardr)
                {
                    AgentCDRInfo acdr = AgentsCDR.Find(p => p.AgentName == r["name"].ToString());

                    if (acdr == null)
                    {
                        acdr = new AgentCDRInfo();
                        acdr.AgentName = r["name"].ToString();
                        acdr.AgentNumber = r["extension"].ToString();

                        if (Days <= 1)
                            dt = start_dt;
                        else
                            dt = new DateTime(start_dt.Year, start_dt.Month, start_dt.Day);

                        /*while (dt <= end_dt)
                        {
                            HHPstat hs = new HHPstat();
                            hs.TimePeriod = dt;
                            acdr._HHPStat.Add(hs);

                            if (Days == 1)
                                dt = dt.AddMinutes(30);
                            else
                                dt = dt.AddDays(1);

                        }*/

                        AgentsCDR.Add(acdr);
                    }
                }

                ardr.Close();
                acon.Close();
                #endregion

                /*
                 * Статистика из AsteriskCDRDB
                 */
                #region AsteriskCDRDBStatistik
                MySqlConnection AsteriskCDRConnection = GetACDRDBConnection();

                MySqlCommand cmd;

                if (!forPeriod)
                {
                    cmd = new MySqlCommand("acdrstatistic_operators", AsteriskCDRConnection);
                }
                else
                {
                    cmd = new MySqlCommand("acdrstatistic_operators_period", AsteriskCDRConnection);
                }

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?start_dt", start_dt.ToString("yyyy-MM-dd hh:mm")));
                cmd.Parameters.Add(new MySqlParameter("?end_dt", end_dt.ToString("yyyy-MM-dd hh:mm")));
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        string timePeriod = "";

                        if (!forPeriod)
                            timePeriod = Convert.ToDateTime(record["dt"]).ToShortDateString() + " " + record["h"].ToString() + ":" + record["m"].ToString();
                        else
                            timePeriod = Convert.ToDateTime(record["dt"]).ToShortDateString();

                        AgentCDRInfo acdr = AgentsCDR.Find(p => p.AgentNumber == record["agent"].ToString());

                        if (acdr != null)
                        {
                            HHPstat agenthhps = acdr._HHPStat.Find(p => p.TimePeriod == Convert.ToDateTime(timePeriod));

                            if (agenthhps == null)
                            {
                                agenthhps = new HHPstat();
                                agenthhps.TimePeriod = Convert.ToDateTime(timePeriod);
                                acdr._HHPStat.Add(agenthhps);
                            }

                            agenthhps.Answered = Convert.ToInt32(record["AnsweredTotal"]);
                            agenthhps.AvgAnswerTime = new TimeSpan(0, 0, Convert.ToInt32(record["AvgAnswerTime"]));
                            agenthhps.MaxAnswerTime = new TimeSpan(0, 0, Convert.ToInt32(record["MaxAnswerTime"]));
                            agenthhps.AvgIncomingTalkTime = new TimeSpan(0, 0, Convert.ToInt32(record["incomingavgtalktime"]));
                            agenthhps.AbandonTotal = Convert.ToInt32(record["AbandonTotal"]);
                            agenthhps.Abandonafter10 = Convert.ToInt32(record["Abandonafter10"]);
                            agenthhps.AbandonAvgWaitTime = new TimeSpan(0, 0, Convert.ToInt32(record["AbandonAvgWaitTime"]));
                            agenthhps.AbandonMaxWaitTime = new TimeSpan(0, 0, Convert.ToInt32(record["AbandonMaxWaitTime"]));
                            agenthhps.OutgoingTotal += Convert.ToInt32(record["OutgoingTotal"]);
                            agenthhps.AvgOutgoingTalkTime = new TimeSpan(0, 0, Convert.ToInt32(record["outgoingavgtalktime"]));
                        }
                    }
                }

                rdr.Close();

                cmd = new MySqlCommand("acdrstatistic_operators_summary", AsteriskCDRConnection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?start_dt", start_dt.ToString("yyyy-MM-dd hh:mm")));
                cmd.Parameters.Add(new MySqlParameter("?end_dt", end_dt.ToString("yyyy-MM-dd hh:mm")));
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        AgentCDRInfo acdr = AgentsCDR.Find(p => p.AgentNumber == record["agent"].ToString());

                        if (acdr != null)
                        {
                            acdr.AvgAnswerTime = new TimeSpan(0, 0, Convert.ToInt32(record["AvgAnswerTime"]));
                            acdr.MaxAnswerTime = new TimeSpan(0, 0, Convert.ToInt32(record["MaxAnswerTime"]));
                            acdr.AbandonTotal = Convert.ToInt32(record["AbandonTotal"]);
                            acdr.Abandonafter10 = Convert.ToInt32(record["Abandonafter10"]);
                            acdr.AbandonAvgWaitTime = new TimeSpan(0, 0, Convert.ToInt32(record["AbandonAvgWaitTime"]));
                            acdr.AbandonMaxWaitTime = new TimeSpan(0, 0, Convert.ToInt32(record["AbandonMaxWaitTime"]));
                            acdr.AnsweredTotal = Convert.ToInt32(record["AnsweredTotal"]);
                            acdr.OutgoingTotal = Convert.ToInt32(record["OutgoingTotal"]);
                            acdr.AvgIncomingTalk = new TimeSpan(0, 0, Convert.ToInt32(record["incomingavgtalktime"]));
                            acdr.AvgOutgoingTalk = new TimeSpan(0, 0, Convert.ToInt32(record["outgoingavgtalktime"]));
                            acdr.TotalTalkTime = new TimeSpan(0, 0, Convert.ToInt32(record["totaltalktime"]));
                        }
                    }
                }

                rdr.Close();

                if(!forPeriod)
                    cmd = new MySqlCommand("acdrstatistic_summary", AsteriskCDRConnection);
                else
                    cmd = new MySqlCommand("acdrstatistic_summary_period", AsteriskCDRConnection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?start_dt", start_dt.ToString("yyyy-MM-dd hh:mm")));
                cmd.Parameters.Add(new MySqlParameter("?end_dt", end_dt.ToString("yyyy-MM-dd hh:mm")));
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        string timePeriod = "";

                        if (!forPeriod)
                            timePeriod = Convert.ToDateTime(record["dt"]).ToShortDateString() + " " + record["h"].ToString() + ":" + record["m"].ToString();
                        else
                            timePeriod = Convert.ToDateTime(record["dt"]).ToShortDateString();

                        HHPstat ohhp = _overallHHPStat.Find(p => p.TimePeriod == Convert.ToDateTime(timePeriod));

                        if (ohhp == null)
                        {
                            ohhp = new HHPstat();
                            ohhp.TimePeriod = Convert.ToDateTime(timePeriod);
                            _overallHHPStat.Add(ohhp);
                        }

                        ohhp.OutgoingTotal = Convert.ToInt32(record["OutgoingTotal"]);
                        ohhp.AvgOutgoingTalkTime = new TimeSpan(0, 0, Convert.ToInt32(record["outgoingavgtalktime"]));
                    }
                }

                rdr.Close();

                AsteriskCDRConnection.Close();
                #endregion
                /*
                 * Статистика из qstatlite
                 */
                #region
                MySqlConnection qstatliteconnection = GetConnection();
                if (!forPeriod)
                    cmd = new MySqlCommand("incomingstatistic", qstatliteconnection);
                else
                    cmd = new MySqlCommand("incomingstatistic_period", qstatliteconnection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("?start_dt", start_dt.ToString("yyyy-MM-dd hh:mm")));
                cmd.Parameters.Add(new MySqlParameter("?end_dt", end_dt.ToString("yyyy-MM-dd hh:mm")));
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        string timePeriod = "";

                        if (!forPeriod)
                            timePeriod = Convert.ToDateTime(record["dt"]).ToShortDateString() + " " + record["h"].ToString() + ":" + record["m"].ToString();
                        else
                            timePeriod = Convert.ToDateTime(record["dt"]).ToShortDateString();

                        HHPstat ohhp = _overallHHPStat.Find(p => p.TimePeriod == Convert.ToDateTime(timePeriod));

                        if (ohhp == null)
                        {
                            ohhp = new HHPstat();
                            ohhp.TimePeriod = Convert.ToDateTime(timePeriod);
                            _overallHHPStat.Add(ohhp);
                        }

                        ohhp.IncomingTotal = Convert.ToInt32(record["ENTERQUEUE"]);
                        ohhp.Answered = Convert.ToInt32(record["Answered"]);
                        ohhp.AbandonTotal = Convert.ToInt32(record["Abandoned"]);
                        ohhp.Abandonafter10 = Convert.ToInt32(record["Abandonafter10"]);
                        ohhp.AnsweredFor10Sec = Convert.ToInt32(record["AnsweredFor10Sec"]);
                        ohhp.MAX_QUEUED = Convert.ToInt32(record["MAXQUEUE"]);
                        ohhp.MaxAnswerTime = new TimeSpan(0, 0, Convert.ToInt32(record["MaxAnswerTime"]));
                        ohhp.AvgAnswerTime = new TimeSpan(0, 0, Convert.ToInt32(record["AvgAnswerTime"]));
                        ohhp.MaxAnswerTimeOperator = new TimeSpan(0, 0, Convert.ToInt32(record["MaxAnswerTimeOperator"]));
                        ohhp.AvgAnswerTimeOperator = new TimeSpan(0, 0, Convert.ToInt32(record["AvgAnswerTimeOperator"]));
                        ohhp.AbandonAvgWaitTime = new TimeSpan(0, 0, Convert.ToInt32(record["AbandonAvgWaitTime"]));
                        ohhp.AbandonMaxWaitTime = new TimeSpan(0, 0, Convert.ToInt32(record["AbandonMaxWaitTime"]));
                        ohhp.AnsweredPrc = (Double)ohhp.Answered / (Double)ohhp.IncomingTotal * (Double)100;
                        ohhp.SL = (Double)ohhp.AnsweredFor10Sec / (Double)ohhp.Answered * (Double)100;
                        ohhp.AvgIncomingTalkTime = new TimeSpan(0, 0, Convert.ToInt32(record["AvgIncomingTalkTime"]));
                    }
                }

                rdr.Close();
                qstatliteconnection.Close();
                #endregion

                /*
                 * Статистика по статусам пользователей
                 */
                #region StateStat
                MySqlConnection qconnection = GetQuasiConnection();
                MySqlCommand qcmd = new MySqlCommand("statusstatistics_operators_summary", qconnection);
                qcmd.CommandType = System.Data.CommandType.StoredProcedure;
                qcmd.Parameters.Add(new MySqlParameter("?start_dt", start_dt.ToString("yyyy-MM-dd hh:mm")));
                qcmd.Parameters.Add(new MySqlParameter("?end_dt", end_dt.ToString("yyyy-MM-dd hh:mm")));
                rdr = qcmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    foreach (DbDataRecord record in rdr)
                    {
                        AgentCDRInfo acdr = AgentsCDR.Find(p => p.AgentNumber == record["agent"].ToString());

                        if (acdr != null)
                        {
                            acdr.OnlineTime = new TimeSpan(0, 0, Convert.ToInt32(record["onlinetime"]));
                            acdr.RestTime = new TimeSpan(0, 0, Convert.ToInt32(record["resttime"]));
                            acdr.InternetTime = new TimeSpan(0, 0, Convert.ToInt32(record["internettime"]));
                            acdr.RegisteredTime = new TimeSpan(0, 0, Convert.ToInt32(record["registeredtime"]));
                            acdr.FreeTime = acdr.OnlineTime - acdr.TotalTalkTime;
                        }
                    }
                }

                rdr.Close();
                qconnection.Close();
                #endregion

                AvgAnswerTime = new TimeSpan(0, 0, (int)_overallHHPStat.Average(p => Convert.ToInt32(p.AvgAnswerTime.TotalSeconds)));
                AvgAnswerTimeOperator = new TimeSpan(0, 0, (int)_overallHHPStat.Average(p => Convert.ToInt32(p.AvgAnswerTimeOperator.TotalSeconds)));
                AbandonAvgWaitTime = new TimeSpan(0, 0, (int)_overallHHPStat.Average(p => Convert.ToInt32(p.AbandonAvgWaitTime.TotalSeconds)));
                AvgIncomingTalkTime = new TimeSpan(0, 0, (int)_overallHHPStat.Average(p => Convert.ToInt32(p.AvgIncomingTalkTime.TotalSeconds)));
                AvgOutgoingTalkTime = new TimeSpan(0, 0, (int)_overallHHPStat.Average(p => Convert.ToInt32(p.AvgOutgoingTalkTime.TotalSeconds)));

                for(int i = 0; i < _overallHHPStat.Count; i++)
                {
                    IncomingTotal += _overallHHPStat[i].IncomingTotal;
                    Answered += _overallHHPStat[i].Answered;
                    AbandonTotal += _overallHHPStat[i].AbandonTotal;
                    Abandonafter10 += _overallHHPStat[i].Abandonafter10;
                    AnsweredFor10Sec += _overallHHPStat[i].AnsweredFor10Sec;
                    AnsweredPrc = (Double)Answered / (Double)IncomingTotal * (Double)100;
                    SL = (Double)AnsweredFor10Sec / (Double)Answered * (Double)100;
                    OutgoingTotal += _overallHHPStat[i].OutgoingTotal;
                    _overallHHPStat[i].Total = _overallHHPStat[i].IncomingTotal + _overallHHPStat[i].OutgoingTotal;
                    _overallHHPStat[i].ACR = (Double)_overallHHPStat[i].Abandonafter10 / (Double)_overallHHPStat[i].IncomingTotal * (Double)100;

                    if (MAX_QUEUED < _overallHHPStat[i].MAX_QUEUED)
                        MAX_QUEUED = _overallHHPStat[i].MAX_QUEUED;

                    if (MaxAnswerTime < _overallHHPStat[i].MaxAnswerTime)
                        MaxAnswerTime = _overallHHPStat[i].MaxAnswerTime;

                    if (AbandonMaxWaitTime < _overallHHPStat[i].AbandonMaxWaitTime)
                        AbandonMaxWaitTime = _overallHHPStat[i].AbandonMaxWaitTime;

                    if (MaxAnswerTimeOperator < _overallHHPStat[i].MaxAnswerTimeOperator)
                        MaxAnswerTimeOperator = _overallHHPStat[i].MaxAnswerTimeOperator;
                }

                Total = IncomingTotal + OutgoingTotal;
                ACR = (Double)Abandonafter10 / (Double)IncomingTotal * (Double)100;
            }
            public void ServiceLevelDetail(DateTime start_dt, DateTime end_dt, string queues, string agents)
            {
                MySqlConnection con = GetConnection();
                MySqlCommand com = new MySqlCommand(String.Format(@"SELECT qs.datetime AS datetime, q.queue AS qname, ag.agent AS qagent, ac.event AS qevent, qs.info1 AS info1, qs.info2 AS info2,  qs.info3 AS info3 FROM queue_stats AS qs, qname AS q, qagent AS ag, qevent AS ac WHERE qs.qname = q.qname_id AND qs.qagent = ag.agent_id AND qs.qevent = ac.event_id AND qs.datetime >= '{0}' AND qs.datetime <= '{1}' AND q.queue IN ({2}) AND ag.agent in ({3}) AND ac.event IN ('COMPLETECALLER', 'COMPLETEAGENT','TRANSFER','CONNECT') ORDER BY qs.datetime", start_dt.ToString("yyyy-MM-dd HH:mm:ss"), end_dt.ToString("yyyy-MM-dd HH:mm:ss"), queues, agents), con);
                MySqlDataReader rdr = com.ExecuteReader();

                if (rdr.HasRows)
                    foreach (DbDataRecord record in rdr)
                    {
                        if (record["qevent"].ToString() == "CONNECT")
                        {
                            DateTime dt = Convert.ToDateTime(record["datetime"]);
                            DateTime sdt = Convert.ToDateTime(dt.ToShortDateString());
                            bool fpoh = false;

                            if (dt.Minute < 30)
                                fpoh = true;

                            ServiceLevel sl = SLPHH.Find(p => p.dt == sdt && p.hour == dt.Hour && p.fpoh == fpoh);
                            ServiceLevel sld = SLPD.Find(p => p.dt == sdt);
                            ServiceLevel slm = SLPM.Find(p => p.dt.Month == sdt.Month);

                            if (sl == null)
                            {
                                sl = new ServiceLevel();
                                sl.dt = sdt;
                                sl.hour = dt.Hour;
                                sl.fpoh = fpoh;
                                SLPHH.Add(sl);
                            }
                            if (sld == null)
                            {
                                sld = new ServiceLevel();
                                sld.dt = sdt;
                                sld.hour = dt.Hour;
                                sld.fpoh = fpoh;
                                SLPD.Add(sld);
                            }
                            if (slm == null)
                            {
                                slm = new ServiceLevel();
                                slm.dt = sdt;
                                slm.hour = dt.Hour;
                                slm.fpoh = fpoh;
                                SLPM.Add(slm);
                            }

                            if (Convert.ToInt32(record["info1"]) >= 0 && Convert.ToInt32(record["info1"]) <= 10)
                            {
                                sl.Sec10++;
                                sld.Sec10++;
                                slm.Sec10++;
                            }
                            if (Convert.ToInt32(record["info1"]) > 10)
                            {
                                sl.Sec20++;
                                sl.Sec30++;
                                sl.Sec40++;
                                sl.Sec50++;
                                sl.Sec60++;
                                sl.Sec70++;
                                sl.Sec80++;
                                sl.Sec90++;
                                sl.SecOver90++;

                                sld.Sec20++;
                                sld.Sec30++;
                                sld.Sec40++;
                                sld.Sec50++;
                                sld.Sec60++;
                                sld.Sec70++;
                                sld.Sec80++;
                                sld.Sec90++;
                                sld.SecOver90++;

                                slm.Sec20++;
                                slm.Sec30++;
                                slm.Sec40++;
                                slm.Sec50++;
                                slm.Sec60++;
                                slm.Sec70++;
                                slm.Sec80++;
                                slm.Sec90++;
                                slm.SecOver90++;
                            }
                        }
                    }

                rdr.Close();
                con.Close();
            }
            public void MissedCallsDetail(DateTime start_dt, DateTime end_dt, string queues, string agents)
            {
                MySqlConnection con = GetConnection();
                MySqlCommand com = new MySqlCommand("missed_calls", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add(new MySqlParameter("?start_dt", start_dt.ToString("yyyy-MM-dd hh:mm")));
                com.Parameters.Add(new MySqlParameter("?end_dt", end_dt.ToString("yyyy-MM-dd hh:mm")));
                MySqlDataReader rdr = com.ExecuteReader();

                if (rdr.HasRows)
                    foreach (DbDataRecord record in rdr)
                    {
                        DateTime edt = Convert.ToDateTime(record["edt"]);
                        DateTime adt = Convert.ToDateTime(record["adt"]);
                        MissedCall mc = MissedCalls.Find(p => p.uniqueid == record["uniqueid"].ToString());

                        if (mc == null)
                        {
                            mc = new MissedCall();
                            mc.uniqueid = record["uniqueid"].ToString();
                            MissedCalls.Add(mc);
                        }

                        mc.start_pos = Convert.ToInt32(record["ainfo2"]);
                        mc.end_pos = Convert.ToInt32(record["ainfo1"]);
                        mc.Queue = record["qname"].ToString();
                        mc.AbandonDt = adt;
                        mc.WaitTime = Convert.ToInt32(record["ainfo3"]);
                        mc.EnterQueueDt = edt;
                        mc.phonenumber = record["einfo2"].ToString();
                        mc.agent = record["agent"].ToString();
                        mc.WaitTimeForOperator = new TimeSpan(0, 0 , Convert.ToInt32(record["waittime"]));
                    }

                rdr.Close();
                con.Close();
            }
            public void GetRecordsList(DateTime start_dt, DateTime end_dt)
            {
                MySqlConnection con = GetACDRDBConnection();
                MySqlCommand com = new MySqlCommand(String.Format(@"SELECT * FROM cdr WHERE recordingfile <> '' AND calldate >= '{0}' and calldate <= '{1}'", start_dt.ToString("yyyy-MM-dd HH:mm:ss"), end_dt.ToString("yyyy-MM-dd HH:mm:ss")), con);
                MySqlDataReader rdr = com.ExecuteReader();

                if (rdr.HasRows)
                    foreach (DbDataRecord record in rdr)
                    {
                        RecordInfo ri = new RecordInfo();
                        ri.calldate = Convert.ToDateTime(record["calldate"]);
                        ri.src = record["src"].ToString();
                        ri.dst = record["dst"].ToString();
                        ri.recordingfile = record["recordingfile"].ToString();
                        RecordsList.Add(ri);
                    }

                rdr.Close();
                con.Close();
            }
        }
    }
}