using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    public class GetUserListPDACommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public GetUserListPDACommand(PengaturanPutstampViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

            _viewModel.IsEmptyUser = false;
            _viewModel.IsLoadingForm = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000 },
                { "CurrentPage" , 1 },
                { "IncludeAkses" , false },
            };

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-user", param));

            _viewModel.UserList = new ObservableCollection<MasterUserDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterUserDto>>();

                    var overriden = new ObservableCollection<MasterUserDto>();
                    foreach (var item in data)
                    {
                        if (item.NamaRole.ToLower().Contains("admin"))
                            overriden.Add(item);
                    }

                    _viewModel.UserList = overriden;
                }
            }

            _viewModel.IsEmptyUser = _viewModel.UserList.Count == 0;
            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
