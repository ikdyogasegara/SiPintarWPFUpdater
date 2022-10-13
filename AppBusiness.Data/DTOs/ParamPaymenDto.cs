using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamCekTagihanByIdPelangganAirDto
    {
        public int? IdPdam { get; set; }
        public DateTime? Tanggal { get; set; }
        public string ListIdPelangganAir { get; set; }
    }

    public class ParamCekTagihanByIdNonAirDto
    {
        public int? IdPdam { get; set; }
        public DateTime? Tanggal { get; set; }
        public string ListIdNonAir { get; set; }
    }

    public class ParamCekTagihanByIdPelangganLimbahDto
    {
        public int? IdPdam { get; set; }
        public DateTime? Tanggal { get; set; }
        public string ListIdPelangganLimbah { get; set; }
    }

    public class ParamCekTagihanByIdPelangganLlttDto
    {
        public int? IdPdam { get; set; }
        public DateTime? Tanggal { get; set; }
        public string ListIdPelangganLltt { get; set; }
    }

    public class ParamCekTagihanByNomorAirDto
    {
        public int? IdPdam { get; set; }
        public DateTime? Tanggal { get; set; }
        public string ListNomorAir { get; set; }
    }




    public class ParamBayarTagihanDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdLoket { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdKasir { get; set; }
        public List<BayarRekeningAir> RekeningAir { get; set; }
        public List<BayarRekeningLimbah> RekeningLimbah { get; set; }
        public List<BayarRekeningLltt> RekeningLltt { get; set; }
        public List<BayarRekeningNonAir> RekeningNonAir { get; set; }
        public List<BayarAngsuranAir> AngsuranAir { get; set; }
        public List<BayarAngsuranNonAir> AngsuranNonAir { get; set; }
    }

    public class ParamBatalTagihanDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public DateTime? WaktuBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public int? IdLoket { get; set; }
        public int? IdUser { get; set; }
        public List<BayarRekeningAir> RekeningAir { get; set; }
        public List<BayarRekeningLimbah> RekeningLimbah { get; set; }
        public List<BayarRekeningLltt> RekeningLltt { get; set; }
        public List<BayarRekeningNonAir> RekeningNonAir { get; set; }
        public List<BayarAngsuranAir> AngsuranAir { get; set; }
        public List<BayarAngsuranNonAir> AngsuranNonAir { get; set; }
    }

    public class ParamBatalTagihanPerNomorTransaksiDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public DateTime? WaktuBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public int? IdKolektifTransaksi { get; set; }
        public int? IdLoket { get; set; }
        public int? IdUser { get; set; }
        public string NomorTransaksi { get; set; }
    }

    public class ParamCetakTransaksiDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public List<BayarRekeningAir> RekeningAir { get; set; } = new List<BayarRekeningAir>();
        public List<BayarRekeningLimbah> RekeningLimbah { get; set; } = new List<BayarRekeningLimbah>();
        public List<BayarRekeningLltt> RekeningLltt { get; set; } = new List<BayarRekeningLltt>();
        public List<BayarRekeningNonAir> RekeningNonAir { get; set; } = new List<BayarRekeningNonAir>();
        public List<BayarAngsuranAir> AngsuranAir { get; set; } = new List<BayarAngsuranAir>();
        public List<BayarAngsuranNonAir> AngsuranNonAir { get; set; } = new List<BayarAngsuranNonAir>();
        public bool? IncludeData { get; set; } = false;
    }

    public class BayarRekeningAir
    {
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public decimal? Denda { get; set; } = 0;
        public decimal? Diskon { get; set; } = 0;
    }

    public class BayarRekeningLimbah
    {
        public int? IdPelangganLimbah { get; set; }
        public int? IdPeriode { get; set; }
    }

    public class BayarRekeningLltt
    {
        public int? IdPelangganLltt { get; set; }
        public int? IdPeriode { get; set; }
    }

    public class BayarRekeningNonAir
    {
        public int? IdNonAir { get; set; }
    }

    public class BayarAngsuranAir
    {
        public int? Id { get; set; }
    }

    public class BayarAngsuranNonAir
    {
        public int? Id { get; set; }
    }
}
