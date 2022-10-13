using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;

        public SettingTableFormView(InteraksiJenisPersediaanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (InteraksiJenisPersediaanViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            KodeJenisBarang.IsChecked = false;
            NamaJenisBarang.IsChecked = false;
            KodeKeperluan.IsChecked = false;
            Keperluan.IsChecked = false;
            KodeDebet.IsChecked = false;
            NamaDebet.IsChecked = false;
            KodeKredit.IsChecked = false;
            NamaKredit.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            KodeJenisBarang.IsChecked = true;
            NamaJenisBarang.IsChecked = true;
            KodeKeperluan.IsChecked = true;
            Keperluan.IsChecked = true;
            KodeDebet.IsChecked = true;
            NamaDebet.IsChecked = true;
            KodeKredit.IsChecked = true;
            NamaKredit.IsChecked = true;
        }

        private bool CheckSelected
        {
            get
            {
                var isSelected = false;

                if (KodeJenisBarang.IsChecked == true)
                    isSelected = true;
                if (NamaJenisBarang.IsChecked == true)
                    isSelected = true;
                if (KodeKeperluan.IsChecked == true)
                    isSelected = true;
                if (Keperluan.IsChecked == true)
                    isSelected = true;
                if (KodeDebet.IsChecked == true)
                    isSelected = true;
                if (NamaDebet.IsChecked == true)
                    isSelected = true;
                if (KodeKredit.IsChecked == true)
                    isSelected = true;
                if (NamaKredit.IsChecked == true)
                    isSelected = true;

                return isSelected;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            OkButton.IsEnabled = CheckSelected;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, bool?>
            {
                { "KodeJenisBarang", KodeJenisBarang.IsChecked },
                { "NamaJenisBarang", NamaJenisBarang.IsChecked },
                { "KodeKeperluan", KodeKeperluan.IsChecked },
                { "Keperluan", Keperluan.IsChecked },
                { "KodeDebet", KodeDebet.IsChecked },
                { "NamaDebet", NamaDebet.IsChecked },
                { "KodeKredit", KodeKredit.IsChecked },
                { "NamaKredit", NamaKredit.IsChecked }

            };

            _ = Task.Run(() => (_viewModel.OnSubmitSettingTableFormCommand as AsyncCommandBase)!.ExecuteAsync(param));

            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
