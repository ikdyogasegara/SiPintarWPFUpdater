using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly InteraksiViewModel _viewModel;

        public OnResetFilterCommand(InteraksiViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            _viewModel.IsWilayahChecked = false;
            _viewModel.IsGolonganChecked = false;

            _viewModel.FilterWilayah = null;
            _viewModel.FilterGolongan = null;

            #region InPersediaan
            _viewModel.IsJenisChecked = false;
            _viewModel.IsKeperluanChecked = false;
            _viewModel.IsDebetChecked = false;
            _viewModel.IsKreditChecked = false;
            _viewModel.FilterJenis = new();
            _viewModel.FilterKeperluan = new();
            _viewModel.FilterDebet = new();
            _viewModel.FilterKredit = new();
            #endregion

            #region InPenyusutan
            _viewModel.IsAkunAktivaChecked = false;
            _viewModel.IsAkunAkmPenyusutanChecked = false;
            _viewModel.IsAkunBiayaChecked = false;
            _viewModel.FilterAkunAktiva = new();
            _viewModel.FilterAkunAkmPenyusutan = new();
            _viewModel.FilterAkunBiaya = new();
            #endregion

            _viewModel.OnRefreshCommand.Execute(null);

            await Task.FromResult(true);

        }
    }
}
