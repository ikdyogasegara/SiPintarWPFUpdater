using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganL2T2
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PelangganL2T2ViewModel)DataContext;

            CheckButton();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "NoLltt", NoLltt.IsChecked },
                { "NoSamb", NoSambungan.IsChecked },
                { "Nama", Nama.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "KodeLltt", KodeLltt.IsChecked },
                { "GolLltt", GolonganLltt.IsChecked },
                { "Rayon", KodeRayon.IsChecked },
                { "Kelurahan", KodeKelurahan.IsChecked },
                { "Kolektif", KodeKolektif.IsChecked },
                { "NoTelp", NoTelp.IsChecked },
                { "NoHp", NoHp.IsChecked },
                { "Email", Email.IsChecked },
                { "Ktp", Ktp.IsChecked },
                { "Keterangan", Keterangan.IsChecked },
                { "Flag", Flag.IsChecked },
                { "Status", Status.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (NoLltt.IsChecked == true)
                IsSelected = true;
            if (NoSambungan.IsChecked == true)
                IsSelected = true;
            if (Nama.IsChecked == true)
                IsSelected = true;
            if (Alamat.IsChecked == true)
                IsSelected = true;
            if (KodeLltt.IsChecked == true)
                IsSelected = true;
            if (GolonganLltt.IsChecked == true)
                IsSelected = true;
            if (KodeRayon.IsChecked == true)
                IsSelected = true;
            if (KodeKelurahan.IsChecked == true)
                IsSelected = true;
            if (KodeKolektif.IsChecked == true)
                IsSelected = true;
            if (NoTelp.IsChecked == true)
                IsSelected = true;
            if (NoHp.IsChecked == true)
                IsSelected = true;
            if (Email.IsChecked == true)
                IsSelected = true;
            if (Ktp.IsChecked == true)
                IsSelected = true;
            if (Keterangan.IsChecked == true)
                IsSelected = true;
            if (Flag.IsChecked == true)
                IsSelected = true;
            if (Status.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NoLltt.IsChecked = true;
            NoSambungan.IsChecked = true;
            Nama.IsChecked = true;
            Alamat.IsChecked = true;
            KodeLltt.IsChecked = true;
            GolonganLltt.IsChecked = true;
            KodeRayon.IsChecked = true;
            KodeKelurahan.IsChecked = true;
            KodeKolektif.IsChecked = true;
            NoTelp.IsChecked = true;
            NoHp.IsChecked = true;
            Email.IsChecked = true;
            Ktp.IsChecked = true;
            Keterangan.IsChecked = true;
            Flag.IsChecked = true;
            Status.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            NoLltt.IsChecked = false;
            NoSambungan.IsChecked = false;
            Nama.IsChecked = false;
            Alamat.IsChecked = false;
            KodeLltt.IsChecked = false;
            GolonganLltt.IsChecked = false;
            KodeRayon.IsChecked = false;
            KodeKelurahan.IsChecked = false;
            KodeKolektif.IsChecked = false;
            NoTelp.IsChecked = false;
            NoHp.IsChecked = false;
            Email.IsChecked = false;
            Ktp.IsChecked = false;
            Keterangan.IsChecked = false;
            Flag.IsChecked = false;
            Status.IsChecked = false;
        }
    }
}
