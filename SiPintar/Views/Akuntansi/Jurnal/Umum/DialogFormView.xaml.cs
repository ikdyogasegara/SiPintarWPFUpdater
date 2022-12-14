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
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.Views.Akuntansi.Jurnal.Umum
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly UmumViewModel _viewModel;
        public DialogFormView(UmumViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (UmumViewModel)DataContext;

            Title.Text = ((UmumViewModel)DataContext).IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Jurnal Umum";
            OkButton.Content = ((UmumViewModel)DataContext).IsAdd ? "Tambah " : "Simpan ";

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

        private void ButtonAddTransaksi_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenTransaksiAddFormCommand.Execute(null);
        }

        private void ButtonEditTransaksi_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenTransaksiEditFormCommand.Execute(null);
        }

        private void ButtonDeleteTransaksi_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenTransaksiDeleteFormCommand.Execute(null);
        }
    }
}
