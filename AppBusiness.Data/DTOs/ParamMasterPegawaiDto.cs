﻿using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string NoPenduduk { get; set; }
        public string TempatLahir { get; set; }
        public DateTime? TglLahir { get; set; }
        public int? IdJenisKelamin { get; set; }
        public int? IdGolonganDarah { get; set; }
        public int? IdAgama { get; set; }
        public string Alamat { get; set; }
        public string AlamatKtp { get; set; }
        public bool? FlagKawin { get; set; } = false;
        public DateTime? TglKawin { get; set; }

        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public int? IdPendidikan { get; set; }
        public string Jurusan { get; set; }
        public int? TahunLulus { get; set; }
        public int? IdPendidikanSebelumnya { get; set; }
        public string JurusanSebelumnya { get; set; }
        public int? TahunLulusSebelumnya { get; set; }
        public int? IdStatusPegawai { get; set; }
        public DateTime? TglMasuk { get; set; }
        public DateTime? Tgl80 { get; set; }
        public DateTime? Tgl100 { get; set; }
        public DateTime? TglHabisKontrak { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public string Tugas { get; set; }
        public DateTime? TmtJabatan { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public DateTime? TmtGolongan { get; set; }
        public DateTime? TglBerkala { get; set; }
        public DateTime? TglPangkat { get; set; }


        public DateTime? TglBerkalaSebelumnya { get; set; }
        public string SkBerkalaSebelumnya { get; set; }
        public DateTime? TglPangkatSebelumnya { get; set; }
        public string SkPangkatSebelumnya { get; set; }


        public string NoBpjsKes { get; set; }
        public string NoBpjsTk { get; set; }
        public string Npwp { get; set; }
        public string Bank { get; set; }
        public string NoRek { get; set; }
        public int? Pin { get; set; }
        public string Foto { get; set; }
        public string FotoKtp { get; set; }
        public string FotoKk { get; set; }
        public string Keterangan1 { get; set; }
        public string Keterangan2 { get; set; }
        public string Keterangan3 { get; set; }
        public string Keterangan4 { get; set; }
        public string Keterangan5 { get; set; }
        public int? NoUrut { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPegawaiFilterDto : ParamMasterPegawaiDto
    {
        public int? IdDepartemen { get; set; }
        public bool? TermasukPegawaiNonAktif { get; set; }
        public int? IdTipeKeluarga { get; set; }
        public bool? FlagPensiun { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class ParamMasterPostingStatusKeluargaPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPostingJadwalPensiunPegawaiDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamVerifikasiPensiunDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public int? IdPegawai { get; set; }
        public int? IdJenisPensiun { get; set; }
        public string NoSkPensiun { get; set; }
        public DateTime? TglSkPensiun { get; set; }
    }
}
