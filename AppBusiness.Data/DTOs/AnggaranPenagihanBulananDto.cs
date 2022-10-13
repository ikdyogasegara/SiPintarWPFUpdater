using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class AnggaranPenagihanBulananDto
    {
        public int? IdPdam { get; set; }
        public int? IdAnggaran { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdGolongan { get; set; }

        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? Tahun { get; set; }
        public int? Jenis { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public decimal? Mar { get; set; }

        public decimal? Apr { get; set; }

        public decimal? Mei { get; set; }

        public decimal? Jun { get; set; }

        public decimal? Jul { get; set; }

        public decimal? Agst { get; set; }

        public decimal? Sept { get; set; }

        public decimal? Okt { get; set; }

        public decimal? Nov { get; set; }

        public decimal? Des { get; set; }

        public DateTime? WaktuUpdate { get; set; }

        public bool? FlagHapus { get; set; }
    }
}
