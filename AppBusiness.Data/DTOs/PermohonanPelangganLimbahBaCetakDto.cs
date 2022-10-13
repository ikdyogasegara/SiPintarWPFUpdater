using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganLimbahBaCetakDto
    {
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime WaktuPermohonan { get; set; }
        public string Keterangan { get; set; }
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
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
        public string NomorSpk { get; set; }
        public DateTime TanggalSpk { get; set; }
        public string NomorBa { get; set; }
        public DateTime TanggalBa { get; set; }

        public string StatusPemesanan { get; set; }
        public string StatusRangkaianMeter { get; set; }
        public string StatusTutupTotal { get; set; }
        public string StatusSegel { get; set; }
        public string MerekMeter { get; set; }
        public string WarnaSegel { get; set; }
        public string Memo { get; set; }
        public string StatusPembayaran { get; set; }
        public DateTime WaktuAntarMulai { get; set; }
        public DateTime WaktuAntarSelesai { get; set; }
        public decimal JumlahTangki { get; set; }
        public decimal JumlahSisaTangki { get; set; }
        public decimal DiantarSebelumnya { get; set; }
        public decimal DiantarHariIni { get; set; }
        public string KeteranganLapangan { get; set; }
        public string NoSeriMeter { get; set; }
        public decimal AngkatMeter { get; set; }
        public string NoSeriSegel { get; set; }

        public string NomorRab { get; set; }
        public DateTime TanggalRab { get; set; }
    }
}
