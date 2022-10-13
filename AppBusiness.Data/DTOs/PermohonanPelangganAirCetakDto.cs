using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirCetakDto
    {
        public int IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime WaktuPermohonan { get; set; }
        public string Keterangan { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public string NamaUser { get; set; }
        public string NamaUserLogin { get; set; }

        public string NomorSpk { get; set; }
        public DateTime TanggalSpk { get; set; }
        public string NamaUserSpk { get; set; }
        public string AlasanBatalSpk { get; set; }

        public string NomorBa { get; set; }
        public DateTime TanggalBa { get; set; }
        public string NomorRab { get; set; }
        public DateTime TanggalRab { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
    }
}
