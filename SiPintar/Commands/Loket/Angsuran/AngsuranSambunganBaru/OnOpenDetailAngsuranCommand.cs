using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru
{
    public class OnOpenDetailAngsuranCommand : AsyncCommandBase
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailAngsuranCommand(AngsuranSambunganBaruViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsDetailAngsuranVisible = true;
            _viewModel.Parent._detailAngsuran.LinkApi = "rekening-angsuran-non-air-cetak";
            _viewModel.Parent._detailAngsuran.TemplateBeritaAcara = "BeritaAcaraAngsuranSambungBaru";
            _viewModel.Parent._detailAngsuran.TemplateSpa = "SuratPernyataanAngsuranSambungBaru";
            await Task.FromResult(_isTest);
        }
    }
}
