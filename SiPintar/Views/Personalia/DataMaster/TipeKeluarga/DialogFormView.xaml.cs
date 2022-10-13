using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Views.Personalia.DataMaster.TipeKeluarga
{
    public partial class DialogFormView : UserControl
    {
        private readonly TipeKeluargaViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (TipeKeluargaViewModel)DataContext;

            Title.Text = ((TipeKeluargaViewModel)DataContext).IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Tipe Keluarga";

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
