using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Views.Loket.Dialog
{
    public partial class BatalkanFormView : UserControl
    {
        public BatalkanFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            AlasanBatal.SelectedItem = null;
            SetDisplay();
            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void SetDisplay()
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                switch (vm.FilterTipeTransaksi.Tipe)
                {
                    case "Pembayaran Rek. Air":
                        Title.Text += " Rekening Air";
                        break;
                    case "Pembayaran Rek. Limbah":
                        Title.Text += " Rekening Limbah";
                        break;
                    case "Pembayaran Rek. L2T2":
                        Title.Text += " Rekening L2T2";
                        break;
                    case "Pembayaran Rek. Non Air":
                        Title.Text += " Rekening Non Air";
                        break;
                }

                Nama.Text = vm.SelectedData.Nama;
                Tanggal.Text = vm.SelectedData.WaktuTransaksi?.ToString("dd MMMM yyyy");
                Nominal.Text = $"{vm.SelectedData.Total:n0}";
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

                var listRekeningAir = new List<BayarRekeningAir>();
                var listRekeningLimbah = new List<BayarRekeningLimbah>();
                var listRekeningLltt = new List<BayarRekeningLltt>();
                var listRekeningNonAir = new List<BayarRekeningNonAir>();

                switch (vm.FilterTipeTransaksi.Tipe)
                {
                    case "Pembayaran Rek. Air":
                        listRekeningAir.Add(new BayarRekeningAir() { IdPelangganAir = vm.SelectedData.IdPelangganAir, IdPeriode = vm.SelectedData.IdPeriode, });
                        break;
                    case "Pembayaran Rek. Limbah":
                        listRekeningLimbah.Add(new BayarRekeningLimbah() { IdPelangganLimbah = vm.SelectedData.IdPelangganLimbah, IdPeriode = vm.SelectedData.IdPeriode, });
                        break;
                    case "Pembayaran Rek. L2T2":
                        listRekeningLltt.Add(new BayarRekeningLltt() { IdPelangganLltt = vm.SelectedData.IdPelangganLltt, IdPeriode = vm.SelectedData.IdPeriode, });
                        break;
                    case "Pembayaran Rek. Non Air":
                        listRekeningNonAir.Add(new BayarRekeningNonAir() { IdNonAir = vm.SelectedData.IdNonAir, });
                        break;
                }

                var body = new Dictionary<string, dynamic>
                {
                    {"WaktuBatal", DateTime.Now},
                    {"IdAlasanBatal", vm.SelectedAlasan.IdAlasanBatal},
                    {"IdLoket", Application.Current.Resources["IdLoket"]?.ToString()},
                    {"IdUser", Application.Current.Resources["IdUser"]?.ToString()},
                    {"RekeningAir", listRekeningAir},
                    {"RekeningLimbah", listRekeningLimbah},
                    {"RekeningLltt", listRekeningLltt},
                    {"RekeningNonAir", listRekeningNonAir},
                    {"Angsuran", new List<BayarAngsuranAir>()},
                };

                _ = ((AsyncCommandBase)vm.OnSubmitBatalkanCommand).ExecuteAsync(body);
            }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            OkButton.IsEnabled = AlasanBatal.SelectedItem != null;
        }
    }
}
