using System;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.CariTagihan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly CariTagihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(CariTagihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEmpty = true;
            _viewModel.KolektifForm = null;
            _viewModel.JenisNonAirForm = null;
            _viewModel.ParentPage.TanggalTransaksi = DateTime.Now;
            _viewModel.KolektifList = MasterListGlobal.MasterKolektif;
            _viewModel.JenisNonAirList = MasterListGlobal.MasterJenisNonAir;
            _viewModel.ParentPage.IsNamaRoleAdmin = AppSetting.IsNamaRoleAdmin;
            await Task.FromResult(_isTest);
        }
    }
}
