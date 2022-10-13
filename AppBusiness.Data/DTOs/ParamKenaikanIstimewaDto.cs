using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamKenaikanIstimewaDto
    {
        public int? IdPdam { get; set; }
        public int? IdKenaikanIstimewa { get; set; }
        public int? IdPegawai { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public int? IdJabatan { get; set; }
        public int? IdDivisi { get; set; }
        public int? IdAreaKerja { get; set; }
        public string Tugas { get; set; }
        public int? IdPendidikan { get; set; }
        public int? IdGolonganPegawaiBaru { get; set; }
        public string NoSk { get; set; }
        public DateTime? TanggalSk { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamKenaikanIstimewaFilterDto : ParamKenaikanIstimewaDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
