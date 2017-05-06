using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Models
{
    public class CalendarModel
    {
        public class Employee
        {
            public string Name { get; set; }
            public string Login { get; set; }
        }
        public List<Employee> employees { get; set; }
        public CalendarModel()
        {
            employees = new List<Employee>();
        }
    }

    public class CalendarTaskViewModel
    {
        private CalendarEntities db = new CalendarEntities();

        public int id { get; set; }
        [Display(Name="Задача")]
        public string title { get; set; }
        [Display(Name = "Описание")]
        public string description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string username { get; set; }
        public List<string> uNames { get; set; }
        public IEnumerable<SelectListItem> MemberList
        {
            get
            {
                List<SelectListItem> members = new List<SelectListItem>();

                using (var context = new PrincipalContext(ContextType.Domain, "fg.local"))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, "Управляющие");

                    if (group != null)
                        foreach (Principal p in group.GetMembers())
                        {
                            UserPrincipal theUser = p as UserPrincipal;

                            if (!theUser.IsAccountLockedOut())
                            {
                                members.Add(new SelectListItem() { Text = theUser.Name, Value = theUser.SamAccountName });
                            }
                        }
                }

                return members;
            }
        }

        public void Save()
        {
            if (id == 0)
            {
                mc_task t = new mc_task() { title = title, description = description, DateStart = DateStart, DateEnd = DateEnd, username = username, dt = DateTime.Now };
                db.mc_task.Add(t);
                db.SaveChanges();
                this.id = t.id;
            }
            else
            {
                mc_task t = db.mc_task.FirstOrDefault(p => p.id == id);

                if (t != null)
                {
                    t.title = title;
                    t.description = description;
                    t.DateStart = DateStart;
                    t.DateEnd = DateEnd;
                    t.username = username;
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < uNames.Count; i++)
            {
                string s = uNames[i];
                var ut = db.mc_userTask.FirstOrDefault(p => p.taskId == id && p.username == s);

                if (ut == null)
                    db.mc_userTask.Add(new mc_userTask() { taskId = id, username = uNames[i], dt = DateTime.Now, appointed = username });
            }

            db.SaveChanges();
        }
        public CalendarTaskViewModel(int id)
        {
            mc_task t = db.mc_task.FirstOrDefault(p => p.id == id);
            this.id = t.id;
            this.title = t.title;
            this.description = t.description;
            this.DateStart = t.DateStart;
            this.DateEnd = t.DateEnd;
            this.username = t.username;
        }
        public CalendarTaskViewModel()
        {
        }
    }
    public class Planner
    {
        private CalendarEntities db = new CalendarEntities();
        [Display(Name="Месяц")]
        public DateTime Period { get; set; }
        public List<mc_task> Tasks { get; set; }
        public string userName { get; set; }
        public void GetTasks()
        {
            Tasks = new List<mc_task>();
            DateTime DateStart = new DateTime(Period.Year, Period.Month, 1);
            DateTime DateEnd = new DateTime(Period.Year, Period.Month, DateTime.DaysInMonth(Period.Year, Period.Month));

            var userTasks = (from t in db.mc_task
                             join ut in db.mc_userTask on t.id equals ut.taskId
                             where ut.username == userName
                             select t
                ).Where(p => p.DateStart <= DateEnd && DateEnd >= DateStart).ToList();

            if (userTasks == null)
                return;

            Tasks = userTasks.ToList();
        }
    }
}