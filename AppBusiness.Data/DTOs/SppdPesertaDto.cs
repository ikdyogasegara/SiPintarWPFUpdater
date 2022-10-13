using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class SppdPesertaDto : ICloneable
    {
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string Jabatan { get; set; }
        public string AreaKerja { get; set; }
        public string Tugas { get; set; }
        public string Departemen { get; set; }
        public string Divisi { get; set; }
        public DateTime? Waktuupdate { get; set; }
        public List<SppdPesertaBiayaDto> Biaya { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
