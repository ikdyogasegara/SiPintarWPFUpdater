using System.Collections.Generic;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.Paket
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PaketViewModel Vm;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as PaketViewModel;
            DataContext = Vm;

            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Paket";

            if (!Vm.IsAdd)
            {
                NamaPaket.Text = Vm.SelectedData.NamaPaketBarang;
            }

            TotalBarang.GotFocus += DecimalValidationHelper.Object_GotFocus;
            TotalBarang.LostFocus += DecimalValidationHelper.Object_LostFocus;
            TotalBarang.TextInput += DecimalValidationHelper.DecimalValidationTextBox;
            TambahCheck();
            SubmitCheck();
        }

        private void BtnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var Param = new ParamMasterBarangPaketDto()
            {
                NamaPaketBarang = NamaPaket.Text,
                Detail = new List<ParamMasterBarangPaketDetailDto>()
            };
            Vm.Data1.ForEach(x => Param.Detail.Add(new ParamMasterBarangPaketDetailDto()
            {
                IdBarang = x.IdBarang,
                Qty = x.Qty
            }));
            if (Vm.IsAdd)
            {
                Vm.OnSubmitAddFormCommand.Execute(Param);
            }
            else
            {
                Param.IdPaketBarang = Vm.SelectedData.IdPaketBarang;
                Vm.OnSubmitEditFormCommand.Execute(Param);
            }
        }

        private void TambahCheck()
        {
            BtnTambahBarang.IsEnabled = DecimalValidationHelper.FormatStringIdToDecimal(TotalBarang.Text) > decimal.Zero && DaftarBarang.SelectedItem != null;
        }

        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NamaPaket.Text) && Vm.Data1?.Count > 0;
        }

        private void DaftarBarang_SelectionChanged(object sender, SelectionChangedEventArgs e) => TambahCheck();
        private void TotalBarang_TextChanged(object sender, TextChangedEventArgs e) => TambahCheck();

        private void BtnTambahBarang_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Vm != null)
            {
                Vm.OnTambahBarangCommand.Execute(null);
                SubmitCheck();
            }
        }

        private void BtnHapus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Vm != null)
            {
                Vm.OnHapusBarangCommand.Execute(null);
                SubmitCheck();
            }
        }

        private void NamaPaket_TextChanged(object sender, TextChangedEventArgs e) => SubmitCheck();
        private void DataGridBarang_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e) => SubmitCheck();
    }
}
