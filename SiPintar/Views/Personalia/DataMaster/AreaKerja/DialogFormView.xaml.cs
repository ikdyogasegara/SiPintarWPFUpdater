using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Views.Personalia.DataMaster.AreaKerja
{
    public partial class DialogFormView : UserControl
    {
        private readonly AreaKerjaViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (AreaKerjaViewModel)DataContext;

            Title.Text = ((AreaKerjaViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Area Kerja";

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
