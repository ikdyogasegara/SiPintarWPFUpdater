using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Billing.Supervisi.UploadDownload
{
    public partial class DialogFormView : UserControl
    {
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
