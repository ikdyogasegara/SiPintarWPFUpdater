using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ListVerifikasiPermohonanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string NomorBerkas { get; set; }
        public string Sumber { get; set; }
        public string JenisPelanggan { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public List<dynamic> Info { get; set; }
    }
}
