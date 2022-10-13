using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Gudang.MasterData;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Gudang.MasterData.Barang
{
    public class OnPilihFotoBarangCommand : AsyncCommandBase
    {
        private readonly BarangViewModel Vm;
        public readonly bool IsTest;

        public OnPilihFotoBarangCommand(BarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = null;
                if (!IsTest)
                {
                    openFileDialog = new OpenFileDialog
                    {
                        Filter = "JPEG File|*.jpg|Bitmap File|*.bmp|PNG File|*.png"
                    };
                }

                if (!IsTest && openFileDialog != null && openFileDialog.ShowDialog() == true)
                {
                    Vm.FotoBarangFormUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.ToString());
                ShowWarning();
            }

            await Task.FromResult(IsTest);
        }

        private void ShowWarning()
        {
            if (!IsTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Data",
                    "Pastikan file anda dalam format yang benar dan mengandung data yang sesuai.",
                    "warning",
                    "Batal",
                    false,
                    "gudang"
                ), "GudangInnerDialog");
        }
    }
}
