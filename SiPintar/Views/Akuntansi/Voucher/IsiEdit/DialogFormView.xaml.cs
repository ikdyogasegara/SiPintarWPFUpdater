using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Voucher;

namespace SiPintar.Views.Akuntansi.Voucher.IsiEdit
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    ///

    public class DataTransaksi
    {
        public int Nomor { get; set; }
        public string KodeTransaksi { get; set; }
        public string Transaksi { get; set; }
        public string KodeWilayah { get; set; }
        public string Wilayah { get; set; }
        public int Debet { get; set; }
        public int Kredit { get; set; }
    }


    public partial class DialogFormView : UserControl
    {
        private readonly IsiEditViewModel _viewModel;
        private int _rowNumber;

        public DialogFormView(IsiEditViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (IsiEditViewModel)DataContext;

            Title.Text = ((IsiEditViewModel)DataContext).IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Voucher Operasional & Non Operasional";
            OkButton.Content = ((IsiEditViewModel)DataContext).IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

        }

        private void DataGridTransaksi_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            //if (string.IsNullOrEmpty(KodeMaterial.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(NamaMaterial.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(Satuan.Text))
            //    OkButton.IsEnabled = false;
            //else if (string.IsNullOrEmpty(HargaJual.Text))
            //    OkButton.IsEnabled = false;
            //else
            //    OkButton.IsEnabled = true;

        }

        private void ButtonTambah_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.IsEmptyDataTransaksi = false;

            _rowNumber++;

            var dataList = new DataTransaksi
            {
                Nomor = _rowNumber,
                KodeTransaksi = "",
                Transaksi = "",
                KodeWilayah = "",
                Wilayah = "",
                Kredit = 0,
                Debet = 0
            };

            DataGridTransaksi.Items.Add(dataList);
        }

        private void ButtonKodePerkiraan_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenKodePerkiraanCommand.Execute(null);
        }

        private void ButtonPosTandingan_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenPosTandinganCommand.Execute(null);
        }
    }
}
