using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public partial class Step2AddFormView : UserControl
    {
        public Step2AddFormView()
        {
            InitializeComponent();

            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                if (viewModel.IsFotoBukti1PermohonanFormChecked == true || viewModel.IsFotoBukti2PermohonanFormChecked == true || viewModel.IsFotoBukti3PermohonanFormChecked == true)
                {
                    FotoSection.Visibility = Visibility.Visible;
                }
                else
                {
                    FotoSection.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = DataGridContent.SelectedItem;
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.SelectedPiutangAir = item as RekeningAirPiutangWpf;

                if (viewModel.Parent.IsLoading || viewModel.SelectedPiutangAir == null)
                    return;

                viewModel.IsEdit = true;

                _ = DialogHost.Show(new StanMeterFormView(viewModel), "KoreksiRekeningSubDialog");
            }
        }
    }
}
