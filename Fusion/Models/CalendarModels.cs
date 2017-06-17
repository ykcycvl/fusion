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
        [Required]
        public string title { get; set; }
        [Display(Name = "Описание")]
        [Required]
        public string description { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        public string username { get; set; }
        public bool Deleted { get; set; }
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
                                if (uNames != null)
                                    if (uNames.FirstOrDefault(t => t == theUser.SamAccountName) != null)
                                        members.Add(new SelectListItem() { Text = theUser.Name, Value = theUser.SamAccountName, Selected = true });
                                    else
                                        members.Add(new SelectListItem() { Text = theUser.Name, Value = theUser.SamAccountName, Selected = false });
                                else
                                    members.Add(new SelectListItem() { Text = theUser.Name, Value = theUser.SamAccountName, Selected = false });
                        }
                }

                return members;
            }
        }

        public void Clone()
        {
            if (id == 0)
                return;
            else
            {
                mc_task t = db.mc_task.FirstOrDefault(p => p.id == id);

                if (t != null)
                {
                    mc_task task = new mc_task() { title = t.title, description = t.description, DateStart = t.DateStart, DateEnd = t.DateEnd, username = username, dt = DateTime.Now };
                    db.mc_task.Add(task);
                    db.SaveChanges();
                    this.id = task.id;
                }
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
                }

                var uts = db.mc_userTask.Where(p => p.taskId == id).ToList();

                foreach (var ut in uts)
                    db.mc_userTask.Remove(ut);

                db.SaveChanges();
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
        public void Delete()
        {
            mc_task t = db.mc_task.FirstOrDefault(p => p.id == id);

            if (t != null)
                t.deleted = true;

            db.SaveChanges();
        }
        public CalendarTaskViewModel(int id)
        {
            mc_task t = db.mc_task.FirstOrDefault(p => p.id == id);
            uNames = new List<string>();
            this.id = t.id;
            this.title = t.title;
            this.description = t.description;
            this.DateStart = t.DateStart;
            this.DateEnd = t.DateEnd;
            this.username = t.username;
            this.Deleted = t.deleted;

            var ut = db.mc_userTask.Where(p => p.taskId == id);

            if (ut != null)
                foreach (var u in ut.ToList())
                    uNames.Add(u.username);
        }
        public void GetTaskByUTID(int utid)
        {
            mc_userTask t = db.mc_userTask.FirstOrDefault(p => p.id == utid);
            uNames = new List<string>();
            this.id = t.mc_task.id;
            this.title = t.mc_task.title;
            this.description = t.mc_task.description;
            this.DateStart = t.mc_task.DateStart;
            this.DateEnd = t.mc_task.DateEnd;
            this.username = t.mc_task.username;
            this.Deleted = t.mc_task.deleted;

            var ut = db.mc_userTask.Where(p => p.taskId == id);

            if (ut != null)
                foreach (var u in ut.ToList())
                    uNames.Add(u.username);
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
                             where ut.username == userName && !t.deleted
                             select t
                ).Where(p => p.DateStart <= DateEnd && DateEnd >= DateStart).ToList();

            if (userTasks == null)
                return;

            Tasks = userTasks.ToList();
        }
    }
}