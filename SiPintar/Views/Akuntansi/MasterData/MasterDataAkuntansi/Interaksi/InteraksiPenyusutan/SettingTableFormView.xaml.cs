using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;

        public SettingTableFormView(InteraksiPenyusutanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (InteraksiPenyusutanViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            AkunAktiva.IsChecked = false;
            NamaAktiva.IsChecked = false;
            AkunPenyusutan.IsChecked = false;
            AkumulasiPenyusutan.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            AkunAktiva.IsChecked = true;
            NamaAktiva.IsChecked = true;
            AkunPenyusutan.IsChecked = true;
            AkumulasiPenyusutan.IsChecked = true;
        }

        private bool CheckSelected()
        {
            var isSelected = false;

            if (AkunAktiva.IsChecked == true)
                isSelected = true;
            if (NamaAktiva.IsChecked == true)
                isSelected = true;
            if (AkunPenyusutan.IsChecked == true)
                isSelected = true;
            if (AkumulasiPenyusutan.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var isSelected = CheckSelected();

            if (isSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

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
                { "AkunAktiva", AkunAktiva.IsChecked },
                { "NamaAktiva", NamaAktiva.IsChecked },
                { "AkunPenyusutan", AkunPenyusutan.IsChecked },
                { "AkumulasiPenyusutan", AkumulasiPenyusutan.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableFormCommand).ExecuteAsync(param));
        }
    }
}
