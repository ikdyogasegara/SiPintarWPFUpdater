using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Views.Perencanaan.Atribut.Paket
{
    public partial class MaterialView : UserControl
    {
        public MaterialView()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string TagName = ((Button)sender).Tag.ToString();
            var viewModel = (PaketMaterialViewModel)((Button)sender).DataContext;

            if (TagName == "paket")
            {
                viewModel.SelectedPaket = null;
            }
            else if (TagName == "rincian")
            {
                viewModel.SelectedRincian = null;
            }

            _ = Task.Run(() => viewModel.OnLoadCommand.Execute(TagName));
        }
    }
}
