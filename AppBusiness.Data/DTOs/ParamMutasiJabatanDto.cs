using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMutasiJabatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public string NoSk { get; set; }
        public DateTime? TglSk { get; set; }
        public DateTime? TglBerlaku { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamMutasiJabatanDetailDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMutasiJabatanDetailDto
    {
        public int? Urutan { get; set; } = 0;
        public int? IdPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public string Tugas { get; set; }
        public int? IdJabatanBaru { get; set; }
        public int? IdDepartemenBaru { get; set; }
        public int? IdDivisiBaru { get; set; }
        public int? IdAreaKerjaBaru { get; set; }
        public string TugasBaru { get; set; }
    }

    public class ParamMutasiJabatanFilterDto : ParamMutasiJabatanDto
    {
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdStatusPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public string Tugas { get; set; }
        public int? IdJabatanBaru { get; set; }
        public int? IdDepartemenBaru { get; set; }
        public int? IdDivisiBaru { get; set; }
        public int? IdAreaKerjaBaru { get; set; }
        public string TugasBaru { get; set; }
        public bool? Verifikasi { get; set; }
    }

    public class ParamVerifikasiMutasiJabatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
