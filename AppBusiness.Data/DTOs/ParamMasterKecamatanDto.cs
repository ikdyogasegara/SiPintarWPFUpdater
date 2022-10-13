using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKecamatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKecamatanFilterDto : ParamMasterKecamatanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
