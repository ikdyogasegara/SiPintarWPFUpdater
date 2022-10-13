using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(SaldoAwalPerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null) { return; }


            if (_viewModel.SelectedData.PerkiraanWpf != null)
            {
                if (_viewModel.SelectedData.PerkiraanWpf == "perkiraan1")
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "AkuntansiRootDialog", "Pengaturan Saldo Awal Perkiraan Tidak Diizinkan", "Saldo awal perkiraan hanya bisa diinputkan pada Sub Kode Perkiraan saja", "warning", "Kembali", false, "akuntansi");
                }
                else if (_viewModel.SelectedData.PerkiraanWpf == "perkiraan2")
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "AkuntansiRootDialog", "Pengaturan Saldo Awal Perkiraan Tidak Diizinkan", "Saldo awal perkiraan hanya bisa diinputkan pada Sub Kode Perkiraan saja", "warning", "Kembali", false, "akuntansi");
                }
                else if (_viewModel.SelectedData.PerkiraanWpf == "perkiraan3")
                {
                    _viewModel.FormPerkiraan = (SaldoAwalPerkiraanDto)_viewModel.SelectedData.Clone();
                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
                }
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "AkuntansiRootDialog", "Saldo Awal Perkiraan", "Silahkan pilih data", "warning", "Ok", false, "akuntansi");
            }

            _viewModel.IsLoadingForm = false;
        }


        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
