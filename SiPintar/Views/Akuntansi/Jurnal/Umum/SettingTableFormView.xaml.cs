using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.Views.Akuntansi.Jurnal.Umum
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly UmumViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (UmumViewModel)DataContext;

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

            var param = new Dictionary<string, bool?>
            {
                { "NomorBukti", NomorBukti.IsChecked },
                { "Jumlah", Jumlah.IsChecked },
                { "Uraian", Uraian.IsChecked },
                { "TanggalTransaksi", TanggalTransaksi.IsChecked }
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (NomorBukti.IsChecked == true)
                IsSelected = true;
            if (Jumlah.IsChecked == true)
                IsSelected = true;
            if (Uraian.IsChecked == true)
                IsSelected = true;
            if (TanggalTransaksi.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NomorBukti.IsChecked = true;
            Jumlah.IsChecked = true;
            Uraian.IsChecked = true;
            TanggalTransaksi.IsChecked = true;

        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            NomorBukti.IsChecked = false;
            Jumlah.IsChecked = false;
            Uraian.IsChecked = false;
            TanggalTransaksi.IsChecked = false;
        }
    }
}
