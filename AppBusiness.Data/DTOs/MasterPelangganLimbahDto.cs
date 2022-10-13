using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPelangganLimbahDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
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
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
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
        public DateTime? WaktuUpdate { get; set; }
        public bool? FlagHapus { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
