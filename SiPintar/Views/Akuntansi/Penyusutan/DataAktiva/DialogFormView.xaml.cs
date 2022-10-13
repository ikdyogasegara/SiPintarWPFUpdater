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
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.Views.Akuntansi.Penyusutan.DataAktiva
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly DataAktivaViewModel _viewModel;

        public DialogFormView(DataAktivaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DataAktivaViewModel)DataContext;

            Title.Text = ((DataAktivaViewModel)DataContext).IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Data Aktiva";
            OkButton.Content = ((DataAktivaViewModel)DataContext).IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

            if (((DataAktivaViewModel)DataContext).IsAdd)
            {
                RadioOtomatis.IsChecked = true;
            }
            else
            {
                RadioManual.IsChecked = true;
                RadioOtomatis.IsEnabled = false;
            }

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void RadioButtonOtomatis_Checked(object sender, RoutedEventArgs e)
        {
            OtomatisInput.Visibility = Visibility.Visible;
            ManualInput.Visibility = Visibility.Collapsed;
        }

        private void RadioButtonManual_Checked(object sender, RoutedEventArgs e)
        {
            OtomatisInput.Visibility = Visibility.Collapsed;
            ManualInput.Visibility = Visibility.Visible;
        }


        private void Bulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}
