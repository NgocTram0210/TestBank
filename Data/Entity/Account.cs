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
    
    public partial class Account
    {
        public int AccountID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string AccountName { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Decentralization { get; set; }
    }
}
