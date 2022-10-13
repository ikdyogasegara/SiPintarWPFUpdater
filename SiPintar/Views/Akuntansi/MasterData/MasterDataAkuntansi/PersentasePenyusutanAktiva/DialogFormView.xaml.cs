using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;
        public DialogFormView(PersentasePenyusutanAktivaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PersentasePenyusutanAktivaViewModel)DataContext;

            InitView();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void InitView()
        {
            Title.Text = "Tambah ";
            OkButton.Content = "Tambah ";
            if (!_viewModel.IsAdd)
            {
                Title.Text = "Koreksi ";
                OkButton.Content = "Simpan ";

                switch (_viewModel.FormData!.KodeSusut)
                {
                    case "B":
                        NilaiBeli.IsChecked = true;
                        break;
                    case "P":
                        NilaiPerolehan.IsChecked = true;
                        break;
                    default:
                        NilaiBeli.IsChecked = false;
                        NilaiPerolehan.IsChecked = false;
                        break;
                };
            }
            Title.Text += "Pengaturan Persentase Peny. Aktiva";

            Persen.PreviewTextInput += Helpers.DecimalValidationHelper.DecimalValidationTextBox;
            Persen.GotFocus += Helpers.DecimalValidationHelper.Object_GotFocus;
            Persen.LostFocus += Helpers.DecimalValidationHelper.Object_LostFocus;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(_viewModel.FormData!.KodeGolAktiva))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(_viewModel.FormData.NamaGolAktiva))
                OkButton.IsEnabled = false;
            else if (_viewModel.FormData.JmlTahun == null)
                OkButton.IsEnabled = false;
            else if (_viewModel.FormData.JmlPersen == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NilaiBeli_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsCheckTipeAktiva = true;
            CheckButton();
        }

        private void NilaiPerolehan_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsCheckTipeAktiva = false;
            CheckButton();
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = new ParamMasterPenyusutanAktivaDto
            {
                IdGolAktiva = _viewModel.IsAdd ? null : _viewModel.FormData!.IdGolAktiva,
                KodeGolAktiva = _viewModel.FormData!.KodeGolAktiva,
                NamaGolAktiva = _viewModel.FormData!.NamaGolAktiva,
                JmlTahun = _viewModel.FormData!.JmlTahun,
                JmlPersen = _viewModel.FormData!.JmlPersen,
                KodeSusut = _viewModel.IsCheckTipeAktiva ? "B" : "P"
            };

            _viewModel.OnSubmitFormCommand.Execute(data);
        }
    }
}
