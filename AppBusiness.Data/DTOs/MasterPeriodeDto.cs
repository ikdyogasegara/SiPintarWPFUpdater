using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeriodeDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? Tahun { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public bool? Status { get; set; } = false;
        public int? PelangganAir { get; set; } = 0;
        public int? SudahBaca { get; set; } = 0;
        public int? BelumBaca { get; set; } = 0;
        public decimal? JumlahPakaiAir { get; set; } = 0;
        public int? JumlahKelainan { get; set; } = 0;
        public int? JumlahTaksir { get; set; } = 0;
        public int? JumlahFotoRumah { get; set; } = 0;
        public int? PelangganAirPublish { get; set; } = 0;
        public int? PelangganLimbah { get; set; } = 0;
        public int? PelangganLimbahPublish { get; set; } = 0;
        public int? PelangganLLTT { get; set; } = 0;
        public int? PelangganLLTTPublish { get; set; } = 0;
        public decimal? JumlahRekeningAir { get; set; } = 0;
        public decimal? JumlahRekeningAirLunas { get; set; } = 0;
        public decimal? JumlahRekeningAirPiutang { get; set; } = 0;
        public decimal? JumlahRekeningLimbah { get; set; } = 0;
        public decimal? JumlahRekeningLimbahLunas { get; set; } = 0;
        public decimal? JumlahRekeningLimbahPiutang { get; set; } = 0;
        public decimal? JumlahRekeningLLTT { get; set; } = 0;
        public decimal? JumlahRekeningLLTTLunas { get; set; } = 0;
        public decimal? JumlahRekeningLLTTPiutang { get; set; } = 0;
        public DateTime? TglMulaiTagih { get; set; }
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
