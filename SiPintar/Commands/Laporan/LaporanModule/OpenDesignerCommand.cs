using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.ViewModels.Laporan;

namespace SiPintar.Commands.Laporan.LaporanModule
{
    public class OpenDesignerCommand : AsyncCommandBase
    {
        private readonly LaporanModuleViewModel Vm;
        private readonly bool _isTest;

        public OpenDesignerCommand(LaporanModuleViewModel viewModel, bool isTest = false)
        {
            Vm = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var report = parameter as ReportModelDto;
            if (report != null)
            {
                var moduleName = $"{Vm.SelectedModule[0].ToString().ToUpper()}{Vm.SelectedModule[1..]}";
                var namaReport = moduleName + "\\" + report.Name;
                ((OnCetakCommand)Vm.OnCetakCommand).IsCetak = false;
                ((OnCetakCommand)Vm.OnCetakCommand).TemplateName = namaReport;
                ((OnCetakCommand)Vm.OnCetakCommand).Method = CetakApiMethod.POST;
                ((OnCetakCommand)Vm.OnCetakCommand).CustomIdentifier = "DialogFormReportDialog";

                var param = new Dictionary<string, dynamic>()
                {
                    { "IdModel", report.IdModel },
                    { "Template", true },
                };
                await ((AsyncCommandBase)Vm.OnCetakCommand).ExecuteAsync(param);
                ((OnCetakCommand)Vm.OnCetakCommand).Method = CetakApiMethod.GET;
                ((OnCetakCommand)Vm.OnCetakCommand).CustomIdentifier = null;
            }
            _ = Vm;
            await Task.FromResult(_isTest);
        }
    }
}
