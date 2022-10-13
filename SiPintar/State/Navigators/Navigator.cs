using System;
using System.Diagnostics.CodeAnalysis;
using SiPintar.ViewModels;

namespace SiPintar.State.Navigators
{
    [ExcludeFromCodeCoverage]
    public class Navigator : INavigator
    {
        private ViewModelBase _bacameterViewModel;
        public ViewModelBase BacameterCurrentViewModel
        {
            get { return _bacameterViewModel; }
            set { _bacameterViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _billingViewModel;
        public ViewModelBase BillingCurrentViewModel
        {
            get { return _billingViewModel; }
            set { _billingViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _hublangViewModel;
        public ViewModelBase HublangCurrentViewModel
        {
            get { return _hublangViewModel; }
            set { _hublangViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _loketViewModel;
        public ViewModelBase LoketCurrentViewModel
        {
            get { return _loketViewModel; }
            set { _loketViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _distribusiViewModel;
        public ViewModelBase DistribusiCurrentViewModel
        {
            get { return _distribusiViewModel; }
            set { _distribusiViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _perencanaanViewModel;
        public ViewModelBase PerencanaanCurrentViewModel
        {
            get { return _perencanaanViewModel; }
            set { _perencanaanViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _gudangViewModel;
        public ViewModelBase GudangCurrentViewModel
        {
            get { return _gudangViewModel; }
            set { _gudangViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _personaliaViewModel;
        public ViewModelBase PersonaliaCurrentViewModel
        {
            get { return _personaliaViewModel; }
            set { _personaliaViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _akuntansiViewModel;
        public ViewModelBase AkuntansiCurrentViewModel
        {
            get { return _akuntansiViewModel; }
            set { _akuntansiViewModel = value; StateChanged?.Invoke(); }
        }

        private ViewModelBase _laporanCurrentViewModel;

        public ViewModelBase LaporanCurrentViewModel
        {
            get { return _laporanCurrentViewModel; }
            set { _laporanCurrentViewModel = value; StateChanged?.Invoke(); }
        }

        public event Action StateChanged;
    }
}
