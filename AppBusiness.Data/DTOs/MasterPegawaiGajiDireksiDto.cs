using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPegawaiGajiDireksiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public int? IdGajiDireksi { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public decimal? Jumlah { get; set; }
        public bool? FlagPersen { get; set; }
        public decimal? Persentase { get; set; }
        public string Berdasarkan { get; set; }
        public int? IdPegawaiRef { get; set; }
        public string NoPegawaiRef { get; set; }
        public string NamaPegawaiRef { get; set; }
        public string PersenDari { get; set; }
        public string List_IdJenisTunjangan { get; set; }
        public string List_IdJenisPotongan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
