using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterBankDto
    {
        public int? IdPdam { get; set; }
        public int? IdBank { get; set; }
        public string? NamaBank { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string? KodePerkiraan3 { get; set; }
        public string? NamaPerkiraan3 { get; set; }
        public string? NoRekening { get; set; }
        public string? Keterangan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public static class MasterBankDtoExtension
    {
        public static SaldoAwalRekonDto ToSaldoAwalRekonDto(this MasterBankDto masterBank)
        {
            if (masterBank == null) return null;

            return new SaldoAwalRekonDto
            {
                IdBank = masterBank.IdBank,
                IdSaldoKasBank = null,
                NamaBank = masterBank.NamaBank,
                TanggalSaldo = null,
                JumlahSaldo = 0,
                JumlahSaldoOri = 0
            };
        }
    }
}
