using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterBagianMemintaBarangDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public string NamaBagianMemintaBarang { get; set; }
        public int? IdDivisi { get; set; }
        public string NamaTtd { get; set; }
        public string JabatanTtd { get; set; }
        public string NikTtd { get; set; }
        public string NamaTtd2 { get; set; }
        public string JabatanTtd2 { get; set; }
        public string NikTtd2 { get; set; }
        public int? IdWilayah { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ParamMasterBagianMemintaBarangFilterDto : ParamMasterBagianMemintaBarangDto
    {
    }
}
