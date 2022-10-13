using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.BagianMemintaBarang
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly BagianMemintaBarangViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as BagianMemintaBarangViewModel;
            DataContext = Vm;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Bagian Meminta Barang";
            if (!Vm.IsAdd)
            {
                NamaBagianMemintaBarang.Text = Vm.SelectedData.NamaBagianMemintaBarang;
                Divisi.SelectedItem = Vm.DaftarDivisi.FirstOrDefault(x => x.IdDivisi == Vm.SelectedData.IdDivisi);
                Wilayah.SelectedItem = Vm.DaftarWilayah.FirstOrDefault(x => x.IdWilayah == Vm.SelectedData.IdWilayah);
                NamaTtd.Text = Vm.SelectedData.NamaTtd;
                JabatanTtd.Text = Vm.SelectedData.JabatanTtd;
                NikTtd.Text = Vm.SelectedData.NikTtd;
                NamaTtd2.Text = Vm.SelectedData.NamaTtd2;
                JabatanTtd2.Text = Vm.SelectedData.JabatanTtd2;
                NikTtd2.Text = Vm.SelectedData.NikTtd2;
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
                new ParamMasterBagianMemintaBarangDto()
                {
                    NamaBagianMemintaBarang = NamaBagianMemintaBarang.Text,
                    IdDivisi = ((MasterDivisiDto)Divisi.SelectedItem).IdDivisi,
                    IdWilayah = ((MasterWilayahDto)Wilayah.SelectedItem).IdWilayah,
                    NamaTtd = NamaTtd.Text,
                    JabatanTtd = JabatanTtd.Text,
                    NikTtd = NikTtd.Text,
                    NamaTtd2 = NamaTtd2.Text,
                    JabatanTtd2 = JabatanTtd2.Text,
                    NikTtd2 = NikTtd2.Text
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                    new ParamMasterBagianMemintaBarangDto()
                    {
                        IdBagianMemintaBarang = Vm.SelectedData.IdBagianMemintaBarang,
                        NamaBagianMemintaBarang = NamaBagianMemintaBarang.Text,
                        IdDivisi = ((MasterDivisiDto)Divisi.SelectedItem).IdDivisi,
                        IdWilayah = ((MasterWilayahDto)Wilayah.SelectedItem).IdWilayah,
                        NamaTtd = NamaTtd.Text,
                        JabatanTtd = JabatanTtd.Text,
                        NikTtd = NikTtd.Text,
                        NamaTtd2 = NamaTtd2.Text,
                        JabatanTtd2 = JabatanTtd2.Text,
                        NikTtd2 = NikTtd2.Text
                    });
            }
        }
        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NamaBagianMemintaBarang.Text) && Divisi.SelectedItem != null && Wilayah.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(NamaTtd.Text) && !string.IsNullOrWhiteSpace(JabatanTtd.Text) && !string.IsNullOrWhiteSpace(NikTtd.Text) &&
                !string.IsNullOrWhiteSpace(NamaTtd2.Text) && !string.IsNullOrWhiteSpace(JabatanTtd2.Text) && !string.IsNullOrWhiteSpace(NikTtd2.Text);
        }

    }
}
