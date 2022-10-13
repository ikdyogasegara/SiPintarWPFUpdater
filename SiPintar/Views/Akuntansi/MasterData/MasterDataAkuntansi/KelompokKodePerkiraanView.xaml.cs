using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;


namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for KelompokKodePerkiraanView.xaml
    /// </summary>
    public partial class KelompokKodePerkiraanView : UserControl
    {
        private KelompokKodePerkiraanViewModel CurrentViewModel;
        public KelompokKodePerkiraanView()
        {
            InitializeComponent();

            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);

            ShowFilter_Click();
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(230);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (KelompokKodePerkiraanViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan1ViewModel perkiraan1)
                perkiraan1.OnLoadCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan2ViewModel perkiraan2)
                perkiraan2.OnLoadCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan3ViewModel perkiraan3)
                perkiraan3.OnLoadCommand.Execute(null);
        }

        private void Tabel_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (KelompokKodePerkiraanViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan1ViewModel perkiraan1)
                perkiraan1.OnOpenSettingTableFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan2ViewModel perkiraan2)
                perkiraan2.OnOpenSettingTableFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan3ViewModel perkiraan3)
                perkiraan3.OnOpenSettingTableFormCommand.Execute(null);
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (KelompokKodePerkiraanViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan1ViewModel perkiraan1)
                _ = ((AsyncCommandBase)perkiraan1.OnOpenDeleteFormCommand).ExecuteAsync(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan2ViewModel perkiraan2)
                _ = ((AsyncCommandBase)perkiraan2.OnOpenDeleteFormCommand).ExecuteAsync(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan3ViewModel perkiraan3)
                _ = ((AsyncCommandBase)perkiraan3.OnOpenDeleteFormCommand).ExecuteAsync(null);
        }

        private void Koreksi_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (KelompokKodePerkiraanViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan1ViewModel perkiraan1)
                perkiraan1.OnOpenEditFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan2ViewModel perkiraan2)
                perkiraan2.OnOpenEditFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan3ViewModel perkiraan3)
                perkiraan3.OnOpenEditFormCommand.Execute(null);
        }

        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (KelompokKodePerkiraanViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan1ViewModel perkiraan1)
                _ = ((AsyncCommandBase)perkiraan1.OnOpenAddFormCommand).ExecuteAsync(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan2ViewModel perkiraan2)
                _ = ((AsyncCommandBase)perkiraan2.OnOpenAddFormCommand).ExecuteAsync(null);
            else if (CurrentViewModel.PageViewModel is KelompokKodePerkiraan3ViewModel perkiraan3)
                _ = ((AsyncCommandBase)perkiraan3.OnOpenAddFormCommand).ExecuteAsync(null);
        }

    }
}
