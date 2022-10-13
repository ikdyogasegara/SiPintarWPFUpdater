using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Distribusi.Distribusi;

namespace SiPintar.Views.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    /// <summary>
    /// Interaction logic for DialogFormAddRotasiMeterView.xaml
    /// </summary>
    public partial class DialogFormAddRotasiMeterView : UserControl
    {
        private readonly GantiMeterViewModel ViewModel;

        public DialogFormAddRotasiMeterView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as GantiMeterViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            ViewModel.CurrentStep = 1;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (GantiMeterViewModel)DataContext;

            if (_viewModel.CurrentStep == 1)
                DialogHost.CloseDialogCommand.Execute(null, null);
            else if (_viewModel.CurrentStep == 2)
                _viewModel.CurrentStep = 1;
            else
                _viewModel.CurrentStep = 2;
        }

        private void Select_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            var item = DataGridContent.SelectedItem;
            var _viewModel = (GantiMeterViewModel)DataContext;

            _viewModel.SelectedPelanggan = item as MasterPelangganAirDto;
            //_viewModel.CurrentStep = 2;
            //OkButton.IsEnabled = false;

            _ = ((AsyncCommandBase)_viewModel.OnShowConfirmSubmitCommand).ExecuteAsync(null);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
