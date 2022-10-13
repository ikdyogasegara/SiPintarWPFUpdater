using System;
using System.Diagnostics.CodeAnalysis;
using AppBusiness.Data.DTOs;

namespace AppBusiness.Data.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class KalkulasiRekeningAirHelper
    {
        public static ResultKalkulasiRekeningAirDto Hitung(
            decimal? pakai,
            MasterGolonganDto golongan,
            MasterDiameterDto diameter,
            MasterAdministrasiLainDto administrasiLain,
            MasterPemeliharaanLainDto pemeliharaanLain,
            MasterRetribusiLainDto retribusiLain,
            MasterMeteraiDto meterai,
            MasterStatusDto status,
            int? idFlag,
            DateTime? tglMulaiDenda1,
            DateTime? tglMulaiDenda2,
            DateTime? tglMulaiDenda3,
            DateTime? tglMulaiDenda4,
            DateTime? tglMulaiDendaPerBulan,
            bool? simulasi = false,
            bool? flagHanyaAbonemen = false
        )
        {
            var paramAdministrasi = (golongan != null ? golongan.Administrasi : 0) +
                                    (diameter != null ? diameter.Administrasi : 0) +
                                    (administrasiLain != null ? administrasiLain.Administrasi : 0);

            var paramPemeliharaan = (golongan != null ? golongan.Pemeliharaan : 0) +
                                    (diameter != null ? diameter.Pemeliharaan : 0) +
                                    (pemeliharaanLain != null ? pemeliharaanLain.Pemeliharaan : 0);

            var paramRetribusi = (golongan != null ? golongan.Retribusi : 0) +
                                 (diameter != null ? diameter.Retribusi : 0) +
                                 (retribusiLain != null ? retribusiLain.Retribusi : 0);

            var paramPelayanan = (golongan != null ? golongan.Pelayanan : 0) +
                                 (diameter != null ? diameter.Pelayanan : 0);

            var paramAirLimbah = (golongan != null ? golongan.AirLimbah : 0) +
                                 (diameter != null ? diameter.AirLimbah : 0);

            var paramMinPakai = golongan != null ? golongan.MinPakai : 0;

            var paramDendaPakai0 = (golongan != null ? golongan.DendaPakai0 : 0) +
                                   (diameter != null ? diameter.DendaPakai0 : 0);

            var paramRetribusiPakai = golongan != null ? golongan.RetribusiPakai : 0;

            var paramPpn = golongan != null ? golongan.Ppn : 0;

            var paramAdministrasiLain = administrasiLain != null ? administrasiLain.Administrasi : 0;
            var paramPemeliharaanLain = pemeliharaanLain != null ? pemeliharaanLain.Pemeliharaan : 0;
            var paramRetribusiLain = retribusiLain != null ? retribusiLain.Retribusi : 0;

            var paramMeteraiBatas1 = meterai != null ? meterai.Batas1 : 0;
            var paramMeteraiBatas2 = meterai != null ? meterai.Batas2 : 0;
            var paramMeteraiBatas3 = meterai != null ? meterai.Batas3 : 0;
            var paramMeterai1 = meterai != null ? meterai.Meterai1 : 0;
            var paramMeterai2 = meterai != null ? meterai.Meterai2 : 0;
            var paramMeterai3 = meterai != null ? meterai.Meterai3 : 0;

            var bb1 = golongan != null ? golongan.Bb1 : 0;
            var bb2 = golongan != null ? golongan.Bb2 : 0;
            var bb3 = golongan != null ? golongan.Bb3 : 0;
            var bb4 = golongan != null ? golongan.Bb4 : 0;
            var bb5 = golongan != null ? golongan.Bb5 : 0;

            var ba1 = golongan != null ? golongan.Ba1 : 0;
            var ba2 = golongan != null ? golongan.Ba2 : 0;
            var ba3 = golongan != null ? golongan.Ba3 : 0;
            var ba4 = golongan != null ? golongan.Ba4 : 0;
            var ba5 = golongan != null ? golongan.Ba5 : 0;

            var t1 = golongan != null ? golongan.T1 : 0;
            var t2 = golongan != null ? golongan.T2 : 0;
            var t3 = golongan != null ? golongan.T3 : 0;
            var t4 = golongan != null ? golongan.T4 : 0;
            var t5 = golongan != null ? golongan.T5 : 0;

            var t1Tetap = golongan != null ? golongan.T1Tetap : false;
            var t2Tetap = golongan != null ? golongan.T2Tetap : false;
            var t3Tetap = golongan != null ? golongan.T3Tetap : false;
            var t4Tetap = golongan != null ? golongan.T4Tetap : false;
            var t5Tetap = golongan != null ? golongan.T5Tetap : false;

            decimal? Pakai = pakai ?? 0;
            decimal? Meterai = 0;
            decimal? biayaPemakaian = 0;
            decimal? blok1 = 0;
            decimal? blok2 = 0;
            decimal? blok3 = 0;
            decimal? blok4 = 0;
            decimal? blok5 = 0;
            decimal? prog1 = 0;
            decimal? prog2 = 0;
            decimal? prog3 = 0;
            decimal? prog4 = 0;
            decimal? prog5 = 0;

            #region Cari Pakai Kalkulasi

            var pakaiKalkulasi = paramMinPakai > 0 && paramMinPakai > Pakai ? paramMinPakai : Pakai;

            #endregion

            #region Cek Apakah Kena Denda Pakai 0

            var dendaPakai0 = Pakai == 0 ? paramDendaPakai0 : 0;

            #endregion

            #region Cek Apakah STATUS tanpa biayapemakaian & HITUNG

            if (status == null || (status.TanpaBiayaPemakaianAir == false && flagHanyaAbonemen == false))
            {
                #region Cari Blok & Progresif 1

                switch (pakaiKalkulasi >= bb1)
                {
                    case true when pakaiKalkulasi <= ba1:
                        blok1 = pakaiKalkulasi;
                        break;
                    default:
                        blok1 = ba1 - bb1 < 0 ? 0 : ba1 - bb1;
                        break;
                }

                prog1 = t1Tetap == false ? blok1 * t1 : t1;

                #endregion

                #region Cari Blok & Progresif 2

                switch (pakaiKalkulasi > bb2)
                {
                    case true when pakaiKalkulasi <= ba2:
                        blok2 = pakaiKalkulasi - ba1;
                        break;
                    default:
                        {
                            blok2 = (pakaiKalkulasi > ba2) switch
                            {
                                true => ba2 - ba1,
                                _ => blok2
                            };

                            break;
                        }
                }

                prog2 = t2Tetap == false ? blok2 * t2 : t2;

                #endregion

                #region Cari Blok & Progresif 3

                switch (pakaiKalkulasi > bb3)
                {
                    case true when pakaiKalkulasi <= ba3:
                        blok3 = pakaiKalkulasi - ba2;
                        break;
                    default:
                        {
                            blok3 = (pakaiKalkulasi > ba3) switch
                            {
                                true => ba3 - ba2,
                                _ => blok3
                            };

                            break;
                        }
                }

                prog3 = t3Tetap == false ? blok3 * t3 : t3;

                #endregion

                #region Cari Blok & Progresif 4

                switch (pakaiKalkulasi > bb4)
                {
                    case true when pakaiKalkulasi <= ba4:
                        blok4 = pakaiKalkulasi - ba3;
                        break;
                    default:
                        {
                            blok4 = (pakaiKalkulasi > ba4) switch
                            {
                                true => ba4 - ba3,
                                _ => blok4
                            };

                            break;
                        }
                }

                prog4 = t4Tetap == false ? blok4 * t4 : t4;

                #endregion

                #region Cari Blok & Progresif 5

                switch (pakaiKalkulasi > bb5)
                {
                    case true when pakaiKalkulasi <= ba5:
                        blok5 = pakaiKalkulasi - ba4;
                        break;
                    default:
                        {
                            blok5 = (pakaiKalkulasi > ba5) switch
                            {
                                true => ba5 - ba4,
                                _ => blok5
                            };

                            break;
                        }
                }

                prog5 = t5Tetap == false ? blok5 * t5 : t5;

                #endregion

                #region Hitung Biaya pemakaian

                biayaPemakaian = prog1 + prog2 + prog3 + prog4 + prog5;

                #endregion
            }

            #endregion

            #region Hitung Retribusi Pakai

            var retribusiPakai = Pakai * paramRetribusiPakai;

            #endregion

            #region Hitung PPN

            var ppn = (biayaPemakaian + paramAdministrasi +
                       paramPemeliharaan + paramRetribusi +
                       paramPelayanan + paramAirLimbah +
                       retribusiPakai + dendaPakai0 +
                       paramAdministrasiLain + paramPemeliharaanLain +
                       paramRetribusiLain
                      ) *
                      (paramPpn / 100);

            #endregion

            #region Hitung Meterai

            var temp = biayaPemakaian +
                       paramAdministrasi +
                       paramPemeliharaan +
                       paramRetribusi +
                       paramPelayanan +
                       paramAirLimbah +
                       retribusiPakai +
                       dendaPakai0 +
                       paramAdministrasiLain +
                       paramPemeliharaanLain +
                       paramRetribusiLain +
                       ppn;

            switch (temp >= paramMeteraiBatas1)
            {
                case true when temp < paramMeteraiBatas2:
                    Meterai = paramMeterai1;
                    break;
                default:
                    {
                        switch (temp >= paramMeteraiBatas2)
                        {
                            case true when temp < paramMeteraiBatas3:
                                Meterai = paramMeterai2;
                                break;
                            default:
                                {
                                    Meterai = (temp >= paramMeteraiBatas3) switch
                                    {
                                        true => paramMeterai3,
                                        _ => Meterai
                                    };

                                    break;
                                }
                        }

                        break;
                    }
            }

            #endregion

            #region Hitung Rekair

            var rekair = biayaPemakaian +
                         paramAdministrasi +
                         paramPemeliharaan +
                         paramRetribusi +
                         paramPelayanan +
                         paramAirLimbah +
                         retribusiPakai +
                         dendaPakai0 +
                         paramAdministrasiLain +
                         paramPemeliharaanLain +
                         paramRetribusiLain +
                         ppn +
                         Meterai;

            #endregion

            #region Hitung Denda

            decimal? denda = 0;
            if (idFlag != 2)
            {
                var tanggal = DateTime.Now.Date;
                decimal? dendaProgresif = 0;
                decimal? dendaBulanan = 0;

                switch (golongan != null)
                {
                    case true:

                        if (tglMulaiDendaPerBulan != null && tanggal.Date >= tglMulaiDendaPerBulan.Value.Date && golongan.DendaTunggakanPerBulan > 0)
                        {
                            dendaBulanan = golongan.DendaTunggakanPerBulan * MonthDifference(tglMulaiDendaPerBulan.Value, tanggal);
                        }

                        if (tglMulaiDenda4 != null && tanggal.Date >= tglMulaiDenda4.Value.Date && golongan.DendaTunggakan4 > 0)
                        {
                            dendaProgresif = golongan.DendaTunggakan4;
                        }
                        else if (tglMulaiDenda3 != null && tanggal.Date >= tglMulaiDenda3.Value.Date && golongan.DendaTunggakan3 > 0)
                        {
                            dendaProgresif = golongan.DendaTunggakan3;
                        }
                        else if (tglMulaiDenda2 != null && tanggal.Date >= tglMulaiDenda2.Value.Date && golongan.DendaTunggakan2 > 0)
                        {
                            dendaProgresif = golongan.DendaTunggakan2;
                        }
                        else if (tglMulaiDenda1 != null && tanggal.Date >= tglMulaiDenda1.Value.Date && golongan.DendaTunggakan1 > 0)
                        {
                            dendaProgresif = golongan.DendaTunggakan1;
                        }

                        denda = dendaProgresif + dendaBulanan;
                        break;
                }
            }

            #endregion

            return new ResultKalkulasiRekeningAirDto
            {
                Pakai = Pakai,
                PakaiKalkulasi = pakaiKalkulasi,
                BiayaPemakaian = biayaPemakaian,
                Administrasi = paramAdministrasi,
                Pemeliharaan = paramPemeliharaan,
                Retribusi = paramRetribusi,
                Pelayanan = paramPelayanan,
                AirLimbah = paramAirLimbah,
                MinPakai = paramMinPakai,
                DendaPakai0 = dendaPakai0,
                AdministrasiLain = paramAdministrasiLain,
                PemeliharaanLain = paramPemeliharaanLain,
                RetribusiLain = paramRetribusiLain + retribusiPakai,
                Ppn = ppn,
                Meterai = Meterai,
                Rekair = rekair,
                Denda = denda,
                Total = rekair + denda,
                Blok1 = blok1 < 0 ? 0 : blok1,
                Blok2 = blok2 < 0 ? 0 : blok2,
                Blok3 = blok3 < 0 ? 0 : blok3,
                Blok4 = blok4 < 0 ? 0 : blok4,
                Blok5 = blok5 < 0 ? 0 : blok5,
                Prog1 = prog1 < 0 ? 0 : prog1,
                Prog2 = prog2 < 0 ? 0 : prog2,
                Prog3 = prog3 < 0 ? 0 : prog3,
                Prog4 = prog4 < 0 ? 0 : prog4,
                Prog5 = prog5 < 0 ? 0 : prog5,
                FlagMinimumPakai = golongan != null && golongan.MinPakai >= Pakai
            };
        }

        private static int MonthDifference(DateTime startDate, DateTime endDate)
        {
            return Math.Abs(startDate.Month - endDate.Month + (12 * (startDate.Year - endDate.Year)));
        }
    }
}
