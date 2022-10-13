using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnCalCulationKoreksiCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnCalCulationKoreksiCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesKoreksi == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            _viewModel.IsLoadingKoreksi = true;
            _viewModel.IsEmpty = false;

            if (_viewModel.SelectedDataPeriode != null)
            {
                _viewModel.HitungKoreksi = new RekeningAirDto();
                _viewModel.HitungKoreksiDetail = new RekeningAirDetailDto();

                var golongan = _viewModel.MasterGolonganList.FirstOrDefault(c => c.IdGolongan == _viewModel.SelectedData.IdGolongan);
                var diameter = _viewModel.MasterDiameterList.FirstOrDefault(c => c.IdDiameter == _viewModel.SelectedData.IdDiameter);
                var administrasiLain = _viewModel.MasterAdministrasiLainList.FirstOrDefault(c => c.IdAdministrasiLain == _viewModel.SelectedData.IdAdministrasiLain);
                var pemeliharaanLain = _viewModel.MasterPemeliharaanLainList.FirstOrDefault(c => c.IdPemeliharaanLain == _viewModel.SelectedData.IdPemeliharaanLain);
                var retribusiLain = _viewModel.MasterRetribusiLainList.FirstOrDefault(c => c.IdRetribusiLain == _viewModel.SelectedData.IdRetribusiLain);
                var meterai = _viewModel.MasterMeteraiList.Where(c => c.KodePeriodeMulaiBerlaku <= _viewModel.SelectedData.KodePeriode).OrderByDescending(c => c.KodePeriodeMulaiBerlaku).FirstOrDefault();
                var status = _viewModel.MasterStatusList.FirstOrDefault(c => c.IdStatus == _viewModel.SelectedData.IdStatus);

                var result = KalkulasiRekeningAirHelper.Hitung(
                    _viewModel.KoreksiForm.Pakai,
                    golongan,
                    diameter,
                    administrasiLain,
                    pemeliharaanLain,
                    retribusiLain,
                    meterai,
                    status,
                    _viewModel.SelectedData.IdFlag,
                    _viewModel.SelectedData.TglMulaiDenda1,
                    _viewModel.SelectedData.TglMulaiDenda2,
                    _viewModel.SelectedData.TglMulaiDenda3,
                    _viewModel.SelectedData.TglMulaiDenda4,
                    _viewModel.SelectedData.TglMulaiDendaPerBulan);

                _viewModel.HitungKoreksi = new RekeningAirDto()
                {
                    Pakai = result.Pakai,
                    PakaiKalkulasi = result.PakaiKalkulasi,
                    BiayaPemakaian = result.BiayaPemakaian,
                    Administrasi = result.Administrasi,
                    Pemeliharaan = result.Pemeliharaan,
                    Retribusi = result.Retribusi,
                    Pelayanan = result.Pelayanan,
                    AirLimbah = result.AirLimbah,
                    DendaPakai0 = result.DendaPakai0,
                    AdministrasiLain = result.AdministrasiLain,
                    PemeliharaanLain = result.PemeliharaanLain,
                    RetribusiLain = result.RetribusiLain,
                    Ppn = result.Ppn,
                    Meterai = result.Meterai,
                    Rekair = result.Rekair,
                    Denda = result.Denda,
                    Total = result.Total,
                    FlagMinimumPakai = result.FlagMinimumPakai,
                };

                _viewModel.HitungKoreksiDetail = new RekeningAirDetailDto()
                {
                    Blok1 = result.Blok1,
                    Blok2 = result.Blok2,
                    Blok3 = result.Blok3,
                    Blok4 = result.Blok4,
                    Blok5 = result.Blok5,
                    Prog1 = result.Prog1,
                    Prog2 = result.Prog2,
                    Prog3 = result.Prog3,
                    Prog4 = result.Prog4,
                    Prog5 = result.Prog5,
                };
            }

            _viewModel.IsLoadingKoreksi = false;
            await Task.FromResult(_isTest);
        }
    }
}
