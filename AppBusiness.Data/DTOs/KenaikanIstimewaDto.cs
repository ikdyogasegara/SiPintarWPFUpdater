using System;

namespace AppBusiness.Data.DTOs
{
    public class KenaikanIstimewaDto
    {
        public int? IdPdam { get; set; }
        public int? IdKenaikanIstimewa { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public string Jabatan { get; set; }
        public int? IdDivisi { get; set; }
        public string Divisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public string AreaKerja { get; set; }
        public string Tugas { get; set; }
        public int? IdPendidikan { get; set; }
        public string Pendidikan { get; set; }
        public int? IdGolonganPegawaiBaru { get; set; }
        public string KodeGolonganPegawaiBaru { get; set; }
        public string NoSk { get; set; }
        public int? PeriodeSk { get; set; }
        public DateTime? TanggalSk { get; set; }
        public DateTime? TanggalInput { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
