using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Distribusi.Atribut.KelainanGantiMeter;

namespace SiPintar.Views.Distribusi.Atribut.KelainanGantiMeter.GantiMeterNonRutin
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly GantiMeterNonRutinViewModel Vm;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            Vm = dataContext as GantiMeterNonRutinViewModel;
            RadioDenganBiaya.IsChecked = Vm.IsCheckedDenganBiaya;

            CheckButton();

            if (!Vm.IsAdd)
                TitleDialog.Text = $"Koreksi Jenis Kelainan Ganti Meter Non - Rutin {Vm.SelectedData?.JenisGantiMeter}";
            else
                TitleDialog.Text = $"Tambah Jenis Kelainan Ganti Meter Non - Rutin";
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();
        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();
        private void Radio_Click(object sender, RoutedEventArgs e) => CheckButton();


        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Kategori.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaKelainan.Text))
                OkButton.IsEnabled = false;
            else if ((RadioDenganBiaya.IsChecked == false) && (RadioTanpaBiaya.IsChecked == false))
                OkButton.IsEnabled = false;
            else if ((RadioDenganBiaya.IsChecked == true) && string.IsNullOrEmpty(Vm.JenisNonair?.NamaJenisNonAir))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(Vm.WarnaMeter?.WarnaGantiMeter))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Vm.IsCheckedDenganBiaya = true;
            PanelJenisBiaya.Visibility = Visibility.Visible;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Vm.IsCheckedDenganBiaya = false;
            PanelJenisBiaya.Visibility = Visibility.Collapsed;
        }
    }
}
