using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fusion.Models
{
    public class ITDeptModels
    {
        private CalendarEntities db = new CalendarEntities();

        public class Task
        {
            public int id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public string GetTasks(string username)
        {
            string result = "";
            var utasks = db.mc_userTask.Where(p => !p.mc_task.deleted && p.username == username).ToList();

            foreach (var ut in utasks)
            {
                result = result += "\r\n" + String.Format("<event id=\"{0}\" start_date=\"{1}\" end_date=\"{2}\" text=\"{3}\" details=\"{4}\" event_type=\"1\" userId=\"{5}\" />", ut.mc_task.id, ut.mc_task.DateStart.ToString("yyyy-MM-dd HH:mm"), ut.mc_task.DateEnd.ToString("yyyy-MM-dd 18:00"), ut.mc_task.title, ut.mc_task.description, ut.username);
            }

            return result;
        }

        public List<Task> Deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<Task[]>(jsonString).ToList();
        }
    }

    public class ITModels
    {
        private Entities db = new Entities();
        public string GetTaskList()
        {
            string result = "";

            var tasks = db.p_task;

            foreach(var task in tasks)
                result = result += "\r\n" + String.Format("<event id=\"{0}\" start_date=\"{1}\" end_date=\"{2}\" text=\"{3}\" details=\"{4}\" event_type=\"1\" userId=\"{5}\" />", task.id, task.StartDT.ToString("yyyy-MM-dd HH:mm"), task.EndDT.ToString("yyyy-MM-dd 18:00"), task.Title, task.Description, "");

            return result;
        }
    }
}