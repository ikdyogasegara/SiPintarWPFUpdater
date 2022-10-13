using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirKoreksiRekeningCetakDto
    {
        public int Id { get; set; }
        public int IdPdam { get; set; }
        public string StatusVerifikasiText { get; set; }
        public DateTime WaktuUsulan { get; set; }
        public int IdPermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public int IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public DateTime WaktuPermohonan { get; set; }
        public int IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public int IdPeriode { get; set; }
        public int KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public int StatusVerifikasiLapangan { get; set; }
        public DateTime WaktuStatusVerifikasiLapangan { get; set; }
        public string KeteranganStatusVerifikasiLapangan { get; set; }
        public int StatusVerifikasiPusat { get; set; }
        public DateTime WaktuStatusVerifikasiPusat { get; set; }
        public string KeteranganStatusVerifikasiPusat { get; set; }
        public bool FlagLunas { get; set; }
        public DateTime WaktuPelunasan { get; set; }
        public decimal StanLalu { get; set; } = 0;
        public decimal StanSkrg { get; set; } = 0;
        public decimal StanAngkat { get; set; } = 0;
        public decimal Pakai { get; set; } = 0;
        public decimal BiayaPemakaian { get; set; } = 0;
        public decimal Administrasi { get; set; } = 0;
        public decimal Pemeliharaan { get; set; } = 0;
        public decimal Retribusi { get; set; } = 0;
        public decimal Pelayanan { get; set; } = 0;
        public decimal AirLimbah { get; set; } = 0;
        public decimal DendaPakai0 { get; set; } = 0;
        public decimal AdministrasiLain { get; set; } = 0;
        public decimal PemeliharaanLain { get; set; } = 0;
        public decimal RetribusiLain { get; set; } = 0;
        public decimal Ppn { get; set; } = 0;
        public decimal Meterai { get; set; } = 0;
        public decimal RekAir { get; set; } = 0;
        public decimal Denda { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        public bool FlagHanyaAbonemen { get; set; }
        public decimal StanLalu_Usulan { get; set; } = 0;
        public decimal StanSkrg_Usulan { get; set; } = 0;
        public decimal StanAngkat_Usulan { get; set; } = 0;
        public decimal Pakai_Usulan { get; set; } = 0;
        public decimal BiayaPemakaian_Usulan { get; set; } = 0;
        public decimal Administrasi_Usulan { get; set; } = 0;
        public decimal Pemeliharaan_Usulan { get; set; } = 0;
        public decimal Retribusi_Usulan { get; set; } = 0;
        public decimal Pelayanan_Usulan { get; set; } = 0;
        public decimal AirLimbah_Usulan { get; set; } = 0;
        public decimal DendaPakai0_Usulan { get; set; } = 0;
        public decimal AdministrasiLain_Usulan { get; set; } = 0;
        public decimal PemeliharaanLain_Usulan { get; set; } = 0;
        public decimal RetribusiLain_Usulan { get; set; } = 0;
        public decimal Ppn_Usulan { get; set; } = 0;
        public decimal Meterai_Usulan { get; set; } = 0;
        public decimal RekAir_Usulan { get; set; } = 0;
        public decimal Denda_Usulan { get; set; } = 0;
        public decimal Total_Usulan { get; set; } = 0;
        public decimal StanLalu_Baru { get; set; } = 0;
        public decimal StanSkrg_Baru { get; set; } = 0;
        public decimal StanAngkat_Baru { get; set; } = 0;
        public decimal Pakai_Baru { get; set; } = 0;
        public decimal BiayaPemakaian_Baru { get; set; } = 0;
        public decimal Administrasi_Baru { get; set; } = 0;
        public decimal Pemeliharaan_Baru { get; set; } = 0;
        public decimal Retribusi_Baru { get; set; } = 0;
        public decimal Pelayanan_Baru { get; set; } = 0;
        public decimal AirLimbah_Baru { get; set; } = 0;
        public decimal DendaPakai0_Baru { get; set; } = 0;
        public decimal AdministrasiLain_Baru { get; set; } = 0;
        public decimal PemeliharaanLain_Baru { get; set; } = 0;
        public decimal RetribusiLain_Baru { get; set; } = 0;
        public decimal Ppn_Baru { get; set; } = 0;
        public decimal Meterai_Baru { get; set; } = 0;
        public decimal RekAir_Baru { get; set; } = 0;
        public decimal Denda_Baru { get; set; } = 0;
        public decimal Total_Baru { get; set; } = 0;
    }
}
