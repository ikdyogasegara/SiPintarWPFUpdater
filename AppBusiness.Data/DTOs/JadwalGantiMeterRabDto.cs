using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class JadwalGantiMeterRabDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Tahun { get; set; }
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

        public string NamaPaketReguler { get; set; }
        public List<JadwalGantiMeterRabDetailDto> DetailReguler { get; set; }
        public bool? FlagDialihkanKeVendor { get; set; } = false;
        public bool? FlagBiayaDibebankanKePdam { get; set; } = false;
        public string NamaPaketPersil { get; set; }
        public List<JadwalGantiMeterRabDetailDto> DetailPersil { get; set; }
        public bool? FlagDialihkanKeVendorPersil { get; set; } = false;
        public bool? FlagBiayaDibebankankePdamPersil { get; set; } = false;
        public string FotoDenah1 { get; set; }
        public string FotoDenah2 { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public bool? FlagBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public RekeningNonAirViewDto NonAirRab { get; set; }
    }
}
