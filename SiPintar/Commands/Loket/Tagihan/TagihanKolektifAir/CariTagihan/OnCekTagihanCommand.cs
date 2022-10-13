using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Tagihan;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.CariTagihan
{
    public class OnCekTagihanCommand : AsyncCommandBase
    {
        private readonly CariTagihanViewModel _viewModel;
        private readonly bool _isTest;

        public OnCekTagihanCommand(CariTagihanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            SetSelectedData();

            if (_viewModel.Parent.ListSelectedPelangganAir != null && _viewModel.Parent.ListSelectedPelangganAir.Count > 0)
            {
                _viewModel.Parent.UpdatePage("DetailTagihanByIdPelangganAir", _viewModel.ParentPage.TanggalTransaksi);
            }

            if (_viewModel.Parent.ListSelectedNonAir != null && _viewModel.Parent.ListSelectedNonAir.Count > 0)
            {
                _viewModel.Parent.UpdatePage("DetailTagihanByIdNonAir", _viewModel.ParentPage.TanggalTransaksi);
            }
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void SetSelectedData()
        {
            if (!_isTest)
            {
                if (_viewModel.ListSelectedPelangganAir != null && _viewModel.ListSelectedPelangganAir.Count > 0)
                {
                    var selectedPelangganAir = new ObservableCollection<MasterPelangganAirWpf>();
                    if (_viewModel.ListSelectedPelangganAir != null)
                    {
                        foreach (var item in _viewModel.ListSelectedPelangganAir)
                        {
                            if (item.IsSelected == true && selectedPelangganAir.FirstOrDefault(i => i.IdPelangganAir == item.IdPelangganAir) == null)
                                selectedPelangganAir.Add(item);
                        }
                    }

                    _viewModel.Parent.ListSelectedPelangganAir = selectedPelangganAir;
                }

                if (_viewModel.ListSelectedNonAir != null && _viewModel.ListSelectedNonAir.Count > 0)
                {
                    var selectedNonAir = new ObservableCollection<RekeningNonAirWpf>();
                    if (_viewModel.ListSelectedNonAir != null)
                    {
                        foreach (var item in _viewModel.ListSelectedNonAir)
                        {
                            if (item.IsSelected == true && selectedNonAir.FirstOrDefault(i => i.IdNonAir == item.IdNonAir) == null)
                                selectedNonAir.Add(item);
                        }
                    }

                    _viewModel.Parent.ListSelectedNonAir = selectedNonAir;
                }
            }
        }
    }
}
