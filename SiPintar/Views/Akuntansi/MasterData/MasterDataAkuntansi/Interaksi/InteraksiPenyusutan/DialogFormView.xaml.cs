using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiPenyusutan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly InteraksiPenyusutanViewModel _viewModel;
        public DialogFormView(InteraksiPenyusutanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (InteraksiPenyusutanViewModel)DataContext;

            Title.Text = ((InteraksiPenyusutanViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Interaksi Penyusutan";
            OkButton.Content = ((InteraksiPenyusutanViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckButton()
        {
            if (_viewModel.SelectedAkunAktiva?.IdPerkiraan2 == null)
                OkButton.IsEnabled = false;
            else if (_viewModel.SelectedAkunAkumPenyusutan?.IdPerkiraan3 == null)
                OkButton.IsEnabled = false;
            else if (_viewModel.SelectedAkunBiaya?.IdPerkiraan3 == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_viewModel.Parent.IsAdd)
                _viewModel.InPenyusustanForm.IdInPenyusutan = _viewModel.SelectedData.IdInPenyusutan;

            _viewModel.InPenyusustanForm.IdAkunAktiva = _viewModel.SelectedAkunAktiva.IdPerkiraan2;
            _viewModel.InPenyusustanForm.IdAkunAkmPeny = _viewModel.SelectedAkunAkumPenyusutan.IdPerkiraan3;
            _viewModel.InPenyusustanForm.IdAkunBiaya = _viewModel.SelectedAkunBiaya.IdPerkiraan3;

            _ = (_viewModel.OnSubmitFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
        }

        private void KodeAkunAktiva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KodeAkumulasiPenyusutan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KodeAkunBiaya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
