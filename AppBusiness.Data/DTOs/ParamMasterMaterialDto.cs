using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterMaterialDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdMaterial { get; set; }
        public string KodeMaterial { get; set; }
        public string NamaMaterial { get; set; }
        public bool? MaterialLimbah { get; set; } = false;
        public string Satuan { get; set; }
        public decimal? HargaBeli { get; set; } = 0;
        public decimal? HargaJual { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ParamMasterMaterialFilterDto : ParamMasterMaterialDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
