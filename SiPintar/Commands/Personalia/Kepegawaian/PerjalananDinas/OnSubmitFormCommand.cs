using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;


namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(PerjalananDinasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PersonaliaRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var body = new Dictionary<string, dynamic>
            {
                {"NoSpt", $"{_viewModel.FormSpt1}/{_viewModel.FormSpt2}/{_viewModel.FormSpt3}/{_viewModel.FormSpt4}/{_viewModel.FormSpt5}" },
                {"NoSppd", $"{_viewModel.FormSppd1}/{_viewModel.FormSppd2}/{_viewModel.FormSppd3}/{_viewModel.FormSppd4}/{_viewModel.FormSppd5}" },
                {"Dasar", _viewModel.FormDasar },
                {"Keperluan", _viewModel.FormKeperluan },
                {"TempatBerangkat", _viewModel.FormTempatBerangkat },
                {"TempatTujuan", _viewModel.FormTempatTujuan },

                {"TglBerangkat", _viewModel.FormTglBerangkat },
                {"TglKembali", _viewModel.FormTglKembali },
                {"LamaDinas", _viewModel.FormLamaDinas },
                {"Transportasi", _viewModel.FormTransportasi },
                {"BebanAnggaran", _viewModel.FormBebanAnggaran },
                {"Keterangan", _viewModel.FormKeterangan },

                {"Peserta", _viewModel.FormPesertaList.Select(p => new ParamSppdPesertaDto{ IdPegawai = p.IdPegawai }).ToList() },
                {"Biaya", _viewModel.FormPesertaList.SelectMany(p => p.Biaya.Select(b => new ParamSppdPesertaBiayaDto {
                    IdPegawai = p.IdPegawai,
                    IdBiayaSppd = b.IdBiayaSppd,
                    Biaya = b.Biaya,
                    Qty = b.Qty,
                    Jumlah = b.Jumlah,
                    Keterangan = b.Keterangan,
                })).ToList() },
            };

            RestApiResponse Response = null;
            if (!_viewModel.IsEdit)
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/sppd", body));
            else
            {
                body["NoSpt"] = _viewModel.FormSpt;
                body["NoSppd"] = _viewModel.FormSppd;
                body.Add("IdSppd", _viewModel.SelectedData.IdSppd);
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/sppd", null, body));
            }

            DialogHelper.CloseDialog(_isTest, false, "PersonaliaRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadCommand).ExecuteAsync(null));
            await Task.FromResult(_isTest);
        }
    }
}
