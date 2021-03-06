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
    
    public partial class SyncIteration
    {
        public SyncIteration()
        {
            this.SyncIterationChunk = new HashSet<SyncIterationChunk>();
        }
    
        public System.Guid Id { get; set; }
        public string StandaloneServer { get; set; }
        public byte[] ExportEmployeesException { get; set; }
        public byte[] HeaderEmployees { get; set; }
        public int EmployeesChunksCount { get; set; }
        public byte[] ImportEventsException { get; set; }
        public byte[] HeaderEvents { get; set; }
        public int EventsChunksCount { get; set; }
        public int IsImportEventsComplete { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual ICollection<SyncIterationChunk> SyncIterationChunk { get; set; }
    }
}
