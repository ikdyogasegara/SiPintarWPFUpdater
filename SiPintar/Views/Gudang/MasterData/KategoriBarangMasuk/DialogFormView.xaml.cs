using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.KategoriBarangMasuk
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly KategoriBarangMasukViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as KategoriBarangMasukViewModel;
            DataContext = Vm;
            Vm.Flag = null;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Kategori Barang Masuk";
            if (!Vm.IsAdd)
            {
                NamaKategori.Text = Vm.SelectedData.Kategori;
                Ppn.Text = DecimalValidationHelper.FormatDecimalToStringId(Vm.SelectedData.Ppn);
                Vm.Flag = Vm.DaftarFlag.FirstOrDefault(x => x.Key == Vm.SelectedData.Flag.Value);
            }
            PreviewKeyUp += DialogFormView_PreviewKeyUp;
            Ppn.LostFocus += DecimalValidationHelper.Object_LostFocus;
            Ppn.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Ppn.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            SubmitCheck();
        }

        private void DialogFormView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void ButtonSimpan_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Vm.IsAdd)
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitAddFormCommand).ExecuteAsync(
                new ParamMasterKategoriBarangMasukDto()
                {
                    Kategori = NamaKategori.Text,
                    Ppn = DecimalValidationHelper.FormatStringIdToDecimal(Ppn.Text),
                    Flag = ((KeyValuePair<bool, string>)Flag.SelectedItem).Key
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                    new ParamMasterKategoriBarangMasukDto()
                    {
                        IdKategoriBarangMasuk = Vm.SelectedData.IdKategoriBarangMasuk,
                        Kategori = NamaKategori.Text,
                        Ppn = DecimalValidationHelper.FormatStringIdToDecimal(Ppn.Text),
                        Flag = ((KeyValuePair<bool, string>)Flag.SelectedItem).Key
                    });
            }
        }
        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void Flag_SelectionChanged(object sender, SelectionChangedEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NamaKategori.Text) && !string.IsNullOrWhiteSpace(Ppn.Text) && Flag.SelectedItem != null;
        }
    }
}
