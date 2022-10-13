using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganLimbah
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly PelangganLimbahViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PelangganLimbahViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            var param = new Dictionary<string, bool?>
            {
                { "NoLimbah", NoLimbah.IsChecked },
                { "NoSamb", NoSambungan.IsChecked },
                { "Nama", Nama.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "KodeLimbah", KodeLimbah.IsChecked },
                { "TarifLimbah", GolonganLimbah.IsChecked },
                { "Rayon", Rayon.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Kolektif", Kolektif.IsChecked },
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

            if (NoLimbah.IsChecked == true)
                IsSelected = true;
            if (NoSambungan.IsChecked == true)
                IsSelected = true;
            if (Nama.IsChecked == true)
                IsSelected = true;
            if (Alamat.IsChecked == true)
                IsSelected = true;
            if (KodeLimbah.IsChecked == true)
                IsSelected = true;
            if (GolonganLimbah.IsChecked == true)
                IsSelected = true;
            if (Rayon.IsChecked == true)
                IsSelected = true;
            if (Kelurahan.IsChecked == true)
                IsSelected = true;
            if (Kolektif.IsChecked == true)
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
            NoLimbah.IsChecked = true;
            NoSambungan.IsChecked = true;
            Nama.IsChecked = true;
            Alamat.IsChecked = true;
            KodeLimbah.IsChecked = true;
            GolonganLimbah.IsChecked = true;
            Rayon.IsChecked = true;
            Kelurahan.IsChecked = true;
            Kolektif.IsChecked = true;
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
            NoLimbah.IsChecked = false;
            NoSambungan.IsChecked = false;
            Nama.IsChecked = false;
            Alamat.IsChecked = false;
            KodeLimbah.IsChecked = false;
            GolonganLimbah.IsChecked = false;
            Rayon.IsChecked = false;
            Kelurahan.IsChecked = false;
            Kolektif.IsChecked = false;
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
