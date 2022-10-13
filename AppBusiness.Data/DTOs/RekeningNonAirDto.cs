using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class RekeningNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }

        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }

        public string NomorNonAir { get; set; }
        public string JenisPelanggan { get; set; }
        public string JenisTipePelanggan { get; set; }
        public string NomorPelanggan { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TanggalMulaiTagih { get; set; }
        public DateTime? TanggalKadaluarsa { get; set; }
        public string NomorTransaksi { get; set; }
        public bool? StatusTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdUserTransaksi { get; set; }
        public string NamaUserTransaksi { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string AlasanBatal { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeArea { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string KodeTarif { get; set; }
        public string NamaTarif { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdTarifLimbah { get; set; }
        public int? IdTarifLltt { get; set; }
        public bool? FlagAngsur { get; set; }
        public int? IdAngsuran { get; set; }
        public int? Termin { get; set; }
        public bool? FlagManual { get; set; }
        public int? IdPermohonanSambunganBaru { get; set; }
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
        public List<RekeningNonAirDetailDto> Detail { get; set; }
    }

    public class RekeningNonAirViewDto
    {
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string NomorNonAir { get; set; }
        public decimal? Total { get; set; }
        public string NomorTransaksi { get; set; }
        public bool? StatusTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdUserTransaksi { get; set; }
        public string NamaUserTransaksi { get; set; }
        public bool? FlagAngsur { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public List<RekeningNonAirDetailDto> Detail { get; set; }
    }

    public class ParamRekeningNonAirFilterDto : RekeningNonAirDto
    {
        public string NoSambAir { get; set; }
        public string NomorLimbah { get; set; }
        public string NomorLltt { get; set; }
        public int? IdWilayah { get; set; }
        public int? TahunTransaksi { get; set; }
        public DateTime? WaktuTransaksiAwal { get; set; }
        public DateTime? WaktuTransaksiAkhir { get; set; }
        public bool? SelainJenisLikeSambunganBaru { get; set; }
    }
}
