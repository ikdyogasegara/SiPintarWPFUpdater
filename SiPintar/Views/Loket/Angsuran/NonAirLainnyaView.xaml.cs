using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands.Global;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Views.Loket.Angsuran
{
    public partial class NonAirLainnyaView : UserControl
    {
        public NonAirLainnyaView()
        {
            InitializeComponent();
        }

        private void DataGridContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridContent_Loaded(object sender, RoutedEventArgs e)
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(DataGridContent, UpdatedSource);
            }
        }

        private void UpdatedSource(object sender, EventArgs e)
        {

        }

        private void DesginTemplateSpa_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is AngsuranNonAirViewModel vm))
            {
                return;
            }

            ((OnCetakCommand)vm.OnCetakSpaCommand).IsCetak = false;
            ((OnCetakCommand)vm.OnCetakSpaCommand).Method = CetakApiMethod.GET;
            ((OnCetakCommand)vm.OnCetakSpaCommand).TemplateName = "SuratPernyataanAngsuranNonAir";
            vm.OnCetakSpaCommand.Execute(null);
        }

        private void CetakSpa_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is AngsuranNonAirViewModel vm))
            {
                return;
            }

            if (vm.SelectedData is null)
            {
                return; ;
            }

            ((OnCetakCommand)vm.OnCetakSpaCommand).IsCetak = true;
            ((OnCetakCommand)vm.OnCetakSpaCommand).IsPreview = true;
            ((OnCetakCommand)vm.OnCetakSpaCommand).Method = CetakApiMethod.POST;
            ((OnCetakCommand)vm.OnCetakSpaCommand).TemplateName = "SuratPernyataanAngsuranNonAir";
            var param = new Dictionary<string, dynamic>()
            {
                { "IdAngsuran", vm.SelectedData.IdAngsuran }
            };
            vm.OnCetakSpaCommand.Execute(param);
        }

        private void DesginTemplateBeritaAcara_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is AngsuranNonAirViewModel vm))
            {
                return;
            }

            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).IsCetak = false;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).Method = CetakApiMethod.GET;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).TemplateName = "BeritaAcaraAngsuranNonAir";
            vm.OnCetakBeritaAcaraCommand.Execute(null);
        }

        private void CetakBeritaAcara_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is AngsuranNonAirViewModel vm))
            {
                return;
            }

            if (vm.SelectedData is null)
            {
                return; ;
            }

            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).IsCetak = true;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).IsPreview = true;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).Method = CetakApiMethod.POST;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).TemplateName = "BeritaAcaraAngsuranNonAir";
            var param = new Dictionary<string, dynamic>()
            {
                { "IdAngsuran", vm.SelectedData.IdAngsuran }
            };
            vm.OnCetakBeritaAcaraCommand.Execute(param);
        }
    }
}
