using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        public DialogFormView(PersentaseTarifPajakViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PersentaseTarifPajakViewModel)DataContext;

            Title.Text = ((PersentaseTarifPajakViewModel)DataContext).IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Pengaturan Persentase Tarif Pajak";
            OkButton.Content = ((PersentaseTarifPajakViewModel)DataContext).IsAdd ? "Tambah " : "Simpan ";

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
