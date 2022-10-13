using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for InteraksiView.xaml
    /// </summary>
    public partial class InteraksiView : UserControl
    {
        private InteraksiViewModel _currentViewModel = null!;
        public InteraksiView()
        {
            InitializeComponent();
            _currentViewModel = (InteraksiViewModel)DataContext;

            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);

            HideFilter_Click();
        }

        private void HideFilter_Click(object sender = null!, RoutedEventArgs e = null!)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null!, RoutedEventArgs e = null!)
        {
            FilterWrapper.Width = new GridLength(230);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _currentViewModel = (InteraksiViewModel)DataContext;

            if (_currentViewModel.PageViewModel == null)
                return;

            switch (_currentViewModel.PageViewModel)
            {
                case InteraksiLayananViewModel interaksiLayanan:
                    _ = (interaksiLayanan.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiJenisPersediaanViewModel interaksiJenisPersediaan:
                    CheckParamFilter();
                    _ = (interaksiJenisPersediaan.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiPenyusutanViewModel interaksiPenyusutan:
                    CheckParamFilter();
                    _ = (interaksiPenyusutan.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                default:
                    break;
            }
        }

        private void CheckParamFilter()
        {
            switch (_currentViewModel.PageViewModel)
            {
                case InteraksiJenisPersediaanViewModel:
                    CheckJenis.IsChecked = ComboKodeJenis.SelectedItem != null;
                    CheckKeperluan.IsChecked = ComboKodeKeperluan.SelectedItem != null;
                    CheckDebet.IsChecked = ComboKodeDebet.SelectedItem != null;
                    CheckKredit.IsChecked = ComboKodeKredit.SelectedItem != null;
                    break;
                case InteraksiPenyusutanViewModel:
                    CheckAkunAktiva.IsChecked = ComboKodeAkunAktiva.SelectedItem != null;
                    CheckAkunAkmPenyusutan.IsChecked = ComboKodeAkunAkmPenyusutan.SelectedItem != null;
                    CheckAkunBiaya.IsChecked = ComboKodeAkunBiaya.SelectedItem != null;
                    break;
                case InteraksiLayananViewModel:

                default:
                    break;
            }
        }

        private void Tabel_Click(object sender, RoutedEventArgs e)
        {
            _currentViewModel = (InteraksiViewModel)DataContext;

            if (_currentViewModel.PageViewModel == null)
                return;

            switch (_currentViewModel.PageViewModel)
            {
                case InteraksiLayananViewModel interaksiLayanan:
                    _ = (interaksiLayanan.OnOpenSettingTableFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiJenisPersediaanViewModel interaksiJenisPersediaan:
                    _ = (interaksiJenisPersediaan.OnOpenSettingTableFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiPenyusutanViewModel interaksiPenyusutan:
                    _ = (interaksiPenyusutan.OnOpenSettingTableFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                default:
                    break;
            }
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            _currentViewModel = (InteraksiViewModel)DataContext;

            if (_currentViewModel.PageViewModel == null)
                return;

            switch (_currentViewModel.PageViewModel)
            {
                case InteraksiLayananViewModel interaksiLayanan:
                    _ = (interaksiLayanan.OnOpenDeleteFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiJenisPersediaanViewModel interaksiJenisPersediaan:
                    _ = (interaksiJenisPersediaan.OnOpenDeleteFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiPenyusutanViewModel interaksiPenyusutan:
                    _ = (interaksiPenyusutan.OnOpenDeleteFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                default:
                    break;
            }
        }

        private void Koreksi_Click(object sender, RoutedEventArgs e)
        {
            _currentViewModel = (InteraksiViewModel)DataContext;

            if (_currentViewModel.PageViewModel == null)
                return;

            switch (_currentViewModel.PageViewModel)
            {
                case InteraksiLayananViewModel interaksiLayanan:
                    _ = (interaksiLayanan.OnOpenEditFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiJenisPersediaanViewModel interaksiJenisPersediaan:
                    _ = (interaksiJenisPersediaan.OnOpenEditFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiPenyusutanViewModel interaksiPenyusutan:
                    _ = (interaksiPenyusutan.OnOpenEditFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                default:
                    break;
            }
        }

        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _currentViewModel = (InteraksiViewModel)DataContext;

            if (_currentViewModel.PageViewModel == null)
                return;

            switch (_currentViewModel.PageViewModel)
            {
                case InteraksiLayananViewModel interaksiLayanan:
                    _ = (interaksiLayanan.OnOpenAddFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiJenisPersediaanViewModel interaksiJenisPersediaan:
                    _ = (interaksiJenisPersediaan.OnOpenAddFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                case InteraksiPenyusutanViewModel interaksiPenyusutan:
                    _ = (interaksiPenyusutan.OnOpenAddFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
                    break;
                default:
                    break;
            }
        }

        private void JenisInteraksiPelayanan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh_Click(sender, e);
        }
    }
}
