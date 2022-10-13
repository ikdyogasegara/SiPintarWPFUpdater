using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using SiPintar.Commands.Gudang.Pengolahan.Posting;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.Pengolahan
{
    public class PostingViewModel : ViewModelBase
    {
        public PostingViewModel(PengolahanViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parent;
            _isPosting = false;
            _postingStatus = decimal.Zero;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnProsesPostingCommand = new OnProsesPostingCommand(this, restApi, isTest);
            OnCancelPostingCommand = new OnCancelPostingCommand(this, isTest);
            NavItems = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() { Label = "Kartu Stock", IsSelected = false },
                new HorizontalNavigationItem() { Label = "Rekap Stock Barang", IsSelected = false }
            };
        }

        public List<HorizontalNavigationItem> NavItems { get; set; }

        public ICommand OnLoadCommand { get; }
        public ICommand OnProsesPostingCommand { get; }
        public ICommand OnCancelPostingCommand { get; }
        public CancellationTokenSource? CancelToken { get; set; }

        private bool _isPosting;
        public bool IsPosting
        {
            get => _isPosting;
            set
            {
                _isPosting = value;
                OnPropertyChanged();
            }
        }

        private decimal _postingStatus;
        public decimal PostingStatus
        {
            get => _postingStatus;
            set
            {
                _postingStatus = value;
                OnPropertyChanged();
            }
        }

    }
}
