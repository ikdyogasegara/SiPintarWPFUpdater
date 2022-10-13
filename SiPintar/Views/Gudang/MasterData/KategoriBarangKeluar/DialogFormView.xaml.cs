using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.KategoriBarangKeluar
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly KategoriBarangKeluarViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as KategoriBarangKeluarViewModel;
            DataContext = Vm;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Kategori Barang Keluar";
            if (!Vm.IsAdd)
            {
                NamaKategori.Text = Vm.SelectedData.Kategori;
                KodeNomor.Text = Vm.SelectedData.KodeNomor;
            }
            PreviewKeyUp += DialogFormView_PreviewKeyUp;
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
                new ParamMasterKategoriBarangKeluarDto()
                {
                    Kategori = NamaKategori.Text,
                    KodeNomor = KodeNomor.Text
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                    new ParamMasterKategoriBarangKeluarDto()
                    {
                        IdKategoriBarangKeluar = Vm.SelectedData.IdKategoriBarangKeluar,
                        Kategori = NamaKategori.Text,
                        KodeNomor = KodeNomor.Text
                    });
            }
        }
        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NamaKategori.Text) && !string.IsNullOrWhiteSpace(KodeNomor.Text);
        }
    }
}
