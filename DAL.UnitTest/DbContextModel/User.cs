//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.UnitTest.DbContextModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Status = "A";
            this.ModifiedAt = new DateTime(635197650711110000, DateTimeKind.Unspecified);
            this.Notes = new HashSet<Note>();
        }
    
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
    
        public virtual ICollection<Note> Notes { get; set; }
    }
}
