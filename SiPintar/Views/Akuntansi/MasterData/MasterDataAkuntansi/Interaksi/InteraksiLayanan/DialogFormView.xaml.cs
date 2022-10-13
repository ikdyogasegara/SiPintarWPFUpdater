using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly InteraksiLayananViewModel _viewModel;
        public DialogFormView(InteraksiLayananViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (InteraksiLayananViewModel)DataContext;

            Title.Text = ((InteraksiLayananViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Koreksi ";

            if (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key == 0)
                Title.Text += "Interaksi Pelayanan Air";
            else
                Title.Text += "Interaksi Pelayanan Non Air";

            ToggleFlag.IsChecked = _viewModel.SelectedData?.FlagPembentukRekair ?? false;

            OkButton.Content = ((InteraksiLayananViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (_viewModel.Parent.IsAdd)
            {
                if (_viewModel.SelectedWilayah?.IdWilayah.HasValue != true)
                    OkButton.IsEnabled = false;
                else if (_viewModel.SelectedGolongan?.IdGolongan.HasValue != true)
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == true && _viewModel.SelectedPerkiraan3Debet?.IdPerkiraan3.HasValue != true)
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == true && _viewModel.SelectedPerkiraan3Kredit?.IdPerkiraan3.HasValue != true)
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == true && ToggleFlag.IsChecked == true && string.IsNullOrEmpty(_viewModel.SelectedPembentukRekAir.Value))
                    OkButton.IsEnabled = false;

                // Non Air
                else if (_viewModel.Parent.IsInPelayananAir == false && _viewModel.SelectedPerkiraan3NonAir?.IdPerkiraan3.HasValue != true)
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == false && string.IsNullOrEmpty(_viewModel.SelectedPembentukRekNonAir.Value))
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == false && _viewModel.SelectedJenisNonAir?.IdJenisNonAir.HasValue != true)
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(ComboKodeWilayah.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(ComboKodeGolongan.Text))
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == true && string.IsNullOrEmpty(ComboKodePerkiraan3Debet.Text))
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == true && string.IsNullOrEmpty(ComboKodePerkiraan3Kredit.Text))
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == true && ToggleFlag.IsChecked == true && string.IsNullOrEmpty(ComboKeterangan.Text))
                    OkButton.IsEnabled = false;

                // Non Air
                else if (_viewModel.Parent.IsInPelayananAir == false && string.IsNullOrEmpty(ComboKeterangan2.Text))
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == false && string.IsNullOrEmpty(ComboKodePerkiraan3.Text))
                    OkButton.IsEnabled = false;
                else if (_viewModel.Parent.IsInPelayananAir == false && string.IsNullOrEmpty(ComboKodeJenisNonAir.Text))
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHelper.CloseDialog(false, false, "AkuntansiRootDialog");

            //var data = new dynamic;
            dynamic data = new System.Dynamic.ExpandoObject();
            if (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key == 0)
            {
                data = new ParamMasterInPelayananAirDto();
                if (!_viewModel.Parent.IsAdd)
                {
                    data.IdInPelayananAir = _viewModel.SelectedData.IdInPelayananAir;
                    data.IdPdam = _viewModel.SelectedData.IdPdam;
                }

                data.IdWilayah = _viewModel.SelectedWilayah.IdWilayah;
                data.IdGolongan = _viewModel.SelectedGolongan.IdGolongan;
                data.IdPerkiraan3Debet = _viewModel.SelectedPerkiraan3Debet.IdPerkiraan3;
                data.IdPerkiraan3Kredit = _viewModel.SelectedPerkiraan3Kredit.IdPerkiraan3;
                data.FlagPembentukRekair = ComboKeterangan.IsEnabled == true ? true : false;
                data.Keterangan = _viewModel.SelectedPembentukRekAir.Value;
            }
            else if (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key == 1)
            {
                data = new ParamMasterInPelayananNonAirDto();
                if (!_viewModel.Parent.IsAdd)
                {
                    data.IdInPelayananNonAir = _viewModel.SelectedData.IdInPelayananNonAir;
                    data.IdPdam = _viewModel.SelectedData.IdPdam;
                }

                data.IdPerkiraan3 = _viewModel.SelectedPerkiraan3NonAir.IdPerkiraan3;
                data.Keterangan = _viewModel.SelectedPembentukRekNonAir.Value;
                data.IdWilayah = _viewModel.SelectedWilayah.IdWilayah;
                data.IdGolongan = _viewModel.SelectedGolongan.IdGolongan;
                data.IdJenisNonAir = _viewModel.SelectedJenisNonAir.IdJenisNonAir;
            }

            _viewModel.OnSubmitFormCommand.Execute(data);
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ComboKeterangan.IsEnabled = true;
            CheckButton();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ComboKeterangan.IsEnabled = false;
            CheckButton();
        }

        private void ComboKodeGolongan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

    }
}
