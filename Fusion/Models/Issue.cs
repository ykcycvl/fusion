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
    
    public partial class Issue
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string IssueContent { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime LastModified { get; set; }
        public string LastEditor { get; set; }
    }
}