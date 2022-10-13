using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Pendaftaran;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PendaftaranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedTipePermohonanComboBox == null)
                return;

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            _viewModel.IsDetail = false;
            _viewModel.IsEdit = false;

            _viewModel.FormData = new PermohonanNonPelangganForm()
            {
                WaktuPermohonan = DateTime.Now,
                Parameter = DefineParameter()
            };

            _viewModel.IsNewFotoSuratPernyataan = false;
            _viewModel.IsNewFotoKk = false;
            _viewModel.IsNewFotoKtp = false;
            _viewModel.IsNewFotoImb = false;

            _viewModel.IsFotoSuratPernyataanFormChecked = false;
            _viewModel.IsFotoKkFormChecked = false;
            _viewModel.IsFotoKtpFormChecked = false;
            _viewModel.IsFotoImbFormChecked = false;

            _viewModel.IsMeteraiFormChecked = false;
            _viewModel.IsMapPlastikFormChecked = false;
            _viewModel.IsDenahLokasiFormChecked = false;

            _viewModel.KelurahanForm = null;
            _viewModel.RayonForm = null;
            _viewModel.BlokForm = null;
            _viewModel.JenisBangunanForm = null;
            _viewModel.KepemilikanBangunanForm = null;
            _viewModel.PeruntukanForm = null;
            _viewModel.PekerjaanForm = null;
            _viewModel.SumberAirForm = null;
            _viewModel.TipePendaftaranForm = null;

            Biaya();

            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);

            if (!_isTest) await DialogHost.Show(new MasterAddFormView(_viewModel), "HublangRootDialog");

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private List<PermohonanNonPelangganDetailDto> DefineParameter()
        {
            var result = new List<PermohonanNonPelangganDetailDto>();

            if (_viewModel.SelectedTipePermohonanComboBox.DetailParameter != null)
            {
                foreach (var item in _viewModel.SelectedTipePermohonanComboBox.DetailParameter)
                {
                    result.Add(new PermohonanNonPelangganDetailDto()
                    {
                        Parameter = item.Parameter,
                        TipeData = item.TipeData,
                        Value = null
                    });
                }
            }

            return result;
        }

        [ExcludeFromCodeCoverage]
        private void Biaya()
        {
            if (!_isTest && _viewModel.JenisNonAir != null && _viewModel.JenisNonAir.DetailBiaya != null)
            {
                #region BiayaPendaftaranForm
                _viewModel.BiayaPendaftaranForm =
                    _viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "pendaftaran") != null
                        ? (decimal)_viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "pendaftaran")
                            .Biaya
                        : 0;

                _viewModel.CalculateBiayaCommand.Execute("pendaftaran");
                #endregion

                #region BiayaPendaftaranForm
                _viewModel.BiayaOpnameForm =
                    _viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "opname") != null
                        ? (decimal)_viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "opname")
                            .Biaya
                        : 0;

                _viewModel.CalculateBiayaCommand.Execute("opname");
                #endregion

                #region BiayaFormulirForm
                _viewModel.BiayaFormulirForm =
                    _viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "formulir") != null
                        ? (decimal)_viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "formulir")
                            .Biaya
                        : 0;

                _viewModel.CalculateBiayaCommand.Execute("formulir");
                #endregion

                #region BiayaJaminanForm
                _viewModel.BiayaJaminanForm =
                    _viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "jaminan") != null
                        ? (decimal)_viewModel.JenisNonAir.DetailBiaya.FirstOrDefault(c => c.Parameter.ToLower() == "jaminan")
                            .Biaya
                        : 0;

                _viewModel.CalculateBiayaCommand.Execute("jaminan");
                #endregion
            }
        }
    }
}
