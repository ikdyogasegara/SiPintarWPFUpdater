using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.UploadDownload
{
    public class OnConfirmUploadCommand : AsyncCommandBase
    {
        private readonly UploadDownloadViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public OnConfirmUploadCommand(UploadDownloadViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            //Set Init Value for upload
            SetInitValue();
            SetLabelDanProgress();
            ShowLoading();
            await Task.Delay(1000);
            RestApiResponse tagihanAirResponse = null;
            RestApiResponse tagihanLimbahResponse = null;
            RestApiResponse tagihanLlttResponse = null;

            //Prepare
            if (_viewModel.TagihanAir)
            {
                tagihanAirResponse = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-air-per-rayon-prepare", null));
                if (!tagihanAirResponse.IsError)
                {
                    var result = tagihanAirResponse.Data;
                    if (result.Status && result.Data != null)
                    {
                        _viewModel.DataTagihanAirUpload.AllData = result.Data.ToObject<List<dynamic>>();
                        SetLabelDanProgress();
                    }
                }
            }
            if (_viewModel.TagihanLimbah)
            {
                tagihanLimbahResponse = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-limbah-per-rayon-prepare", null));
                if (!tagihanLimbahResponse.IsError)
                {
                    var result = tagihanLimbahResponse.Data;
                    if (result.Status && result.Data != null)
                    {
                        _viewModel.DataTagihanLimbahUpload.AllData = result.Data.ToObject<List<dynamic>>();
                        SetLabelDanProgress();
                    }
                }
            }
            if (_viewModel.TagihanLltt)
            {
                tagihanLlttResponse = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-lltt-per-rayon-prepare", null));
                if (!tagihanLlttResponse.IsError)
                {
                    var result = tagihanLlttResponse.Data;
                    if (result.Status && result.Data != null)
                    {
                        _viewModel.DataTagihanLlttUpload.AllData = result.Data.ToObject<List<dynamic>>();
                        SetLabelDanProgress();
                    }
                }
            }

            AppDispatcherHelper.Run(_isTest, () =>
            {
                DialogHost.Close("BillingRootDialog");
            });
            _viewModel.ProgressVis = true;
            _viewModel.IsUploading = true;
            SetLabelDanProgress();


            if (_viewModel.TagihanAir)
            {
                if (!_viewModel.DataTagihanAirUpload.AllData.Any())
                    _viewModel.ProgressTagihanAir = 100;
                foreach (var item in _viewModel.DataTagihanAirUpload.AllData)
                {
                    await Task.Run(() =>
                    {
                        var tempBody = new Dictionary<string, dynamic>
                        {
                            { "idRayon", item.IdRayon },
                            { "WaktuUpload", DateTime.Now },
                        };

                        var temp = _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-air-per-rayon", tempBody).Result;
                        if (!temp.IsError)
                        {
                            var tempResult = temp.Data;
                            if (!tempResult.Status)
                                _viewModel.DataTagihanAirUpload.FailedData.Add(item);
                            else
                            {
                                _viewModel.DataTagihanAirUpload.Success += (int)item.Jumlah;
                                SetLabelDanProgress();
                            }
                        }
                        else
                            _viewModel.DataTagihanAirUpload.FailedData.Add(item);
                    });
                }

            }

            if (_viewModel.TagihanLimbah)
            {
                if (!_viewModel.DataTagihanLimbahUpload.AllData.Any())
                    _viewModel.ProgressTagihanLimbah = 100;
                foreach (var item in _viewModel.DataTagihanLimbahUpload.AllData)
                {
                    await Task.Run(() =>
                    {
                        var tempBody = new Dictionary<string, dynamic>
                        {
                            { "idRayon", item.IdRayon },
                            { "WaktuUpload", DateTime.Now },
                        };

                        var temp = _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-limbah-per-rayon", tempBody).Result;
                        if (!temp.IsError)
                        {
                            var tempResult = temp.Data;
                            if (!tempResult.Status)
                                _viewModel.DataTagihanLimbahUpload.FailedData.Add(item);
                            else
                            {
                                _viewModel.DataTagihanLimbahUpload.Success += (int)item.Jumlah;
                                SetLabelDanProgress();
                            }
                        }
                        else
                            _viewModel.DataTagihanLimbahUpload.FailedData.Add(item);
                    });
                }
            }

            if (_viewModel.TagihanLltt)
            {
                if (!_viewModel.DataTagihanLlttUpload.AllData.Any())
                    _viewModel.ProgressTagihanLltt = 100;
                foreach (var item in _viewModel.DataTagihanLlttUpload.AllData)
                {
                    await Task.Run(() =>
                    {
                        var tempBody = new Dictionary<string, dynamic>
                        {
                            { "idRayon", item.IdRayon },
                            { "WaktuUpload", DateTime.Now },
                        };

                        var temp = _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-lltt-per-rayon", tempBody).Result;
                        if (!temp.IsError)
                        {
                            var tempResult = temp.Data;
                            if (!tempResult.Status)
                                _viewModel.DataTagihanLlttUpload.FailedData.Add(item);
                            else
                            {
                                _viewModel.DataTagihanLlttUpload.Success += (int)item.Jumlah;
                                SetLabelDanProgress();
                            }
                        }
                        else
                            _viewModel.DataTagihanLlttUpload.FailedData.Add(item);
                    });
                }
            }

            //check if any failed
            _viewModel.TotalFailed = 0;
            _viewModel.DataTagihanAirUpload.FailedData.ForEach(x => _viewModel.TotalFailed += (int)x.Jumlah);
            _viewModel.DataTagihanLimbahUpload.FailedData.ForEach(x => _viewModel.TotalFailed += (int)x.Jumlah);
            _viewModel.DataTagihanLlttUpload.FailedData.ForEach(x => _viewModel.TotalFailed += (int)x.Jumlah);
            ShowResult(_viewModel.TotalFailed);
            await Task.FromResult(true);
        }

        private void SetInitValue()
        {
            _viewModel.DataTagihanAirUpload.Success = 0;
            _viewModel.DataTagihanAirUpload.AllData.Clear();
            _viewModel.DataTagihanAirUpload.FailedData.Clear();
            _viewModel.DataTagihanLimbahUpload.Success = 0;
            _viewModel.DataTagihanLimbahUpload.AllData.Clear();
            _viewModel.DataTagihanLimbahUpload.FailedData.Clear();
            _viewModel.DataTagihanLlttUpload.Success = 0;
            _viewModel.DataTagihanLlttUpload.AllData.Clear();
            _viewModel.DataTagihanLlttUpload.FailedData.Clear();
        }

        private void SetLabelDanProgress()
        {

            if (_viewModel.DataTagihanAirUpload.AllData.Sum(x => x.Jumlah) > 0)
                _viewModel.ProgressTagihanAir = (int)Math.Floor((decimal)_viewModel.DataTagihanAirUpload.Success / (decimal)_viewModel.DataTagihanAirUpload.AllData.Sum(x => x.Jumlah) * 100);
            else
                _viewModel.ProgressTagihanAir = 100;

            if (_viewModel.DataTagihanLimbahUpload.AllData.Sum(x => x.Jumlah) > 0)
                _viewModel.ProgressTagihanLimbah = (int)Math.Floor((decimal)_viewModel.DataTagihanLimbahUpload.Success / (decimal)_viewModel.DataTagihanLimbahUpload.AllData.Sum(x => x.Jumlah) * 100);
            else
                _viewModel.ProgressTagihanLimbah = 100;

            if (_viewModel.DataTagihanLlttUpload.AllData.Sum(x => x.Jumlah) > 0)
                _viewModel.ProgressTagihanLltt = (int)Math.Floor((decimal)_viewModel.DataTagihanLlttUpload.Success / (decimal)_viewModel.DataTagihanLlttUpload.AllData.Sum(x => x.Jumlah) * 100);
            else
                _viewModel.ProgressTagihanLltt = 100;

            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("id-ID");
            _viewModel.LabelProgressTagihanAir = _viewModel.DataTagihanAirUpload.AllData.Count == 0 ? "data sudah terbaru" : $"{_viewModel.DataTagihanAirUpload.Success:n0}/{_viewModel.DataTagihanAirUpload.AllData.Sum(x => x.Jumlah):n0} Upload sukses";
            _viewModel.LabelProgressTagihanLimbah = _viewModel.DataTagihanLimbahUpload.AllData.Count == 0 ? "data sudah terbaru" : $"{_viewModel.DataTagihanLimbahUpload.Success:n0}/{_viewModel.DataTagihanLimbahUpload.AllData.Sum(x => x.Jumlah):n0} Upload sukses";
            _viewModel.LabelProgressTagihanLltt = _viewModel.DataTagihanLlttUpload.AllData.Count == 0 ? "data sudah terbaru" : $"{_viewModel.DataTagihanLlttUpload.Success:n0}/{_viewModel.DataTagihanLlttUpload.AllData.Sum(x => x.Jumlah):n0} Upload sukses";
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                _ = DialogHost.Show(new GlobalLoadingDialog("Mohon tunggu...", "Sedang mempersiapkan", "data..."), "BillingRootDialog");
            }
        }

        private void ShowResult(int totalFailed)
        {
            if (totalFailed == 0)
            {
                _viewModel.IsUploading = false;
                _viewModel.IsUploadDone = true;
            }
            else
            {
                _viewModel.IsUploading = false;
                _viewModel.IsUploadDone = true;
                _viewModel.CanReUpload = true;
            }
        }
    }

}
