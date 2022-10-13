using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Views.Bacameter.SistemKontrol
{
    public partial class PengaturanPutstampView : UserControl
    {
        public PengaturanPutstampView()
        {
            InitializeComponent();
        }

        private void FilePathPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var ViewModel = (PengaturanPutstampViewModel)DataContext;
            if (ViewModel != null)
                ViewModel.OnBrowseFileCommand.Execute(null);
        }
    }
}
