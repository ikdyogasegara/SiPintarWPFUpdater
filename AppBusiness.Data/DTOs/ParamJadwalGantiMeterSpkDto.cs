using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamJadwalGantiMeterSpkDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public DateTime? TanggalSpk { get; set; }
        public int? IdUser { get; set; }
        public List<int?> IdPelangganAir { get; set; }
        public List<ParamPetugasSpkJadwalGantiMeterDto> Petugas { get; set; }
        public List<ParamJadwalGantiMeterSpkSPPBDto> Sppb { get; set; }
    }

    public class ParamPetugasSpkJadwalGantiMeterDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamJadwalGantiMeterSpkSPPBDto
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public decimal? Harga { get; set; } = 0;
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
    }

    public class ParamJadwalGantiMeterSpkFotoDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdPelangganAir { get; set; }
    }

    public class ParamJadwalGantiMeterSpkDeleteDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdPelangganAir { get; set; }
    }
}
