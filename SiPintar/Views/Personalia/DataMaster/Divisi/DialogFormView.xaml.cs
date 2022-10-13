using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Views.Personalia.DataMaster.Divisi
{
    public partial class DialogFormView : UserControl
    {
        private readonly DivisiViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DivisiViewModel)DataContext;

            Title.Text = ((DivisiViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Divisi";

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
