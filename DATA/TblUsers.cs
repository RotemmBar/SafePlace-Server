//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblUsers()
        {
            this.TblTreats = new HashSet<TblTreats>();
            this.TblFile = new HashSet<TblFile>();
        }
    
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Patient_Id { get; set; }
        public bool IsSuperUser { get; set; }
        public Nullable<int> IsSuperUserNew { get; set; }
    
        public virtual TblPatient TblPatient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblTreats> TblTreats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblFile> TblFile { get; set; }
    }
}
