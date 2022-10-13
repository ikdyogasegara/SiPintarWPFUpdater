using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Views.Personalia.GajiPokok.TenagaKontrak
{
    public partial class DialogFormView : UserControl
    {
        private readonly TenagaKontrakViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (TenagaKontrakViewModel)DataContext;

            Title.Text = ((TenagaKontrakViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Gaji Pegawai Kontrak";

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

        }

    }
}
