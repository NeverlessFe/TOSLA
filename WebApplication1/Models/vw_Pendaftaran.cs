//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_Pendaftaran
    {
        public long RecordID { get; set; }
        public string RegistrationID { get; set; }
        public string Periode { get; set; }
        public string NamaLengkap { get; set; }
        public string TanggalLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public Nullable<bool> MemilikiLahan { get; set; }
        public string LuasLahan { get; set; }
        public Nullable<bool> SaluranIrigasi { get; set; }
        public string JarakIrigasi { get; set; }
        public Nullable<bool> PengalamanJaheMerah { get; set; }
        public string TerkahirMenanamJaheMerah { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string RejectReason { get; set; }
        public string Hasil { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}