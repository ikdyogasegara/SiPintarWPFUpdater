using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Bantuan.SaranPengaduan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Bantuan
{
    public class SaranPengaduanViewModel : ViewModelBase
    {
        public SaranPengaduanViewModel(BantuanViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnSubmitCommand = new OnSubmitCommand(this, restApi);
            OnResetCommand = new OnResetCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnSubmitCommand { get; }
        public ICommand OnResetCommand { get; }

        private string _namaPetugas;
        public string NamaPetugas
        {
            get => _namaPetugas;
            set { _namaPetugas = value; OnPropertyChanged(); OnPropertyChanged("IsCompleteForm"); }
        }

        private string _namaPdam;
        public string NamaPDAM
        {
            get => _namaPdam;
            set { _namaPdam = value; OnPropertyChanged(); OnPropertyChanged("IsCompleteForm"); }
        }

        private string _bagian;
        public string Bagian
        {
            get => _bagian;
            set { _bagian = value; OnPropertyChanged(); OnPropertyChanged("IsCompleteForm"); }
        }

        private string _noHp;
        public string NoHp
        {
            get => _noHp;
            set { _noHp = value; OnPropertyChanged(); OnPropertyChanged("IsCompleteForm"); }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private decimal _rating;
        public decimal Rating
        {
            get => _rating;
            set { _rating = value; OnPropertyChanged(); OnPropertyChanged("IsCompleteForm"); }
        }

        private string _komentar;
        public string Komentar
        {
            get => _komentar;
            set { _komentar = value; OnPropertyChanged(); OnPropertyChanged("IsCompleteForm"); }
        }

        private ObservableCollection<MasterSaranPertanyaanDto> _masterSaranPertanyaan;
        public ObservableCollection<MasterSaranPertanyaanDto> MasterSaranPertanyaan
        {
            get => _masterSaranPertanyaan;
            set { _masterSaranPertanyaan = value; OnPropertyChanged(); }
        }


        private List<dynamic> _saranPertanyaanBagian1;
        public List<dynamic> SaranPertanyaanBagian1
        {
            get => _saranPertanyaanBagian1;
            set { _saranPertanyaanBagian1 = value; OnPropertyChanged(); }
        }

        private List<dynamic> _saranPertanyaanBagian2;
        public List<dynamic> SaranPertanyaanBagian2
        {
            get => _saranPertanyaanBagian2;
            set { _saranPertanyaanBagian2 = value; OnPropertyChanged(); }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }


        public bool IsCompleteForm
        {
            get => !string.IsNullOrEmpty(NamaPetugas) && !string.IsNullOrEmpty(Bagian) &&
                !string.IsNullOrEmpty(NamaPDAM) && !string.IsNullOrEmpty(NoHp) &&
                Rating > decimal.Zero;
            set { throw new NotImplementedException(); }
        }

        private ObservableCollection<MasterSaranPertanyaanDto> _selectedMasterSaranPertanyaan;
        public ObservableCollection<MasterSaranPertanyaanDto> SelectedMasterSaranPertanyaan
        {
            get { return _selectedMasterSaranPertanyaan; }
            set
            {
                _selectedMasterSaranPertanyaan = value;
                OnPropertyChanged();
            }
        }

    }
}
