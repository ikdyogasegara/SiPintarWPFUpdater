using System.Windows;
using System.Windows.Controls;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    /// <summary>
    /// Interaction logic for DialogInputQtyView.xaml
    /// </summary>
    public partial class DialogInputQtyView : UserControl
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public DialogInputQtyView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupervisiPengajuanPembelianViewModel;
            DataContext = Vm;
            UserControlBase.SetButton(this);
            TotalQty.GotFocus += DecimalValidationHelper.Object_GotFocus;
            TotalQty.LostFocus += DecimalValidationHelper.Object_LostFocus;
            TotalQty.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            TotalQty.Text = "1";

            CheckSubmit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vm.OnTambahBarangPengajuanCommand.Execute(new ParamBarangPengajuanPembelianWpf()
            {
                IdBarang = Vm.SelectedDataBarang.IdBarang,
                NamaBarang = Vm.SelectedDataBarang.NamaBarang,
                Qty = DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text),
                SatuanBarang = Vm.SelectedDataBarang.SatuanBarang,
                //API CONCEPT NOT AVAILABLE YET
                Stock = 0
            });
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text) > 0;
        }

        private void TotalQty_TextChanged(object sender, TextChangedEventArgs e) => CheckSubmit();
    }
}
