using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang
{
    public partial class PengajuanPengeluaranView : UserControl
    {
        private readonly TransaksiBarangKeluarViewModel _vm;

        public PengajuanPengeluaranView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as TransaksiBarangKeluarViewModel;
            DataContext = _vm;
            PreviewKeyUp += OnPreviewKeyUp;
            Qty.LostFocus += DecimalValidationHelper.Object_GotFocus;
            Qty.GotFocus += QtyOnGotFocus;
            Qty.GotFocus += DecimalValidationHelper.Object_LostFocus;
            Qty.PreviewKeyDown += QtyOnPreviewKeyDown;
            _vm.OnEnterKodeBarangEvent += VmOnOnEnterKodeBarangEvent;
            _vm.OnAddBarangToListPengajuanEvent += VmOnOnAddBarangToListPengajuanEvent;
        }

        private void VmOnOnAddBarangToListPengajuanEvent()
        {
            KodeBarang.Focus();
        }

        private void QtyOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox tx && e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tx.Text))
                {
                    _vm.CariBarangForm.Qty = DecimalValidationHelper.FormatStringIdToDecimal(tx.Text);
                    _vm.OnAddBarangToListFormCommand.Execute(null);
                }
            }
        }

        private void QtyOnGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tx)
            {
                tx.SelectAll();
            }
        }

        private void VmOnOnEnterKodeBarangEvent()
        {
            Qty.Text = DecimalValidationHelper.FormatDecimalToStringId(decimal.Zero);
            Qty.Focus();
        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

            if (e.Key == Key.F3)
            {
                _vm.OnOpenCariBarangCommand.Execute(null);
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e) => CheckButton();

        private void Cbx_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void TanggalTransaksi_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void KodeBarang_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _vm.OnGetBarangDetailCommand.Execute(null);
            }
        }

        private void FrameworkElement_OnSourceUpdated(object sender, DataTransferEventArgs e) => CheckButton();

        private void KeteranganPenggunaanBarang_TextChanged(object sender, TextChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            SubmitBtn.IsEnabled = !string.IsNullOrWhiteSpace(_vm.Form.NomorRegistrasiWpf)
                && !string.IsNullOrWhiteSpace(_vm.Form.NomorPengajuanPengeluaranWpf)
                && _vm.SelectedKategori != null
                && _vm.SelectedGudang != null
                && _vm.SelectedBagianMemintaBarang != null
                && !string.IsNullOrWhiteSpace(KeteranganPenggunaanBarang.Text)
                && _vm.Form.TanggalPengajuanWpf.HasValue
                && _vm.Form.DetailWpf?.Count > 0;
        }
    }
}

