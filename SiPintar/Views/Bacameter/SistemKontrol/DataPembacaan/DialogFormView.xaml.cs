using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Views.Bacameter.SistemKontrol.DataPembacaan
{
    public partial class DialogFormView : UserControl
    {
        private readonly DataPembacaanViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DataPembacaanViewModel)DataContext;

            CheckButton();

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

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void Bulan_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void Tahun_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (Bulan.SelectedItem == null || Tahun.SelectedItem == null ||
                string.IsNullOrEmpty(Tgl1.Text) || string.IsNullOrEmpty(Tgl2.Text) ||
                string.IsNullOrEmpty(Tgl3.Text) || string.IsNullOrEmpty(Tgl4.Text) ||
                Agreement.IsChecked == null || (bool)!Agreement.IsChecked)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void CheckboxButton_Checked(object sender, RoutedEventArgs e) => CheckButton();
    }
}
