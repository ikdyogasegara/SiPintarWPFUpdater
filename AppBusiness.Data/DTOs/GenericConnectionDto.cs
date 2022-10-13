using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class GenericConnectionDto
    {
        public int? Id { get; set; }
        public string ConnectionName { get; set; }
        public string KeyToken { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Port { get; set; }
        public string Db { get; set; }
        public DateTime? Waktuupdate { get; set; }
    }
}
