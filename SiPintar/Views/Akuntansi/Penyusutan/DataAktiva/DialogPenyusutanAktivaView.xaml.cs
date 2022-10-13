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
    /// Interaction logic for DialogPenyusutanAktivaView.xaml
    /// </summary>
    public partial class DialogPenyusutanAktivaView : UserControl
    {
        private readonly DataAktivaViewModel _viewModel;

        public DialogPenyusutanAktivaView(DataAktivaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DataAktivaViewModel)DataContext;


            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            _viewModel.OnOpenPenyusutanConfirmCommand.Execute(null);
        }

    }
}
