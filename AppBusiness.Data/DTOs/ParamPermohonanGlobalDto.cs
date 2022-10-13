using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPermohonanGlobalDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdTarifLimbah { get; set; }
        public int? IdTarifLltt { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public string Keterangan { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public int? IdUser { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamPermohonanParameterDto> DetailParameter { get; set; }
        public int? IdJenisNonAir { get; set; }
        public List<ParamPermohonanBiayaDto> DetailBiaya { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public bool? FlagUsulan { get; set; }
    }

    public class ParamPermohonanParameterDto
    {
        public string Parameter { get; set; }
        public string TipeData { get; set; }
        public dynamic Value { get; set; }
        public string InfoValue { get; set; }
    }

    public class ParamPermohonanBiayaDto
    {
        public string Parameter { get; set; }
        public string PostBiaya { get; set; }
        public decimal? Value { get; set; } = 0;
    }
}
