using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanPelangganLimbahSpkDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public DateTime? TanggalSpk { get; set; }
        public int? IdUser { get; set; }
        public List<ParamPetugasSpkLimbahDto> Petugas { get; set; }
        public List<ParamPermohonanPelangganLimbahSpkSPPBDto> Sppb { get; set; }
    }

    public class ParamPetugasSpkLimbahDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamPermohonanPelangganLimbahSpkSPPBDto
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public decimal? Harga { get; set; } = 0;
        public string Satuan { get; set; }
        public decimal? Qty { get; set; } = 0;
    }
}
