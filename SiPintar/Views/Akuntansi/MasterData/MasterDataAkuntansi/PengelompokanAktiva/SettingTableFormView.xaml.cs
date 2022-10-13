using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly PengelompokanAktivaViewModel _viewModel;

        public SettingTableFormView(PengelompokanAktivaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PengelompokanAktivaViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            GolAktiva.IsChecked = false;
            UraianAktiva.IsChecked = false;
            JumlahTahun.IsChecked = false;
            TipeAktiva.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            GolAktiva.IsChecked = true;
            UraianAktiva.IsChecked = true;
            JumlahTahun.IsChecked = true;
            TipeAktiva.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (GolAktiva.IsChecked == true)
                IsSelected = true;
            if (UraianAktiva.IsChecked == true)
                IsSelected = true;
            if (JumlahTahun.IsChecked == true)
                IsSelected = true;
            if (TipeAktiva.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
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
                { "GolAktiva", GolAktiva.IsChecked },
                { "UraianAktiva", UraianAktiva.IsChecked },
                { "JumlahTahun", JumlahTahun.IsChecked },
                { "TipeAktiva", TipeAktiva.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableFormCommand).ExecuteAsync(param));
        }
    }
}
