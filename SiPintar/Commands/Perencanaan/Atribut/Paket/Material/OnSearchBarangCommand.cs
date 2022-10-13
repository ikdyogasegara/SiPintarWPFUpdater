﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnSearchBarangCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = App.Current.Resources["api_version"]?.ToString();

        public OnSearchBarangCommand(PaketMaterialViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            string ErrorMessage = null;
            _viewModel.IsEmptySearch = false;

            var param = new Dictionary<string, dynamic>();

            if (!string.IsNullOrEmpty(_viewModel.NamaBarangForm))
                param.Add("NamaMaterial", _viewModel.NamaBarangForm);

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync($"/api/{ApiVersion}/master-material", param));

            _viewModel.SearchList = new ObservableCollection<MasterMaterialDto>();

            if (!Response.IsError)
            {
                var Result = Response.Data;

                if (Result.Status && Result.Data != null)
                    _viewModel.SearchList = Result.Data.ToObject<ObservableCollection<MasterMaterialDto>>();
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            _ = ErrorMessage;

            _viewModel.IsEmptySearch = _viewModel.SearchList.Count == 0;

            _viewModel.IsLoadingForm = false;

            _viewModel.IsTriggerSearch = !string.IsNullOrEmpty(_viewModel.NamaBarangForm);

            await Task.FromResult(_isTest);
        }
    }
}
