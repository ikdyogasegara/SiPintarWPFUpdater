using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class BatalSpkFormView : UserControl
    {
        private readonly PermohonanViewModel _vm;

        public BatalSpkFormView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as PermohonanViewModel;
            DataContext = _vm;
            BtnSubmit.IsEnabled = false;
            CheckSubmit();
            PreviewKeyUp += BatalSpkView_PreviewKeyUp;
        }

        private void BatalSpkView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, this);
        }

        private void Alasan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckSubmit();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            _ = ((AsyncCommandBase)_vm.OnSubmitBatalSpkFormCommand).ExecuteAsync(null);
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = Alasan.SelectedItem != null;
        }
    }
}
