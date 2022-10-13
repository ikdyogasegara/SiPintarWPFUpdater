using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;

namespace SiPintar.Views.Global.Dialog
{
    /// <summary>
    /// Interaction logic for PilihPelangganDialog.xaml
    /// </summary>
    public partial class PilihPelangganDialog : UserControl
    {
        public List<JenisPelangganClass> JenisPelangganList;
        public readonly IRestApiClientModel _restApi;
        public List<DataPelangganClass> DataList = new List<DataPelangganClass>();

        private bool _submitSelect;
        private readonly Func<bool> _addFunc;
        private readonly Func<dynamic, bool> _setDataFunc;

        public PilihPelangganDialog(IRestApiClientModel restApi, Func<dynamic, bool> setDataFunc, Func<bool> addFunc = null, List<JenisPelangganType> jenisPelangganType = null)
        {
            InitializeComponent();
            _restApi = restApi;
            _setDataFunc = setDataFunc;

            if (jenisPelangganType?.Count > 0)
            {
                JenisPelangganList = new List<JenisPelangganClass>();
                foreach (var item in jenisPelangganType)
                {
                    switch (item)
                    {
                        case JenisPelangganType.AIR:
                            JenisPelangganList.Add(new JenisPelangganClass() { Key = "AIR", Value = "Pelanggan Air" });
                            break;
                        case JenisPelangganType.LLTT:
                            JenisPelangganList.Add(new JenisPelangganClass() { Key = "LIMBAH", Value = "Pelanggan Limbah" });
                            break;
                        case JenisPelangganType.LIMBAH:
                            JenisPelangganList.Add(new JenisPelangganClass() { Key = "LLTT", Value = "Pelanggan Lltt" });
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                JenisPelangganList = new List<JenisPelangganClass>()
                {
                    new JenisPelangganClass(){ Key = "AIR", Value = "Pelanggan Air"},
                    new JenisPelangganClass(){ Key = "LIMBAH", Value = "Pelanggan Limbah"},
                    new JenisPelangganClass(){ Key = "LLTT", Value = "Pelanggan Lltt"}
                };
            }

            JenisPelanggan.ItemsSource = JenisPelangganList;
            JenisPelanggan.DisplayMemberPath = "Value";
            JenisPelanggan.SelectedItem = null;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            NamaPelanggan.PreviewKeyUp += Cari_KeyUp;
            NomorSambungan.PreviewKeyUp += Cari_KeyUp;
            Alamat.PreviewKeyUp += Cari_KeyUp;

            _addFunc = addFunc;
            Unloaded += PilihPelangganDialog_Unloaded;
        }

        private void PilihPelangganDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_addFunc != null && _submitSelect)
                _addFunc();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            CheckButtonCariPelanggan();
            if (e.Key == Key.Escape && LoadingObject.Visibility != Visibility.Visible)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private async Task GetPelangganAsync()
        {
            LoadingObject.Visibility = Visibility.Visible;
            var selectedJenisPelanggan = JenisPelanggan.SelectedItem as JenisPelangganClass;
            var param = new Dictionary<string, dynamic>();

            if (!string.IsNullOrWhiteSpace(NamaPelanggan.Text))
            {
                if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "AIR")
                {
                    param.Add("Nama", NamaPelanggan.Text);
                }
                else if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "LIMBAH")
                {
                    param.Add("Nama", NamaPelanggan.Text);
                }
                else
                {
                    param.Add("Nama", NamaPelanggan.Text);
                }
            }

            if (!string.IsNullOrWhiteSpace(NomorSambungan.Text))
            {
                if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "AIR")
                {
                    param.Add("NoSamb", NomorSambungan.Text);
                }
                else if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "LIMBAH")
                {
                    param.Add("NomorLimbah", NomorSambungan.Text);
                }
                else
                {
                    param.Add("NomorLltt", NomorSambungan.Text);
                }
            }

            if (!string.IsNullOrWhiteSpace(Alamat.Text))
            {
                if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "AIR")
                {
                    param.Add("Alamat", Alamat.Text);
                }
                else if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "LIMBAH")
                {
                    param.Add("Alamat", Alamat.Text);
                }
                else
                {
                    param.Add("Alamat", Alamat.Text);
                }
            }

            param.Add("pageSize", 100);
            param.Add("currentPage", 1);


            var linkApi = selectedJenisPelanggan.Key switch
            {
                "AIR" => "master-pelanggan-air",
                "LIMBAH" => "master-pelanggan-limbah",
                _ => "master-pelanggan-lltt",
            };

            var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{linkApi}", param));

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status && result.Data != null)
                {
                    var tempList = new List<DataPelangganClass>();
                    if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "AIR")
                    {
                        var temp = result.Data.ToObject<List<MasterPelangganAirDto>>();
                        JenisPelangganList.Clear();
                        temp.ForEach(x => tempList.Add(new DataPelangganClass() { NamaPelanggan = x.Nama, NomorPelanggan = x.NoSamb, Alamat = x.Alamat, Data = x }));
                    }
                    else if (selectedJenisPelanggan != null && selectedJenisPelanggan.Key == "LIMBAH")
                    {
                        var temp = result.Data.ToObject<List<MasterPelangganLimbahDto>>();
                        JenisPelangganList.Clear();
                        temp.ForEach(x => tempList.Add(new DataPelangganClass() { NamaPelanggan = x.Nama, NomorPelanggan = x.NoSamb, Alamat = x.Alamat, Data = x }));
                    }
                    else
                    {
                        var temp = result.Data.ToObject<List<MasterPelangganLlttDto>>();
                        JenisPelangganList.Clear();
                        temp.ForEach(x => tempList.Add(new DataPelangganClass() { NamaPelanggan = x.Nama, NomorPelanggan = x.NoSamb, Alamat = x.Alamat, Data = x }));
                    }
                    DataList = tempList;
                    DataGridPelanggan.ItemsSource = DataList;
                    DataGridPelanggan.UpdateLayout();
                }
            }
            LoadingObject.Visibility = Visibility.Collapsed;
        }

        private void JenisPelanggan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NamaPelanggan.Clear();
            Alamat.Clear();
            NomorSambungan.Clear();
            CariPelangganButton.IsEnabled = false;
        }

        private void CariPelangganButton_Click(object sender, RoutedEventArgs e)
        {
            if (CariPelangganButton.IsEnabled)
            {
                _ = GetPelangganAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPelanggan.SelectedItem != null)
            {
                _submitSelect = true;
                var select = DataGridPelanggan.SelectedItem as DataPelangganClass;
                if (select.Data is MasterPelangganAirDto)
                {
                    var temp = select.Data as MasterPelangganAirDto;
                    _setDataFunc(temp);
                }
                if (select.Data is MasterPelangganLimbahDto)
                {
                    var temp = select.Data as MasterPelangganLimbahDto;
                    _setDataFunc(temp);
                }
                if (select.Data is MasterPelangganLlttDto)
                {
                    var temp = select.Data as MasterPelangganLlttDto;
                    _setDataFunc(temp);
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CheckButtonCariPelanggan()
        {

            if (NamaPelanggan.Text.Trim().Length > 0 && JenisPelanggan.Text != "")
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (NomorSambungan.Text.Trim().Length > 0 && JenisPelanggan.Text != "")
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (Alamat.Text.Trim().Length > 0 && JenisPelanggan.Text != "")
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            CariPelangganButton.IsEnabled = false;
        }

        private void Cari_KeyUp(object sender, KeyEventArgs e)
        {
            CheckButtonCariPelanggan();
            if (e.Key == Key.Enter && CariPelangganButton.IsEnabled)
            {
                CariPelangganButton_Click(null, null);
            }
        }
    }

    public class JenisPelangganClass
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class DataPelangganClass
    {
        public string NamaPelanggan { get; set; }
        public string NomorPelanggan { get; set; }
        public string Alamat { get; set; }
        public dynamic Data { get; set; }
    }

    public enum JenisPelangganType
    {
        AIR,
        LLTT,
        LIMBAH
    }
}
