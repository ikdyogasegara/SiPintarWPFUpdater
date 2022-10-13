using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekapAbsensiDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public int? HariKerja { get; set; } = 0;
        public int? Izin { get; set; } = 0;
        public int? Sakit { get; set; } = 0;
        public int? Alpha { get; set; } = 0;
        public int? Cuti { get; set; } = 0;
        public int? Dispensasi { get; set; } = 0;
        public int? Terlambat { get; set; } = 0;
        public int? AkumulasiTerlambat { get; set; } = 0;
        public int? Mendahului { get; set; } = 0;
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekapAbsensiFilterDto : ParamRekapAbsensiDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
    }

    public class ParamRekapAbsensiPostingDto
    {
        public int? IdPdam { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public int? HariKerja { get; set; } = 0;
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekapAbsensiMultipleDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? KodePeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

        public List<ParamRekapAbsensiDetailPegawaiDto> Detail { get; set; }
    }

    public class ParamRekapAbsensiDetailPegawaiDto
    {
        public int? IdPegawai { get; set; }
        public int? HariKerja { get; set; } = 0;
        public int? Izin { get; set; } = 0;
        public int? Sakit { get; set; } = 0;
        public int? Alpha { get; set; } = 0;
        public int? Cuti { get; set; } = 0;
        public int? Dispensasi { get; set; } = 0;
        public int? Terlambat { get; set; } = 0;
        public int? AkumulasiTerlambat { get; set; } = 0;
        public int? Mendahului { get; set; } = 0;
    }
}
