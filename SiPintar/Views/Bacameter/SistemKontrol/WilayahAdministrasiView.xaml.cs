using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;

namespace SiPintar.Views.Bacameter.SistemKontrol
{
    public partial class WilayahAdministrasiView : UserControl
    {
        private WilayahAdministrasiViewModel CurrentViewModel;
        public WilayahAdministrasiView()
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
            CurrentViewModel = (WilayahAdministrasiViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            //if (CurrentViewModel.PageViewModel is RayonViewModel rayon)
            //    rayon.OnExportCommand.Execute(null);
            //else if (CurrentViewModel.PageViewModel is KecamatanViewModel kecamatan)
            //    kecamatan.OnExportCommand.Execute(null);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (WilayahAdministrasiViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is RayonViewModel rayon)
                rayon.OnLoadCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is KecamatanViewModel kecamatan)
                kecamatan.OnLoadCommand.Execute(null);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string Current = ((RadioButton)sender).Content.ToString();
            var viewModel = (WilayahAdministrasiViewModel)DataContext;

            viewModel.CurrentSection = Current.ToLower();
        }
    }
}
