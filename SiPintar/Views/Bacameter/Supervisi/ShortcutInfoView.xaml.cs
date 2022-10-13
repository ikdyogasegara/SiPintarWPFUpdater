using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class ShortcutInfoView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;
        public ShortcutInfoView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
