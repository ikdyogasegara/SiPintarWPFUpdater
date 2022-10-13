using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using Serilog;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PetugasBaca
{
    [ExcludeFromCodeCoverage]
    public class GetFotoCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public GetFotoCommand(PetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var IdPetugasBaca = (int)parameter;

            if (_viewModel.IsEdit)
                _viewModel.FotoPetugasURI = null;
            else
                _viewModel.FotoThumbnailURI = null;

            await GetFotoAsync(IdPetugasBaca);

            await Task.FromResult(_isTest);
        }

        private async Task GetFotoAsync(int IdPetugasBaca)
        {
            string ErrorMessage = null;

            var param = new Dictionary<string, dynamic>
            {
                { "IdPetugasBaca" , IdPetugasBaca }
            };

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await _restApi.GetAsync($"/api/{ApiVersion}/master-petugas-baca-link-foto", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<ObservableCollection<MasterPetugasBacaDto>>();

                    var Url = data[0].FotoPetugasBaca;

                    if (!_viewModel.IsEdit || _viewModel.IsOnGrid)
                        _viewModel.FotoThumbnailURI = Url != null ? new Uri(Url, UriKind.Absolute) : null;
                    else
                        _viewModel.FotoPetugasURI = Url != null ? new Uri(Url, UriKind.Absolute) : null;
                }
                else
                {
                    ErrorMessage = Response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (ErrorMessage != null)
            {
                Debug.WriteLine(ErrorMessage);
                Log.Logger.Error(ErrorMessage);
            }
        }
    }
}
