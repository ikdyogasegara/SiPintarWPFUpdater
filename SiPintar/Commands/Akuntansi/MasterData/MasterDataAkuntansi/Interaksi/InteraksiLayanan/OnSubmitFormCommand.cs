using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(InteraksiLayananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                "", true, true, 20);
            try
            {
                var selectedData = _viewModel.SelectedData;

                var data = new object();
                Type type;
                switch (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key)
                {
                    case 0:
                        data = parameter as ParamMasterInPelayananAirDto;
                        type = typeof(ParamMasterInPelayananAirDto);
                        break;
                    default:
                        data = parameter as ParamMasterInPelayananNonAirDto;
                        type = typeof(ParamMasterInPelayananNonAirDto);
                        break;
                }


                PropertyInfo[] properties = type.GetProperties();
                var body = new Dictionary<string, dynamic>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                    {
                        body.Add(property.Name, property.GetValue(data));
                    }
                }

                RestApiResponse response;
                if (_viewModel.Parent.IsAdd)
                    response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.Parent.Url}", body);
                else
                    response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_viewModel.Parent.Url}", "", body);

                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        if (!_viewModel.Parent.IsAdd)
                        {
                            selectedData.IdGolongan = body["IdGolongan"];
                            selectedData.IdWilayah = body["IdWilayah"];
                            selectedData.KeteranganWpf = body["Keterangan"];


                            if (_viewModel.Parent.SelectedJenisInteraksiPelayanan.Key == 0)
                            {
                                selectedData.IdPerkiraan3Debet = body["IdPerkiraan3Debet"];
                                selectedData.IdPerkiraan3Kredit = body["IdPerkiraan3Kredit"];
                                selectedData.KodePerkiraan3DebetWpf = _viewModel.Parent.Perkiraan3DebetList.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Debet).KodePerkiraan3;
                                selectedData.NamaPerkiraan3DebetWpf = _viewModel.Parent.Perkiraan3DebetList.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Debet).NamaPerkiraan3;
                                selectedData.KodePerkiraan3KreditWpf = _viewModel.Parent.Perkiraan3KreditList.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Kredit).KodePerkiraan3;
                                selectedData.NamaPerkiraan3KreditWpf = _viewModel.Parent.Perkiraan3KreditList.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3Kredit).NamaPerkiraan3;
                                selectedData.FlagPembentukRekairWpf = body["FlagPembentukRekair"] ?? false;
                            }
                            else
                            {
                                selectedData.IdPerkiraan3 = body["IdPerkiraan3"];
                                selectedData.IdJenisNonAir = body["IdJenisNonAir"];
                                selectedData.KodePerkiraan3 = _viewModel.Parent.Perkiraan3NonAirList.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3).KodePerkiraan3;
                                selectedData.NamaPerkiraan3 = _viewModel.Parent.Perkiraan3NonAirList.FirstOrDefault(x => x.IdPerkiraan3 == selectedData.IdPerkiraan3).NamaPerkiraan3;
                                selectedData.KodeJenisNonAir = _viewModel.Parent.JenisNonAirList.FirstOrDefault(x => x.IdJenisNonAir == selectedData.IdJenisNonAir).KodeJenisNonAir;
                                selectedData.NamaJenisNonAir = _viewModel.Parent.JenisNonAirList.FirstOrDefault(x => x.IdJenisNonAir == selectedData.IdJenisNonAir).NamaJenisNonAir;
                            }
                            selectedData.KodeWilayahWpf = _viewModel.Parent.WilayahList.FirstOrDefault(x => x.IdWilayah == selectedData.IdWilayah).KodeWilayah;
                            selectedData.NamaWilayahWpf = _viewModel.Parent.WilayahList.FirstOrDefault(x => x.IdWilayah == selectedData.IdWilayah).KodeWilayah;
                            selectedData.KodeGolonganWpf = _viewModel.Parent.GolonganList.FirstOrDefault(x => x.IdGolongan == selectedData.IdGolongan).KodeGolongan;
                            selectedData.NamaGolonganWpf = _viewModel.Parent.GolonganList.FirstOrDefault(x => x.IdGolongan == selectedData.IdGolongan).KodeGolongan;

                        }
                        else
                            _viewModel.OnLoadCommand.Execute(null);

                        DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "akuntansi");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "akuntansi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "akuntansi");
            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
            }


            //End - Get Filter Data
            await Task.FromResult(_isTest);
        }
    }
}
