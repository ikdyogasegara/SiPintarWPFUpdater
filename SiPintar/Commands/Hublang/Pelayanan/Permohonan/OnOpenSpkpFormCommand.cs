using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Permohonan;


namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnOpenSpkpFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenSpkpFormCommand(PermohonanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null && _viewModel.SelectedData.RabWpf != null)
            {
                if (_viewModel.SelectedData.RabWpf.NonAirRab != null && _viewModel.SelectedData.RabWpf.NonAirRab.Total > 0 && (_viewModel.SelectedData.RabWpf.NonAirRab.StatusTransaksi == false || _viewModel.SelectedData.RabWpf.NonAirRab.StatusTransaksi == null))
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PerencanaanRootDialog",
                        "SPKP Tidak diijinkan",
                        "RAB harus dilunasi terlebih dahulu !",
                        "warning",
                        "Batal",
                        false,
                        "perencanaan");
                }
                else
                {
                    _viewModel.TanggalSpkp = _viewModel.SelectedData.RabWpf.TanggalSpkp ?? DateTime.Now;
                    _viewModel.TanggalSppb = _viewModel.SelectedData.RabWpf.TanggalSppb ?? DateTime.Now;

                    _viewModel.SelectedPegawai = new ObservableCollection<MasterPegawaiDto>();
                    _viewModel.SelectedData.Pelaksana?.ForEach(x =>
                    {
                        _viewModel.SelectedPegawai.Add(_viewModel.PegawaiList?.FirstOrDefault(i => i.IdPegawai == x.IdPegawai));
                    });

                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PerencanaanRootDialog", GetInstance);
                }
            }
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new SpkpFormView(_viewModel);
    }
}
