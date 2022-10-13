using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Setoran
{
    public class GetSetoranCommand : AsyncCommandBase
    {
        private readonly SetoranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public GetSetoranCommand(SetoranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedPeriode == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "LoketRootDialog", "Setoran Loket", "Tidak ada periode yang dipilih!", "warning", moduleName: "loket");
            }
            else
            {
                _viewModel.IsLoading = true;
                _viewModel.DataList = null;
                var year = _viewModel.SelectedPeriode.Tahun.ToString();
                string month = _viewModel.SelectedPeriode.KodePeriode.ToString().Replace(year, "");
                var Param = new Dictionary<string, dynamic>();
                Param.Add("TglPenerimaanMulai", new DateTime(int.Parse(year), int.Parse(month), 1));
                Param.Add("TglPenerimaanSampaiDengan", new DateTime(int.Parse(year), int.Parse(month), DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));

                var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/Setoran-loket", Param));
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status && Result.Data != null)
                    {
                        _viewModel.DataList = Result.Data.ToObject<ObservableCollection<SetoranLoketWpf>>();
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "loket");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "loket");

                _viewModel.DataList ??= new ObservableCollection<SetoranLoketWpf>();
                _viewModel.JumlahLpp = _viewModel.DataList.Sum(x => x.Penerimaan);
                _viewModel.JumlahSetor = _viewModel.DataList.Sum(x => x.Setoran);
                _viewModel.JumlahSelisih = _viewModel.DataList.Sum(x => x.Selisih);

                _viewModel.IsEmpty = !_viewModel.DataList.Any();
                _viewModel.IsLoading = false;
            }
            _ = await Task.FromResult(_isTest);
        }


    }
}
