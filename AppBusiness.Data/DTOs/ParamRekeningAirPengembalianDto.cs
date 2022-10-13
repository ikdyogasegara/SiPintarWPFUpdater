using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAirPengembalianDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPengembalian { get; set; }
        public string NomorBa { get; set; }
        public DateTime? TanggalBa { get; set; }
        public string Alasan { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public decimal? StanLalu_Lama { get; set; } = 0;
        public decimal? StanSkrg_Lama { get; set; } = 0;
        public decimal? StanAngkat_Lama { get; set; } = 0;
        public decimal? Pakai_Lama { get; set; } = 0;
        public decimal? BiayaPemakaian_Lama { get; set; } = 0;
        public decimal? Administrasi_Lama { get; set; } = 0;
        public decimal? Pemeliharaan_Lama { get; set; } = 0;
        public decimal? Retribusi_Lama { get; set; } = 0;
        public decimal? Pelayanan_Lama { get; set; } = 0;
        public decimal? AirLimbah_Lama { get; set; } = 0;
        public decimal? DendaPakai0_Lama { get; set; } = 0;
        public decimal? AdministrasiLain_Lama { get; set; } = 0;
        public decimal? PemeliharaanLain_Lama { get; set; } = 0;
        public decimal? RetribusiLain_Lama { get; set; } = 0;
        public decimal? Meterai_Lama { get; set; } = 0;
        public decimal? Rekair_Lama { get; set; } = 0;
        public decimal? Denda_Lama { get; set; } = 0;
        public decimal? Ppn_Lama { get; set; } = 0;
        public decimal? Total_Lama { get; set; } = 0;

        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanSkrg { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public decimal? Pakai { get; set; } = 0;
        public decimal? BiayaPemakaian { get; set; } = 0;
        public decimal? Administrasi { get; set; } = 0;
        public decimal? Pemeliharaan { get; set; } = 0;
        public decimal? Retribusi { get; set; } = 0;
        public decimal? Pelayanan { get; set; } = 0;
        public decimal? AirLimbah { get; set; } = 0;
        public decimal? DendaPakai0 { get; set; } = 0;
        public decimal? AdministrasiLain { get; set; } = 0;
        public decimal? PemeliharaanLain { get; set; } = 0;
        public decimal? RetribusiLain { get; set; } = 0;
        public decimal? Meterai { get; set; } = 0;
        public decimal? Rekair { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Total { get; set; } = 0;

        public decimal? Blok1 { get; set; } = 0;
        public decimal? Blok2 { get; set; } = 0;
        public decimal? Blok3 { get; set; } = 0;
        public decimal? Blok4 { get; set; } = 0;
        public decimal? Blok5 { get; set; } = 0;

        public decimal? Prog1 { get; set; } = 0;
        public decimal? Prog2 { get; set; } = 0;
        public decimal? Prog3 { get; set; } = 0;
        public decimal? Prog4 { get; set; } = 0;
        public decimal? Prog5 { get; set; } = 0;
        public int? IdUser { get; set; } = 0;
        public string Keterangan { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdPegawai { get; set; }
        public bool? FlagHapus { get; set; } = false;
    }

    public class ParamRekeningAirPengembalianFilterDto : ParamRekeningAirPengembalianDto
    {
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public int? IdArea { get; set; }
        public int? IdWilayah { get; set; }
        public int? IdKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public DateTime? TanggalBaMulai { get; set; }
        public DateTime? TanggalBaSampaiDengan { get; set; }
    }
}
