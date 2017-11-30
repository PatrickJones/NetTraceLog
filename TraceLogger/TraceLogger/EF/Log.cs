//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TraceLogger.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string ApplicationId { get; set; }
        public string TraceEventType { get; set; }
        public int TraceId { get; set; }
        public string Message { get; set; }
        public string Format { get; set; }
        public string Data { get; set; }
        public string CallStack { get; set; }
        public System.DateTime DateTime { get; set; }
        public string LogicalOperationStack { get; set; }
        public int ProcessId { get; set; }
        public string ThreadId { get; set; }
        public long Timestamp { get; set; }
        public System.Guid UserId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
    }
}
