using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;

namespace SiPintar.Utilities.Services.Filter
{
    [ExcludeFromCodeCoverage]
    public class FilterService : IFilterService
    {
        private readonly IRestApiClientModel _restApi;


        private readonly Dictionary<string, string> _filterList = new Dictionary<string, string>()
        {
            {"MasterGolongan", "master-tarif-golongan"},
            {"MasterDiameter", "master-tarif-diameter"},
            {"MasterRayon", "master-rayon"},
            {"MasterArea", "master-area"},
            {"MasterWilayah", "master-wilayah"},
            {"MasterKelurahan", "master-kelurahan"},
            {"MasterKecamatan", "master-kecamatan"},
            {"MasterCabang", "master-cabang"},
            {"MasterUser", "master-user"},
            {"MasterTipePermohonan", "master-tipe-permohonan"},
            {"MasterStatus", "master-status"},
            {"MasterKolektif", "master-kolektif"},
            {"MasterFlag", "master-flag"},
            {"MasterKondisiMeter", "master-kondisi-meter"},
            {"MasterAdministrasiLain", "master-tarif-administrasi-lain"},
            {"MasterPemeliharaanLain", "master-tarif-pemeliharaan-lain"},
            {"MasterRetribusiLain", "master-tarif-retribusi-lain"},
            {"MasterMeterai", "master-meterai"},
            {"MasterBlok", "master-blok"},
            {"MasterMerekMeter", "master-merek-meter"},
            {"MasterLoket", "master-loket"},
            {"MasterKelainan", "master-kelainan"},
            {"MasterPetugasBaca", "master-petugas-baca"},
            {"MasterPeriode", "master-periode-rekening"},
            {"MasterPeriodeGudang", "master-periode-gudang"},
            {"MasterPekerjaan", "master-pekerjaan"},
            {"MasterTarifLimbah", "master-tarif-limbah"},
            {"MasterTarifLltt", "master-tarif-lltt"},
            {"MasterSumberAir", "master-sumber-air"},
            {"MasterJenisBangunan", "master-jenis-bangunan"},
            {"MasterKepemilikan", "master-kepemilikan"},
            {"MasterPeruntukan", "master-peruntukan"},
            {"MasterDma", "master-dma"},
            {"MasterDmz", "master-dmz"},
            {"MasterJenisNonAir", "master-jenis-non-air"},
            {"MasterJenisBarang", "master-jenis-barang"},
            {"MasterPerkiraan3", "master-perkiraan3"},
            {"MasterPerkiraan2", "master-perkiraan2"},
            {"MasterPerkiraan1", "master-perkiraan1"},
            {"MasterAkunEtap", "master-akun-etap"},
            {"MasterAkunBank", "master-akun-bank"},
            {"MasterAlasanBatal", "master-alasan-batal"},
            {"MasterTipePendaftaranSambungan", "master-tipe-pendaftaran-sambungan"},
            {"MasterPegawai", "master-pegawai"},
            {"MasterMaterial", "master-material"},
            {"MasterTipePerkiraan", "master-tipe-perkiraan"},
            {"MasterJenisPenerimaanPengeluaran", "master-jenis-penerimaan-pengeluaran"},
            {"MasterKategoriBarangKeluar", "master-kategori-barang-keluar"},
            {"MasterKategoriBarangMasuk", "master-kategori-barang-masuk"},
            {"MasterGudang", "master-gudang"},
            {"MasterBagianMemintaBarang", "master-bagian-meminta-barang"},
            {"DaftarPostingAkuntansi", "posting-periode-akuntansi" },
            {"MasterPenyusutanAktiva", "master-penyusutan-aktiva" },
            {"MasterBarang", "master-barang"},
            {"MasterPaketRab", "master-paket"},
            {"MasterJenisGantiMeter", "master-jenis-ganti-meter"},
            {"MasterTarifTangki", "master-tarif-tangki"},
            {"MasterPeriodeAkuntansi", "master-periode-akuntansi"},
            {"MasterPeriodeAkuntansiTriwulan", "master-periode-akuntansi-triwulan"},
            {"AnggaranLabaRugiMaster", "anggaran-laba-rugi-master"},
            {"MasterKeperluan", "master-keperluan"},
            {"MasterParameterAkuntansi","master-parameter-akuntansi"},
            {"DaftarPenerimaanKas", "penerimaan-lainnya"},
            {"MasterBank", "master-bank"},
            {"ReportFilterCustom", "report-filter-custom"},

            {"NeracaMaster", "neraca-master"},
            {"ArusKasMaster", "arus-kas-tidak-langsung-master"},
            {"EkuitasMaster", "ekuitas-master"},
            {"PerputaranUangMaster", "laporan-perputaran-uang-master"},
            {"MasterHarianKas", "harian-kas-master"},
            {"MasterJenisVoucher", "master-jenis-voucher"},
        };

        public FilterService()
        {
            _restApi = new RestApiClientModel();
        }

        public async Task<T> GetFilterDataAsync<T>(string name = null, Dictionary<string, dynamic> additionalParam = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var param = new Dictionary<string, dynamic>() { { "CurrentPage", 1 }, { "PageSize", 10000000 }, };

                if (additionalParam != null)
                {
                    foreach (var i in additionalParam)
                    {
                        param.Add(i.Key, i.Value);
                    }
                }

                if (GetWaktuUpdate(name).HasValue)
                {
                    param.Add("WaktuUpdate", GetWaktuUpdate(name));
                }

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_filterList[name]}", param);
                if (!response.IsError)
                {
                    var result = response.Data;

                    if (result.Status && result.Data != null && result.Data.Count > 0)
                    {
                        var temp = result.Data.ToObject<T>();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            SetData(name, temp);
                        });
                        return GetData(name);
                    }
                }
            }

            return await Task.FromResult(Activator.CreateInstance<T>());
        }

        private DateTime? GetWaktuUpdate(string name)
        {
            if (name == "MasterGolongan" && MasterListGlobal.MasterGolongan != null)
            {
                return MasterListGlobal.MasterGolongan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterDiameter" && MasterListGlobal.MasterDiameter != null)
            {
                return MasterListGlobal.MasterDiameter.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterRayon" && MasterListGlobal.MasterRayon != null)
            {
                return MasterListGlobal.MasterRayon.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterArea" && MasterListGlobal.MasterArea != null)
            {
                return MasterListGlobal.MasterArea.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterWilayah" && MasterListGlobal.MasterWilayah != null)
            {
                return MasterListGlobal.MasterWilayah.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKelurahan" && MasterListGlobal.MasterKelurahan != null)
            {
                return MasterListGlobal.MasterKelurahan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKecamatan" && MasterListGlobal.MasterKecamatan != null)
            {
                return MasterListGlobal.MasterKecamatan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterCabang" && MasterListGlobal.MasterCabang != null)
            {
                return MasterListGlobal.MasterCabang.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterUser" && MasterListGlobal.MasterUser != null)
            {
                return MasterListGlobal.MasterUser.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterTipePermohonan" && MasterListGlobal.MasterTipePermohonan != null)
            {
                return MasterListGlobal.MasterTipePermohonan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterStatus" && MasterListGlobal.MasterStatus != null)
            {
                return MasterListGlobal.MasterStatus.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKolektif" && MasterListGlobal.MasterKolektif != null)
            {
                return MasterListGlobal.MasterKolektif.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterFlag" && MasterListGlobal.MasterFlag != null)
            {
                return MasterListGlobal.MasterFlag.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKondisiMeter" && MasterListGlobal.MasterKondisiMeter != null)
            {
                return MasterListGlobal.MasterKondisiMeter.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterAdministrasiLain" && MasterListGlobal.MasterAdministrasiLain != null)
            {
                return MasterListGlobal.MasterAdministrasiLain.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPemeliharaanLain" && MasterListGlobal.MasterPemeliharaanLain != null)
            {
                return MasterListGlobal.MasterPemeliharaanLain.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterRetribusiLain" && MasterListGlobal.MasterRetribusiLain != null)
            {
                return MasterListGlobal.MasterRetribusiLain.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterMeterai" && MasterListGlobal.MasterMeterai != null)
            {
                return MasterListGlobal.MasterMeterai.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterBlok" && MasterListGlobal.MasterBlok != null)
            {
                return MasterListGlobal.MasterBlok.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterMerekMeter" && MasterListGlobal.MasterMerekMeter != null)
            {
                return MasterListGlobal.MasterMerekMeter.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterLoket" && MasterListGlobal.MasterLoket != null)
            {
                return MasterListGlobal.MasterLoket.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKelainan" && MasterListGlobal.MasterKelainan != null)
            {
                return MasterListGlobal.MasterKelainan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPetugasBaca" && MasterListGlobal.MasterPetugasBaca != null)
            {
                return MasterListGlobal.MasterPetugasBaca.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPeriode" && MasterListGlobal.MasterPeriode != null)
            {
                return MasterListGlobal.MasterPeriode.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPeriodeGudang" && MasterListGlobal.MasterPeriodeGudang != null)
            {
                return MasterListGlobal.MasterPeriodeGudang.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPekerjaan" && MasterListGlobal.MasterPekerjaan != null)
            {
                return MasterListGlobal.MasterPekerjaan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterTarifLimbah" && MasterListGlobal.MasterTarifLimbah != null)
            {
                return MasterListGlobal.MasterTarifLimbah.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterTarifLltt" && MasterListGlobal.MasterTarifLltt != null)
            {
                return MasterListGlobal.MasterTarifLltt.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterSumberAir" && MasterListGlobal.MasterSumberAir != null)
            {
                return MasterListGlobal.MasterSumberAir.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterJenisBangunan" && MasterListGlobal.MasterJenisBangunan != null)
            {
                return MasterListGlobal.MasterJenisBangunan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKepemilikan" && MasterListGlobal.MasterKepemilikan != null)
            {
                return MasterListGlobal.MasterKepemilikan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPeruntukan" && MasterListGlobal.MasterPeruntukan != null)
            {
                return MasterListGlobal.MasterPeruntukan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterDma" && MasterListGlobal.MasterDma != null)
            {
                return MasterListGlobal.MasterDma.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterDmz" && MasterListGlobal.MasterDmz != null)
            {
                return MasterListGlobal.MasterDmz.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterJenisNonAir" && MasterListGlobal.MasterJenisNonAir != null)
            {
                return MasterListGlobal.MasterJenisNonAir.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterJenisBarang" && MasterListGlobal.MasterJenisBarang != null)
            {
                return MasterListGlobal.MasterJenisBarang.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPerkiraan3" && MasterListGlobal.MasterPerkiraan3 != null)
            {
                return MasterListGlobal.MasterPerkiraan3.Max(x => x.WaktuUpdate);
            }


            if (name == "MasterPerkiraan2" && MasterListGlobal.MasterPerkiraan2 != null)
            {
                return MasterListGlobal.MasterPerkiraan2.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPerkiraan1" && MasterListGlobal.MasterPerkiraan1 != null)
            {
                return MasterListGlobal.MasterPerkiraan1.Max(x => x.WaktuUpdate);
            }


            if (name == "MasterAkunEtap" && MasterListGlobal.MasterAkunEtap != null)
            {
                return MasterListGlobal.MasterAkunEtap.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterAkunBank" && MasterListGlobal.MasterAkunBank != null)
            {
                return MasterListGlobal.MasterAkunBank.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterAlasanBatal" && MasterListGlobal.MasterAlasanBatal != null)
            {
                return MasterListGlobal.MasterAlasanBatal.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterTipePendaftaranSambungan" && MasterListGlobal.MasterTipePendaftaranSambungan != null)
            {
                return MasterListGlobal.MasterTipePendaftaranSambungan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPegawai" && MasterListGlobal.MasterPegawai != null)
            {
                return MasterListGlobal.MasterPegawai.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterMaterial" && MasterListGlobal.MasterMaterial != null)
            {
                return MasterListGlobal.MasterMaterial.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterTipePerkiraan" && MasterListGlobal.MasterTipePerkiraan != null)
            {
                return MasterListGlobal.MasterTipePerkiraan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterJenisPenerimaanPengeluaran" && MasterListGlobal.MasterJenisPenerimaanPengeluaran != null)
            {
                return MasterListGlobal.MasterJenisPenerimaanPengeluaran.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKategoriBarangKeluar" && MasterListGlobal.MasterKategoriBarangKeluar != null)
            {
                return MasterListGlobal.MasterKategoriBarangKeluar.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKategoriBarangMasuk" && MasterListGlobal.MasterKategoriBarangMasuk != null)
            {
                return MasterListGlobal.MasterKategoriBarangMasuk.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterGudang" && MasterListGlobal.MasterGudang != null)
            {
                return MasterListGlobal.MasterGudang.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterBagianMemintaBarang" && MasterListGlobal.MasterBagianMemintaBarang != null)
            {
                return MasterListGlobal.MasterBagianMemintaBarang.Max(x => x.WaktuUpdate);
            }

            if (name == "DaftarPostingAkuntansi" && MasterListGlobal.MasterBagianMemintaBarang != null)
            {
                return MasterListGlobal.MasterBagianMemintaBarang.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPenyusutanAktiva" && MasterListGlobal.MasterPenyusutanAktiva != null)
            {
                return MasterListGlobal.MasterPenyusutanAktiva.Max(x => x.WaktuUpdate);
            }
            if (name == "MasterBarang" && MasterListGlobal.MasterBarang != null)
            {
                return MasterListGlobal.MasterBarang.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPaketRab" && MasterListGlobal.MasterPaketRab != null)
            {
                return MasterListGlobal.MasterPaketRab.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterJenisGantiMeter" && MasterListGlobal.MasterJenisGantiMeter != null)
            {
                return MasterListGlobal.MasterJenisGantiMeter.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterTarifTangki" && MasterListGlobal.MasterTarifTangki != null)
            {
                return MasterListGlobal.MasterTarifTangki.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterPeriodeAkuntansi" && MasterListGlobal.MasterPeriodeAkuntansi != null)
            {
                return MasterListGlobal.MasterPeriodeAkuntansi.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterKeperluan" && MasterListGlobal.MasterKeperluan != null)
            {
                return MasterListGlobal.MasterKeperluan.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterParameterAkuntansi" && MasterListGlobal.MasterParameterAkuntansi != null)
            {
                return MasterListGlobal.MasterParameterAkuntansi.Max(x => x.WaktuUpdate);
            }

            if (name == "DaftarPenerimaanLainnya" && MasterListGlobal.DaftarPenerimaanKas != null)
            {
                return MasterListGlobal.DaftarPenerimaanKas.Max(x => x.WaktuUpdate);
            }

            if (name == "MasterBank" && MasterListGlobal.MasterBank != null)
            {
                return MasterListGlobal.MasterBank.Max(x => x.WaktuUpdate);
            }

            if (name == "ReportFilterCustom" && MasterListGlobal.ReportFilterCustom != null)
            {
                return MasterListGlobal.ReportFilterCustom.Max(x => x.WaktuUpdate);
            }

            return null;
        }

        private dynamic GetData(string name)
        {
            return name switch
            {
                "MasterGolongan" => MasterListGlobal.MasterGolongan,
                "MasterDiameter" => MasterListGlobal.MasterDiameter,
                "MasterRayon" => MasterListGlobal.MasterRayon,
                "MasterArea" => MasterListGlobal.MasterArea,
                "MasterWilayah" => MasterListGlobal.MasterWilayah,
                "MasterKelurahan" => MasterListGlobal.MasterKelurahan,
                "MasterKecamatan" => MasterListGlobal.MasterKecamatan,
                "MasterCabang" => MasterListGlobal.MasterCabang,
                "MasterUser" => MasterListGlobal.MasterUser,
                "MasterTipePermohonan" => MasterListGlobal.MasterTipePermohonan,
                "MasterStatus" => MasterListGlobal.MasterStatus,
                "MasterKolektif" => MasterListGlobal.MasterKolektif,
                "MasterFlag" => MasterListGlobal.MasterFlag,
                "MasterKondisiMeter" => MasterListGlobal.MasterKondisiMeter,
                "MasterAdministrasiLain" => MasterListGlobal.MasterAdministrasiLain,
                "MasterPemeliharaanLain" => MasterListGlobal.MasterPemeliharaanLain,
                "MasterRetribusiLain" => MasterListGlobal.MasterRetribusiLain,
                "MasterMeterai" => MasterListGlobal.MasterMeterai,
                "MasterBlok" => MasterListGlobal.MasterBlok,
                "MasterMerekMeter" => MasterListGlobal.MasterMerekMeter,
                "MasterLoket" => MasterListGlobal.MasterLoket,
                "MasterKelainan" => MasterListGlobal.MasterKelainan,
                "MasterPetugasBaca" => MasterListGlobal.MasterPetugasBaca,
                "MasterPeriode" => MasterListGlobal.MasterPeriode,
                "MasterPeriodeGudang" => MasterListGlobal.MasterPeriodeGudang,
                "MasterPekerjaan" => MasterListGlobal.MasterPekerjaan,
                "MasterTarifLimbah" => MasterListGlobal.MasterTarifLimbah,
                "MasterTarifLltt" => MasterListGlobal.MasterTarifLltt,
                "MasterSumberAir" => MasterListGlobal.MasterSumberAir,
                "MasterJenisBangunan" => MasterListGlobal.MasterJenisBangunan,
                "MasterKepemilikan" => MasterListGlobal.MasterKepemilikan,
                "MasterPeruntukan" => MasterListGlobal.MasterPeruntukan,
                "MasterDma" => MasterListGlobal.MasterDma,
                "MasterDmz" => MasterListGlobal.MasterDmz,
                "MasterJenisNonAir" => MasterListGlobal.MasterJenisNonAir,
                "MasterJenisBarang" => MasterListGlobal.MasterJenisBarang,
                "MasterPerkiraan3" => MasterListGlobal.MasterPerkiraan3,
                "MasterPerkiraan2" => MasterListGlobal.MasterPerkiraan2,
                "MasterPerkiraan1" => MasterListGlobal.MasterPerkiraan1,
                "MasterAkunEtap" => MasterListGlobal.MasterAkunEtap,
                "MasterAkunBank" => MasterListGlobal.MasterAkunBank,
                "MasterAlasanBatal" => MasterListGlobal.MasterAlasanBatal,
                "MasterTipePendaftaranSambungan" => MasterListGlobal.MasterTipePendaftaranSambungan,
                "MasterPegawai" => MasterListGlobal.MasterPegawai,
                "MasterMaterial" => MasterListGlobal.MasterMaterial,
                "MasterTipePerkiraan" => MasterListGlobal.MasterTipePerkiraan,
                "MasterJenisPenerimaanPengeluaran" => MasterListGlobal.MasterJenisPenerimaanPengeluaran,
                "MasterKategoriBarangKeluar" => MasterListGlobal.MasterKategoriBarangKeluar,
                "MasterKategoriBarangMasuk" => MasterListGlobal.MasterKategoriBarangMasuk,
                "MasterGudang" => MasterListGlobal.MasterGudang,
                "MasterBagianMemintaBarang" => MasterListGlobal.MasterBagianMemintaBarang,
                "DaftarPostingAkuntansi" => MasterListGlobal.DaftarPostingAkuntansi,
                "MasterPenyusutanAktiva" => MasterListGlobal.MasterPenyusutanAktiva,
                "MasterBarang" => MasterListGlobal.MasterBarang,
                "MasterPaketRab" => MasterListGlobal.MasterPaketRab,
                "MasterJenisGantiMeter" => MasterListGlobal.MasterJenisGantiMeter,
                "MasterTarifTangki" => MasterListGlobal.MasterTarifTangki,
                "MasterPeriodeAkuntansi" => MasterListGlobal.MasterPeriodeAkuntansi,
                "AnggaranLabaRugiMaster" => MasterListGlobal.AnggaranLabaRugiMaster,
                "MasterKeperluan" => MasterListGlobal.MasterKeperluan,
                "MasterParameterAkuntansi" => MasterListGlobal.MasterParameterAkuntansi,
                "DaftarPenerimaanKas" => MasterListGlobal.DaftarPenerimaanKas,
                "MasterBank" => MasterListGlobal.MasterBank,
                "MasterPeriodeAkuntansiTriwulan" => MasterListGlobal.MasterPeriodeAkuntansiTriwulan,
                "ReportFilterCustom" => MasterListGlobal.ReportFilterCustom,
                "NeracaMaster" => MasterListGlobal.NeracaMaster,
                "ArusKasMaster" => MasterListGlobal.ArusKasMaster,
                "EkuitasMaster" => MasterListGlobal.EkuitasMaster,
                "PerputaranUangMaster" => MasterListGlobal.PerputaranUangMaster,
                "MasterHarianKas" => MasterListGlobal.MasterHarianKas,
                "MasterJenisVoucher" => MasterListGlobal.MasterJenisVoucher,
                _ => null
            };
        }

        private void SetData(string name, dynamic param)
        {
            if (name == "MasterGolongan")
            {
                MasterListGlobal.MasterGolongan ??= new ObservableCollection<MasterGolonganDto>();
                var temp = param as ObservableCollection<MasterGolonganDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterGolongan?.FirstOrDefault(x => x.IdGolongan == item.IdGolongan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterGolongan.IndexOf(dataFind);
                        MasterListGlobal.MasterGolongan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterGolongan.Add(item);
                    }
                }
            }

            if (name == "MasterDiameter")
            {
                MasterListGlobal.MasterDiameter ??= new ObservableCollection<MasterDiameterDto>();
                var temp = param as ObservableCollection<MasterDiameterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterDiameter?.FirstOrDefault(x => x.IdDiameter == item.IdDiameter);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterDiameter.IndexOf(dataFind);
                        MasterListGlobal.MasterDiameter[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterDiameter.Add(item);
                    }
                }
            }

            if (name == "MasterRayon")
            {
                MasterListGlobal.MasterRayon ??= new ObservableCollection<MasterRayonDto>();
                var temp = param as ObservableCollection<MasterRayonDto>;
                foreach (var item in temp)
                {
                    try
                    {
                        var dataFind = MasterListGlobal.MasterRayon?.FirstOrDefault(x => x.IdRayon == item.IdRayon);
                        if (dataFind != null)
                        {
                            var idx = MasterListGlobal.MasterRayon.IndexOf(dataFind);
                            MasterListGlobal.MasterRayon[idx] = item;
                        }
                        else
                        {
                            MasterListGlobal.MasterRayon.Add(item);
                        }
                    }
                    catch { }

                }
            }

            if (name == "MasterArea")
            {
                MasterListGlobal.MasterArea ??= new ObservableCollection<MasterAreaDto>();
                var temp = param as ObservableCollection<MasterAreaDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterArea?.FirstOrDefault(x => x.IdArea == item.IdArea);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterArea.IndexOf(dataFind);
                        MasterListGlobal.MasterArea[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterArea.Add(item);
                    }
                }
            }

            if (name == "MasterWilayah")
            {
                MasterListGlobal.MasterWilayah ??= new ObservableCollection<MasterWilayahDto>();
                var temp = param as ObservableCollection<MasterWilayahDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterWilayah?.FirstOrDefault(x => x.IdWilayah == item.IdWilayah);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterWilayah.IndexOf(dataFind);
                        MasterListGlobal.MasterWilayah[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterWilayah.Add(item);
                    }
                }
            }

            if (name == "MasterKelurahan")
            {
                MasterListGlobal.MasterKelurahan ??= new ObservableCollection<MasterKelurahanDto>();
                var temp = param as ObservableCollection<MasterKelurahanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKelurahan?.FirstOrDefault(x => x.IdKelurahan == item.IdKelurahan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKelurahan.IndexOf(dataFind);
                        MasterListGlobal.MasterKelurahan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKelurahan.Add(item);
                    }
                }
            }

            if (name == "MasterKecamatan")
            {
                MasterListGlobal.MasterKecamatan ??= new ObservableCollection<MasterKecamatanDto>();
                var temp = param as ObservableCollection<MasterKecamatanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKecamatan?.FirstOrDefault(x => x.IdKecamatan == item.IdKecamatan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKecamatan.IndexOf(dataFind);
                        MasterListGlobal.MasterKecamatan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKecamatan.Add(item);
                    }
                }
            }

            if (name == "MasterCabang")
            {
                MasterListGlobal.MasterCabang ??= new ObservableCollection<MasterCabangDto>();
                var temp = param as ObservableCollection<MasterCabangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterCabang?.FirstOrDefault(x => x.IdCabang == item.IdCabang);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterCabang.IndexOf(dataFind);
                        MasterListGlobal.MasterCabang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterCabang.Add(item);
                    }
                }
            }

            if (name == "MasterUser")
            {
                MasterListGlobal.MasterUser ??= new ObservableCollection<MasterUserDto>();
                var temp = param as ObservableCollection<MasterUserDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterUser?.FirstOrDefault(x => x.IdUser == item.IdUser);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterUser.IndexOf(dataFind);
                        MasterListGlobal.MasterUser[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterUser.Add(item);
                    }
                }
            }

            if (name == "MasterTipePermohonan")
            {
                MasterListGlobal.MasterTipePermohonan ??= new ObservableCollection<MasterTipePermohonanDto>();
                var temp = param as ObservableCollection<MasterTipePermohonanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterTipePermohonan?.FirstOrDefault(x => x.IdTipePermohonan == item.IdTipePermohonan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterTipePermohonan.IndexOf(dataFind);
                        MasterListGlobal.MasterTipePermohonan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterTipePermohonan.Add(item);
                    }
                }
            }

            if (name == "MasterStatus")
            {
                MasterListGlobal.MasterStatus ??= new ObservableCollection<MasterStatusDto>();
                var temp = param as ObservableCollection<MasterStatusDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterStatus?.FirstOrDefault(x => x.IdStatus == item.IdStatus);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterStatus.IndexOf(dataFind);
                        MasterListGlobal.MasterStatus[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterStatus.Add(item);
                    }
                }
            }

            if (name == "MasterKolektif")
            {
                MasterListGlobal.MasterKolektif ??= new ObservableCollection<MasterKolektifDto>();
                var temp = param as ObservableCollection<MasterKolektifDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKolektif?.FirstOrDefault(x => x.IdKolektif == item.IdKolektif);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKolektif.IndexOf(dataFind);
                        MasterListGlobal.MasterKolektif[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKolektif.Add(item);
                    }
                }
            }

            if (name == "MasterFlag")
            {
                MasterListGlobal.MasterFlag ??= new ObservableCollection<MasterFlagDto>();
                var temp = param as ObservableCollection<MasterFlagDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterFlag?.FirstOrDefault(x => x.IdFlag == item.IdFlag);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterFlag.IndexOf(dataFind);
                        MasterListGlobal.MasterFlag[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterFlag.Add(item);
                    }
                }
            }

            if (name == "MasterKondisiMeter")
            {
                MasterListGlobal.MasterKondisiMeter ??= new ObservableCollection<MasterKondisiMeterDto>();
                var temp = param as ObservableCollection<MasterKondisiMeterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKondisiMeter?.FirstOrDefault(x => x.IdKondisiMeter == item.IdKondisiMeter);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKondisiMeter.IndexOf(dataFind);
                        MasterListGlobal.MasterKondisiMeter[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKondisiMeter.Add(item);
                    }
                }
            }

            if (name == "MasterAdministrasiLain")
            {
                MasterListGlobal.MasterAdministrasiLain ??= new ObservableCollection<MasterAdministrasiLainDto>();
                var temp = param as ObservableCollection<MasterAdministrasiLainDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterAdministrasiLain?.FirstOrDefault(x => x.IdAdministrasiLain == item.IdAdministrasiLain);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterAdministrasiLain.IndexOf(dataFind);
                        MasterListGlobal.MasterAdministrasiLain[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterAdministrasiLain.Add(item);
                    }
                }
            }

            if (name == "MasterPemeliharaanLain")
            {
                MasterListGlobal.MasterPemeliharaanLain ??= new ObservableCollection<MasterPemeliharaanLainDto>();
                var temp = param as ObservableCollection<MasterPemeliharaanLainDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPemeliharaanLain?.FirstOrDefault(x => x.IdPemeliharaanLain == item.IdPemeliharaanLain);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPemeliharaanLain.IndexOf(dataFind);
                        MasterListGlobal.MasterPemeliharaanLain[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPemeliharaanLain.Add(item);
                    }
                }
            }

            if (name == "MasterRetribusiLain")
            {
                MasterListGlobal.MasterRetribusiLain ??= new ObservableCollection<MasterRetribusiLainDto>();
                var temp = param as ObservableCollection<MasterRetribusiLainDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterRetribusiLain?.FirstOrDefault(x => x.IdRetribusiLain == item.IdRetribusiLain);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterRetribusiLain.IndexOf(dataFind);
                        MasterListGlobal.MasterRetribusiLain[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterRetribusiLain.Add(item);
                    }
                }
            }

            if (name == "MasterMeterai")
            {
                MasterListGlobal.MasterMeterai ??= new ObservableCollection<MasterMeteraiDto>();
                var temp = param as ObservableCollection<MasterMeteraiDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterMeterai?.FirstOrDefault(x => x.ID == item.ID);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterMeterai.IndexOf(dataFind);
                        MasterListGlobal.MasterMeterai[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterMeterai.Add(item);
                    }
                }
            }

            if (name == "MasterBlok")
            {
                MasterListGlobal.MasterBlok ??= new ObservableCollection<MasterBlokDto>();
                var temp = param as ObservableCollection<MasterBlokDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterBlok?.FirstOrDefault(x => x.IdBlok == item.IdBlok);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterBlok.IndexOf(dataFind);
                        MasterListGlobal.MasterBlok[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterBlok.Add(item);
                    }
                }
            }

            if (name == "MasterMerekMeter")
            {
                MasterListGlobal.MasterMerekMeter ??= new ObservableCollection<MasterMerekMeterDto>();
                var temp = param as ObservableCollection<MasterMerekMeterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterMerekMeter?.FirstOrDefault(x => x.IdMerekMeter == item.IdMerekMeter);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterMerekMeter.IndexOf(dataFind);
                        MasterListGlobal.MasterMerekMeter[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterMerekMeter.Add(item);
                    }
                }
            }

            if (name == "MasterLoket")
            {
                MasterListGlobal.MasterLoket ??= new ObservableCollection<MasterLoketDto>();
                var temp = param as ObservableCollection<MasterLoketDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterLoket?.FirstOrDefault(x => x.IdLoket == item.IdLoket);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterLoket.IndexOf(dataFind);
                        MasterListGlobal.MasterLoket[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterLoket.Add(item);
                    }
                }
            }

            if (name == "MasterKelainan")
            {
                MasterListGlobal.MasterKelainan ??= new ObservableCollection<MasterKelainanDto>();
                var temp = param as ObservableCollection<MasterKelainanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKelainan?.FirstOrDefault(x => x.IdKelainan == item.IdKelainan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKelainan.IndexOf(dataFind);
                        MasterListGlobal.MasterKelainan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKelainan.Add(item);
                    }
                }
            }

            if (name == "MasterPetugasBaca")
            {
                MasterListGlobal.MasterPetugasBaca ??= new ObservableCollection<MasterPetugasBacaDto>();
                var temp = param as ObservableCollection<MasterPetugasBacaDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPetugasBaca?.FirstOrDefault(x => x.IdPetugasBaca == item.IdPetugasBaca);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPetugasBaca.IndexOf(dataFind);
                        MasterListGlobal.MasterPetugasBaca[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPetugasBaca.Add(item);
                    }
                }
            }

            if (name == "MasterPeriode")
            {
                MasterListGlobal.MasterPeriode ??= new ObservableCollection<MasterPeriodeDto>();
                var temp = param as ObservableCollection<MasterPeriodeDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPeriode?.FirstOrDefault(x => x.IdPeriode == item.IdPeriode);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPeriode.IndexOf(dataFind);
                        MasterListGlobal.MasterPeriode[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPeriode.Add(item);
                    }
                }
            }

            if (name == "MasterPeriodeGudang")
            {
                MasterListGlobal.MasterPeriodeGudang ??= new ObservableCollection<MasterPeriodeGudangDto>();
                var temp = param as ObservableCollection<MasterPeriodeGudangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPeriodeGudang?.FirstOrDefault(x => x.IdPeriode == item.IdPeriode);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPeriodeGudang.IndexOf(dataFind);
                        MasterListGlobal.MasterPeriodeGudang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPeriodeGudang.Add(item);
                    }
                }
            }

            if (name == "MasterPekerjaan")
            {
                MasterListGlobal.MasterPekerjaan ??= new ObservableCollection<MasterPekerjaanDto>();
                var temp = param as ObservableCollection<MasterPekerjaanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPekerjaan?.FirstOrDefault(x => x.IdPekerjaan == item.IdPekerjaan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPekerjaan.IndexOf(dataFind);
                        MasterListGlobal.MasterPekerjaan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPekerjaan.Add(item);
                    }
                }
            }

            if (name == "MasterTarifLimbah")
            {
                MasterListGlobal.MasterTarifLimbah ??= new ObservableCollection<MasterTarifLimbahDto>();
                var temp = param as ObservableCollection<MasterTarifLimbahDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterTarifLimbah?.FirstOrDefault(x => x.IdTarifLimbah == item.IdTarifLimbah);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterTarifLimbah.IndexOf(dataFind);
                        MasterListGlobal.MasterTarifLimbah[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterTarifLimbah.Add(item);
                    }
                }
            }

            if (name == "MasterTarifLltt")
            {
                MasterListGlobal.MasterTarifLltt ??= new ObservableCollection<MasterTarifLlttDto>();
                var temp = param as ObservableCollection<MasterTarifLlttDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterTarifLltt?.FirstOrDefault(x => x.IdTarifLltt == item.IdTarifLltt);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterTarifLltt.IndexOf(dataFind);
                        MasterListGlobal.MasterTarifLltt[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterTarifLltt.Add(item);
                    }
                }
            }

            if (name == "MasterSumberAir")
            {
                MasterListGlobal.MasterSumberAir ??= new ObservableCollection<MasterSumberAirDto>();
                var temp = param as ObservableCollection<MasterSumberAirDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterSumberAir?.FirstOrDefault(x => x.IdSumberAir == item.IdSumberAir);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterSumberAir.IndexOf(dataFind);
                        MasterListGlobal.MasterSumberAir[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterSumberAir.Add(item);
                    }
                }
            }

            if (name == "MasterJenisBangunan")
            {
                MasterListGlobal.MasterJenisBangunan ??= new ObservableCollection<MasterJenisBangunanDto>();
                var temp = param as ObservableCollection<MasterJenisBangunanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisBangunan?.FirstOrDefault(x => x.IdJenisBangunan == item.IdJenisBangunan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisBangunan.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisBangunan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisBangunan.Add(item);
                    }
                }
            }

            if (name == "MasterKepemilikan")
            {
                MasterListGlobal.MasterKepemilikan ??= new ObservableCollection<MasterKepemilikanDto>();
                var temp = param as ObservableCollection<MasterKepemilikanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKepemilikan?.FirstOrDefault(x => x.IdKepemilikan == item.IdKepemilikan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKepemilikan.IndexOf(dataFind);
                        MasterListGlobal.MasterKepemilikan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKepemilikan.Add(item);
                    }
                }
            }

            if (name == "MasterPeruntukan")
            {
                MasterListGlobal.MasterPeruntukan ??= new ObservableCollection<MasterPeruntukanDto>();
                var temp = param as ObservableCollection<MasterPeruntukanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPeruntukan?.FirstOrDefault(x => x.IdPeruntukan == item.IdPeruntukan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPeruntukan.IndexOf(dataFind);
                        MasterListGlobal.MasterPeruntukan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPeruntukan.Add(item);
                    }
                }
            }

            if (name == "MasterDma")
            {
                MasterListGlobal.MasterDma ??= new ObservableCollection<MasterDmaDto>();
                var temp = param as ObservableCollection<MasterDmaDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterDma?.FirstOrDefault(x => x.IdDma == item.IdDma);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterDma.IndexOf(dataFind);
                        MasterListGlobal.MasterDma[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterDma.Add(item);
                    }
                }
            }

            if (name == "MasterDmz")
            {
                MasterListGlobal.MasterDmz ??= new ObservableCollection<MasterDmzDto>();
                var temp = param as ObservableCollection<MasterDmzDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterDmz?.FirstOrDefault(x => x.IdDmz == item.IdDmz);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterDmz.IndexOf(dataFind);
                        MasterListGlobal.MasterDmz[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterDmz.Add(item);
                    }
                }
            }

            if (name == "MasterJenisNonAir")
            {
                MasterListGlobal.MasterJenisNonAir ??= new ObservableCollection<MasterJenisNonAirDto>();
                var temp = param as ObservableCollection<MasterJenisNonAirDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisNonAir?.FirstOrDefault(x => x.IdJenisNonAir == item.IdJenisNonAir);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisNonAir.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisNonAir[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisNonAir.Add(item);
                    }
                }
            }

            if (name == "MasterJenisBarang")
            {
                MasterListGlobal.MasterJenisBarang ??= new ObservableCollection<MasterJenisBarangDto>();
                var temp = param as ObservableCollection<MasterJenisBarangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisBarang?.FirstOrDefault(x => x.IdJenisBarang == item.IdJenisBarang);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisBarang.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisBarang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisBarang.Add(item);
                    }
                }
            }

            if (name == "MasterPerkiraan3")
            {
                MasterListGlobal.MasterPerkiraan3 ??= new ObservableCollection<MasterPerkiraan3Dto>();
                var temp = param as ObservableCollection<MasterPerkiraan3Dto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPerkiraan3.FirstOrDefault(x => x.IdPerkiraan3 == item.IdPerkiraan3);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPerkiraan3.IndexOf(dataFind);
                        MasterListGlobal.MasterPerkiraan3[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPerkiraan3.Add(item);
                    }
                }
            }

            if (name == "MasterPerkiraan2")
            {
                MasterListGlobal.MasterPerkiraan2 ??= new ObservableCollection<MasterPerkiraan2Dto>();
                var temp = param as ObservableCollection<MasterPerkiraan2Dto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPerkiraan2.FirstOrDefault(x => x.IdPerkiraan2 == item.IdPerkiraan2);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPerkiraan2.IndexOf(dataFind);
                        MasterListGlobal.MasterPerkiraan2[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPerkiraan2.Add(item);
                    }
                }
            }

            if (name == "MasterPerkiraan1")
            {
                MasterListGlobal.MasterPerkiraan1 ??= new ObservableCollection<MasterPerkiraan1Dto>();
                var temp = param as ObservableCollection<MasterPerkiraan1Dto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPerkiraan1.FirstOrDefault(x => x.IdPerkiraan1 == item.IdPerkiraan1);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPerkiraan1.IndexOf(dataFind);
                        MasterListGlobal.MasterPerkiraan1[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPerkiraan1.Add(item);
                    }
                }
            }

            if (name == "MasterAkunEtap")
            {
                MasterListGlobal.MasterAkunEtap ??= new ObservableCollection<MasterAkunEtapDto>();
                var temp = param as ObservableCollection<MasterAkunEtapDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterAkunEtap?.FirstOrDefault(x => x.IdAkunEtap == item.IdAkunEtap);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterAkunEtap.IndexOf(dataFind);
                        MasterListGlobal.MasterAkunEtap[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterAkunEtap.Add(item);
                    }
                }
            }

            if (name == "MasterAkunBank")
            {
                MasterListGlobal.MasterAkunBank ??= new ObservableCollection<MasterAkunBankDto>();
                var temp = param as ObservableCollection<MasterAkunBankDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterAkunBank.FirstOrDefault(x => x.IdParameter == item.IdParameter);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterAkunBank.IndexOf(dataFind);
                        MasterListGlobal.MasterAkunBank[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterAkunBank.Add(item);
                    }
                }
            }

            if (name == "MasterAlasanBatal")
            {
                MasterListGlobal.MasterAlasanBatal ??= new ObservableCollection<MasterAlasanBatalDto>();
                var temp = param as ObservableCollection<MasterAlasanBatalDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterAlasanBatal?.FirstOrDefault(x => x.IdAlasanBatal == item.IdAlasanBatal);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterAlasanBatal.IndexOf(dataFind);
                        MasterListGlobal.MasterAlasanBatal[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterAlasanBatal.Add(item);
                    }
                }
            }

            if (name == "MasterTipePendaftaranSambungan")
            {
                MasterListGlobal.MasterTipePendaftaranSambungan ??= new ObservableCollection<MasterTipePendaftaranSambunganDto>();
                var temp = param as ObservableCollection<MasterTipePendaftaranSambunganDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterTipePendaftaranSambungan?.FirstOrDefault(x => x.IdTipePendaftaranSambungan == item.IdTipePendaftaranSambungan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterTipePendaftaranSambungan.IndexOf(dataFind);
                        MasterListGlobal.MasterTipePendaftaranSambungan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterTipePendaftaranSambungan.Add(item);
                    }
                }
            }

            if (name == "MasterPegawai")
            {
                MasterListGlobal.MasterPegawai ??= new ObservableCollection<MasterPegawaiDto>();
                var temp = param as ObservableCollection<MasterPegawaiDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPegawai?.FirstOrDefault(x => x.IdPegawai == item.IdPegawai);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPegawai.IndexOf(dataFind);
                        MasterListGlobal.MasterPegawai[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPegawai.Add(item);
                    }
                }
            }

            if (name == "MasterTipePerkiraan")
            {
                MasterListGlobal.MasterTipePerkiraan ??= new ObservableCollection<MasterTipePerkiraanDto>();
                var temp = param as ObservableCollection<MasterTipePerkiraanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterTipePerkiraan?.FirstOrDefault(x => x.IdTipe == item.IdTipe);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterTipePerkiraan.IndexOf(dataFind);
                        MasterListGlobal.MasterTipePerkiraan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterTipePerkiraan.Add(item);
                    }
                }
            }

            if (name == "MasterMaterial")
            {
                MasterListGlobal.MasterMaterial ??= new ObservableCollection<MasterMaterialDto>();
                var temp = param as ObservableCollection<MasterMaterialDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterMaterial?.FirstOrDefault(x => x.IdMaterial == item.IdMaterial);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterMaterial.IndexOf(dataFind);
                        MasterListGlobal.MasterMaterial[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterMaterial.Add(item);
                    }
                }
            }

            if (name == "MasterKategoriBarangKeluar")
            {
                MasterListGlobal.MasterKategoriBarangKeluar ??= new ObservableCollection<MasterKategoriBarangKeluarDto>();
                var temp = param as ObservableCollection<MasterKategoriBarangKeluarDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKategoriBarangKeluar?.FirstOrDefault(x => x.IdKategoriBarangKeluar == item.IdKategoriBarangKeluar);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKategoriBarangKeluar.IndexOf(dataFind);
                        MasterListGlobal.MasterKategoriBarangKeluar[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKategoriBarangKeluar.Add(item);
                    }
                }
            }

            if (name == "MasterKategoriBarangMasuk")
            {
                MasterListGlobal.MasterKategoriBarangMasuk ??= new ObservableCollection<MasterKategoriBarangMasukDto>();
                var temp = param as ObservableCollection<MasterKategoriBarangMasukDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKategoriBarangMasuk.FirstOrDefault(x => x.IdKategoriBarangMasuk == item.IdKategoriBarangMasuk);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKategoriBarangMasuk.IndexOf(dataFind);
                        MasterListGlobal.MasterKategoriBarangMasuk[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKategoriBarangMasuk.Add(item);
                    }
                }
            }

            if (name == "MasterJenisPenerimaanPengeluaran")
            {
                MasterListGlobal.MasterJenisPenerimaanPengeluaran ??= new ObservableCollection<MasterJenisPenerimaanPengeluaranDto>();
                var temp = param as ObservableCollection<MasterJenisPenerimaanPengeluaranDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisPenerimaanPengeluaran?.FirstOrDefault(x => x.IdJenis == item.IdJenis);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisPenerimaanPengeluaran.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisPenerimaanPengeluaran[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisPenerimaanPengeluaran.Add(item);
                    }
                }
            }

            if (name == "MasterGudang")
            {
                MasterListGlobal.MasterGudang ??= new ObservableCollection<MasterGudangDto>();
                var temp = param as ObservableCollection<MasterGudangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterGudang?.FirstOrDefault(x => x.IdGudang == item.IdGudang);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterGudang.IndexOf(dataFind);
                        MasterListGlobal.MasterGudang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterGudang.Add(item);
                    }
                }
            }

            if (name == "MasterBagianMemintaBarang")
            {
                MasterListGlobal.MasterBagianMemintaBarang ??= new ObservableCollection<MasterBagianMemintaBarangDto>();
                var temp = param as ObservableCollection<MasterBagianMemintaBarangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterBagianMemintaBarang?.FirstOrDefault(x => x.IdBagianMemintaBarang == item.IdBagianMemintaBarang);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterBagianMemintaBarang.IndexOf(dataFind);
                        MasterListGlobal.MasterBagianMemintaBarang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterBagianMemintaBarang.Add(item);
                    }
                }
            }

            if (name == "DaftarPostingAkuntansi")
            {
                MasterListGlobal.DaftarPostingAkuntansi ??= new ObservableCollection<DaftarPostingAkuntansiDto>();
                var temp = param as ObservableCollection<DaftarPostingAkuntansiDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.DaftarPostingAkuntansi?.FirstOrDefault(x => x.IdPeriode == item.IdPeriode);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.DaftarPostingAkuntansi.IndexOf(dataFind);
                        MasterListGlobal.DaftarPostingAkuntansi[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.DaftarPostingAkuntansi.Add(item);
                    }
                }
            }

            if (name == "MasterPenyusutanAktiva")
            {
                MasterListGlobal.MasterPenyusutanAktiva ??= new ObservableCollection<MasterPenyusutanAktivaDto>();
                var temp = param as ObservableCollection<MasterPenyusutanAktivaDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPenyusutanAktiva?.FirstOrDefault(x => x.IdGolAktiva == item.IdGolAktiva);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPenyusutanAktiva.IndexOf(dataFind);
                        MasterListGlobal.MasterPenyusutanAktiva[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPenyusutanAktiva.Add(item);
                    }
                }
            }

            if (name == "MasterJenisBarang")
            {
                MasterListGlobal.MasterJenisBarang ??= new ObservableCollection<MasterJenisBarangDto>();
                var temp = param as ObservableCollection<MasterJenisBarangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisBarang?.FirstOrDefault(x => x.IdJenisBarang == item.IdJenisBarang);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisBarang.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisBarang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisBarang.Add(item);
                    }
                }
            }

            if (name == "MasterBarang")
            {
                MasterListGlobal.MasterBarang ??= new ObservableCollection<MasterBarangDto>();
                var temp = param as ObservableCollection<MasterBarangDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterBarang?.FirstOrDefault(x => x.IdBarang == item.IdBarang);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterBarang.IndexOf(dataFind);
                        MasterListGlobal.MasterBarang[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterBarang.Add(item);
                    }
                }
            }

            if (name == "MasterPaketRab")
            {
                MasterListGlobal.MasterPaketRab ??= new ObservableCollection<MasterPaketDto>();
                var temp = param as ObservableCollection<MasterPaketDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterPaketRab?.FirstOrDefault(x => x.IdPaket == item.IdPaket);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPaketRab.IndexOf(dataFind);
                        MasterListGlobal.MasterPaketRab[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPaketRab.Add(item);
                    }
                }
            }

            if (name == "MasterJenisGantiMeter")
            {
                MasterListGlobal.MasterJenisGantiMeter ??= new ObservableCollection<MasterJenisGantiMeterDto>();
                var temp = param as ObservableCollection<MasterJenisGantiMeterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisGantiMeter?.FirstOrDefault(x => x.IdJenisGantiMeter == item.IdJenisGantiMeter);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisGantiMeter.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisGantiMeter[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisGantiMeter.Add(item);
                    }
                }
            }

            if (name == "MasterTarifTangki")
            {
                MasterListGlobal.MasterTarifTangki ??= new ObservableCollection<MasterTarifTangkiDto>();
                var temp = param as ObservableCollection<MasterTarifTangkiDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterTarifTangki?.FirstOrDefault(x => x.IdTarifTangki == item.IdTarifTangki);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterTarifTangki.IndexOf(dataFind);
                        MasterListGlobal.MasterTarifTangki[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterTarifTangki.Add(item);
                    }
                }
            }

            if (name == "MasterPeriodeAkuntansi")
            {
                MasterListGlobal.MasterPeriodeAkuntansi ??= new ObservableCollection<MasterPeriodeAkuntansiDto>();
                var temp = param as ObservableCollection<MasterPeriodeAkuntansiDto>;
                foreach (var item in temp.OrderByDescending(x => x.KodePeriode))
                {
                    var dataFind = MasterListGlobal.MasterPeriodeAkuntansi?.FirstOrDefault(x => x.IdPeriode == item.IdPeriode);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPeriodeAkuntansi.IndexOf(dataFind);
                        MasterListGlobal.MasterPeriodeAkuntansi[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPeriodeAkuntansi.Add(item);
                    }
                }
            }

            if (name == "MasterPeriodeAkuntansiTriwulan")
            {
                MasterListGlobal.MasterPeriodeAkuntansiTriwulan ??= new ObservableCollection<dynamic>();
                var temp = param as ObservableCollection<dynamic>;
                foreach (var item in temp.OrderByDescending(x => x.Tahun))
                {
                    var dataFind = MasterListGlobal.MasterPeriodeAkuntansiTriwulan?.FirstOrDefault(x => x.IdTriwulan == item.IdTriwulan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterPeriodeAkuntansiTriwulan.IndexOf(dataFind);
                        MasterListGlobal.MasterPeriodeAkuntansiTriwulan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterPeriodeAkuntansiTriwulan.Add(item);
                    }
                }
            }

            if (name == "AnggaranLabaRugiMaster")
            {
                MasterListGlobal.AnggaranLabaRugiMaster ??= new ObservableCollection<AnggaranLabaRugiMasterDto>();
                var temp = param as ObservableCollection<AnggaranLabaRugiMasterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.AnggaranLabaRugiMaster?.FirstOrDefault(x => x.IdGroupLabaRugi == item.IdGroupLabaRugi);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.AnggaranLabaRugiMaster.IndexOf(dataFind);
                        MasterListGlobal.AnggaranLabaRugiMaster[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.AnggaranLabaRugiMaster.Add(item);
                    }
                }
            }

            if (name == "MasterKeperluan")
            {
                MasterListGlobal.MasterKeperluan ??= new ObservableCollection<MasterKeperluanDto>();
                var temp = param as ObservableCollection<MasterKeperluanDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterKeperluan?.FirstOrDefault(x => x.IdKeperluan == item.IdKeperluan);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterKeperluan.IndexOf(dataFind);
                        MasterListGlobal.MasterKeperluan[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterKeperluan.Add(item);
                    }
                }
            }

            if (name == "MasterParameterAkuntansi")
            {
                MasterListGlobal.MasterParameterAkuntansi ??= new ObservableCollection<MasterParameterAkuntansiDto>();
                var temp = param as ObservableCollection<MasterParameterAkuntansiDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterParameterAkuntansi?.FirstOrDefault(x => x.IdParameter == item.IdParameter);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterParameterAkuntansi.IndexOf(dataFind);
                        MasterListGlobal.MasterParameterAkuntansi[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterParameterAkuntansi.Add(item);
                    }
                }
            }

            if (name == "DaftarPenerimaanKas")
            {
                MasterListGlobal.DaftarPenerimaanKas ??= new ObservableCollection<DaftarPenerimaanKasDto>();
                var temp = param as ObservableCollection<DaftarPenerimaanKasDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.DaftarPenerimaanKas?.FirstOrDefault(x => x.IdDaftarPenerimaanKas == item.IdDaftarPenerimaanKas);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.DaftarPenerimaanKas.IndexOf(dataFind);
                        MasterListGlobal.DaftarPenerimaanKas[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.DaftarPenerimaanKas.Add(item);
                    }
                }
            }

            if (name == "MasterBank")
            {
                MasterListGlobal.MasterBank ??= new ObservableCollection<MasterBankDto>();
                var temp = param as ObservableCollection<MasterBankDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterBank?.FirstOrDefault(x => x.IdBank == item.IdBank);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterBank.IndexOf(dataFind);
                        MasterListGlobal.MasterBank[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterBank.Add(item);
                    }
                }
            }

            if (name == "ReportFilterCustom")
            {
                MasterListGlobal.ReportFilterCustom ??= new ObservableCollection<ReportFilterCustomDto>();
                var temp = param as ObservableCollection<ReportFilterCustomDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.ReportFilterCustom?.FirstOrDefault(x => x.IdFilterCustom == item.IdFilterCustom);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.ReportFilterCustom.IndexOf(dataFind);
                        MasterListGlobal.ReportFilterCustom[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.ReportFilterCustom.Add(item);
                    }
                }
            }

            if (name == "ArusKasMaster")
            {
                MasterListGlobal.ArusKasMaster ??= new ObservableCollection<ArusKasTidakLangsungMasterDto>();
                var temp = param as ObservableCollection<ArusKasTidakLangsungMasterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.ArusKasMaster?.FirstOrDefault(x => x.IdArusKasMaster == item.IdArusKasMaster);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.ArusKasMaster.IndexOf(dataFind);
                        MasterListGlobal.ArusKasMaster[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.ArusKasMaster.Add(item);
                    }
                }
            }

            if (name == "NeracaMaster")
            {
                MasterListGlobal.NeracaMaster ??= new ObservableCollection<NeracaMasterDto>();
                var temp = param as ObservableCollection<NeracaMasterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.NeracaMaster?.FirstOrDefault(x => x.IdNeracaMaster == item.IdNeracaMaster);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.NeracaMaster.IndexOf(dataFind);
                        MasterListGlobal.NeracaMaster[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.NeracaMaster.Add(item);
                    }
                }
            }

            if (name == "EkuitasMaster")
            {
                MasterListGlobal.EkuitasMaster ??= new ObservableCollection<EkuitasMasterDto>();
                var temp = param as ObservableCollection<EkuitasMasterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.EkuitasMaster?.FirstOrDefault(x => x.IdEkuitasMaster == item.IdEkuitasMaster);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.EkuitasMaster.IndexOf(dataFind);
                        MasterListGlobal.EkuitasMaster[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.EkuitasMaster.Add(item);
                    }
                }
            }

            if (name == "PerputaranUangMaster")
            {
                MasterListGlobal.PerputaranUangMaster ??= new ObservableCollection<LaporanPerputaranUangMasterDto>();
                var temp = param as ObservableCollection<LaporanPerputaranUangMasterDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.PerputaranUangMaster?.FirstOrDefault(x => x.IdPerputaranUangMaster == item.IdPerputaranUangMaster);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.PerputaranUangMaster.IndexOf(dataFind);
                        MasterListGlobal.PerputaranUangMaster[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.PerputaranUangMaster.Add(item);
                    }
                }
            }

            if (name == "MasterHarianKas")
            {
                MasterListGlobal.MasterHarianKas ??= new ObservableCollection<MasterHarianKasDto>();
                var temp = param as ObservableCollection<MasterHarianKasDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterHarianKas?.FirstOrDefault(x => x.IdLhkMaster == item.IdLhkMaster);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterHarianKas.IndexOf(dataFind);
                        MasterListGlobal.MasterHarianKas[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterHarianKas.Add(item);
                    }
                }
            }

            if (name == "MasterJenisVoucher")
            {
                MasterListGlobal.MasterJenisVoucher ??= new ObservableCollection<MasterJenisVoucherDto>();
                var temp = param as ObservableCollection<MasterJenisVoucherDto>;
                foreach (var item in temp)
                {
                    var dataFind = MasterListGlobal.MasterJenisVoucher?.FirstOrDefault(x => x.IdJenisVoucher == item.IdJenisVoucher);
                    if (dataFind != null)
                    {
                        var idx = MasterListGlobal.MasterJenisVoucher.IndexOf(dataFind);
                        MasterListGlobal.MasterJenisVoucher[idx] = item;
                    }
                    else
                    {
                        MasterListGlobal.MasterJenisVoucher.Add(item);
                    }
                }
            }
        }
    }
}
