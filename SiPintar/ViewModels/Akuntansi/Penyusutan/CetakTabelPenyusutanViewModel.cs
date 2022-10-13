using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SiPintar.Commands.Akuntansi.Penyusutan.CetakTabelPenyusutan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Penyusutan
{
    public class CetakTabelPenyusutanViewModel : ViewModelBase
    {
        public CetakTabelPenyusutanViewModel(PenyusutanViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
        }
        public ICommand OnLoadCommand { get; }
    }
}
