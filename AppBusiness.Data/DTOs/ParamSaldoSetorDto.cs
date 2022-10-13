using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamSaldoSetorDto
    {
        public int? IdPdam { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TglSetor { get; set; }

        public decimal? JumlahPenerimaan { get; set; }
        public int? IdLoket { get; set; }
        public int? IdSetor { get; set; }
        public List<ParamListDetailDto>? Detail { get; set; }
        public decimal? Sisa { get; set; }
        public int? FlagSetor { get; set; }

        public int? IdUserRequest { get; set; }
    }

    public class ParamListDetailDto
    {
        public int? IdBank { get; set; }
        public string? NamaBank { get; set; }
        public string? Ref { get; set; }
        public decimal? JumlahSetor { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public int? IdWilayah { get; set; } = 1;//perlu dikusi lagi
    }

    public static class WpfMapper
    {
        public static ParamSaldoSetorDto ToSaldoSetorWpf(this SaldoSetorDto saldoSetor, List<MasterBankDto>? bank)
        {
            saldoSetor ??= new();
            saldoSetor.DetailSetoranDto ??= new();
            bank ??= new();

            var listBank = bank.Select(x => new ParamListDetailDto
            {
                IdBank = x.IdBank,
                NamaBank = x.NamaBank,
                Ref = string.Empty,
                JumlahSetor = decimal.Zero,
                IdPerkiraan3 = x.IdPerkiraan3
            });

            var result = new ParamSaldoSetorDto
            {
                IdPdam = saldoSetor.IdPdam,
                TglSetor = saldoSetor.TglSetor,
                IdSetor = saldoSetor.IdSetor,
                IdLoket = saldoSetor.IdLoket,
                JumlahPenerimaan = decimal.Zero,
                Detail = saldoSetor.DetailSetoranDto.Select(x => x.ToSaldoSetorDetailWpf(bank)).ToList(),
                Sisa = saldoSetor.Sisa,
                FlagSetor = saldoSetor.FlagSetor ?? 0
            };

            var existBankIds = saldoSetor.DetailSetoranDto.Select(x => x.IdBank);

            result.Detail.AddRange(listBank.Where(x => !existBankIds.Contains(x.IdBank)));

            return result;
        }

        public static ParamListDetailDto ToSaldoSetorDetailWpf(this SaldoSetorDetailDto saldoSetorDetail, List<MasterBankDto>? bank)
        {
            var bankQeury = bank!.Where(x => x.IdBank == saldoSetorDetail.IdBank);
            var result = new ParamListDetailDto
            {
                IdBank = saldoSetorDetail.IdBank,
                NamaBank = bankQeury.Select(x => x.NamaBank).FirstOrDefault(),
                Ref = saldoSetorDetail.Ref,
                JumlahSetor = saldoSetorDetail.Jumlah ?? 0,
                IdPerkiraan3 = bankQeury.Select(x => x.IdPerkiraan3).FirstOrDefault(),
            };

            return result;
        }
    }
}
