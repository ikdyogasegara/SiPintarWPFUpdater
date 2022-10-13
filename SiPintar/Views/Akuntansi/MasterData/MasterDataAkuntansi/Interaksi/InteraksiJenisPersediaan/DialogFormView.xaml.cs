using System;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        public DialogFormView(InteraksiJenisPersediaanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (InteraksiJenisPersediaanViewModel)DataContext;

            InitView();
            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void InitView()
        {
            Title.Text = _viewModel.Parent.IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Interaksi Persediaan";
            OkButton.Content = _viewModel.Parent.IsAdd ? "Tambah " : "Simpan ";
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (_viewModel.SelectedJenis?.IdJenisBarang == null)
                OkButton.IsEnabled = false;
            else if (_viewModel.SelectedKeperluan?.IdKeperluan == null)
                OkButton.IsEnabled = false;
            else if (_viewModel.SelectedDebet?.IdPerkiraan3 == null)
                OkButton.IsEnabled = false;
            else if (_viewModel.SelectedKredit?.IdPerkiraan3 == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_viewModel.Parent.IsAdd)
                _viewModel.InJenisPersediaanForm.IdInPersediaan = _viewModel.SelectedData.IdInPersediaan;

            _viewModel.InJenisPersediaanForm.IdJenis = _viewModel.SelectedJenis.IdJenisBarang;
            _viewModel.InJenisPersediaanForm.IdKeperluan = _viewModel.SelectedKeperluan.IdKeperluan;
            _viewModel.InJenisPersediaanForm.IdDebet = _viewModel.SelectedDebet.IdPerkiraan3;
            _viewModel.InJenisPersediaanForm.IdKredit = _viewModel.SelectedKredit.IdPerkiraan3;
            _viewModel.InJenisPersediaanForm.Aktiva = _viewModel.IsAktivaChecked ? 1 : 0;

            _ = (_viewModel.OnSubmitFormCommand as AsyncCommandBase)!.ExecuteAsync(null!);
        }

        private void KodeJenisBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KodeKeperluan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KodeDebet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void KodeKredit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
