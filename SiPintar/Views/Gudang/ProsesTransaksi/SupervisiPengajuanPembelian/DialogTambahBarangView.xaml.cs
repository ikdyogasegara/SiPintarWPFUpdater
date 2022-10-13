using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogTambahBarangView.xaml
    /// </summary>
    public partial class DialogTambahBarangView : UserControl
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public DialogTambahBarangView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;

            Vm.BarangTambahSatuan = new ParamPengajuanPembelianBarangTambahBarangWpf()
            {
                IdPengajuanPembelian = Vm.SelectedData.IdPengajuanPembelian,
                Qty = 1
            };

            TotalQty.GotFocus += DecimalValidationHelper.Object_GotFocus;
            TotalQty.LostFocus += DecimalValidationHelper.Object_LostFocus;
            TotalQty.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;

            PreviewKeyUp += DialogTambahBarangView_PreviewKeyUp;
            CheckSubmit();
        }

        private void DialogTambahBarangView_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NomorPengajuanPembelianBarang.Text) && !string.IsNullOrWhiteSpace(KodeBarang.Text)
                && DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text) > 0;
        }

        private void TextChanged(object sender, TextChangedEventArgs e) => CheckSubmit();
    }
}
