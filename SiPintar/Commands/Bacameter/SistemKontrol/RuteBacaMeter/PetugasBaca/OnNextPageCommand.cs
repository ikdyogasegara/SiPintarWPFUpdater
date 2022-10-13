﻿using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var current = parameter as string;

            if (current == "petugas" && _viewModel.CurrentPagePetugas < _viewModel.TotalPagePetugas)
            {
                _viewModel.CurrentPagePetugas += 1;

                _viewModel.GetDataListCommand.Execute(null);
            }
            else if (current == "jadwal" && _viewModel.CurrentPageJadwal < _viewModel.TotalPageJadwal)
            {
                _viewModel.CurrentPageJadwal += 1;

                _viewModel.GetJadwalListCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
