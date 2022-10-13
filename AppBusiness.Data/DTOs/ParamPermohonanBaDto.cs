using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanBaDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public DateTime? TanggalBa { get; set; }
        public int? IdUser { get; set; }
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

        public string PersilNamaPaket { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public string DistribusiNamaPaket { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;
        public List<ParamPermohonanRkpDetailDto> DetailRkp { get; set; }
        public List<ParamPetugasBaDto> Petugas { get; set; }
    }

    public class ParamPetugasBaDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamPermohonanRkpDetailDto
    {
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Uraian { get; set; }
        public decimal? HargaSatuan { get; set; } = 0;
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Jumlah { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Keuntungan { get; set; } = 0;
        public decimal? JasaDariBahan { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public string Kategori { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public decimal? QtyRkp { get; set; } = 0;
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagDistribusi { get; set; } = false;
    }
}
