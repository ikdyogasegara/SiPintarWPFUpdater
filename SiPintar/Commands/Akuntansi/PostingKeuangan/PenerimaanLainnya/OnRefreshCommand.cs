using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnRefreshCommand(PenerimaanLainnyaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;

            var param = new Dictionary<string, dynamic>
            {
                { "PageSize" , _viewModel.LimitData.Key },
                { "CurrentPage", _viewModel.CurrentPage },
            };

            AddParams(param);

            var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/penerimaan-lainnya", param);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    _viewModel.DataList = result.Data.ToObject<ObservableCollection<DaftarPenerimaanKasDataGridDto>>()!;
                    _viewModel.TotalPage = result.TotalPage;
                    _viewModel.TotalRecord = result.Record;

                    _viewModel.DataList = new(SetFlagTutupBuku());
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg!);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

            _viewModel.IsPreviousButtonEnabled = _viewModel.CurrentPage > 1;
            _viewModel.IsNextButtonEnabled = _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberActive = _viewModel.CurrentPage == 1;
            _viewModel.IsRightPageNumberActive = _viewModel.CurrentPage == _viewModel.TotalPage;
            _viewModel.IsLeftPageNumberFillerVisible = _viewModel.CurrentPage != 2;
            _viewModel.IsRightPageNumberFillerVisible = _viewModel.CurrentPage != _viewModel.TotalPage - 1;
            _viewModel.IsMiddlePageNumberVisible = _viewModel.CurrentPage > 1 && _viewModel.CurrentPage < _viewModel.TotalPage;
            _viewModel.IsEmpty = !_viewModel.DataList.Any();

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }

        private void AddParams(Dictionary<string, dynamic> param)
        {
            if (_viewModel.IsTanggalInputChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterTanggalInputAwal))
                param.Add("TanggalMulai", _viewModel.FilterTanggalInputAwal);

            if (_viewModel.IsTanggalInputChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterTanggalInputAkhir))
                param.Add("TanggalSampaiDengan", _viewModel.FilterTanggalInputAkhir);

            if (_viewModel.IsUraianChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterUraian))
                param.Add("Uraian", _viewModel.FilterUraian);

            if (_viewModel.IsNoTransChecked && !string.IsNullOrWhiteSpace(_viewModel.FilterNoTrans))
                param.Add("NoTrans", _viewModel.FilterNoTrans);

            if (_viewModel.IsWilayahChecked && _viewModel.FilterWilayah?.IdWilayah != null)
                param.Add("IdWilayah", _viewModel.FilterWilayah.IdWilayah!);

            if (_viewModel.IsPerkiraanDebetChecked && _viewModel.FilterPerkiraanDebet?.IdPerkiraan3 != null)
                param.Add("IdPerkiraanDebet", _viewModel.FilterPerkiraanDebet.IdPerkiraan3);

            if (_viewModel.IsPerkiraanKreditChecked && _viewModel.FilterPerkiraanKredit?.IdPerkiraan3 != null)
                param.Add("IdPerkiraan", _viewModel.FilterPerkiraanKredit.IdPerkiraan3);
        }

        [ExcludeFromCodeCoverage]
        private List<DaftarPenerimaanKasDataGridDto> SetFlagTutupBuku()
        {
            IEnumerable<IGrouping<dynamic, DaftarPenerimaanKasDataGridDto>> periodeList;
            List<DaftarPenerimaanKasDataGridDto> newList = new();

            if (_viewModel.DataList.Any(x => x.IdPeriode == null) || _isTest)
            {
                periodeList = _viewModel.DataList.GroupBy(x => ((DateTime)x.WaktuInput!).ToString("yyyyMM", null));
                foreach (var item in periodeList)
                {
                    var flagTutupBuku = _viewModel.PeriodeTutupBukuList
                        .Where(x => x.KodePeriode.ToString() == item.Key)
                        .Select(x => x.FlagTutupBuku)
                        .FirstOrDefault();
                    item.ForEach(x => x.StatusPostingText = flagTutupBuku ?? false ? "Sudah Posting" : "Belum Posting");
                    newList.AddRange(item.ToList());
                }
            }

            if (!_viewModel.DataList.Any(x => x.IdPeriode == null) || _isTest)
            {
                periodeList = _viewModel.DataList.GroupBy(x => x.IdPeriode.ToString()!);
                foreach (var item in periodeList)
                {
                    var flagTutupBuku = _viewModel.PeriodeTutupBukuList
                        .Where(x => x.IdPeriode.ToString() == item.Key)
                        .Select(x => x.FlagTutupBuku)
                        .FirstOrDefault();
                    item.ForEach(x => x.StatusPostingText = flagTutupBuku ?? false ? "Sudah Posting" : "Belum Posting");
                    newList.AddRange(item.ToList());
                }
            }

            return newList.OrderByDescending(x => x.WaktuUpdate).ToList();
        }
    }
}
