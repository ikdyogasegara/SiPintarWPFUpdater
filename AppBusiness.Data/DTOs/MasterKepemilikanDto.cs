using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKepemilikanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKepemilikan { get; set; }
        public string KodeKepemilikan { get; set; }
        public string NamaKepemilikan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
