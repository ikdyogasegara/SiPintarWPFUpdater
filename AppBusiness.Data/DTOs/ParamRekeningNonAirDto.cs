using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? KodePeriode { get; set; }
        public string NomorNonAir { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TanggalMulaiTagih { get; set; }
        public DateTime? TanggalKadaluarsa { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdTarifLimbah { get; set; }
        public int? IdTarifLltt { get; set; }
        public bool? FlagAngsur { get; set; } = false;
        public bool? FlagManual { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<RekeningNonAirDetailDto> Detail { get; set; }
    }
}
