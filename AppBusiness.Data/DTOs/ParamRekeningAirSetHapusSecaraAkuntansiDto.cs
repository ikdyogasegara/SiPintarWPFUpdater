namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAirSetHapusSecaraAkuntansiDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? KodePeriodeAwal { get; set; }
        public int? KodePeriodeAkhir { get; set; }
        public bool? UpdateMasterPelanggan { get; set; }
    }
}
