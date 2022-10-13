using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Views.Personalia.Tunjangan.TunjanganBulanan
{
    /// <summary>
    /// Interaction logic for DialogDeleteView.xaml
    /// </summary>
    public partial class DialogDeleteView : UserControl
    {
        private readonly TunjanganBulananViewModel ViewModel;

        public DialogDeleteView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as TunjanganBulananViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            OkButton.IsEnabled = true;
        }
    }
}
