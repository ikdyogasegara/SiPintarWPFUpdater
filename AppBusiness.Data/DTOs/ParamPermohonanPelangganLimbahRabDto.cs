using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanPelangganLimbahRabDto
    {
        public int? IdUserRequest { get; set; }

        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdJenisNonair { get; set; }
        public DateTime? TanggalRab { get; set; }
        public int? IdUser { get; set; }
        public string PersilNamaPaket { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public decimal? PersilSubTotal { get; set; } = 0;
        public decimal? PersilDibebankankePdam { get; set; } = 0;
        public decimal? PersilTotal { get; set; } = 0;

        public string DistribusiNamaPaket { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;
        public decimal? DistribusiSubTotal { get; set; } = 0;
        public decimal? DistribusiDibebankankePdam { get; set; } = 0;
        public decimal? DistribusiTotal { get; set; } = 0;

        public List<ParamPermohonanPelangganLimbahRabDetailDto> Detail { get; set; }

        public decimal? RekapSubTotal { get; set; } = 0;
        public decimal? RekapDibebankankePdam { get; set; } = 0;
        public decimal? RekapTotal { get; set; } = 0;
    }

    public class ParamPermohonanPelangganLimbahRabDetailDto
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

    public class ParamPermohonanPelangganLimbahBPPIDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public DateTime? TanggalBppi { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPermohonanPelangganLimbahSPKPdanSPPBDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public DateTime? TanggalSpkp { get; set; }
        public DateTime? TanggalSppb { get; set; }
        public int? IdUserRequest { get; set; }
        public List<ParamPetugasSPKPLimbahDto> Petugas { get; set; }
    }

    public class ParamPetugasSPKPLimbahDto
    {
        public int? IdPegawai { get; set; }
    }
}
