namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPegawaiGajiDewanPengawasDto
    {
        public int? IdPdam { get; set; }
        public int? IdGajiDewanPengawas { get; set; }
        public int? IdPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public bool? FlagPersen { get; set; } = false;
        public decimal? Persentase { get; set; }
        public string Berdasarkan { get; set; }
        public int? IdPegawaiRef { get; set; }
        public string PersenDari { get; set; }
        public string List_IdJenisTunjangan { get; set; }
        public string List_IdJenisPotongan { get; set; }
        public string KeteranganJabatan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPegawaiGajiDewanPengawasFilterDto : ParamMasterPegawaiGajiDewanPengawasDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
