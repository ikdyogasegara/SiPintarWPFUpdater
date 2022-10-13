using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi.PeriodePosting
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PeriodePostingViewModel Vm;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as PeriodePostingViewModel;
            DataContext = Vm;

            PreviewKeyUp += DialogFormView_PreviewKeyUp;
        }

        private void DialogFormView_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void BtnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Vm != null)
            {
                _ = ((AsyncCommandBase)Vm.OnOpenConfirmationAddCommand).ExecuteAsync(null);
            }
        }
    }
}
