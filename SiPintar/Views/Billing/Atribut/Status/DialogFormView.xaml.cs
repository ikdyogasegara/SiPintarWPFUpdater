using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Views.Billing.Atribut.Status
{
    public partial class DialogFormView : UserControl
    {
        private readonly StatusViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (StatusViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Status";

            CheckButton(true);

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton(true);

        private void CheckButton(bool? IsCheckRadio = null)
        {
            if (string.IsNullOrEmpty(Nama.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;

            if (IsCheckRadio != null)
                CheckRadio();
        }

        private void CheckRadio()
        {
            YesRekeningAir.IsChecked = _viewModel.IncludeRekeningAirForm;
            NoRekeningAir.IsChecked = !_viewModel.IncludeRekeningAirForm;
            YesRekeningLimbah.IsChecked = _viewModel.IncludeRekeningLimbahForm;
            NoRekeningLimbah.IsChecked = !_viewModel.IncludeRekeningLimbahForm;
            YesRekeningLLTT.IsChecked = _viewModel.IncludeRekeningLLTTForm;
            NoRekeningLLTT.IsChecked = !_viewModel.IncludeRekeningLLTTForm;
            YesRekeningBiaya.IsChecked = _viewModel.TanpaBiayaAirForm;
            NoRekeningBiaya.IsChecked = !_viewModel.TanpaBiayaAirForm;
        }

        private void IncludeRekeningAir_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.IncludeRekeningAirForm = (bool)((RadioButton)sender).IsChecked && ((RadioButton)sender).Tag.ToString() == "YesRekeningAir";
            CheckButton();
        }

        private void IncludeRekeningLimbah_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.IncludeRekeningLimbahForm = (bool)((RadioButton)sender).IsChecked && ((RadioButton)sender).Tag.ToString() == "YesRekeningLimbah";
            CheckButton();
        }

        private void IncludeRekeningLLTT_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.IncludeRekeningLLTTForm = (bool)((RadioButton)sender).IsChecked && ((RadioButton)sender).Tag.ToString() == "YesRekeningLLTT";
            CheckButton();
        }

        private void TanpaBiayaPemakaianAir_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.TanpaBiayaAirForm = (bool)((RadioButton)sender).IsChecked && ((RadioButton)sender).Tag.ToString() == "YesRekeningBiaya";
            CheckButton();
        }
    }
}
