﻿using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirDetailCetakDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public string ValueString { get; set; }
        public decimal ValueDecimal { get; set; } = 0;
        public int ValueInteger { get; set; }
        public DateTime ValueDate { get; set; }
        public bool ValueBool { get; set; }
    }
}
