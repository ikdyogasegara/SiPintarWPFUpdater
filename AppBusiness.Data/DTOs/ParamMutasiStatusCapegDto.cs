using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMutasiStatusCapegDto
    {
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public string NoSk { get; set; }
        public DateTime? TglSk { get; set; }
        public DateTime? TglBerlaku { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamMutasiStatusCapegDetailDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMutasiStatusCapegDetailDto
    {
        public int? IdPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public int? IdPendidikan { get; set; }
        public string Tugas { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public int? Mkg_Thn { get; set; } = 0;
        public int? Mkg_Bln { get; set; } = 0;
        public string NoPegawai { get; set; }
    }

    public class ParamMutasiStatusCapegFilterDto : ParamMutasiStatusCapegDto
    {
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDepartemen { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public int? IdPendidikan { get; set; }
        public string Tugas { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public int? IdAgama { get; set; }
        public int? IdJenisKelamin { get; set; }
        public bool? FlagKawin { get; set; }
        public int? Mkg_Thn { get; set; }
        public int? Mkg_Bln { get; set; }
        public bool? Verifikasi { get; set; }
    }

    public class ParamVerifikasiMutasiStatusCapegDto
    {
        public int? IdPdam { get; set; }
        public int? IdMutasi { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
