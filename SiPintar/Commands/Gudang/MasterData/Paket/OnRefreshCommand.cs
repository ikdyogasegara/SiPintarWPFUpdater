using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnRefreshCommand(PaketViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            Vm.DataGroup?.Remove(Vm.Data);
            Vm.DataGroup?.Refresh();
            Vm.IsEmpty = true;
            var Param = new Dictionary<string, dynamic>();
            if (Vm.IsNamaPaketChecked)
            {
                Param.Add("NamaPaketBarang", Vm.FilterNamaPaket ?? "");
            }
            if (Vm.IsUnitChecked && Vm.FilterUnit != null)
            {
                Param.Add("IdSatuanBarang", Vm.FilterUnit?.IdSatuanBarang);
            }
            if (Vm.IsKodeBarangChecked)
            {
                Param.Add("KodeBarang", Vm.FilterKodeBarang ?? "");
            }
            if (Vm.IsDeskripsiChecked)
            {
                Param.Add("NamaBarang", Vm.FilterDeskripsi ?? "");
            }
            Param.Add("currentPage", Vm.CurrentPage);
            Param.Add("pageSize", Vm.LimitData.Key);
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-paket-barang-detail", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.Data = Result.Data.ToObject<ObservableCollection<MasterBarangPaketDetailDto>>();
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

            ReloadData();
            Vm.IsLoading = false;
            await Task.FromResult(IsTest);
        }

        public void ReloadData()
        {
            Vm.DataGroup = new ListCollectionView(Vm.Data);
            using (Vm.DataGroup.DeferRefresh())
            {
                Vm.DataGroup.GroupDescriptions.Add(new PropertyGroupDescription("IdPaketBarang"));
            }
        }
    }
}
