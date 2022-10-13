using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnLoadDetailJenisNonairCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadDetailJenisNonairCommand(TagihanManualViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1 },
                { "CurrentPage" , 1 },
                { "IdJenisNonAir" , _viewModel.SelectedIdJenisNonAir },
            };

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jenis-non-air", param));
            _viewModel.JenisNonAirDetailList = new ObservableCollection<MasterJenisNonAirDetailDto>();

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var list = result.Data.ToObject<ObservableCollection<MasterJenisNonAirDto>>();

                    if (list != null && list.FirstOrDefault().DetailBiaya != null)
                    {
                        var temp = list.FirstOrDefault().DetailBiaya.ToList();

                        foreach (var i in temp)
                        {
                            _viewModel.JenisNonAirDetailList.Add(i);
                        }
                    }
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            await Task.FromResult(_isTest);
        }
    }
}
