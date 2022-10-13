using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class GenericQueryDto
    {
        public int? Id { get; set; }
        public string Note { get; set; }
        public string QuerySelect { get; set; }
        public DateTime? Waktuupdate { get; set; }
    }
}
