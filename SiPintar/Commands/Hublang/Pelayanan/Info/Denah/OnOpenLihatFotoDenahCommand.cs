using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.Denah
{
    public class OnOpenLihatFotoDenahCommand : AsyncCommandBase
    {
        private readonly DenahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly string FolderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public OnOpenLihatFotoDenahCommand(DenahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            if (_viewModel.SelectedData.FotoDenah1 == null && _viewModel.SelectedData.FotoDenah2 == null)
            {
                ShowAlert();
                return;
            }

            DialogHelper.ShowLoading(_isTest, "HublangRootDialog");

            var param = new Dictionary<string, dynamic>
            {
                { "IdPermohonan", _viewModel.SelectedData.IdPermohonan}
            };

            string _EndPoint = "";

            switch (_viewModel.SelectedData.Kategori)
            {
                case "AIR":
                    _EndPoint = "permohonan-pelanggan-air-rab-link-foto";
                    break;
                case "LIMBAH":
                    _EndPoint = "permohonan-pelanggan-limbah-rab-link-foto";
                    break;
                case "L2T2":
                    _EndPoint = "permohonan-pelanggan-lltt-rab-link-foto";
                    break;
                case "NON-PEL":
                    _EndPoint = "permohonan-non-pelanggan-rab-link-foto";
                    break;
            }

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_EndPoint}", param));

            _viewModel.FotoForm = new List<BitmapImage>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var result = Result.Data.ToObject<ObservableCollection<PermohonanPelangganAirRabDto>>();
                    _viewModel.Tipe = "Foto Denah";
                    _viewModel.LabelNamaTipePermohonan = "Nama Permohonan";
                    _viewModel.NamaTipePermohonan = _viewModel.SelectedData.NamaTipePermohonan ?? "-";
                    _viewModel.LabelNomor = "Nomor RAB";
                    _viewModel.Nomor = _viewModel.SelectedData.NomorRab ?? "-";
                    _viewModel.FotoForm = GetFoto(result);
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);
            }

            _viewModel.IsLoadingForm = false;
            ShowResult();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private List<BitmapImage> GetFoto(ObservableCollection<PermohonanPelangganAirRabDto> result)
        {
            var image = new List<BitmapImage>();

            if (!string.IsNullOrEmpty(result[0].FotoDenah1))
                image.Add(ImageChecker(result[0].FotoDenah1));

            if (!string.IsNullOrEmpty(result[0].FotoDenah2))
                image.Add(ImageChecker(result[0].FotoDenah2));

            return image;
        }

        [ExcludeFromCodeCoverage]
        private BitmapImage ImageChecker(string UrlFoto)
        {
            var result = new BitmapImage();

            if (!_isTest)
            {
                var ImageSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute);

                result.BeginInit();

                if (!string.IsNullOrEmpty(UrlFoto))
                {
                    try
                    {
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        var LocalPathFile = Path.Combine(FolderPath, UrlFoto.Split('/')[^1]);
                        var FileExtension = UrlFoto.Split('.')[^1];

                        if (!File.Exists(LocalPathFile))
                        {
                            using var webClient = new WebClient();
                            byte[] data = webClient.DownloadData(UrlFoto);

                            var ImgFormat = ImageFormat.Jpeg;
                            if (FileExtension.ToLower() == "png")
                                ImgFormat = ImageFormat.Png;
                            else if (FileExtension.ToLower() == "bmp")
                                ImgFormat = ImageFormat.Bmp;

                            using var mem = new MemoryStream(data);
                            using var dataImage = Image.FromStream(mem);
                            dataImage.Save(LocalPathFile, ImgFormat);
                            mem.Flush();
                            mem.Close();
                        }

                        result.UriSource = new Uri(LocalPathFile, UriKind.RelativeOrAbsolute);
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        result.UriSource = ImageSource;
                    }
                }
                else
                {
                    result.UriSource = ImageSource;
                }

                result.EndInit();
            }

            return result;
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.Close("HublangRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

                if (_viewModel.FotoForm.Count > 0)
                    Application.Current.Dispatcher.Invoke(delegate { ResultDialog(); });
                else
                    ShowAlert();
            }
        }

        [ExcludeFromCodeCoverage]
        private void ResultDialog()
        {
            _ = DialogHost.Show(new FotoDenahView(_viewModel), "HublangRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowAlert()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { AlertDialog(); });
        }

        [ExcludeFromCodeCoverage]
        private void AlertDialog()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Foto yang Dilampirkan",
                    "Silakan pilih permohonan lain yang memiliki lampiran foto",
                    "error",
                    "oke"
                ), "HublangRootDialog");
        }
    }
}
