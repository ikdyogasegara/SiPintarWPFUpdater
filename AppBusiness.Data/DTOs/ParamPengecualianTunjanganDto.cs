using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPengecualianTunjanganDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengecualianTunjangan { get; set; }
        public int? IdPegawai { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagRutin { get; set; } = false;
        public int? KodePeriode { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamPengecualianTunjanganDetailDto> Detail { get; set; }
    }

    public class ParamPengecualianTunjanganDetailDto
    {
        public int? IdJenisTunjangan { get; set; }
    }

    public class ParamPengecualianTunjanganFilterDto : ParamPengecualianTunjanganDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
