using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class DialogUsulanFormView : UserControl
    {
        private readonly PermohonanViewModel _viewModel;

        private bool _isMapInitiated;
        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;

        public DialogUsulanFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PermohonanViewModel;
            DataContext = _viewModel;

            if (_viewModel.IsFor == "add")
                Title.Text = $"Tambah {_viewModel.FiturName} {_viewModel.SelectedTipePermohonan.NamaTipePermohonan} {_viewModel.SelectedJenisPelanggan}";
            else if (_viewModel.IsFor == "edit")
                Title.Text = $"Koreksi {_viewModel.FiturName} {_viewModel.SelectedTipePermohonan.NamaTipePermohonan} {_viewModel.SelectedJenisPelanggan}";
            else
                Title.Text = $"Detail {_viewModel.FiturName} {_viewModel.SelectedTipePermohonan.NamaTipePermohonan} {_viewModel.SelectedJenisPelanggan}";

            Title2.Text = _viewModel.SelectedTipePermohonan.NamaTipePermohonan;

            FotoBeritaAcara.Visibility = _viewModel.IsBeritaAcara ? Visibility.Visible : Visibility.Collapsed;
            NomorBa.Visibility = _viewModel.IsBeritaAcara ? Visibility.Visible : Visibility.Collapsed;
            TanggalBa.Visibility = _viewModel.IsBeritaAcara ? Visibility.Visible : Visibility.Collapsed;
            TanggalPermohonan.Visibility = _viewModel.IsBeritaAcara ? Visibility.Collapsed : Visibility.Visible;
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
                ColumnNoSamb.Visibility = Visibility.Visible;
                ColumnGolongan.Visibility = Visibility.Visible;
            }
            if (_viewModel.IsPelangganLimbah)
            {
                ColumnNomorLimbah.Visibility = Visibility.Visible;
                ColumnTarifLimbah.Visibility = Visibility.Visible;
            }
            if (_viewModel.IsPelangganLltt)
            {
                ColumnNomorLltt.Visibility = Visibility.Visible;
                ColumnTarifLltt.Visibility = Visibility.Visible;
            }

            TglPermohonan.SelectedDate = DateTime.Now;

            SetupFirst();

            if (_viewModel.IsFor == "edit" || _viewModel.IsFor == "detail")
            {
                HeaderAddForm.Visibility = Visibility.Collapsed;
                HeaderEditForm.Visibility = Visibility.Visible;
                BtnSubmit.Content = "Simpan";
                BtnSubmit.Width = 100;
                SetupEdit();
                DataGridPelanggan_MouseDoubleClick(null, null);
            }
            else if (_viewModel.IsFor == "add")
            {
                HeaderAddForm.Visibility = Visibility.Visible;
                HeaderEditForm.Visibility = Visibility.Collapsed;
                BtnSubmit.Content = "Simpan";
                BtnSubmit.Width = 100;

                if (_viewModel.IsNonPelanggan)
                {
                    _viewModel.FormData = new PermohonanWpf();
                    FormSetNonPelanggan.Visibility = Visibility.Visible;
                    BtnToForm.Visibility = Visibility.Visible;
                    FormCariPelanggan.Visibility = Visibility.Collapsed;
                }
                else
                {
                    FormSetNonPelanggan.Visibility = Visibility.Collapsed;
                    FormCariPelanggan.Visibility = Visibility.Visible;
                }

                DataGridPelanggan_MouseDoubleClick(null, null);
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
                    {"currentPage", _viewModel.CurrentPagePelanggan}
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
                    param.Add("NomorLimbah", NomorSambungan.Text);
                }

                if (_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan == "SAMBUNG_KEMBALI")
                {
                    param.Add("ForSambungKembali", true);
                }

                if (_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan == "TUTUP_SEMENTARA")
                {
                    param.Add("ForTutupSementara", true);
                }

                if (_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan == "TUTUP_TOTAL")
                {
                    param.Add("ForTutupTotal", true);
                }

                _ = ((AsyncCommandBase)_viewModel.OnCariPelangganCommand).ExecuteAsync(param);
            }
        }

        private void DataGridPelanggan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.SelectedDataPelanggan != null)
            {
                Loading.Visibility = Visibility.Visible;
                _ = Task.Run(async () =>
                {
                    var data = await _viewModel.GetDataPiutangAsync(_viewModel.SelectedDataPelanggan.IdPelangganAir);
                    AppDispatcherHelper.Run(false, () =>
                    {
                        _viewModel.DaftarTagihanPiutang = data;
                        Loading.Visibility = Visibility.Collapsed;
                    });
                });

                _viewModel.IsCariPelangganForm = false;

                FormInputData.Visibility = Visibility.Visible;

                if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
                {
                    BtnHitung.Visibility = Visibility.Visible;
                }
                else
                {
                    BtnSubmit.Visibility = Visibility.Visible;
                }

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

                if (_viewModel.IsFor == "add" && _viewModel.IsNonPelanggan == false)
                {
                    BtnBack.Visibility = Visibility.Visible;
                    Tips.Visibility = Visibility.Collapsed;
                }

                scrollBar.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                _viewModel.FormData = JsonConvert.DeserializeObject<PermohonanWpf>(JsonConvert.SerializeObject(_viewModel.SelectedDataPelanggan));



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

                    if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.AlamatMapWpf))
                    {
                        _viewModel.FormData.AlamatMap = _viewModel.SelectedData.AlamatMapWpf;
                    }

                    if (_viewModel.IsBeritaAcara)
                    {
                        if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.BeritaAcara.FotoBukti1))
                        {
                            _viewModel.FormData.FotoBuktiBeritaAcara1 = _viewModel.SelectedData.BeritaAcara.FotoBukti1;
                        }

                        if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.BeritaAcara.FotoBukti2))
                        {
                            _viewModel.FormData.FotoBuktiBeritaAcara2 = _viewModel.SelectedData.BeritaAcara.FotoBukti2;
                        }

                        if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.BeritaAcara.FotoBukti3))
                        {
                            _viewModel.FormData.FotoBuktiBeritaAcara3 = _viewModel.SelectedData.BeritaAcara.FotoBukti3;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.FotoBukti1))
                        {
                            _viewModel.FormData.FotoBukti1 = _viewModel.SelectedData.FotoBukti1;
                        }

                        if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.FotoBukti2))
                        {
                            _viewModel.FormData.FotoBukti2 = _viewModel.SelectedData.FotoBukti2;
                        }

                        if (!string.IsNullOrWhiteSpace(_viewModel.SelectedData.FotoBukti3))
                        {
                            _viewModel.FormData.FotoBukti3 = _viewModel.SelectedData.FotoBukti3;
                        }
                    }
                }
                #endregion

                InitializeFotoDanMap();
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
            viewModel.IsDenahLokasiFormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude) && !string.IsNullOrWhiteSpace(viewModel.FormData.Latitude);

            if (_viewModel.IsFor == "detail")
            {
                if (!viewModel.IsFotoBukti1FormChecked)
                {
                    FotoPermohonan1.Visibility = Visibility.Collapsed;
                }

                if (!viewModel.IsFotoBukti2FormChecked)
                {
                    FotoPermohonan2.Visibility = Visibility.Collapsed;
                }

                if (!viewModel.IsFotoBukti3FormChecked)
                {
                    FotoPermohonan3.Visibility = Visibility.Collapsed;
                }

                if (!viewModel.IsDenahLokasiFormChecked)
                {
                    TitikLokasi.Visibility = Visibility.Collapsed;
                }

                if (!viewModel.IsFotoBukti1FormChecked && !viewModel.IsFotoBukti2FormChecked && !viewModel.IsFotoBukti3FormChecked && !viewModel.IsDenahLokasiFormChecked)
                {
                    FotodanMapSection.Visibility = Visibility.Collapsed;
                }
            }

            viewModel.IsFotoBuktiBeritaAcara1FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiBeritaAcara1);
            viewModel.IsFotoBuktiBeritaAcara2FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiBeritaAcara2);
            viewModel.IsFotoBuktiBeritaAcara3FormChecked = !string.IsNullOrWhiteSpace(viewModel.FormData.FotoBuktiBeritaAcara3);

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
            Keterangan.Text = _viewModel.SelectedData.Keterangan;
            TglPermohonan.SelectedDate = _viewModel.SelectedData.WaktuPermohonan;

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
                                var obj = _viewModel.SelectedData.Parameter.Find(x => x.Parameter == item.Parameter);
                                if (obj != null && !string.IsNullOrWhiteSpace((string)obj.Value))
                                {
                                    childDatePicker.SelectedDate = Convert.ToDateTime(obj.Value, CultureInfo.InvariantCulture);
                                }
                            }
                            else if (item.TipeData == "int")
                            {
                                var childGrid = elem.Children[1] as Grid;
                                var childInput = childGrid.Children[0] as TextBox;
                                var obj = _viewModel.SelectedData.Parameter.Find(x => x.Parameter == item.Parameter);
                                if (obj != null)
                                    childInput.Text = IntegerValidationHelper.FormatIntegerToStringId(obj.Value is null ? 0 : Convert.ToInt32(obj.Value));
                            }
                            else if (item.TipeData == "decimal")
                            {
                                var childGrid = elem.Children[1] as Grid;
                                var childInput = childGrid.Children[0] as TextBox;
                                var obj = _viewModel.SelectedData.Parameter.Find(x => x.Parameter == item.Parameter);
                                if (obj != null)
                                    childInput.Text = DecimalValidationHelper.FormatDecimalToStringId(obj.Value is null ? decimal.Zero : Convert.ToDecimal(obj.Value));
                            }
                            else
                            {
                                if (_viewModel.SelectedData.NamaTipePermohonan.ToLower().Trim() == "air tangki" &&
                                    (item.Parameter.ToLower().Trim() == "estimasi waktu pengiriman" ||
                                     item.Parameter.ToLower().Trim() == "waktu penagihan"))
                                {
                                    var childGrid = elem.Children[1] as Grid;
                                    var childInput = childGrid.Children[0] as ComboBox;
                                    var obj = _viewModel.SelectedData.Parameter.Find(x => x.Parameter == item.Parameter);
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
                                    var obj = _viewModel.SelectedData.Parameter.Find(x => x.Parameter == item.Parameter);
                                    if (obj != null)
                                        childInput.Text = Convert.ToString(obj.Value);
                                }
                            }
                        }
                        else
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[0] as ComboBox;
                            var obj = _viewModel.SelectedData.Parameter.Find(x => x.Parameter == item.Parameter);
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
                            var obj = _viewModel.SelectedData.NonAirReguler?.Detail.FirstOrDefault(x => x.Parameter == item.Parameter);
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

            if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
            {
                BtnHitung_Click(BtnHitung, null);
            }

        }

        private void ChildInput_BiayaChanged(object sender, TextChangedEventArgs e)
        {
            decimal biaya = 0;
            decimal ppn, total;
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

            ppn = _viewModel.SelectedTipePermohonan.PersentasePpn / 100 * biaya;
            total = biaya + ppn;
            Biaya.Text = biaya.ToString("#,##0.##", new CultureInfo("id-ID"));
            Ppn.Text = ppn.ToString("#,##0.##", new CultureInfo("id-ID"));
            Total.Text = total.ToString("#,##0.##", new CultureInfo("id-ID"));
        }

        private void ChildDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => CheckSubmitStatus();

        private void ChildInput_PreviewKeyUp(object sender, KeyEventArgs e) => CheckSubmitStatus();

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckBtnToFormStatus();

        private void ChildInput_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckSubmitStatus();

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsFor == "edit")
            {
                _viewModel.FormData.IdPermohonan = _viewModel.SelectedData.IdPermohonan;
            }

            _viewModel.FormData.WaktuPermohonan = TglPermohonan.SelectedDate?.AddMilliseconds(DateTime.Now.TimeOfDay.TotalMilliseconds) ?? DateTime.Now;
            _viewModel.FormData.IdTipePermohonan = _viewModel.SelectedTipePermohonan.IdTipePermohonan;
            _viewModel.FormData.Keterangan = Keterangan.Text;
            _viewModel.FormData.IdUser = Convert.ToInt32(Application.Current.Resources["IdUser"].ToString());

            if (_viewModel.IsNonPelanggan)
            {
                _viewModel.FormData.Nama = NamaNonPelanggan.Text;
                _viewModel.FormData.Alamat = AlamatNonPelanggan.Text;
                _viewModel.FormData.Rt = RtNonPelanggan.Text;
                _viewModel.FormData.Rw = RwNonPelanggan.Text;
                _viewModel.FormData.NoHp = HpNonPelanggan.Text;
                _viewModel.FormData.NoTelp = TelpNonPelanggan.Text;
                _viewModel.FormData.Email = EmailNonPelanggan.Text;
                _viewModel.FormData.IdKelurahan = _viewModel.KelurahanForm?.IdKelurahan;
                _viewModel.FormData.IdRayon = _viewModel.RayonForm?.IdRayon;

                if (_viewModel.IsAlamatMapFormChecked)
                {
                    _viewModel.FormData.Alamat = _viewModel.FormData.AlamatMap;
                }
            }

            _viewModel.FormDataDetailParameter = new List<ParamPermohonanParameterDto>();
            _viewModel.FormDataDetailBiaya = new List<ParamPermohonanBiayaDto>();
            _viewModel.FormData.FlagUsulan = true;

            #region extrak Detail Parameter
            foreach (var item in _viewModel.SelectedTipePermohonan.DetailParameter)
            {
                foreach (StackPanel elem in Canvas.Children)
                {
                    var childLabel = elem.Children[0] as TextBlock;

                    if (childLabel != null && item.Parameter == childLabel.Text)
                    {
                        if (item.IdListData == null && item.TipeData != "bool")
                        {
                            if (item.TipeData == "date")
                            {
                                var childBorder = elem.Children[1] as Border;
                                var childGrid = childBorder.Child as Grid;
                                var childDatePicker = childGrid.Children[0] as DatePicker;
                                var valueSelected = childDatePicker.SelectedDate;
                                _viewModel.FormDataDetailParameter.Add(new ParamPermohonanParameterDto() { Parameter = item.Parameter, TipeData = item.TipeData, Value = valueSelected.Value.ToString("yyyy-MM-dd HH:mm:ss"), InfoValue = valueSelected.Value.ToString("yyyy-MM-dd") });
                            }
                            else
                            {
                                if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki" && (item.Parameter.ToLower().Trim() == "estimasi waktu pengiriman" || item.Parameter.ToLower().Trim() == "waktu penagihan"))
                                {
                                    var childGrid = elem.Children[1] as Grid;
                                    var childInput = childGrid.Children[0] as ComboBox;
                                    dynamic valueSelected = childInput.SelectedItem;
                                    _viewModel.FormDataDetailParameter.Add(new ParamPermohonanParameterDto() { Parameter = item.Parameter, TipeData = item.TipeData, Value = valueSelected.Key, InfoValue = valueSelected.Key });
                                }
                                else
                                {
                                    var childGrid = elem.Children[1] as Grid;
                                    var childInput = childGrid.Children[0] as TextBox;
                                    var valueSelected = childInput.Text;
                                    _viewModel.FormDataDetailParameter.Add(new ParamPermohonanParameterDto()
                                    {
                                        Parameter = item.Parameter,
                                        TipeData = item.TipeData,
                                        Value = item.TipeData switch
                                        {
                                            "int" => IntegerValidationHelper.FormatStringIdToInteger(valueSelected),
                                            "decimal" => DecimalValidationHelper.FormatStringIdToDecimal(valueSelected),
                                            _ => valueSelected,
                                        },
                                        InfoValue = valueSelected
                                    });
                                }
                            }
                        }
                        else
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[0] as ComboBox;
                            dynamic valueSelected = childInput.SelectedItem;
                            _viewModel.FormDataDetailParameter.Add(new ParamPermohonanParameterDto() { Parameter = item.Parameter, TipeData = item.TipeData, Value = valueSelected.Key, InfoValue = childInput.Text });
                        }
                    }
                }
            }
            #endregion

            #region extrak Detail Biaya

            if (_viewModel.SelectedTipePermohonanComboBox.KodeTipePermohonan != "AIR_TANGKI")
            {
                foreach (var item in _viewModel.SelectedTipePermohonan.DetailBiaya)
                {
                    foreach (StackPanel elem in Canvas.Children)
                    {
                        var childLabel = elem.Children[0] as TextBlock;
                        if (item.Parameter == childLabel.Text)
                        {
                            var childGrid = elem.Children[1] as Grid;
                            var childInput = childGrid.Children[0] as TextBox;
                            var valueSelected = childInput.Text;
                            _viewModel.FormDataDetailBiaya.Add(new ParamPermohonanBiayaDto() { Parameter = item.Parameter, PostBiaya = item.PostBiaya, Value = DecimalValidationHelper.FormatStringIdToDecimal(valueSelected) });
                        }
                    }
                }
            }
            else
            {
                _viewModel.FormDataDetailBiaya.Add(
                new ParamPermohonanBiayaDto()
                {
                    Parameter = "Biaya Air",
                    PostBiaya = "tangki",
                    Value = DecimalValidationHelper.FormatStringIdToDecimal(AirTangkiBiayaAir.Text)
                }
                );

                _viewModel.FormDataDetailBiaya.Add(
                    new ParamPermohonanBiayaDto()
                    {
                        Parameter = "Biaya Transport",
                        PostBiaya = "tangki",
                        Value = DecimalValidationHelper.FormatStringIdToDecimal(AirTangkiTransport.Text)
                    }
                );

                _viewModel.FormDataDetailBiaya.Add(
                    new ParamPermohonanBiayaDto()
                    {
                        Parameter = "Biaya Administrasi",
                        PostBiaya = "tangki",
                        Value = DecimalValidationHelper.FormatStringIdToDecimal(AirTangkiAdministrasi.Text)
                    }
                );

                _viewModel.FormDataDetailBiaya.Add(
                    new ParamPermohonanBiayaDto()
                    {
                        Parameter = "Biaya Operasional",
                        PostBiaya = "tangki",
                        Value = DecimalValidationHelper.FormatStringIdToDecimal(AirTangkiOperasional.Text)
                    }
                );

                _viewModel.FormDataDetailBiaya.Add(
                    new ParamPermohonanBiayaDto()
                    {
                        Parameter = "PPN",
                        PostBiaya = "tangki",
                        Value = DecimalValidationHelper.FormatStringIdToDecimal(AirTangkiPpn.Text)
                    }
                );
            }

            #endregion

            #region add to parameter body
            var param = JsonConvert.DeserializeObject<ParamPermohonanGlobalDto>(JsonConvert.SerializeObject(_viewModel.FormData));
            if (param != null)
            {
                param.DetailParameter = _viewModel.FormDataDetailParameter;
                param.DetailBiaya = _viewModel.FormDataDetailBiaya;
                param.IdJenisNonAir = _viewModel.SelectedTipePermohonan.IdJenisNonAir;

                var data = param;
                var type = typeof(ParamPermohonanGlobalDto);
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
                _ = ((AsyncCommandBase)_viewModel.OnSubmitUsulanFormCommand).ExecuteAsync(body);
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
            BtnHitung.IsEnabled = status;

            if (_viewModel.SelectedTipePermohonan.NamaTipePermohonan.ToLower().Trim() == "air tangki")
            {
                BtnSubmit.Visibility = Visibility.Collapsed;
                BtnHitung.Visibility = Visibility.Visible;
            }
        }

        private void CheckBtnToFormStatus()
        {
            if (!string.IsNullOrWhiteSpace(NamaNonPelanggan.Text) && !string.IsNullOrWhiteSpace(AlamatNonPelanggan.Text) && !string.IsNullOrWhiteSpace(HpNonPelanggan.Text))
            {
                BtnToForm.IsEnabled = true;
            }
            else
            {
                BtnToForm.IsEnabled = false;
            }
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (FormSetNonPelanggan.Visibility == Visibility.Visible)
            {
                _viewModel.SelectedDataPelanggan = new MasterPelangganGlobalWpf();
                _viewModel.SelectedDataPelanggan.Nama = NamaNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.Alamat = AlamatNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.Rt = RtNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.Rw = RwNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.NoHp = HpNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.NoTelp = TelpNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.Email = EmailNonPelanggan.Text;
                _viewModel.SelectedDataPelanggan.IdKelurahan = _viewModel.KelurahanForm?.IdKelurahan;
                _viewModel.SelectedDataPelanggan.NamaKelurahan = _viewModel.KelurahanForm?.NamaKelurahan;
                _viewModel.SelectedDataPelanggan.NamaKecamatan = _viewModel.KecamatanList.FirstOrDefault(c => c.IdKecamatan == _viewModel.KelurahanForm?.IdKecamatan)?.NamaKecamatan;
                _viewModel.SelectedDataPelanggan.IdRayon = _viewModel.RayonForm?.IdRayon;
                _viewModel.SelectedDataPelanggan.NamaRayon = _viewModel.RayonForm?.NamaRayon;
                _viewModel.SelectedDataPelanggan.NamaWilayah = _viewModel.WilayahList.FirstOrDefault(c => c.IdWilayah == _viewModel.RayonForm?.IdWilayah)?.NamaWilayah;
                IsAlamatMap.Visibility = Visibility.Visible;
                BtnToForm.Visibility = Visibility.Collapsed;
            };

            DataGridPelanggan_MouseDoubleClick(null, null);
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

        private void BtnHitung_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.IsEnabled = false;
            _ = HitungBiayaAsync(btn);
        }

        private void BtnOnBrowseFileBukti1Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti1");
            CheckSubmitStatus();
        }

        private void BtnOnBrowseFileBukti2Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti2");
            CheckSubmitStatus();
        }

        private void BtnOnBrowseFileBukti3Command_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti3");
            CheckSubmitStatus();
        }

        private async Task HitungBiayaAsync(Button btn)
        {
            //calculate here
            int? idTarifAirTangki = null;
            var tarif = _viewModel.DataParamPermohonan.FirstOrDefault(x => x.Name.ToLower().Trim() == "tarif air tangki");
            foreach (StackPanel elem in Canvas.Children)
            {
                var childLabel = elem.Children[0] as TextBlock;
                if (childLabel.Text.ToLower().Trim() == "tarif air tangki")
                {
                    var childGrid = elem.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as ComboBox;
                    var valueSelected = childInput.SelectedItem as ParamPermohonanData;
                    idTarifAirTangki = Convert.ToInt32(valueSelected?.Key);
                }
            }

            var param = new Dictionary<string, dynamic>();
            param.Add("idTarifTangki", idTarifAirTangki);
            foreach (StackPanel elem in Canvas.Children)
            {
                var childLabel = elem.Children[0] as TextBlock;
                if (childLabel.Text.ToLower().Trim() == "jumlah m3")
                {
                    var childGrid = elem.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as TextBox;
                    var valueSelected = childInput.Text;
                    param.Add("jumlahm3", DecimalValidationHelper.FormatStringIdToDecimal(valueSelected));
                }

                if (childLabel.Text.ToLower().Trim() == "jarak tujuan (km)")
                {
                    var childGrid = elem.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as TextBox;
                    var valueSelected = childInput.Text;
                    param.Add("jarakKm", DecimalValidationHelper.FormatStringIdToDecimal(valueSelected));
                }

                if (childLabel.Text.ToLower().Trim() == "jumlah armada tangki air")
                {
                    var childGrid = elem.Children[1] as Grid;
                    var childInput = childGrid.Children[0] as TextBox;
                    var valueSelected = childInput.Text;
                    param.Add("jumlahArmada", IntegerValidationHelper.FormatStringIdToInteger(valueSelected));
                }
            }

            await ((AsyncCommandBase)_viewModel.OnHitungTarifAirTangkiCommand).ExecuteAsync(param);

            AirTangkiBiayaAir.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.BiayaAir);
            AirTangkiTransport.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.BiayaTransport);
            AirTangkiAdministrasi.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.BiayaAdministrasi);
            AirTangkiOperasional.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.BiayaOperasional);
            AirTangkiBiayatotal.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.BiayaTotal);
            AirTangkiPpn.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.Ppn);
            Total.Text = DecimalValidationHelper.FormatDecimalToStringId(_viewModel.HasilHitungRumus?.Total);
            BtnSubmit.Visibility = Visibility.Visible;
            BtnHitung.Visibility = Visibility.Collapsed;
            btn.IsEnabled = true;
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

        private void Lokasi_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click();
        }

        private void Search_Click(object sender = null, RoutedEventArgs e = null)
        {
            var viewModel = (PermohonanViewModel)DataContext;

            if (string.IsNullOrEmpty(Lokasi.Text) || viewModel == null)
                return;

            _ = SearchAddressAsync(viewModel);
        }

        private async Task SearchAddressAsync(PermohonanViewModel viewModel)
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
            if (DataContext is PermohonanViewModel viewModel)
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
            var viewModel = (PermohonanViewModel)DataContext;
            viewModel.FormData.Latitude = _currentLat;
            viewModel.FormData.Longitude = _currentLon;
            // viewModel.FormData.AlamatMap = _currentAlamatMap;
        }

        #endregion

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
