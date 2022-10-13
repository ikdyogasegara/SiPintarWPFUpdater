using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.CariTagihan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir
{
    public class CariTagihanViewModel : ViewModelBase
    {
        public readonly TagihanKolektifAirViewModel Parent;

        public CariTagihanViewModel(TagihanKolektifAirViewModel parent, TagihanViewModel parentPage, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;
            ParentPage = parentPage;
            OnLoadCommand = new OnLoadCommand(this);
            OnCekTagihanCommand = new OnCekTagihanCommand(this);
            OnOpenSearchCommand = new OnOpenSearchCommand(this, isTest);
            OnSearchPelangganAirCommand = new OnSearchPelangganAirCommand(this, restApi);
            OnSearchNonAirCommand = new OnSearchNonAirCommand(this, restApi);
            OnConfirmRemoveFromListCommand = new OnConfirmRemoveFromListCommand(this, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnCekTagihanCommand { get; }
        public ICommand OnOpenSearchCommand { get; }
        public ICommand OnSearchPelangganAirCommand { get; }
        public ICommand OnSearchNonAirCommand { get; }
        public ICommand OnConfirmRemoveFromListCommand { get; }


        private TagihanViewModel _parentPage;
        public TagihanViewModel ParentPage
        {
            get => _parentPage;
            set { _parentPage = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEmptyAir;
        public bool IsEmptyAir
        {
            get => _isEmptyAir;
            set { _isEmptyAir = value; OnPropertyChanged(); }
        }

        private bool _isEmptyNonAir;
        public bool IsEmptyNonAir
        {
            get => _isEmptyNonAir;
            set { _isEmptyNonAir = value; OnPropertyChanged(); }
        }

        // form search

        private string _filterStatusPelunasan;
        public string FilterStatusPelunasan
        {
            get => _filterStatusPelunasan;
            set { _filterStatusPelunasan = value; OnPropertyChanged(); }
        }

        private MasterKolektifDto _kolektifForm;
        public MasterKolektifDto KolektifForm
        {
            get => _kolektifForm;
            set { _kolektifForm = value; OnPropertyChanged(); }
        }

        private MasterJenisNonAirDto _jenisNonAirForm;
        public MasterJenisNonAirDto JenisNonAirForm
        {
            get => _jenisNonAirForm;
            set { _jenisNonAirForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKolektifDto> _kolektifList;
        public ObservableCollection<MasterKolektifDto> KolektifList
        {
            get => _kolektifList;
            set { _kolektifList = value; OnPropertyChanged(); }
        }


        private ObservableCollection<MasterJenisNonAirDto> _jenisNonAirList;
        public ObservableCollection<MasterJenisNonAirDto> JenisNonAirList
        {
            get => _jenisNonAirList;
            set { _jenisNonAirList = value; OnPropertyChanged(); }
        }


        private ObservableCollection<MasterPelangganAirWpf> _listSearchPelangganAir;
        public ObservableCollection<MasterPelangganAirWpf> ListSearchPelangganAir
        {
            get => _listSearchPelangganAir;
            set { _listSearchPelangganAir = value; OnPropertyChanged(); }
        }


        public bool IsAllSelectedSearchPelangganAir
        {
            get
            {
                if (ListSearchPelangganAir != null)
                {
                    var count = ListSearchPelangganAir.Count(x => x.IsSelected);
                    return count == ListSearchPelangganAir.Count && count > 0;
                }
                return false;
            }
            set
            {
                if (ListSearchPelangganAir != null)
                {
                    var temp = new ObservableCollection<MasterPelangganAirWpf>(ListSearchPelangganAir);
                    foreach (var item in temp)
                        item.IsSelected = value;

                    ListSearchPelangganAir = temp;
                    TotalSelectedSearchPelangganAir = temp.Where(i => i.IsSelected).ToList().Count;
                }
                OnPropertyChanged("IsAllSelectedSearchPelangganAir");
            }
        }

        private ObservableCollection<RekeningNonAirWpf> _listSearchNonAir;
        public ObservableCollection<RekeningNonAirWpf> ListSearchNonAir
        {
            get => _listSearchNonAir;
            set { _listSearchNonAir = value; OnPropertyChanged(); }
        }


        public bool IsAllSelectedSearchNonAir
        {
            get
            {
                if (ListSearchNonAir != null)
                {
                    var count = ListSearchNonAir.Count(x => x.IsSelected);
                    return count == ListSearchNonAir.Count && count > 0;
                }
                return false;
            }
            set
            {
                if (ListSearchNonAir != null)
                {
                    var temp = new ObservableCollection<RekeningNonAirWpf>(ListSearchNonAir);
                    foreach (var item in temp)
                        item.IsSelected = value;

                    ListSearchNonAir = temp;
                    TotalSelectedSearchNonAir = temp.Where(i => i.IsSelected).ToList().Count;
                }
                OnPropertyChanged("IsAllSelectedSearchNonAir");
            }
        }

        private int _totalSelectedSearchPelangganAir;
        public int TotalSelectedSearchPelangganAir
        {
            get => _totalSelectedSearchPelangganAir;
            set { _totalSelectedSearchPelangganAir = value; OnPropertyChanged(); }
        }

        private int _totalSelectedSearchNonAir;
        public int TotalSelectedSearchNonAir
        {
            get => _totalSelectedSearchNonAir;
            set { _totalSelectedSearchNonAir = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MasterPelangganAirWpf> ListSelectedPelangganAir
        {
            get => Parent.ListSelectedPelangganAir;
            set { Parent.ListSelectedPelangganAir = value; OnPropertyChanged(); }
        }

        public ObservableCollection<RekeningNonAirWpf> ListSelectedNonAir
        {
            get => Parent.ListSelectedNonAir;
            set { Parent.ListSelectedNonAir = value; OnPropertyChanged(); }
        }

        public bool IsAllSelectedListPelangganAir
        {
            get
            {
                if (ListSelectedPelangganAir != null)
                {
                    var count = ListSelectedPelangganAir.Count(x => x.IsSelected);
                    return count == ListSelectedPelangganAir.Count && count > 0;
                }
                return false;
            }
            set
            {
                if (ListSelectedPelangganAir != null)
                {
                    var temp = new ObservableCollection<MasterPelangganAirWpf>(ListSelectedPelangganAir);
                    foreach (var item in temp)
                        item.IsSelected = value;

                    ListSelectedPelangganAir = temp;
                    TotalSelectedListPelangganAir = temp.Where(i => i.IsSelected).ToList().Count;
                }
                OnPropertyChanged("IsAllSelectedListPelangganAir");
            }
        }

        private int _totalSelectedListPelangganAir;
        public int TotalSelectedListPelangganAir
        {
            get => _totalSelectedListPelangganAir;
            set { _totalSelectedListPelangganAir = value; OnPropertyChanged(); }
        }

        public bool IsAllSelectedListNonAir
        {
            get
            {
                if (ListSelectedNonAir != null)
                {
                    var count = ListSelectedNonAir.Count(x => x.IsSelected);
                    return count == ListSelectedNonAir.Count && count > 0;
                }
                return false;
            }
            set
            {
                if (ListSelectedNonAir != null)
                {
                    var temp = new ObservableCollection<RekeningNonAirWpf>(ListSelectedNonAir);
                    foreach (var item in temp)
                        item.IsSelected = value;

                    ListSelectedNonAir = temp;
                    TotalSelectedListNonAir = temp.Where(i => i.IsSelected).ToList().Count;
                }
                OnPropertyChanged("IsAllSelectedListNonAir");
            }
        }

        private int _totalSelectedListNonAir;
        public int TotalSelectedListNonAir
        {
            get => _totalSelectedListNonAir;
            set { _totalSelectedListNonAir = value; OnPropertyChanged(); }
        }

        public void CheckDataUpdate()
        {
            OnPropertyChanged("IsAllSelectedSearchPelangganAir");
            OnPropertyChanged("IsAllSelectedSearchNonAir");

            OnPropertyChanged("IsAllSelectedListPelangganAir");
            OnPropertyChanged("IsAllSelectedListNonAir");
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(10, "10"),
                    new KeyValuePair<int, string>(20, "20"),
                    new KeyValuePair<int, string>(50, "50"),
                    new KeyValuePair<int, string>(100, "100"),
                    new KeyValuePair<int, string>(200, "200"),
                    new KeyValuePair<int, string>(300, "300"),
                    new KeyValuePair<int, string>(500, "500"),
                };
                return data;
            }
        }

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(20, "20");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
            }
        }

        #region prev next page data
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage = 1;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;
        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecord;
        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisible;
        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;
        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;
        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;
        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;
        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
