using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirRabCetakDto
    {
        public string NomorRab { get; set; }
        public DateTime TanggalRab { get; set; } = DateTime.Now;
        public string NomorBppi { get; set; }
        public DateTime TanggalBppi { get; set; } = DateTime.Now;
        public string NomorSpkp { get; set; }
        public DateTime TanggalSpkp { get; set; } = DateTime.Now;
        public string NomorSppb { get; set; }
        public DateTime TanggalSppb { get; set; } = DateTime.Now;
        public string NomorBapb { get; set; }
        public int IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime TanggalKadaluarsa { get; set; } = DateTime.Now;
        public string NamaPaketReguler { get; set; }
        public string NamaPaketPersil { get; set; }
    }
}
