using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class DialogBaFormView : UserControl
    {
        private readonly PermohonanViewModel _viewModel;

        private bool _isMapInitiated;
        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;
        private readonly IRestApiClientModel _restApi;
        private readonly string _urlFotoPermohonan;
        private PermohonanWpf _tempSelected;
        private bool _isComplete = true;
        private bool _generateNosamb;

        private int _totalForm;
        private UIElement _formElement;
        private readonly List<int> SelectedPegawai = new List<int>();
        private readonly List<StackPanel> PetugasComponentList = new List<StackPanel>();

        public DialogBaFormView(object dataContext, IRestApiClientModel restApi)
        {
            InitializeComponent();
            _viewModel = dataContext as PermohonanViewModel;
            DataContext = _viewModel;

            _restApi = restApi;

            if (_viewModel.IsFor == "add")
                TitleForm.Text = $"Tambah {_viewModel.FiturName} {_viewModel.SelectedTipePermohonan.NamaTipePermohonan} {_viewModel.SelectedJenisPelanggan}";
            else if (_viewModel.IsFor == "edit")
                TitleForm.Text = $"Koreksi {_viewModel.FiturName} {_viewModel.SelectedTipePermohonan.NamaTipePermohonan} {_viewModel.SelectedJenisPelanggan}";
            else
                TitleForm.Text = $"Detail {_viewModel.FiturName} {_viewModel.SelectedTipePermohonan.NamaTipePermohonan} {_viewModel.SelectedJenisPelanggan}";

            GroupFoto.IsChecked = true;

            NomorBa.Visibility = Visibility.Visible;
            TanggalBa.Visibility = _viewModel.IsBeritaAcara ? Visibility.Visible : Visibility.Collapsed;
            CanvasEstimasiBiaya.Visibility = _viewModel.IsBeritaAcara ? Visibility.Collapsed : Visibility.Visible;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            NamaPelanggan.KeyUp += Cari_KeyUp;
            NomorSambungan.KeyUp += Cari_KeyUp;
            Alamat.KeyUp += Cari_KeyUp;

            ColumnNoSamb.Visibility = Visibility.Collapsed;
            ColumnNomorLimbah.Visibility = Visibility.Collapsed;
            ColumnNomorLltt.Visibility = Visibility.Collapsed;
            ColumnGolongan.Visibility = Visibility.Collapsed;
            ColumnTarifLimbah.Visibility = Visibility.Collapsed;
            ColumnTarifLltt.Visibility = Visibility.Collapsed;
            _isComplete = true;
            _generateNosamb = false;

            if (_viewModel.IsPelangganAir)
            {
                _urlFotoPermohonan = "permohonan-pelanggan-air-link-foto";
                ColumnNoSamb.Visibility = Visibility.Visible;
                ColumnGolongan.Visibility = Visibility.Visible;
                InfoPelangan.Visibility = Visibility.Visible;
                InfoNonPelangan.Visibility = Visibility.Collapsed;
            }

            if (_viewModel.IsPelangganLimbah)
            {
                _urlFotoPermohonan = "permohonan-pelanggan-limbah-link-foto";
                ColumnNomorLimbah.Visibility = Visibility.Visible;
                ColumnTarifLimbah.Visibility = Visibility.Visible;
                InfoPelangan.Visibility = Visibility.Visible;
                InfoNonPelangan.Visibility = Visibility.Collapsed;
            }

            if (_viewModel.IsPelangganLltt)
            {
                _urlFotoPermohonan = "permohonan-pelanggan-lltt-link-foto";
                ColumnNomorLltt.Visibility = Visibility.Visible;
                ColumnTarifLltt.Visibility = Visibility.Visible;
                InfoPelangan.Visibility = Visibility.Visible;
                InfoNonPelangan.Visibility = Visibility.Collapsed;
            }

            if (_viewModel.IsNonPelanggan)
            {
                _urlFotoPermohonan = "permohonan-non-pelanggan-link-foto";
                InfoPelangan.Visibility = Visibility.Collapsed;
                InfoNonPelangan.Visibility = Visibility.Visible;
            }

            if (_viewModel.SelectedTipePermohonan.KodeTipePermohonan.Contains("SAMBUNGAN_BARU_AIR"))
            {
                GroupDataSambunganBaru.Visibility = Visibility.Visible;
            }
            else
            {
                GroupDataSambunganBaru.Visibility = Visibility.Collapsed;
            }

            if ((_viewModel.SelectedTipePermohonan.KodeTipePermohonan.Contains("SAMBUNG_KEMBALI") || _viewModel.SelectedTipePermohonan.KodeTipePermohonan.Contains("TUTUP_TOTAL")) && _viewModel.IsPelangganAir == true)
            {
                GroupDataMeteran.Visibility = Visibility.Visible;
            }
            else
            {
                GroupDataMeteran.Visibility = Visibility.Collapsed;
            }

            SetupFirst();


            TanggalSection.Visibility = Visibility.Collapsed;

            if (_viewModel.IsFor == "edit" || _viewModel.IsFor == "detail")
            {
                HeaderAddForm.Visibility = Visibility.Collapsed;
                HeaderEditForm.Visibility = Visibility.Visible;
                BtnSubmit.Content = "Simpan";
                BtnSubmit.Width = 100;
                LoadData();
            }
            else if (_viewModel.IsFor == "add")
            {
                PanelPaketRabPersil.Visibility = Visibility.Visible;
                PanelPaketRabPersilText.Visibility = Visibility.Collapsed;
                PanelPaketRabDistribusi.Visibility = Visibility.Visible;
                PanelPaketRabDistribusiText.Visibility = Visibility.Collapsed;

                HeaderAddForm.Visibility = Visibility.Visible;
                HeaderEditForm.Visibility = Visibility.Collapsed;
                BtnSubmit.Content = "Simpan";
                BtnSubmit.Width = 100;

                _viewModel.FormData = new PermohonanWpf();
                FormCariPelanggan.Visibility = Visibility.Visible;
            }

            if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
            {
                AlasanContainer.Visibility = Visibility.Collapsed;
                CanvasBiayaNormal.Visibility = Visibility.Collapsed;
                CanvasBiayaAirTangki.Visibility = Visibility.Visible;
                AirTangkiBiayaAir.Text = "0";
                AirTangkiTransport.Text = "0";
                AirTangkiAdministrasi.Text = "0";
                AirTangkiOperasional.Text = "0";
                AirTangkiBiayatotal.Text = "0";
                AirTangkiBiayaAir.Text = "0";
                AirTangkiPpn.Text = "0";
                Keterangan.Text = "-";
            }

            Keterangan.PreviewKeyUp += Keterangan_PreviewKeyUp;
        }

        private void SetupFirst()
        {
            var idx = 0;
            foreach (var item in _viewModel.SelectedTipePermohonan.DetailParameter.Where(c => AppExtensionsHelpers.Right(c.Parameter, 4).ToLower() != "lama"))
            {
                StackPanel temp = null;
                if (item.IdListData is null)
                {
                    if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() != "air tangki")
                    {
                        temp = SetupDataNonList(item, idx);
                    }
                    else
                    {
                        if (item.Parameter.ToLower().Trim() == "estimasi waktu pengiriman" || item.Parameter.ToLower().Trim() == "waktu penagihan")
                        {
                            temp = SetupDataList(item, idx);
                        }
                        else
                        {
                            temp = SetupDataNonList(item, idx);
                        }
                    }
                }
                else
                {
                    temp = SetupDataList(item, idx);
                }

                if (temp != null)
                {
                    Canvas.Children.Insert(idx, temp);
                    idx++;
                }
            }

            if (_viewModel.IsBeritaAcara == false)
            {
                decimal biaya = 0;
                decimal ppn = 0;
                decimal total = 0;
                foreach (var item in _viewModel.SelectedTipePermohonan.DetailBiaya)
                {
                    var temp = Resources["TextBoxDecimalTemplate"] as StackPanel;
                    var childLabel = temp.Children[0] as TextBlock;
                    childLabel.Text = item.Parameter;
                    var childGrid = temp.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as TextBox;
                    childInput.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                    childInput.GotFocus += DecimalValidationHelper.Object_GotFocus;
                    childInput.LostFocus += DecimalValidationHelper.Object_LostFocus;
                    childInput.TextChanged += ChildInput_BiayaChanged;
                    childInput.Text = DecimalValidationHelper.FormatDecimalToStringId(item.Biaya ?? decimal.Zero);
                    childInput.IsEnabled = !(item.IsLocked ?? false);
                    if (!item.IsLocked ?? true)
                    {
                        childInput.PreviewKeyUp += ChildInput_PreviewKeyUp;
                    }

                    Canvas.Children.Insert(idx, temp);
                    idx++;
                    biaya += item.Biaya ?? decimal.Zero;
                }

                if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
                {
                    total = decimal.Zero;
                }
                else
                {
                    ppn = _viewModel.SelectedTipePermohonan.PersentasePpn / 100 * biaya;
                    total = biaya + ppn;
                }

                Biaya.Text = biaya.ToString("#,##0.##", CultureInfo.GetCultureInfo("id-ID"));
                Ppn.Text = ppn.ToString("#,##0.##", CultureInfo.GetCultureInfo("id-ID"));
                Total.Text = total.ToString("#,##0.##", CultureInfo.GetCultureInfo("id-ID"));
            }
        }

        private void Keterangan_PreviewKeyUp(object sender, KeyEventArgs e)
        {

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
            CariPelangganButton.IsEnabled = true;
        }

        private void GenerateNoSamb_Click(object sender, RoutedEventArgs e)
        {
            _ = GenerateNoSambAsync();
        }

        private async Task GenerateNoSambAsync()
        {
            try
            {
                if (_viewModel.RayonForm == null)
                    return;

                var param = new Dictionary<string, dynamic>
                {
                    {"IdRayon", _viewModel.RayonForm.IdRayon},
                };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-pelanggan-air-generate-nosamb", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<dynamic>>();

                        if (data != null)
                        {
                            _viewModel.FormData.NosambYangDiberikanWpf = data[0].NoSamb;
                            GridButtonGenerateNosamb.Visibility = Visibility.Collapsed;
                            LabelNosambYangDiberikan.Visibility = Visibility.Visible;
                            _generateNosamb = true;
                            DialogHelper.ShowSnackbar(false, "success", "Generate Nomor Sukses");

                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(false, "danger", response.Data.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(false, "danger", response.Error.Message);
            }
            catch (Exception ex)
            {
                DialogHelper.ShowSnackbar(false, "danger", ex.Message);
            }
        }

        private void CariPelangganButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DataPermohonan.Clear();

            var param = new Dictionary<string, dynamic>
            {
                {"Nama", NamaPelanggan.Text},
                {"Alamat", Alamat.Text},
                {"pageSize", _viewModel.LimitDataPelanggan.Key},
                {"currentPage", _viewModel.CurrentPagePelanggan},
                {"idTipePermohonan", _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan},
                {"includeRabDetail", true},
                {"statusPermohonanText", "Menunggu Berita Acara"},
            };

            if (_viewModel.IsPelangganAir)
            {
                param.Add("NoSamb", NomorSambungan.Text);
            }

            if (_viewModel.IsPelangganLimbah)
            {
                param.Add("NomorLimbah", NomorSambungan.Text);
            }

            if (_viewModel.IsPelangganLltt)
            {
                param.Add("NomorLltt", NomorSambungan.Text);
            }

            _ = ((AsyncCommandBase)_viewModel.OnCariPermohonanCommand).ExecuteAsync(param);
        }

        private void DataGridPelanggan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _tempSelected = _viewModel.SelectedDataPermohonan;
            _tempSelected.BeritaAcara = new PermohonanBaDto();

            LoadData(true);
        }

        private void LoadData(bool? tipe = false)
        {
            if (tipe == true)
            {
                _viewModel.FormData = _tempSelected;
            }
            else
            {
                _viewModel.FormData = _viewModel.SelectedData;

                _viewModel.MerekMeterForm = _viewModel.MerekMeterList.FirstOrDefault(c => c.NamaMerekMeter == _viewModel.FormData.BeritaAcara.MerekMeter);
            }

            _viewModel.IsCariPelangganForm = false;

            if (!string.IsNullOrWhiteSpace(_viewModel.FormData.NosambYangDiberikan))
            {
                GridButtonGenerateNosamb.Visibility = Visibility.Collapsed;
                LabelNosambYangDiberikan.Visibility = Visibility.Visible;
            }
            else
            {
                GridButtonGenerateNosamb.Visibility = Visibility.Visible;
                LabelNosambYangDiberikan.Visibility = Visibility.Collapsed;
            }

            FormInputData.Visibility = Visibility.Visible;

            BtnSubmit.Visibility = Visibility.Visible;

            if (_viewModel.IsFor == "detail")
            {
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

            #region jika edit atau detail biar ambil map , foto dari permohonan, dan RAB existsing

            if (_viewModel.IsFor != "add" && _viewModel.FormData != null)
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.LatitudeWpf))
                {
                    _viewModel.FormData.Latitude = _viewModel.FormData.LatitudeWpf;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.LongitudeWpf))
                {
                    _viewModel.FormData.Longitude = _viewModel.FormData.LongitudeWpf;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.AlamatMapWpf))
                {
                    _viewModel.FormData.AlamatMap = _viewModel.FormData.AlamatMapWpf;
                }

                #region load foto permohonan
                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.FotoBukti1))
                {
                    _viewModel.FormData.FotoBukti1 = _viewModel.FormData.FotoBukti1;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.FotoBukti2))
                {
                    _viewModel.FormData.FotoBukti2 = _viewModel.FormData.FotoBukti2;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.FormData.FotoBukti3))
                {
                    _viewModel.FormData.FotoBukti3 = _viewModel.FormData.FotoBukti3;
                }
                #endregion

                #region load foto spk
                if (_viewModel.FormData.SPK != null)
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.SPK.FotoBukti1))
                    {
                        _viewModel.FormData.FotoBuktiSpk1 = _viewModel.FormData.SPK.FotoBukti1;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.SPK.FotoBukti2))
                    {
                        _viewModel.FormData.FotoBuktiSpk2 = _viewModel.FormData.SPK.FotoBukti2;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.SPK.FotoBukti3))
                    {
                        _viewModel.FormData.FotoBuktiSpk3 = _viewModel.FormData.SPK.FotoBukti3;
                    }
                }
                #endregion

                #region load foto Rab
                if (_viewModel.FormData.RAB != null)
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.RAB.FotoDenah1))
                    {
                        _viewModel.FormData.FotoDenah1 = _viewModel.FormData.RAB.FotoDenah1;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.RAB.FotoDenah2))
                    {
                        _viewModel.FormData.FotoDenah2 = _viewModel.FormData.RAB.FotoDenah2;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.RAB.FotoBukti1))
                    {
                        _viewModel.FormData.FotoBuktiRab1 = _viewModel.FormData.RAB.FotoBukti1;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.RAB.FotoBukti2))
                    {
                        _viewModel.FormData.FotoBuktiRab2 = _viewModel.FormData.RAB.FotoBukti2;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.RAB.FotoBukti3))
                    {
                        _viewModel.FormData.FotoBuktiRab3 = _viewModel.FormData.RAB.FotoBukti3;
                    }
                }
                #endregion

                #region load foto berita acara
                if (_viewModel.FormData.BeritaAcara != null)
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.BeritaAcara.FotoBukti1))
                    {
                        _viewModel.FormData.FotoBuktiBeritaAcara1 = _viewModel.FormData.BeritaAcara.FotoBukti1;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.BeritaAcara.FotoBukti2))
                    {
                        _viewModel.FormData.FotoBuktiBeritaAcara2 = _viewModel.FormData.BeritaAcara.FotoBukti2;
                    }

                    if (!string.IsNullOrWhiteSpace(_viewModel.FormData.BeritaAcara.FotoBukti3))
                    {
                        _viewModel.FormData.FotoBuktiBeritaAcara3 = _viewModel.FormData.BeritaAcara.FotoBukti3;
                    }
                }
                #endregion

            }

            #region load RAB
            if (_viewModel.IsFor == "add")
            {
                GetDetailRabExist();
            }
            else
            {
                GetDetailRkpExist();
            }
            #endregion

            #endregion

            #region load data sambungan baru
            if (GroupDataSambunganBaru.Visibility == Visibility.Visible)
            {
                _viewModel.TipePendaftaranForm = _viewModel.TipePendaftaranList.FirstOrDefault(c => c.IdTipePendaftaranSambungan == _viewModel.FormData.IdTipePendaftaranSambungan);
                _viewModel.PekerjaanForm = _viewModel.PekerjaanList.FirstOrDefault(c => c.IdPekerjaan == _viewModel.FormData.IdPekerjaan);
                _viewModel.RayonForm = _viewModel.RayonList.FirstOrDefault(c => c.IdRayon == _viewModel.FormData.IdRayon);
                _viewModel.BlokForm = _viewModel.BlokList.FirstOrDefault(c => c.IdBlok == _viewModel.FormData.IdBlok);
                _viewModel.KelurahanForm = _viewModel.KelurahanList.FirstOrDefault(c => c.IdKelurahan == _viewModel.FormData.IdKelurahan);
                _viewModel.JenisBangunanForm = _viewModel.JenisBangunanList.FirstOrDefault(c => c.IdJenisBangunan == _viewModel.FormData.IdJenisBangunan);
                _viewModel.KepemilikanBangunanForm = _viewModel.KepemilikanBangunanList.FirstOrDefault(c => c.IdKepemilikan == _viewModel.FormData.IdKepemilikan);
                _viewModel.PeruntukanForm = _viewModel.PeruntukanList.FirstOrDefault(c => c.IdPeruntukan == _viewModel.FormData.IdPeruntukan);
                _viewModel.SumberAirForm = _viewModel.SumberAirList.FirstOrDefault(c => c.IdSumberAir == _viewModel.FormData.IdSumberAir);
                _viewModel.GolonganForm = _viewModel.GolonganList.FirstOrDefault(c => c.IdGolongan == _viewModel.FormData.IdGolongan);
                _viewModel.DiameterForm = _viewModel.DiameterList.FirstOrDefault(c => c.IdDiameter == _viewModel.FormData.IdDiameter);
                _viewModel.AdministrasiLainForm = _viewModel.AdministrasiLainList.FirstOrDefault(c => c.IdAdministrasiLain == _viewModel.FormData.IdAdministrasiLain);
                _viewModel.PemeliharaanLainForm = _viewModel.PemeliharaanLainList.FirstOrDefault(c => c.IdPemeliharaanLain == _viewModel.FormData.IdPemeliharaanLain);
                _viewModel.RetribusiLainForm = _viewModel.RetribusiLainList.FirstOrDefault(c => c.IdRetribusiLain == _viewModel.FormData.IdRetribusiLain);
                _viewModel.GolonganForm = _viewModel.GolonganList.FirstOrDefault(c => c.IdGolongan == _viewModel.FormData.IdGolongan);
                _viewModel.DiameterForm = _viewModel.DiameterList.FirstOrDefault(c => c.IdDiameter == _viewModel.FormData.IdDiameter);
                _viewModel.AdministrasiLainForm = _viewModel.AdministrasiLainList.FirstOrDefault(c => c.IdAdministrasiLain == _viewModel.FormData.IdAdministrasiLain);
                _viewModel.PemeliharaanLainForm = _viewModel.PemeliharaanLainList.FirstOrDefault(c => c.IdPemeliharaanLain == _viewModel.FormData.IdPemeliharaanLain);
                _viewModel.RetribusiLainForm = _viewModel.RetribusiLainList.FirstOrDefault(c => c.IdRetribusiLain == _viewModel.FormData.IdRetribusiLain);
                _viewModel.DmaForm = _viewModel.DmaList.FirstOrDefault(c => c.IdDma == _viewModel.FormData.IdDma);
                _viewModel.DmzForm = _viewModel.DmzList.FirstOrDefault(c => c.IdDmz == _viewModel.FormData.IdDmz);
                _viewModel.MerekMeterForm = _viewModel.MerekMeterList.FirstOrDefault(c => c.IdMerekMeter == _viewModel.FormData.IdMerekMeter);
            }
            #endregion

            InitializeFotoDanMap();
            SetupEdit();
            SetupPegawai();

            if (_viewModel.IsFor == "edit")
            {
                PanelPaketRabPersil.Visibility = Visibility.Visible;
                PanelPaketRabPersilText.Visibility = Visibility.Collapsed;
                PanelPaketRabDistribusi.Visibility = Visibility.Visible;
                PanelPaketRabDistribusiText.Visibility = Visibility.Collapsed;
                BtnLoadRabPipaPersil.Visibility = Visibility.Visible;
                BtnLoadRabPipaDistribusi.Visibility = Visibility.Visible;
            }
            else if (_viewModel.IsFor == "detail")
            {
                PanelPaketRabPersil.Visibility = Visibility.Collapsed;
                PanelPaketRabPersilText.Visibility = Visibility.Visible;
                PanelPaketRabDistribusi.Visibility = Visibility.Collapsed;
                PanelPaketRabDistribusiText.Visibility = Visibility.Visible;

                BtnOngkosPipaPersil.Visibility = Visibility.Collapsed;
                BtnMaterialPipaPersil.Visibility = Visibility.Collapsed;
                BtnOngkosPipaDistribusi.Visibility = Visibility.Collapsed;
                BtnMaterialPipaDistribusi.Visibility = Visibility.Collapsed;

                BtnLoadRabPipaPersil.Visibility = Visibility.Collapsed;
                BtnLoadRabPipaDistribusi.Visibility = Visibility.Collapsed;

                if (!_viewModel.DetailRabPipaPersil.Any())
                {
                    PanelCheckBoxPipaPersil.Visibility = Visibility.Collapsed;
                    DataGridDetailRabPersil.Visibility = Visibility.Collapsed;
                }

                if (!_viewModel.DetailRabPipaDistribusi.Any())
                {
                    PanelCheckBoxPipaDistribusi.Visibility = Visibility.Collapsed;
                    DataGridDetailRabDistribusi.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void InitializeFotoDanMap()
        {
            _isMapInitiated = false;

            var viewModel = (PermohonanViewModel)DataContext;

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

            viewModel.IsFotoBukti1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBukti1);
            viewModel.IsFotoBukti2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBukti2);
            viewModel.IsFotoBukti3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBukti3);

            viewModel.IsFotoBuktiSpk1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiSpk1);
            viewModel.IsFotoBuktiSpk2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiSpk2);
            viewModel.IsFotoBuktiSpk3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiSpk3);

            viewModel.IsFotoDenah1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoDenah1);
            viewModel.IsFotoDenah2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoDenah2);
            viewModel.IsFotoBuktiRab1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiRab1);
            viewModel.IsFotoBuktiRab2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiRab2);
            viewModel.IsFotoBuktiRab3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiRab3);

            viewModel.IsFotoBuktiBeritaAcara1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiBeritaAcara1);
            viewModel.IsFotoBuktiBeritaAcara2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiBeritaAcara2);
            viewModel.IsFotoBuktiBeritaAcara3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiBeritaAcara3);

            viewModel.IsDenahLokasiFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) && !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude);

            _ = GetFotoPermohonanAsync();
        }

        private async Task GetFotoPermohonanAsync()
        {
            try
            {
                if (_viewModel.FormData.IdPermohonan == null)
                    return;

                var param = new Dictionary<string, dynamic> { { "IdPermohonan", _viewModel.FormData.IdPermohonan } };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_urlFotoPermohonan}", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<NamaFotoDto>>();

                        if (data != null)
                        {
                            _viewModel.FotoBukti1Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti1));
                            _viewModel.FotoBukti2Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti2));
                            _viewModel.FotoBukti3Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti3));
                        }
                    }
                    else
                        DialogHelper.ShowSnackbar(false, "danger", response.Data.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(false, "danger", response.Error.Message);
            }
            catch (Exception ex)
            {
                DialogHelper.ShowSnackbar(false, "danger", ex.Message);
            }
        }

        private StackPanel SetupDataNonList(MasterTipePermohonanDetailDto item, int idx)
        {
            StackPanel temp;
            if (item.TipeData == "bool")
            {
                temp = Resources["ComboBoxTemplate"] as StackPanel;
                var childLabel = temp.Children[0] as TextBlock;
                childLabel.Text = item.Parameter;
                var childGrid = temp.Children[1] as Grid;
                var childInput = childGrid.Children[0] as ComboBox;
                var dataDetail = _viewModel.DataParamPermohonan.Where(x => x.Name == item.Parameter).FirstOrDefault();
                childInput.ItemsSource = new List<dynamic>() { new { Key = true, Value = "Ya" }, new { Key = false, Value = "Tidak" } };
                childInput.DisplayMemberPath = "Value";
                childInput.SelectionChanged += ChildInput_SelectionChanged;
            }
            else if (item.TipeData == "date")
            {
                temp = Resources["DatePickerTemplate"] as StackPanel;
                var childLabel = temp.Children[0] as TextBlock;
                childLabel.Text = item.Parameter;
                var childBorder = temp.Children[1] as Border;
                var childGrid = childBorder.Child as Grid;
                var childDatePicker = childGrid.Children[0] as DatePicker;
                childDatePicker.SelectedDateChanged += ChildDatePicker_SelectedDateChanged;
            }
            else if (item.TipeData == "int")
            {
                temp = Resources["TextBoxNumberTemplate"] as StackPanel;
                var childLabel = temp.Children[0] as TextBlock;
                childLabel.Text = item.Parameter;
                var childGrid = temp.Children[1] as Grid;
                var childInput = childGrid.Children[0] as TextBox;
                childInput.PreviewTextInput += IntegerValidationHelper.IntegerValidationTextBox;
                childInput.GotFocus += IntegerValidationHelper.Object_GotFocus;
                childInput.LostFocus += IntegerValidationHelper.Object_LostFocus;
                childInput.PreviewKeyUp += ChildInput_PreviewKeyUp;
            }
            else if (item.TipeData == "decimal")
            {
                temp = Resources["TextBoxDecimalTemplate"] as StackPanel;
                var childLabel = temp.Children[0] as TextBlock;
                childLabel.Text = item.Parameter;
                var childGrid = temp.Children[1] as Grid;
                var childInput = childGrid.Children[0] as TextBox;
                childInput.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                childInput.GotFocus += DecimalValidationHelper.Object_GotFocus;
                childInput.LostFocus += DecimalValidationHelper.Object_LostFocus;
                childInput.PreviewKeyUp += ChildInput_PreviewKeyUp;
            }
            else
            {
                temp = Resources["TextBoxTemplate"] as StackPanel;
                var childLabel = temp.Children[0] as TextBlock;
                childLabel.Text = item.Parameter;
                var childGrid = temp.Children[1] as Grid;
                var childInput = childGrid.Children[0] as TextBox;
                childInput.PreviewKeyUp += ChildInput_PreviewKeyUp;
            }

            return temp;
        }

        private StackPanel SetupDataList(MasterTipePermohonanDetailDto item, int idx)
        {
            StackPanel temp;
            if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
            {
                if (item.Parameter.ToLower().Trim() == "estimasi waktu pengiriman")
                {
                    temp = Resources["ComboBoxTemplate"] as StackPanel;
                    var childLabel = temp.Children[0] as TextBlock;
                    childLabel.Text = item.Parameter;
                    var childGrid = temp.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as ComboBox;
                    var dataDetail = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("15-60 menit setelah pesan", "15-60 menit setelah pesan"),
                        new KeyValuePair<string, string>("1-2 jam setelah pesan", "1-2 jam setelah pesan"),
                        new KeyValuePair<string, string>("2-4 Jam setelah pesan", "2-4 Jam setelah pesan"),
                        new KeyValuePair<string, string>("4-12 Jam setelah pesan", "4-12 Jam setelah pesan"),
                        new KeyValuePair<string, string>("1 x 24 jam setelah pesan", "1 x 24 jam setelah pesan"),
                        new KeyValuePair<string, string>("2 x 24 jam setelah pesan", "2 x 24 jam setelah pesan"),
                        new KeyValuePair<string, string>("2-3 hari setelah pesan", "2-3 hari setelah pesan"),
                    };
                    childInput.ItemsSource = dataDetail;
                    childInput.DisplayMemberPath = "Value";
                    childInput.SelectionChanged += ChildInput_SelectionChanged;
                }
                else if (item.Parameter.ToLower().Trim() == "waktu penagihan")
                {
                    temp = Resources["ComboBoxTemplate"] as StackPanel;
                    var childLabel = temp.Children[0] as TextBlock;
                    childLabel.Text = item.Parameter;
                    var childGrid = temp.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as ComboBox;
                    var dataDetail = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Setelah proses permohonan", "Setelah proses permohonan"), new KeyValuePair<string, string>("Setelah pengantaran", "Setelah pengantaran"), new KeyValuePair<string, string>("Setelah BA", "Setelah BA") };
                    childInput.ItemsSource = dataDetail;
                    childInput.DisplayMemberPath = "Value";
                    childInput.SelectionChanged += ChildInput_SelectionChanged;
                }
                else
                {
                    temp = Resources["ComboBoxTemplate"] as StackPanel;
                    var childLabel = temp.Children[0] as TextBlock;
                    childLabel.Text = item.Parameter;
                    var childGrid = temp.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as ComboBox;
                    var dataDetail = _viewModel.DataParamPermohonan.Where(x => x.Name == item.Parameter).FirstOrDefault();
                    childInput.ItemsSource = dataDetail.Data;
                    childInput.DisplayMemberPath = "Value";
                    childInput.SelectionChanged += ChildInput_SelectionChanged;
                }
            }
            else
            {
                temp = Resources["ComboBoxTemplate"] as StackPanel;
                var childLabel = temp.Children[0] as TextBlock;
                childLabel.Text = item.Parameter;
                var childGrid = temp.Children[1] as Grid;
                var childInput = childGrid.Children[0] as ComboBox;
                var dataDetail = _viewModel.DataParamPermohonan.Where(x => x.Name == item.Parameter).FirstOrDefault();
                childInput.ItemsSource = dataDetail.Data;
                childInput.DisplayMemberPath = "Value";
                childInput.SelectionChanged += ChildInput_SelectionChanged;
            }

            return temp;
        }

        private void SetupEdit()
        {
            TanggalSection.Visibility = Visibility.Visible;
            Keterangan.Text = _viewModel.FormData.Keterangan;
            TglBeritaAcara.SelectedDate = _viewModel.FormData.BeritaAcara.TanggalBa ?? DateTime.Now;
            TglPermohonan.SelectedDate = _viewModel.FormData.WaktuPermohonan;

            foreach (var item in _viewModel.SelectedTipePermohonan.DetailParameter.Where(c => AppExtensionsHelpers.Right(c.Parameter, 4).ToLower() != "lama"))
            {
                foreach (StackPanel elem in Canvas.Children)
                {
                    var childLabel = elem.Children[0] as TextBlock;
                    if (item.Parameter == childLabel.Text)
                    {
                        if (item.IdListData == null && item.TipeData != "bool")
                        {
                            if (item.TipeData == "date")
                            {
                                var childBorder = elem.Children[1] as Border;
                                var childGrid = childBorder.Child as Grid;
                                var childDatePicker = childGrid.Children[0] as DatePicker;
                                childDatePicker.SelectedDateFormat = DatePickerFormat.Long;
                                var obj = _viewModel.FormData.Parameter.Find(x => x.Parameter == item.Parameter);
                                if (obj != null && !string.IsNullOrWhiteSpace((string)obj.Value))
                                {
                                    childDatePicker.SelectedDate = Convert.ToDateTime(obj.Value, CultureInfo.InvariantCulture);
                                }
                            }
                            else if (item.TipeData == "int")
                            {
                                var childGrid = elem.Children[1] as Grid;
                                var childInput = childGrid.Children[0] as TextBox;
                                var obj = _viewModel.FormData.Parameter.Find(x => x.Parameter == item.Parameter);
                                if (obj != null)
                                    childInput.Text = IntegerValidationHelper.FormatIntegerToStringId(obj.Value is null ? 0 : Convert.ToInt32(obj.Value));
                            }
                            else if (item.TipeData == "decimal")
                            {
                                var childGrid = elem.Children[1] as Grid;
                                var childInput = childGrid.Children[0] as TextBox;
                                var obj = _viewModel.FormData.Parameter.Find(x => x.Parameter == item.Parameter);
                                if (obj != null)
                                    childInput.Text = DecimalValidationHelper.FormatDecimalToStringId(obj.Value is null ? decimal.Zero : Convert.ToDecimal(obj.Value));
                            }
                            else
                            {
                                if (_viewModel.FormData.NamaTipePermohonan.ToLower().Trim() == "air tangki" &&
                                    (item.Parameter.ToLower().Trim() == "estimasi waktu pengiriman" ||
                                     item.Parameter.ToLower().Trim() == "waktu penagihan"))
                                {
                                    var childGrid = elem.Children[1] as Grid;
                                    var childInput = childGrid.Children[0] as ComboBox;
                                    var obj = _viewModel.FormData.Parameter.Find(x => x.Parameter == item.Parameter);
                                    if (obj != null)
                                    {
                                        var list = childInput.ItemsSource as List<KeyValuePair<string, string>>;
                                        KeyValuePair<string, string>? select = list.Find(x => x.Key == Convert.ToString(obj.Value));
                                        if (select != null)
                                            childInput.SelectedItem = select.Value;
                                    }
                                }
                                else
                                {
                                    var childGrid = elem.Children[1] as Grid;
                                    var childInput = childGrid.Children[0] as TextBox;
                                    var obj = _viewModel.FormData.Parameter.Find(x => x.Parameter == item.Parameter);
                                    if (obj != null)
                                        childInput.Text = Convert.ToString(obj.Value);
                                }
                            }
                        }
                        else
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[0] as ComboBox;
                            var obj = _viewModel.FormData.Parameter.Find(x => x.Parameter == item.Parameter);
                            if (obj != null)
                            {
                                if (item.TipeData == "bool")
                                {
                                    var list = childInput.ItemsSource as List<dynamic>;
                                    var select = list.Find(x => (bool)x.Key == (bool)obj.Value);
                                    if (select != null)
                                        childInput.SelectedItem = select;
                                }
                                else
                                {
                                    var list = childInput.ItemsSource as List<ParamPermohonanData>;
                                    var select = list.Find(x => x.Key == Convert.ToString(obj.Value));
                                    if (select != null)
                                        childInput.SelectedItem = select;
                                }
                            }
                        }
                    }
                }
            }

            decimal biaya = 0;
            decimal ppn = 0;
            decimal total = 0;
            foreach (var item in _viewModel.SelectedTipePermohonan.DetailBiaya)
            {
                if (item.IsLocked.HasValue && !item.IsLocked.Value)
                {
                    foreach (StackPanel elem in Canvas.Children)
                    {
                        var childLabel = elem.Children[0] as TextBlock;
                        if (childLabel.Text == item.Parameter)
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[index: 0] as TextBox;
                            var obj = _viewModel.FormData.NonAirReguler?.Detail.FirstOrDefault(x => x.Parameter == item.Parameter);
                            if (obj != null)
                                childInput.Text = DecimalValidationHelper.FormatDecimalToStringId(obj.Value ?? decimal.Zero);
                        }
                    }
                }

                if (item.Biaya.HasValue)
                    biaya += item.Biaya ?? decimal.Zero;
            }

            if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
            {
                total = decimal.Zero;
            }
            else
            {
                ppn = _viewModel.SelectedTipePermohonan.PersentasePpn / 100 * biaya;
                total = biaya + ppn;
            }

            Biaya.Text = biaya.ToString("#,##0.##", new CultureInfo("id-ID"));
            Ppn.Text = ppn.ToString("#,##0.##", new CultureInfo("id-ID"));
            Total.Text = total.ToString("#,##0.##", new CultureInfo("id-ID"));
        }

        private void ChildInput_BiayaChanged(object sender, TextChangedEventArgs e)
        {
            decimal biaya = 0;
            foreach (var item in _viewModel.SelectedTipePermohonan.DetailBiaya)
            {
                if (item.IsLocked.HasValue && !item.IsLocked.Value)
                {
                    foreach (StackPanel elem in Canvas.Children)
                    {
                        var childLabel = elem.Children[0] as TextBlock;
                        if (childLabel.Text == item.Parameter)
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[0] as TextBox;
                            biaya += DecimalValidationHelper.FormatStringIdToDecimal(childInput.Text);
                        }
                    }
                }
                else
                    biaya += item.Biaya ?? decimal.Zero;
            }

            var ppn = _viewModel.SelectedTipePermohonan.PersentasePpn / 100 * biaya;
            var total = biaya + ppn;
            Biaya.Text = biaya.ToString("#,##0.##", new CultureInfo("id-ID"));
            Ppn.Text = ppn.ToString("#,##0.##", new CultureInfo("id-ID"));
            Total.Text = total.ToString("#,##0.##", new CultureInfo("id-ID"));
        }

        private void ChildDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ChildInput_PreviewKeyUp(object sender, KeyEventArgs e) => CheckSubmitStatus();

        private void ChildInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            CheckSubmitStatus();

            if (GroupDataSambunganBaru.Visibility == Visibility.Visible && _isComplete == false)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                  false,
                  true,
                  "InnerGlobalRootDialog",
                  "Data Sambungan Baru Belum Lengkap",
                  $"Harap lengkapi data sambungan baru untuk dapat melanjutkan proses !",
                  "warning",
                  moduleName: "distribusi");
            }
            else
            {
                _viewModel.FormBa = new ParamPermohonanBaDto();

                if (_viewModel.IsFor == "edit")
                {
                    _viewModel.FormBa.IdPermohonan = _viewModel.SelectedData.IdPermohonan;
                }
                else
                {
                    _viewModel.FormBa.IdPermohonan = _viewModel.SelectedDataPermohonan.IdPermohonan;
                }

                _viewModel.FormBa.TanggalBa = TglBeritaAcara.SelectedDate?.AddMilliseconds(DateTime.Now.TimeOfDay.TotalMilliseconds) ?? DateTime.Now;

                _viewModel.FormBa.IdUser = Convert.ToInt32(Application.Current.Resources["IdUser"]);

                _viewModel.FormBa.PersilNamaPaket = _viewModel.SelectedPaketRabPipaPersil?.NamaPaket;
                _viewModel.FormBa.PersilFlagDialihkanKeVendor = DialihkanKeVendorPipaPersil.IsChecked == true;
                _viewModel.FormBa.PersilFlagBiayaDibebankanKePdam = DialihkanKePDAMPipaPersil.IsChecked == true;

                _viewModel.FormBa.DistribusiNamaPaket = _viewModel.SelectedPaketRabPipaDistribusi?.NamaPaket;
                _viewModel.FormBa.DistribusiFlagDialihkanKeVendor = DialihkanKeVendorPipaDistribusi.IsChecked == true;
                _viewModel.FormBa.DistribusiFlagBiayaDibebankankePdam = DialihkanKePDAMPipaDistribusi.IsChecked == true;

                var tempDetail = new List<ParamPermohonanRkpDetailDto>();

                if (_viewModel.DetailRabPipaPersil != null)
                {
                    foreach (var i in _viewModel.DetailRabPipaPersil)
                    {
                        tempDetail.Add(new ParamPermohonanRkpDetailDto
                        {
                            Tipe = i.Tipe,
                            Kode = i.Kode,
                            Uraian = i.Uraian,
                            HargaSatuan = i.HargaSatuan,
                            Satuan = i.Satuan,
                            Qty = i.Qty,
                            Jumlah = i.Jumlah,
                            Ppn = i.Ppn,
                            Keuntungan = i.Keuntungan,
                            JasaDariBahan = i.JasaDariBahan,
                            Total = i.Total,
                            Kategori = "-",
                            Kelompok = i.Kelompok,
                            PostBiaya = i.PostBiaya,
                            QtyRkp = i.Qty,
                            FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdamWpf,
                            FlagDialihkanKeVendor = i.FlagDialihkanKevendorWpf,
                            FlagPaket = i.Tipe.Right(8) != "Tambahan",
                            FlagDistribusi = false,
                        });
                    }
                }

                if (_viewModel.DetailRabPipaDistribusi != null)
                {
                    foreach (var i in _viewModel.DetailRabPipaDistribusi)
                    {
                        tempDetail.Add(new ParamPermohonanRkpDetailDto
                        {
                            Tipe = i.Tipe,
                            Kode = i.Kode,
                            Uraian = i.Uraian,
                            HargaSatuan = i.HargaSatuan,
                            Satuan = i.Satuan,
                            Qty = i.Qty,
                            Jumlah = i.Jumlah,
                            Ppn = i.Ppn,
                            Keuntungan = i.Keuntungan,
                            JasaDariBahan = i.JasaDariBahan,
                            Total = i.Total,
                            Kategori = "-",
                            Kelompok = i.Kelompok,
                            PostBiaya = i.PostBiaya,
                            QtyRkp = i.Qty,
                            FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdamWpf,
                            FlagDialihkanKeVendor = i.FlagDialihkanKevendorWpf,
                            FlagPaket = i.Tipe.Right(8) != "Tambahan",
                            FlagDistribusi = true,
                        });
                    }
                }

                _viewModel.FormBa.DetailRkp = tempDetail;

                var petugas = new List<ParamPetugasBaDto>();
                var counter = 0;
                foreach (var item in PetugasComponentList)
                {
                    ComboBox option = null;
                    if (counter == 0)
                    {
                        option = (ComboBox)LogicalTreeHelper.FindLogicalNode(item as UIElement, "Petugas1");
                    }
                    else
                    {
                        option = (ComboBox)LogicalTreeHelper.FindLogicalNode(item as UIElement, "OptionList");
                    }
                    if (option != null && option.SelectedItem != null)
                    {
                        petugas.Add(new ParamPetugasBaDto() { IdPegawai = ((MasterPegawaiDto)option.SelectedItem).IdPegawai });
                    }
                    counter++;
                }

                _viewModel.FormBa.Petugas = petugas;

                if (GroupDataMeteran.Visibility == Visibility.Visible)
                {
                    _viewModel.FormBa.MerekMeter = _viewModel.MerekMeterForm?.NamaMerekMeter;
                    _viewModel.FormBa.NoSeriMeter = _viewModel.FormData.BeritaAcara.NoSeriMeter;
                    _viewModel.FormBa.AngkatMeter = _viewModel.FormData.BeritaAcara.AngkatMeter;
                    _viewModel.FormBa.KeteranganLapangan = _viewModel.FormData.BeritaAcara.KeteranganLapangan;
                }

                #region add to parameter body
                if (_viewModel.FormBa != null)
                {
                    var data = _viewModel.FormBa;
                    var type = typeof(ParamPermohonanBaDto);
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

                    #region update data permohonan sambungan air
                    if (GroupDataSambunganBaru.Visibility == Visibility.Visible)
                    {
                        var bodyPerbarui = new Dictionary<string, dynamic>()
                        {
                            {"IdPermohonan", _viewModel.FormData?.IdPermohonan },
                            {"IdTipePendaftaranSambungan", _viewModel.TipePendaftaranForm?.IdTipePendaftaranSambungan},
                            {"Nama", _viewModel.FormData?.Nama},
                            {"Alamat", _viewModel.FormData?.Alamat},
                            {"NosambYangDiberikan", _viewModel.FormData?.NosambYangDiberikan},
                            {"Rt", _viewModel.FormData?.Rt},
                            {"Rw", _viewModel.FormData?.Rw},
                            {"NoHp", _viewModel.FormData?.NoHp},
                            {"NoTelp", _viewModel.FormData?.NoTelp},
                            {"Email", _viewModel.FormData?.Email},
                            {"IdPekerjaan", _viewModel.PekerjaanForm?.IdPekerjaan},
                            {"NoKk", _viewModel.FormData?.NoKk},
                            {"NoKtp", _viewModel.FormData?.NoKtp},
                            {"IdKelurahan", _viewModel.KelurahanForm?.IdKelurahan},
                            {"IdRayon", _viewModel.RayonForm?.IdRayon},
                            {"IdBlok", _viewModel.BlokForm?.IdBlok},
                            {"IdGolongan", _viewModel.GolonganForm?.IdGolongan},
                            {"IdDiameter", _viewModel.DiameterForm?.IdDiameter},
                            {"IdAdministrasiLain", _viewModel.AdministrasiLainForm?.IdAdministrasiLain},
                            {"IdPemeliharaanLain", _viewModel.PemeliharaanLainForm?.IdPemeliharaanLain},
                            {"IdRetribusiLain", _viewModel.RetribusiLainForm?.IdRetribusiLain},
                            {"FlagLanjutKeLanggananLimbah", _viewModel.FormData?.FlagLanjutKeLanggananLimbah == true},
                            {"Penghuni", _viewModel.FormData?.Penghuni},
                            {"LuasTanah", _viewModel.FormData?.LuasTanah},
                            {"LuasRumah", _viewModel.FormData?.LuasRumah},
                            {"IdJenisBangunan", _viewModel.JenisBangunanForm?.IdJenisBangunan},
                            {"IdKepemilikan", _viewModel.KepemilikanBangunanForm?.IdKepemilikan},
                            {"IdPeruntukan", _viewModel.PeruntukanForm?.IdPeruntukan},
                            {"IdSumberAir", _viewModel.SumberAirForm?.IdSumberAir},
                            {"KodePost", _viewModel.FormData?.KodePost},
                            {"DayaListrik", _viewModel.FormData?.DayaListrik},
                            {"Keterangan", _viewModel.FormData?.Keterangan},
                            {"AlamatPemilik", _viewModel.FormData?.AlamatPemilik},
                            {"NamaPemilik", _viewModel.FormData?.NamaPemilik},
                            {"NosambDepan", _viewModel.FormData?.NosambDepan},
                            {"NosambBelakang", _viewModel.FormData?.NosambBelakang},
                            {"NosambKanan", _viewModel.FormData?.NosambKanan},
                            {"NosambKiri", _viewModel.FormData?.NosambKiri},
                            {"IdDma", _viewModel.DmaForm?.IdDma},
                            {"IdDmz", _viewModel.DmzForm?.IdDmz},
                            {"IdMerekMeter", _viewModel.MerekMeterForm?.IdMerekMeter},
                            {"NoSeriMeter", _viewModel.FormData?.NoSeriMeter},
                            {"StanAwalPasang", _viewModel.FormData?.StanAwalPasang ?? 0},
                        };

                        if (_generateNosamb == true)
                        {
                            bodyPerbarui.Add("FlagInsertMasterPelanggan", true); /// update permohonan-non-pelanggan untuk langsung add calon pelanggan ke master pelanggan air
                        }

                        _ = ((AsyncCommandBase)_viewModel.OnSubmitBaPerbaruiDataSambBaruFormCommand).ExecuteAsync(bodyPerbarui);
                    }
                    #endregion

                    _ = ((AsyncCommandBase)_viewModel.OnSubmitBaFormCommand).ExecuteAsync(body);
                }
            }
        }

        private void CheckSubmitStatus()
        {
            if (GroupDataSambunganBaru.Visibility == Visibility.Visible)
            {
                _isComplete = false;

                if (_viewModel.RayonForm != null && _viewModel.TipePendaftaranForm != null && !string.IsNullOrWhiteSpace(Nama.Text)
                     && !string.IsNullOrWhiteSpace(AlamatSambBaru.Text) && !string.IsNullOrWhiteSpace(NoHandphone.Text) && !string.IsNullOrWhiteSpace(NoKk.Text)
                      && !string.IsNullOrWhiteSpace(NoKtp.Text) && _viewModel.KelurahanForm != null && _viewModel.RayonForm != null
                      && _viewModel.GolonganForm != null && _viewModel.DiameterForm != null && _viewModel.AdministrasiLainForm != null
                      && _viewModel.PemeliharaanLainForm != null && _viewModel.RetribusiLainForm != null && !string.IsNullOrWhiteSpace(_viewModel.FormData.NosambYangDiberikanWpf))
                {
                    _isComplete = true;
                }
            }
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PermohonanViewModel)
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

        private void BtnOnBrowseFileBuktiBeritaAcara1Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_buktiberitaacara1");
            CheckSubmitStatus();
        }

        private void BtnOnBrowseFileBuktiBeritaAcara2Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_buktiberitaacara2");
            CheckSubmitStatus();
        }

        private void BtnOnBrowseFileBuktiBeritaAcara3Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_buktiberitaacara3");
            CheckSubmitStatus();
        }

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

        private void SetMarker()
        {
            var viewModel = (PermohonanViewModel)DataContext;
            if (viewModel.FormData != null && viewModel.FormData.Latitude != null && viewModel.FormData.Longitude != null)
            {
                var markerList = new List<MapMarkerObject>() { new MapMarkerObject() { MarkerInformation = $"{viewModel.FormData.Nama}", Latitude = viewModel.FormData.Latitude, Longitude = viewModel.FormData.Longitude } };

                MapConfigHelper.SetMarker(MainMap, markerList);
            }
        }

        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PermohonanViewModel)DataContext;
            _ = DialogHost.Show(new MapView(viewModel, RefreshMap), "InnerGlobalRootDialog");
        }

        private bool RefreshMap()
        {
            if (DataContext is PermohonanViewModel viewModel)
            {
                MapConfigHelper.SetCenter(MainMap, viewModel.FormData.Latitude, viewModel.FormData.Longitude);
            }

            return true;
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
            var viewModel = (PermohonanViewModel)DataContext;
            viewModel.FormData.Latitude = _currentLat;
            viewModel.FormData.Longitude = _currentLon;
            // viewModel.FormData.AlamatMap = _currentAlamatMap;
        }

        #endregion

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            PipaPersilSection.Visibility = current == "GroupRabPipaPersil" ? Visibility.Visible : Visibility.Collapsed;
            PipaDistribusiSection.Visibility = current == "GroupRabPipaDistribusi" ? Visibility.Visible : Visibility.Collapsed;
            FotoSection.Visibility = current == "GroupFoto" ? Visibility.Visible : Visibility.Collapsed;
            PermohonanSection.Visibility = current == "GroupPermohonan" ? Visibility.Visible : Visibility.Collapsed;
            DataSambunganBaruSection.Visibility = current == "GroupDataSambunganBaru" ? Visibility.Visible : Visibility.Collapsed;
            DataMeteranSection.Visibility = current == "GroupDataMeteran" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadRabPipaPersil_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaPersil != null)
            {
                _viewModel.DetailRabPipaPersil = new ObservableCollection<DetailRabWpf>();
                var detail = new ObservableCollection<DetailRabWpf>();

                if (_viewModel.SelectedPaketRabPipaPersil != null)
                {
                    BtnOngkosPipaPersil.Visibility = Visibility.Visible;
                    BtnMaterialPipaPersil.Visibility = Visibility.Visible;

                    if (_viewModel.SelectedPaketRabPipaPersil.DetailMaterial != null)
                    {
                        var listMaterial = _viewModel.SelectedPaketRabPipaPersil.DetailMaterial;
                        var listOngkos = _viewModel.SelectedPaketRabPipaPersil.DetailOngkos;

                        var persenPpnMaterial = _viewModel.SelectedPaketRabPipaPersil.PpnMaterial ?? 0;
                        var persenPpnOngkos = _viewModel.SelectedPaketRabPipaPersil.PpnOngkos ?? 0;
                        var persenKeuntungan = _viewModel.SelectedPaketRabPipaPersil.PersentaseKeuntungan ?? 0;
                        var persenJasaDariBahan = _viewModel.SelectedPaketRabPipaPersil.PersentaseJasaDariBahan ?? 0;

                        foreach (var i in listMaterial)
                        {
                            var ppn = persenPpnMaterial / 100 * i.Total;
                            var keuntungan = persenKeuntungan / 100 * i.Total;
                            var jasaDariBahan = persenJasaDariBahan / 100 * i.Total;

                            detail.Add(new DetailRabWpf
                            {
                                Kode = i.KodeMaterial,
                                Tipe = "Material",
                                Uraian = i.NamaMaterial,
                                HargaSatuan = i.HargaJual,
                                Satuan = i.Satuan,
                                Qty = i.Qty,
                                Jumlah = i.Total,
                                Ppn = ppn,
                                Keuntungan = keuntungan,
                                JasaDariBahan = jasaDariBahan,
                                Total = i.Total + ppn + keuntungan + jasaDariBahan,
                                FlagBiayaDibebankanKePdam = DialihkanKePDAMPipaPersil.IsChecked == true,
                                FlagDialihkanKevendor = DialihkanKeVendorPipaPersil.IsChecked == true,
                            });
                        }

                        foreach (var i in listOngkos)
                        {
                            var ppn = persenPpnOngkos / 100 * i.Total;
                            var keuntungan = persenKeuntungan / 100 * i.Total;

                            detail.Add(new DetailRabWpf
                            {
                                Kode = i.KodeOngkos,
                                Tipe = "Ongkos",
                                Uraian = i.NamaOngkos,
                                HargaSatuan = i.Biaya,
                                Satuan = i.Satuan,
                                Qty = i.Qty,
                                Jumlah = i.Total,
                                Ppn = ppn,
                                Keuntungan = keuntungan,
                                Total = i.Total + ppn + keuntungan,
                                Kelompok = i.Kelompok,
                                PostBiaya = i.PostBiaya,
                                FlagBiayaDibebankanKePdam = DialihkanKePDAMPipaPersil.IsChecked == true,
                                FlagDialihkanKevendor = DialihkanKeVendorPipaPersil.IsChecked == true,
                            });
                        }
                    }

                    _viewModel.DetailRabPipaPersil = detail;
                }

                _viewModel.OnHitungRabCommand.Execute(null);
            }

            CheckSubmitStatus();
        }

        private void PilihMaterialTambahanPipaPersil_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaPersil != null)
            {
                _viewModel.TipeRab = "Pipa Persil";
                _viewModel.DaftarMaterial.Clear();

                var flagBiayaDibebankanKePdam = DialihkanKePDAMPipaPersil.IsChecked == true;
                var flagDialihkanKeVendor = DialihkanKeVendorPipaPersil.IsChecked == true;

                _ = DialogHost.Show(new DaftarMaterialView(_viewModel, flagBiayaDibebankanKePdam, flagDialihkanKeVendor), "InnerGlobalRootDialog");
            }
        }

        private void PilihOngkosTambahanPipaPersil_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaPersil != null)
            {
                _viewModel.TipeRab = "Pipa Persil";
                _viewModel.DaftarOngkos.Clear();

                var flagBiayaDibebankanKePdam = DialihkanKePDAMPipaPersil.IsChecked == true;
                var flagDialihkanKeVendor = DialihkanKeVendorPipaPersil.IsChecked == true;

                _ = DialogHost.Show(new DaftarOngkosView(_viewModel, flagBiayaDibebankanKePdam, flagDialihkanKeVendor), "InnerGlobalRootDialog");
            }
        }

        private void DeletePipaPersil_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DetailRabPipaPersil != null && _viewModel.SelectedDetailRabPipaPersil != null)
            {
                var temp = _viewModel.SelectedDetailRabPipaPersil;
                _viewModel.DetailRabPipaPersil.Remove(_viewModel.DetailRabPipaPersil.FirstOrDefault(c => c.Kode == temp.Kode && c.Tipe == temp.Tipe));
                _viewModel.OnHitungRabCommand.Execute(null);
            }
        }

        private void DibebankanKeVendorPipaPersil_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DetailRabPipaPersil != null && _viewModel.SelectedDetailRabPipaPersil != null)
            {
                var temp = _viewModel.SelectedDetailRabPipaPersil;
                temp.FlagDialihkanKevendorWpf = true;
                _viewModel.OnHitungRabCommand.Execute(null);
            }
        }

        private void DibebankanKePDAMPipaPersil_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DetailRabPipaPersil != null && _viewModel.SelectedDetailRabPipaPersil != null)
            {
                var temp = _viewModel.SelectedDetailRabPipaPersil;
                temp.FlagBiayaDibebankanKePdamWpf = true;
                _viewModel.OnHitungRabCommand.Execute(null);
            }
        }

        private void LoadRabPipaDistribusi_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaDistribusi != null)
            {
                _viewModel.DetailRabPipaDistribusi = new ObservableCollection<DetailRabWpf>();
                var detail = new ObservableCollection<DetailRabWpf>();

                if (_viewModel.SelectedPaketRabPipaDistribusi != null)
                {
                    BtnOngkosPipaDistribusi.Visibility = Visibility.Visible;
                    BtnMaterialPipaDistribusi.Visibility = Visibility.Visible;

                    if (_viewModel.SelectedPaketRabPipaDistribusi.DetailMaterial != null)
                    {
                        var listMaterial = _viewModel.SelectedPaketRabPipaDistribusi.DetailMaterial;
                        var listOngkos = _viewModel.SelectedPaketRabPipaDistribusi.DetailOngkos;

                        var persenPpnMaterial = _viewModel.SelectedPaketRabPipaDistribusi.PpnMaterial ?? 0;
                        var persenPpnOngkos = _viewModel.SelectedPaketRabPipaDistribusi.PpnOngkos ?? 0;
                        var persenKeuntungan = _viewModel.SelectedPaketRabPipaDistribusi.PersentaseKeuntungan ?? 0;
                        var persenJasaDariBahan = _viewModel.SelectedPaketRabPipaDistribusi.PersentaseJasaDariBahan ?? 0;

                        foreach (var i in listMaterial)
                        {
                            var ppn = persenPpnMaterial / 100 * i.Total;
                            var keuntungan = persenKeuntungan / 100 * i.Total;
                            var jasaDariBahan = persenJasaDariBahan / 100 * i.Total;

                            detail.Add(new DetailRabWpf
                            {
                                Kode = i.KodeMaterial,
                                Tipe = "Material",
                                Uraian = i.NamaMaterial,
                                HargaSatuan = i.HargaJual,
                                Satuan = i.Satuan,
                                Qty = i.Qty,
                                Jumlah = i.Total,
                                Ppn = ppn,
                                Keuntungan = keuntungan,
                                JasaDariBahan = jasaDariBahan,
                                Total = i.Total + ppn + keuntungan + jasaDariBahan,
                                FlagBiayaDibebankanKePdam = DialihkanKePDAMPipaDistribusi.IsChecked == true,
                                FlagDialihkanKevendor = DialihkanKeVendorPipaDistribusi.IsChecked == true,
                            });
                        }

                        foreach (var i in listOngkos)
                        {
                            var ppn = persenPpnOngkos / 100 * i.Total;
                            var keuntungan = persenKeuntungan / 100 * i.Total;

                            detail.Add(new DetailRabWpf
                            {
                                Kode = i.KodeOngkos,
                                Tipe = "Ongkos",
                                Uraian = i.NamaOngkos,
                                HargaSatuan = i.Biaya,
                                Satuan = i.Satuan,
                                Qty = i.Qty,
                                Jumlah = i.Total,
                                Ppn = ppn,
                                Keuntungan = keuntungan,
                                Total = i.Total + ppn + keuntungan,
                                Kelompok = i.Kelompok,
                                PostBiaya = i.PostBiaya,
                                FlagBiayaDibebankanKePdam = DialihkanKePDAMPipaDistribusi.IsChecked == true,
                                FlagDialihkanKevendor = DialihkanKeVendorPipaDistribusi.IsChecked == true,
                            });
                        }
                    }

                    _viewModel.DetailRabPipaDistribusi = detail;
                }

                _viewModel.OnHitungRabCommand.Execute(null);
            }

            CheckSubmitStatus();
        }

        private void PilihMaterialTambahanPipaDistribusi_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaDistribusi != null)
            {
                _viewModel.TipeRab = "Pipa Distribusi";
                _viewModel.DaftarMaterial.Clear();

                var flagBiayaDibebankanKePdam = DialihkanKePDAMPipaDistribusi.IsChecked == true;
                var flagDialihkanKeVendor = DialihkanKeVendorPipaDistribusi.IsChecked == true;

                _ = DialogHost.Show(new DaftarMaterialView(_viewModel, flagBiayaDibebankanKePdam, flagDialihkanKeVendor), "InnerGlobalRootDialog");
            }
        }

        private void PilihOngkosTambahanPipaDistribusi_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPaketRabPipaDistribusi != null)
            {
                _viewModel.TipeRab = "Pipa Distribusi";
                _viewModel.DaftarOngkos.Clear();

                var flagBiayaDibebankanKePdam = DialihkanKePDAMPipaDistribusi.IsChecked == true;
                var flagDialihkanKeVendor = DialihkanKeVendorPipaDistribusi.IsChecked == true;

                _ = DialogHost.Show(new DaftarOngkosView(_viewModel, flagBiayaDibebankanKePdam, flagDialihkanKeVendor), "InnerGlobalRootDialog");
            }
        }

        private void DeletePipaDistribusi_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DetailRabPipaDistribusi != null && _viewModel.SelectedDetailRabPipaDistribusi != null)
            {
                var temp = _viewModel.SelectedDetailRabPipaDistribusi;
                _viewModel.DetailRabPipaDistribusi.Remove(_viewModel.DetailRabPipaDistribusi.FirstOrDefault(c => c.Kode == temp.Kode && c.Tipe == temp.Tipe));
                _viewModel.OnHitungRabCommand.Execute(null);
            }
        }

        private void DibebankanKeVendorPipaDistribusi_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DetailRabPipaDistribusi != null && _viewModel.SelectedDetailRabPipaDistribusi != null)
            {
                var temp = _viewModel.SelectedDetailRabPipaDistribusi;
                temp.FlagDialihkanKevendorWpf = true;
                _viewModel.OnHitungRabCommand.Execute(null);
            }
        }

        private void DibebankanKePDAMPipaDistribusi_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.DetailRabPipaDistribusi != null && _viewModel.SelectedDetailRabPipaDistribusi != null)
            {
                var temp = _viewModel.SelectedDetailRabPipaDistribusi;
                temp.FlagBiayaDibebankanKePdamWpf = true;
                _viewModel.OnHitungRabCommand.Execute(null);
            }
        }

        private void GetDetailRabExist()
        {
            var tempDetailPipaPersil = new ObservableCollection<DetailRabWpf>();
            var tempDetailPipaDistribusi = new ObservableCollection<DetailRabWpf>();
            var selectedData = _viewModel.FormData;

            if (!string.IsNullOrWhiteSpace(selectedData.RAB?.PersilNamaPaket))
            {
                _viewModel.SelectedPaketRabPipaPersil = _viewModel.PaketRabList?.FirstOrDefault(c => c.NamaPaket == selectedData.RAB.PersilNamaPaket) ?? null;

                PaketRabPersilText.Text = _viewModel.SelectedPaketRabPipaPersil?.NamaPaket;
            }
            else
            {
                _viewModel.SelectedPaketRabPipaPersil = null;
                PaketRabPersilText.Visibility = Visibility.Collapsed;
                PaketRabPersil.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(selectedData.RAB?.DistribusiNamaPaket))
            {
                _viewModel.SelectedPaketRabPipaDistribusi = _viewModel.PaketRabList?.FirstOrDefault(c => c.NamaPaket == selectedData.RAB.DistribusiNamaPaket) ?? null;
                PaketRabDistribusiText.Text = _viewModel.SelectedPaketRabPipaDistribusi?.NamaPaket;
            }
            else
            {
                _viewModel.SelectedPaketRabPipaDistribusi = null;
                PaketRabDistribusiText.Visibility = Visibility.Collapsed;
                PaketRabDistribusi.Visibility = Visibility.Visible;
            }

            DialihkanKeVendorPipaPersil.IsChecked = selectedData.RAB?.PersilFlagDialihkanKeVendor == true;
            DialihkanKePDAMPipaPersil.IsChecked = selectedData.RAB?.PersilFlagBiayaDibebankanKePdam == true;

            DialihkanKeVendorPipaDistribusi.IsChecked = selectedData.RAB?.DistribusiFlagDialihkanKeVendor == true;
            DialihkanKePDAMPipaDistribusi.IsChecked = selectedData.RAB?.DistribusiFlagBiayaDibebankankePdam == true;

            if (selectedData.RAB?.DetailPersil != null)
            {
                foreach (var i in selectedData.RAB.DetailPersil)
                {
                    tempDetailPipaPersil.Add(new DetailRabWpf
                    {
                        Kode = i.Kode,
                        Tipe = i.Tipe,
                        Uraian = i.Uraian,
                        Satuan = i.Satuan,
                        HargaSatuan = i.HargaSatuan,
                        Qty = i.Qty,
                        Jumlah = i.Jumlah,
                        Ppn = i.Ppn,
                        Keuntungan = i.Keuntungan,
                        JasaDariBahan = i.JasaDariBahan,
                        Total = i.Total,
                        Kelompok = i.Kelompok,
                        PostBiaya = i.PostBiaya,
                        FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdam,
                        FlagDialihkanKevendor = i.FlagDialihkanKeVendor
                    });
                }
            }

            if (selectedData.RAB?.DetailDistribusi != null)
            {
                foreach (var i in selectedData.RAB.DetailDistribusi)
                {
                    tempDetailPipaDistribusi.Add(new DetailRabWpf
                    {
                        Kode = i.Kode,
                        Tipe = i.Tipe,
                        Uraian = i.Uraian,
                        Satuan = i.Satuan,
                        HargaSatuan = i.HargaSatuan,
                        Qty = i.Qty,
                        Jumlah = i.Jumlah,
                        Ppn = i.Ppn,
                        Keuntungan = i.Keuntungan,
                        JasaDariBahan = i.JasaDariBahan,
                        Total = i.Total,
                        Kelompok = i.Kelompok,
                        PostBiaya = i.PostBiaya,
                        FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdam,
                        FlagDialihkanKevendor = i.FlagDialihkanKeVendor
                    });
                }
            }

            _viewModel.DetailRabPipaPersil = tempDetailPipaPersil;
            _viewModel.DetailRabPipaDistribusi = tempDetailPipaDistribusi;
            _viewModel.OnHitungRabCommand.Execute(null);
        }

        private void GetDetailRkpExist()
        {
            var tempDetailPipaPersil = new ObservableCollection<DetailRabWpf>();
            var tempDetailPipaDistribusi = new ObservableCollection<DetailRabWpf>();
            var selectedData = _viewModel.FormData;

            if (!string.IsNullOrWhiteSpace(selectedData.BeritaAcara?.PersilNamaPaket))
            {
                _viewModel.SelectedPaketRabPipaPersil = _viewModel.PaketRabList?.FirstOrDefault(c => c.NamaPaket == selectedData.BeritaAcara.PersilNamaPaket) ?? null;

                PaketRabPersilText.Text = _viewModel.SelectedPaketRabPipaPersil?.NamaPaket;
            }
            else
            {
                _viewModel.SelectedPaketRabPipaPersil = null;
                PaketRabPersilText.Visibility = Visibility.Collapsed;
                PaketRabPersil.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrWhiteSpace(selectedData.BeritaAcara?.DistribusiNamaPaket))
            {
                _viewModel.SelectedPaketRabPipaDistribusi = _viewModel.PaketRabList?.FirstOrDefault(c => c.NamaPaket == selectedData.BeritaAcara.DistribusiNamaPaket) ?? null;
                PaketRabDistribusiText.Text = _viewModel.SelectedPaketRabPipaDistribusi?.NamaPaket;
            }
            else
            {
                _viewModel.SelectedPaketRabPipaDistribusi = null;
                PaketRabDistribusiText.Visibility = Visibility.Collapsed;
                PaketRabDistribusi.Visibility = Visibility.Visible;
            }

            DialihkanKeVendorPipaPersil.IsChecked = selectedData.BeritaAcara?.PersilFlagDialihkanKeVendor == true;
            DialihkanKePDAMPipaPersil.IsChecked = selectedData.BeritaAcara?.PersilFlagBiayaDibebankanKePdam == true;

            DialihkanKeVendorPipaDistribusi.IsChecked = selectedData.BeritaAcara?.DistribusiFlagDialihkanKeVendor == true;
            DialihkanKePDAMPipaDistribusi.IsChecked = selectedData.BeritaAcara?.DistribusiFlagBiayaDibebankankePdam == true;

            if (selectedData.BeritaAcara?.DetailPersil != null)
            {
                foreach (var i in selectedData.BeritaAcara.DetailPersil)
                {
                    tempDetailPipaPersil.Add(new DetailRabWpf
                    {
                        Kode = i.Kode,
                        Tipe = i.Tipe,
                        Uraian = i.Uraian,
                        Satuan = i.Satuan,
                        HargaSatuan = i.HargaSatuan,
                        Qty = i.Qty,
                        Jumlah = i.Jumlah,
                        Ppn = i.Ppn,
                        Keuntungan = i.Keuntungan,
                        JasaDariBahan = i.JasaDariBahan,
                        Total = i.Total,
                        Kelompok = i.Kelompok,
                        PostBiaya = i.PostBiaya,
                        FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdam,
                        FlagDialihkanKevendor = i.FlagDialihkanKeVendor
                    });
                }
            }

            if (selectedData.BeritaAcara?.DetailDistribusi != null)
            {
                foreach (var i in selectedData.BeritaAcara.DetailDistribusi)
                {
                    tempDetailPipaDistribusi.Add(new DetailRabWpf
                    {
                        Kode = i.Kode,
                        Tipe = i.Tipe,
                        Uraian = i.Uraian,
                        Satuan = i.Satuan,
                        HargaSatuan = i.HargaSatuan,
                        Qty = i.Qty,
                        Jumlah = i.Jumlah,
                        Ppn = i.Ppn,
                        Keuntungan = i.Keuntungan,
                        JasaDariBahan = i.JasaDariBahan,
                        Total = i.Total,
                        Kelompok = i.Kelompok,
                        PostBiaya = i.PostBiaya,
                        FlagBiayaDibebankanKePdam = i.FlagBiayaDibebankanKePdam,
                        FlagDialihkanKevendor = i.FlagDialihkanKeVendor
                    });
                }
            }

            _viewModel.DetailRabPipaPersil = tempDetailPipaPersil;
            _viewModel.DetailRabPipaDistribusi = tempDetailPipaDistribusi;
            _viewModel.OnHitungRabCommand.Execute(null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void KodePair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SetupPegawai()
        {
            _viewModel.SelectedPegawai = new ObservableCollection<MasterPegawaiDto>();

            if (_viewModel.FormData?.Pelaksana != null)
            {
                _viewModel.FormData.Pelaksana?.ForEach(x =>
                {
                    _viewModel.SelectedPegawai.Add(_viewModel.PegawaiList?.FirstOrDefault(i => i.IdPegawai == x.IdPegawai));
                });

                PetugasComponentList.Add(FirstPegawai);
                var temp = _viewModel.SelectedPegawai?.FirstOrDefault();
                if (temp != null)
                {
                    var src = Petugas1.ItemsSource as ObservableCollection<MasterPegawaiDto>;
                    Petugas1.SelectedItem = _viewModel.PegawaiList.FirstOrDefault(x => x.IdPegawai == temp.IdPegawai);
                }
                if (_viewModel.SelectedPegawai?.Count > 1)
                {
                    var idx = -1;
                    foreach (var item in _viewModel.SelectedPegawai)
                    {
                        idx++;
                        if (idx == 0)
                            continue;
                        else
                        {
                            AppendFormElement(_totalForm, true, _viewModel.PegawaiList.FirstOrDefault(x => x.IdPegawai == item.IdPegawai));
                        }
                    }
                }
            }
        }

        private void AddPegawai_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppendFormElement(_totalForm, true);
                ContentForm.ScrollToBottom();
            }
            catch (Exception error)
            {
                Console.WriteLine("add btn: " + error);
            }
        }

        private void DeletePegawaiBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var current = sender as Button;
                var parent1 = (StackPanel)current.Parent;
                var parent2 = (StackPanel)parent1.Parent;
                var parent3 = (StackPanel)parent2.Parent;

                int index = parent3.Children.IndexOf(parent2);

                // delete in collection
                var optionList = (ComboBox)LogicalTreeHelper.FindLogicalNode(ContentWrapper.Children[index], $"OptionList");
                if (optionList != null && optionList.SelectedIndex >= 0)
                {
                    var currentData = SelectedPegawai[index];
                    var check = _viewModel.SelectedPegawai.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(currentData));

                    if (check != null)
                        _viewModel.SelectedPegawai.Remove(check);

                    SelectedPegawai.RemoveAt(index);
                }
                // end delete in collection
                PetugasComponentList.Remove(parent2);
                ContentWrapper.Children.RemoveAt(index);
                _totalForm--;

                // next form
                for (int i = index; i < ContentWrapper.Children.Count; i++)
                {
                    var title = (TextBlock)LogicalTreeHelper.FindLogicalNode(ContentWrapper.Children[i], "Title");
                    if (title != null)
                    {
                        title.Text = "Petugas Pelaksana " + (i + 2).ToString();
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("delete btn: " + error);
            }
        }

        private void AppendFormElement(int i, bool IsHidden = false, MasterPegawaiDto selectedItem = null)
        {
            var main = FindResource("PegawaiComponent") as StackPanel;
            _formElement = main;

            ContentWrapper.Children.Insert(i, _formElement);
            PetugasComponentList.Add(_formElement as StackPanel);
            var component = ContentWrapper.Children[i];

            // Assign Title
            var title = (TextBlock)LogicalTreeHelper.FindLogicalNode(component, "Title");
            if (title != null)
            {
                title.Text += " " + (i + 2).ToString();
            }

            // Assign Delete Button
            var delete = (Button)LogicalTreeHelper.FindLogicalNode(component, "DeletePegawaiBtn");
            if (delete != null)
            {
                delete.Tag += (i + 2).ToString();
                if (i + 1 > 1 && !IsHidden)
                    delete.Visibility = Visibility.Visible;
            }

            // Assign Option
            var option = (ComboBox)LogicalTreeHelper.FindLogicalNode(component, "OptionList");
            if (option != null)
            {
                option.Tag += (i + 2).ToString();
                if (_viewModel.PegawaiList != null)
                {
                    option.IsEnabled = true;
                    option.ItemsSource = _viewModel.PegawaiList;
                    option.DisplayMemberPath = "NamaPegawai";
                    option.SelectedItem = _viewModel.PegawaiList.FirstOrDefault(x => x.IdPegawai == selectedItem?.IdPegawai);
                }
            }

            _totalForm = i + 1;
        }

        private void OptionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var Element = (ComboBox)sender;

                if (Element.SelectedIndex >= 0)
                {
                    var Value = Element.Items.GetItemAt(Element.SelectedIndex);
                    var Result = Value.GetType().GetProperty("IdPegawai").GetValue(Value, null).ToString();

                    if (Result != null)
                    {
                        SelectedPegawai.Add(Convert.ToInt32(Result));
                        var check = _viewModel.SelectedPegawai.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(Result));
                        var current = _viewModel.PegawaiList.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(Result));

                        if (check == null && current != null)
                            _viewModel.SelectedPegawai.Add(current);
                    }

                    foreach (var item in e.RemovedItems)
                    {
                        var ID = item.GetType().GetProperty("IdPegawai").GetValue(item, null).ToString();

                        if (ID != null)
                        {
                            SelectedPegawai.Remove(Convert.ToInt32(ID));
                            var check = _viewModel.SelectedPegawai.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(ID));

                            if (check != null)
                                _viewModel.SelectedPegawai.Remove(check);
                        }
                    }

                    var parent = (Grid)Element.Parent;
                    var Overlay = (TextBlock)LogicalTreeHelper.FindLogicalNode(parent.Children[1], $"PlaceHolder");
                    if (Overlay != null)
                    {
                        Overlay.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("error change option: " + error);
            }
            CheckSubmitStatus();
        }
    }
}
