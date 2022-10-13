using System;

namespace AppBusiness.Data.DTOs
{
    public class MutasiStatusCapegDetailDto : ICloneable
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public string Jabatan { get; set; }
        public int? IdDepartemen { get; set; }
        public string Departemen { get; set; }
        public int? IdDivisi { get; set; }
        public string Divisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public string AreaKerja { get; set; }
        public int? IdPendidikan { get; set; }
        public string Pendidikan { get; set; }
        public string Tugas { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
        public int? Mkg_Thn { get; set; }
        public int? Mkg_Bln { get; set; }
        public string NoPegawaiBaru { get; set; }
        public DateTime? Waktuupdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
