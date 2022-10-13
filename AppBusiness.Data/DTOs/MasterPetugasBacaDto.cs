using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPetugasBacaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPetugasBaca { get; set; }
        public string KodePetugasBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string NamaUser { get; set; }
        public string JenisPembaca { get; set; }
        public string Alamat { get; set; }
        public DateTime? TglLahir { get; set; }
        public string NoHp { get; set; }
        public DateTime? TglMulaiKerja { get; set; }
        public string FotoPetugasBaca { get; set; }
        public string Keterangan { get; set; }
        public bool? Status { get; set; }
        public DateTime? WaktuCreate { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
