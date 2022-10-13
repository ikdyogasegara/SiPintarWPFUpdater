using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppBusiness.Data.DTOs
{
    public class SaldoSetorDto
    {
        public int? IdPdam { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TglSetor { get; set; }
        public int? IdSetor { get; set; }
        public int? IdLoket { get; set; }
        public decimal? JumlahSetor { get; set; }
        public decimal? Sisa { get; set; }
        public int? FlagSetor { get; set; }

        public DateTime? WaktuUpdate { get; set; }

        public List<SaldoSetorDetailDto>? DetailSetoranDto { get; set; }

    }
}
