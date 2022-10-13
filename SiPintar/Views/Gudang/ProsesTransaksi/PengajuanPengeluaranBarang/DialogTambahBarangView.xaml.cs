using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang
{
    public partial class DialogTambahBarangView : UserControl
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        public DialogTambahBarangView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as TransaksiBarangKeluarViewModel;
            DataContext = _vm;
            PreviewKeyUp += OnPreviewKeyUp;
            KodeBarang.Focus();
            _vm.OnBarangTambahEnterEvent += VmOnOnBarangTambahEnterEvent;
            KuantitasBarang.GotFocus += DecimalValidationHelper.Object_GotFocus;
            KuantitasBarang.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void VmOnOnBarangTambahEnterEvent()
        {
            if (!string.IsNullOrWhiteSpace(_vm.CariBarangForm.NamaBarang))
            {
                KuantitasBarang.Focus();
                KuantitasBarang.SelectAll();
            }
            else
            {
                KodeBarang.Focus();
            }
        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

            if (e.Key == Key.F3)
            {
                _vm.OnOpenCariBarangTambahCommand.Execute(null);
            }
        }

        private void ButtonSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(KodeBarang.Text) && DecimalValidationHelper.FormatStringIdToDecimal(KuantitasBarang.Text) > 0)
            {
                _vm.OnTambahBarangKePengajuanCommand.Execute(KodeBarang.Text);
            }
        }

        private void KodeBarang_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox tx)
            {
                if (!string.IsNullOrWhiteSpace(tx.Text))
                {
                    _vm.OnGetBarangTambahDetailCommand.Execute(tx.Text);
                    _vm.IsErrorCariKodeBarang = false;
                }
            }
            if (sender is TextBox tx1 && !string.IsNullOrWhiteSpace(tx1.Text))
            {
                _vm.IsErrorCariKodeBarang = false;
            }
        }

        private void KuantitasBarang_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonSubmit_OnClick(sender, e);
            }
        }
    }
}

