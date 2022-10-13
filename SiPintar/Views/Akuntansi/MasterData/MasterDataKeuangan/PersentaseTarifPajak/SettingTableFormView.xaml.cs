using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;


namespace SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    /// <summary>
    /// Interaction logic for SettingTableFormView.xaml
    /// </summary>
    public partial class SettingTableFormView : UserControl
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;

        public SettingTableFormView(PersentaseTarifPajakViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PersentaseTarifPajakViewModel)DataContext;

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            KodePersen.IsChecked = false;
            NamaPersen.IsChecked = false;
            JumlahPersen.IsChecked = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            KodePersen.IsChecked = true;
            NamaPersen.IsChecked = true;
            JumlahPersen.IsChecked = true;
        }

        private bool CheckSelected()
        {
            bool IsSelected = false;

            if (KodePersen.IsChecked == true)
                IsSelected = true;
            if (NamaPersen.IsChecked == true)
                IsSelected = true;
            if (JumlahPersen.IsChecked == true)
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
                { "KodePersen", KodePersen.IsChecked },
                { "NamaPersen", NamaPersen.IsChecked },
                { "JumlahPersen", JumlahPersen.IsChecked },
            };

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitSettingTableFormCommand).ExecuteAsync(param));
        }
    }
}
