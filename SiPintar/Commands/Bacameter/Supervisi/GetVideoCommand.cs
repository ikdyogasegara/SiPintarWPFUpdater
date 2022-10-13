using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Serilog;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class GetVideoCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;
        private int section = 1; // 1: Video Normal, 2: Video Baca Ulang

        private readonly string FolderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public GetVideoCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var UrlFile = parameter as string;

            _viewModel.IsLoadingBukti = true;

            string pathDirectory = App.Current.Resources["IsDLokasiFotoMeter"]?.ToString();
            UrlFile = Path.Combine(pathDirectory, UrlFile);

            if (UrlFile.ToLower().Contains("u.mp4"))
                section = 2;
            else
                section = 1;

            await GetVideoAsync(UrlFile);

            _viewModel.IsLoadingBukti = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetVideoAsync(string UrlFile)
        {
            bool HasVideo = false;
            Uri result = null;

            if (!_isTest)
            {
                if (!string.IsNullOrEmpty(UrlFile))
                {
                    try
                    {
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        var FileName = UrlFile.Contains('/') ? UrlFile.Split('/')[^1] : UrlFile.Split('\\')[^1];
                        var LocalPathFile = Path.Combine(FolderPath, FileName);
                        var FileExtension = UrlFile.Split('.')[^1];

                        if (!File.Exists(LocalPathFile))
                        {
                            using var webClient = new WebClient();
                            byte[] data = webClient.DownloadData(UrlFile);

                            using var mem = new MemoryStream(data);

                            FileStream fileStream = File.Create(LocalPathFile, (int)mem.Length);
                            byte[] bytesInStream = new byte[mem.Length];
                            await mem.ReadAsync(bytesInStream);
                            await fileStream.WriteAsync(bytesInStream);
                            fileStream.Close();
                            await mem.DisposeAsync();
                        }

                        result = new Uri(LocalPathFile, UriKind.RelativeOrAbsolute);

                        HasVideo = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        HasVideo = false;
                    }
                }
                else
                {
                    HasVideo = false;
                }
            }

            if (section == 1)
            {
                _viewModel.FileVideo = result;
                _viewModel.HasVideo = HasVideo;
            }
            else if (section == 2)
            {
                _viewModel.FileVideo = result;
                _viewModel.HasVideoUlang = HasVideo;
            }
        }
    }
}
