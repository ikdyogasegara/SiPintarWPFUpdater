namespace AppBusiness.Data.DTOs
{
    public class ParamTagihanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamTagihanFilterDto : ParamTagihanDto
    {
    }
}
