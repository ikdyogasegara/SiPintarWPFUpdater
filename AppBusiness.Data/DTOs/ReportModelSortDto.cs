using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ReportModelSortDto
    {
        public int IdPdam { get; set; }
        public int IdModel { get; set; }
        public int IdSort { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
