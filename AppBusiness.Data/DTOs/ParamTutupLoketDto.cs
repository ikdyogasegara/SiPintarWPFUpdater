using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamTutupLoketDto
    {
        public int? IdPdam { get; set; }
        public int? IdTutupLoket { get; set; }
        public DateTime? TglPenerimaan { get; set; }
        public int? IdLoket { get; set; }
        public int? IdUser { get; set; }
        public decimal? Penerimaan { get; set; } = 0;
        public decimal? UangKecil { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamTutupLoketFilterDto : ParamTutupLoketDto
    {
        public DateTime? TglPenerimaanMulai { get; set; }
        public DateTime? TglPenerimaanSampaiDengan { get; set; }
    }
}
