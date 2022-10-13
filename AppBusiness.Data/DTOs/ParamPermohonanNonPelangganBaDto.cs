using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanNonPelangganBaDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public DateTime? TanggalBa { get; set; }
        public int? IdUser { get; set; }
        public string StatusPemesanan { get; set; }
        public string StatusRangkaianMeter { get; set; }
        public string StatusTutupTotal { get; set; }
        public string StatusSegel { get; set; }
        public string MerekMeter { get; set; }
        public string WarnaSegel { get; set; }
        public string Memo { get; set; }
        public string StatusPembayaran { get; set; }
        public DateTime? WaktuAntarMulai { get; set; }
        public DateTime? WaktuAntarSelesai { get; set; }
        public decimal? JumlahTangki { get; set; }
        public decimal? JumlahSisaTangki { get; set; }
        public decimal? DiantarSebelumnya { get; set; }
        public decimal? DiantarHariIni { get; set; }
        public string KeteranganLapangan { get; set; }
        public string NoSeriMeter { get; set; }
        public decimal? AngkatMeter { get; set; }
        public string NoSeriSegel { get; set; }
        public string PersilNamaPaket { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public string DistribusiNamaPaket { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;
        public List<ParamPermohonanNonPelangganRkpDetailDto> DetailRkp { get; set; }
        public List<ParamPetugasBaNonPelangganDto> Petugas { get; set; }

        public ParamPermohonanNonPelangganBaSambunganBaruDto DataSambBaru { get; set; }
    }

    public class ParamPetugasBaNonPelangganDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamPermohonanNonPelangganRkpDetailDto
    {
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Uraian { get; set; }
        public decimal? HargaSatuan { get; set; } = 0;
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Jumlah { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Keuntungan { get; set; } = 0;
        public decimal? JasaDariBahan { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public string Kategori { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public decimal? QtyRkp { get; set; } = 0;
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagDistribusi { get; set; } = false;
    }

    public class ParamPermohonanNonPelangganBaSambunganBaruDto
    {
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdBlok { get; set; }
        public int? IdPeruntukan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public int? IdKepemilikan { get; set; }
        public int? IdPekerjaan { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdSumberAir { get; set; }
        public int? IdDma { get; set; }
        public int? IdDmz { get; set; }
        public int? IdMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string NoSeriMeter { get; set; }
        public DateTime? TglMeter { get; set; }
        public int? UrutanBaca { get; set; }
        public decimal? StanAwalPasang { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string KodePost { get; set; }
        public decimal? DayaListrik { get; set; }
        public decimal? LuasTanah { get; set; }
        public decimal? LuasRumah { get; set; }
        public string Keterangan { get; set; }
        public string Nosamb { get; set; }
        public string NoRekening { get; set; }
        public string NosambDepan { get; set; }
        public string NosambBelakang { get; set; }
        public string NosambKiri { get; set; }
        public string NosambKanan { get; set; }
        public int? Penghuni { get; set; }
    }
}
