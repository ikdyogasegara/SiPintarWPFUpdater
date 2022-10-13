using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class DetailPelangganView : UserControl
    {
        public DetailPelangganView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            var _viewModel = (SupervisiViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
