//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoruHazirla.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sinavlar
    {
        public int SinavId { get; set; }
        public string Title { get; set; }
        public string Icerik { get; set; }
        public string s1 { get; set; }
        public string s1a { get; set; }
        public string s1b { get; set; }
        public string s1c { get; set; }
        public string s1d { get; set; }
        public string s1cevap { get; set; }
        public string s2 { get; set; }
        public string s2a { get; set; }
        public string s2b { get; set; }
        public string s2c { get; set; }
        public string s2d { get; set; }
        public string s2cevap { get; set; }
        public string s3 { get; set; }
        public string s3a { get; set; }
        public string s3b { get; set; }
        public string s3c { get; set; }
        public string s3d { get; set; }
        public string s3cevap { get; set; }
        public string s4 { get; set; }
        public string s4a { get; set; }
        public string s4b { get; set; }
        public string s4c { get; set; }
        public string s4d { get; set; }
        public string s4cevap { get; set; }
        public string Tarih { get; set; }
        public string Hazirlayan { get; set; }
    
        public virtual Kullanici Kullanici { get; set; }
    }
}
