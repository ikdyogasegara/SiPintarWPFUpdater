using System;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PengelompokanAktiva
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PengelompokanAktivaViewModel _viewModel;
        public DialogFormView(PengelompokanAktivaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PengelompokanAktivaViewModel)DataContext;

            Title.Text = ((PengelompokanAktivaViewModel)DataContext).IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Pengaturan Pengelempokan Data Aktiva";
            OkButton.Content = ((PengelompokanAktivaViewModel)DataContext).IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
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
