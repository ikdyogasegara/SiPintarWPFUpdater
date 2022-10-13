using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class TableSettingFormView : UserControl
    {
        private readonly PendaftaranViewModel _viewModel;
        public TableSettingFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PendaftaranViewModel)DataContext;

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
                { "Flag", Flag.IsChecked },
                { "NomorPendaftaran", NomorPendaftaran.IsChecked },
                { "Tanggal", Tanggal.IsChecked },
                { "Nama", Nama.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "NoHandphone", NoHandphone.IsChecked },
                { "NoSambDepan", NoSambDepan.IsChecked },
                { "NoSambBelakang", NoSambBelakang.IsChecked },
                { "NoSambKiri", NoSambKiri.IsChecked },
                { "NoSambKanan", NoSambKanan.IsChecked },
                { "Cabang", Cabang.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Penghuni", Penghuni.IsChecked },
                { "JenisBangunan", JenisBangunan.IsChecked },
                { "Pekerjaan", Pekerjaan.IsChecked },
                { "Biaya", Biaya.IsChecked },
                { "Tipe", Tipe.IsChecked },
                { "User", User.IsChecked },
                { "NomorRAB", NomorRab.IsChecked },
                { "TanggalRAB", TanggalRab.IsChecked },
                { "NomorSPPRAB", NomorSpprab.IsChecked },
                { "TanggalSPPRAB", TanggalSpprab.IsChecked },
                { "NomorBA", NomorBa.IsChecked },
                { "NomorSPKO", NomorSpko.IsChecked },
                { "NomorSPKP", NomorSpkp.IsChecked },
                { "NomorSPA", NomorSpa.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitTableSettingCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var isSelected = CheckSelected();

            if (isSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected()
        {
            var isSelected = false;

            if (Flag.IsChecked == true)
                isSelected = true;
            if (NomorPendaftaran.IsChecked == true)
                isSelected = true;
            if (Tanggal.IsChecked == true)
                isSelected = true;
            if (Nama.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;
            if (NoHandphone.IsChecked == true)
                isSelected = true;
            if (NoSambDepan.IsChecked == true)
                isSelected = true;
            if (NoSambBelakang.IsChecked == true)
                isSelected = true;
            if (NoSambKiri.IsChecked == true)
                isSelected = true;
            if (NoSambKanan.IsChecked == true)
                isSelected = true;
            if (Cabang.IsChecked == true)
                isSelected = true;
            if (Kelurahan.IsChecked == true)
                isSelected = true;
            if (Penghuni.IsChecked == true)
                isSelected = true;
            if (JenisBangunan.IsChecked == true)
                isSelected = true;
            if (Pekerjaan.IsChecked == true)
                isSelected = true;
            if (Biaya.IsChecked == true)
                isSelected = true;
            if (Tipe.IsChecked == true)
                isSelected = true;
            if (User.IsChecked == true)
                isSelected = true;
            if (NomorRab.IsChecked == true)
                isSelected = true;
            if (TanggalRab.IsChecked == true)
                isSelected = true;
            if (NomorSpprab.IsChecked == true)
                isSelected = true;
            if (TanggalSpprab.IsChecked == true)
                isSelected = true;
            if (NomorBa.IsChecked == true)
                isSelected = true;
            if (NomorSpko.IsChecked == true)
                isSelected = true;
            if (NomorSpkp.IsChecked == true)
                isSelected = true;
            if (NomorSpa.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Flag.IsChecked = true;
            NomorPendaftaran.IsChecked = true;
            Tanggal.IsChecked = true;
            Nama.IsChecked = true;
            Alamat.IsChecked = true;
            NoHandphone.IsChecked = true;
            NoSambDepan.IsChecked = true;
            NoSambBelakang.IsChecked = true;
            NoSambKiri.IsChecked = true;
            NoSambKanan.IsChecked = true;
            Cabang.IsChecked = true;
            Kelurahan.IsChecked = true;
            Penghuni.IsChecked = true;
            JenisBangunan.IsChecked = true;
            Pekerjaan.IsChecked = true;
            Biaya.IsChecked = true;
            Tipe.IsChecked = true;
            User.IsChecked = true;
            NomorRab.IsChecked = true;
            TanggalRab.IsChecked = true;
            NomorSpprab.IsChecked = true;
            TanggalSpprab.IsChecked = true;
            NomorBa.IsChecked = true;
            NomorSpko.IsChecked = true;
            NomorSpkp.IsChecked = true;
            NomorSpa.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Flag.IsChecked = false;
            NomorPendaftaran.IsChecked = false;
            Tanggal.IsChecked = false;
            Nama.IsChecked = false;
            Alamat.IsChecked = false;
            NoHandphone.IsChecked = false;
            NoSambDepan.IsChecked = false;
            NoSambBelakang.IsChecked = false;
            NoSambKiri.IsChecked = false;
            NoSambKanan.IsChecked = false;
            Cabang.IsChecked = false;
            Kelurahan.IsChecked = false;
            Penghuni.IsChecked = false;
            JenisBangunan.IsChecked = false;
            Pekerjaan.IsChecked = false;
            Biaya.IsChecked = false;
            Tipe.IsChecked = false;
            User.IsChecked = false;
            NomorRab.IsChecked = false;
            TanggalRab.IsChecked = false;
            NomorSpprab.IsChecked = false;
            TanggalSpprab.IsChecked = false;
            NomorBa.IsChecked = false;
            NomorSpko.IsChecked = false;
            NomorSpkp.IsChecked = false;
            NomorSpa.IsChecked = false;
        }
    }
}
