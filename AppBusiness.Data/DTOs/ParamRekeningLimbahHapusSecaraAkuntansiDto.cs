namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningLimbahHapusSecaraAkuntansiDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? KodePeriodeAwal { get; set; }
        public int? KodePeriodeAkhir { get; set; }
    }
}
