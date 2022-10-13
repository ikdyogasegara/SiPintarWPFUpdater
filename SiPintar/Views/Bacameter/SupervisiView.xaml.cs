using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter
{
    public partial class SupervisiView : UserControl
    {
        private readonly bool _isMapInitiated;
        private string _mediaSection = "stan";

        public SupervisiView()
        {
            InitializeComponent();
            DataContextChanged += SupervisiView_DataContextChanged;

            ShowFilter_Click();

            IsFotoStan.IsChecked = true;

            if (!_isMapInitiated)
            {
                _ = Task.Run(() => MapConfigHelper.Initiate(MainMap));
                _isMapInitiated = true;
            }
        }

        private void SupervisiView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var Vm = DataContext as SupervisiViewModel;
            if (Vm != null)
            {
                PreviewKeyUp += new KeyEventHandler(HandleKeyboard);
            }
        }

        private void UpdateDataGrid()
        {
            var vm = DataContext as SupervisiViewModel;
            if (vm != null)
            {
                var saveList = vm.RekeningList;
                var saveId = vm.SelectedData?.IdPelangganAir;
                vm.SelectedData = null;
                vm.RekeningList = null;
                vm.RekeningList = saveList;
                vm.SelectedData = vm.RekeningList.FirstOrDefault(x => x.IdPelangganAir == saveId);
                DataGridContent.ScrollIntoView(vm.SelectedData);
            }
        }

        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            var _viewModel = (SupervisiViewModel)DataGridContent.DataContext;
            var SelectedData = _viewModel.SelectedData;

            // Ctrl + ...
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.F: // Ctrl + F: show/hide filter panel
                        if (FilterSection.Visibility == Visibility.Visible)
                            HideFilter_Click();
                        else
                            ShowFilter_Click();
                        break;
                    case Key.G: // Ctrl + G: table column setting
                        _viewModel.OnOpenSettingTableFormCommand.Execute(null);
                        break;
                    case Key.E: // Ctrl + E: export

                        break;
                    case Key.P: // Ctrl + P: print (satuan pelanggan)

                        break;
                    case Key.Return: // Ctrl + Enter: lihat detail pelanggan
                        if (SelectedData != null)
                            _viewModel.OnOpenDetailPelangganCommand.Execute(null);
                        break;
                    case Key.Delete: // Ctrl + Delete: hapus hasil baca

                        break;
                    case Key.D1: // Ctrl + 1: koreksi hasil baca
                    case Key.D2: // Ctrl + 2: koreksi m3 sekarang
                    case Key.NumPad1: // Ctrl + 1: koreksi hasil baca (Numpad Keyboard)
                    case Key.NumPad2: // Ctrl + 2: koreksi m3 sekarang (Numpad Keyboard)
                        if (SelectedData != null)
                            _viewModel.OnOpenKoreksiDialogCommand.Execute(null);
                        break;
                    default:
                        break;
                }
                return;
            }

            switch (e.Key)
            {
                case Key.F2:
                    if (!SelectedData.IsVerifikasi)
                        _viewModel.OnOpenVerifikasiDialogCommand.Execute(null);
                    break;
                case Key.F4:
                    if (!SelectedData.IsUnverifikasi)
                        _viewModel.OnOpenUnverifikasiDialogCommand.Execute(null);
                    break;
                default:
                    break;
            }

            if (e.SystemKey == Key.F10) // F10: isi lampiran
            {
                _viewModel.OnOpenLampiranDialogCommand.Execute(null);
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("ExportMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (SupervisiViewModel)DataGridContent.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridContent },
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }

        private void HideFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(195);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        #region GMAP CONTROL
        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap, false, 16);
        }

        private void ZoomIn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainMap.Zoom < MainMap.MaxZoom)
                MainMap.Zoom++;
        }

        private void ZoomOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainMap.Zoom > MainMap.MinZoom)
                MainMap.Zoom--;
        }
        #endregion

        private void BtnBulanIni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBulanLalu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {

        }

        // BUKTI BACA STAN

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Section_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (SupervisiViewModel)DataGridContent.DataContext;
            var current = ((RadioButton)sender).Name;

            switch (current)
            {
                case "IsFotoStan":
                    SetFotoStan(_viewModel);
                    _mediaSection = "stan";
                    break;
                case "IsFotoRumah":
                    SetFotoRumah(_viewModel);
                    _mediaSection = "rumah";
                    break;
                case "IsVideo":
                    SetVideo(_viewModel);
                    _mediaSection = "video";
                    break;
                default:
                    break;
            }

            BorderFotoStan.Visibility = current == "IsFotoStan" ? Visibility.Visible : Visibility.Collapsed;
            BorderFotoRumah.Visibility = current == "IsFotoRumah" ? Visibility.Visible : Visibility.Collapsed;
            BorderVideo.Visibility = current == "IsVideo" ? Visibility.Visible : Visibility.Collapsed;

            IsFotoStan.IsChecked = current == "IsFotoStan";
            IsFotoRumah.IsChecked = current == "IsFotoRumah";
            IsVideo.IsChecked = current == "IsVideo";
        }

        private void SetFotoStan(SupervisiViewModel viewModel)
        {
            try
            {
                if (viewModel.SelectedData != null && viewModel.SelectedData.AdaFotoMeter == true)
                {
                    string Bulan = viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    string Tahun = viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{viewModel.SelectedData.NoSamb}.jpg");
                    viewModel.GetFotoStanCommand.Execute(UrlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR IMAGE: " + e);
            }
        }

        private void SetFotoRumah(SupervisiViewModel viewModel)
        {
            try
            {
                if (viewModel.SelectedData != null && viewModel.SelectedData.AdaFotoRumah == true)
                {
                    string Bulan = viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    string Tahun = viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{viewModel.SelectedData.NoSamb}R.jpg");
                    viewModel.GetFotoRumahCommand.Execute(UrlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR IMAGE: " + e);
            }
        }

        private void SetVideo(SupervisiViewModel viewModel)
        {
            try
            {
                if (viewModel.SelectedData != null && viewModel.SelectedData.AdaVideo == true)
                {
                    string Bulan = viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    string Tahun = viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var UrlFoto = Path.Combine(Bulan + Tahun, $"{viewModel.SelectedData.NoSamb}V.mp4");
                    viewModel.GetVideoCommand.Execute(UrlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR VIDEO: " + e);
            }
        }

        // END BUKTI BACA STAN

        private void DetailPelanggan_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (SupervisiViewModel)DataGridContent.DataContext;

            if (!_viewModel.IsLoading)
            {
                _viewModel.OnOpenDetailPelangganCommand.Execute(true);
            }
        }

        private void DataGridContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mediaSection = "stan";

            var _viewModel = (SupervisiViewModel)DataGridContent.DataContext;

            _viewModel.SelectedIndex = DataGridContent.SelectedIndex;

            _viewModel.FileFotoStan = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
            _viewModel.FileFotoRumah = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
            _viewModel.FileVideo = null;

            BorderFotoStan.Visibility = _mediaSection == "stan" ? Visibility.Visible : Visibility.Collapsed;
            BorderFotoRumah.Visibility = _mediaSection == "rumah" ? Visibility.Visible : Visibility.Collapsed;
            BorderVideo.Visibility = _mediaSection == "video" ? Visibility.Visible : Visibility.Collapsed;

            IsFotoStan.IsChecked = _mediaSection == "stan";
            IsFotoRumah.IsChecked = _mediaSection == "rumah";
            IsVideo.IsChecked = _mediaSection == "video";

            if (_viewModel.SelectedData != null && _viewModel.SelectedData.Latitude != null && _viewModel.SelectedData.Longitude != null)
            {
                var MarkerList = new List<MapMarkerObject>()
                {
                    new MapMarkerObject()
                    {
                        MarkerInformation = $"LOKASI PELANGGAN\n{_viewModel.SelectedData.Nama}",
                        Latitude = _viewModel.SelectedData.Latitude,
                        Longitude = _viewModel.SelectedData.Longitude
                    }
                };

                MapConfigHelper.SetMarker(MainMap, MarkerList);
            }
        }

        private void DataGridContent_Loaded(object sender, RoutedEventArgs e)
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(DataGridContent, UpdatedSource);
            }
        }

        private void UpdatedSource(object sender, EventArgs e)
        {
            var viewModel = (SupervisiViewModel)DataGridContent.DataContext;

            if (viewModel != null && viewModel.SelectedIndex >= 0)
            {
                DataGridContent.SelectedIndex = viewModel.SelectedIndex;
                DataGridContent.ScrollIntoView(DataGridContent.SelectedItem);
            }
        }
    }
}
