using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.JenisBarang
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly JenisBarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnRefreshCommand(JenisBarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            Vm.Data?.Clear();
            Vm.IsEmpty = true;
            var Param = new Dictionary<string, dynamic>();
            if (Vm.IsTipeBarangChecked && Vm.FilterTipeBarang != null && Vm.FilterTipeBarang.IdTipeBarang.HasValue)
            {
                Param.Add("IdTipeBarang", Vm.FilterTipeBarang?.IdTipeBarang.Value);
            }
            if (Vm.IsNamaJenisBarangChecked)
            {
                Param.Add("NamaJenisBarang", Vm.FilterNamaJenisBarang ?? "");
            }
            Param.Add("currentPage", Vm.CurrentPage);
            Param.Add("pageSize", Vm.LimitData.Key);
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-jenis-barang", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.Data = Result.Data.ToObject<ObservableCollection<MasterJenisBarangDto>>();
                    Vm.IsEmpty = Vm.Data != null && Vm.Data?.Count == 0;
                    Vm.TotalRecord = Result.Record;
                    Vm.TotalPage = Result.TotalPage;
                    Vm.IsPreviousButtonEnabled = Vm.CurrentPage > 1;
                    Vm.IsNextButtonEnabled = Vm.CurrentPage < Vm.TotalPage;
                    Vm.IsLeftPageNumberActive = Vm.CurrentPage == 1;
                    Vm.IsRightPageNumberActive = Vm.CurrentPage == Vm.TotalPage;
                    Vm.IsLeftPageNumberFillerVisible = Vm.CurrentPage != 2;
                    Vm.IsRightPageNumberFillerVisible = Vm.CurrentPage != Vm.TotalPage - 1;
                    Vm.IsMiddlePageNumberVisible = Vm.CurrentPage > 1 && Vm.CurrentPage < Vm.TotalPage;
                    Vm.IsPageNumberVisible = Vm.TotalPage > 1;
                }
                DialogHelper.ShowSnackbar(IsTest, Result.Status ? "success" : "danger", Result.Ui_msg, "gudang");
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            Vm.IsLoading = false;
            await Task.FromResult(IsTest);
        }
    }
}
