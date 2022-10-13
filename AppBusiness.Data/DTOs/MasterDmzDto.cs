using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDmzDto
    {
        public int? IdPdam { get; set; }
        public int? IdDmz { get; set; }
        public string KodeDmz { get; set; }
        public string NamaDmz { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
