using System;

namespace AppBusiness.Data.DTOs
{
    public class MutasiJabatanDetailDto : ICloneable
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public int? Urutan { get; set; }
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
        public string Tugas { get; set; }
        public int? IdJabatanBaru { get; set; }
        public string JabatanBaru { get; set; }
        public int? IdDepartemenBaru { get; set; }
        public string DepartemenBaru { get; set; }
        public int? IdDivisiBaru { get; set; }
        public string DivisiBaru { get; set; }
        public int? IdAreaKerjaBaru { get; set; }
        public string AreaKerjaBaru { get; set; }
        public string TugasBaru { get; set; }
        public DateTime? Waktuupdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
