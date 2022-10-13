using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirKoreksiRekening
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public DateTime? WaktuUsulan { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? StatusVerifikasiLapangan { get; set; }
        public DateTime? WaktuStatusVerifikasiLapangan { get; set; }
        public string KeteranganStatusVerifikasiLapangan { get; set; }
        public int? StatusVerifikasiPusat { get; set; }
        public DateTime? WaktuStatusVerifikasiPusat { get; set; }
        public string KeteranganStatusVerifikasiPusat { get; set; }
        public string NomorBa { get; set; }
        public decimal? StanLalu { get; set; }
        public decimal? StanSkrg { get; set; }
        public decimal? StanAngkat { get; set; }
        public decimal? Pakai { get; set; }
        public decimal? BiayaPemakaian { get; set; }
        public decimal? Administrasi { get; set; }
        public decimal? Pemeliharaan { get; set; }
        public decimal? Retribusi { get; set; }
        public decimal? Pelayanan { get; set; }
        public decimal? AirLimbah { get; set; }
        public decimal? DendaPakai0 { get; set; }
        public decimal? AdministrasiLain { get; set; }
        public decimal? PemeliharaanLain { get; set; }
        public decimal? RetribusiLain { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Meterai { get; set; }
        public decimal? RekAir { get; set; }
        public decimal? Denda { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public bool? FlagHanyaAbonemen { get; set; } = false;
        public decimal? StanLalu_Usulan { get; set; } = 0;
        public decimal? StanSkrg_Usulan { get; set; } = 0;
        public decimal? StanAngkat_Usulan { get; set; } = 0;
        public decimal? Pakai_Usulan { get; set; } = 0;
        public decimal? BiayaPemakaian_Usulan { get; set; } = 0;
        public decimal? Administrasi_Usulan { get; set; } = 0;
        public decimal? Pemeliharaan_Usulan { get; set; } = 0;
        public decimal? Retribusi_Usulan { get; set; } = 0;
        public decimal? Pelayanan_Usulan { get; set; } = 0;
        public decimal? AirLimbah_Usulan { get; set; } = 0;
        public decimal? DendaPakai0_Usulan { get; set; } = 0;
        public decimal? AdministrasiLain_Usulan { get; set; } = 0;
        public decimal? PemeliharaanLain_Usulan { get; set; } = 0;
        public decimal? RetribusiLain_Usulan { get; set; } = 0;
        public decimal? Ppn_Usulan { get; set; } = 0;
        public decimal? Meterai_Usulan { get; set; } = 0;
        public decimal? RekAir_Usulan { get; set; } = 0;
        public decimal? Denda_Usulan { get; set; } = 0;
        public decimal? Total_Usulan { get; set; } = 0;
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public decimal? StanLalu_Baru { get; set; }
        public decimal? StanSkrg_Baru { get; set; }
        public decimal? StanAngkat_Baru { get; set; }
        public decimal? Pakai_Baru { get; set; }
        public decimal? BiayaPemakaian_Baru { get; set; }
        public decimal? Administrasi_Baru { get; set; }
        public decimal? Pemeliharaan_Baru { get; set; }
        public decimal? Retribusi_Baru { get; set; }
        public decimal? Pelayanan_Baru { get; set; }
        public decimal? AirLimbah_Baru { get; set; }
        public decimal? DendaPakai0_Baru { get; set; }
        public decimal? AdministrasiLain_Baru { get; set; }
        public decimal? PemeliharaanLain_Baru { get; set; }
        public decimal? RetribusiLain_Baru { get; set; }
        public decimal? Ppn_Baru { get; set; }
        public decimal? Meterai_Baru { get; set; }
        public decimal? RekAir_Baru { get; set; }
        public decimal? Denda_Baru { get; set; } = 0;
        public decimal? Total_Baru { get; set; } = 0;
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
