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
    public class OnSubmitAddTunggakanCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitAddTunggakanCommand(AngsuranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            _viewModel.FormSubmitAngsuran = new ParamRekeningAngsuranAirDto()
            {
                IdPdam = _viewModel.SelectedPelanggan.IdPdam,
                NoAngsuran = "0",
                Nama = _viewModel.SelectedPelanggan.Nama,
                Alamat = _viewModel.SelectedPelanggan.Alamat,
                IdPelangganAir = _viewModel.SelectedPelanggan.IdPelangganAir,
                NoHp = _viewModel.NoHpForm,
                NoTelp = _viewModel.NoTelpForm,
                JumlahTermin = _viewModel.TerminForm,
                JumlahAngsuranPokok = _viewModel.RekeningAirList.Sum(x => x.Total),
                JumlahAngsuranBunga = 0,
                JumlahUangMuka = _viewModel.UangMukaForm,
                Total = _viewModel.RekeningAirList.Sum(x => x.Total),
                IdUser = Convert.ToInt32(Application.Current.Resources["IdUser"]),
                TglMulaiTagihPertama = _viewModel.TglTerminForm,
                Detail = _viewModel.AngsuranKalkulasi.ToList()
            };

            Type type = typeof(ParamRekeningAngsuranAirDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormSubmitAngsuran));
                }
            }
            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-angsuran-air", body));

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

            await ((AsyncCommandBase)_viewModel._tunggakan.OnLoadCommand).ExecuteAsync(null);
            DialogHelper.CloseDialog(_isTest, true, "LoketRootDialog", dg);
            _ = await Task.FromResult(_isTest);
        }
    }
}
