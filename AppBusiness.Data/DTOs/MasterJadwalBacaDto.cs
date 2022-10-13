using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJadwalBacaDto
    {
        public int? IdPdam { get; set; }
        public int? IdJadwalBaca { get; set; }
        public int? IdPetugasBaca { get; set; }
        public string PetugasBaca { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? TanggalMulaiBaca { get; set; } = 0;
        public int? ToleransiMinus { get; set; } = 0;
        public int? ToleransiPlus { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
