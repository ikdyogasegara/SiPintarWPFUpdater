using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;
namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(PengeluaranLainnyaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PeriodeTutupBukuList.FirstOrDefault(x => x.IdPeriode == _viewModel.PengeluaranLainnyaForm.IdPeriode) != null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialogForm",
                    "Tambah Data Posting Pengeluaran Lainnya",
                    $"Maaf data tidak dapat ditambah, karena periode {_viewModel.PengeluaranLainnyaForm.NamaPeriode} sudah mengalami Proses POSTING",
                    "error",
                    "OK",
                    false,
                    "akuntansi");
                return;
            }

            _viewModel.IsLoadingForm = true;

            var type = typeof(ParamDaftarBiayaKasDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.PengeluaranLainnyaForm));
                }
            }

            RestApiResponse response;
            if (_viewModel.IsAdd)
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/daftar-biaya-kas", body);
            else
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/daftar-biaya-kas", null!, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    if (!_viewModel.IsAdd)
                    {
                        var selectedData = _viewModel.SelectedData;

                        selectedData.IdPeriode = body["IdPeriode"];
                        selectedData.NomorTransaksi = body["NomorTransaksi"];
                        selectedData.IdWilayah = body["IdWilayah"];
                        selectedData.KodeWilayahWpf = _viewModel.WilayahList.FirstOrDefault(x => x.IdWilayah == selectedData?.IdWilayah).KodeWilayah;
                        selectedData.NamaWilayahWpf = _viewModel.WilayahList.FirstOrDefault(x => x.IdWilayah == selectedData.IdWilayah).NamaWilayah;
                        selectedData.IdPerkiraan3Debet = body["IdPerkiraan3Debet"];
                        selectedData.KodePerkiraan3DebetWpf = _viewModel.Perkiraan3List.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Debet).KodePerkiraan3;
                        selectedData.NamaPerkiraan3DebetWpf = _viewModel.Perkiraan3List.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Debet).NamaPerkiraan3;
                        selectedData.IdPerkiraan3Kredit = body["IdPerkiraan3Kredit"];
                        selectedData.KodePerkiraan3KreditWpf = _viewModel.Perkiraan3List.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Kredit).KodePerkiraan3;
                        selectedData.NamaPerkiraan3KreditWpf = _viewModel.Perkiraan3List.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Kredit).NamaPerkiraan3;
                        selectedData.UraianWpf = body["Uraian"];
                        selectedData.WaktuInputWpf = body["WaktuInput"];
                        selectedData.JumlahNominalWpf = body["JumlahNominal"];
                        selectedData.Kategori = body["Kategori"];
                        selectedData.SumberData = body["SumberData"];
                    }
                    else
                        _viewModel.OnRefreshCommand.Execute(null);

                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg!, "akuntansi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg!, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message!, "akuntansi");

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
