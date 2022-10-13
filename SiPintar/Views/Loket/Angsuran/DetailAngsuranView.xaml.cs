using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Views.Loket.Angsuran
{
    /// <summary>
    /// Interaction logic for DetailAngsuranView.xaml
    /// </summary>
    public partial class DetailAngsuranView : UserControl
    {
        public DetailAngsuranView()
        {
            InitializeComponent();
        }

        private void CetakBeritaAcara_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is DetailAngsuranViewModel vm))
            {
                return;
            }

            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).LinkApi = vm.LinkApi;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).TemplateName = vm.TemplateBeritaAcara;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).IsCetak = true;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).IsPreview = true;
            ((OnCetakCommand)vm.OnCetakBeritaAcaraCommand).Method = CetakApiMethod.POST;
            var param = new Dictionary<string, dynamic>()
            {
                { "IdAngsuran", vm.SelectedData.IdAngsuran }
            };
            vm.OnCetakBeritaAcaraCommand.Execute(param);
        }

        private void CetakSpa_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is DetailAngsuranViewModel vm))
            {
                return;
            }

            ((OnCetakCommand)vm.OnCetakSuratPernyataanCommand).LinkApi = vm.LinkApi;
            ((OnCetakCommand)vm.OnCetakSuratPernyataanCommand).TemplateName = vm.TemplateSpa;
            ((OnCetakCommand)vm.OnCetakSuratPernyataanCommand).IsCetak = true;
            ((OnCetakCommand)vm.OnCetakSuratPernyataanCommand).IsPreview = true;
            ((OnCetakCommand)vm.OnCetakSuratPernyataanCommand).Method = CetakApiMethod.POST;
            var param = new Dictionary<string, dynamic>()
            {
                { "IdAngsuran", vm.SelectedData.IdAngsuran }
            };
            vm.OnCetakSuratPernyataanCommand.Execute(param);
        }
    }
}
