using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTipeBarangDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdTipeBarang { get; set; }
        public string NamaTipeBarang { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ParamMasterTipeBarangFilterDto : ParamMasterTipeBarangDto
    {
    }
}
