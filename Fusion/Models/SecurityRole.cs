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
    
    public partial class SecurityRole
    {
        public SecurityRole()
        {
            this.EmployeeDepartmentSecurityRole = new HashSet<EmployeeDepartmentSecurityRole>();
            this.EmployeeSecurityRole = new HashSet<EmployeeSecurityRole>();
            this.SecurityRoleReport = new HashSet<SecurityRoleReport>();
        }
    
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public string Description { get; set; }
        public int Enabled { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual ICollection<EmployeeDepartmentSecurityRole> EmployeeDepartmentSecurityRole { get; set; }
        public virtual ICollection<EmployeeSecurityRole> EmployeeSecurityRole { get; set; }
        public virtual ICollection<SecurityRoleReport> SecurityRoleReport { get; set; }
    }
}