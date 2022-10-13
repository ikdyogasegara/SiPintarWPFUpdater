using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;
using SiPintar.Views.Loket.Dialog;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.DetailTagihan
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitBayarCommand : AsyncCommandBase
    {
        private readonly DetailTagihanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private List<BayarRekeningAir> _listRekeningAir;
        private List<BayarRekeningLimbah> _listRekeningLimbah;
        private List<BayarRekeningLltt> _listRekeningLltt;
        private List<BayarRekeningNonAir> _listRekeningNonAir;
        private List<BayarAngsuranAir> _listAngsuranAir;
        private List<BayarAngsuranNonAir> _listAngsuranNonAir;
        private decimal _kembalian = 0;

        public OnSubmitBayarCommand(DetailTagihanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsEmpty || _viewModel.JumlahBayar <= 0)
                return;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "LoketRootDialog");

            SetData();

            await PaymentProcessAsync();

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void SetData()
        {
            _listRekeningAir = new List<BayarRekeningAir>();
            _listRekeningLimbah = new List<BayarRekeningLimbah>();
            _listRekeningLltt = new List<BayarRekeningLltt>();
            _listRekeningNonAir = new List<BayarRekeningNonAir>();
            _listAngsuranAir = new List<BayarAngsuranAir>();
            _listAngsuranNonAir = new List<BayarAngsuranNonAir>();

            var rekeningAir = _viewModel.Tagihan.Where(c => c.IsSelected && c.JenisTagihan == "Air").ToList();

            if (rekeningAir.Any())
            {
                foreach (var air in rekeningAir)
                {
                    _listRekeningAir.Add(new BayarRekeningAir() { IdPelangganAir = air.IdPelangganAir, IdPeriode = air.IdPeriode, Denda = air.DendaWpf, Diskon = 0 });
                }
            }

            var rekeningNonAir = _viewModel.Tagihan.Where(c => c.IsSelected && c.JenisTagihan == "Non Air").ToList();

            if (rekeningNonAir.Any())
            {
                foreach (var nonair in rekeningNonAir)
                {
                    _listRekeningNonAir.Add(new BayarRekeningNonAir() { IdNonAir = nonair.IdNonAir });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task PaymentProcessAsync()
        {
            var idLoket = Application.Current.Resources["IdLoket"] != null ? Application.Current.Resources["IdLoket"].ToString() : "0";
            var idKolektif = _viewModel.SelectedKolektif != null && _viewModel.IsKolektifChecked ? _viewModel.SelectedKolektif.IdKolektif : null;

            var body = new Dictionary<string, dynamic>
            {
                { "WaktuTransaksi", _viewModel.ParentPage.WaktuTransaksi },
                { "IdLoket", idLoket },
                { "IdKolektif", idKolektif },
                { "IdKasir", Application.Current.Resources["IdUser"]?.ToString() },
                { "RekeningAir", _listRekeningAir != null && _listRekeningAir.Count > 0 ? _listRekeningAir : new List<BayarRekeningAir>() },
                { "RekeningLimbah", _listRekeningLimbah != null && _listRekeningLimbah.Count > 0 ? _listRekeningLimbah : new List<BayarRekeningLimbah>() },
                { "RekeningLltt", _listRekeningLltt != null && _listRekeningLltt.Count > 0 ? _listRekeningLltt : new List<BayarRekeningLltt>() },
                { "RekeningNonAir", _listRekeningNonAir != null && _listRekeningNonAir.Count > 0 ? _listRekeningNonAir : new List<BayarRekeningNonAir>() },
                { "AngsuranAir", _listAngsuranAir != null && _listAngsuranAir.Count > 0 ? _listAngsuranAir : new List<BayarAngsuranAir>() },
                { "AngsuranNonAir", _listAngsuranNonAir != null && _listAngsuranNonAir.Count > 0 ? _listAngsuranNonAir : new List<BayarAngsuranNonAir>() }
            };

            var bodyCetak = new Dictionary<string, dynamic>
            {
                { "IdKasir", Application.Current.Resources["IdUser"]?.ToString() },
                { "RekeningAir", _listRekeningAir != null && _listRekeningAir.Count > 0 ? _listRekeningAir : new List<BayarRekeningAir>() },
                { "RekeningLimbah", _listRekeningLimbah != null && _listRekeningLimbah.Count > 0 ? _listRekeningLimbah : new List<BayarRekeningLimbah>() },
                { "RekeningLltt", _listRekeningLltt != null && _listRekeningLltt.Count > 0 ? _listRekeningLltt : new List<BayarRekeningLltt>() },
                { "RekeningNonAir", _listRekeningNonAir != null && _listRekeningNonAir.Count > 0 ? _listRekeningNonAir : new List<BayarRekeningNonAir>() },
                { "AngsuranAir", _listAngsuranAir != null && _listAngsuranAir.Count > 0 ? _listAngsuranAir : new List<BayarAngsuranAir>() },
                { "AngsuranNonAir", _listAngsuranNonAir != null && _listAngsuranNonAir.Count > 0 ? _listAngsuranNonAir : new List<BayarAngsuranNonAir>() },
                { "IncludeData", true },
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/payment-bayar", body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    ((OnCetakCommand)_viewModel.OnCetakCommand).IsCetak = true;
                    ((OnCetakCommand)_viewModel.OnCetakCommand).IsPreview = _viewModel.IsLihatKuitansiSebelumCetak;
                    ((OnCetakCommand)_viewModel.OnCetakCommand).Method = CetakApiMethod.POST;
                    ((OnCetakCommand)_viewModel.OnCetakCommand).TemplateName = "KwitansiTagihan";
                    await ((AsyncCommandBase)_viewModel.OnCetakCommand).ExecuteAsync(bodyCetak);

                    _kembalian = _viewModel.JumlahBayar - _viewModel.RincianDetail.TotalTagihan;
                    _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "LoketRootDialog", GetInstance);
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "loket");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "loket");
                    DialogHelper.CloseDialog(_isTest, true, "LoketRootDialog");
                }
            }
            else
            {
                ErrorMessage = response.Error.Message;
                DialogHelper.ShowSnackbar(_isTest, "danger", ErrorMessage, "loket");
                DialogHelper.CloseDialog(_isTest, true, "LoketRootDialog");
            }
        }

        private object GetInstance() => new BayarTagihanSuccess(_kembalian, null, _viewModel.OnCloseDetailTagihanCommand, _viewModel.OnRefreshCommand);

    }
}
