namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJenisTunjanganDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisTunjangan { get; set; }
        public string KodeJenisTunjangan { get; set; }
        public string NamaJenisTunjangan { get; set; }
        public int? Urutan { get; set; } = 0;
        public string Tipe { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJenisTunjanganFilterDto : ParamMasterJenisTunjanganDto
    {
    }
}
