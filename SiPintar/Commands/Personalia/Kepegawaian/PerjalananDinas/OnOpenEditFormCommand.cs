using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Personalia.Kepegawaian.PerjalananDinas;

namespace SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PerjalananDinasViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PerjalananDinasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            _viewModel.IsEdit = true;

            if (_viewModel.SelectedData != null)
            {
                _viewModel.FormSpt = _viewModel.SelectedData.NoSpt;
                _viewModel.FormSppd = _viewModel.SelectedData.NoSppd;
                _viewModel.FormDasar = _viewModel.SelectedData.Dasar;
                _viewModel.FormKeperluan = _viewModel.SelectedData.Keperluan;
                _viewModel.FormTempatBerangkat = _viewModel.SelectedData.TempatBerangkat;
                _viewModel.FormTempatTujuan = _viewModel.SelectedData.TempatTujuan;

                _viewModel.FormTglBerangkat = _viewModel.SelectedData.TglBerangkat;
                _viewModel.FormTglKembali = _viewModel.SelectedData.TglKembali;
                _viewModel.FormLamaDinas = _viewModel.SelectedData.LamaDinas;
                _viewModel.FormTransportasi = _viewModel.SelectedData.Transportasi;
                _viewModel.FormBebanAnggaran = _viewModel.SelectedData.BebanAnggaran;
                _viewModel.FormKeterangan = _viewModel.SelectedData.Keterangan;
                _viewModel.FormPesertaList = new ObservableCollection<SppdPesertaWpf>();
                var formPesertaList = new ObservableCollection<SppdPesertaWpf>();

                Type type = typeof(SppdPesertaDto);
                PropertyInfo[] properties = type.GetProperties();
                for (int i = 0; i < _viewModel.SelectedData.Peserta.Count; i++)
                {
                    var formPeserta = new SppdPesertaWpf();
                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(formPeserta, property.GetValue(_viewModel.SelectedData.Peserta[i], null));
                    }
                    formPesertaList.Add(formPeserta);
                }
                _viewModel.FormPesertaList = formPesertaList;

                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PersonaliaRootDialog", () => new DialogFormView(_viewModel));
            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "PersonaliaRootDialog",
                    "Koreksi Perjalanan Dinas",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "Personalia");

            await Task.FromResult(_isTest);
        }
    }
}
