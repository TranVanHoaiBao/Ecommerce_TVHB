//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce_TVHB.ContactDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public Nullable<int> Categoryid { get; set; }
        public string producter { get; set; }
        public string specication { get; set; }
        public string origin { get; set; }
        public string ShortDes { get; set; }
        public string FullDes { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PriceDiscount { get; set; }
        public Nullable<short> discount { get; set; }
        public Nullable<int> Soild { get; set; }
        public Nullable<int> Available { get; set; }
        public Nullable<int> Typeid { get; set; }
        public string Slug { get; set; }
        public Nullable<int> Bandld { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> Showonhomepage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
    }
}