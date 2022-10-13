using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.UploadDownload
{
    public class OnReUploadCommand : AsyncCommandBase
    {
        private readonly UploadDownloadViewModel _viewModel;
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public OnReUploadCommand(UploadDownloadViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            SetupReUpload();

            if (_viewModel.TagihanAir)
            {
                for (var i = 0; i < _viewModel.DataTagihanAirUpload.FailedData.Count; i++)
                {
                    var item = _viewModel.DataTagihanAirUpload.FailedData[i];
                    var tempBody = new Dictionary<string, dynamic>
                        {
                            { "IdRayon", item.IdRayon }
                        };

                    var temp = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-air-per-rayon", tempBody);
                    if (!temp.IsError)
                    {
                        var tempResult = temp.Data;
                        if (tempResult.Status)
                        {
                            _viewModel.DataTagihanAirUpload.Success += (int)item.Jumlah;
                            SetLabelDanProgress();
                            _viewModel.DataTagihanAirUpload.FailedData.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            if (_viewModel.TagihanLimbah)
            {
                for (var i = 0; i < _viewModel.DataTagihanLimbahUpload.FailedData.Count; i++)
                {
                    var item = _viewModel.DataTagihanLimbahUpload.FailedData[i];
                    var tempBody = new Dictionary<string, dynamic>
                        {
                            { "IdRayon", item.IdRayon }
                        };

                    var temp = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-limbah-per-rayon", tempBody);
                    if (!temp.IsError)
                    {
                        var tempResult = temp.Data;
                        if (tempResult.Status)
                        {
                            _viewModel.DataTagihanLimbahUpload.Success += (int)item.Jumlah;
                            SetLabelDanProgress();
                            _viewModel.DataTagihanLimbahUpload.FailedData.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            if (_viewModel.TagihanLltt)
            {
                for (var i = 0; i < _viewModel.DataTagihanLlttUpload.FailedData.Count; i++)
                {
                    var item = _viewModel.DataTagihanLlttUpload.FailedData[i];
                    var tempBody = new Dictionary<string, dynamic>
                        {
                            { "IdRayon", item.IdRayon }
                        };

                    var temp = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-lltt-per-rayon", tempBody);
                    if (!temp.IsError)
                    {
                        var tempResult = temp.Data;
                        if (tempResult.Status)
                        {
                            _viewModel.DataTagihanLlttUpload.Success += (int)item.Jumlah;
                            SetLabelDanProgress();
                            _viewModel.DataTagihanLlttUpload.FailedData.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            //check if any failed
            _viewModel.TotalFailed = 0;
            _viewModel.DataTagihanAirUpload.FailedData.ForEach(x => _viewModel.TotalFailed += (int)x.Jumlah);
            _viewModel.DataTagihanLimbahUpload.FailedData.ForEach(x => _viewModel.TotalFailed += (int)x.Jumlah);
            _viewModel.DataTagihanLlttUpload.FailedData.ForEach(x => _viewModel.TotalFailed += (int)x.Jumlah);
            ShowResult(_viewModel.TotalFailed);
            await Task.FromResult(_isTest);
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
        private void SetupReUpload()
        {
            _viewModel.IsUploading = true;
            _viewModel.IsUploadDone = false;
            _viewModel.CanReUpload = false;
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
