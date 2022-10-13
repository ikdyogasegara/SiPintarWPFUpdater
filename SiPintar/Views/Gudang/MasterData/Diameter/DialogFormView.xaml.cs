using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.Diameter
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly DiameterViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as DiameterViewModel;
            DataContext = Vm;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Diameter Barang";
            if (!Vm.IsAdd)
            {
                DiameterBarang.Text = Vm.SelectedData.DiameterBarang;
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
                new MasterDiameterBarangDto()
                {
                    DiameterBarang = DiameterBarang.Text
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                    new MasterDiameterBarangDto()
                    {
                        IdDiameterBarang = Vm.SelectedData.IdDiameterBarang,
                        DiameterBarang = DiameterBarang.Text
                    });
            }
        }
        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(DiameterBarang.Text);
        }
    }
}
