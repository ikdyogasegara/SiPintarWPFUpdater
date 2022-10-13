using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class MasterAddFormView : UserControl
    {
        private readonly PendaftaranViewModel _viewModel;
        private int _currentStep = 1;

        public MasterAddFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PendaftaranViewModel;
            DataContext = _viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            _viewModel.FormData.WaktuPermohonan = DateTime.Now;
            SetStep(1);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStep == 1)
                DialogHost.CloseDialogCommand.Execute(null, null);
            else if (_currentStep == 2)
                SetStep(1);
            else
                SetStep(2);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStep == 1)
            {
                if (_viewModel.FormData.IdTipePendaftaranSambungan == null || string.IsNullOrEmpty(_viewModel.FormData.Nama) || string.IsNullOrEmpty(_viewModel.FormData.NoHp) ||
                    string.IsNullOrEmpty(_viewModel.FormData.Alamat) || string.IsNullOrEmpty(_viewModel.FormData.NoKtp) ||
                    string.IsNullOrEmpty(_viewModel.FormData.NoKk) || _viewModel.FormData.IdKelurahan == null ||
                    _viewModel.FormData.IdRayon == null || string.IsNullOrEmpty(_viewModel.FormData.Keterangan))
                {
                    ShowAlert();
                    return;
                }
                SetStep(2);
            }
            else if (_currentStep == 2)
            {
                SetStep(3);
            }
            else
            {
                Proses();
            }
        }

        private void Proses()
        {
            if (_viewModel.IsAlamatMapFormChecked)
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.AlamatMap))
                {
                    _viewModel.FormData.Alamat = _viewModel.FormData.AlamatMap;
                }
            }

            var body = new Dictionary<string, dynamic>
            {
                { "IdTipePermohonan", _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan },
                { "IdJenisNonAir" , _viewModel.SelectedTipePermohonanComboBox.IdJenisNonAir },
                { "DetailBiaya" , SetBiaya() },
                { "IdUser" , Application.Current.Resources["IdUser"].ToString() },
                { "DetailParameter" , _viewModel.FormData.Parameter }
            };

            var type = typeof(PermohonanNonPelangganForm);
            var properties = type.GetProperties();
            _viewModel.FormData.FlagPendaftaran = true;

            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" &&
                    property.Name != "FlagUI" && property.Name != "IdTipePermohonan" &&
                    property.Name != "IdUser" && property.Name != "DetailBiaya" &&
                    property.Name != "IdJenisNonAir" && property.Name != "FotoKtp" && property.Name != "FotoImb" &&
                    property.Name != "FotoKk" && property.Name != "FotoSuratPernyataan" && property.Name != "Parameter")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.FormData));
                }
            }

            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = ((AsyncCommandBase)_viewModel.OnSubmitAddFormCommand).ExecuteAsync(body);
        }

        private List<ParamPermohonanNonPelangganBiayaDto> SetBiaya()
        {
            var result = new List<ParamPermohonanNonPelangganBiayaDto>();

            var biayaDaftar = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Pendaftaran",
                PostBiaya = "pendaftaran",
                Value = _viewModel.TotalBiayaPendaftaranForm - _viewModel.PpnBiayaPendaftaranForm
            };
            result.Add(biayaDaftar);

            var biayaDaftarPpn = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Ppn Pendaftaran",
                PostBiaya = "pendaftaran",
                Value = _viewModel.PpnBiayaPendaftaranForm
            };
            result.Add(biayaDaftarPpn);

            var biayaOpname = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Opname",
                PostBiaya = "perencanaan",
                Value = _viewModel.TotalBiayaOpnameForm - _viewModel.PpnBiayaOpnameForm
            };
            result.Add(biayaOpname);

            var biayaOpnamePpn = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Ppn Opname",
                PostBiaya = "perencanaan",
                Value = _viewModel.PpnBiayaOpnameForm
            };
            result.Add(biayaOpnamePpn);

            var biayaFormulir = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Formulir",
                PostBiaya = "formulir",
                Value = _viewModel.TotalBiayaFormulirForm - _viewModel.PpnBiayaFormulirForm
            };
            result.Add(biayaFormulir);

            var biayaFormulirPpn = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Ppn Formulir",
                PostBiaya = "formulir",
                Value = _viewModel.PpnBiayaFormulirForm
            };
            result.Add(biayaFormulirPpn);

            var biayaJaminan = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Jaminan",
                PostBiaya = "jaminan",
                Value = _viewModel.TotalBiayaJaminanForm - _viewModel.PpnBiayaJaminanForm
            };
            result.Add(biayaJaminan);

            var biayaJaminanPpn = new ParamPermohonanNonPelangganBiayaDto()
            {
                Parameter = "Ppn Jaminan",
                PostBiaya = "jaminan",
                Value = _viewModel.PpnBiayaJaminanForm
            };
            result.Add(biayaJaminanPpn);

            return result;
        }

        private void SetStep(int step)
        {
            _currentStep = step;
            BackBtn.Visibility = step > 1 ? Visibility.Visible : Visibility.Collapsed;
            OkButton.Content = step == 3 ? "Daftar Sambungan Baru" : "Selanjutnya";

            ContentStep1.Visibility = step == 1 ? Visibility.Visible : Visibility.Collapsed;
            ContentStep2.Visibility = step == 2 ? Visibility.Visible : Visibility.Collapsed;
            ContentStep3.Visibility = step == 3 ? Visibility.Visible : Visibility.Collapsed;

            switch (step)
            {
                case 1:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 2:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 3:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    break;
                default:
                    break;
            }
        }

        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Form Tidak Lengkap",
                    "Mohon lengkapi data yang dibutuhkan. Field bertanda (*) bersifat mandatory.",
                    "warning",
                    "Oke",
                    false,
                    "hublang"
                ), "SambunganSubDialog");
        }
    }
}
