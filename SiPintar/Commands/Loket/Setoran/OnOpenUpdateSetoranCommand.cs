using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using Serilog;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;
using SiPintar.Views.Loket.Setoran;

namespace SiPintar.Commands.Loket.Setoran
{
    public class OnOpenUpdateSetoranCommand : AsyncCommandBase
    {
        private readonly SetoranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly string _folderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net", "SetoranImage");

        public OnOpenUpdateSetoranCommand(SetoranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "LoketRootDialog", "Mohon tunggu", "sedang memuat data...");
            _viewModel.FormSetoran = new ParamSetoranLoketDto()
            {
                IdPdam = _viewModel.SelectedData.IdPdam,
                IdSetoranLoket = _viewModel.SelectedData.IdSetoranLoket,
                IdBank = _viewModel.SelectedData.IdBank,
                IdLoket = _viewModel.SelectedData.IdLoket,
                IdUser = _viewModel.SelectedData.IdUser,
                Keterangan = _viewModel.SelectedData.Keterangan,
                Penerimaan = _viewModel.SelectedData.Penerimaan,
                Setoran = _viewModel.SelectedData.Setoran,
                TglPenerimaan = _viewModel.SelectedData.TglPenerimaan,
                TglSetor = _viewModel.SelectedData.TglSetor,
            };

            _viewModel.BankForm = _viewModel.BankList.FirstOrDefault(x => x.IdBank == _viewModel.SelectedData.IdBank);

            #region Get FotoSetoran
            string ErrorMessage = null;
            var detail = _viewModel.SelectedData;


            var param = new Dictionary<string, dynamic>
            {
                { "IdSetoranLoket", _viewModel.SelectedData.IdSetoranLoket }
            };

            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/setoran-loket-link-foto", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    JArray data = Result.Data.ToObject<JArray>();
                    detail.Foto = ((JObject)data[0]).GetValue("foto", StringComparison.OrdinalIgnoreCase)?.ToString();
                    _viewModel.SelectedData = detail;
                    _viewModel.ResiFormPath = await GetImageAsync(detail.Foto);
                }
                else
                {
                    ErrorMessage = Response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            Debug.WriteLine(ErrorMessage);
            #endregion

            DialogHelper.CloseDialog(_isTest, true, "LoketRootDialog", dg);
            _ = await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "LoketRootDialog", GetInstance);
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<string> GetImageAsync(string urlFile)
        {
            if (!_isTest && !string.IsNullOrEmpty(urlFile))
            {
                try
                {
                    if (!Directory.Exists(_folderPath))
                        Directory.CreateDirectory(_folderPath);

                    var fileName = urlFile.Contains('/') ? urlFile.Split('/')[^1] : urlFile.Split('\\')[^1];
                    var localPathFile = Path.Combine(_folderPath, fileName);
                    var name = urlFile.Replace(".thumbnails", string.Empty);
                    var fileExtension = name.Split('.')[^1];

                    if (File.Exists(localPathFile))
                    {
                        File.Delete(localPathFile);
                    }

                    using var webClient = new WebClient();
                    var data = webClient.DownloadData(urlFile);

                    var imgFormat = ImageFormat.Jpeg;
                    if (fileExtension.ToLower() == "png")
                        imgFormat = ImageFormat.Png;
                    else if (fileExtension.ToLower() == "bmp")
                        imgFormat = ImageFormat.Bmp;

                    await using var mem = new MemoryStream(data);
                    using var dataImage = Image.FromStream(mem);
                    dataImage.Save(localPathFile, imgFormat);
                    await mem.DisposeAsync();
                    return localPathFile;
                }
                catch (Exception errorImage)
                {
                    Debug.WriteLine("error get image " + errorImage);
                    Log.Logger.Error("error get image " + errorImage);
                }
            }
            return null;
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new FormSetoranDialog(_viewModel);

    }
}
