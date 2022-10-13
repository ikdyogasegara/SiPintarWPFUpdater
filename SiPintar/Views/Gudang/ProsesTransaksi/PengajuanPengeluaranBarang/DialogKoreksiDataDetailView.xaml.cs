using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang
{
    public partial class DialogKoreksiDataDetailView : UserControl
    {
        private readonly TransaksiBarangKeluarViewModel _vm;

        public DialogKoreksiDataDetailView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as TransaksiBarangKeluarViewModel;
            DataContext = _vm;
            KuantitasBarang.GotFocus += DecimalValidationHelper.Object_GotFocus;
            KuantitasBarang.LostFocus += DecimalValidationHelper.Object_LostFocus;
            KuantitasBarang.Text =
                DecimalValidationHelper.FormatDecimalToStringId(_vm.SelectedDataDetail.Qty);
            KuantitasBarang.Focus();
            KuantitasBarang.SelectAll();
            PreviewKeyUp += DialogKoreksiDataDetailView_PreviewKeyUp;
        }

        private void DialogKoreksiDataDetailView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (DecimalValidationHelper.FormatStringIdToDecimal(KuantitasBarang.Text) > 0)
            {
                _vm.OnSubmitKoreksiDataDetailCommand.Execute(new Dictionary<string, dynamic>()
                {
                    { "idPengajuanPengeluaran", _vm.SelectedData.IdPengajuanPengeluaran },
                    { "idBarang", _vm.SelectedDataDetail.IdBarang },
                    { "qty", DecimalValidationHelper.FormatStringIdToDecimal(KuantitasBarang.Text) },
                });
            }
        }

        private void KuantitasBarang_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonBase_OnClick(sender, e);
            }
        }
    }
}

