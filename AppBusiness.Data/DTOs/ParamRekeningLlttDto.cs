using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningLlttDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
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
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public int? HapusSecaraAkuntansi { get; set; }
        public decimal? BiayaAwal { get; set; }
        public decimal? BiayaAkhir { get; set; }
        public bool? FlagPublish { get; set; }
        public DateTime? WaktuPublishAwal { get; set; }
        public DateTime? WaktuPublishAkhir { get; set; }
        public bool? FlagUpload { get; set; }
        public DateTime? WaktuUploadAwal { get; set; }
        public DateTime? WaktuUploadAkhir { get; set; }
        public string NomorTransaksi { get; set; }
        public bool? StatusTransaksi { get; set; }
        public int? TahunTransaksi { get; set; }
        public DateTime? WaktuTransaksiAwal { get; set; }
        public DateTime? WaktuTransaksiAkhir { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string AlasanBatal { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningLlttFilterDto : ParamRekeningLlttDto
    {
        public int? IdUserTransaksi { get; set; } // kasir
    }
}
