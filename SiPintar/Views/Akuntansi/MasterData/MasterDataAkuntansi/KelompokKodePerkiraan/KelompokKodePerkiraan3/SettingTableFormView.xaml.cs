using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly KelompokKodePerkiraan3ViewModel _viewModel;

        public SettingTableFormView(KelompokKodePerkiraan3ViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (KelompokKodePerkiraan3ViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            KodePerkiraan.IsChecked = false;
            NamaPerkiraan.IsChecked = false;
            KodeAkunEtap.IsChecked = false;
            NamaAkunEtap.IsChecked = false;
            KodeGolAktiva.IsChecked = false;
            NamaGolAktiva.IsChecked = false;
            KodeJenisVoucher.IsChecked = false;
            NamaJenisVoucher.IsChecked = false;

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            KodePerkiraan.IsChecked = true;
            NamaPerkiraan.IsChecked = true;
            KodeAkunEtap.IsChecked = true;
            NamaAkunEtap.IsChecked = true;
            KodeGolAktiva.IsChecked = true;
            NamaGolAktiva.IsChecked = true;
            KodeJenisVoucher.IsChecked = true;
            NamaJenisVoucher.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (KodePerkiraan.IsChecked == true)
                IsSelected = true;
            if (NamaPerkiraan.IsChecked == true)
                IsSelected = true;
            if (KodeAkunEtap.IsChecked == true)
                IsSelected = true;
            if (NamaAkunEtap.IsChecked == true)
                IsSelected = true;
            if (KodeGolAktiva.IsChecked == true)
                IsSelected = true;
            if (NamaGolAktiva.IsChecked == true)
                IsSelected = true;
            if (KodeJenisVoucher.IsChecked == true)
                IsSelected = true;
            if (NamaJenisVoucher.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            bool IsSelected = CheckSelected();

            if (IsSelected)
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
                { "KodePerkiraan", KodePerkiraan.IsChecked },
                { "NamaPerkiraan", NamaPerkiraan.IsChecked },
                { "KodeAkunEtap", KodeAkunEtap.IsChecked },
                { "NamaAkunEtap", NamaAkunEtap.IsChecked },
                { "KodeGolAktiva", KodeGolAktiva.IsChecked },
                { "NamaGolAktiva", NamaGolAktiva.IsChecked },
                { "KodeJenisVoucher", KodeJenisVoucher.IsChecked },
                { "NamaJenisVoucher", NamaJenisVoucher.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableFormCommand).ExecuteAsync(param));
        }
    }
}
