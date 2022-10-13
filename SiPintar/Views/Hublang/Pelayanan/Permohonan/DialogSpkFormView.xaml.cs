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
using System.Windows.Threading;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class DialogSpkFormView : UserControl
    {
        private readonly PermohonanViewModel _viewModel;

        private bool _isMapInitiated;
        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;
        private readonly IRestApiClientModel _restApi;
        private readonly string _urlFotoPermohonan;
        private int _totalForm;
        private UIElement _formElement;
        private readonly List<int> SelectedPegawai = new List<int>();
        private readonly List<StackPanel> PetugasComponentList = new List<StackPanel>();
        private PermohonanWpf _tempSelected;

        public DialogSpkFormView(object dataContext, IRestApiClientModel restApi)
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

            GroupSPK.IsChecked = true;

            NomorBa.Visibility = _viewModel.IsBeritaAcara ? Visibility.Visible : Visibility.Collapsed;
            TanggalBa.Visibility = _viewModel.IsBeritaAcara ? Visibility.Visible : Visibility.Collapsed;
            TanggalSpk.Visibility = _viewModel.IsBeritaAcara ? Visibility.Collapsed : Visibility.Visible;
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

            if (_viewModel.IsPelangganAir)
            {
                _urlFotoPermohonan = "permohonan-pelanggan-air-link-foto";
                ColumnNoSamb.Visibility = Visibility.Visible;
                ColumnGolongan.Visibility = Visibility.Visible;
            }

            if (_viewModel.IsPelangganLimbah)
            {
                _urlFotoPermohonan = "permohonan-pelanggan-limbah-link-foto";
                ColumnNomorLimbah.Visibility = Visibility.Visible;
                ColumnTarifLimbah.Visibility = Visibility.Visible;
            }

            if (_viewModel.IsPelangganLltt)
            {
                _urlFotoPermohonan = "permohonan-pelanggan-lltt-link-foto";
                ColumnNomorLltt.Visibility = Visibility.Visible;
                ColumnTarifLltt.Visibility = Visibility.Visible;
            }

            if (_viewModel.IsNonPelanggan)
            {
                _urlFotoPermohonan = "permohonan-non-pelanggan-link-foto";
            }

            SetupFirst();
            SetupPegawai();
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

        private void Keterangan_PreviewKeyUp(object sender, KeyEventArgs e) => CheckSubmitStatus();

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
                {"includeRabDetail", false},
                {"statusPermohonanText", "Menunggu SPK"},
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
            _tempSelected.SPK = new PermohonanSpkDto();
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
            }

            _viewModel.IsCariPelangganForm = false;

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

            #region jika edit atau detail biar ambil map dan foto dari permohonan

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

            #endregion

            InitializeFotoDanMap();
            SetupEdit();
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
            TglSpk.SelectedDate = _viewModel.FormData.SPK.TanggalSpk ?? DateTime.Now;
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

        private void ChildDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => CheckSubmitStatus();

        private void ChildInput_PreviewKeyUp(object sender, KeyEventArgs e) => CheckSubmitStatus();

        private void ChildInput_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckSubmitStatus();

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            _viewModel.FormSpk = new ParamPermohonanSpkDto();

            if (_viewModel.IsFor == "edit")
            {
                _viewModel.FormSpk.IdPermohonan = _viewModel.SelectedData.IdPermohonan;
            }
            else
            {
                _viewModel.FormSpk.IdPermohonan = _viewModel.SelectedDataPermohonan.IdPermohonan;
            }

            _viewModel.FormSpk.TanggalSpk = TglSpk.SelectedDate?.AddMilliseconds(DateTime.Now.TimeOfDay.TotalMilliseconds) ?? DateTime.Now;

            var petugas = new List<ParamPetugasSpkDto>();
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
                    petugas.Add(new ParamPetugasSpkDto() { IdPegawai = ((MasterPegawaiDto)option.SelectedItem).IdPegawai });
                }
                counter++;
            }

            _viewModel.FormSpk.Petugas = petugas;
            _viewModel.FormSpk.IdUser = Convert.ToInt32(Application.Current.Resources["IdUser"]);

            _viewModel.FormSpk.Sppb = new List<ParamPermohonanSpkSPPBDto>();

            #region add to parameter body
            if (_viewModel.FormSpk != null)
            {
                var data = _viewModel.FormSpk;
                var type = typeof(ParamPermohonanSpkDto);
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
                _ = ((AsyncCommandBase)_viewModel.OnSubmitSpkFormCommand).ExecuteAsync(body);
            }
        }

        private void CheckSubmitStatus()
        {
            var status = true;
            foreach (var item in _viewModel.SelectedTipePermohonan.DetailParameter)
            {
                foreach (StackPanel elem in Canvas.Children)
                {
                    var childLabel = elem.Children[0] as TextBlock;
                    if (item.Parameter == childLabel.Text)
                    {
                        if (item.IsRequired.HasValue && item.IsRequired.Value)
                        {
                            if (item.IdListData == null && item.TipeData != "bool")
                            {
                                if (item.TipeData == "date")
                                {
                                    var childBorder = elem.Children[1] as Border;
                                    var childGrid = childBorder.Child as Grid;
                                    var childDatePicker = childGrid.Children[0] as DatePicker;
                                    var valueSelected = childDatePicker.SelectedDate;
                                    if (!valueSelected.HasValue)
                                        status = false;
                                }
                                else
                                {
                                    if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki" &&
                                        (item.Parameter.ToLower().Trim() == "estimasi waktu pengiriman" || item.Parameter.ToLower().Trim() == "waktu penagihan"))
                                    {
                                        var childGrid = elem.Children[1] as Grid;
                                        var childInput = childGrid.Children[0] as ComboBox;
                                        if (childInput.SelectedItem == null)
                                            status = false;
                                    }
                                    else
                                    {
                                        var childGrid = elem.Children[1] as Grid;
                                        var childInput = childGrid.Children[0] as TextBox;
                                        var valueSelected = childInput.Text;
                                        if (string.IsNullOrWhiteSpace(valueSelected))
                                            status = false;
                                    }
                                }
                            }
                            else
                            {
                                var childGrid = elem.Children[1] as Grid;
                                var childInput = childGrid.Children[0] as ComboBox;
                                dynamic valueSelected = childInput.SelectedItem;
                                if (valueSelected == null)
                                    status = false;
                            }
                        }
                    }
                }
            }

            foreach (var item in _viewModel.SelectedTipePermohonan.DetailBiaya)
            {
                foreach (StackPanel elem in Canvas.Children)
                {
                    var childLabel = elem.Children[0] as TextBlock;
                    if (item.Parameter == childLabel.Text)
                    {
                        if (item.IsLocked.HasValue && !item.IsLocked.Value)
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[0] as TextBox;
                            var valueSelected = childInput.Text;
                            if (string.IsNullOrWhiteSpace(valueSelected))
                                status = false;
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(Keterangan.Text))
                status = false;

            BtnSubmit.IsEnabled = status;
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

        private void BtnOnBrowseFileBuktiSpk1Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_buktispk1");
            CheckSubmitStatus();
        }

        private void BtnOnBrowseFileBuktiSpk2Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_buktispk2");
            CheckSubmitStatus();
        }

        private void BtnOnBrowseFileBuktiSpk3Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_buktispk3");
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

        private void SetupPegawai()
        {
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

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            SPKSection.Visibility = current == "GroupSPK" ? Visibility.Visible : Visibility.Collapsed;
            PermohonanSection.Visibility = current == "GroupPermohonan" ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
