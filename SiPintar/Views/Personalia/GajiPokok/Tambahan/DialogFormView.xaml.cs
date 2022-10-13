using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.GajiPokok;

namespace SiPintar.Views.Personalia.GajiPokok.Tambahan
{
    public partial class DialogFormView : UserControl
    {
        private readonly TambahanViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (TambahanViewModel)DataContext;

            Title.Text = ((TambahanViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Gaji Pegawai Tambahan";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }

    }
}
