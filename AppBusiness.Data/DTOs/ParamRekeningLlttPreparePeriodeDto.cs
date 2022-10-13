namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningLlttPreparePeriodeDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdTarifLltt { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdStatus { get; set; }
        public int? IdFlag { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public bool? FlagPublish { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public bool? FlagUpload { get; set; } = false;
        public int? HapusSecaraAkuntansi { get; set; } = 0;
    }
}
