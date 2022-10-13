using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamJadwalGantiMeterRabDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Tahun { get; set; }
        public int? IdJenisNonair { get; set; }
        public DateTime? TanggalRab { get; set; }
        public int? IdUser { get; set; }
        public string NamaPaketReguler { get; set; }
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public string NamaPaketPersil { get; set; }
        public bool? FlagDialihkanKeVendorPersil { get; set; } = false;
        public bool? FlagBiayaDibebankankePdamPersil { get; set; } = false;
        public List<ParamJadwalGantiMeterRabDetailDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamJadwalGantiMeterRabDetailDto
    {
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public decimal? Harga { get; set; } = 0;
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? SebelumPpn { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Keuntungan { get; set; } = 0;
        public decimal? Jumlah { get; set; } = 0;
        public string Kategori { get; set; }
        public decimal? QtyRkp { get; set; } = 0;
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagPersil { get; set; } = false;
    }

    public class ParamJadwalGantiMeterBPPIDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Tahun { get; set; }
        public int? IdUser { get; set; }
        public DateTime? TanggalBppi { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamJadwalGantiMeterSPKPdanSPPBDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Tahun { get; set; }
        public int? IdUser { get; set; }
        public DateTime? TanggalSpkp { get; set; }
        public DateTime? TanggalSppb { get; set; }
        public int? IdUserRequest { get; set; }
        public List<ParamPetugasSPKPJadwalGantiMeterDto> Petugas { get; set; }
    }

    public class ParamPetugasSPKPJadwalGantiMeterDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamJadwalGantiMeterRabFotoDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdPelangganAir { get; set; }
    }
}
