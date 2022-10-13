using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Data.DTOs
{
    public class ReportFilterCustomDto
    {
        public int? IdFilterCustom { get; set; }
        public string Nama { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public virtual ICollection<ReportFilterCustomDetailDto> ReportFilterCustomDetail { get; set; }
    }

    public class ReportFilterCustomDetailDto
    {
        public int? IdFilterCustom { get; set; }
        public int? IdFilterCustomDetail { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public virtual ReportFilterCustomDto ReportFilterCustom { get; set; }
    }
}
