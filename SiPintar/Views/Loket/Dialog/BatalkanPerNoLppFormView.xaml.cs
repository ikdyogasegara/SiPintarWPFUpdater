using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Views.Loket.Dialog
{
    public partial class BatalkanPerNoLppFormView : UserControl
    {
        private readonly string _nomorTransaksi;
        private readonly DateTime _waktuTransaksi;
        public BatalkanPerNoLppFormView(object dataContext, string nomorTransaksi, DateTime waktuTransaksi)
        {
            InitializeComponent();
            DataContext = dataContext;

            AlasanBatal.SelectedItem = null;
            _nomorTransaksi = nomorTransaksi;
            _waktuTransaksi = waktuTransaksi;

            SetDisplay();
            CheckButton();

            PreviewKeyUp += HandleEsc;
        }
        private void SetDisplay()
        {
            if (DataContext is RiwayatTransaksiViewModel)
            {
                Title.Text += " Per Nomor Transaksi";
                Nomor.Text = _nomorTransaksi;
                Tanggal.Text = _waktuTransaksi.ToString("dd MMMM yyyy");
            }
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);

                var body = new Dictionary<string, dynamic>
                {
                    { "WaktuBatal", DateTime.Now },
                    { "IdAlasanBatal", vm.SelectedAlasan.IdAlasanBatal },
                    { "IdLoket", Application.Current.Resources["IdLoket"]?.ToString() },
                    { "IdUser", Application.Current.Resources["IdUser"]?.ToString() },
                    { "NomorTransaksi", _nomorTransaksi },
                };

                _ = ((AsyncCommandBase)vm.OnSubmitBatalkanPerNoLppCommand).ExecuteAsync(body);
            }
        }
        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void CheckButton()
        {
            OkButton.IsEnabled = AlasanBatal.SelectedItem != null;
        }
    }
}

