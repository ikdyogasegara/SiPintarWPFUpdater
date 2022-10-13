using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKelurahanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKecamatan { get; set; }
        public int? JumlahJiwa { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKelurahanFilterDto : ParamMasterKelurahanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
