using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Views.Bacameter.SistemKontrol
{
    public partial class RuteBacaMeterView : UserControl
    {
        private RuteBacaMeterViewModel CurrentViewModel;
        public RuteBacaMeterView()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string Current = ((RadioButton)sender).Tag.ToString();
            var viewModel = (RuteBacaMeterViewModel)DataContext;

            viewModel.CurrentSection = Current.ToLower();
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
            CurrentViewModel = (RuteBacaMeterViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is DataRayonViewModel rayon)
                rayon.OnExportCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
                petugas.OnExportCommand.Execute(null);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (RuteBacaMeterViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is DataRayonViewModel rayon)
                rayon.OnRefreshCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
                petugas.OnRefreshCommand.Execute(null);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (RuteBacaMeterViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is DataRayonViewModel rayon)
                rayon.OnOpenAddFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
                petugas.OnOpenAddFormCommand.Execute(null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (RuteBacaMeterViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is DataRayonViewModel rayon)
                rayon.OnOpenDeleteConfirmationCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is ViewModels.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBacaViewModel petugas)
                petugas.OnOpenDeleteConfirmationCommand.Execute(null);
        }
    }
}
