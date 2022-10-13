using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string StatusPermohonanText { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public string Keterangan { get; set; }
        public string KeteranganLapangan { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }

        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? IdTarifLimbah { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
        public int? IdTarifLltt { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public int? IdBlok { get; set; }
        public string NamaBlok { get; set; }
        public int? IdUser { get; set; }
        public int? IdNonAir { get; set; }
        public string NamaUser { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }

        public string FotoBuktiSpk1 { get; set; }
        public string FotoBuktiSpk2 { get; set; }
        public string FotoBuktiSpk3 { get; set; }

        public string FotoDenah1 { get; set; }
        public string FotoDenah2 { get; set; }
        public string FotoBuktiRab1 { get; set; }
        public string FotoBuktiRab2 { get; set; }
        public string FotoBuktiRab3 { get; set; }

        public string FotoBuktiBeritaAcara1 { get; set; }
        public string FotoBuktiBeritaAcara2 { get; set; }
        public string FotoBuktiBeritaAcara3 { get; set; }

        public bool? FlagVerifikasi { get; set; }
        public DateTime? WaktuVerifikasi { get; set; }
        public List<PermohonanDetailDto> Parameter { get; set; }
        public RekeningNonAirViewDto NonAirReguler { get; set; }
        public PermohonanSpkDto SPK { get; set; }
        public PermohonanBaDto BeritaAcara { get; set; }
        public List<PermohonanPetugasDto> Pelaksana { get; set; }
        public PermohonanRabDto RAB { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? StatusPermohonan { get; set; }

        public bool? FlagPendaftaran { get; set; }
        public int? IdTipePendaftaranSambungan { get; set; }
        public string NamaTipePendaftaranSambungan { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string KodePost { get; set; }
        public decimal? DayaListrik { get; set; }
        public decimal? LuasTanah { get; set; }
        public decimal? LuasRumah { get; set; }
        public decimal? StanAwalPasang { get; set; }
        public int? IdPeruntukan { get; set; }
        public string NamaPeruntukan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public string NamaJenisBangunan { get; set; }
        public int? IdKepemilikan { get; set; }
        public string NamaKepemilikan { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NamaPekerjaan { get; set; }
        public string NosambYangDiberikan { get; set; }
        public string NosambDepan { get; set; }
        public string NosambBelakang { get; set; }
        public string NosambKiri { get; set; }
        public string NosambKanan { get; set; }
        public int? Penghuni { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdSumberAir { get; set; }
        public string NamaSumberAir { get; set; }
        public int? IdDma { get; set; }
        public int? IdDmz { get; set; }
        public int? IdMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string NoSeriMeter { get; set; }
        public int? UrutanBaca { get; set; }
        public string NamaPemilik { get; set; }
        public string AlamatPemilik { get; set; }
        public bool? FlagLanjutKeLanggananLimbah { get; set; }
        public bool? FlagUsulan { get; set; }
    }

    public class PermohonanDetailDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public dynamic Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class PermohonanSpkDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string NomorSpk { get; set; }
        public DateTime? TanggalSpk { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public bool? FlagBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public List<PermohonanRabDetailDto> SppbDariSpk { get; set; }
    }

    public class PermohonanRabDetailDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Uraian { get; set; }
        public decimal? HargaSatuan { get; set; }
        public string Satuan { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Jumlah { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Keuntungan { get; set; }
        public decimal? JasaDariBahan { get; set; }
        public decimal? Total { get; set; }
        public string Kategori { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public decimal? QtyRkp { get; set; }
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagDistribusi { get; set; } = false;
        public DateTime? Waktuupdate { get; set; }
    }

    public class PermohonanBaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string NomorBa { get; set; }
        public DateTime? TanggalBa { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }

        public string PersilNamaPaket { get; set; }
        public List<PermohonanRkpDetailDto> DetailPersil { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public string DistribusiNamaPaket { get; set; }
        public List<PermohonanRkpDetailDto> DetailDistribusi { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;

        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
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
        public bool? FlagBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class PermohonanPetugasDto
    {
        public int? IdPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? IdDivisi { get; set; }
        public string Divisi { get; set; }
    }

    public class PermohonanRabDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdJenisNonair { get; set; }
        public int? IdNonAir { get; set; }
        public string NomorRab { get; set; }
        public DateTime? TanggalRab { get; set; }
        public string NomorBppi { get; set; }
        public DateTime? TanggalBppi { get; set; }
        public string NomorSpkp { get; set; }
        public DateTime? TanggalSpkp { get; set; }
        public string NomorSppb { get; set; }
        public DateTime? TanggalSppb { get; set; }
        public string NomorBapb { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? TanggalKadaluarsa { get; set; }
        public DateTime? Waktuupdate { get; set; }

        public string PersilNamaPaket { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public decimal? PersilSubTotal { get; set; } = 0;
        public decimal? PersilDibebankankePdam { get; set; } = 0;
        public decimal? PersilTotal { get; set; } = 0;
        public List<PermohonanRabDetailDto> DetailPersil { get; set; }


        public string DistribusiNamaPaket { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;
        public decimal? DistribusiSubTotal { get; set; } = 0;
        public decimal? DistribusiDibebankankePdam { get; set; } = 0;
        public decimal? DistribusiTotal { get; set; } = 0;
        public List<PermohonanRabDetailDto> DetailDistribusi { get; set; }


        public string FotoDenah1 { get; set; }
        public string FotoDenah2 { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }


        public decimal? RekapSubTotal { get; set; } = 0;
        public decimal? RekapDibebankankePdam { get; set; } = 0;
        public decimal? RekapTotal { get; set; } = 0;

        public bool? FlagBatal { get; set; } = false;
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public RekeningNonAirViewDto NonAirRab { get; set; }
    }

    public class PermohonanRkpDetailDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string Tipe { get; set; }
        public string Kode { get; set; }
        public string Uraian { get; set; }
        public decimal? HargaSatuan { get; set; }
        public string Satuan { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Jumlah { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Keuntungan { get; set; }
        public decimal? JasaDariBahan { get; set; }
        public decimal? Total { get; set; }
        public string Kategori { get; set; }
        public string Kelompok { get; set; }
        public string PostBiaya { get; set; }
        public decimal? QtyRkp { get; set; }
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagPaket { get; set; } = false;
        public bool? FlagDistribusi { get; set; } = false;
        public DateTime? Waktuupdate { get; set; }
    }
}
