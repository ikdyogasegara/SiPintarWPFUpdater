using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.Web
{
    [ExcludeFromCodeCoverage]
    public class OnOpenWeblinkCommand : AsyncCommandBase
    {
        private readonly MainViewModel _parent;
        private readonly WebViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenWeblinkCommand(WebViewModel viewModel, MainViewModel parent, bool isTest = false)
        {
            _viewModel = viewModel;
            _parent = parent;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _parent;
            _ = _viewModel;

            if (parameter == null)
                return;

            var Url = parameter as string;

            if (string.IsNullOrEmpty(Url))
                return;

            try
            {
                Debug.WriteLine("Open ==> " + Url);

                if (Url.ToLower().Contains("sipintarreport.exe"))
                    CloneAuthenticationForReport(Url);

                var psi = new ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = Url;
                Process.Start(psi);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }

            await Task.FromResult(_isTest);
        }

        private void CloneAuthenticationForReport(string Url)
        {
            string reportLocation = Url.Replace("SiPintarReport.exe", "data.db");
            string DataDb = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\data.db";

            if (!File.Exists(DataDb))
                return;

            File.Copy(DataDb, reportLocation, true);
        }
    }
}
