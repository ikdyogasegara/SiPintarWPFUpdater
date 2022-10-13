using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Atribut.Paket.Rab;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Atribut.Paket
{
    [ExcludeFromCodeCoverage]
    public class PaketRabViewModel : ViewModelBase
    {
        public PaketRabViewModel(PaketViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnOpenDetailCommand = new OnOpenDetailCommand(this);
        }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnOpenDetailCommand { get; }

        private ObservableCollection<MasterPaketDto> _dataList;
        public ObservableCollection<MasterPaketDto> DataList
        {
            get => _dataList;
            set { _dataList = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private MasterPaketDto _selectedData;
        public MasterPaketDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        // Form

        private string _kodeForm;
        public string KodeForm
        {
            get => _kodeForm;
            set { _kodeForm = value; OnPropertyChanged(); }
        }

        private string _namaForm;
        public string NamaForm
        {
            get => _namaForm;
            set { _namaForm = value; OnPropertyChanged(); }
        }

        private MasterPaketMaterialDto _paketMaterialForm;
        public MasterPaketMaterialDto PaketMaterialForm
        {
            get => _paketMaterialForm;
            set
            {
                _paketMaterialForm = value;
                OnPropertyChanged();

                PpnMaterialForm = 0;
            }
        }

        private MasterPaketOngkosDto _paketOngkosForm;
        public MasterPaketOngkosDto PaketOngkosForm
        {
            get => _paketOngkosForm;
            set
            {
                _paketOngkosForm = value;
                OnPropertyChanged();

                PpnOngkosForm = 0;
            }
        }

        private decimal? _hargaPaketForm;
        public decimal? HargaPaketForm
        {
            get => _hargaPaketForm;
            set { _hargaPaketForm = value; OnPropertyChanged(); }
        }

        private bool _withPpnForm;
        public bool WithPpnForm
        {
            get => _withPpnForm;
            set
            {
                _withPpnForm = value;
                OnPropertyChanged();

                if (value == true)
                {
                    NoPpnForm = false;

                    if (!IsEdit)
                    {
                        PpnMaterialTambahanForm = 0;
                        PpnOngkosTambahanForm = 0;
                        PpnMaterialForm = 0;
                        PpnOngkosForm = 0;
                    }
                }
            }
        }

        private bool _noPpnForm;
        public bool NoPpnForm
        {
            get => _noPpnForm;
            set
            {
                _noPpnForm = value;
                OnPropertyChanged();

                if (value == true)
                {
                    WithPpnForm = false;

                    if (!IsEdit)
                    {
                        PpnMaterialTambahanForm = 0;
                        PpnOngkosTambahanForm = 0;
                        PpnMaterialForm = 0;
                        PpnOngkosForm = 0;
                    }
                }
            }
        }

        private bool _isHargaMaterialChecked;
        public bool IsHargaMaterialChecked
        {
            get => _isHargaMaterialChecked;
            set
            {
                _isHargaMaterialChecked = value;
                OnPropertyChanged();

                if (!IsEdit)
                    HargaNetMaterialForm = 0;
            }
        }

        private bool _isHargaOngkosChecked;
        public bool IsHargaOngkosChecked
        {
            get => _isHargaOngkosChecked;
            set
            {
                _isHargaOngkosChecked = value;
                OnPropertyChanged();

                if (!IsEdit)
                    HargaNetOngkosForm = 0;
            }
        }

        private decimal? _persentaseKeuntunganForm;
        public decimal? PersentaseKeuntunganForm
        {
            get => _persentaseKeuntunganForm;
            set { _persentaseKeuntunganForm = value; OnPropertyChanged(); }
        }

        private decimal? _persentaseJasaDariBahanForm;
        public decimal? PersentaseJasaDariBahanForm
        {
            get => _persentaseJasaDariBahanForm;
            set { _persentaseJasaDariBahanForm = value; OnPropertyChanged(); }
        }

        private decimal? _hargaNetMaterialForm;
        public decimal? HargaNetMaterialForm
        {
            get => _hargaNetMaterialForm;
            set { _hargaNetMaterialForm = value; OnPropertyChanged(); }
        }

        private decimal? _hargaNetOngkosForm;
        public decimal? HargaNetOngkosForm
        {
            get => _hargaNetOngkosForm;
            set { _hargaNetOngkosForm = value; OnPropertyChanged(); }
        }

        private decimal? _ppnMaterialForm;
        public decimal? PpnMaterialForm
        {
            get => _ppnMaterialForm;
            set { _ppnMaterialForm = value; OnPropertyChanged(); }
        }

        private decimal? _ppnOngkosForm;
        public decimal? PpnOngkosForm
        {
            get => _ppnOngkosForm;
            set { _ppnOngkosForm = value; OnPropertyChanged(); }
        }

        private decimal? _ppnMaterialTambahanForm;
        public decimal? PpnMaterialTambahanForm
        {
            get => _ppnMaterialTambahanForm;
            set { _ppnMaterialTambahanForm = value; OnPropertyChanged(); }
        }

        private decimal? _ppnOngkosTambahanForm;
        public decimal? PpnOngkosTambahanForm
        {
            get => _ppnOngkosTambahanForm;
            set { _ppnOngkosTambahanForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPaketMaterialDto> _paketMaterialList;
        public ObservableCollection<MasterPaketMaterialDto> PaketMaterialList
        {
            get => _paketMaterialList;
            set { _paketMaterialList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPaketOngkosDto> _paketOngkosList;
        public ObservableCollection<MasterPaketOngkosDto> PaketOngkosList
        {
            get => _paketOngkosList;
            set { _paketOngkosList = value; OnPropertyChanged(); }
        }

        // End Form
    }
}
