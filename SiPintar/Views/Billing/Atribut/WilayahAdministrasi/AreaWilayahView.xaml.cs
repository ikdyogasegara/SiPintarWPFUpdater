using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Views.Billing.Atribut.WilayahAdministrasi
{
    public partial class AreaWilayahView : UserControl
    {
        public AreaWilayahView()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string TagName = ((Button)sender).Tag.ToString();
            var viewModel = (AreaWilayahViewModel)((Button)sender).DataContext;

            if (TagName == "wilayah")
            {
                viewModel.SelectedWilayah = null;
                viewModel.WilayahList.Clear();
            }
            else if (TagName == "area")
            {
                viewModel.SelectedArea = null;
                viewModel.AreaList.Clear();
            }
            else if (TagName == "rayon")
            {
                viewModel.SelectedRayon = null;
                viewModel.RayonList.Clear();
            }

            _ = Task.Run(() => viewModel.OnLoadCommand.Execute(TagName));
        }
    }
}
