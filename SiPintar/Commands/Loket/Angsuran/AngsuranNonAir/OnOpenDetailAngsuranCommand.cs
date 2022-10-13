using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranNonAir
{
    public class OnOpenDetailAngsuranCommand : AsyncCommandBase
    {
        private readonly AngsuranNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailAngsuranCommand(AngsuranNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsDetailAngsuranVisible = true;
            _viewModel.Parent._detailAngsuran.LinkApi = "rekening-angsuran-non-air-cetak";
            _viewModel.Parent._detailAngsuran.TemplateBeritaAcara = "BeritaAcaraAngsuranNonAir";
            _viewModel.Parent._detailAngsuran.TemplateSpa = "SuratPernyataanAngsuranNonAir";
            await Task.FromResult(_isTest);
        }
    }
}
