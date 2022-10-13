using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class BppiFormView : UserControl
    {
        private readonly PermohonanViewModel _vm;

        public BppiFormView(object dataContext)
        {
            InitializeComponent();
            _vm = dataContext as PermohonanViewModel;
            DataContext = _vm;
            BtnSubmit.IsEnabled = false;
            TanggalBppi.SelectedDate = DateTime.Now;
            CheckSubmit();
            PreviewKeyUp += BppiFormView_PreviewKeyUp;
        }

        private void BppiFormView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, this);
        }

        private void Tanggal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckSubmit();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "IdPermohonan", _vm.SelectedData.IdPermohonan },
                { "TanggalBppi", TanggalBppi.SelectedDate },
            };

            _ = ((AsyncCommandBase)_vm.OnSubmitBppiFormCommand).ExecuteAsync(param);
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = TanggalBppi.SelectedDate != null;
        }
    }
}
