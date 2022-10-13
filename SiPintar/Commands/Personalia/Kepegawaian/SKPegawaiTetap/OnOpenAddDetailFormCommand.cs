﻿using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.SKPegawaiTetap;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    public class OnOpenAddDetailFormCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddDetailFormCommand(SKPegawaiTetapViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            if (_viewModel.SelectedDataPegawai == null)
                return;

            _viewModel.IsLoading = true;
            _viewModel.IsEditDetail = false;
            _viewModel.IsUbahNik = false;
            DialogHelper.CloseDialog(_isTest, false, "PersonaliaInnerDialog");

            _viewModel.FormDetailData = new MutasiStatusTetapDetailDto
            {
                NamaPegawai = _viewModel.SelectedDataPegawai.NamaPegawai,
                Jabatan = _viewModel.SelectedDataPegawai.Jabatan,
                Departemen = _viewModel.SelectedDataPegawai.Departemen,
                Divisi = _viewModel.SelectedDataPegawai.Divisi,
                AreaKerja = _viewModel.SelectedDataPegawai.AreaKerja,
                Tugas = _viewModel.SelectedDataPegawai.Tugas,
                Pendidikan = _viewModel.SelectedDataPegawai.Pendidikan,

                IdPegawai = _viewModel.SelectedDataPegawai.IdPegawai,
                IdJabatan = _viewModel.SelectedDataPegawai.IdJabatan,
                IdDepartemen = _viewModel.SelectedDataPegawai.IdDepartemen,
                IdDivisi = _viewModel.SelectedDataPegawai.IdDivisi,
                IdAreaKerja = _viewModel.SelectedDataPegawai.IdAreaKerja,
                IdPendidikan = _viewModel.SelectedDataPegawai.IdPendidikan,
                IdGolonganPegawai = _viewModel.SelectedDataPegawai.IdGolonganPegawai,

                NoPegawai = _viewModel.SelectedDataPegawai.NoPegawai,
                Mkg_Thn = 0,
                Mkg_Bln = 0,
            };

            _viewModel.IsLoading = false;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaInnerDialog", () => new DialogDetailFormView(_viewModel));

            await Task.FromResult(_isTest);
        }
    }
}
