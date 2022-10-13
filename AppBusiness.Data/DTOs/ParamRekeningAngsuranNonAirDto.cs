using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAngsuranNonAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public string NoAngsuran { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public DateTime? WaktuDaftar { get; set; }
        public int? JumlahTermin { get; set; } = 0;
        public decimal? JumlahAngsuranPokok { get; set; } = 0;
        public decimal? JumlahAngsuranBunga { get; set; } = 0;
        public decimal? JumlahUangMuka { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public int? IdUser { get; set; }
        public DateTime? TglMulaiTagihPertama { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamRekeningAngsuranNonAirDetailDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranNonAirFilterDto : ParamRekeningAngsuranNonAirDto
    {
        public string NoSamb { get; set; }
        public string NomorLimbah { get; set; }
        public string NomorLltt { get; set; }
        public bool? FlagPublish { get; set; }
        public DateTime? WaktuPublish { get; set; }
        public bool? FlagLunas { get; set; }
        public DateTime? WaktuLunas { get; set; }
        public bool? StatusTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdPeriode { get; set; }
        public int? Termin { get; set; }
    }


    public class ParamRekeningAngsuranNonAirUpdatePenanggungBebanDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdUserRequest { get; set; }
    }


    public class ParamRekeningAngsuranNonAirDetailDto
    {
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? Termin { get; set; } = 0;
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public DateTime? TglMulaiTagih { get; set; }
        public decimal? Total { get; set; } = 0;
    }

    public class ParamRekeningAngsuranRekeningNonAirPublishDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranRekeningNonAirBeritaAcaraDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranRekeningNonAirTglMulaiTagihDto
    {
        public int? IdPdam { get; set; }
        public List<ParamRekeningAngsuranRekeningNonAirTglMulaiTagihListDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }


    public class ParamRekeningAngsuranRekeningNonAirTglMulaiTagihListDto
    {
        public int? Id { get; set; }
        public DateTime? TglMulaiTagih { get; set; }
    }

    public class ParamRekeningAngsuranNonAirKalkulasiDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public decimal? PersentaseUangMukaMaksimal { get; set; } = 0;
        public decimal? UangMuka { get; set; } = 0;
        public int? JumlahTermin { get; set; } = 0;
        public DateTime? TglMulaiTagihPertama { get; set; }
        public ParamRekeningAngsuranNonAirKalkulasiDetailDto Detail { get; set; }
    }

    public class ParamRekeningAngsuranNonAirKalkulasiDetailDto
    {
        public int? IdNonair { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? IdJenisNonair { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public decimal? Total { get; set; } = 0;
    }
}
