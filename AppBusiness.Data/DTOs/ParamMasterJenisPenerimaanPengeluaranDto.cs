using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisPenerimaanPengeluaranDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenis { get; set; }
        public string KodeJenis { get; set; }
        public string NamaJenis { get; set; }
        public string TipeJenis { get; set; } = "-";
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

    }
    public class ParamMasterJenisPenerimaanPengeluaranFilterDto : ParamMasterJenisPenerimaanPengeluaranDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
