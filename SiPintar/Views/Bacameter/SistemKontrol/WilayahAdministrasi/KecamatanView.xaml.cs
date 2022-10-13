using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Bacameter.SistemKontrol.WilayahAdministrasi;

namespace SiPintar.Views.Bacameter.SistemKontrol.WilayahAdministrasi
{
    public partial class KecamatanView : UserControl
    {
        public KecamatanView()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string TagName = ((Button)sender).Tag.ToString();
            var viewModel = (KecamatanViewModel)((Button)sender).DataContext;

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
