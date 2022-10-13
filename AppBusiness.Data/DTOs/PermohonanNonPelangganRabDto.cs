using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanNonPelangganRabDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdJenisNonair { get; set; }
        public int? IdNonAir { get; set; }
        public string NomorRab { get; set; }
        public DateTime? TanggalRab { get; set; }
        public string NomorBppi { get; set; }
        public DateTime? TanggalBppi { get; set; }
        public string NomorSpkp { get; set; }
        public DateTime? TanggalSpkp { get; set; }
        public string NomorSppb { get; set; }
        public DateTime? TanggalSppb { get; set; }
        public string NomorBapb { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? TanggalKadaluarsa { get; set; }
        public DateTime? Waktuupdate { get; set; }

        public string PersilNamaPaket { get; set; }
        public bool? PersilFlagDialihkanKeVendor { get; set; } = false;
        public bool? PersilFlagBiayaDibebankanKePdam { get; set; } = false;
        public decimal? PersilSubTotal { get; set; } = 0;
        public decimal? PersilDibebankankePdam { get; set; } = 0;
        public decimal? PersilTotal { get; set; } = 0;
        public List<PermohonanNonPelangganRabDetailDto> DetailPersil { get; set; }


        public string DistribusiNamaPaket { get; set; }
        public bool? DistribusiFlagDialihkanKeVendor { get; set; } = false;
        public bool? DistribusiFlagBiayaDibebankankePdam { get; set; } = false;
        public decimal? DistribusiSubTotal { get; set; } = 0;
        public decimal? DistribusiDibebankankePdam { get; set; } = 0;
        public decimal? DistribusiTotal { get; set; } = 0;
        public List<PermohonanNonPelangganRabDetailDto> DetailDistribusi { get; set; }


        public string FotoDenah1 { get; set; }
        public string FotoDenah2 { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }


        public decimal? RekapSubTotal { get; set; } = 0;
        public decimal? RekapDibebankankePdam { get; set; } = 0;
        public decimal? RekapTotal { get; set; } = 0;

        public bool? FlagBatal { get; set; } = false;
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public RekeningNonAirViewDto NonAirRab { get; set; }
    }
}
