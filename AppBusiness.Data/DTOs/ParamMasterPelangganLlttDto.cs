using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPelangganLlttDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public int? IdTarifLltt { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string NoKtp { get; set; }
        public string Email { get; set; }
        public string Keterangan { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string FotoRumah1 { get; set; }
        public string FotoRumah2 { get; set; }
        public string FotoRumah3 { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public string SumberPerubahan { get; set; }
    }

    public class ParamMasterPelangganLlttFilterDto : ParamMasterPelangganLlttDto
    {
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public string NamaStatus { get; set; }
        public string NamaFlag { get; set; }
        public bool? ForMap { get; set; } = false;
        public bool? ForSambungKembali { get; set; } = false;
        public bool? ForTutupSementara { get; set; } = false;
        public bool? ForTutupTotal { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
    }
}
