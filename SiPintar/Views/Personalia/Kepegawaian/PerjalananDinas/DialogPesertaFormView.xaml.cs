using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas
{
    public partial class DialogPesertaFormView : UserControl
    {
        private readonly PerjalananDinasViewModel _viewModel;
        public DialogPesertaFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PerjalananDinasViewModel;
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
        private void CheckForm_TargetUpdated(object sender, DataTransferEventArgs e) => CheckButton();

        private void CheckButton()
        {
            //if (DataGridPerjalananDinasPesertaBiaya.Items.Count < 1)
            //    OkButton.IsEnabled = false;
            //else
            //    OkButton.IsEnabled = true;
        }

    }
}
