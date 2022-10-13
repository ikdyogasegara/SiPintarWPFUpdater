using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPengecualianPotonganDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengecualianPotongan { get; set; }
        public int? IdPegawai { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagRutin { get; set; } = false;
        public int? KodePeriode { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamPengecualianPotonganDetailDto> Detail { get; set; }
    }

    public class ParamPengecualianPotonganDetailDto
    {
        public int? IdJenisPotongan { get; set; }
    }

    public class ParamPengecualianPotonganFilterDto : ParamPengecualianPotonganDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
