//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class classs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public classs()
        {
            this.attendances = new HashSet<attendance>();
            this.student_reg = new HashSet<student_reg>();
        }
    
        public int class_id { get; set; }
        public string class_level { get; set; }
        public string class_day { get; set; }
        public string class_starttime { get; set; }
        public string class_endtime { get; set; }
        public Nullable<int> student_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<attendance> attendances { get; set; }
        public virtual student student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_reg> student_reg { get; set; }
    }
}
