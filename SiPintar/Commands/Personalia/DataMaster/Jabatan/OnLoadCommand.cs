﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Jabatan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly JabatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(JabatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsLoading = true;

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-jabatan"));
            _viewModel.JabatanList = new ObservableCollection<MasterJabatanDto>();
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    _viewModel.JabatanList = Result.Data.ToObject<ObservableCollection<MasterJabatanDto>>();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg, "personalia");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message, "personalia");
            _viewModel.IsEmpty = !_viewModel.JabatanList.Any();

            _viewModel.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
