using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Global.Dialog
{
    public partial class DialogBgProcess : UserControl
    {
        public DialogBgProcess(object viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
