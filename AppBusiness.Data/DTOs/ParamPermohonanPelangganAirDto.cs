using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanPelangganAirDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdPelangganAir { get; set; }
        public string Keterangan { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public int? IdUser { get; set; }
        public bool? FlagUsulan { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public List<ParamPermohonanPelangganAirDetailDto> DetailParameter { get; set; }
        public int? IdJenisNonAir { get; set; }
        public List<ParamPermohonanPelangganAirBiayaDto> DetailBiaya { get; set; }
        public DateTime? WaktuPermohonan { get; set; }


    }

    public class ParamPermohonanPelangganAirDetailDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public dynamic Value { get; set; }
        public string InfoValue { get; set; }
    }

    public class ParamPermohonanPelangganAirBiayaDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Value { get; set; } = 0;
    }


    public class ParamPermohonanPelangganAirFilterDto
    {
        public string Fitur { get; set; }
        public string StatusPermohonanText { get; set; }
        public bool? IncludeRabDetail { get; set; } = false;
        public bool? IncludeBaDetail { get; set; } = false;
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public string Keterangan { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
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
        public int? IdUser { get; set; }
        public int? IdUserVerifikasi { get; set; }
        public string NamaUser { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public List<PermohonanPelangganAirDetailDto> DetailParameter { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public List<RekeningNonAirDetailDto> DetailBiaya { get; set; }
        public RekeningNonAirViewDto NonAir { get; set; }
        public PermohonanPelangganAirSpkDto SPK { get; set; }
        public PermohonanPelangganAirBaDto BeritaAcara { get; set; }
        public List<PermohonanPelangganAirPetugasDto> Petugas { get; set; }
        public List<PermohonanPelangganAirRabDto> RAB { get; set; }
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
        public bool? FlagUsulan { get; set; }
    }
}
