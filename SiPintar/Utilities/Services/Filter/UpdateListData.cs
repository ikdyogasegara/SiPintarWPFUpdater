using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;

namespace SiPintar.Utilities.Services.Filter
{
    [ExcludeFromCodeCoverage]
    public static class UpdateListData
    {
        public static async Task ProcessAsync(bool isTest = false, List<string> updateList = null)
        {
            if (isTest == false)
            {
                if (updateList == null)
                {
                    updateList = new List<string>() { "-" };
                }

                IFilterService filterService = new FilterService();

                if (updateList.Contains("MasterGolongan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterGolonganDto>>("MasterGolongan");

                if (updateList.Contains("MasterDiameter"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterDiameterDto>>("MasterDiameter");

                if (updateList.Contains("MasterRayon"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterRayonDto>>("MasterRayon");

                if (updateList.Contains("MasterArea"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterAreaDto>>("MasterArea");

                if (updateList.Contains("MasterWilayah"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterWilayahDto>>("MasterWilayah");

                if (updateList.Contains("MasterKelurahan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKelurahanDto>>("MasterKelurahan");

                if (updateList.Contains("MasterKecamatan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKecamatanDto>>("MasterKecamatan");

                if (updateList.Contains("MasterCabang"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterCabangDto>>("MasterCabang");

                if (updateList.Contains("MasterUser"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterUserDto>>("MasterUser");

                if (updateList.Contains("MasterTipePermohonan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterTipePermohonanDto>>("MasterTipePermohonan");

                if (updateList.Contains("MasterStatus"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterStatusDto>>("MasterStatus");

                if (updateList.Contains("MasterKolektif"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKolektifDto>>("MasterKolektif");

                if (updateList.Contains("MasterFlag"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterFlagDto>>("MasterFlag");

                if (updateList.Contains("MasterKondisiMeter"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKondisiMeterDto>>("MasterKondisiMeter");

                if (updateList.Contains("MasterAdministrasiLain"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterAdministrasiLainDto>>("MasterAdministrasiLain");

                if (updateList.Contains("MasterPemeliharaanLain"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPemeliharaanLainDto>>("MasterPemeliharaanLain");

                if (updateList.Contains("MasterRetribusiLain"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterRetribusiLainDto>>("MasterRetribusiLain");

                if (updateList.Contains("MasterMeterai"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterMeteraiDto>>("MasterMeterai");

                if (updateList.Contains("MasterBlok"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterBlokDto>>("MasterBlok");

                if (updateList.Contains("MasterMerekMeter"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterMerekMeterDto>>("MasterMerekMeter");

                if (updateList.Contains("MasterLoket"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterLoketDto>>("MasterLoket");

                if (updateList.Contains("MasterKelainan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKelainanDto>>("MasterKelainan");

                if (updateList.Contains("MasterPetugasBaca"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPetugasBacaDto>>("MasterPetugasBaca");

                if (updateList.Contains("MasterPeriode"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPeriodeDto>>("MasterPeriode");

                if (updateList.Contains("MasterPekerjaan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPekerjaanDto>>("MasterPekerjaan");

                if (updateList.Contains("MasterTarifLimbah"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterTarifLimbahDto>>("MasterTarifLimbah");

                if (updateList.Contains("MasterTarifLltt"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterTarifLlttDto>>("MasterTarifLltt");

                if (updateList.Contains("MasterSumberAir"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterSumberAirDto>>("MasterSumberAir");

                if (updateList.Contains("MasterJenisBangunan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterJenisBangunanDto>>("MasterJenisBangunan");

                if (updateList.Contains("MasterKepemilikan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKepemilikanDto>>("MasterKepemilikan");

                if (updateList.Contains("MasterPeruntukan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPeruntukanDto>>("MasterPeruntukan");

                if (updateList.Contains("MasterDma"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterDmaDto>>("MasterDma");

                if (updateList.Contains("MasterDmz"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterDmzDto>>("MasterDmz");

                if (updateList.Contains("MasterJenisNonAir"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterJenisNonAirDto>>("MasterJenisNonAir");

                if (updateList.Contains("MasterTipePendaftaranSambungan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterTipePendaftaranSambunganDto>>("MasterTipePendaftaranSambungan");

                if (updateList.Contains("MasterAlasanBatal"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterAlasanBatalDto>>("MasterAlasanBatal");

                if (updateList.Contains("MasterPegawai"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPegawaiDto>>("MasterPegawai");

                if (updateList.Contains("MasterMaterial"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterMaterialDto>>("MasterMaterial");

                if (updateList.Contains("MasterBagianMemintaBarang"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterBagianMemintaBarangDto>>("MasterBagianMemintaBarang");

                if (updateList.Contains("MasterBarang"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterBarangDto>>("MasterBarang");

                if (updateList.Contains("MasterPaketRab"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPaketDto>>("MasterPaketRab");

                if (updateList.Contains("MasterJenisGantiMeter"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterJenisGantiMeterDto>>("MasterJenisGantiMeter");

                if (updateList.Contains("MasterTarifTangki"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterTarifTangkiDto>>("MasterTarifTangki");

                if (updateList.Contains("MasterPeriodeAkuntansi"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPeriodeAkuntansiDto>>("MasterPeriodeAkuntansi");

                if (updateList.Contains("AnggaranLabaRugiMaster"))
                    await filterService.GetFilterDataAsync<ObservableCollection<AnggaranLabaRugiMasterDto>>("AnggaranLabaRugiMaster");

                if (updateList.Contains("MasterKategoriBarangMasuk"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKategoriBarangMasukDto>>("MasterKategoriBarangMasuk");

                if (updateList.Contains("MasterPeriodeGudang"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPeriodeGudangDto>>("MasterPeriodeGudang");

                if (updateList.Contains("MasterTipePerkiraan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterTipePerkiraanDto>>("MasterTipePerkiraan");

                if (updateList.Contains("MasterJenisPenerimaanPengeluaran"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterJenisPenerimaanPengeluaranDto>>("MasterJenisPenerimaanPengeluaran");

                if (updateList.Contains("MasterKategoriBarangKeluar"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKategoriBarangKeluarDto>>("MasterKategoriBarangKeluar");

                if (updateList.Contains("MasterGudang"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterGudangDto>>("MasterGudang");

                if (updateList.Contains("MasterPerkiraan1"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPerkiraan1Dto>>("MasterPerkiraan1");

                if (updateList.Contains("MasterPerkiraan2"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPerkiraan2Dto>>("MasterPerkiraan2");

                if (updateList.Contains("MasterPerkiraan3"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPerkiraan3Dto>>("MasterPerkiraan3");

                if (updateList.Contains("MasterAkunEtap"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterAkunEtapDto>>("MasterAkunEtap");

                if (updateList.Contains("DaftarPostingAkuntansi"))
                    await filterService.GetFilterDataAsync<ObservableCollection<DaftarPostingAkuntansiDto>>("DaftarPostingAkuntansi");

                if (updateList.Contains("MasterPenyusutanAktiva"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterPenyusutanAktivaDto>>("MasterPenyusutanAktiva");

                if (updateList.Contains("MasterJenisBarang"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterJenisBarangDto>>("MasterJenisBarang");

                if (updateList.Contains("MasterAkunBank"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterAkunBankDto>>("MasterAkunBank");

                if (updateList.Contains("MasterKeperluan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterKeperluanDto>>("MasterKeperluan");

                if (updateList.Contains("MasterParameterAkuntansi"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterParameterAkuntansiDto>>("MasterParameterAkuntansi");

                if (updateList.Contains("DaftarPenerimaanLainnya"))
                    await filterService.GetFilterDataAsync<ObservableCollection<DaftarPenerimaanKasDto>>("DaftarPenerimaanLainnya");

                if (updateList.Contains("MasterBank"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterBankDto>>("MasterBank");

                if (updateList.Contains("MasterLoket"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterLoketDto>>("MasterLoket");

                if (updateList.Contains("MasterPeriodeAkuntansiTriwulan"))
                    await filterService.GetFilterDataAsync<ObservableCollection<dynamic>>("MasterPeriodeAkuntansiTriwulan");

                if (updateList.Contains("ReportFilterCustom"))
                    await filterService.GetFilterDataAsync<ObservableCollection<ReportFilterCustomDto>>("ReportFilterCustom");

                if (updateList.Contains("NeracaMaster"))
                    await filterService.GetFilterDataAsync<ObservableCollection<NeracaMasterDto>>("NeracaMaster");

                if (updateList.Contains("ArusKasMaster"))
                    await filterService.GetFilterDataAsync<ObservableCollection<ArusKasTidakLangsungMasterDto>>("ArusKasMaster");

                if (updateList.Contains("EkuitasMaster"))
                    await filterService.GetFilterDataAsync<ObservableCollection<EkuitasMasterDto>>("EkuitasMaster");

                if (updateList.Contains("MasterJenisVoucher"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterJenisVoucherDto>>("MasterJenisVoucher");

                if (updateList.Contains("MasterHarianKas"))
                    await filterService.GetFilterDataAsync<ObservableCollection<MasterHarianKasDto>>("MasterHarianKas");

                if (updateList.Contains("PerputaranUangMaster"))
                    await filterService.GetFilterDataAsync<ObservableCollection<LaporanPerputaranUangMasterDto>>("PerputaranUangMaster");
            }
        }
    }
}
