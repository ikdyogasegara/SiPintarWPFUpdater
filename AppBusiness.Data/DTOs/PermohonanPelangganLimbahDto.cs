using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganLimbahDto
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
        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Alamat { get; set; }
        public int? IdTarifLimbah { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
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
        public string NamaUser { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public bool? FlagVerifikasi { get; set; }
        public DateTime? WaktuVerifikasi { get; set; }
        public bool? FlagUsulan { get; set; }
        public List<PermohonanPelangganLimbahDetailDto> Parameter { get; set; }
        public RekeningNonAirViewDto NonAirReguler { get; set; }
        public PermohonanPelangganLimbahSpkDto SPK { get; set; }
        public PermohonanPelangganLimbahBaDto BeritaAcara { get; set; }
        public List<PermohonanPelangganLimbahPetugasDto> Pelaksana { get; set; }
        public PermohonanPelangganLimbahRabDto RAB { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int StatusPermohonan { get; set; }
    }
}
