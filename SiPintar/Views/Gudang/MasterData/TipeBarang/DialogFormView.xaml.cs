using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.TipeBarang
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly TipeBarangViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as TipeBarangViewModel;
            DataContext = Vm;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Tipe Barang";
            if (!Vm.IsAdd)
            {
                NamaTipeBarang.Text = Vm.SelectedData.NamaTipeBarang;
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
                _ = ((AsyncCommandBase)Vm.OnSubmitAddFormCommand).ExecuteAsync(NamaTipeBarang.Text);
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(NamaTipeBarang.Text);
            }
        }

        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(NamaTipeBarang.Text) && true;
        }
    }
}
