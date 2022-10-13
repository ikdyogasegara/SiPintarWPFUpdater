using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace SiPintar.Views.Global.Other
{
    public partial class PrintPreviewNoTemplate : Window
    {
        public PrintPreviewNoTemplate()
        {
            InitializeComponent();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        public IDocumentPaginatorSource Document
        {
            get { return viewer.Document; }
            set { viewer.Document = value; }
        }
    }
}
