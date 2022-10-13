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
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class KoreksiFormView : UserControl
    {
        private bool _isMapInitiated;

        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;

        public KoreksiFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            Initialize();
            CheckButton();
        }

        private void Initialize()
        {
            DataPendaftaran.IsChecked = true;
            _isMapInitiated = false;

            var viewModel = (PendaftaranViewModel)DataContext;

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
                Title.Text = "Koreksi Pendaftaran Sambungan Baru";
            }
            else
            {
                Title.Text = "Detail Pendaftaran Sambungan Baru";
            }


            viewModel.IsAlamatMapFormChecked = viewModel.FormData.Alamat == viewModel.FormData.AlamatMap;

            #region foto
            viewModel.IsFotoSuratPernyataanFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoSuratPernyataan);
            viewModel.IsFotoKkFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoKk);
            viewModel.IsFotoKtpFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoKtp);
            viewModel.IsFotoImbFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoImb);
            #endregion

            #region denah map
            viewModel.IsDenahLokasiFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) &&
                                                  !string.IsNullOrWhiteSpace(viewModel.FormData.Longitude);
            #endregion

            #region combo box
            viewModel.KelurahanForm = viewModel.KelurahanList.FirstOrDefault(i => i.IdKelurahan == viewModel.FormData.IdKelurahan);
            viewModel.RayonForm = viewModel.RayonList.FirstOrDefault(i => i.IdRayon == viewModel.FormData.IdRayon);
            viewModel.BlokForm = viewModel.BlokList.FirstOrDefault(i => i.IdBlok == viewModel.FormData.IdBlok);
            viewModel.JenisBangunanForm = viewModel.JenisBangunanList.FirstOrDefault(i => i.IdJenisBangunan == viewModel.FormData.IdJenisBangunan);
            viewModel.KepemilikanBangunanForm = viewModel.KepemilikanBangunanList.FirstOrDefault(i => i.IdKepemilikan == viewModel.FormData.IdKepemilikan);
            viewModel.PeruntukanForm = viewModel.PeruntukanList.FirstOrDefault(i => i.IdPeruntukan == viewModel.FormData.IdPeruntukan);
            viewModel.PekerjaanForm = viewModel.PekerjaanList.FirstOrDefault(i => i.IdPekerjaan == viewModel.FormData.IdPekerjaan);
            viewModel.SumberAirForm = viewModel.SumberAirList.FirstOrDefault(i => i.IdSumberAir == viewModel.FormData.IdSumberAir);
            viewModel.TipePendaftaranForm = viewModel.TipePendaftaranList.FirstOrDefault(i => i.IdTipePendaftaranSambungan == viewModel.FormData.IdTipePendaftaranSambungan);
            #endregion

            switch (viewModel.FormData.StatusPermohonanText)
            {
                case "Menunggu Pelunasan Reguler":
                    SetStep(2);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FCE9ED");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#E31F52");
                    break;
                case "Menunggu SPK":
                    SetStep(3);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFF8EA");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#f2542a");
                    break;
                case "Menunggu RAB":
                    SetStep(4);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDCCF4");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#b263d7");
                    break;
                case "Menunggu BPPI":
                    SetStep(4);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#CCEDFF");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#bc722d");
                    break;
                case "Menunggu Pelunasan RAB":
                    SetStep(5);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FCE9ED");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#bc722d");
                    break;
                case "Menunggu SPKP":
                    SetStep(6);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DDCCF4");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#026ab5");
                    break;
                case "Menunggu BA":
                    SetStep(7);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#D6A23A");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1e7b49");
                    break;
                case "Menunggu Verifikasi":
                    SetStep(8);
                    StatusBg.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#96DAFF");
                    StatusText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#1e7b49");
                    break;
                default:
                    SetStep(1);
                    break;
            }
        }

        private void SetStep(int step)
        {
            switch (step)
            {
                case 1:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 2:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 3:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 4:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 5:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 6:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 7:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BDBDBD");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#A7A7A7");
                    break;
                case 8:
                    StepNumber1.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber2.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber3.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber4.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber5.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber6.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber7.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepNumber8.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#0088E2");

                    StepText1.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText3.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText4.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText5.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText6.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText7.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#4BCA81");
                    StepText8.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#252B46");
                    break;
                default:
                    break;
            }
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
            //var viewModel = (PendaftaranViewModel)DataContext;

            //if (string.IsNullOrEmpty(viewModel.FormData.Nama) || string.IsNullOrEmpty(viewModel.FormData.NoHp) ||
            //    string.IsNullOrEmpty(viewModel.FormData.Alamat) || string.IsNullOrEmpty(viewModel.FormData.NoKtp) ||
            //    string.IsNullOrEmpty(viewModel.FormData.NoKk) || viewModel.FormData.IdKelurahan == null ||
            //    viewModel.FormData.IdRayon == null || viewModel.FormData.Keterangan == null)
            //{
            //    OkButton.IsEnabled = false;
            //}
            //else
            //{
            //    OkButton.IsEnabled = true;
            //}
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            DataPendaftaranSection.Visibility = current == "DataPendaftaran" ? Visibility.Visible : Visibility.Collapsed;
            SuratKelengkapanSection.Visibility = current == "SuratKelengkapan" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SetMarker()
        {
            var viewModel = (PendaftaranViewModel)DataContext;
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
            var viewModel = (PendaftaranViewModel)DataContext;
            _ = DialogHost.Show(new MapView(viewModel, RefreshMap), "InnerHublangRootDialog");
        }

        public bool RefreshMap()
        {
            if (DataContext is PendaftaranViewModel viewModel)
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
            var viewModel = (PendaftaranViewModel)DataContext;

            if (string.IsNullOrEmpty(Lokasi.Text) || viewModel == null)
                return;

            _ = SearchAddressAsync(viewModel);
        }

        private async Task SearchAddressAsync(PendaftaranViewModel viewModel)
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
            if (DataContext is PendaftaranViewModel viewModel)
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
            var viewModel = (PendaftaranViewModel)DataContext;
            viewModel.FormData.Latitude = _currentLat;
            viewModel.FormData.Longitude = _currentLon;
            viewModel.FormData.AlamatMap = _currentAlamatMap;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PendaftaranViewModel)DataContext;

            if (viewModel.FormData.IdTipePendaftaranSambungan == null || string.IsNullOrEmpty(viewModel.FormData.Nama) || string.IsNullOrEmpty(viewModel.FormData.NoHp) ||
                    string.IsNullOrEmpty(viewModel.FormData.Alamat) || string.IsNullOrEmpty(viewModel.FormData.NoKtp) ||
                    string.IsNullOrEmpty(viewModel.FormData.NoKk) || viewModel.FormData.IdKelurahan == null ||
                    viewModel.FormData.IdRayon == null || string.IsNullOrEmpty(viewModel.FormData.Keterangan))
            {
                ShowAlert();
            }
            else
            {
                if (viewModel.IsAlamatMapFormChecked)
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.FormData.AlamatMap))
                    {
                        viewModel.FormData.Alamat = viewModel.FormData.AlamatMap;
                    }
                }

                var body = new Dictionary<string, dynamic>
                {
                    { "IdTipePermohonan", viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan },
                    { "DetailBiaya" , null }, // saat koreksi tidak boleh merubah biaya
                    { "IdUser" , Application.Current.Resources["IdUser"].ToString() },
                };

                var type = typeof(PermohonanNonPelangganForm);
                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name != "IdPdam" && property.Name != "IdUserRequest" &&
                        property.Name != "FlagUI" && property.Name != "IdTipePermohonan" &&
                        property.Name != "IdUser" && property.Name != "DetailBiaya" && property.Name != "DetailParameter" &&
                        property.Name != "IdJenisNonAir" && property.Name != "FotoKtp" && property.Name != "FotoImb" &&
                        property.Name != "FotoKk" && property.Name != "FotoSuratPernyataan" && property.Name != "Parameter")
                    {
                        body.Add(property.Name, property.GetValue(viewModel.FormData));
                    }
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
                _ = ((AsyncCommandBase)viewModel.OnSubmitEditFormCommand).ExecuteAsync(body);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ShowAlert()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Form Tidak Lengkap",
                    "Mohon lengkapi data yang dibutuhkan. Field bertanda (*) bersifat mandatory.",
                    "warning",
                    "Oke",
                    false,
                    "hublang"
                ), "SambunganSubDialog");
        }
    }
}
