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
    
    public partial class TblFills
    {
        public string Patient_Id { get; set; }
        public int File_Num { get; set; }
        public string temp { get; set; }
        public string TherapistId { get; set; }
    
        public virtual TblFile TblFile { get; set; }
        public virtual TblPatient TblPatient { get; set; }
    }
}
