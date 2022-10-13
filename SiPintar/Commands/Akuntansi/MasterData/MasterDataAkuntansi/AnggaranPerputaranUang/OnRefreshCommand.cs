using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly AnggaranPerputaranUangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(AnggaranPerputaranUangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedTahun.Value == null) return;

            if (!_viewModel.IsEdit)
                _viewModel.IsLoading = true;


            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 1000000 },
                { "CurrentPage", 1 },
                { "Tahun", _viewModel.SelectedTahun.Value }
            };

            if (_viewModel.SelectedWilayah.IdWilayah != null)
                param["IdWilayah"] = _viewModel.SelectedWilayah.IdWilayah;

            if (_viewModel.IsKodePerkiraanChecked && _viewModel.FilterKodePerkiraan != null)
                param["KodePerkiraan3"] = _viewModel.FilterKodePerkiraan;

            if (_viewModel.IsNamaPerkiraanChecked && _viewModel.FilterNamaPerkiraan != null)
                param["NamaPerkiraan3"] = _viewModel.FilterNamaPerkiraan;


            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/laporan-perputaran-uang", param);
            _viewModel.LaporanPerputaranUang = new ObservableCollection<LaporanPerputaranUangDto>();
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.LaporanPerputaranUang = result.Data.ToObject<ObservableCollection<LaporanPerputaranUangDto>>()!;
                    if (_viewModel.SelectedWilayah.IdWilayah == null && !_viewModel.IsKonsolidasi)
                    {
                        var dataPusat = _viewModel.LaporanPerputaranUang
                            .GroupBy(x => x.IdPerkiraan3)
                            .Select(y => new LaporanPerputaranUangDto
                            {
                                IdPdam = y.First().IdPdam,
                                IdPerputaranUangMaster = y.First().IdPerputaranUangMaster,
                                Header = y.First().Header,
                                IdKelompok = y.First().IdKelompok,
                                Uraian = y.First().Uraian,
                                Tahun = y.First().Tahun,
                                IdPerkiraan3 = y.First().IdPerkiraan3,
                                KodePerkiraan3 = y.First().KodePerkiraan3,
                                NamaPerkiraan3 = y.First().NamaPerkiraan3,
                                Anggaran1 = y.Sum(z => z.Anggaran1),
                                Anggaran2 = y.Sum(z => z.Anggaran2),
                                Anggaran3 = y.Sum(z => z.Anggaran3),
                                Anggaran4 = y.Sum(z => z.Anggaran4),
                                Anggaran5 = y.Sum(z => z.Anggaran5),
                                Anggaran6 = y.Sum(z => z.Anggaran6),
                                Anggaran7 = y.Sum(z => z.Anggaran7),
                                Anggaran8 = y.Sum(z => z.Anggaran8),
                                Anggaran9 = y.Sum(z => z.Anggaran9),
                                Anggaran10 = y.Sum(z => z.Anggaran10),
                                Anggaran11 = y.Sum(z => z.Anggaran11),
                                Anggaran12 = y.Sum(z => z.Anggaran12)
                            });

                        _viewModel.LaporanPerputaranUang = new ObservableCollection<LaporanPerputaranUangDto>(dataPusat);
                    }

                    var data = _viewModel.LaporanPerputaranUang.OrderBy(x => x.IdPerputaranUangMaster).GroupBy(x => x.IdPerputaranUangMaster);
                    var dataRekap = _viewModel.LaporanPerputaranUang.GroupBy(x => x.Header);


                    if (!_viewModel.IsEdit)
                    {
                        _viewModel.DataJenisList = new ObservableCollection<KeyValuePair<string, string>>();
                        if (_viewModel.IsRekap)
                        {
                            foreach (var item in dataRekap)
                            {
                                _viewModel.DataJenisList.Add(new KeyValuePair<string, string>(
                                    _viewModel.LaporanPerputaranUang.FirstOrDefault(x => x.Header == item.Key)!.Header,
                                    _viewModel.LaporanPerputaranUang.FirstOrDefault(x => x.Header == item.Key)!.Header
                                ));
                            }
                        }
                        else
                        {
                            foreach (var item in data)
                            {
                                _viewModel.DataJenisList.Add(new KeyValuePair<string, string>(
                                    _viewModel.LaporanPerputaranUang.FirstOrDefault(x => x.IdPerputaranUangMaster == item.Key)!.Header,
                                    _viewModel.LaporanPerputaranUang.FirstOrDefault(x => x.IdPerputaranUangMaster == item.Key)!.Uraian
                                ));
                            }
                        }

                    }
                    else
                    {
                        _viewModel.DataUraianList = new ObservableCollection<LaporanPerputaranUangDto>(_viewModel.LaporanPerputaranUang.Where(x => x.Uraian == _viewModel.SelectedDataJenis.Value));
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
            }

            _viewModel.IsEmpty = _viewModel.DataJenisList?.Count == 0;
            _viewModel.IsLoading = false;
            _viewModel.IsEdit = false;

            await Task.FromResult(_isTest);
        }


    }
}
