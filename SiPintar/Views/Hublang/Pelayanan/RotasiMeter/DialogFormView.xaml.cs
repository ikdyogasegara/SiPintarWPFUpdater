using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.RotasiMeter
{
    public partial class DialogFormView : UserControl
    {
        private readonly RotasiMeterViewModel _viewModel;

        private bool _isMapInitiated;
        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as RotasiMeterViewModel;
            DataContext = _viewModel;

            if (_viewModel.IsFor == "add")
                Title.Text = $"Tambah Jadwal Rotasi / Pergantian Meter Rutin";
            else if (_viewModel.IsFor == "edit")
                Title.Text = $"Koreksi Jadwal Rotasi / Pergantian Meter Rutin";
            else
                Title.Text = $"Detail Jadwal Rotasi / Pergantian Meter Rutin";

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            NamaPelanggan.KeyUp += Cari_KeyUp;
            NomorSambungan.KeyUp += Cari_KeyUp;
            Alamat.KeyUp += Cari_KeyUp;

            TglPermohonan.SelectedDate = DateTime.Now;

            if (_viewModel.IsFor == "edit" || _viewModel.IsFor == "detail")
            {
                HeaderAddForm.Visibility = Visibility.Collapsed;
                HeaderEditForm.Visibility = Visibility.Visible;
                BtnSubmit.Content = "Simpan";
                BtnSubmit.Width = 100;
                DataGridPelanggan_MouseDoubleClick(null, null);
            }
            else if (_viewModel.IsFor == "add")
            {
                HeaderAddForm.Visibility = Visibility.Visible;
                HeaderEditForm.Visibility = Visibility.Collapsed;
                BtnSubmit.Content = "Simpan";
                BtnSubmit.Width = 100;
                FormCariPelanggan.Visibility = Visibility.Visible;
                DataGridPelanggan_MouseDoubleClick(null, null);
            }
        }

        private void Cari_KeyUp(object sender, KeyEventArgs e)
        {
            CheckButtonCariPelanggan();
            if (e.Key == Key.Enter && CariPelangganButton.IsEnabled)
            {
                CariPelangganButton_Click(null, null);
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckButtonCariPelanggan()
        {
            if (NamaPelanggan.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (NomorSambungan.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            if (Alamat.Text.Trim().Length > 0)
            {
                CariPelangganButton.IsEnabled = true;
                return;
            }

            CariPelangganButton.IsEnabled = false;
        }

        private void CariPelangganButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NamaPelanggan.Text) || !string.IsNullOrWhiteSpace(Alamat.Text) || !string.IsNullOrWhiteSpace(NomorSambungan.Text))
            {
                _viewModel.DataPelanggan.Clear();

                var param = new Dictionary<string, dynamic>
                {
                    {"Nama", NamaPelanggan.Text},
                    {"Alamat", Alamat.Text},
                    {"pageSize", _viewModel.LimitDataPelanggan.Key},
                    {"currentPage", _viewModel.CurrentPagePelanggan},
                    {"Nosamb", NomorSambungan.Text},
                };

                _ = ((AsyncCommandBase)_viewModel.OnCariPelangganCommand).ExecuteAsync(param);
            }
        }

        private void DataGridPelanggan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.SelectedDataPelanggan != null)
            {
                _viewModel.IsCariPelangganForm = false;

                FormInputData.Visibility = Visibility.Visible;
                BtnSubmit.Visibility = Visibility.Visible;

                IsAlamatMap.Visibility = Visibility.Visible;

                if (_viewModel.IsFor == "detail")
                {
                    IsAlamatMap.Visibility = Visibility.Collapsed;
                    BtnSubmit.Visibility = Visibility.Collapsed;
                }

                StepCariPelangganNumber.Background = new SolidColorBrush(Color.FromRgb(75, 202, 129));
                StepCariPelangganText.Foreground = new SolidColorBrush(Color.FromRgb(75, 202, 129));
                StepIsiFormNumber.Background = new SolidColorBrush(Color.FromRgb(0, 136, 226));
                StepIsiFormText.Foreground = new SolidColorBrush(Color.FromRgb(37, 43, 70));

                if (_viewModel.IsFor == "add")
                {
                    BtnBack.Visibility = Visibility.Visible;
                    Tips.Visibility = Visibility.Collapsed;
                }

                scrollBar.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                _viewModel.FormData = JsonConvert.DeserializeObject<JadwalGantiMeterWpf>(JsonConvert.SerializeObject(_viewModel.SelectedDataPelanggan));

                #region jika edit atau detail biar ambil map dan foto dari permohonan
                if (_viewModel.IsFor != "add" && _viewModel.SelectedData != null)
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.LatitudeWpf))
                    {
                        _viewModel.FormData.Latitude = _viewModel.SelectedData.LatitudeWpf;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.LongitudeWpf))
                    {
                        _viewModel.FormData.Longitude = _viewModel.SelectedData.LongitudeWpf;
                    }

                    // if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.AlamatMapWpf))
                    // {
                    //     _viewModel.FormData.AlamatMap = _viewModel.SelectedData.AlamatMapWpf;
                    // }
                }
                #endregion

                InitializeFotoDanMap();
            }
        }

        private void InitializeFotoDanMap()
        {
            _isMapInitiated = false;

            var viewModel = (RotasiMeterViewModel)DataContext;

            MapConfigHelper.Initiate(MainMap);
            MapConfigHelper.OnLoaded(MainMap, false, 16);

            if (!_isMapInitiated)
            {
                if (viewModel.FormData != null && !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) && !string.IsNullOrWhiteSpace(viewModel.FormData.Longitude))
                {
                    _ = MapConfigHelper.GetAddressByLatLonAsync(viewModel.FormData.Latitude, viewModel.FormData.Longitude);
                    MapConfigHelper.SetCenter(MainMap, viewModel.FormData.Latitude, viewModel.FormData.Longitude);
                    _isMapInitiated = true;
                }
            }

            viewModel.IsDenahLokasiFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) && !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude);

            if (_viewModel.IsFor == "detail")
            {
                if (!viewModel.IsDenahLokasiFormChecked)
                {
                    TitikLokasi.Visibility = Visibility.Collapsed;
                }

                if (!viewModel.IsDenahLokasiFormChecked)
                {
                    FotodanMapSection.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsFor == "edit")
            {
                _viewModel.FormData.IdPelangganAir = _viewModel.SelectedData.IdPelangganAir;
                _viewModel.FormData.Tahun = _viewModel.SelectedData.Tahun;
            }

            _viewModel.FormData.Tahun = 2022;
            _viewModel.FormData.IdPelangganAir = _viewModel.SelectedDataPelanggan.IdPelangganAir;
            _viewModel.FormData.IdJenisGantiMeter = 1;
            _viewModel.FormData.IdGolongan = _viewModel.SelectedDataPelanggan.IdGolongan;
            _viewModel.FormData.IdDiameter = _viewModel.SelectedDataPelanggan.IdDiameter;
            _viewModel.FormData.IdRayon = _viewModel.SelectedDataPelanggan.IdRayon;
            _viewModel.FormData.IdKelurahan = _viewModel.SelectedDataPelanggan.IdKelurahan;
            _viewModel.FormData.IdMerekMeter = _viewModel.SelectedDataPelanggan.MasterPelangganAirDetail?.IdMerekMeter ?? null;
            _viewModel.FormData.NoSeriMeter = _viewModel.SelectedDataPelanggan.MasterPelangganAirDetail?.NoSeriMeter ?? null;
            _viewModel.FormData.TglJadwal = TglPermohonan.SelectedDate?.AddMilliseconds(DateTime.Now.TimeOfDay.TotalMilliseconds) ?? DateTime.Now;
            _viewModel.FormData.TglMeter = _viewModel.SelectedDataPelanggan.MasterPelangganAirDetail?.TglMeter ?? null;
            // _viewModel.FormData.IdUser = Convert.ToInt32(Application.Current.Resources["IdUser"].ToString());

            _viewModel.FormDataDetailParameter = new List<ParamPermohonanParameterDto>();
            _viewModel.FormDataDetailBiaya = new List<ParamPermohonanBiayaDto>();

            #region add to parameter body
            var param = JsonConvert.DeserializeObject<ParamPermohonanGlobalDto>(JsonConvert.SerializeObject(_viewModel.FormData));
            if (param != null)
            {
                param.DetailParameter = _viewModel.FormDataDetailParameter;
                param.DetailBiaya = _viewModel.FormDataDetailBiaya;

                var data = param;
                var type = typeof(ParamJadwalGantiMeterDto);
                var properties = type.GetProperties();
                var body = new Dictionary<string, dynamic>();
                foreach (var property in properties)
                {
                    if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                    {
                        body.Add(property.Name, property.GetValue(data));
                    }
                }

                #endregion

                DialogHost.CloseDialogCommand.Execute(null, null);
                _ = ((AsyncCommandBase)_viewModel.OnSubmitFormCommand).ExecuteAsync(body);
            }
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            DataGridPelanggan_MouseDoubleClick(null, null);
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RotasiMeterViewModel)
                CariPelangganButton_Click(null, null);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentPagePelanggan--;
            CariPelangganButton_Click(null, null);
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentPagePelanggan++;
            CariPelangganButton_Click(null, null);
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            _viewModel.IsCariPelangganForm = true;
            FormInputData.Visibility = Visibility.Collapsed;
            BtnSubmit.Visibility = Visibility.Collapsed;
            StepCariPelangganNumber.Background = new SolidColorBrush(Color.FromRgb(0, 136, 226));
            StepCariPelangganText.Foreground = new SolidColorBrush(Color.FromRgb(37, 43, 70));
            StepIsiFormNumber.Background = new SolidColorBrush(Color.FromRgb(167, 167, 167));
            StepIsiFormText.Foreground = new SolidColorBrush(Color.FromRgb(167, 167, 167));
            BtnBack.Visibility = Visibility.Collapsed;
            Tips.Visibility = Visibility.Visible;
            scrollBar.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        #region GMAP CONTROL
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainMap.Zoom < MainMap.MaxZoom)
                MainMap.Zoom++;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainMap.Zoom > MainMap.MinZoom)
                MainMap.Zoom--;
        }

        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (RotasiMeterViewModel)DataContext;
            _ = DialogHost.Show(new MapView(viewModel, RefreshMap), "InnerGlobalRootDialog");
        }

        private bool RefreshMap()
        {
            if (DataContext is RotasiMeterViewModel viewModel)
            {
                MapConfigHelper.SetCenter(MainMap, viewModel.FormData.Latitude, viewModel.FormData.Longitude);
            }
            return true;
        }

        private void Lokasi_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click();
        }

        private void Search_Click(object sender = null, RoutedEventArgs e = null)
        {
            var viewModel = (RotasiMeterViewModel)DataContext;

            if (string.IsNullOrEmpty(Lokasi.Text) || viewModel == null)
                return;

            _ = SearchAddressAsync(viewModel);
        }

        private async Task SearchAddressAsync(RotasiMeterViewModel viewModel)
        {
            viewModel.IsLoadingMap = true;

            var result = await MapConfigHelper.GetLatLonByAddressAsync(Lokasi.Text);

            if (result != null && result.Length > 0)
            {
                _currentLat = result[0].Replace(',', '.');
                _currentLon = result[1].Replace(',', '.');

                MapConfigHelper.SetCenter(MainMap, _currentLat, _currentLon);
                CenterMarker.Visibility = Visibility.Visible;
                LonText.Text = _currentLon;
                LatText.Text = _currentLat;

                var alamatResult = await MapConfigHelper.GetAddressByLatLonAsync(_currentLat, _currentLon);
                AlamatMap.Text = alamatResult;
                _currentAlamatMap = alamatResult;

                SetCurrentPoint();
            }

            viewModel.IsLoadingMap = false;
        }

        private void MainMap_OnMapDrag()
        {
            if (DataContext is RotasiMeterViewModel viewModel)
            {
                viewModel.IsLoadingMap = true;
                viewModel.IsLoadingForm = true;
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    CenterMarker.Visibility = Visibility.Visible;
                    var center = MainMap.Position;
                    _ = GetAddressNameAsync(center);
                    viewModel.IsLoadingMap = false;
                    viewModel.IsLoadingForm = false;
                };
            }
        }

        private async Task GetAddressNameAsync(PointLatLng center)
        {
            _currentLat = center.Lat.ToString().Replace(',', '.');
            _currentLon = center.Lng.ToString().Replace(',', '.');

            var result = await MapConfigHelper.GetAddressByLatLonAsync(_currentLat, _currentLon);
            AlamatMap.Text = result;
            _currentAlamatMap = result;
            LonText.Text = _currentLon;
            LatText.Text = _currentLat;

            Debug.WriteLine(result);

            SetCurrentPoint();
        }

        private void SetCurrentPoint()
        {
            var viewModel = (RotasiMeterViewModel)DataContext;
            viewModel.FormData.Latitude = _currentLat;
            viewModel.FormData.Longitude = _currentLon;
            // viewModel.FormData.AlamatMap = _currentAlamatMap;
        }

        #endregion
    }
}
