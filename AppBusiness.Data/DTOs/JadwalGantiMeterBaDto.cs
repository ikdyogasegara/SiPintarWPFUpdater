using System;

namespace AppBusiness.Data.DTOs
{
    public class JadwalGantiMeterBaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Tahun { get; set; }
        public string NomorBa { get; set; }
        public DateTime? TanggalBa { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public string Keterangan { get; set; }
        public decimal? AngkaMeterLama { get; set; }
        public decimal? AngkaMeterBaru { get; set; }
        public string NoSeriMeter { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdMerekMeter { get; set; }
        public bool? FlagBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
