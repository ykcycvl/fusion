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
    
    public partial class ArticleCategory
    {
        public ArticleCategory()
        {
            this.Article = new HashSet<Article>();
        }
    
        public int id { get; set; }
        public string CategoryName { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
    
        public virtual ICollection<Article> Article { get; set; }
    }
}