using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Loket.Dialog
{
    public partial class BayarTagihanSuccess : UserControl
    {
        private readonly ICommand _lihatRiwayat;
        private readonly ICommand _backToMainPage;
        private readonly ICommand _closeDialog;
        private readonly bool _showMenuRiwayat;

        public BayarTagihanSuccess(decimal? change, ICommand LihatRiwayatCommand, ICommand backToMainPageCommand, ICommand CloseDialogCommand, bool showMenuRiwayat = true)
        {
            InitializeComponent();

            SetDisplay(change);

            _lihatRiwayat = LihatRiwayatCommand;
            _backToMainPage = backToMainPageCommand;
            _closeDialog = CloseDialogCommand;
            _showMenuRiwayat = showMenuRiwayat;
        }

        private void SetDisplay(decimal? change)
        {
            PurchaseChange.Text = change.HasValue ? $"{change:N0}" : "0";

            ButtonRiwayat.Visibility = _showMenuRiwayat ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Riwayat_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _lihatRiwayat.Execute(null);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _closeDialog.Execute(null);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void BackToMain_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _backToMainPage.Execute(null);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
