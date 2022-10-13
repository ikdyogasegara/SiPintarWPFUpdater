using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppBusiness.Data.DTOs
{
    public class ListPermohonanDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public int? JumlahPelangganAir { get; set; } = 0;
        public int? JumlahPelangganLimbah { get; set; } = 0;
        public int? JumlahPelangganLltt { get; set; } = 0;
        public int? JumlahNonPelanggan { get; set; } = 0;
        public int? Jumlah { get; set; } = 0;
    }
}
