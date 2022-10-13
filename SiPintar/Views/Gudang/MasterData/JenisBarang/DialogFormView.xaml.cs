using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.JenisBarang
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly JenisBarangViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as JenisBarangViewModel;
            DataContext = Vm;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Jenis Barang";
            if (!Vm.IsAdd)
            {
                IdTipeBarang.SelectedItem = Vm.DataTipeBarang.FirstOrDefault(x => x.IdTipeBarang == Vm.SelectedData.IdTipeBarang);
                KodeTipeBarang.Text = Vm.SelectedData.KodeJenisBarang;
                NamaTipeBarang.Text = Vm.SelectedData.NamaJenisBarang;
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
                new MasterJenisBarangDto()
                {
                    IdTipeBarang = ((MasterTipeBarangDto)IdTipeBarang.SelectedItem).IdTipeBarang,
                    KodeJenisBarang = KodeTipeBarang.Text,
                    NamaJenisBarang = NamaTipeBarang.Text
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                    new MasterJenisBarangDto()
                    {
                        IdJenisBarang = Vm.SelectedData.IdJenisBarang,
                        IdTipeBarang = ((MasterTipeBarangDto)IdTipeBarang.SelectedItem).IdTipeBarang,
                        KodeJenisBarang = KodeTipeBarang.Text,
                        NamaJenisBarang = NamaTipeBarang.Text
                    });
            }
        }

        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = IdTipeBarang.SelectedItem != null && !string.IsNullOrWhiteSpace(KodeTipeBarang.Text) && !string.IsNullOrWhiteSpace(NamaTipeBarang.Text);
        }
    }
}
