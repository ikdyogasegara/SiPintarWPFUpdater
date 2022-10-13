using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.TagihanManual
{
    /// <summary>
    /// Interaction logic for DetailBiayaView.xaml
    /// </summary>
    public partial class DetailBiayaView : UserControl
    {
        private readonly TagihanManualViewModel _viewModel;

        public DetailBiayaView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as TagihanManualViewModel;
            DataContext = _viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
