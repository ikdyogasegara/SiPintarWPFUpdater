using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SiPintar.Commands;
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;

namespace SiPintar.Views.Akuntansi.Jurnal.Kas
{
    /// <summary>
    /// Interaction logic for PenerimaanKasView.xaml
    /// </summary>
    public partial class PenerimaanKasView : UserControl
    {
        public PenerimaanKasView()
        {
            InitializeComponent();
        }
        private void PeriodePosting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OkButton.IsEnabled = CheckButton();
        }

        private void TipeUraian_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OkButton.IsEnabled = CheckButton();
        }

        private bool CheckButton()
        {
            return PeriodePosting.SelectedItem != null && TipeUraian.SelectedItem != null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PenerimaanKasViewModel vm && CheckButton())
            {
                _ = (vm.OnCetakDataCommand as AsyncCommandBase)!.ExecuteAsync(null!);
            }
        }
    }
}
