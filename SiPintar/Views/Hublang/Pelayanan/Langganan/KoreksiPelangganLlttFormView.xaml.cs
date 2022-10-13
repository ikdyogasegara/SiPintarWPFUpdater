using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Langganan
{
    public partial class KoreksiPelangganLlttFormView : UserControl
    {
        private bool _isMapInitiated;

        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;

        public KoreksiPelangganLlttFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            Initialize();
            CheckButton();
        }

        private void Initialize()
        {
            DataLangganan.IsChecked = true;
            _isMapInitiated = false;

            var viewModel = (LanggananViewModel)DataContext;

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

            if (viewModel.IsEdit)
            {
                Title.Text = "Koreksi Pelanggan L2T2";
            }
            else
            {
                Title.Text = "Detail Pelanggan L2T2";
            }

            viewModel.IsAlamatMapFormChecked = viewModel.FormData.Alamat == viewModel.FormData.AlamatMap;

            viewModel.IsFotoRumah1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoRumah1);
            viewModel.IsFotoRumah2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoRumah2);
            viewModel.IsFotoRumah3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoRumah3);

            viewModel.IsDenahLokasiFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) &&
                                                 !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude);

            viewModel.TarifLlttForm = viewModel.TarifLlttList.FirstOrDefault(i => i.IdTarifLltt == viewModel.FormData.IdTarifLltt);
            viewModel.RayonForm = viewModel.RayonList?.FirstOrDefault(i => i.IdRayon == viewModel.FormData.IdRayon);
            viewModel.KelurahanForm = viewModel.KelurahanList?.FirstOrDefault(i => i.IdKelurahan == viewModel.FormData.IdKelurahan);
            viewModel.KolektifForm = viewModel.KolektifList?.FirstOrDefault(i => i.IdKolektif == viewModel.FormData.IdKolektif);
            viewModel.FlagForm = viewModel.FlagList?.FirstOrDefault(i => i.IdFlag == viewModel.FormData.IdFlag);
            viewModel.StatusForm = viewModel.StatusList?.FirstOrDefault(i => i.IdStatus == viewModel.FormData.IdStatus);
        }

        private static void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void KodePair_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        #region GMAP CONTROL
        private void ZoomInButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainMap.Zoom < MainMap.MaxZoom)
                MainMap.Zoom++;
        }

        private void ZoomOutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainMap.Zoom > MainMap.MinZoom)
                MainMap.Zoom--;
        }
        #endregion

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            DataLanggananSection.Visibility = current == "DataLangganan" ? Visibility.Visible : Visibility.Collapsed;
            FotodanMapSection.Visibility = current == "FotodanMap" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SetMarker()
        {
            var viewModel = (LanggananViewModel)DataContext;
            if (viewModel.FormData != null && viewModel.FormData.Latitude != null && viewModel.FormData.Longitude != null)
            {
                var markerList = new List<MapMarkerObject>()
                {
                    new MapMarkerObject()
                    {
                        MarkerInformation = $"{viewModel.FormData.Nama}",
                        Latitude = viewModel.FormData.Latitude,
                        Longitude = viewModel.FormData.Longitude
                    }
                };

                MapConfigHelper.SetMarker(MainMap, markerList);
            }
        }

        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (LanggananViewModel)DataContext;
            _ = DialogHost.Show(new MapView(viewModel, RefreshMap), "InnerHublangRootDialog");
        }

        public bool RefreshMap()
        {
            if (DataContext is LanggananViewModel viewModel)
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
            var viewModel = (LanggananViewModel)DataContext;

            if (string.IsNullOrEmpty(Lokasi.Text) || viewModel == null)
                return;

            _ = SearchAddressAsync(viewModel);
        }

        private async Task SearchAddressAsync(LanggananViewModel viewModel)
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
            if (DataContext is LanggananViewModel viewModel)
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
            var viewModel = (LanggananViewModel)DataContext;
            viewModel.FormData.Latitude = _currentLat;
            viewModel.FormData.Longitude = _currentLon;
            viewModel.FormData.AlamatMap = _currentAlamatMap;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (LanggananViewModel)DataContext;

            var body = new Dictionary<string, dynamic>();
            body.Add("IdPelangganLltt", viewModel.SelectedData.IdPelangganLltt);

            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Nama))
                body.Add("Nama", viewModel.FormData.Nama);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.NoHp))
                body.Add("NoHp", viewModel.FormData.NoHp);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.NoTelp))
                body.Add("NoTelp", viewModel.FormData.NoTelp);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Email))
                body.Add("Email", viewModel.FormData.Email);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.NoKtp))
                body.Add("NoKtp", viewModel.FormData.NoKtp);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Alamat))
                body.Add("Alamat", viewModel.IsAlamatMapFormChecked ? viewModel.FormData.AlamatMap : viewModel.FormData.Alamat);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Rt))
                body.Add("Rt", viewModel.FormData.Rt);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Rw))
                body.Add("Rw", viewModel.FormData.Rw);
            if (viewModel.RayonForm != null)
                body.Add("IdRayon", viewModel.RayonForm.IdRayon);
            if (viewModel.KelurahanForm != null)
                body.Add("IdKelurahan", viewModel.KelurahanForm.IdKelurahan);
            if (viewModel.TarifLlttForm != null)
                body.Add("IdTarifLltt", viewModel.TarifLlttForm.IdTarifLltt);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Keterangan))
                body.Add("Keterangan", viewModel.FormData.Keterangan);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.AlamatMap))
                body.Add("AlamatMap", viewModel.FormData.AlamatMap);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Latitude))
                body.Add("Latitude", viewModel.FormData.Latitude);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Longitude))
                body.Add("Longitude", viewModel.FormData.Longitude);
            if (viewModel.KolektifForm != null)
                body.Add("IdKolektif", viewModel.KolektifForm.IdKolektif);
            if (viewModel.FlagForm != null)
                body.Add("IdFlag", viewModel.FlagForm.IdFlag);
            if (viewModel.StatusForm != null)
                body.Add("IdStatus", viewModel.StatusForm.IdStatus);

            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = ((AsyncCommandBase)viewModel.OnSubmitEditFormCommand).ExecuteAsync(body);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
