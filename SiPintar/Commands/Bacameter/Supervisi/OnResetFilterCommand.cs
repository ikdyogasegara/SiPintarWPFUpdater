using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsSemuaStatusFilter = true;
            _viewModel.IsBelumStatusFilter = false;
            _viewModel.IsSudahStatusFilter = false;
            _viewModel.JenisVerifikasiFilter = _viewModel.JenisVerifikasiList[0];

            _viewModel.IsRentangWaktuChecked = false;
            _viewModel.IsKelainanChecked = false;
            _viewModel.IsJumlahPakaiChecked = false;
            _viewModel.IsStanAwalChecked = false;
            _viewModel.IsStanAkhirChecked = false;
            _viewModel.IsJenisPipaChecked = false;
            _viewModel.IsHitungAirLimbahChecked = false;
            _viewModel.IsSegelRusakChecked = false;
            _viewModel.IsKodeRayonChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsKodeGolonganChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsKodeKelurahanChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsKodeKecamatanChecked = false;
            _viewModel.IsKecamatanChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsLuasRumahChecked = false;
            _viewModel.IsKeakuratanChecked = false;
            _viewModel.IsPetugasBacaChecked = false;
            _viewModel.IsNamaPelangganChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsLainnyaChecked = false;

            _viewModel.RekeningFilter = new ParamRekeningAirFilterDto()
            {
                FlagBaca = null,
                Taksasi = null,
                FlagKoreksi = null,
                FlagVerifikasi = null,
                FlagPublish = null,
                FlagUpload = null,
                AdaFotoMeter = null,
                AdaFotoRumah = null,
                AdaVideo = null,
                FlagMinimumPakai = null
            };

            if (!_isTest)
                _viewModel.GetListCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
