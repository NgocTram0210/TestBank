//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestScore
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public Nullable<int> Test { get; set; }
        public Nullable<System.DateTime> DateTest { get; set; }
        public Nullable<int> Class { get; set; }
        public Nullable<double> Time { get; set; }
        public Nullable<int> Level { get; set; }
    }
}
