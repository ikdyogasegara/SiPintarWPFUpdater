using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.ViewModels.Perencanaan.Bantuan;

namespace SiPintar.Commands.Perencanaan.Bantuan.SaranPengaduan
{
    public class OnResetCommand : AsyncCommandBase
    {
        private readonly SaranPengaduanViewModel _viewModel;

        public OnResetCommand(SaranPengaduanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Bagian = null;
            _viewModel.NoHp = null;
            _viewModel.Email = null;
            _viewModel.Rating = decimal.Zero;

            var Pertanyaan1 = new List<dynamic>(_viewModel.SaranPertanyaanBagian1);
            foreach (var item in Pertanyaan1)
                item.IsSelected = false;

            var Pertanyaan2 = new List<dynamic>(_viewModel.SaranPertanyaanBagian2);
            foreach (var item in Pertanyaan2)
                item.IsSelected = false;

            _viewModel.SaranPertanyaanBagian1 = Pertanyaan1;
            _viewModel.SaranPertanyaanBagian2 = Pertanyaan2;

            _viewModel.Komentar = null;

            await Task.FromResult(true);
        }
    }
}
