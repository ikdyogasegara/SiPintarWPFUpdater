using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.MutasiJabatan
{
    public partial class DialogDetailFormView : UserControl
    {
        private readonly MutasiJabatanViewModel _viewModel;
        public DialogDetailFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as MutasiJabatanViewModel;
            DataContext = _viewModel;

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
            if (Jabatan.SelectedValue == null && Departemen.SelectedValue == null
                && Divisi.SelectedValue == null && AreaKerja.SelectedValue == null
                && string.IsNullOrEmpty(Detail.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

    }
}
