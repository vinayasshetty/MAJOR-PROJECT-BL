//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentResultManagement.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentResult
    {
        public int ResultId { get; set; }
        public Nullable<int> AdminId { get; set; }
        public string UniversitySeatNumber { get; set; }
        public string SubjectId { get; set; }
        public Nullable<int> Marks { get; set; }
        public string Grade { get; set; }
        public string Result { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
