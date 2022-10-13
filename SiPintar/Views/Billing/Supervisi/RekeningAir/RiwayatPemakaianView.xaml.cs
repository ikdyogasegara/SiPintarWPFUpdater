using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    /// <summary>
    /// Interaction logic for RiwayatPemakaianView.xaml
    /// </summary>
    public partial class RiwayatPemakaianView : UserControl
    {
        private readonly RekeningAirViewModel _vm;
        public RiwayatPemakaianView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as RekeningAirViewModel;
            DataContext = _vm;
            PreviewKeyUp += new KeyEventHandler(HandleKey);
        }

        private void HandleKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void Tahun_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => GetData();

        private void GetData()
        {

        }

    }
}
