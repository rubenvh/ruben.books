//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ruben.Books.CommandLine.OldDatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class boekgelezen
    {
        public int BookId { get; set; }
        public System.DateTime datum { get; set; }
    
        public virtual boeken Book { get; set; }
    }
}
