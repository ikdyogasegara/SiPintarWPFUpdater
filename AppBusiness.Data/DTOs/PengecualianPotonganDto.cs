using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PengecualianPotonganDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengecualianPotongan { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagRutin { get; set; }
        public int? KodePeriode { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public List<PengecualianPotonganDetailDto> Detail { get; set; }
    }
}
