using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTipePerkiraanDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipe { get; set; }
        public string KodeTipe { get; set; }
        public string NamaTipe { get; set; }
        public string Header { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterTipePerkiraanFilterDto : ParamMasterTipePerkiraanDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
