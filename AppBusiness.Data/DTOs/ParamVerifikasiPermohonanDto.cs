namespace AppBusiness.Data.DTOs
{
    public class ParamVerifikasiPermohonanFilterDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string Sumber { get; set; }
        public int? JumlahPelangganAir { get; set; }
        public int? JumlahPelangganLimbah { get; set; }
        public int? JumlahPelangganLltt { get; set; }
        public int? JumlahNonPelanggan { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamVerifikasiPermohonanDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipePermohonan { get; set; }
        public int? IdPermohonan { get; set; }
        public string JenisPelanggan { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
