namespace AppBusiness.Data.DTOs
{
    public class ParamMasterOngkosDto
    {
        public int? IdPdam { get; set; }
        public int? IdOngkos { get; set; }
        public string KodeOngkos { get; set; }
        public string NamaOngkos { get; set; }
        public bool? OngkosLimbah { get; set; } = false;
        public string Satuan { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public string Perhitungan { get; set; }
        public int? IdPaketMaterial { get; set; }
        public decimal? Persentase { get; set; } = 0;
        public decimal? Biaya { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterOngkosFilterDto : ParamMasterOngkosDto
    {
    }
}
