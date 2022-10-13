using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnHitungRabCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly bool _isTest;

        public OnHitungRabCommand(PermohonanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Hitung();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void Hitung()
        {
            if (!_isTest)
            {
                #region pipa persil
                var tempPipaPersil = _viewModel.DetailRabPipaPersil;
                _viewModel.SummaryPipaPersil.BiayaBahan = tempPipaPersil.Where(c => c.Tipe == "Material").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.BiayaPemasangan = tempPipaPersil.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "pemasangan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.BiayaPendaftaran = tempPipaPersil.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "pendaftaran").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.JaminanLangganan = tempPipaPersil.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "jaminan langganan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.Perencanaan = tempPipaPersil.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "perencanaan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.Materai = tempPipaPersil.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "meterai").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.Lainnya = tempPipaPersil.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "lainnya").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersil.Ppn = tempPipaPersil.Where(c => c.Tipe == "Material" || c.Tipe == "Ongkos").Sum(c => c.Ppn) ?? 0;
                _viewModel.SummaryPipaPersil.JasaDariBahan = tempPipaPersil.Where(c => c.Tipe == "Material").Sum(c => c.JasaDariBahan) ?? 0;
                _viewModel.SummaryPipaPersil.Keuntungan = tempPipaPersil.Where(c => c.Tipe == "Material" || c.Tipe == "Ongkos").Sum(c => c.Keuntungan) ?? 0;
                _viewModel.SummaryPipaPersil.Total = tempPipaPersil.Where(c => c.Tipe == "Material" || c.Tipe == "Ongkos").Sum(c => c.Total) ?? 0;

                _viewModel.SummaryPipaPersilTambahan.BiayaBahan = tempPipaPersil.Where(c => c.Tipe == "Material Tambahan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.BiayaPemasangan = tempPipaPersil.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "pemasangan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.BiayaPendaftaran = tempPipaPersil.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "pendaftaran").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.JaminanLangganan = tempPipaPersil.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "jaminan langganan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.Perencanaan = tempPipaPersil.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "perencanaan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.Materai = tempPipaPersil.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "meterai").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.Lainnya = tempPipaPersil.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "lainnya").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.Ppn = tempPipaPersil.Where(c => c.Tipe == "Material Tambahan" || c.Tipe == "Ongkos Tambahan").Sum(c => c.Ppn) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.JasaDariBahan = tempPipaPersil.Where(c => c.Tipe == "Material Tambahan").Sum(c => c.JasaDariBahan) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.Keuntungan = tempPipaPersil.Where(c => c.Tipe == "Material Tambahan" || c.Tipe == "Ongkos Tambahan").Sum(c => c.Keuntungan) ?? 0;
                _viewModel.SummaryPipaPersilTambahan.Total = tempPipaPersil.Where(c => c.Tipe == "Material Tambahan" || c.Tipe == "Ongkos Tambahan").Sum(c => c.Total) ?? 0;

                _viewModel.RekapRabPipaPersil.SubTotal = _viewModel.SummaryPipaPersil.Total + _viewModel.SummaryPipaPersilTambahan.Total;
                _viewModel.RekapRabPipaPersil.HargaNetMaterial = _viewModel.SelectedPaketRabPipaPersil?.HargaNetMaterial ?? 0;
                _viewModel.RekapRabPipaPersil.HargaNetOngkos = _viewModel.SelectedPaketRabPipaPersil?.HargaNetOngkos ?? 0;
                _viewModel.RekapRabPipaPersil.HargaNetPaket = _viewModel.SelectedPaketRabPipaPersil?.HargaNetPaket ?? 0;
                _viewModel.RekapRabPipaPersil.Penyesuaian = _viewModel.RekapRabPipaPersil.HargaNetPaket == 0 ? 0 : _viewModel.RekapRabPipaPersil.HargaNetPaket - _viewModel.RekapRabPipaPersil.SubTotal;
                _viewModel.RekapRabPipaPersil.TotalRab = _viewModel.RekapRabPipaPersil.HargaNetPaket == 0 ? _viewModel.RekapRabPipaPersil.SubTotal : _viewModel.RekapRabPipaPersil.HargaNetPaket;
                #endregion

                #region pipa distribusi
                var tempPipaDistribusi = _viewModel.DetailRabPipaDistribusi;
                _viewModel.SummaryPipaDistribusi.BiayaBahan = tempPipaDistribusi.Where(c => c.Tipe == "Material").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.BiayaPemasangan = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "pemasangan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.BiayaPendaftaran = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "pendaftaran").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.JaminanLangganan = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "jaminan langganan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.Perencanaan = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "perencanaan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.Materai = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "meterai").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.Lainnya = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos" && c.PostBiaya == "lainnya").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusi.Ppn = tempPipaDistribusi.Where(c => c.Tipe == "Material" || c.Tipe == "Ongkos").Sum(c => c.Ppn) ?? 0;
                _viewModel.SummaryPipaDistribusi.JasaDariBahan = tempPipaDistribusi.Where(c => c.Tipe == "Material").Sum(c => c.JasaDariBahan) ?? 0;
                _viewModel.SummaryPipaDistribusi.Keuntungan = tempPipaDistribusi.Where(c => c.Tipe == "Material" || c.Tipe == "Ongkos").Sum(c => c.Keuntungan) ?? 0;
                _viewModel.SummaryPipaDistribusi.Total = tempPipaDistribusi.Where(c => c.Tipe == "Material" || c.Tipe == "Ongkos").Sum(c => c.Total) ?? 0;

                _viewModel.SummaryPipaDistribusiTambahan.BiayaBahan = tempPipaDistribusi.Where(c => c.Tipe == "Material Tambahan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.BiayaPemasangan = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "pemasangan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.BiayaPendaftaran = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "pendaftaran").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.JaminanLangganan = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "jaminan langganan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.Perencanaan = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "perencanaan").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.Materai = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "meterai").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.Lainnya = tempPipaDistribusi.Where(c => c.Tipe == "Ongkos Tambahan" && c.PostBiaya == "lainnya").Sum(c => c.Jumlah) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.Ppn = tempPipaDistribusi.Where(c => c.Tipe == "Material Tambahan" || c.Tipe == "Ongkos Tambahan").Sum(c => c.Ppn) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.JasaDariBahan = tempPipaDistribusi.Where(c => c.Tipe == "Material Tambahan").Sum(c => c.JasaDariBahan) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.Keuntungan = tempPipaDistribusi.Where(c => c.Tipe == "Material Tambahan" || c.Tipe == "Ongkos Tambahan").Sum(c => c.Keuntungan) ?? 0;
                _viewModel.SummaryPipaDistribusiTambahan.Total = tempPipaDistribusi.Where(c => c.Tipe == "Material Tambahan" || c.Tipe == "Ongkos Tambahan").Sum(c => c.Total) ?? 0;

                _viewModel.RekapRabPipaDistribusi.SubTotal = _viewModel.SummaryPipaDistribusi.Total + _viewModel.SummaryPipaDistribusiTambahan.Total;
                _viewModel.RekapRabPipaDistribusi.HargaNetMaterial = _viewModel.SelectedPaketRabPipaDistribusi?.HargaNetMaterial ?? 0;
                _viewModel.RekapRabPipaDistribusi.HargaNetOngkos = _viewModel.SelectedPaketRabPipaDistribusi?.HargaNetOngkos ?? 0;
                _viewModel.RekapRabPipaDistribusi.HargaNetPaket = _viewModel.SelectedPaketRabPipaDistribusi?.HargaNetPaket ?? 0;
                _viewModel.RekapRabPipaDistribusi.Penyesuaian = _viewModel.RekapRabPipaDistribusi.HargaNetPaket == 0 ? 0 : _viewModel.RekapRabPipaDistribusi.HargaNetPaket - _viewModel.RekapRabPipaDistribusi.SubTotal;
                _viewModel.RekapRabPipaDistribusi.TotalRab = _viewModel.RekapRabPipaDistribusi.HargaNetPaket == 0 ? _viewModel.RekapRabPipaDistribusi.SubTotal : _viewModel.RekapRabPipaDistribusi.HargaNetPaket;
                #endregion


                #region Rekap RAB
                _viewModel.SummaryTotal.BiayaBahan = _viewModel.SummaryPipaPersil.BiayaBahan + _viewModel.SummaryPipaDistribusi.BiayaBahan;
                _viewModel.SummaryTotal.BiayaPemasangan = _viewModel.SummaryPipaPersil.BiayaPemasangan + _viewModel.SummaryPipaDistribusi.BiayaPemasangan;
                _viewModel.SummaryTotal.BiayaPendaftaran = _viewModel.SummaryPipaPersil.BiayaPendaftaran + _viewModel.SummaryPipaDistribusi.BiayaPendaftaran;
                _viewModel.SummaryTotal.JaminanLangganan = _viewModel.SummaryPipaPersil.JaminanLangganan + _viewModel.SummaryPipaDistribusi.JaminanLangganan;
                _viewModel.SummaryTotal.Perencanaan = _viewModel.SummaryPipaPersil.Perencanaan + _viewModel.SummaryPipaDistribusi.Perencanaan;
                _viewModel.SummaryTotal.Materai = _viewModel.SummaryPipaPersil.Materai + _viewModel.SummaryPipaDistribusi.Materai;
                _viewModel.SummaryTotal.Lainnya = _viewModel.SummaryPipaPersil.Lainnya + _viewModel.SummaryPipaDistribusi.Lainnya;
                _viewModel.SummaryTotal.Ppn = _viewModel.SummaryPipaPersil.Ppn + _viewModel.SummaryPipaDistribusi.Ppn;
                _viewModel.SummaryTotal.JasaDariBahan = _viewModel.SummaryPipaPersil.JasaDariBahan + _viewModel.SummaryPipaDistribusi.JasaDariBahan;
                _viewModel.SummaryTotal.Keuntungan = _viewModel.SummaryPipaPersil.Keuntungan + _viewModel.SummaryPipaDistribusi.Keuntungan;
                _viewModel.SummaryTotal.Total = _viewModel.SummaryPipaPersil.Total + _viewModel.SummaryPipaDistribusi.Total;

                _viewModel.RekapRabTotal.SubTotal = _viewModel.RekapRabPipaPersil.SubTotal + _viewModel.RekapRabPipaDistribusi.SubTotal;
                _viewModel.RekapRabTotal.HargaNetMaterial = _viewModel.RekapRabPipaPersil.HargaNetMaterial + _viewModel.RekapRabPipaDistribusi.HargaNetMaterial;
                _viewModel.RekapRabTotal.HargaNetOngkos = _viewModel.RekapRabPipaPersil.HargaNetOngkos + _viewModel.RekapRabPipaDistribusi.HargaNetOngkos;
                _viewModel.RekapRabTotal.HargaNetPaket = _viewModel.RekapRabPipaPersil.HargaNetPaket + _viewModel.RekapRabPipaDistribusi.HargaNetPaket;
                _viewModel.RekapRabTotal.Penyesuaian = _viewModel.RekapRabPipaPersil.Penyesuaian + _viewModel.RekapRabPipaDistribusi.Penyesuaian;
                _viewModel.RekapRabTotal.TotalRab = _viewModel.RekapRabPipaPersil.TotalRab + _viewModel.RekapRabPipaDistribusi.TotalRab;
                #endregion
            }
        }
    }
}
