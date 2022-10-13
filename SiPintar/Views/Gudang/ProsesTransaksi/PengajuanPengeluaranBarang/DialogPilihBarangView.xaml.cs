using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang
{
    public partial class DialogPilihBarangView : UserControl
    {
        private readonly TransaksiBarangKeluarViewModel _vm;

        private readonly Func<MasterBarangWpf, bool> _submitFunction;

        public DialogPilihBarangView(object dataContext, Func<MasterBarangWpf, bool> submitFunction = null)
        {
            InitializeComponent();
            _vm = dataContext as TransaksiBarangKeluarViewModel;
            DataContext = _vm;
            _vm.OnGetCariBarangListCommand.Execute(null);
            PreviewKeyUp += OnPreviewKeyUp;
            _submitFunction = submitFunction;
        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void UIElement_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            _vm.OnGetCariBarangListCommand.Execute(null);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_submitFunction != null)
            {
                _ = _submitFunction(new MasterBarangWpf()
                {
                    KodeBarang = _vm.SelectedBarangPilihList.KodeBarang,
                    NamaBarang = _vm.SelectedBarangPilihList.NamaBarang,
                    SatuanBarang = _vm.SelectedBarangPilihList.SatuanBarang,
                    Qty = 1
                });
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Cari_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            _vm.OnGetCariBarangListCommand.Execute(null);
        }
    }
}

