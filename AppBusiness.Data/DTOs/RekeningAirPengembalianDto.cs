using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirPengembalianDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengembalian { get; set; }
        public string NomorBa { get; set; }
        public DateTime? TanggalBa { get; set; }
        public string Alasan { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }

        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }

        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }

        public string KodeArea { get; set; }
        public string NamaArea { get; set; }

        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }

        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }

        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }

        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }

        public decimal? StanLalu_Lama { get; set; }
        public decimal? StanSkrg_Lama { get; set; }
        public decimal? StanAngkat_Lama { get; set; }
        public decimal? Pakai_Lama { get; set; }
        public decimal? BiayaPemakaian_Lama { get; set; }
        public decimal? Administrasi_Lama { get; set; }
        public decimal? Pemeliharaan_Lama { get; set; }
        public decimal? Retribusi_Lama { get; set; }
        public decimal? Pelayanan_Lama { get; set; }
        public decimal? AirLimbah_Lama { get; set; }
        public decimal? DendaPakai0_Lama { get; set; }
        public decimal? AdministrasiLain_Lama { get; set; }
        public decimal? PemeliharaanLain_Lama { get; set; }
        public decimal? RetribusiLain_Lama { get; set; }
        public decimal? Meterai_Lama { get; set; }
        public decimal? Rekair_Lama { get; set; }
        public decimal? Denda_Lama { get; set; }
        public decimal? Ppn_Lama { get; set; }
        public decimal? Total_Lama { get; set; }

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
        public decimal? Meterai { get; set; }
        public decimal? Rekair { get; set; }
        public decimal? Denda { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Total { get; set; }

        public decimal? Blok1 { get; set; }
        public decimal? Blok2 { get; set; }
        public decimal? Blok3 { get; set; }
        public decimal? Blok4 { get; set; }
        public decimal? Blok5 { get; set; }

        public decimal? Prog1 { get; set; }
        public decimal? Prog2 { get; set; }
        public decimal? Prog3 { get; set; }
        public decimal? Prog4 { get; set; }
        public decimal? Prog5 { get; set; }


        public int? IdUser { get; set; }
        public string NamaUser { get; set; }

        public string Keterangan { get; set; }
        public int? IdKondisiMeter { get; set; }
        public string NamaKondisiMeter { get; set; }
        public int? IdPegawai { get; set; }
        public string NamaPegawai { get; set; }


        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
