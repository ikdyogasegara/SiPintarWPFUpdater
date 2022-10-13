using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamSaldoAwalRekonBankDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public DateTime? TanggalSaldo { get; set; }
        public List<ProsesSaldoBank> ProsesSaldoBank { get; set; }

    }
    public class ProsesSaldoBank
    {
        public int? IdSaldoKasBank { get; set; }
        public int? IdBank { get; set; }
        public decimal? JumlahSaldo { get; set; }
        public decimal? JumlahSaldoOri { get; set; }
    }
}
