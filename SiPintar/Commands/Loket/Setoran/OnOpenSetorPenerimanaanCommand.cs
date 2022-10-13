using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;
using SiPintar.Views.Loket.Setoran;

namespace SiPintar.Commands.Loket.Setoran
{
    public class OnOpenSetorPenerimanaanCommand : AsyncCommandBase
    {
        private readonly SetoranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSetorPenerimanaanCommand(SetoranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.ResiFormPath = null;
            _viewModel.FormSetoran = new ParamSetoranLoketDto()
            {
                IdPdam = _viewModel.SelectedData.IdPdam,
                IdSetoranLoket = _viewModel.SelectedData.IdSetoranLoket,
                IdLoket = _viewModel.SelectedData.IdLoket,
                IdUser = _viewModel.SelectedData.IdUser,
                Keterangan = _viewModel.SelectedData.Keterangan,
                Penerimaan = _viewModel.SelectedData.Penerimaan,
                TglPenerimaan = _viewModel.SelectedData.TglPenerimaan,
                TglSetor = System.DateTime.Now,
            };


            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "LoketRootDialog", GetInstance);
            _ = await Task.FromResult(_isTest);
        }

        private object GetInstance() => new FormSetoranDialog(_viewModel);
    }
}
