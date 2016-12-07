using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fusion.Models.CallCenter
{
    public class CDRViewModels
    {
        public class CDRInfo
        {
            [DisplayName("Очередь")]
            public IEnumerable<string> SelectedQueues { get; set; }
            [DisplayName("Оператор")]
            public IEnumerable<string> SelectedAgents { get; set; }
            [DisplayName("Начальная дата")]
            public DateTime start_dt { get; set; }
            [DisplayName("Конечная дата")]
            public DateTime end_dt { get; set; }
            public IEnumerable<SelectListItem> Agents { get; set; }
            public IEnumerable<SelectListItem> Queues { get; set; }
            public CDR.CDRInfo cdrinfo;
            public CDRInfo()
            {
                cdrinfo = new CDR.CDRInfo();
                SelectedQueues = cdrinfo.Queues.Where(p => p.name == "2000" || p.name == "2003").Select(x => x.name);
                SelectedAgents = cdrinfo.Agents.Select(x => x.name);
                Queues = cdrinfo.Queues
                    .Select(x => new SelectListItem {
                        Value = x.name,
                        Text = x.name,
                    }).ToList();
                Agents = cdrinfo.Agents
                    .Select(x => new SelectListItem
                    {
                        Value = x.name,
                        Text = x.name,
                    }).ToList();

                start_dt = DateTime.Today;
                end_dt = DateTime.Today;
            }
            public void GetOverallStatistic(bool forPeriod)
            {
                string queues = "";
                string agents = "";

                foreach (var queue in SelectedQueues)
                {
                    queues += "'" + queue.ToString() + "',";
                }

                queues = queues.Substring(0, queues.Length - 1);

                foreach (var agent in SelectedAgents)
                {
                    agents += "'" + agent.ToString() + "',";
                }

                agents = agents.Substring(0, agents.Length - 1);

                cdrinfo.GetOverallStatistic(start_dt, end_dt, queues, agents, forPeriod);
            }
            public void ServiceLevelDetail()
            {
                string queues = "";
                string agents = "";

                foreach (var queue in SelectedQueues)
                {
                    queues += "'" + queue.ToString() + "',";
                }

                queues = queues.Substring(0, queues.Length - 1);

                foreach (var agent in SelectedAgents)
                {
                    agents += "'" + agent.ToString() + "',";
                }

                agents = agents.Substring(0, agents.Length - 1);

                cdrinfo.ServiceLevelDetail(start_dt, end_dt, queues, agents);
            }
            public void MissedCallsDetail()
            {
                string queues = "";
                string agents = "";

                foreach (var queue in SelectedQueues)
                {
                    queues += "'" + queue.ToString() + "',";
                }

                queues = queues.Substring(0, queues.Length - 1);

                foreach (var agent in SelectedAgents)
                {
                    agents += "'" + agent.ToString() + "',";
                }

                agents = agents.Substring(0, agents.Length - 1);

                cdrinfo.MissedCallsDetail(start_dt, end_dt, queues, agents);
            }
            public void GetRecordsList()
            {
                cdrinfo.GetRecordsList(start_dt, end_dt);
            }
        }
    }
}