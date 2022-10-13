using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class HasilBacaUlangView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;

        public HasilBacaUlangView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

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
            var Section = ((Button)sender).Tag.ToString();

            if (_viewModel.SelectedData == null)
                return;

            string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

            string Url = null;
            if (Section == "Video2")
                Url = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}VU.mp4");

            _viewModel.OnOpenLihatVideoCommand.Execute(Url);
        }

        private void MaxFotoStanButton_Click(object sender, RoutedEventArgs e)
        {
            var Section = ((Button)sender).Tag.ToString();

            if (_viewModel.SelectedData == null)
                return;

            string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

            string Url = null;
            if (Section == "FotoStan2")
                Url = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}U.jpg");

            _viewModel.OnOpenLihatFotoStanCommand.Execute(Url);
        }

        private void MaxFotoRumahButton_Click(object sender, RoutedEventArgs e)
        {
            var Section = ((Button)sender).Tag.ToString();

            if (_viewModel.SelectedData == null)
                return;

            string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
            string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

            string Url = null;
            if (Section == "FotoRumah2")
                Url = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}RU.jpg");

            _viewModel.OnOpenLihatFotoRumahCommand.Execute(Url);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitKonfirmasiHasilBacaUlangCommand).ExecuteAsync(null));
        }

        private void Batalkan_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitPermintaanBacaUlangCommand).ExecuteAsync(null));
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

        private void SetFotoStan(SupervisiViewModel _viewModel, int Sequence)
        {
            try
            {
                bool Condition;
                if (Sequence == 1)
                    Condition = _viewModel.SelectedData != null && _viewModel.SelectedData.AdaFotoMeter == true;
                else
                    Condition = _viewModel.SelectedData != null;

                if (Condition)
                {
                    string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}.jpg");
                    if (Sequence == 2)
                        UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}U.jpg");

                    _viewModel.GetFotoStanCommand.Execute(UrlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR IMAGE: " + e);
            }
        }

        private void SetFotoRumah(SupervisiViewModel _viewModel, int Sequence)
        {
            try
            {
                bool Condition;
                if (Sequence == 1)
                    Condition = _viewModel.SelectedData != null && _viewModel.SelectedData.AdaFotoRumah == true;
                else
                    Condition = _viewModel.SelectedData != null;

                if (Condition)
                {
                    string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}R.jpg");
                    if (Sequence == 2)
                        UrlFoto = Path.Combine(Bulan + Tahun, ".thumbnails", $"{_viewModel.SelectedData.NoSamb}RU.jpg");

                    _viewModel.GetFotoRumahCommand.Execute(UrlFoto);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR IMAGE: " + e);
            }
        }

        private void SetVideo(SupervisiViewModel _viewModel, int Sequence)
        {
            try
            {
                bool Condition;
                if (Sequence == 1)
                    Condition = _viewModel.SelectedData != null && _viewModel.SelectedData.AdaVideo == true;
                else
                    Condition = _viewModel.SelectedData != null;

                if (Condition)
                {
                    string Bulan = _viewModel.SelectedData.KodePeriode?.ToString().Substring(4);
                    string Tahun = _viewModel.SelectedData.KodePeriode?.ToString().Substring(2, 2);

                    var UrlFoto = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}V.mp4");
                    if (Sequence == 2)
                        UrlFoto = Path.Combine(Bulan + Tahun, $"{_viewModel.SelectedData.NoSamb}VU.mp4");

                    _viewModel.GetVideoCommand.Execute(UrlFoto);
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
