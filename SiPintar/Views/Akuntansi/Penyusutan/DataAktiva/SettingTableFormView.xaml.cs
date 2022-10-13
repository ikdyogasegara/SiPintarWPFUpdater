using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.Views.Akuntansi.Penyusutan.DataAktiva
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly DataAktivaViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DataAktivaViewModel)DataContext;

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
                { "Uraian", Uraian.IsChecked },
                { "Tahun", Tahun.IsChecked },
                { "Lokasi", Lokasi.IsChecked },
                { "Asal", Asal.IsChecked },
                { "Tanggal", Tanggal.IsChecked },
                { "Nilai", Nilai.IsChecked },
                { "Mutasi", Mutasi.IsChecked },
                { "Perolehan", Perolehan.IsChecked },
                { "JmlTahun", JmlTahun.IsChecked },
                { "Persen", Persen.IsChecked },
                { "NilaiBukuLalu", NilaiBukuLalu.IsChecked },
                { "AkunPenyLalu", AkunPenyLalu.IsChecked },
                { "PenyusutanJanuari", PenyusutanJanuari.IsChecked },
                { "PenyusutanFebruari", PenyusutanFebruari.IsChecked },
                { "PenyusutanMaret", PenyusutanMaret.IsChecked },
                { "PenyusutanApril", PenyusutanApril.IsChecked },
                { "PenyusutanMei", PenyusutanMei.IsChecked },
                { "PenyusutanJuni", PenyusutanJuni.IsChecked },
                { "PenyusutanJuli", PenyusutanJuli.IsChecked },
                { "PenyusutanAgustus", PenyusutanAgustus.IsChecked },
                { "PenyusutanSeptember", PenyusutanSeptember.IsChecked },
                { "PenyusutanOktober", PenyusutanOktober.IsChecked },
                { "PenyusutanNovember", PenyusutanNovember.IsChecked },
                { "PenyusutanDesember", PenyusutanDesember.IsChecked },
                { "AkunPenyusutanJanuari", AkunPenyusutanJanuari.IsChecked },
                { "AkunPenyusutanFebruari", AkunPenyusutanFebruari.IsChecked },
                { "AkunPenyusutanMaret", AkunPenyusutanMaret.IsChecked },
                { "AkunPenyusutanApril", AkunPenyusutanApril.IsChecked },
                { "AkunPenyusutanMei", AkunPenyusutanMei.IsChecked },
                { "AkunPenyusutanJuni", AkunPenyusutanJuni.IsChecked },
                { "AkunPenyusutanJuli", AkunPenyusutanJuli.IsChecked },
                { "AkunPenyusutanAgustus", AkunPenyusutanAgustus.IsChecked },
                { "AkunPenyusutanSeptember", AkunPenyusutanSeptember.IsChecked },
                { "AkunPenyusutanOktober", AkunPenyusutanOktober.IsChecked },
                { "AkunPenyusutanNovember", AkunPenyusutanNovember.IsChecked },
                { "AkunPenyusutanDesember", AkunPenyusutanDesember.IsChecked },
                { "NilaiBukuJanuari", NilaiBukuJanuari.IsChecked },
                { "NilaiBukuFebruari", NilaiBukuFebruari.IsChecked },
                { "NilaiBukuMaret", NilaiBukuMaret.IsChecked },
                { "NilaiBukuApril", NilaiBukuApril.IsChecked },
                { "NilaiBukuMei", NilaiBukuMei.IsChecked },
                { "NilaiBukuJuni", NilaiBukuJuni.IsChecked },
                { "NilaiBukuJuli", NilaiBukuJuli.IsChecked },
                { "NilaiBukuAgustus", NilaiBukuAgustus.IsChecked },
                { "NilaiBukuSeptember", NilaiBukuSeptember.IsChecked },
                { "NilaiBukuOktober", NilaiBukuOktober.IsChecked },
                { "NilaiBukuNovember", NilaiBukuNovember.IsChecked },
                { "NilaiBukuDesember", NilaiBukuDesember.IsChecked }
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

            if (Uraian.IsChecked == true)
                IsSelected = true;
            if (Tahun.IsChecked == true)
                IsSelected = true;
            if (Lokasi.IsChecked == true)
                IsSelected = true;
            if (Asal.IsChecked == true)
                IsSelected = true;
            if (Tanggal.IsChecked == true)
                IsSelected = true;
            if (Nilai.IsChecked == true)
                IsSelected = true;
            if (Mutasi.IsChecked == true)
                IsSelected = true;
            if (Perolehan.IsChecked == true)
                IsSelected = true;
            if (JmlTahun.IsChecked == true)
                IsSelected = true;
            if (Persen.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuLalu.IsChecked == true)
                IsSelected = true;
            if (AkunPenyLalu.IsChecked == true)
                IsSelected = true;

            if (PenyusutanJanuari.IsChecked == true)
                IsSelected = true;
            if (PenyusutanFebruari.IsChecked == true)
                IsSelected = true;
            if (PenyusutanMaret.IsChecked == true)
                IsSelected = true;
            if (PenyusutanApril.IsChecked == true)
                IsSelected = true;
            if (PenyusutanMei.IsChecked == true)
                IsSelected = true;
            if (PenyusutanJuni.IsChecked == true)
                IsSelected = true;
            if (PenyusutanJuli.IsChecked == true)
                IsSelected = true;
            if (PenyusutanAgustus.IsChecked == true)
                IsSelected = true;
            if (PenyusutanSeptember.IsChecked == true)
                IsSelected = true;
            if (PenyusutanOktober.IsChecked == true)
                IsSelected = true;
            if (PenyusutanNovember.IsChecked == true)
                IsSelected = true;
            if (PenyusutanDesember.IsChecked == true)
                IsSelected = true;

            if (AkunPenyusutanJanuari.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanFebruari.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanMaret.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanApril.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanMei.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanJuni.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanJuli.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanAgustus.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanSeptember.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanOktober.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanNovember.IsChecked == true)
                IsSelected = true;
            if (AkunPenyusutanDesember.IsChecked == true)
                IsSelected = true;

            if (NilaiBukuJanuari.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuFebruari.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuMaret.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuApril.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuMei.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuJuni.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuJuli.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuAgustus.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuSeptember.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuOktober.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuNovember.IsChecked == true)
                IsSelected = true;
            if (NilaiBukuDesember.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Uraian.IsChecked = true;
            Tahun.IsChecked = true;
            Lokasi.IsChecked = true;
            Asal.IsChecked = true;
            Tanggal.IsChecked = true;
            Nilai.IsChecked = true;
            Mutasi.IsChecked = true;
            Perolehan.IsChecked = true;
            JmlTahun.IsChecked = true;
            Persen.IsChecked = true;
            NilaiBukuLalu.IsChecked = true;
            AkunPenyLalu.IsChecked = true;
            PenyusutanJanuari.IsChecked = true;
            PenyusutanFebruari.IsChecked = true;
            PenyusutanMaret.IsChecked = true;
            PenyusutanApril.IsChecked = true;
            PenyusutanMei.IsChecked = true;
            PenyusutanJuni.IsChecked = true;
            PenyusutanJuli.IsChecked = true;
            PenyusutanAgustus.IsChecked = true;
            PenyusutanSeptember.IsChecked = true;
            PenyusutanOktober.IsChecked = true;
            PenyusutanNovember.IsChecked = true;
            PenyusutanDesember.IsChecked = true;
            AkunPenyusutanJanuari.IsChecked = true;
            AkunPenyusutanFebruari.IsChecked = true;
            AkunPenyusutanMaret.IsChecked = true;
            AkunPenyusutanApril.IsChecked = true;
            AkunPenyusutanMei.IsChecked = true;
            AkunPenyusutanJuni.IsChecked = true;
            AkunPenyusutanJuli.IsChecked = true;
            AkunPenyusutanAgustus.IsChecked = true;
            AkunPenyusutanSeptember.IsChecked = true;
            AkunPenyusutanOktober.IsChecked = true;
            AkunPenyusutanNovember.IsChecked = true;
            AkunPenyusutanDesember.IsChecked = true;
            NilaiBukuJanuari.IsChecked = true;
            NilaiBukuFebruari.IsChecked = true;
            NilaiBukuMaret.IsChecked = true;
            NilaiBukuApril.IsChecked = true;
            NilaiBukuMei.IsChecked = true;
            NilaiBukuJuni.IsChecked = true;
            NilaiBukuJuli.IsChecked = true;
            NilaiBukuAgustus.IsChecked = true;
            NilaiBukuSeptember.IsChecked = true;
            NilaiBukuOktober.IsChecked = true;
            NilaiBukuNovember.IsChecked = true;
            NilaiBukuDesember.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Uraian.IsChecked = false;
            Tahun.IsChecked = false;
            Lokasi.IsChecked = false;
            Asal.IsChecked = false;
            Tanggal.IsChecked = false;
            Nilai.IsChecked = false;
            Mutasi.IsChecked = false;
            Perolehan.IsChecked = false;
            JmlTahun.IsChecked = false;
            Persen.IsChecked = false;
            NilaiBukuLalu.IsChecked = false;
            AkunPenyLalu.IsChecked = false;
            PenyusutanJanuari.IsChecked = false;
            PenyusutanFebruari.IsChecked = false;
            PenyusutanMaret.IsChecked = false;
            PenyusutanApril.IsChecked = false;
            PenyusutanMei.IsChecked = false;
            PenyusutanJuni.IsChecked = false;
            PenyusutanJuli.IsChecked = false;
            PenyusutanAgustus.IsChecked = false;
            PenyusutanSeptember.IsChecked = false;
            PenyusutanOktober.IsChecked = false;
            PenyusutanNovember.IsChecked = false;
            PenyusutanDesember.IsChecked = false;
            AkunPenyusutanJanuari.IsChecked = false;
            AkunPenyusutanFebruari.IsChecked = false;
            AkunPenyusutanMaret.IsChecked = false;
            AkunPenyusutanApril.IsChecked = false;
            AkunPenyusutanMei.IsChecked = false;
            AkunPenyusutanJuni.IsChecked = false;
            AkunPenyusutanJuli.IsChecked = false;
            AkunPenyusutanAgustus.IsChecked = false;
            AkunPenyusutanSeptember.IsChecked = false;
            AkunPenyusutanOktober.IsChecked = false;
            AkunPenyusutanNovember.IsChecked = false;
            AkunPenyusutanDesember.IsChecked = false;
            NilaiBukuJanuari.IsChecked = false;
            NilaiBukuFebruari.IsChecked = false;
            NilaiBukuMaret.IsChecked = false;
            NilaiBukuApril.IsChecked = false;
            NilaiBukuMei.IsChecked = false;
            NilaiBukuJuni.IsChecked = false;
            NilaiBukuJuli.IsChecked = false;
            NilaiBukuAgustus.IsChecked = false;
            NilaiBukuSeptember.IsChecked = false;
            NilaiBukuOktober.IsChecked = false;
            NilaiBukuNovember.IsChecked = false;
            NilaiBukuDesember.IsChecked = false;
        }
    }
}
