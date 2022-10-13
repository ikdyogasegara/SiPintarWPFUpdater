using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanNonPelangganDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public bool? FlagPendaftaran { get; set; }
        public int? IdTipePendaftaranSambungan { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? UrutanBaca { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public int? IdBlok { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string KodePost { get; set; }
        public decimal? DayaListrik { get; set; } = 0;
        public decimal? LuasTanah { get; set; } = 0;
        public decimal? LuasRumah { get; set; } = 0;
        public int? IdPeruntukan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public int? IdKepemilikan { get; set; }
        public int? IdPekerjaan { get; set; }
        public int? IdSumberAir { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdDma { get; set; }
        public int? IdDmz { get; set; }
        public int? IdMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string Keterangan { get; set; }
        public string NosambYangDiberikan { get; set; }
        public string NosambDepan { get; set; }
        public string NosambBelakang { get; set; }
        public string NosambKiri { get; set; }
        public string NosambKanan { get; set; }
        public int? Penghuni { get; set; } = 0;
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string NamaPemilik { get; set; }
        public string AlamatPemilik { get; set; }
        public string NoSeriMeter { get; set; }
        public decimal? StanAwalPasang { get; set; }
        public int? IdUser { get; set; }
        public int? IdNonAir { get; set; }
        public string FotoKtp { get; set; }
        public string FotoKk { get; set; }
        public string FotoSuratPernyataan { get; set; }
        public string FotoImb { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public bool? FlagLanjutKeLanggananLimbah { get; set; } = false;
        public bool? FlagUsulan { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public List<ParamPermohonanNonPelangganDetailDto> DetailParameter { get; set; }
        public int? IdJenisNonAir { get; set; }
        public List<ParamPermohonanNonPelangganBiayaDto> DetailBiaya { get; set; }
        public bool? FlagInsertMasterPelanggan { get; set; } = false;
    }

    public class ParamPermohonanNonPelangganDetailDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public dynamic Value { get; set; }

    }

    public class ParamPermohonanNonPelangganBiayaDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Value { get; set; } = 0;
    }


    public class ParamPermohonanNonPelangganFilterDto
    {
        public string Fitur { get; set; }
        public bool? IncludeRabDetail { get; set; } = false;
        public bool? IncludeBaDetail { get; set; } = false;
        public string StatusPermohonanText { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public bool? FlagPendaftaran { get; set; }
        public int? IdTipePendaftaranSambungan { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
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
        public string Rt { get; set; }
        public string Rw { get; set; }
        public int? IdBlok { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string KodePost { get; set; }
        public decimal? DayaListrik { get; set; }
        public decimal? LuasTanah { get; set; }
        public decimal? LuasRumah { get; set; }
        public int? IdPeruntukan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public int? IdKepemilikan { get; set; }
        public int? IdPekerjaan { get; set; }
        public string Keterangan { get; set; }
        public string NosambYangDiberikan { get; set; }
        public string NosambDepan { get; set; }
        public string NosambBelakang { get; set; }
        public string NosambKiri { get; set; }
        public string NosambKanan { get; set; }
        public int? Penghuni { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public string FotoKtp { get; set; }
        public string FotoKk { get; set; }
        public string FotoSuratPernyataan { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public List<PermohonanNonPelangganDetailDto> DetailParameter { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public List<RekeningNonAirDetailDto> DetailBiaya { get; set; }
        public RekeningNonAirViewDto NonAir { get; set; }
        public PermohonanNonPelangganSpkDto SPK { get; set; }
        public PermohonanNonPelangganBaDto BeritaAcara { get; set; }
        public List<PermohonanNonPelangganPetugasDto> Petugas { get; set; }
        public List<PermohonanNonPelangganRabDto> RAB { get; set; }
        public virtual bool? AdaSPK { get; set; }
        public virtual bool? AdaBeritaAcara { get; set; }
        public virtual DateTime? TanggalMulaiPermohonan { get; set; }
        public virtual DateTime? TanggalSampaiDenganPermohonan { get; set; }
        public virtual DateTime? TanggalMulaiSPK { get; set; }
        public virtual DateTime? TanggalSampaiDenganSPK { get; set; }
        public virtual DateTime? TanggalMulaiBa { get; set; }
        public virtual DateTime? TanggalSampaiDenganBa { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }


        public bool? StatusTransaksiNonAirReguler { get; set; }
        public bool? StatusTransaksiRab { get; set; }
        public virtual bool? AdaRab { get; set; }
        public virtual bool? SudahBppi { get; set; }
        public virtual bool? SudahSpkp { get; set; }
        public virtual bool? SudahSppb { get; set; }
        public string NomorSpk { get; set; }
        public string NomorRab { get; set; }
        public string NomorBa { get; set; }

        public int? IdUserRequest { get; set; }
        public bool? IncludeData { get; set; } = false;
        public bool? IsSelesai { get; set; }

        public int? IdUserPermohonan { get; set; }
        public int? IdUserBeritaAcara { get; set; }

    }
}
