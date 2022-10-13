using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Views.Billing.Atribut.WilayahAdministrasi
{
    public partial class KelurahanView : UserControl
    {
        public KelurahanView()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string TagName = ((Button)sender).Tag.ToString();
            var viewModel = (KelurahanViewModel)((Button)sender).DataContext;

            if (TagName == "cabang")
            {
                viewModel.SelectedCabang = null;
                viewModel.CabangList.Clear();
            }
            else if (TagName == "kecamatan")
            {
                viewModel.SelectedKecamatan = null;
                viewModel.KecamatanList.Clear();
            }
            else if (TagName == "kelurahan")
            {
                viewModel.SelectedKelurahan = null;
                viewModel.KelurahanList.Clear();
            }

            _ = Task.Run(() => viewModel.OnLoadCommand.Execute(TagName));
        }
    }
}
