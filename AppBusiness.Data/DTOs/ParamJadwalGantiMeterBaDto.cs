using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamJadwalGantiMeterBaDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public DateTime? TanggalBa { get; set; }
        public int? IdUser { get; set; }
        public List<ParamJadwalGantiMeterBaDetailDto> Detail { get; set; }
        public List<ParamPetugasBaJadwalGantiMeterDto> Petugas { get; set; }
    }

    public class ParamJadwalGantiMeterBaDetailDto
    {
        public int? IdPelangganAir { get; set; }
        public string Keterangan { get; set; }
        public decimal? AngkaMeterLama { get; set; }
        public decimal? AngkaMeterBaru { get; set; }
        public string NoSeriMeter { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdMerekMeter { get; set; }
        public bool? FlagBatal { get; set; } = false;
        public int? IdAlasanBatal { get; set; }
    }

    public class ParamPetugasBaJadwalGantiMeterDto
    {
        public int? IdPegawai { get; set; }
    }


    public class ParamJadwalGantiMeterBaFotoDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdPelangganAir { get; set; }
    }

    public class ParamJadwalGantiMeterBaDeleteDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdPelangganAir { get; set; }
    }
}
