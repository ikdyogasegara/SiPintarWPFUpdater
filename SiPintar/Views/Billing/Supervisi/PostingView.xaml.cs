using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi
{
    public partial class PostingView : UserControl
    {
        public PostingView()
        {
            InitializeComponent();
        }
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("ExportMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PostingViewModel)DataGridPosting.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridPosting },
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }

        private void CheckTahun_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckTahun.IsChecked == true)
                ComboTahun.IsEnabled = true;
        }

        private void CheckTahun_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckTahun.IsChecked == false)
            {
                ComboTahun.IsEnabled = false;
                ComboTahun.SelectedIndex = -1;
            }
        }

        private void CheckNamaPengguna_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckNamaPengguna.IsChecked == true)
                ComboNamaPengguna.IsEnabled = true;
        }

        private void CheckNamaPengguna_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckNamaPengguna.IsChecked == false)
            {
                ComboNamaPengguna.IsEnabled = false;
                ComboNamaPengguna.SelectedIndex = -1;
            }
        }

        private void CheckTahunRekening_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckTahunRekening.IsChecked == true)
            {
                ComboTahunRekening.IsEnabled = true;
                ComboTahunRekening.SelectedIndex = -1;
            }
        }

        private void CheckTahunRekening_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckTahunRekening.IsChecked == false)
            {
                ComboTahunRekening.IsEnabled = false;
                ComboTahunRekening.SelectedIndex = -1;
            }
        }
    }
}
