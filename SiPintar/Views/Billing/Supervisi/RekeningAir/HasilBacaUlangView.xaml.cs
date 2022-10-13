using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    public partial class HasilBacaUlangView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;

        public HasilBacaUlangView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            IsFotoStan1.IsChecked = true;
            IsFotoStan2.IsChecked = true;

            SetData("IsFotoStan1");
            SetData("IsFotoStan2");
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                CloseDialog();
        }

        private void MaxVideoButton_Click(object sender, RoutedEventArgs e)
        {
            var section = ((Button)sender).Tag.ToString();

            if (_viewModel.SelectedData == null)
                return;

            var Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            var Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

            string url = null;
            if (section == "Video2")
                url = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}VU.mp4");

            _viewModel.OnOpenLihatVideoCommand.Execute(url);
        }

        private void MaxFotoStanButton_Click(object sender, RoutedEventArgs e)
        {
            var section = ((Button)sender).Tag.ToString();

            if (_viewModel.SelectedData == null)
                return;

            var Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            var Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);
            if (section == "FotoStan2")
                _ = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}U.jpg");

            // _viewModel.OnOpenLihatFotoStanCommand.Execute(url);
        }

        private void MaxFotoRumahButton_Click(object sender, RoutedEventArgs e)
        {
            var section = ((Button)sender).Tag.ToString();

            if (_viewModel.SelectedData == null)
                return;

            var Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            var Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);
            if (section == "FotoRumah2")
                _ = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}RU.jpg");

            // _viewModel.OnOpenLihatFotoRumahCommand.Execute(url);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
            // _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitKonfirmasiHasilBacaUlangCommand).ExecuteAsync(null));
        }

        private void Batalkan_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
            // _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitPermintaanBacaUlangCommand).ExecuteAsync(null));
        }

        private void Section_Click(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            SetData(current);
        }

        private void SetData(string current)
        {
            switch (current)
            {
                case "IsFotoStan1":
                    SetFotoStan(_viewModel, 1);
                    break;
                case "IsFotoRumah1":
                    SetFotoRumah(_viewModel, 1);
                    break;
                case "IsVideo1":
                    SetVideo(_viewModel, 1);
                    break;
                case "IsFotoStan2":
                    SetFotoStan(_viewModel, 2);
                    break;
                case "IsFotoRumah2":
                    SetFotoRumah(_viewModel, 2);
                    break;
                case "IsVideo2":
                    SetVideo(_viewModel, 2);
                    break;
                default:
                    break;
            }

            if (current.Contains("1"))
            {
                BorderFotoStan1.Visibility = current == "IsFotoStan1" ? Visibility.Visible : Visibility.Collapsed;
                BorderFotoRumah1.Visibility = current == "IsFotoRumah1" ? Visibility.Visible : Visibility.Collapsed;
                BorderVideo1.Visibility = current == "IsVideo1" ? Visibility.Visible : Visibility.Collapsed;

                IsFotoStan1.IsChecked = current == "IsFotoStan1";
                IsFotoRumah1.IsChecked = current == "IsFotoRumah1";
                IsVideo1.IsChecked = current == "IsVideo1";
            }
            else
            {
                BorderFotoStan2.Visibility = current == "IsFotoStan2" ? Visibility.Visible : Visibility.Collapsed;
                BorderFotoRumah2.Visibility = current == "IsFotoRumah2" ? Visibility.Visible : Visibility.Collapsed;
                BorderVideo2.Visibility = current == "IsVideo2" ? Visibility.Visible : Visibility.Collapsed;

                IsFotoStan2.IsChecked = current == "IsFotoStan2";
                IsFotoRumah2.IsChecked = current == "IsFotoRumah2";
                IsVideo2.IsChecked = current == "IsVideo2";
            }
        }

        private void SetFotoStan(RekeningAirViewModel viewModel, int sequence)
        {
            try
            {
                bool condition;
                if (sequence == 1)
                    condition = viewModel.SelectedData != null && viewModel.SelectedData.AdaFotoMeter == true;
                else
                    condition = viewModel.SelectedData != null;

                if (condition)
                {
                    var Bulan = viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    var Tahun = viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var urlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{viewModel.SelectedData.NoSamb}.jpg");
                    if (sequence == 2)
                        urlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{viewModel.SelectedData.NoSamb}U.jpg");

                    // viewModel.GetFotoStanCommand.Execute(urlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR IMAGE: " + e);
            }
        }

        private void SetFotoRumah(RekeningAirViewModel viewModel, int sequence)
        {
            try
            {
                bool condition;
                if (sequence == 1)
                    condition = viewModel.SelectedData != null && viewModel.SelectedData.AdaFotoRumah == true;
                else
                    condition = viewModel.SelectedData != null;

                if (condition)
                {
                    var Bulan = viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    var Tahun = viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var urlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{viewModel.SelectedData.NoSamb}R.jpg");
                    if (sequence == 2)
                        urlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{viewModel.SelectedData.NoSamb}RU.jpg");

                    // viewModel.GetFotoRumahCommand.Execute(urlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR IMAGE: " + e);
            }
        }

        private void SetVideo(RekeningAirViewModel viewModel, int sequence)
        {
            try
            {
                bool condition;
                if (sequence == 1)
                    condition = viewModel.SelectedData != null && viewModel.SelectedData.AdaVideo == true;
                else
                    condition = viewModel.SelectedData != null;

                if (condition)
                {
                    var Bulan = viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    var Tahun = viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var urlFoto = Path.Combine(Bulan + Tahun, $"{viewModel.SelectedData.NoSamb}V.mp4");
                    if (sequence == 2)
                        urlFoto = Path.Combine(Bulan + Tahun, $"{viewModel.SelectedData.NoSamb}VU.mp4");

                    viewModel.GetVideoCommand.Execute(urlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR VIDEO: " + e);
            }
        }

        private void CloseDialog()
        {
            if (_viewModel != null)
                _viewModel.IsOpenBacaUlang = false;

            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
