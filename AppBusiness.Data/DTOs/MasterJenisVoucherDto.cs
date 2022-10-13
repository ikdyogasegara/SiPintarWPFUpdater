using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisVoucherDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisVoucher { get; set; }
        public string KodeJenisVoucher { get; set; }
        public string NamaJenisVoucher { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
