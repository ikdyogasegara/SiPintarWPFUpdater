using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterTipePermohonanDto
    {
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string Kategori { get; set; }
        public bool? FlagPelangganAir { get; set; } = false;
        public bool? FlagPelangganLimbah { get; set; } = false;
        public bool? FlagPelangganLltt { get; set; } = false;
        public bool? FlagNonPelanggan { get; set; } = false;
        public bool? Step_Spk { get; set; }
        public bool? Step_Rab { get; set; }
        public bool? Step_BeritaAcara { get; set; }
        public bool? Step_Verifikasi { get; set; }
        public bool? FlagSppb { get; set; } = true;
        public DateTime? WaktuUpdate { get; set; }
        public List<MasterTipePermohonanDetailDto> DetailParameter { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public decimal PersentasePpn { get; set; } = 0;
        public List<MasterJenisNonAirDetailDto> DetailBiaya { get; set; }

        public List<StatusPermohonan> StatusPermohonan { get; set; } = new List<StatusPermohonan>
        {
            new StatusPermohonan { Content = "Aktif", Foreground = "#1E7B49", Background = "#F5F5F5" },
            new StatusPermohonan
            {
                Content = "Menunggu Pembayaran", Foreground = "#E31F52", Background = "#FCE9ED"
            },
            new StatusPermohonan { Content = "Sudah SPK", Foreground = "#F2542A", Background = "#FDDB97" },
            new StatusPermohonan
            {
                Content = "Menunggu Pelunasan RAB", Foreground = "#B263D7", Background = "#DDCCF4"
            },
            new StatusPermohonan
            {
                Content = "Menunggu Berita Acara", Foreground = "#E31F52", Background = "#FCE9ED"
            },
            new StatusPermohonan { Content = "Sudah Berita Acara", Foreground = "#026AB5", Background = "#eaf8ff" }
        };
    }

    public class MasterTipePermohonanDetailDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public string Item { get; set; }
        public int? IdListData { get; set; }
        public string EndPoint { get; set; }
        public string FieldKey { get; set; }
        public string FieldDisplayValue1 { get; set; }
        public string FieldDisplayName1 { get; set; }
        public string FieldDisplayValue2 { get; set; }
        public string FieldDisplayName2 { get; set; }
        public bool? IsRequired { get; set; } = true;
    }

    public class StatusPermohonan
    {
        public string Content { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }
    }
}
