using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamJadwalGantiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Tahun { get; set; }
        public bool? Rutin { get; set; } = false;
        public int? IdJenisGantiMeter { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdMerekMeter { get; set; }
        public string NoSeriMeter { get; set; }
        public DateTime? TglMeter { get; set; }
        public DateTime? TglJadwal { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdJenisNonAir { get; set; }
        public List<ParamJadwalGantiMeterBiayaDto> DetailBiaya { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamJadwalGantiMeterBiayaDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Value { get; set; } = 0;
    }

    public class ParamJadwalGantiMeterFilterDto : ParamJadwalGantiMeterDto
    {
        public int? IdWilayah { get; set; }

        // Note tambahan dari widi
        public int? IdArea { get; set; }
        public string Kelainan { get; set; }
        public string NomorSpk { get; set; }
        public string NomorBa { get; set; }
        public string StatusData { get; set; }
        //
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public DateTime? TglMeterMulai { get; set; }
        public DateTime? TglMeterSampaiDengan { get; set; }
        public DateTime? TglJadwalMulai { get; set; }
        public DateTime? TglJadwalSampaiDdengan { get; set; }
        public bool? AdaSPK { get; set; }
        public bool? AdaBeritaAcara { get; set; }
        public bool? AdaRab { get; set; }
    }

    public class ParamJadwalGantiMeterGenerateDto
    {
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public string PasswordUser { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPembatalanJadwalGantiMeterDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdAlasanBatal { get; set; }
        public List<int?> IdPelangganAir { get; set; }
    }
}
