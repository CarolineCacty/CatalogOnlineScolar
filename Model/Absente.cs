//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CatalogScolarOnline.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Absente
    {
        public int AbsentaID { get; set; }
        public System.DateTime Data_absenta { get; set; }
        public bool Motivata { get; set; }
        public int ElevID { get; set; }
        public int PredareID { get; set; }
    
        public virtual Elevi Elevi { get; set; }
        public virtual Predare Predare { get; set; }
    }
}
