using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.PeriodeList = null;
            _viewModel.IsEmptyChart = true;
            _viewModel.IsLoading = false;
            SetComboBox();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void SetComboBox()
        {
            if (!_isTest)
            {
                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                 "MasterRayon",
            }).ContinueWith(result =>
            {
                _viewModel.RayonList = JArray.FromObject(MasterListGlobal.MasterRayon).ToObject<ObservableCollection<MasterRayonDto>>();
                _viewModel.RayonList ??= new();
            });

                _ = UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                 "MasterWilayah",
            }).ContinueWith(result =>
            {
                _viewModel.WilayahList = JArray.FromObject(MasterListGlobal.MasterWilayah).ToObject<ObservableCollection<MasterWilayahDto>>();
                _viewModel.WilayahList ??= new();
            });

            }
        }
    }
}
