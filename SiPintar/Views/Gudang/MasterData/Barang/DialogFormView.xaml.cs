using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.Barang
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly BarangViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as BarangViewModel;
            Vm.KodeBarang = null;
            DataContext = Vm;
            Vm.FotoBarangFormUri = null;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Barang";
            if (!Vm.IsAdd)
            {
                BtnGenerate.IsEnabled = false;
                Vm.FotoBarangFormUri = Vm.FotoBarangUri;
                Vm.KodeBarang = Vm.SelectedData.KodeBarang;
                JenisBarang.SelectedItem = Vm.DaftarJenisBarang.FirstOrDefault(x => x.IdJenisBarang == Vm.SelectedData.IdJenisBarang);
                DiameterBarang.SelectedItem = Vm.DaftarDiameterBarang.FirstOrDefault(x => x.IdDiameterBarang == Vm.SelectedData.IdDiameterBarang);
                SeriBarang.Text = Vm.SelectedData.SeriBarang;
                SatuanBarang.SelectedItem = Vm.DaftarSatuanBarang.FirstOrDefault(x => x.IdSatuanBarang == Vm.SelectedData.IdSatuanBarang);
                MinQty.Text = IntegerValidationHelper.FormatIntegerToStringId(Vm.SelectedData.MinQty);
                MaxQty.Text = IntegerValidationHelper.FormatIntegerToStringId(Vm.SelectedData.MaxQty);
                HargaBeli.Text = DecimalValidationHelper.FormatDecimalToStringId(Vm.SelectedData.HargaBeli);
                HargaJual.Text = DecimalValidationHelper.FormatDecimalToStringId(Vm.SelectedData.HargaJual);
                KodeAkun.SelectedItem = Vm.DaftarKodeAkun.FirstOrDefault(x => x.IdKodeAkun == Vm.SelectedData.IdKodeAkun);
                NamaBarang.Text = Vm.SelectedData.NamaBarang;
                Loker.Text = Vm.SelectedData.Loker;
            }
            PreviewKeyUp += DialogFormView_PreviewKeyUp;
            MinQty.GotFocus += IntegerValidationHelper.Object_GotFocus;
            MinQty.LostFocus += IntegerValidationHelper.Object_LostFocus;
            MinQty.PreviewTextInput += IntegerValidationHelper.IntegerValidationTextBox;
            MaxQty.GotFocus += IntegerValidationHelper.Object_GotFocus;
            MaxQty.LostFocus += IntegerValidationHelper.Object_LostFocus;
            MaxQty.PreviewTextInput += IntegerValidationHelper.IntegerValidationTextBox;
            HargaBeli.GotFocus += DecimalValidationHelper.Object_GotFocus;
            HargaBeli.LostFocus += DecimalValidationHelper.Object_LostFocus;
            HargaBeli.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            HargaJual.GotFocus += DecimalValidationHelper.Object_GotFocus;
            HargaJual.LostFocus += DecimalValidationHelper.Object_LostFocus;
            HargaJual.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            SubmitCheck();
            CheckBtnGenerate();
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
                new ParamMasterBarangDto()
                {
                    KodeBarang = KodeBarang.Text,
                    NamaBarang = NamaBarang.Text,
                    SeriBarang = SeriBarang.Text,
                    IdJenisBarang = ((MasterJenisBarangDto)JenisBarang.SelectedItem).IdJenisBarang,
                    IdDiameterBarang = ((MasterDiameterBarangDto)DiameterBarang.SelectedItem).IdDiameterBarang,
                    IdSatuanBarang = ((MasterSatuanBarangDto)SatuanBarang.SelectedItem).IdSatuanBarang,
                    IdKodeAkun = ((MasterKodeAkunDto)KodeAkun.SelectedItem).IdKodeAkun,
                    MinQty = IntegerValidationHelper.FormatStringIdToInteger(MinQty.Text),
                    MaxQty = IntegerValidationHelper.FormatStringIdToInteger(MaxQty.Text),
                    HargaBeli = DecimalValidationHelper.FormatStringIdToDecimal(HargaBeli.Text),
                    HargaJual = DecimalValidationHelper.FormatStringIdToDecimal(HargaJual.Text),
                    Loker = Loker.Text
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                    new ParamMasterBarangDto()
                    {
                        IdBarang = Vm.SelectedData.IdBarang,
                        KodeBarang = KodeBarang.Text,
                        NamaBarang = NamaBarang.Text,
                        SeriBarang = SeriBarang.Text,
                        IdJenisBarang = ((MasterJenisBarangDto)JenisBarang.SelectedItem).IdJenisBarang,
                        IdDiameterBarang = ((MasterDiameterBarangDto)DiameterBarang.SelectedItem).IdDiameterBarang,
                        IdSatuanBarang = ((MasterSatuanBarangDto)SatuanBarang.SelectedItem).IdSatuanBarang,
                        IdKodeAkun = ((MasterKodeAkunDto)KodeAkun.SelectedItem).IdKodeAkun,
                        MinQty = IntegerValidationHelper.FormatStringIdToInteger(MinQty.Text),
                        MaxQty = IntegerValidationHelper.FormatStringIdToInteger(MaxQty.Text),
                        HargaBeli = DecimalValidationHelper.FormatStringIdToDecimal(HargaBeli.Text),
                        HargaJual = DecimalValidationHelper.FormatStringIdToDecimal(HargaJual.Text),
                        Loker = Loker.Text
                    });
            }
        }
        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(KodeBarang.Text) &&
                JenisBarang.SelectedItem != null &&
                DiameterBarang.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(SeriBarang.Text) &&
                SatuanBarang.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(MinQty.Text) &&
                !string.IsNullOrWhiteSpace(MaxQty.Text) &&
                !string.IsNullOrWhiteSpace(HargaBeli.Text) &&
                !string.IsNullOrWhiteSpace(HargaJual.Text) &&
                KodeAkun.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(Loker.Text) &&
                !string.IsNullOrWhiteSpace(NamaBarang.Text);
        }

        private void ButtonGenerateKodeBaarang_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (JenisBarang.SelectedItem != null)
            {
                var IdJenisBarang = ((MasterJenisBarangDto)JenisBarang.SelectedItem).IdJenisBarang as int?;
                Vm.OnGetKodeBarangCommand.Execute(IdJenisBarang);
            }
        }

        private void ButtonPilihGambar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _ = ((AsyncCommandBase)Vm.OnPilihFotoBarangCommand).ExecuteAsync(null);
        }

        private void CheckBtnGenerate()
        {
            BtnGenerate.IsEnabled = !Vm.IsAdd || (Vm.IsAdd && JenisBarang.SelectedItem != null);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckBtnGenerate();
            SubmitCheck();
        }
    }
}
