using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Views.Personalia.Kepegawaian.Pensiun
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PensiunViewModel ViewModel;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as PensiunViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Sk1.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Sk2.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Sk3.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Sk4.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Sk5.Text))
                OkButton.IsEnabled = false;
            else if (Tgl.SelectedDate == null)
                OkButton.IsEnabled = false;
            else if (JenisPensiun.SelectedValue == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

    }
}
