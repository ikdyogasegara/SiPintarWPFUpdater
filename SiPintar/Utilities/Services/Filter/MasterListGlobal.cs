using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using AppBusiness.Data.DTOs;

namespace SiPintar.Utilities.Services.Filter
{
    [ExcludeFromCodeCoverage]
    public static class MasterListGlobal
    {
        public static ObservableCollection<PengaturanDto> Pengaturan { get; set; } = new ObservableCollection<PengaturanDto>();
        public static ObservableCollection<MasterGolonganDto> MasterGolongan { get; set; } = new ObservableCollection<MasterGolonganDto>();
        public static ObservableCollection<MasterDiameterDto> MasterDiameter { get; set; } = new ObservableCollection<MasterDiameterDto>();
        public static ObservableCollection<MasterRayonDto> MasterRayon { get; set; } = new ObservableCollection<MasterRayonDto>();
        public static ObservableCollection<MasterAreaDto> MasterArea { get; set; } = new ObservableCollection<MasterAreaDto>();
        public static ObservableCollection<MasterWilayahDto> MasterWilayah { get; set; } = new ObservableCollection<MasterWilayahDto>();
        public static ObservableCollection<MasterKelurahanDto> MasterKelurahan { get; set; } = new ObservableCollection<MasterKelurahanDto>();
        public static ObservableCollection<MasterKecamatanDto> MasterKecamatan { get; set; } = new ObservableCollection<MasterKecamatanDto>();
        public static ObservableCollection<MasterCabangDto> MasterCabang { get; set; } = new ObservableCollection<MasterCabangDto>();
        public static ObservableCollection<MasterUserDto> MasterUser { get; set; } = new ObservableCollection<MasterUserDto>();
        public static ObservableCollection<MasterTipePermohonanDto> MasterTipePermohonan { get; set; } = new ObservableCollection<MasterTipePermohonanDto>();
        public static ObservableCollection<MasterStatusDto> MasterStatus { get; set; } = new ObservableCollection<MasterStatusDto>();
        public static ObservableCollection<MasterKolektifDto> MasterKolektif { get; set; } = new ObservableCollection<MasterKolektifDto>();
        public static ObservableCollection<MasterFlagDto> MasterFlag { get; set; } = new ObservableCollection<MasterFlagDto>();
        public static ObservableCollection<MasterKondisiMeterDto> MasterKondisiMeter { get; set; } = new ObservableCollection<MasterKondisiMeterDto>();
        public static ObservableCollection<MasterAdministrasiLainDto> MasterAdministrasiLain { get; set; } = new ObservableCollection<MasterAdministrasiLainDto>();
        public static ObservableCollection<MasterPemeliharaanLainDto> MasterPemeliharaanLain { get; set; } = new ObservableCollection<MasterPemeliharaanLainDto>();
        public static ObservableCollection<MasterRetribusiLainDto> MasterRetribusiLain { get; set; } = new ObservableCollection<MasterRetribusiLainDto>();
        public static ObservableCollection<MasterMeteraiDto> MasterMeterai { get; set; } = new ObservableCollection<MasterMeteraiDto>();
        public static ObservableCollection<MasterBlokDto> MasterBlok { get; set; } = new ObservableCollection<MasterBlokDto>();
        public static ObservableCollection<MasterMerekMeterDto> MasterMerekMeter { get; set; } = new ObservableCollection<MasterMerekMeterDto>();
        public static ObservableCollection<MasterLoketDto> MasterLoket { get; set; } = new ObservableCollection<MasterLoketDto>();
        public static ObservableCollection<MasterKelainanDto> MasterKelainan { get; set; } = new ObservableCollection<MasterKelainanDto>();
        public static ObservableCollection<MasterPetugasBacaDto> MasterPetugasBaca { get; set; } = new ObservableCollection<MasterPetugasBacaDto>();
        public static ObservableCollection<MasterPeriodeDto> MasterPeriode { get; set; } = new ObservableCollection<MasterPeriodeDto>();
        public static ObservableCollection<MasterPeriodeGudangDto> MasterPeriodeGudang { get; set; } = new ObservableCollection<MasterPeriodeGudangDto>();
        public static ObservableCollection<MasterPekerjaanDto> MasterPekerjaan { get; set; } = new ObservableCollection<MasterPekerjaanDto>();
        public static ObservableCollection<MasterTarifLimbahDto> MasterTarifLimbah { get; set; } = new ObservableCollection<MasterTarifLimbahDto>();
        public static ObservableCollection<MasterTarifLlttDto> MasterTarifLltt { get; set; } = new ObservableCollection<MasterTarifLlttDto>();
        public static ObservableCollection<MasterTarifTangkiDto> MasterTarifTangki { get; set; } = new ObservableCollection<MasterTarifTangkiDto>();
        public static ObservableCollection<MasterSumberAirDto> MasterSumberAir { get; set; } = new ObservableCollection<MasterSumberAirDto>();
        public static ObservableCollection<MasterJenisBangunanDto> MasterJenisBangunan { get; set; } = new ObservableCollection<MasterJenisBangunanDto>();
        public static ObservableCollection<MasterKepemilikanDto> MasterKepemilikan { get; set; } = new ObservableCollection<MasterKepemilikanDto>();
        public static ObservableCollection<MasterPeruntukanDto> MasterPeruntukan { get; set; } = new ObservableCollection<MasterPeruntukanDto>();
        public static ObservableCollection<MasterDmaDto> MasterDma { get; set; } = new ObservableCollection<MasterDmaDto>();
        public static ObservableCollection<MasterDmzDto> MasterDmz { get; set; } = new ObservableCollection<MasterDmzDto>();
        public static ObservableCollection<MasterJenisNonAirDto> MasterJenisNonAir { get; set; } = new ObservableCollection<MasterJenisNonAirDto>();
        public static ObservableCollection<MasterJenisBarangDto> MasterJenisBarang { get; set; } = new ObservableCollection<MasterJenisBarangDto>();
        public static ObservableCollection<MasterPerkiraan3Dto> MasterPerkiraan3 { get; set; } = new ObservableCollection<MasterPerkiraan3Dto>();
        public static ObservableCollection<MasterPerkiraan2Dto> MasterPerkiraan2 { get; set; } = new ObservableCollection<MasterPerkiraan2Dto>();
        public static ObservableCollection<MasterPerkiraan1Dto> MasterPerkiraan1 { get; set; } = new ObservableCollection<MasterPerkiraan1Dto>();
        public static ObservableCollection<MasterAkunEtapDto> MasterAkunEtap { get; set; } = new ObservableCollection<MasterAkunEtapDto>();
        public static ObservableCollection<MasterAkunBankDto> MasterAkunBank { get; set; } = new ObservableCollection<MasterAkunBankDto>();
        public static ObservableCollection<MasterAlasanBatalDto> MasterAlasanBatal { get; set; } = new ObservableCollection<MasterAlasanBatalDto>();
        public static ObservableCollection<MasterTipePendaftaranSambunganDto> MasterTipePendaftaranSambungan { get; set; } = new ObservableCollection<MasterTipePendaftaranSambunganDto>();
        public static ObservableCollection<MasterPegawaiDto> MasterPegawai { get; set; } = new ObservableCollection<MasterPegawaiDto>();
        public static ObservableCollection<MasterMaterialDto> MasterMaterial { get; set; } = new ObservableCollection<MasterMaterialDto>();
        public static int NotifUsulanKoreksiRekening_MenungguVerifikasiPusat { get; set; }
        public static int NotifUsulanKoreksiRekening_MenungguVerifikasiLapangan { get; set; }
        public static ObservableCollection<MasterTipePerkiraanDto> MasterTipePerkiraan { get; set; } = new ObservableCollection<MasterTipePerkiraanDto>();
        public static ObservableCollection<MasterJenisPenerimaanPengeluaranDto> MasterJenisPenerimaanPengeluaran { get; set; } = new ObservableCollection<MasterJenisPenerimaanPengeluaranDto>();
        public static ObservableCollection<MasterKategoriBarangKeluarDto> MasterKategoriBarangKeluar { get; set; } = new ObservableCollection<MasterKategoriBarangKeluarDto>();
        public static ObservableCollection<MasterKategoriBarangMasukDto> MasterKategoriBarangMasuk { get; set; } = new ObservableCollection<MasterKategoriBarangMasukDto>();
        public static ObservableCollection<DaftarPostingAkuntansiDto> DaftarPostingAkuntansi { get; set; } = new ObservableCollection<DaftarPostingAkuntansiDto>();
        public static ObservableCollection<MasterPenyusutanAktivaDto> MasterPenyusutanAktiva { get; set; } = new ObservableCollection<MasterPenyusutanAktivaDto>();
        public static ObservableCollection<MasterGudangDto> MasterGudang { get; set; } = new ObservableCollection<MasterGudangDto>();
        public static ObservableCollection<MasterBagianMemintaBarangDto> MasterBagianMemintaBarang { get; set; } = new ObservableCollection<MasterBagianMemintaBarangDto>();
        public static ObservableCollection<MasterBarangDto> MasterBarang { get; set; } = new ObservableCollection<MasterBarangDto>();
        public static ObservableCollection<MasterPaketDto> MasterPaketRab { get; set; } = new ObservableCollection<MasterPaketDto>();
        public static ObservableCollection<MasterJenisGantiMeterDto> MasterJenisGantiMeter { get; set; } = new ObservableCollection<MasterJenisGantiMeterDto>();
        public static ObservableCollection<MasterPeriodeAkuntansiDto> MasterPeriodeAkuntansi { get; set; } = new ObservableCollection<MasterPeriodeAkuntansiDto>();
        public static ObservableCollection<AnggaranLabaRugiMasterDto> AnggaranLabaRugiMaster { get; set; } = new ObservableCollection<AnggaranLabaRugiMasterDto>();
        public static ObservableCollection<MasterKeperluanDto> MasterKeperluan { get; set; } = new ObservableCollection<MasterKeperluanDto>();
        public static ObservableCollection<MasterParameterAkuntansiDto> MasterParameterAkuntansi { get; set; } = new ObservableCollection<MasterParameterAkuntansiDto>();
        public static ObservableCollection<DaftarPenerimaanKasDto> DaftarPenerimaanKas { get; set; } = new ObservableCollection<DaftarPenerimaanKasDto>();
        public static ObservableCollection<MasterBankDto> MasterBank { get; set; } = new ObservableCollection<MasterBankDto>();
        public static ObservableCollection<dynamic> MasterPeriodeAkuntansiTriwulan { get; set; } = new ObservableCollection<dynamic>();
        public static ObservableCollection<ReportFilterCustomDto> ReportFilterCustom { get; set; } = new ObservableCollection<ReportFilterCustomDto>();
        public static ObservableCollection<NeracaMasterDto> NeracaMaster { get; set; } = new ObservableCollection<NeracaMasterDto>();
        public static ObservableCollection<ArusKasTidakLangsungMasterDto> ArusKasMaster { get; set; } = new ObservableCollection<ArusKasTidakLangsungMasterDto>();
        public static ObservableCollection<MasterJenisVoucherDto> MasterJenisVoucher { get; set; } = new ObservableCollection<MasterJenisVoucherDto>();
        public static ObservableCollection<MasterHarianKasDto> MasterHarianKas { get; set; } = new ObservableCollection<MasterHarianKasDto>();
        public static ObservableCollection<LaporanPerputaranUangMasterDto> PerputaranUangMaster { get; set; } = new ObservableCollection<LaporanPerputaranUangMasterDto>();
        public static ObservableCollection<EkuitasMasterDto> EkuitasMaster { get; set; } = new ObservableCollection<EkuitasMasterDto>();


        [ExcludeFromCodeCoverage]
        public static void ClearMasterListGlobal()
        {
            var type = typeof(MasterListGlobal);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name != "ClearMasterListGlobal" && property.Name != "NotifUsulanKoreksiRekening_MenungguVerifikasiPusat" && property.Name != "NotifUsulanKoreksiRekening_MenungguVerifikasiLapangan")
                {
                    property.SetValue(property, null);
                }
            }
        }
    }
}
