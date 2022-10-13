using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Loket.Tagihan;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TagihanKolektifAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(TagihanKolektifAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.KolektifList = MasterListGlobal.MasterKolektif;
            _viewModel.Parent.IsFullPage = false;
            _viewModel.UpdatePage("SearchPage", null);
            _viewModel.PeriodeList = new ObservableCollection<MasterPeriodeDto>();
            await Task.FromResult(_isTest);
        }
    }
}
