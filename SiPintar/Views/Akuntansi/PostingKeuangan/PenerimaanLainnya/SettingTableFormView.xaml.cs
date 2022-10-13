using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PenerimaanLainnyaViewModel)DataContext;

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
                { "NoTrans", NoTrans.IsChecked },
                { "KodeWilayah", KodeWilayah.IsChecked },
                { "NamaWilayah", NamaWilayah.IsChecked },
                { "NamaDebet", NamaDebet.IsChecked },
                { "Uraian", Uraian.IsChecked },
                { "WaktuInput", WaktuInput.IsChecked },
                { "JumlahNominal", JumlahNominal.IsChecked },
                { "KodeKredit", KodeKredit.IsChecked },
                { "NamaKredit", NamaKredit.IsChecked }
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (CheckSelected)
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) => CheckButton();

        private bool CheckSelected
        {
            get
            {
                var isSelected = false;

                if (NoTrans.IsChecked == true)
                    isSelected = true;
                if (KodeWilayah.IsChecked == true)
                    isSelected = true;
                if (NamaWilayah.IsChecked == true)
                    isSelected = true;
                if (NamaDebet.IsChecked == true)
                    isSelected = true;
                if (Uraian.IsChecked == true)
                    isSelected = true;
                if (WaktuInput.IsChecked == true)
                    isSelected = true;
                if (JumlahNominal.IsChecked == true)
                    isSelected = true;
                if (KodeKredit.IsChecked == true)
                    isSelected = true;
                if (NamaKredit.IsChecked == true)
                    isSelected = true;

                return isSelected;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NoTrans.IsChecked = true;
            KodeWilayah.IsChecked = true;
            NamaWilayah.IsChecked = true;
            NamaDebet.IsChecked = true;
            Uraian.IsChecked = true;
            WaktuInput.IsChecked = true;
            JumlahNominal.IsChecked = true;
            KodeKredit.IsChecked = true;
            NamaKredit.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            NoTrans.IsChecked = false;
            KodeWilayah.IsChecked = false;
            NamaWilayah.IsChecked = false;
            NamaDebet.IsChecked = false;
            Uraian.IsChecked = false;
            WaktuInput.IsChecked = false;
            JumlahNominal.IsChecked = false;
            KodeKredit.IsChecked = false;
            NamaKredit.IsChecked = false;
        }
    }
}
