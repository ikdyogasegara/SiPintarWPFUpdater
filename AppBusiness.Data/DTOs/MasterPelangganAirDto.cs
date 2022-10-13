using System;
using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class MasterPelangganAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string NoRekening { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
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
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdBlok { get; set; }
        public int? IdMerekMeter { get; set; }
        public string NoSeriMeter { get; set; }
        public string NoSegel { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdSumberAir { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string FotoRumah1 { get; set; }
        public string FotoRumah2 { get; set; }
        public string FotoRumah3 { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }

        public MasterPelangganAirDetailDto MasterPelangganAirDetail { get; set; }

        public static implicit operator ObservableCollection<object>(MasterPelangganAirDto v)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
