using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Serilog;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class GetVideoCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;
        private int _section = 1; // 1: Video Normal, 2: Video Baca Ulang

        private readonly string _folderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public GetVideoCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var urlFile = parameter as string;

            _viewModel.IsLoadingBukti = true;

            var pathDirectory = AppSetting.LokasiFotoMeter;
            urlFile = Path.Combine(pathDirectory, urlFile);

            _section = urlFile.ToLower().Contains("u.mp4") ? 2 : 1;

            await GetVideoAsync(urlFile);

            _viewModel.IsLoadingBukti = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetVideoAsync(string urlFile)
        {
            var hasVideo = false;
            Uri result = null;

            if (!_isTest)
            {
                if (!string.IsNullOrEmpty(urlFile))
                {
                    try
                    {
                        if (!Directory.Exists(_folderPath))
                            Directory.CreateDirectory(_folderPath);

                        var fileName = urlFile.Contains('/') ? urlFile.Split('/')[^1] : urlFile.Split('\\')[^1];
                        var localPathFile = Path.Combine(_folderPath, fileName);
                        var fileExtension = urlFile.Split('.')[^1];

                        if (!File.Exists(localPathFile))
                        {
                            using var webClient = new WebClient();
                            var data = webClient.DownloadData(urlFile);

                            using var mem = new MemoryStream(data);

                            var fileStream = File.Create(localPathFile, (int)mem.Length);
                            var bytesInStream = new byte[mem.Length];
                            await mem.ReadAsync(bytesInStream);
                            await fileStream.WriteAsync(bytesInStream);
                            fileStream.Close();
                            await mem.DisposeAsync();
                        }

                        result = new Uri(localPathFile, UriKind.RelativeOrAbsolute);

                        hasVideo = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        hasVideo = false;
                    }
                }
                else
                {
                    hasVideo = false;
                }
            }

            if (_section == 1)
            {
                _viewModel.FileVideo = result;
                _viewModel.HasVideo = hasVideo;
            }
            else if (_section == 2)
            {
                _viewModel.FileVideo = result;
                _viewModel.HasVideoUlang = hasVideo;
            }
        }
    }
}
