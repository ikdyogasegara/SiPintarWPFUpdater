using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPegawaiGajiTambahanDto
    {
        public int? IdPdam { get; set; }
        public int? IdGajiTambahan { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string Jabatan { get; set; }
        public string Departemen { get; set; }
        public string Divisi { get; set; }
        public string AreaKerja { get; set; }

        public string Keterangan { get; set; }
        public bool? FlagPersen { get; set; }
        public string FlagPersenText { get; set; }
        public decimal? Nominal { get; set; }
        public int? IdKodeGaji { get; set; }
        public string NamaKodeGaji { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
