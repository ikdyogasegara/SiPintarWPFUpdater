using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.Desktop
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DesktopViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(DesktopViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ListModule = new List<List<object>>();
            // restrict permission
            var Akses = Application.Current.Resources["ModuleAkses"]?.ToString();
            if (Akses != null)
            {
                var Permission = new List<string>();
                var listAkses = Akses.Split(',');
                foreach (var item in listAkses)
                    Permission.Add(item.ToLower());

                _viewModel.ModuleAkses = Permission;
            }

            List<object> data = _viewModel.GetModuleItems();

            int TotalData = data.Count;
            int TotalGrid = 2;
            int TotalList = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalData) / TotalGrid));

            var result = new List<List<object>>();
            for (int i = 0; i < TotalList; i++)
            {
                var subdata = new List<object>();
                for (int j = 0; j < TotalGrid; j++)
                {
                    if (data.Count > 0)
                    {
                        subdata.Add(data[0]);
                        data.RemoveAt(0);
                    }
                }
                result.Add(subdata);
            }

            _viewModel.ListModule = result;

            await Task.FromResult(_isTest);
        }
    }
}
