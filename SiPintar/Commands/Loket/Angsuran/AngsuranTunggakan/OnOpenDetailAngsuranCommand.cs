using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Loket.Angsuran;
using SiPintar.Views.Loket.Angsuran.BuatAngsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    public class OnOpenDetailAngsuranCommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailAngsuranCommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsDetailAngsuranVisible = true;
            _viewModel.Parent._detailAngsuran.LinkApi = "rekening-angsuran-air-cetak";
            _viewModel.Parent._detailAngsuran.TemplateBeritaAcara = "BeritaAcaraAngsuranAir";
            _viewModel.Parent._detailAngsuran.TemplateSpa = "SuratPernyataanAngsuranAir";
            await Task.FromResult(_isTest);
        }
    }
}
