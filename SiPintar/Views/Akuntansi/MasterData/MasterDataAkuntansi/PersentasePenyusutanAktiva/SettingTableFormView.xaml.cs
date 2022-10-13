using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.PersentasePenyusutanAktiva
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly PersentasePenyusutanAktivaViewModel _viewModel;

        public SettingTableFormView(PersentasePenyusutanAktivaViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PersentasePenyusutanAktivaViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            GolAktiva.IsChecked = false;
            NamaGolAktiva.IsChecked = false;
            JumlahTahun.IsChecked = false;
            Persen.IsChecked = false;
            TipeAktiva.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            GolAktiva.IsChecked = true;
            NamaGolAktiva.IsChecked = true;
            JumlahTahun.IsChecked = true;
            Persen.IsChecked = true;
            TipeAktiva.IsChecked = true;
        }

        private bool CheckSelected()
        {
            var isSelected = false;

            if (GolAktiva.IsChecked == true)
                isSelected = true;
            if (NamaGolAktiva.IsChecked == true)
                isSelected = true;
            if (JumlahTahun.IsChecked == true)
                isSelected = true;
            if (Persen.IsChecked == true)
                isSelected = true;
            if (TipeAktiva.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            var isSelected = CheckSelected();

            if (isSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

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
                { "GolAktiva", GolAktiva.IsChecked },
                { "NamaGolAktiva", NamaGolAktiva.IsChecked },
                { "JumlahTahun", JumlahTahun.IsChecked },
                { "Persen", Persen.IsChecked },
                { "TipeAktiva", TipeAktiva.IsChecked },
            };

            _ = Task.Run(() => _viewModel.OnSubmitSettingTableFormCommand.Execute(param));
        }
    }
}
