using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnOpenVerifikasiCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenVerifikasiCommand(UsulanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.IsLoading || _viewModel.SelectedData == null)
                return;

            await ShowDialogAsync(parameter as string);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowDialogAsync(string param)
        {
            if (!_isTest)
            {
                _viewModel.FotoBukti1Uri = null;
                _viewModel.FotoBukti2Uri = null;
                _viewModel.FotoBukti3Uri = null;
                _viewModel.IsFotoBukti1FormChecked = false;
                _viewModel.IsFotoBukti2FormChecked = false;
                _viewModel.IsFotoBukti3FormChecked = false;

                await GetFotoAsync();

                if (param == "lapangan")
                {
                    _ = DialogHost.Show(new VerifikasiView(_viewModel), "HublangRootDialog");
                }
                else if (param == "pusat")
                {
                    _ = DialogHost.Show(new VerifikasiView(_viewModel), "BillingRootDialog");
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFotoAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.IdPermohonan == null)
                    return;

                var param = new Dictionary<string, dynamic>
                {
                    { "IdPermohonan", _viewModel.SelectedData.IdPermohonan },
                    { "IdPeriode", _viewModel.SelectedData.IdPeriode }
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/permohonan-koreksi-rekening-air-link-foto", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<NamaFotoDto>>();

                        if (data != null)
                        {
                            _viewModel.FotoBukti1Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti1));
                            _viewModel.FotoBukti2Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti2));
                            _viewModel.FotoBukti3Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti3));

                            _viewModel.IsFotoBukti1FormChecked = _viewModel.FotoBukti1Uri != null;
                            _viewModel.IsFotoBukti2FormChecked = _viewModel.FotoBukti2Uri != null;
                            _viewModel.IsFotoBukti3FormChecked = _viewModel.FotoBukti3Uri != null;
                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
            }
        }
    }
}
