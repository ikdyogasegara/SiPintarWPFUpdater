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
    public partial class KoreksiPelangganAirFormView : UserControl
    {
        private bool _isMapInitiated;
        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;

        public KoreksiPelangganAirFormView(object dataContext)
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
                Title.Text = "Koreksi Pelanggan Air";
            }
            else
            {
                Title.Text = "Detail Pelanggan Air";
            }

            viewModel.IsAlamatMapFormChecked = viewModel.FormData.Alamat == viewModel.FormData.AlamatMap;

            viewModel.IsFotoRumah1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoRumah1);
            viewModel.IsFotoRumah2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoRumah2);
            viewModel.IsFotoRumah3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoRumah3);

            viewModel.IsDenahLokasiFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) &&
                                                 !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude);

            viewModel.GolonganForm = viewModel.GolonganList?.FirstOrDefault(i => i.IdGolongan == viewModel.FormData.IdGolongan);
            viewModel.DiameterForm = viewModel.DiameterList?.FirstOrDefault(i => i.IdDiameter == viewModel.FormData.IdDiameter);
            viewModel.KelurahanForm = viewModel.KelurahanList?.FirstOrDefault(i => i.IdKelurahan == viewModel.FormData.IdKelurahan);
            viewModel.RayonForm = viewModel.RayonList?.FirstOrDefault(i => i.IdRayon == viewModel.FormData.IdRayon);
            viewModel.BlokForm = viewModel.BlokList?.FirstOrDefault(i => i.IdBlok == viewModel.FormData.MasterPelangganAirDetail?.IdBlok);
            viewModel.JenisBangunanForm = viewModel.JenisBangunanList?.FirstOrDefault(i => i.IdJenisBangunan == viewModel.FormData.MasterPelangganAirDetail?.IdJenisBangunan);
            viewModel.KepemilikanBangunanForm = viewModel.KepemilikanBangunanList?.FirstOrDefault(i => i.IdKepemilikan == viewModel.FormData.MasterPelangganAirDetail?.IdKepemilikan);
            viewModel.PeruntukanForm = viewModel.PeruntukanList?.FirstOrDefault(i => i.IdPeruntukan == viewModel.FormData.MasterPelangganAirDetail?.IdPeruntukan);
            viewModel.PekerjaanForm = viewModel.PekerjaanList?.FirstOrDefault(i => i.IdPekerjaan == viewModel.FormData.MasterPelangganAirDetail?.IdPekerjaan);
            viewModel.SumberAirForm = viewModel.SumberAirList?.FirstOrDefault(i => i.IdSumberAir == viewModel.FormData.MasterPelangganAirDetail?.IdSumberAir);
            viewModel.DmaForm = viewModel.DmaList?.FirstOrDefault(i => i.IdDma == viewModel.FormData.MasterPelangganAirDetail?.IdDma);
            viewModel.DmzForm = viewModel.DmzList?.FirstOrDefault(i => i.IdDmz == viewModel.FormData.MasterPelangganAirDetail?.IdDmz);
            viewModel.MerekMeterForm = viewModel.MerekMeterList?.FirstOrDefault(i => i.IdMerekMeter == viewModel.FormData.MasterPelangganAirDetail?.IdMerekMeter);
            viewModel.KondisiMeterForm = viewModel.KondisiMeterList?.FirstOrDefault(i => i.IdKondisiMeter == viewModel.FormData.MasterPelangganAirDetail?.IdKondisiMeter);
            viewModel.AdministrasiLainForm = viewModel.AdministrasiLainList?.FirstOrDefault(i => i.IdAdministrasiLain == viewModel.FormData.MasterPelangganAirDetail?.IdAdministrasiLain);
            viewModel.PemeliharaanLainForm = viewModel.PemeliharaanLainList?.FirstOrDefault(i => i.IdPemeliharaanLain == viewModel.FormData.MasterPelangganAirDetail?.IdPemeliharaanLain);
            viewModel.RetribusiLainForm = viewModel.RetribusiLainList?.FirstOrDefault(i => i.IdRetribusiLain == viewModel.FormData.MasterPelangganAirDetail?.IdRetribusiLain);
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
            body.Add("IdPelangganAir", viewModel.SelectedData.IdPelangganAir);

            if (!string.IsNullOrWhiteSpace(viewModel.FormData.Nama))
                body.Add("Nama", viewModel.FormData.Nama);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.NoHp))
                body.Add("NoHp", viewModel.FormData.MasterPelangganAirDetail.NoHp);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.NoTelp))
                body.Add("NoTelp", viewModel.FormData.MasterPelangganAirDetail.NoTelp);
            if (viewModel.PekerjaanForm != null)
                body.Add("IdPekerjaan", viewModel.PekerjaanForm.IdPekerjaan);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.NoKk))
                body.Add("NoKk", viewModel.FormData.MasterPelangganAirDetail.NoKk);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.Email))
                body.Add("Email", viewModel.FormData.MasterPelangganAirDetail.Email);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.NoKtp))
                body.Add("NoKtp", viewModel.FormData.MasterPelangganAirDetail.NoKtp);
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
            if (viewModel.BlokForm != null)
                body.Add("IdBlok", viewModel.BlokForm.IdBlok);
            if (viewModel.DmaForm != null)
                body.Add("IdDma", viewModel.DmaForm.IdDma);
            if (viewModel.DmzForm != null)
                body.Add("IdDmz", viewModel.DmzForm.IdDmz);
            if (viewModel.GolonganForm != null)
                body.Add("IdGolongan", viewModel.GolonganForm.IdGolongan);
            if (viewModel.DiameterForm != null)
                body.Add("IdDiameter", viewModel.DiameterForm.IdDiameter);
            if (viewModel.AdministrasiLainForm != null)
                body.Add("IdAdministrasiLain", viewModel.AdministrasiLainForm.IdAdministrasiLain);
            if (viewModel.PemeliharaanLainForm != null)
                body.Add("IdPemeliharaanLain", viewModel.PemeliharaanLainForm.IdPemeliharaanLain);
            if (viewModel.RetribusiLainForm != null)
                body.Add("IdRetribusiLain", viewModel.RetribusiLainForm.IdRetribusiLain);
            if (viewModel.FormData.MasterPelangganAirDetail.Penghuni.HasValue)
                body.Add("Penghuni", viewModel.FormData.MasterPelangganAirDetail.Penghuni);
            if (viewModel.FormData.MasterPelangganAirDetail.LuasTanah.HasValue)
                body.Add("LuasTanah", viewModel.FormData.MasterPelangganAirDetail.LuasTanah);
            if (viewModel.FormData.MasterPelangganAirDetail.LuasRumah.HasValue)
                body.Add("LuasRumah", viewModel.FormData.MasterPelangganAirDetail.LuasRumah);
            if (viewModel.JenisBangunanForm != null)
                body.Add("IdJenisBangunan", viewModel.JenisBangunanForm.IdJenisBangunan);
            if (viewModel.KepemilikanBangunanForm != null)
                body.Add("IdKepemilikan", viewModel.KepemilikanBangunanForm.IdKepemilikan);
            if (viewModel.PeruntukanForm != null)
                body.Add("IdPeruntukan", viewModel.PeruntukanForm.IdPeruntukan);
            if (viewModel.SumberAirForm != null)
                body.Add("IdSumberAir", viewModel.SumberAirForm.IdSumberAir);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.KodePost))
                body.Add("KodePost", viewModel.FormData.MasterPelangganAirDetail.KodePost);
            if (viewModel.FormData.MasterPelangganAirDetail.DayaListrik.HasValue)
                body.Add("DayaListrik", viewModel.FormData.MasterPelangganAirDetail.DayaListrik);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.Keterangan))
                body.Add("Keterangan", viewModel.FormData.MasterPelangganAirDetail.Keterangan);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.NamaPemilik))
                body.Add("NamaPemilik", viewModel.FormData.MasterPelangganAirDetail.NamaPemilik);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.AlamatPemilik))
                body.Add("AlamatPemilik", viewModel.FormData.MasterPelangganAirDetail.AlamatPemilik);
            if (viewModel.MerekMeterForm != null)
                body.Add("IdMerekMeter", viewModel.MerekMeterForm.IdMerekMeter);
            if (viewModel.KondisiMeterForm != null)
                body.Add("IdKondisiMeter", viewModel.KondisiMeterForm.IdKondisiMeter);
            if (!string.IsNullOrWhiteSpace(viewModel.FormData.MasterPelangganAirDetail.NoSeriMeter))
                body.Add("NoSeriMeter", viewModel.FormData.MasterPelangganAirDetail.NoSeriMeter);
            if (viewModel.FormData.MasterPelangganAirDetail.UrutanBaca.HasValue)
                body.Add("UrutanBaca", viewModel.FormData.MasterPelangganAirDetail.UrutanBaca);
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
