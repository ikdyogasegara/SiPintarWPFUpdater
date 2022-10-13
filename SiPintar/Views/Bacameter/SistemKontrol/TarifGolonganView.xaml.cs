using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.Views.Bacameter.SistemKontrol
{
    public partial class TarifGolonganView : UserControl
    {
        private TarifGolonganViewModel CurrentViewModel;
        public TarifGolonganView()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string Current = ((RadioButton)sender).Tag.ToString();
            var viewModel = (TarifGolonganViewModel)DataContext;

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
            CurrentViewModel = (TarifGolonganViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is GolonganViewModel golongan)
                golongan.OnExportCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is DiameterViewModel diameter)
                diameter.OnExportCommand.Execute(null);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (TarifGolonganViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is GolonganViewModel golongan)
                golongan.OnRefreshCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is DiameterViewModel diameter)
                diameter.OnRefreshCommand.Execute(null);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (TarifGolonganViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is GolonganViewModel golongan)
                golongan.OnOpenAddFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is DiameterViewModel diameter)
                diameter.OnOpenAddFormCommand.Execute(null);
        }

        private void Koreksi_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (TarifGolonganViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is GolonganViewModel golongan)
                golongan.OnOpenEditFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is DiameterViewModel diameter)
                diameter.OnOpenEditFormCommand.Execute(null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel = (TarifGolonganViewModel)DataContext;

            if (CurrentViewModel.PageViewModel == null)
                return;

            if (CurrentViewModel.PageViewModel is GolonganViewModel golongan)
                golongan.OnOpenDeleteFormCommand.Execute(null);
            else if (CurrentViewModel.PageViewModel is DiameterViewModel diameter)
                diameter.OnOpenDeleteFormCommand.Execute(null);
        }

    }
}
