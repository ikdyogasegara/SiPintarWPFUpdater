using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.MutasiJabatan;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private MasterPegawaiDto _pegawai;

        public OnOpenEditFormCommand(MutasiJabatanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                if (_viewModel.SelectedData.Verifikasi == true)
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PersonaliaRootDialog",
                        "Koreksi tidak diizinkan",
                        "Data sudah diverifikasi, Jika ingin melakukan koreksi harap hubungi admin",
                        "error",
                        "Batal",
                        false,
                        "Personalia");
                else
                {
                    _viewModel.FormSk = _viewModel.SelectedData.NoSk;
                    _viewModel.FormTglSk = _viewModel.SelectedData.TglSk;
                    _viewModel.FormTglBerlaku = _viewModel.SelectedData.TglBerlaku;
                    _viewModel.FormKeterangan = _viewModel.SelectedData.Keterangan;
                    _viewModel.FormDetailList = new ObservableCollection<MutasiJabatanDetailWpf>();
                    var formDetailList = new ObservableCollection<MutasiJabatanDetailWpf>();

                    Type type = typeof(MutasiJabatanDetailDto);
                    PropertyInfo[] properties = type.GetProperties();
                    for (int i = 0; i < _viewModel.SelectedData.Detail.Count; i++)
                    {
                        await GetPegawaiAsync(_viewModel.SelectedData.Detail[i].IdPegawai);
                        var formDetail = new MutasiJabatanDetailWpf();
                        foreach (PropertyInfo property in properties)
                        {
                            property.SetValue(formDetail, property.GetValue(_viewModel.SelectedData.Detail[i], null));
                        }
                        formDetail.No = i + 1;
                        formDetail.IdPendidikan = _pegawai?.IdPendidikan;
                        formDetail.Pendidikan = _pegawai?.Pendidikan;
                        formDetail.IdGolonganPegawai = _pegawai?.IdGolonganPegawai;
                        formDetail.KodeGolonganPegawai = _pegawai?.KodeGolonganPegawai;
                        formDetail.GolonganPegawai = _pegawai?.GolonganPegawai;
                        formDetailList.Add(formDetail);
                    }
                    _viewModel.FormDetailList = formDetailList;
                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
                }
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Mutasi Jabatan",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }

        public async Task GetPegawaiAsync(int? idPegawai)
        {
            _viewModel.IsLoading = true;
            var param = new Dictionary<string, dynamic> { { "IdPegawai", idPegawai } };
            var Response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pegawai", param);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    _pegawai = Result.Data.ToObject<ObservableCollection<MasterPegawaiDto>>().FirstOrDefault();
                }
            }
            _viewModel.IsLoading = false;
        }
    }
}
