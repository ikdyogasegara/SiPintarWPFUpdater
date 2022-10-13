using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKeluargaDto
    {
        public int? IdPdam { get; set; }
        public int? IdKeluarga { get; set; }
        public int? IdPegawai { get; set; }
        public string NamaKeluarga { get; set; }
        public string Status { get; set; }
        public DateTime? TglLahir { get; set; }
        public bool? FlagKawin { get; set; } = false;
        public int? IdJenisKelamin { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NoPenduduk { get; set; }
        public string NoBpjsKes { get; set; }
        public bool? FlagTanggung { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKeluargaFilterDto : ParamMasterKeluargaDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
