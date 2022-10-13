using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Views.Billing.Atribut
{
    public partial class RuteBacaMeterView : UserControl
    {
        public RuteBacaMeterView()
        {
            InitializeComponent();
            rayon.IsChecked = true;
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RuteBacaMeterViewModel model)
            {
                var current = ((RadioButton)sender).Name;
                model.CurrentSection = current;
            }
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
            if (DataContext is RuteBacaMeterViewModel model)
            {
                if (model.PageViewModel == null)
                    return;

                if (model.PageViewModel is PerRayonViewModel rayon)
                    rayon.OnExportCommand.Execute(null);
                else if (model.PageViewModel is PerPetugasBacaViewModel petugas)
                    petugas.OnExportCommand.Execute(null);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RuteBacaMeterViewModel model)
            {
                if (model.PageViewModel == null)
                    return;

                if (model.PageViewModel is PerRayonViewModel rayon)
                    rayon.OnRefreshCommand.Execute(null);
                else if (model.PageViewModel is PerPetugasBacaViewModel petugas)
                    petugas.OnRefreshCommand.Execute(null);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RuteBacaMeterViewModel model)
            {
                if (model.PageViewModel == null)
                    return;

                if (model.PageViewModel is PerRayonViewModel rayon)
                    rayon.OnOpenAddFormCommand.Execute(null);
                else if (model.PageViewModel is PerPetugasBacaViewModel petugas)
                    petugas.OnOpenAddFormCommand.Execute(null);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RuteBacaMeterViewModel model)
            {
                if (model.PageViewModel == null)
                    return;

                if (model.PageViewModel is PerRayonViewModel rayon)
                    rayon.OnOpenDeleteConfirmationCommand.Execute(null);
                else if (model.PageViewModel is PerPetugasBacaViewModel petugas)
                    petugas.OnOpenDeleteConfirmationCommand.Execute(null);
            }
        }
    }
}
