using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly InteraksiLayananViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(InteraksiLayananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
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
                _viewModel.Parent.IsAdd = true;

                _viewModel.Parent.Perkiraan3List = MasterListGlobal.MasterPerkiraan3;


                _viewModel.SelectedWilayah = new MasterWilayahDto();
                _viewModel.SelectedGolongan = new MasterGolonganDto();
                _viewModel.SelectedJenisNonAir = new MasterJenisNonAirDto();
                _viewModel.SelectedPerkiraan3Debet = new MasterPerkiraan3Dto();
                _viewModel.SelectedPerkiraan3Kredit = new MasterPerkiraan3Dto();
                _viewModel.SelectedPerkiraan3NonAir = new MasterPerkiraan3Dto();
                _viewModel.SelectedPembentukRekNonAir = new KeyValuePair<string, string>();

            }
            catch (Exception e)
            {
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            }

        }

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
