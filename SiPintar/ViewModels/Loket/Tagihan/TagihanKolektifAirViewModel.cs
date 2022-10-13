using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.ViewModels.Loket.Tagihan
{
    public class TagihanKolektifAirViewModel : ViewModelBase
    {
        private readonly CariTagihanViewModel _cari;
        private readonly DetailTagihanViewModel _detail;
        private readonly RiwayatTransaksiViewModel _riwayat;
        public TagihanKolektifAirViewModel(TagihanViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;

            _cari = new CariTagihanViewModel(this, parent, restApi);
            _detail = new DetailTagihanViewModel(this, parent, restApi);
            _riwayat = new RiwayatTransaksiViewModel(this, parent, restApi);

            OnLoadCommand = new OnLoadCommand(this);
        }

        public ICommand OnLoadCommand { get; }

        private TagihanViewModel _parent;
        public TagihanViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName, DateTimeOffset? tanggal = null, string jenisPelanggan = null)
        {
            PageViewModel = pageName switch
            {
                "SearchPage" => _cari,
                "DetailTagihanByIdPelangganAir" => _detail,
                "DetailTagihanByIdNonAir" => _detail,
                "RiwayatTransaksi" => _riwayat,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            LoadPageCommand(pageName, tanggal, jenisPelanggan);
        }

        private void LoadPageCommand(string pageName, DateTimeOffset? tanggal, string jenisPelanggan = null)
        {
            switch (pageName)
            {
                case "SearchPage":
                    ((CariTagihanViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                case "DetailTagihanByIdPelangganAir":
                    ((DetailTagihanViewModel)PageViewModel).Tipe = "ByIdPelangganAir";
                    ((DetailTagihanViewModel)PageViewModel).OnRefreshCommand.Execute(tanggal);
                    break;
                case "DetailTagihanByIdNonAir":
                    ((DetailTagihanViewModel)PageViewModel).Tipe = "ByIdNonAir";
                    ((DetailTagihanViewModel)PageViewModel).OnRefreshCommand.Execute(tanggal);
                    break;
                case "RiwayatTransaksi":
                    ((RiwayatTransaksiViewModel)PageViewModel).OnLoadCommand.Execute(jenisPelanggan);
                    break;
                default:
                    break;
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPelangganAirWpf> _listSelectedPelangganAir;
        public ObservableCollection<MasterPelangganAirWpf> ListSelectedPelangganAir
        {
            get => _listSelectedPelangganAir;
            set { _listSelectedPelangganAir = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RekeningNonAirWpf> _listSelectedNonAir;
        public ObservableCollection<RekeningNonAirWpf> ListSelectedNonAir
        {
            get => _listSelectedNonAir;
            set { _listSelectedNonAir = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPeriodeDto> _periodeList;
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get => _periodeList;
            set
            {
                _periodeList = value;
                OnPropertyChanged();

                IsHasPeriode = _periodeList != null && _periodeList.Count > 0;
            }
        }

        private bool _isHasPeriode;
        public bool IsHasPeriode
        {
            get => _isHasPeriode;
            set { _isHasPeriode = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKolektifDto> _kolektifList;
        public ObservableCollection<MasterKolektifDto> KolektifList
        {
            get => _kolektifList;
            set
            {
                _kolektifList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> TahunList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>();

                int startYear = 2010;
                int endYear = DateTime.Now.Year + 1;
                for (int i = startYear; i <= endYear; i++)
                    data.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));

                return data;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> JenisPelangganList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Pelanggan Air"),
                    new KeyValuePair<int, string>(2, "Pelanggan Limbah"),
                    new KeyValuePair<int, string>(3, "Pelanggan L2T2"),
                };
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
