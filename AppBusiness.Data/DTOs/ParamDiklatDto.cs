using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamDiklatDto
    {
        public int? IdPdam { get; set; }
        public int? IdDiklat { get; set; }
        public string NoSertifikat { get; set; }
        public string Uraian { get; set; }
        public int? Tahun { get; set; }
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public string Tempat { get; set; }
        public string Penyelenggara { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamDiklatDetailDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamDiklatDetailDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamDiklatFilterDto : ParamDiklatDto
    {
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
    }
}
