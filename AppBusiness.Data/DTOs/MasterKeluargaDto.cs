using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKeluargaDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdKeluarga { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string NamaKeluarga { get; set; }
        public string Status { get; set; }
        public DateTime? TglLahir { get; set; }
        public bool? FlagKawin { get; set; } = false;
        public int? IdJenisKelamin { get; set; }
        public string JenisKelamin { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NamaPekerjaan { get; set; }
        public string NoPenduduk { get; set; }
        public string NoBpjsKes { get; set; }
        public bool? FlagTanggung { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
