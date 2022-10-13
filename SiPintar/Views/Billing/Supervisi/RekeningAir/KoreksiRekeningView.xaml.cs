using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    public partial class KoreksiRekeningView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;
        private bool _isProccess;
        private readonly bool? _oldCheckTaksasi;
        private bool _isMapInitiated;
        private string _jenisFoto;
        private bool IsSetEvent { get; set; }


        public KoreksiRekeningView(RekeningAirViewModel dataContext)
        {
            _isProccess = true;
            InitializeComponent();
            DataContext = dataContext;

            _viewModel = (RekeningAirViewModel)DataContext;
            _oldCheckTaksasi = _viewModel.KoreksiForm.Taksasi;

            if (_viewModel.KoreksiForm.FlagPublish == true || _viewModel.SelectedDataPeriode.Status == false)
            {
                PanelStan.Visibility = Visibility.Collapsed;
                TaksirButton.Visibility = Visibility.Collapsed;
                OkButton.Visibility = Visibility.Collapsed;
                PerhitunganBiaya.Visibility = Visibility.Collapsed;
                BtnBatal.Content = "Tutup";
                Title.Text = "Bukti Hasil Baca";
            }

            if (_viewModel.KoreksiForm.Taksasi == true)
            {
                Title.Text = "Koreksi Rekening (Taksasi Pemakaian)";
                CekStanLalu.IsEnabled = true;
                CekStanKini.IsEnabled = true;
                CekStanAngkat.IsEnabled = true;
                CekPakai.IsEnabled = true;

                _viewModel.IsStanLaluChecked = false;
                _viewModel.IsStanKiniChecked = false;
                _viewModel.IsStanAngkatChecked = false;
                _viewModel.IsPakaiChecked = true;
            }
            else
            {
                Title.Text = "Koreksi Rekening";
                CekStanLalu.IsEnabled = true;
                CekStanKini.IsEnabled = true;
                CekStanAngkat.IsEnabled = true;
                CekPakai.IsEnabled = true;

                _viewModel.IsStanLaluChecked = false;
                _viewModel.IsStanKiniChecked = true;
                _viewModel.IsStanAngkatChecked = false;
                _viewModel.IsPakaiChecked = false;
            }

            if (_viewModel.KoreksiForm.StanAngkat > 0)
            {
                CekStanAngkat.IsEnabled = true;
                _viewModel.IsStanAngkatChecked = true;
            }

            StanLalu.Text = _viewModel.KoreksiForm.StanLalu.ToString();
            StanSkrg.Text = _viewModel.KoreksiForm.StanSkrg.ToString();
            StanAngkat.Text = _viewModel.KoreksiForm.StanAngkat.ToString();
            Pakai.Text = _viewModel.KoreksiForm.Pakai.ToString();

            _viewModel.KoreksiSelectedKelainan = _viewModel.MasterKelainanList?.FirstOrDefault(i => i.Kelainan == _viewModel.KoreksiForm.Kelainan);
            _viewModel.IsKelainanChecked = _viewModel.KoreksiSelectedKelainan != null;
            _viewModel.KoreksiSelectedPetugasBaca = _viewModel.MasterPetugasBacaList?.FirstOrDefault(i => i.KodePetugasBaca == _viewModel.KoreksiForm.PetugasBaca);
            _viewModel.IsPetugasBacaChecked = _viewModel.KoreksiSelectedPetugasBaca != null;

            CheckButton();
            InputValidation();
            _isProccess = false;
            HitungStan(1);
            PreviewKeyUp += new KeyEventHandler(HandleKey);
            BuktiPembacaan.IsChecked = true;

        }

        private void HandleKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }

            if (e.Key == Key.Enter && OkButton.IsEnabled == true)
            {
                Submit_Click(null, null);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var type = typeof(RekeningAirDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();

            if (CekPetugasBaca.IsChecked == true && !string.IsNullOrWhiteSpace(PetugasBaca.Text))
            {
                var petugas = _viewModel.MasterPetugasBacaList.FirstOrDefault(c => c.PetugasBaca == PetugasBaca.Text);
                if (petugas != null)
                {
                    _viewModel.KoreksiForm.PetugasBaca = petugas.KodePetugasBaca;
                }
                else
                {
                    _viewModel.KoreksiForm.PetugasBaca = null;
                }
            }
            else
            {
                _viewModel.KoreksiForm.PetugasBaca = null;
            }

            _viewModel.KoreksiForm.Kelainan = CekKelainan.IsChecked == true && !string.IsNullOrWhiteSpace(Kelainan.Text) ? Kelainan.Text : "";

            var param = new RekeningAirDto
            {
                IdUserRequest = _viewModel.SelectedData.IdUserRequest,
                IdPdam = _viewModel.SelectedData.IdPdam,
                IdPeriode = _viewModel.SelectedData.IdPeriode,
                IdPelangganAir = _viewModel.SelectedData.IdPelangganAir,
                IdFlag = _viewModel.SelectedData.IdFlag,
                IdStatus = _viewModel.SelectedData.IdStatus,
                IdKolektif = _viewModel.SelectedData.IdKolektif,
                PetugasBaca = _viewModel.KoreksiForm.PetugasBaca,
                Kelainan = _viewModel.KoreksiForm.Kelainan,
                StanLalu = _viewModel.KoreksiForm.StanLalu,
                StanSkrg = _viewModel.KoreksiForm.StanSkrg,
                StanAngkat = _viewModel.KoreksiForm.StanAngkat,
                Pakai = _viewModel.HitungKoreksi.Pakai,
                PakaiKalkulasi = _viewModel.HitungKoreksi.PakaiKalkulasi,
                BiayaPemakaian = _viewModel.HitungKoreksi.BiayaPemakaian,
                Administrasi = _viewModel.HitungKoreksi.Administrasi,
                Pemeliharaan = _viewModel.HitungKoreksi.Pemeliharaan,
                Retribusi = _viewModel.HitungKoreksi.Retribusi,
                Pelayanan = _viewModel.HitungKoreksi.Pelayanan,
                AirLimbah = _viewModel.HitungKoreksi.AirLimbah,
                DendaPakai0 = _viewModel.HitungKoreksi.DendaPakai0,
                AdministrasiLain = _viewModel.HitungKoreksi.AdministrasiLain,
                PemeliharaanLain = _viewModel.HitungKoreksi.PemeliharaanLain,
                RetribusiLain = _viewModel.HitungKoreksi.RetribusiLain,
                Ppn = _viewModel.HitungKoreksi.Ppn,
                Meterai = _viewModel.HitungKoreksi.Meterai,
                Rekair = _viewModel.HitungKoreksi.Rekair,
                Denda = _viewModel.HitungKoreksi.Denda,
                Total = _viewModel.HitungKoreksi.Total,
                FlagMinimumPakai = _viewModel.HitungKoreksi.FlagMinimumPakai,
                Taksasi = _viewModel.KoreksiForm.Taksasi,
                Taksir = _viewModel.KoreksiForm.Taksir,
                WaktuKoreksi = DateTime.Now,
                FlagKoreksiBilling = true
            };

            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(param));
                }
            }

            var paramDetail = new RekeningAirDetailDto
            {
                Blok1 = _viewModel.HitungKoreksiDetail.Blok1,
                Blok2 = _viewModel.HitungKoreksiDetail.Blok2,
                Blok3 = _viewModel.HitungKoreksiDetail.Blok3,
                Blok4 = _viewModel.HitungKoreksiDetail.Blok4,
                Blok5 = _viewModel.HitungKoreksiDetail.Blok5,
                Prog1 = _viewModel.HitungKoreksiDetail.Prog1,
                Prog2 = _viewModel.HitungKoreksiDetail.Prog2,
                Prog3 = _viewModel.HitungKoreksiDetail.Prog3,
                Prog4 = _viewModel.HitungKoreksiDetail.Prog4,
                Prog5 = _viewModel.HitungKoreksiDetail.Prog5,
            };

            type = typeof(RekeningAirDetailDto);
            properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest" && property.Name != "IdPelangganAir" && property.Name != "IdPeriode")
                {
                    body.Add(property.Name, property.GetValue(paramDetail));
                }
            }

            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = ((AsyncCommandBase)_viewModel.OnSubmitKoreksiRekeningCommand).ExecuteAsync(body);
        }

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(StanSkrg.Text) ||
                string.IsNullOrEmpty(StanLalu.Text) ||
                string.IsNullOrEmpty(Pakai.Text) ||
                string.IsNullOrEmpty(StanAngkat.Text) ||
                (CekPetugasBaca.IsChecked == true && string.IsNullOrEmpty(PetugasBaca.Text)) ||
                (CekKelainan.IsChecked == true && string.IsNullOrEmpty(Kelainan.Text))
               )
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void InputValidation()
        {
            try
            {
                StanSkrg.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                StanSkrg.GotFocus += DecimalValidationHelper.Object_GotFocus;
                StanSkrg.LostFocus += DecimalValidationHelper.Object_LostFocus;
            }
            catch
            {
                StanSkrg.Text = "0";
            }

            try
            {
                StanLalu.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                StanLalu.GotFocus += DecimalValidationHelper.Object_GotFocus;
                StanLalu.LostFocus += DecimalValidationHelper.Object_LostFocus;
            }
            catch
            {
                StanLalu.Text = "0";
            }

            try
            {
                Pakai.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                Pakai.GotFocus += DecimalValidationHelper.Object_GotFocus;
                Pakai.LostFocus += DecimalValidationHelper.Object_LostFocus;
            }
            catch
            {
                Pakai.Text = "0";
            }

            try
            {
                StanAngkat.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                StanAngkat.GotFocus += DecimalValidationHelper.Object_GotFocus;
                StanAngkat.LostFocus += DecimalValidationHelper.Object_LostFocus;
            }
            catch
            {
                StanAngkat.Text = "0";
            }
        }

        private void HitungStan(int param)
        {
            _isProccess = true;
            var stanlalu = Convert.ToInt32(StanLalu.Text.Replace(".", ""));
            var stanskrg = Convert.ToInt32(StanSkrg.Text.Replace(".", ""));
            var stanAngkat = Convert.ToInt32(StanAngkat.Text.Replace(".", ""));
            var pakai = Convert.ToInt32(Pakai.Text.Replace(".", ""));

            if (_viewModel.KoreksiForm.Taksasi == false)
            {
                if (param == 1)
                {
                    if (stanAngkat > 0)
                        pakai = stanAngkat - stanlalu + stanskrg;
                    else
                        pakai = stanskrg - stanlalu;
                }
                else
                {
                    if (stanAngkat == 0)
                        stanskrg = stanlalu + pakai;
                    else
                        stanskrg = stanAngkat - stanlalu + pakai;
                }
            }

            if (CekStanLalu.IsChecked == false) StanLalu.Text = stanlalu.ToString();
            if (CekStanKini.IsChecked == false) StanSkrg.Text = stanskrg.ToString();
            if (CekStanAngkat.IsChecked == false) StanAngkat.Text = stanAngkat.ToString();
            if (CekPakai.IsChecked == false) Pakai.Text = pakai.ToString();

            _viewModel.KoreksiForm.Pakai = pakai;
            _viewModel.KoreksiForm.StanLalu = stanlalu;
            _viewModel.KoreksiForm.StanSkrg = stanskrg;
            _viewModel.KoreksiForm.StanAngkat = stanAngkat;

            _isProccess = false;

            _ = KalkulasiRekeningAsync();

            if (_viewModel.KoreksiForm.Pakai < 0)
            {
                ValidationPakai.Visibility = Visibility.Visible;
                OkButton.IsEnabled = false;
            }
            else
            {
                ValidationPakai.Visibility = Visibility.Collapsed;
                OkButton.IsEnabled = true;
            }
        }

        private void StanLalu_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isProccess && _viewModel.IsStanLaluChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                HitungStan(1);
            }
        }

        private void StanSkrg_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isProccess && _viewModel.IsStanKiniChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                HitungStan(1);
            }
        }

        private void StanAngkat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isProccess && _viewModel.IsStanAngkatChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                HitungStan(1);
            }
        }

        private void Pakai_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isProccess && _viewModel.IsPakaiChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                HitungStan(2);
            }
        }

        private async Task KalkulasiRekeningAsync()
        {
            await ((AsyncCommandBase)_viewModel.OnCalCulationKoreksiCommand).ExecuteAsync(null);

            if (_viewModel.KoreksiForm.Taksir == true)
            {
                Submit_Click(null, null);
            }
        }

        private void Taksir_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.KoreksiForm.Taksir = true;
            _viewModel.IsPakaiChecked = true;
            Pakai.Text = Convert.ToInt32((_viewModel.SelectedData.PakaiBulanLalu + _viewModel.SelectedData.Pakai2BulanLalu + _viewModel.SelectedData.Pakai3BulanLalu) / 3).ToString();
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            PerhitunganSection.Visibility = current == "PerhitunganBiaya" ? Visibility.Visible : Visibility.Collapsed;
            BuktiPembacaanSection.Visibility = current == "BuktiPembacaan" ? Visibility.Visible : Visibility.Collapsed;
            LokasiBacaSection.Visibility = current == "LokasiBaca" ? Visibility.Visible : Visibility.Collapsed;

            if (BuktiPembacaanSection.Visibility == Visibility.Visible)
            {
                BtnBulanIni.IsChecked = true;
            }

            if (LokasiBacaSection.Visibility == Visibility.Visible)
            {
                if (!_isMapInitiated)
                {
                    if (_viewModel.SelectedData != null && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.Latitude) && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.Longitude))
                    {
                        MapConfigHelper.Initiate(MainMapKini);
                        MapConfigHelper.OnLoaded(MainMapKini, false, 16);
                        MapConfigHelper.SetCenter(MainMapKini, _viewModel.SelectedData.Latitude, _viewModel.SelectedData.Longitude);

                        var markerList = new List<MapMarkerObject>()
                        {
                            new MapMarkerObject()
                            {
                                MarkerInformation = $"{_viewModel.SelectedData.Nama}",
                                Latitude = _viewModel.SelectedData.Latitude,
                                Longitude = _viewModel.SelectedData.Longitude
                            }
                        };

                        MapConfigHelper.SetMarker(MainMapKini, markerList);
                    }

                    if (_viewModel.SelectedData != null && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.LatitudeBulanLalu) && !string.IsNullOrWhiteSpace(_viewModel.SelectedData.LongitudeBulanLalu))
                    {
                        MapConfigHelper.Initiate(MainMapLalu);
                        MapConfigHelper.OnLoaded(MainMapLalu, false, 16);
                        MapConfigHelper.SetCenter(MainMapLalu, _viewModel.SelectedData.LatitudeBulanLalu, _viewModel.SelectedData.LongitudeBulanLalu);

                        var markerList = new List<MapMarkerObject>()
                        {
                            new MapMarkerObject()
                            {
                                MarkerInformation = $"{_viewModel.SelectedData.Nama}",
                                Latitude = _viewModel.SelectedData.LatitudeBulanLalu,
                                Longitude = _viewModel.SelectedData.LongitudeBulanLalu
                            }
                        };

                        MapConfigHelper.SetMarker(MainMapLalu, markerList);
                    }

                    _isMapInitiated = true;
                }
            }
        }

        private void FotoMeter(string bulan, string tahun)
        {
            var noFoto = new Uri("/SiPintar;component/Assets/Images/ic_no_pic.png", UriKind.RelativeOrAbsolute);
            var maximizeIconDisabledUri = new Uri("/SiPintar;component/Assets/Images/ic_expand_disabled.png", UriKind.RelativeOrAbsolute);
            var fotoDirectory = AppSetting.LokasiFotoMeter;
            var aksesFotoMeter = AppSetting.AksesFotoMeter;

            var Bulan = bulan;
            var Tahun = tahun;
            if (_viewModel.SelectedData != null)
            {
                var fotoStanUri = new Uri(Path.Combine(fotoDirectory, Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}.jpg"), UriKind.Absolute);
                if (aksesFotoMeter && fotoDirectory != null)
                {
                    LoadImage(fotoStanUri, FotoStan);
                    FotoStan.Width = 200;
                    BorderFotoStan.Height = FotoStan.Height;
                    InfoFotoStan.Visibility = Visibility.Collapsed;
                    MaxFotoStanButton.IsEnabled = true;
                    MaxFotoStanButton.Visibility = Visibility.Visible;
                    _viewModel.Bulan = Bulan;
                    _viewModel.Tahun = Tahun;
                }
                else
                {
                    EmptyFotoStan(noFoto, maximizeIconDisabledUri);
                }
            }
            else
                EmptyFotoStan(noFoto, maximizeIconDisabledUri);

            if (!IsSetEvent)
            {
                _viewModel.UpdateFotoThumbnailEvent += () =>
                {
                    if (_viewModel.SelectedData != null)
                        FotoMeter(_viewModel.SelectedDataPeriode.KodePeriode.ToString().Remove(0, 4), _viewModel.SelectedDataPeriode.Tahun.ToString().Remove(0, 2));
                };
                IsSetEvent = true;
            }
        }

        private void FotoRumah(string bulan, string tahun)
        {
            var noFoto = new Uri("/SiPintar;component/Assets/Images/ic_no_pic.png", UriKind.RelativeOrAbsolute);
            var maximizeIconDisabledUri = new Uri("/SiPintar;component/Assets/Images/ic_expand_disabled.png", UriKind.RelativeOrAbsolute);
            var fotoDirectory = AppSetting.LokasiFotoMeter;
            var aksesFotoMeter = AppSetting.AksesFotoMeter;

            var Bulan = bulan;
            var Tahun = tahun;
            if (_viewModel.SelectedData != null)
            {
                var fotoRumahUri = new Uri(Path.Combine(fotoDirectory, Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}R.jpg"), UriKind.Absolute);
                var maximizeIconUri = new Uri("/SiPintar;component/Assets/Images/ic_expand_disabled.png", UriKind.RelativeOrAbsolute);
                if (aksesFotoMeter && fotoDirectory != null)
                {
                    LoadImage(fotoRumahUri, FotoStan);
                    FotoStan.Width = 200;
                    BorderFotoStan.Height = FotoStan.Height;
                    InfoFotoStan.Visibility = Visibility.Collapsed;
                    MaxFotoStanButton.IsEnabled = true;
                    MaxFotoStanButton.Visibility = Visibility.Visible;
                    _viewModel.Bulan = Bulan;
                    _viewModel.Tahun = Tahun;
                }
                else
                {
                    EmptyFotoStan(noFoto, maximizeIconDisabledUri);
                }
            }
            else
                EmptyFotoStan(noFoto, maximizeIconDisabledUri);

            if (!IsSetEvent)
            {
                _viewModel.UpdateFotoThumbnailEvent += () =>
                {
                    if (_viewModel.SelectedData != null)
                        FotoRumah(_viewModel.SelectedDataPeriode.KodePeriode.ToString().Remove(0, 4), _viewModel.SelectedDataPeriode.Tahun.ToString().Remove(0, 2));
                };
                IsSetEvent = true;
            }
        }

        private void LoadImage(Uri fotoLoc, Image image)
        {
            try
            {
                image.Source = null;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(fotoLoc.OriginalString, UriKind.Absolute);
                bitmapImage.EndInit();
                image.Stretch = Stretch.Fill;
                image.Source = bitmapImage;
            }
            catch (Exception e)
            {
                DialogHelper.ShowSnackbar(false, e.Message, "danger");
            }
        }

        private void EmptyFotoStan(Uri noFoto, Uri maximizeIconDisabledUri)
        {
            if (!AppSetting.AksesFotoMeter)
                InfoFotoStan.Text = "Akses Foto Meter di Nonaktifkan";

            FotoStan.Source = new BitmapImage(noFoto);
            FotoStan.Width = 32;
            InfoFotoStan.Visibility = Visibility.Visible;
            BorderFotoStan.Height = 158;
            MaxFotoStanButton.IsEnabled = false;
            MaxFotoStanButton.Visibility = Visibility.Collapsed;
        }

        private void BuktiBacaMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;

            switch (current)
            {
                case "BtnBulanIni":
                    FotoBulanIni();
                    break;
                case "BtnBulanLalu":
                    FotoBulanLalu();
                    break;
                case "BtnFotoRumah":
                    FotoRumah();
                    break;
                case "BtnVideo":
                    Video();
                    break;
            }
        }

        private void FotoBulanIni()
        {
            if (_viewModel?.SelectedDataPeriode.KodePeriode != null)
            {
                FotoMeter(_viewModel.SelectedDataPeriode.KodePeriode.ToString().Remove(0, 4), _viewModel.SelectedDataPeriode.Tahun.ToString().Remove(0, 2));

                var kodePeriode = _viewModel.SelectedDataPeriode.KodePeriode + "01";
                InfoPeriode.Text = DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("MMMM yyyy", new CultureInfo("id-ID"));
                InfoWaktuBaca.Text = _viewModel.SelectedData.WaktuBaca != null ? Convert.ToDateTime(_viewModel.SelectedData.WaktuBaca, new CultureInfo("id-ID")).ToString("dd MMMM yyyy hh:mm") : "-";
                InfoKelainan.Text = _viewModel.SelectedData.KelainanWpf;
                InfoPetugasBaca.Text = _viewModel.SelectedData.PetugasBacaWpf;

                InfoBorder.Visibility = Visibility.Visible;
                PeriodePanel.Visibility = Visibility.Visible;
                WaktuBacaPanel.Visibility = Visibility.Visible;
                KelainanPanel.Visibility = Visibility.Visible;
                PetugasBacaPanel.Visibility = Visibility.Visible;

                _jenisFoto = "FotoBulanIni";
            }
        }

        private void FotoBulanLalu()
        {
            if (_viewModel?.SelectedDataPeriode.KodePeriode == null) return;
            var kodePeriode = _viewModel.SelectedDataPeriode.KodePeriode + "01"; // asumsi tanggal 01
            var lastmonth = DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(-1);
            var lastYear = (lastmonth.Month.ToString() == "12") ? DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture).AddYears(-1) : DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture);
            FotoMeter(lastmonth.ToString("MM"), lastYear.ToString("yy"));

            InfoPeriode.Text = DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(-1).ToString("MMMM yyyy", new CultureInfo("id-ID"));
            InfoKelainan.Text = _viewModel.SelectedData.KelainanBulanLalu;

            InfoBorder.Visibility = Visibility.Visible;
            PeriodePanel.Visibility = Visibility.Visible;
            WaktuBacaPanel.Visibility = Visibility.Collapsed;
            KelainanPanel.Visibility = Visibility.Visible;
            PetugasBacaPanel.Visibility = Visibility.Collapsed;

            _jenisFoto = "FotoBulanLalu";
        }

        private void FotoRumah()
        {
            if (_viewModel?.SelectedDataPeriode.KodePeriode != null)
            {
                FotoRumah(_viewModel.SelectedDataPeriode.KodePeriode.ToString().Remove(0, 4), _viewModel.SelectedDataPeriode.Tahun.ToString().Remove(0, 2));

                var kodePeriode = _viewModel.SelectedDataPeriode.KodePeriode + "01";
                InfoPeriode.Text = DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("MMMM yyyy", new CultureInfo("id-ID"));
                InfoWaktuBaca.Text = _viewModel.SelectedData.WaktuBaca != null ? Convert.ToDateTime(_viewModel.SelectedData.WaktuBaca, new CultureInfo("id-ID")).ToString("dd MMMM yyyy hh:mm") : "-";
                InfoKelainan.Text = _viewModel.SelectedData.KelainanWpf;
                InfoPetugasBaca.Text = _viewModel.SelectedData.PetugasBacaWpf;

                InfoBorder.Visibility = Visibility.Visible;
                PeriodePanel.Visibility = Visibility.Visible;
                WaktuBacaPanel.Visibility = Visibility.Visible;
                KelainanPanel.Visibility = Visibility.Visible;
                PetugasBacaPanel.Visibility = Visibility.Visible;

                _jenisFoto = "FotoRumah";
            }
        }

        private void Video()
        {
            if (_viewModel?.SelectedDataPeriode.KodePeriode != null)
            {
                FotoMeter(_viewModel.SelectedDataPeriode.KodePeriode.ToString().Remove(0, 4), _viewModel.SelectedDataPeriode.Tahun.ToString().Remove(0, 2));

                var kodePeriode = _viewModel.SelectedDataPeriode.KodePeriode + "01";
                InfoPeriode.Text = DateTime.ParseExact(kodePeriode, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("MMMM yyyy", new CultureInfo("id-ID"));
                InfoWaktuBaca.Text = _viewModel.SelectedData.WaktuBaca != null ? Convert.ToDateTime(_viewModel.SelectedData.WaktuBaca, new CultureInfo("id-ID")).ToString("dd MMMM yyyy hh:mm") : "-";
                InfoKelainan.Text = _viewModel.SelectedData.KelainanWpf;
                InfoPetugasBaca.Text = _viewModel.SelectedData.PetugasBacaWpf;

                InfoBorder.Visibility = Visibility.Visible;
                PeriodePanel.Visibility = Visibility.Visible;
                WaktuBacaPanel.Visibility = Visibility.Visible;
                KelainanPanel.Visibility = Visibility.Visible;
                PetugasBacaPanel.Visibility = Visibility.Visible;

                _jenisFoto = "Video";
            }
        }

        #region GMAP CONTROL

        private void ZoomInButtonLalu_Click(object sender, RoutedEventArgs e)
        {
            if (MainMapLalu.Zoom < MainMapLalu.MaxZoom)
                MainMapLalu.Zoom++;
        }

        private void ZoomOutButtonLalu_Click(object sender, RoutedEventArgs e)
        {
            if (MainMapLalu.Zoom > MainMapLalu.MinZoom)
                MainMapLalu.Zoom--;
        }

        private void OpenMapLalu_Click(object sender, RoutedEventArgs e)
        {
            _ = DialogHost.Show(new MapView(_viewModel, RefreshMapLalu), "InnerBillingRootDialog");
        }

        private void MainMapLalu_OnMapDrag()
        {
        }

        private void ZoomInButtonKini_Click(object sender, RoutedEventArgs e)
        {
            if (MainMapKini.Zoom < MainMapKini.MaxZoom)
                MainMapKini.Zoom++;
        }

        private void ZoomOutButtonKini_Click(object sender, RoutedEventArgs e)
        {
            if (MainMapKini.Zoom > MainMapKini.MinZoom)
                MainMapKini.Zoom--;
        }

        private void OpenMapKini_Click(object sender, RoutedEventArgs e)
        {
            _ = DialogHost.Show(new MapView(_viewModel, RefreshMapKini), "InnerBillingRootDialog");
        }

        private void MainMapKini_OnMapDrag()
        {
        }

        private bool RefreshMapKini()
        {
            MapConfigHelper.SetCenter(MainMapKini, _viewModel.SelectedData.Latitude, _viewModel.SelectedData.Longitude);
            return true;
        }

        private bool RefreshMapLalu()
        {
            MapConfigHelper.SetCenter(MainMapLalu, _viewModel.SelectedData.LatitudeBulanLalu, _viewModel.SelectedData.LongitudeBulanLalu);
            return true;
        }

        #endregion

        private void OpenFotoMeter_Click(object sender, RoutedEventArgs e)
        {
            var bulan = "";
            var tahun = "";
            var isFotoRumah = false;

            switch (_jenisFoto)
            {
                case "FotoBulanIni":
                    bulan = _viewModel.SelectedDataPeriode.KodePeriode.ToString().Right(2);
                    tahun = _viewModel.SelectedDataPeriode.Tahun.ToString().Mid(2, 2);
                    break;
                case "FotoBulanLalu":
                    var kodePeriode = DateTime.ParseExact(_viewModel.SelectedDataPeriode.KodePeriode.ToString() + "01", "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(-1).ToString("yyyyMM");
                    bulan = kodePeriode.Right(2);
                    tahun = kodePeriode.Mid(2, 2);
                    break;
                case "FotoRumah":
                    isFotoRumah = true;
                    bulan = _viewModel.SelectedDataPeriode.KodePeriode.ToString().Right(2);
                    tahun = _viewModel.SelectedDataPeriode.Tahun.ToString().Mid(2, 2);
                    break;
                case "Video":
                    bulan = _viewModel.SelectedDataPeriode.KodePeriode.ToString().Right(2);
                    tahun = _viewModel.SelectedDataPeriode.Tahun.ToString().Mid(2, 2);
                    break;
            }


            _ = DialogHost.Show(new FotoMeterView(bulan, tahun, _viewModel.SelectedData.NoSamb, isFotoRumah), "InnerBillingRootDialog");
        }
    }
}
