using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for PersentasePenyusutanAktivaView.xaml
    /// </summary>
    public partial class PersentasePenyusutanAktivaView : UserControl
    {
        public PersentasePenyusutanAktivaView()
        {
            InitializeComponent();
        }
        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var vm = DataContext as PersentasePenyusutanAktivaViewModel;
            if (vm.SelectedData is null)
                e.Handled = true;
        }

    }
}
