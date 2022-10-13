using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly AnggaranLabaRugiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(AnggaranLabaRugiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            _viewModel.DataUraianList.Clear();

            #region Param

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , 10000000 },
                { "CurrentPage", 1 },
                { "Tahun", _viewModel.SelectedTahun.Value },
            };

            if (_viewModel.SelectedWilayah != null && _viewModel.SelectedWilayah?.IdWilayah != null)
                param.Add("IdWilayah", _viewModel.SelectedWilayah.IdWilayah!);

            if (_viewModel.IsKodePerkiraanChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterKodePerkiraan))
                param.Add("KodePerkiraan", _viewModel.FilterKodePerkiraan);

            if (_viewModel.IsNamaPerkiraanChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNamaPerkiraan))
                param.Add("NamaPerkiraan", _viewModel.FilterNamaPerkiraan);

            #endregion

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/anggaran-laba-rugi", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<AnggaranLabaRugiDto>>()!;
                    SetDataJenisList();
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsEmptyJenis = _viewModel.DataJenisList?.Count == 0;
            _viewModel.IsEdit = false;
            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        private void SetDataJenisList()
        {
            if (!_viewModel.IsEdit)
            {
                _viewModel.DataJenisList = new ObservableCollection<KeyValuePair<string, string>>(
                    _viewModel.DataList.DistinctBy(x => x.Header).Select(x => new KeyValuePair<string, string>(x.Header, x.Uraian)));

                if (!_viewModel.IsKonsolidasi && _viewModel.SelectedWilayah != null && _viewModel.SelectedWilayah.IdWilayah == null)
                    _viewModel.DataList = new ObservableCollection<AnggaranLabaRugiDto>(GroupUraianList());
            }
            else
            {
                _viewModel.DataUraianList = new ObservableCollection<AnggaranLabaRugiDto>(_viewModel.DataList.Where(x => x.Uraian == _viewModel.SelectedDataJenis.Value));
            }
        }

        private List<AnggaranLabaRugiDto> GroupUraianList()
        {
            var result = new List<AnggaranLabaRugiDto>();
            var queryGroup = _viewModel.DataList.GroupBy(x => x.IdPerkiraan3)
                .Select(g => new { Perkiraan3 = g.Key, data = g });

            foreach (var perkiraan3 in queryGroup)
            {
                result.Add(
                new AnggaranLabaRugiDto
                {
                    IdPdam = perkiraan3.data.Select(x => x.IdPdam).First(),
                    IdGroupLabaRugi = perkiraan3.data.Select(x => x.IdGroupLabaRugi).First(),
                    Header = perkiraan3.data.Select(x => x.Header).First(),
                    Uraian = perkiraan3.data.Select(x => x.Uraian).First(),
                    IdPerkiraan3 = perkiraan3.data.Select(x => x.IdPerkiraan3).First(),
                    KodePerkiraan = perkiraan3.data.Select(x => x.KodePerkiraan).First(),
                    NamaPerkiraan = perkiraan3.data.Select(x => x.NamaPerkiraan).First(),
                    IdWilayah = perkiraan3.data.Select(x => x.IdWilayah).First(),
                    KodeWilayah = perkiraan3.data.Select(x => x.KodeWilayah).First(),
                    NamaWilayah = perkiraan3.data.Select(x => x.NamaWilayah).First(),
                    Anggaran1 = perkiraan3.data.Sum(x => x.Anggaran1),
                    Anggaran2 = perkiraan3.data.Sum(x => x.Anggaran2),
                    Anggaran3 = perkiraan3.data.Sum(x => x.Anggaran3),
                    Anggaran4 = perkiraan3.data.Sum(x => x.Anggaran4),
                    Anggaran5 = perkiraan3.data.Sum(x => x.Anggaran5),
                    Anggaran6 = perkiraan3.data.Sum(x => x.Anggaran6),
                    Anggaran7 = perkiraan3.data.Sum(x => x.Anggaran7),
                    Anggaran8 = perkiraan3.data.Sum(x => x.Anggaran8),
                    Anggaran9 = perkiraan3.data.Sum(x => x.Anggaran9),
                    Anggaran10 = perkiraan3.data.Sum(x => x.Anggaran10),
                    Anggaran11 = perkiraan3.data.Sum(x => x.Anggaran11),
                    Anggaran12 = perkiraan3.data.Sum(x => x.Anggaran12),
                });
            }

            return result;
        }
    }
}
