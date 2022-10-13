using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol.JadwalRuteBaca;

namespace SiPintar.Commands.Bacameter.SistemKontrol.JadwalRuteBaca.RayonBelumDijadwal
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly RayonBelumDijadwalViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(RayonBelumDijadwalViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.CurrentPage < _viewModel.TotalPage)
            {
                _viewModel.CurrentPage += 1;

                LoadData();
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void LoadData()
        {
            if (!_isTest)
                _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
