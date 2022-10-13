using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Views.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public partial class ListUserPDAView : UserControl
    {
        private readonly PengaturanPutstampViewModel viewModel;
        public ListUserPDAView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            viewModel = (PengaturanPutstampViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Select_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            var item = DataGridContent.SelectedItem;
            var _viewModel = (PengaturanPutstampViewModel)DataContext;

            _viewModel.SelectedUser = item as MasterUserDto;
            _viewModel.PasswordForm = _viewModel.SelectedUser.PasswordUser;
            _viewModel.OnOpenConfirmationPDACommand.Execute(null);
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Select_Click();
        }

    }
}
