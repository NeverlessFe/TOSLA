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
    
    public partial class vw_EJOLSchedule
    {
        public string MoU_FK { get; set; }
        public string NoBatch_FK { get; set; }
        public bool Sosialiasi { get; set; }
        public string TanggalSosialiasi { get; set; }
        public Nullable<System.DateTime> SosialisasiEndDate { get; set; }
        public bool Penanaman { get; set; }
        public string TanggalPenanaman { get; set; }
        public Nullable<System.DateTime> PenanamanEndDate { get; set; }
        public bool Monitoring { get; set; }
        public Nullable<int> TahapKe { get; set; }
        public string TanggalMonitoring { get; set; }
        public Nullable<System.DateTime> MonitoringEndDate { get; set; }
        public bool Panen { get; set; }
        public string TanggalPanen { get; set; }
        public Nullable<System.DateTime> PanenEndDate { get; set; }
        public bool MonitoringPanen { get; set; }
        public string TanggalMonitoringPanen { get; set; }
        public Nullable<System.DateTime> MonitoringPanenEndDate { get; set; }
        public bool Pengiriman { get; set; }
        public string TanggalPengiriman { get; set; }
        public Nullable<System.DateTime> PengirimanEndDate { get; set; }
        public string Alamat { get; set; }
    }
}
