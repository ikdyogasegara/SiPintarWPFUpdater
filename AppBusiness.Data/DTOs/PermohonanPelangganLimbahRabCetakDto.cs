using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganLimbahRabCetakDto
    {
        public string NomorRab { get; set; }
        public DateTime? TanggalRab { get; set; }
        public string NomorBppi { get; set; }
        public DateTime? TanggalBppi { get; set; }
        public string NomorSpkp { get; set; }
        public DateTime? TanggalSpkp { get; set; }
        public string NomorSppb { get; set; }
        public DateTime? TanggalSppb { get; set; }
        public string NomorBapb { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? TanggalKadaluarsa { get; set; }
        public string NamaPaketReguler { get; set; }
        public string NamaPaketPersil { get; set; }
    }
}
