using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;

namespace SiPintar.Views.Global.Dialog
{
    /// <summary>
    /// Interaction logic for DialogPilihBarangMaterialView.xaml
    /// </summary>
    public partial class DialogPilihBarangMaterialView : UserControl
    {
        private readonly Func<MasterBarangDto, bool> _setData;
        private readonly Func<MasterBarangDto, Task<bool>> _setDataTask;
        private readonly IRestApiClientModel _restApi;
        private readonly int? _idGudang;
        private readonly bool? _notEmptyStock;

        public DialogPilihBarangMaterialView(Func<MasterBarangDto, bool> setData = null, IRestApiClientModel restApi = null, int? idGudang = null, bool? notEmptyStok = false,
            Func<MasterBarangDto, Task<bool>> setDataTask = null)
        {
            InitializeComponent();
            _setData = setData;
            _restApi = restApi;
            _idGudang = idGudang;
            _notEmptyStock = notEmptyStok;
            _ = GetDataAsync();
            _setDataTask = setDataTask;
        }

        private async Task GetDataAsync()
        {
            Loading.Visibility = Visibility.Visible;
            Empty.Visibility = Visibility.Collapsed;

            var param = new Dictionary<string, dynamic> {
                { "NamaBarang", NamaBarang.Text },
                { "KodeBarang", KodeBarang.Text },
            };

            if (_idGudang != null)
            {
                param.Add("IdGudang", _idGudang);
            }
            if (_notEmptyStock != null)
            {
                param.Add("LihatStock", true);
                param.Add("NotEmptyStock", _notEmptyStock);
            }

            var res = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-barang", param);

            Loading.Visibility = Visibility.Collapsed;
            if (!res.IsError && res.Data.Status && res.Data.Data != null)
            {
                var resData = res.Data.Data.ToObject<ObservableCollection<MasterBarangDto>>();
                if (resData != null && resData.Count > 0)
                {
                    DataGridBarang.ItemsSource = resData;
                }
                else
                {
                    Empty.Visibility = Visibility.Visible;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_setData != null && DataGridBarang.SelectedItem is MasterBarangDto s)
            {
                _ = _setData(s);
            }
            if (_setDataTask != null && DataGridBarang.SelectedItem is MasterBarangDto y)
            {
                _ = _setDataTask(y);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CariClick(object sender, RoutedEventArgs e)
        {
            _ = GetDataAsync();
        }
    }
}
