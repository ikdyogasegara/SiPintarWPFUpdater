using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut.TarifAirTangki
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly TarifAirTangkiViewModel _viewModel;

        public SettingTableFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (TarifAirTangkiViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            KodeTarif.IsChecked = false;
            Kategori.IsChecked = false;
            NamaTarif.IsChecked = false;
            BiayaAir.IsChecked = false;
            BiayaAdministrasi.IsChecked = false;
            BiayaOperasional.IsChecked = false;
            BiayaTransport.IsChecked = false;
            Ppn.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            KodeTarif.IsChecked = true;
            Kategori.IsChecked = true;
            NamaTarif.IsChecked = true;
            BiayaAir.IsChecked = true;
            BiayaAdministrasi.IsChecked = true;
            BiayaOperasional.IsChecked = true;
            BiayaTransport.IsChecked = true;
            Ppn.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (KodeTarif.IsChecked == true)
                IsSelected = true;
            if (Kategori.IsChecked == true)
                IsSelected = true;
            if (NamaTarif.IsChecked == true)
                IsSelected = true;
            if (BiayaAir.IsChecked == true)
                IsSelected = true;
            if (BiayaAdministrasi.IsChecked == true)
                IsSelected = true;
            if (BiayaOperasional.IsChecked == true)
                IsSelected = true;
            if (BiayaTransport.IsChecked == true)
                IsSelected = true;
            if (Ppn.IsChecked == true)
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
                { "KodeTarif", KodeTarif.IsChecked },
                { "Kategori", Kategori.IsChecked },
                { "NamaTarif", NamaTarif.IsChecked },
                { "BiayaAir", BiayaAir.IsChecked },
                { "BiayaAdministrasi", BiayaAdministrasi.IsChecked },
                { "BiayaOperasional", BiayaOperasional.IsChecked },
                { "BiayaTransport", BiayaTransport.IsChecked },
                { "Ppn", Ppn.IsChecked }
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableCommand).ExecuteAsync(param));
        }
    }
}
