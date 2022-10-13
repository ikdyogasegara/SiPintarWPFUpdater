using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class OnSubmitAddSambunganBaruCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddSambunganBaruCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(
                _isTest,
                true,
                "LoketRootDialog",
                "Mohon tunggu",
                "sedang memproses tindakan...");

            _viewModel.FormSubmitAngsuranNonAir = new ParamRekeningAngsuranNonAirDto()
            {
                IdPdam = Convert.ToInt32(Application.Current.Resources["IdPdam"]),
                NoAngsuran = "0",
                IdNonAir = _viewModel.SelectedRekeningNonAir.IdNonAir,
                IdJenisNonAir = _viewModel.SelectedRekeningNonAir.IdJenisNonAir,
                Nama = _viewModel.SelectedRekeningNonAir.Nama,
                Alamat = _viewModel.SelectedRekeningNonAir.Alamat,
                IdPelangganAir = 0,
                IdPelangganLimbah = 0,
                IdPelangganLltt = 0,
                NoHp = _viewModel.NoHpForm,
                NoTelp = _viewModel.NoTelpForm,
                JumlahTermin = _viewModel.TerminForm,
                JumlahAngsuranPokok = _viewModel.SelectedRekeningNonAir.Total,
                JumlahAngsuranBunga = 0,
                JumlahUangMuka = _viewModel.UangMukaForm,
                Total = _viewModel.SelectedRekeningNonAir.Total,
                IdUser = Convert.ToInt32(Application.Current.Resources["IdUser"]),
                TglMulaiTagihPertama = _viewModel.TglTerminForm,
                Detail = _viewModel.AngsuranNonAirKalkulasi.ToList()
            };

            Type type = typeof(ParamRekeningAngsuranNonAirDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormSubmitAngsuranNonAir));
                }
            }
            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-non-air", body));

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "loket");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "loket");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "loket");
            }

            _viewModel.IsLoadingForm = false;
            await ((AsyncCommandBase)_viewModel._sambungan.OnLoadCommand).ExecuteAsync(null);
            DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog", dg);
            _ = await Task.FromResult(_isTest);
        }
    }
}
