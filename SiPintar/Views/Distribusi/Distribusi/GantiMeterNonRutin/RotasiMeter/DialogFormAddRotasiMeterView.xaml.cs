using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public partial class DialogFormAddRotasiMeterView : UserControl
    {
        private readonly GantiMeterNonRutinViewModel ViewModel;

        public DialogFormAddRotasiMeterView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as GantiMeterNonRutinViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Select_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            var item = DataGridContent.SelectedItem;
            var _viewModel = (GantiMeterNonRutinViewModel)DataContext;

            _viewModel.SelectedPelanggan = item as MasterPelangganAirDto;
            _ = ((AsyncCommandBase)_viewModel.OnShowConfirmSubmitCommand).ExecuteAsync(null);
        }

    }
}

