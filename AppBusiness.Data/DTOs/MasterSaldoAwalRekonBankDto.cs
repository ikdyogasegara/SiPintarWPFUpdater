using System;
using System.Collections.Generic;
using System.Linq;

namespace AppBusiness.Data.DTOs
{
    public class MasterSaldoAwalRekonBankDto
    {
        public decimal? TotalSaldoKasBank { get; set; }
        public List<SaldoAwalRekonDto> SaldoAwalRekon { get; set; }
        //public List<SaldoAwalPerkiraanBankDto> SaldoAwalPerkiraanBank { get; set; }
    }
    public class SaldoAwalRekonDto
    {
        public int? IdBank { get; set; }
        public int? IdSaldoKasBank { get; set; }
        public string NamaBank { get; set; }
        public DateTime? TanggalSaldo { get; set; }
        public decimal? JumlahSaldo { get; set; }
        public decimal? JumlahSaldoOri { get; set; }
    }

    public static class MasterSaldoAwalRekonBankDtoExtension
    {
        public static ParamSaldoAwalRekonBankDto ToParamSaldoAwalRekonBankDto(this MasterSaldoAwalRekonBankDto masterSaldoAwalRekonBank)
        {
            if (masterSaldoAwalRekonBank == null) return null;

            return new ParamSaldoAwalRekonBankDto
            {
                IdPdam = null,
                IdUserRequest = null,
                TanggalSaldo = masterSaldoAwalRekonBank.SaldoAwalRekon.First().TanggalSaldo,
                ProsesSaldoBank = masterSaldoAwalRekonBank.SaldoAwalRekon.Select(x => new ProsesSaldoBank
                {
                    IdBank = x.IdBank,
                    IdSaldoKasBank = x.IdSaldoKasBank,
                    JumlahSaldo = x.JumlahSaldo,
                    JumlahSaldoOri = x.JumlahSaldoOri
                }).ToList()
            };
        }
    }
}
