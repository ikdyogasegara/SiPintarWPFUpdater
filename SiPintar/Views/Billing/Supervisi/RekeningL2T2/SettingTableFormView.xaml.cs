using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningL2T2
{
    public partial class SettingTableFormView : UserControl
    {
        private readonly RekeningL2T2ViewModel _viewModel;
        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningL2T2ViewModel)DataContext;

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
                { "Publish", Publish.IsChecked },
                { "NoLltt", NoLltt.IsChecked },
                { "NoSamb", NoSamb.IsChecked },
                { "Nama", Nama.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "Biaya", Biaya.IsChecked },
                { "Upload", Upload.IsChecked },
                { "KodeLltt", KodeLltt.IsChecked },
                { "TarifLltt", TarifLltt.IsChecked },
                { "KodeRayon", KodeRayon.IsChecked },
                { "Rayon", Rayon.IsChecked },
                { "Lunas", Lunas.IsChecked },
                { "TglBayar", TglBayar.IsChecked },
                { "KodeWilayah", KodeWilayah.IsChecked },
                { "Wilayah", Wilayah.IsChecked },
                { "WaktuPublish", WaktuPublish.IsChecked },
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

            if (Publish.IsChecked == true)
                IsSelected = true;
            if (NoLltt.IsChecked == true)
                IsSelected = true;
            if (NoSamb.IsChecked == true)
                IsSelected = true;
            if (Nama.IsChecked == true)
                IsSelected = true;
            if (Alamat.IsChecked == true)
                IsSelected = true;
            if (Biaya.IsChecked == true)
                IsSelected = true;
            if (Upload.IsChecked == true)
                IsSelected = true;
            if (KodeLltt.IsChecked == true)
                IsSelected = true;
            if (TarifLltt.IsChecked == true)
                IsSelected = true;
            if (KodeRayon.IsChecked == true)
                IsSelected = true;
            if (Rayon.IsChecked == true)
                IsSelected = true;
            if (Lunas.IsChecked == true)
                IsSelected = true;
            if (TglBayar.IsChecked == true)
                IsSelected = true;
            if (KodeWilayah.IsChecked == true)
                IsSelected = true;
            if (Wilayah.IsChecked == true)
                IsSelected = true;
            if (WaktuPublish.IsChecked == true)
                IsSelected = true;
            if (Flag.IsChecked == true)
                IsSelected = true;
            if (Status.IsChecked == true)
                IsSelected = true;

            return IsSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Publish.IsChecked = true;
            NoLltt.IsChecked = true;
            NoSamb.IsChecked = true;
            Nama.IsChecked = true;
            Alamat.IsChecked = true;
            Biaya.IsChecked = true;
            Upload.IsChecked = true;
            KodeLltt.IsChecked = true;
            TarifLltt.IsChecked = true;
            KodeRayon.IsChecked = true;
            Rayon.IsChecked = true;
            Lunas.IsChecked = true;
            TglBayar.IsChecked = true;
            KodeWilayah.IsChecked = true;
            Wilayah.IsChecked = true;
            WaktuPublish.IsChecked = true;
            Flag.IsChecked = true;
            Status.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Publish.IsChecked = false;
            NoLltt.IsChecked = false;
            NoSamb.IsChecked = false;
            Nama.IsChecked = false;
            Alamat.IsChecked = false;
            Biaya.IsChecked = false;
            Upload.IsChecked = false;
            KodeLltt.IsChecked = false;
            TarifLltt.IsChecked = false;
            KodeRayon.IsChecked = false;
            Rayon.IsChecked = false;
            Lunas.IsChecked = false;
            TglBayar.IsChecked = false;
            KodeWilayah.IsChecked = false;
            Wilayah.IsChecked = false;
            WaktuPublish.IsChecked = false;
            Flag.IsChecked = false;
            Status.IsChecked = false;
        }
    }
}
