using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPetugasBacaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPetugasBaca { get; set; }
        public string KodePetugasBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string NamaUser { get; set; }
        public string Password { get; set; }
        public string JenisPembaca { get; set; }
        public string Alamat { get; set; }
        public DateTime? TglLahir { get; set; }
        public DateTime? TglMulaiKerja { get; set; }
        public string NoHp { get; set; }
        public string FotoPetugasBaca { get; set; }
        public string Keterangan { get; set; }
        public DateTime? WaktuCreate { get; set; }
        public bool? Status { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPetugasBacaFilterDto : ParamMasterPetugasBacaDto
    {
        public DateTime? TglLahirMulai { get; set; }
        public DateTime? TglLahirSampaiDengan { get; set; }
        public DateTime? TglMulaiKerjaMulai { get; set; }
        public DateTime? TglMulaiKerjaSampaiDengan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
