//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fusion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class p_task
    {
        public p_task()
        {
            this.p_taskChecklist = new HashSet<p_taskChecklist>();
            this.p_userTask = new HashSet<p_userTask>();
        }
    
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDT { get; set; }
        public System.DateTime EndDT { get; set; }
        public int StatusID { get; set; }
    
        public virtual p_status p_status { get; set; }
        public virtual ICollection<p_taskChecklist> p_taskChecklist { get; set; }
        public virtual ICollection<p_userTask> p_userTask { get; set; }
    }
}
