using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAngsuranAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public string NoAngsuran { get; set; }
        public int? IdPelangganAir { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public DateTime? WaktuDaftar { get; set; }
        public int? JumlahTermin { get; set; } = 0;
        public decimal? JumlahAngsuranPokok { get; set; } = 0;
        public decimal? JumlahAngsuranBunga { get; set; } = 0;
        public decimal? JumlahUangMuka { get; set; } = 0;
        public decimal? Total { get; set; }
        public int? IdUser { get; set; }
        public DateTime? TglMulaiTagihPertama { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamRekeningAngsuranAirDetailDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranAirFilterDto : ParamRekeningAngsuranAirDto
    {
        public int? IdJenisNonAir { get; set; }
        public string NoSamb { get; set; }
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

    public class ParamRekeningAngsuranAirDetailDto
    {
        public string Uraian { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Termin { get; set; } = 0;
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? PeriodeText { get; set; }

        public DateTime? TglMulaiTagih { get; set; }
        public decimal? BiayaPemakaian { get; set; } = 0;
        public decimal? Administrasi { get; set; } = 0;
        public decimal? Pemeliharaan { get; set; } = 0;
        public decimal? Retribusi { get; set; } = 0;
        public decimal? Pelayanan { get; set; } = 0;
        public decimal? AirLimbah { get; set; } = 0;
        public decimal? DendaPakai0 { get; set; } = 0;
        public decimal? AdministrasiLain { get; set; } = 0;
        public decimal? PemeliharaanLain { get; set; } = 0;
        public decimal? RetribusiLain { get; set; } = 0;
        public decimal? Meterai { get; set; } = 0;
        public decimal? RekAir { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
    }

    public class ParamRekeningAngsuranRekeningAirPublishDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranRekeningAirBeritaAcaraDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranRekeningAirTglMulaiTagihDto
    {
        public int? IdPdam { get; set; }
        public List<ParamRekeningAngsuranRekeningAirListTglMulaiTagihDto> Detail { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamRekeningAngsuranRekeningAirListTglMulaiTagihDto
    {
        public int? Id { get; set; }
        public DateTime? TglMulaiTagih { get; set; }
    }

    public class ParamRekeningAngsuranAirKalkulasiDto
    {
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
        public decimal? PersentaseUangMukaMaksimal { get; set; } = 0;
        public decimal? UangMuka { get; set; } = 0;
        public int? JumlahTermin { get; set; } = 0;
        public DateTime? TglMulaiTagihPertama { get; set; }
        public List<ParamRekeningAngsuranAirKalkulasiDetailDto> Detail { get; set; }
        public List<ParamRekeningAngsuranNonAirLainnyaKalkulasiDetailDto> NonAirLainnya { get; set; }
        public List<ParamRekeningAngsuranLimbahLainnyaKalkulasiDetailDto> Limbah { get; set; }
        public List<ParamRekeningAngsuranLlttLainnyaKalkulasiDetailDto> Lltt { get; set; }
    }

    public class ParamRekeningAngsuranAirKalkulasiDetailDto
    {
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }

        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }

        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public decimal? BiayaPemakaian { get; set; } = 0;
        public decimal? Administrasi { get; set; } = 0;
        public decimal? Pemeliharaan { get; set; } = 0;
        public decimal? Retribusi { get; set; } = 0;
        public decimal? Pelayanan { get; set; } = 0;
        public decimal? AirLimbah { get; set; } = 0;
        public decimal? DendaPakai0 { get; set; } = 0;
        public decimal? AdministrasiLain { get; set; } = 0;
        public decimal? PemeliharaanLain { get; set; } = 0;
        public decimal? RetribusiLain { get; set; } = 0;
        public decimal? Meterai { get; set; } = 0;
        public decimal? RekAir { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
    }

    public class ParamRekeningAngsuranNonAirLainnyaKalkulasiDetailDto
    {
        public int? IdJenisNonair { get; set; }
        public string Keterangan { get; set; }
        public decimal? Total { get; set; } = 0;
        public DateTime? TglMulaiTagih { get; set; }
    }

    public class ParamRekeningAngsuranLimbahLainnyaKalkulasiDetailDto
    {
        public int? IdPeriode { get; set; }
        public decimal? Total { get; set; } = 0;
    }

    public class ParamRekeningAngsuranLlttLainnyaKalkulasiDetailDto
    {
        public int? IdPeriode { get; set; }
        public decimal? Total { get; set; } = 0;
    }
}
