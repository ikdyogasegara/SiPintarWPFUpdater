using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Langganan
{
    public partial class TableSettingFormView : UserControl
    {
        private readonly LanggananViewModel _viewModel;
        public TableSettingFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (LanggananViewModel)DataContext;

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
                { "Nama", Nama.IsChecked },
                { "Alamat", Alamat.IsChecked },
                { "Rayon", Rayon.IsChecked },
                { "Area", Area.IsChecked },
                { "Wilayah", Wilayah.IsChecked },
                { "Kelurahan", Kelurahan.IsChecked },
                { "Kecamatan", Kecamatan.IsChecked },
                { "Cabang", Cabang.IsChecked },
                { "Kolektif", Kolektif.IsChecked },
                { "Flag", Flag.IsChecked },
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

            if (Nama.IsChecked == true)
                isSelected = true;
            if (Alamat.IsChecked == true)
                isSelected = true;
            if (Rayon.IsChecked == true)
                isSelected = true;
            if (Area.IsChecked == true)
                isSelected = true;
            if (Wilayah.IsChecked == true)
                isSelected = true;
            if (Kelurahan.IsChecked == true)
                isSelected = true;
            if (Kecamatan.IsChecked == true)
                isSelected = true;
            if (Cabang.IsChecked == true)
                isSelected = true;
            if (Kolektif.IsChecked == true)
                isSelected = true;
            if (Flag.IsChecked == true)
                isSelected = true;

            return isSelected;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Nama.IsChecked = true;
            Alamat.IsChecked = true;
            Rayon.IsChecked = true;
            Area.IsChecked = true;
            Wilayah.IsChecked = true;
            Kelurahan.IsChecked = true;
            Kecamatan.IsChecked = true;
            Cabang.IsChecked = true;
            Kolektif.IsChecked = true;
            Flag.IsChecked = true;
        }

        private void Kosongkan_Click(object sender, RoutedEventArgs e)
        {
            Nama.IsChecked = false;
            Alamat.IsChecked = false;
            Rayon.IsChecked = false;
            Area.IsChecked = false;
            Wilayah.IsChecked = false;
            Kelurahan.IsChecked = false;
            Kecamatan.IsChecked = false;
            Cabang.IsChecked = false;
            Kolektif.IsChecked = false;
            Flag.IsChecked = false;
        }
    }
}
