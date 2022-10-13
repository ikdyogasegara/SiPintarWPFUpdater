using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterBagianMemintaBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public string NamaBagianMemintaBarang { get; set; }
        public int? IdDivisi { get; set; }
        public string NamaDivisi { get; set; }
        public string NamaTtd { get; set; }
        public string JabatanTtd { get; set; }
        public string NikTtd { get; set; }
        public string NamaTtd2 { get; set; }
        public string JabatanTtd2 { get; set; }
        public string NikTtd2 { get; set; }
        public int? IdWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
