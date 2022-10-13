using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Views.Gudang.MasterData.Supplier
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly SupplierViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as SupplierViewModel;
            DataContext = Vm;
            Title.Text = $"{(Vm.IsAdd ? "Tambah" : "Koreksi")} Suplier";
            if (!Vm.IsAdd)
            {
                KodeSuplier.Text = Vm.SelectedData.KodeSuplier;
                NamaContactPerson.Text = Vm.SelectedData.NamaContactPerson;
                NamaPerusahaan.Text = Vm.SelectedData.NamaPerusahaan;
                Jabatan.Text = Vm.SelectedData.Jabatan;
                Alamat.Text = Vm.SelectedData.Alamat;
                NPWP.Text = Vm.SelectedData.Npwp;
                NoTelp.Text = Vm.SelectedData.NoTelp;
                NoHp.Text = Vm.SelectedData.NoHp;
                Email.Text = Vm.SelectedData.Email;
                Rating.Value = Vm.SelectedData.Rating ?? 0;
                Notes.Text = Vm.SelectedData.Notes;
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
                new ParamMasterSuplierDto()
                {
                    KodeSuplier = KodeSuplier.Text,
                    NamaContactPerson = NamaContactPerson.Text,
                    NamaPerusahaan = NamaPerusahaan.Text,
                    Jabatan = Jabatan.Text,
                    Alamat = Alamat.Text,
                    Npwp = NPWP.Text,
                    NoTelp = NoTelp.Text,
                    NoHp = NoHp.Text,
                    Email = Email.Text,
                    Rating = Rating.Value,
                    Notes = Notes.Text
                });
            }
            else
            {
                _ = ((AsyncCommandBase)Vm.OnSubmitEditFormCommand).ExecuteAsync(
                new ParamMasterSuplierDto()
                {
                    IdSuplier = Vm.SelectedData.IdSuplier,
                    KodeSuplier = KodeSuplier.Text,
                    NamaContactPerson = NamaContactPerson.Text,
                    NamaPerusahaan = NamaPerusahaan.Text,
                    Jabatan = Jabatan.Text,
                    Alamat = Alamat.Text,
                    Npwp = NPWP.Text,
                    NoTelp = NoTelp.Text,
                    NoHp = NoHp.Text,
                    Email = Email.Text,
                    Rating = Rating.Value,
                    Notes = Notes.Text
                });
            }
        }
        private void Text_PreviewKeyUp(object sender, KeyEventArgs e) => SubmitCheck();
        private void SubmitCheck()
        {
            BtnSubmit.IsEnabled = !string.IsNullOrWhiteSpace(KodeSuplier.Text) &&
                !string.IsNullOrWhiteSpace(NamaContactPerson.Text) &&
                !string.IsNullOrWhiteSpace(NamaPerusahaan.Text) &&
                !string.IsNullOrWhiteSpace(Jabatan.Text) &&
                !string.IsNullOrWhiteSpace(Alamat.Text) &&
                !string.IsNullOrWhiteSpace(NPWP.Text) &&
                !string.IsNullOrWhiteSpace(NoTelp.Text) &&
                !string.IsNullOrWhiteSpace(NoHp.Text) &&
                !string.IsNullOrWhiteSpace(Email.Text) &&
                !string.IsNullOrWhiteSpace(Notes.Text);
        }
    }
}
