using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirBaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string NomorBa { get; set; }
        public DateTime? TanggalBa { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }

        public string PersilNamaPaket { get; set; }
        public List<PermohonanPelangganAirRkpDetailDto> DetailPersil { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public string DistribusiNamaPaket { get; set; }
        public List<PermohonanPelangganAirRkpDetailDto> DetailDistribusi { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;


        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public string StatusPemesanan { get; set; }
        public string StatusRangkaianMeter { get; set; }
        public string StatusTutupTotal { get; set; }
        public string StatusSegel { get; set; }
        public string MerekMeter { get; set; }
        public string WarnaSegel { get; set; }
        public string Memo { get; set; }
        public string StatusPembayaran { get; set; }
        public DateTime? WaktuAntarMulai { get; set; }
        public DateTime? WaktuAntarSelesai { get; set; }
        public decimal? JumlahTangki { get; set; }
        public decimal? JumlahSisaTangki { get; set; }
        public decimal? DiantarSebelumnya { get; set; }
        public decimal? DiantarHariIni { get; set; }
        public string KeteranganLapangan { get; set; }
        public string NoSeriMeter { get; set; }
        public decimal? AngkatMeter { get; set; }
        public string NoSeriSegel { get; set; }
        public bool? FlagBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
