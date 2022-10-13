using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPegawaiMkgDto
    {
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public int? Mkg_Thn { get; set; }
        public int? Mkg_Bln { get; set; }
        public int? MkgBerkala_Thn { get; set; }
        public int? MkgBerkala_Bln { get; set; }
        public int? MkgPangkat_Thn { get; set; }
        public int? MkgPangkat_Bln { get; set; }
        public int? Real_Thn { get; set; }
        public int? Real_Bln { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
