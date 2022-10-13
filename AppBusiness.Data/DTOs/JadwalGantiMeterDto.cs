using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class JadwalGantiMeterDto
    {
        public int? IdPdam { get; set; }
        public int? Tahun { get; set; }
        public int? IdPelangganAir { get; set; }
        public bool? Rutin { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdJenisGantiMeter { get; set; }
        public string JenisGantiMeter { get; set; }
        public string Kategori { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdMerekMeter { get; set; }
        public string NamaMerekMeter { get; set; }
        public string NoSeriMeter { get; set; }
        public DateTime? TglMeter { get; set; }
        public DateTime? TglJadwal { get; set; }
        public int? IdNonAir { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public RekeningNonAirViewDto NonAirReguler { get; set; }
        public JadwalGantiMeterSpkDto SPK { get; set; }
        public JadwalGantiMeterBaDto BeritaAcara { get; set; }
        public List<JadwalGantiMeterPetugasDto> Pelaksana { get; set; }
        public JadwalGantiMeterRabDto RAB { get; set; }
    }
}
