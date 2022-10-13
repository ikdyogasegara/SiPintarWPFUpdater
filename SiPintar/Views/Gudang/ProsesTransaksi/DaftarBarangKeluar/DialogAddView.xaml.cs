using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    /// <summary>
    /// Interaction logic for DialogAddView.xaml
    /// </summary>
    public partial class DialogAddView : UserControl
    {
        private readonly DaftarBarangKeluarViewModel _vm;

        public DialogAddView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as DaftarBarangKeluarViewModel;
            DataContext = _vm;
            KuantitasBarang.GotFocus += DecimalValidationHelper.Object_GotFocus;
            KuantitasBarang.LostFocus += DecimalValidationHelper.Object_LostFocus;
            PreviewKeyUp += DialogAddView_PreviewKeyUp;
        }

        private void DialogAddView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSubmit_Click(sender, e);
            }
            if (e.Key == Key.F3)
            {
                Button_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        public bool SetBarang(MasterBarangDto param)
        {
            if (param != null && _vm.SelectedDaftarBarangKeluar != null)
            {
                KodeBarang.Text = param.KodeBarang;
                _vm.OnGetDataBarangAddCommand.Execute(param.KodeBarang);
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ = DialogHelper.ShowCustomDialogViewAsync(false, true, "DialogBarangKeluarAddBarang", () => new DialogPilihBarangMaterialView(SetBarang, _vm._restApi,
                _vm.SelectedDaftarBarangKeluar.IdGudang, true));
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var _totalQuantity = DecimalValidationHelper.FormatStringIdToDecimal(KuantitasBarang.Text);
            if (_vm.SelectedDaftarBarangKeluar != null && _vm.AddBarangWithStock != null && _totalQuantity > 0)
            {
                _vm.OnAddCommand.Execute(new Dictionary<string, dynamic>
                {
                    { "IdBarangKeluar" , _vm.SelectedDaftarBarangKeluar.IdBarangKeluar ?? 0 },
                    { "IdBarang" , _vm.AddBarangWithStock.IdBarang ?? 0 },
                    { "Qty" , _totalQuantity }
                });
            }
        }

        private void KuantitasBarang_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSubmit_Click(sender, e);
            }
        }
    }
}
