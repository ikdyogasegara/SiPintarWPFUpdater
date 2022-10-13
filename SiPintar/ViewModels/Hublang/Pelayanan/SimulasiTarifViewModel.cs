using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.SimulasiTarif;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class SimulasiTarifViewModel : ViewModelBase
    {
        public SimulasiTarifViewModel(PelayananViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnHitungCommand = new OnHitungCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand;
        public ICommand OnHitungCommand;

        private ObservableCollection<MasterGolonganDto> _daftarGolongan;
        public ObservableCollection<MasterGolonganDto> DaftarGolongan
        {
            get { return _daftarGolongan; }
            set
            {
                _daftarGolongan = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _pilihGolongan;
        public MasterGolonganDto PilihGolongan
        {
            get { return _pilihGolongan; }
            set
            {
                _pilihGolongan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDiameterDto> _daftarDiameter;
        public ObservableCollection<MasterDiameterDto> DaftarDiameter
        {
            get { return _daftarDiameter; }
            set
            {
                _daftarDiameter = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _pilihDiameter;
        public MasterDiameterDto PilihDiameter
        {
            get { return _pilihDiameter; }
            set
            {
                _pilihDiameter = value;
                OnPropertyChanged();
            }
        }

        private string _meterTerpakai = "-";
        public string MeterTerpakai
        {
            get { return _meterTerpakai; }
            set
            {
                _meterTerpakai = value;
                OnPropertyChanged();
            }
        }

        private string _biayaPemakaian = "-";
        public string BiayaPemakaian
        {
            get { return _biayaPemakaian; }
            set
            {
                _biayaPemakaian = value;
                OnPropertyChanged();
            }
        }

        private string _biayaPelayanan = "-";
        public string BiayaPelayanan
        {
            get { return _biayaPelayanan; }
            set
            {
                _biayaPelayanan = value;
                OnPropertyChanged();
            }
        }

        private string _adminstrasi = "-";
        public string Adminstrasi
        {
            get { return _adminstrasi; }
            set
            {
                _adminstrasi = value;
                OnPropertyChanged();
            }
        }

        private string _pemeliharaan = "-";
        public string Pemeliharaan
        {
            get { return _pemeliharaan; }
            set
            {
                _pemeliharaan = value;
                OnPropertyChanged();
            }
        }

        private string _retribusi = "-";
        public string Retribusi
        {
            get { return _retribusi; }
            set
            {
                _retribusi = value;
                OnPropertyChanged();
            }
        }

        private string _adminstrasiLain = "-";
        public string AdminstrasiLain
        {
            get { return _adminstrasiLain; }
            set
            {
                _adminstrasiLain = value;
                OnPropertyChanged();
            }
        }

        private string _pemeliharaanLain = "-";
        public string PemeliharaanLain
        {
            get { return _pemeliharaanLain; }
            set
            {
                _pemeliharaanLain = value;
                OnPropertyChanged();
            }
        }

        private string _retribusiLain = "-";
        public string RetribusiLain
        {
            get { return _retribusiLain; }
            set
            {
                _retribusiLain = value;
                OnPropertyChanged();
            }
        }

        private string _retribusiPakai = "-";
        public string RetribusiPakai
        {
            get { return _retribusiPakai; }
            set
            {
                _retribusiPakai = value;
                OnPropertyChanged();
            }
        }

        private string _biayaAirLimbah = "-";
        public string BiayaAirLimbah
        {
            get { return _biayaAirLimbah; }
            set
            {
                _biayaAirLimbah = value;
                OnPropertyChanged();
            }
        }

        private string _meterai = "-";
        public string Meterai
        {
            get { return _meterai; }
            set
            {
                _meterai = value;
                OnPropertyChanged();
            }
        }

        private string _ppn = "-";
        public string Ppn
        {
            get { return _ppn; }
            set
            {
                _ppn = value;
                OnPropertyChanged();
            }
        }

        private string _tagihan = "-";
        public string Tagihan
        {
            get { return _tagihan; }
            set
            {
                _tagihan = value;
                OnPropertyChanged();
            }
        }
    }
}
