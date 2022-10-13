using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamSetoranLoketDto
    {
        public int? IdPdam { get; set; }
        public int? IdSetoranLoket { get; set; }
        public DateTime? TglPenerimaan { get; set; }
        public int? IdLoket { get; set; }
        public decimal? Penerimaan { get; set; } = 0;
        public decimal? Setoran { get; set; } = 0;
        public DateTime? TglSetor { get; set; }
        public int? IdBank { get; set; }
        public string Keterangan { get; set; }
        public int? IdUser { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamSetoranLoketFilterDto : ParamSetoranLoketDto
    {
        public DateTime? TglPenerimaanMulai { get; set; }
        public DateTime? TglPenerimaanSampaiDengan { get; set; }
        public DateTime? TglSetorMulai { get; set; }
        public DateTime? TglSetorSampaiDengan { get; set; }
    }
}
