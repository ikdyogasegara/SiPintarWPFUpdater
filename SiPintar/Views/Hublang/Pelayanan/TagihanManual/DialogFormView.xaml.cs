using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.TagihanManual
{
    public partial class DialogFormView : UserControl
    {
        private readonly TagihanManualViewModel _viewModel;
        private bool _selectPelanggan;

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as TagihanManualViewModel;
            DataContext = _viewModel;

            Title.Text = $"{(_viewModel != null && _viewModel.IsEdit ? "Koreksi" : "Tambah")} Tagihan Non Air";
            BtnSubmit.Content = "Simpan";

            _viewModel.PiutangNonAirList = null;
            JenisPelanggan.IsEnabled = true;
            JenisNonAir.IsEnabled = true;
            NomorPelanggan.IsEnabled = false;

            if (_viewModel.IsEdit)
            {
                ContainerTipePelanggan.Visibility = Visibility.Visible;
                ContainerJenisPelanggan.Visibility = Visibility.Collapsed;
                TipePelanggan.Text = _viewModel.SelectedData.JenisTipePelanggan;

                _viewModel.KelurahanForm = _viewModel.KelurahanList.FirstOrDefault(c => c.IdKelurahan == _viewModel.SelectedData.IdKelurahan);
                _viewModel.RayonForm = _viewModel.RayonList.FirstOrDefault(c => c.IdRayon == _viewModel.SelectedData.IdRayon);
                _viewModel.GolonganForm = _viewModel.GolonganList.FirstOrDefault(c => c.IdGolongan == _viewModel.SelectedData.IdGolongan);
                _viewModel.TarifLimbahForm = _viewModel.TarifLimbahList.FirstOrDefault(c => c.IdTarifLimbah == _viewModel.SelectedData.IdTarifLimbah);
                _viewModel.TarifLlttForm = _viewModel.TarifLlttList.FirstOrDefault(c => c.IdTarifLltt == _viewModel.SelectedData.IdTarifLltt);
                JenisNonAir.SelectedItem = _viewModel.JenisNonAirList.FirstOrDefault(c => c.IdJenisNonAir == _viewModel.SelectedData.IdJenisNonAir);
                _viewModel.JenisPelangganSelected = _viewModel.JenisPelangganList.FirstOrDefault(c => c.NamaJenisPelanggan == _viewModel.SelectedData.JenisPelanggan);

                BtnSubmit.Width = 100;
                JenisPelanggan.IsEnabled = false;

                JenisNonAir.IsHitTestVisible = false;
                JenisNonAir.Focusable = false;

                GolonganGroup.IsHitTestVisible = false;
                GolonganGroup.Focusable = false;

                if (_viewModel.SelectedData.JenisTipePelanggan != "Non Pelanggan")
                {
                    Nama.IsHitTestVisible = false;
                    Nama.Focusable = false;

                    Alamat.IsHitTestVisible = false;
                    Alamat.Focusable = false;

                    KelurahanGroup.IsHitTestVisible = false;
                    KelurahanGroup.Focusable = false;

                    RayonGroup.IsHitTestVisible = false;
                    RayonGroup.Focusable = false;
                }
            }
            else
            {
                ContainerTipePelanggan.Visibility = Visibility.Collapsed;
                ContainerJenisPelanggan.Visibility = Visibility.Visible;

                _viewModel.JenisNonAirDetailList = null;

                if (_viewModel.SelectedPelanggan != null)
                {
                    if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
                    {
                        var temp = _viewModel.SelectedPelanggan as MasterPelangganAirDto;
                        SetupPelangganAir(temp);
                    }
                    if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
                    {
                        var temp = _viewModel.SelectedPelanggan as MasterPelangganLimbahDto;
                        SetupPelangganLimbah(temp);
                    }
                    if (_viewModel.SelectedPelanggan is MasterPelangganLlttDto)
                    {
                        var temp = _viewModel.SelectedPelanggan as MasterPelangganLlttDto;
                        SetupPelangganLltt(temp);
                    }
                }
            }

            CheckForm();
            SetupPiutangNonAir();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            Unloaded += DialogFormView_Unloaded;
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;
            FormulirSection.Visibility = current == "Formulir" ? Visibility.Visible : Visibility.Collapsed;
            PiutangSection.Visibility = current == "Piutang" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SetupPiutangNonAir()
        {
            _viewModel.PiutangNonAirList = null;
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadPiutangNonairCommand).ExecuteAsync(null));
        }

        private void SetupPelangganAir(MasterPelangganAirDto param)
        {
            _viewModel.FormData.NomorPelanggan = param.NoSamb;
            _viewModel.FormData.Nama = param.Nama;
            _viewModel.FormData.Alamat = param.Alamat;
            _viewModel.KelurahanForm = _viewModel.KelurahanList.FirstOrDefault(x => x.IdKelurahan == param.IdKelurahan);
            _viewModel.RayonForm = _viewModel.RayonList.FirstOrDefault(x => x.IdRayon == param.IdRayon);
            _viewModel.GolonganForm = _viewModel.GolonganList.FirstOrDefault(x => x.IdGolongan == param.IdGolongan);

            Nama.IsHitTestVisible = false;
            Nama.Focusable = false;

            Alamat.IsHitTestVisible = false;
            Alamat.Focusable = false;

            KelurahanGroup.IsHitTestVisible = false;
            KelurahanGroup.Focusable = false;

            RayonGroup.IsHitTestVisible = false;
            RayonGroup.Focusable = false;

            GolonganGroup.IsHitTestVisible = false;
            GolonganGroup.Focusable = false;
        }

        private void SetupPelangganLimbah(MasterPelangganLimbahDto param)
        {
            _viewModel.FormData.NomorPelanggan = param.NomorLimbah;
            _viewModel.FormData.Nama = param.Nama;
            _viewModel.FormData.Alamat = param.Alamat;
            _viewModel.KelurahanForm = _viewModel.KelurahanList.FirstOrDefault(x => x.IdKelurahan == param.IdKelurahan);
            _viewModel.RayonForm = _viewModel.RayonList.FirstOrDefault(x => x.IdRayon == param.IdRayon);
            _viewModel.TarifLimbahForm = _viewModel.TarifLimbahList.FirstOrDefault(x => x.IdTarifLimbah == param.IdTarifLimbah);

            Nama.IsHitTestVisible = false;
            Nama.Focusable = false;

            Alamat.IsHitTestVisible = false;
            Alamat.Focusable = false;

            KelurahanGroup.IsHitTestVisible = false;
            KelurahanGroup.Focusable = false;

            RayonGroup.IsHitTestVisible = false;
            RayonGroup.Focusable = false;
            GolonganGroup.IsHitTestVisible = false;
            GolonganGroup.Focusable = false;

        }

        private void SetupPelangganLltt(MasterPelangganLlttDto param)
        {
            _viewModel.FormData.NomorPelanggan = param.NomorLltt;
            _viewModel.FormData.Nama = param.Nama;
            _viewModel.FormData.Alamat = param.Alamat;
            _viewModel.KelurahanForm = _viewModel.KelurahanList.FirstOrDefault(x => x.IdKelurahan == param.IdKelurahan);
            _viewModel.RayonForm = _viewModel.RayonList.FirstOrDefault(x => x.IdRayon == param.IdRayon);
            _viewModel.TarifLlttForm = _viewModel.TarifLlttList.FirstOrDefault(x => x.IdTarifLltt == param.IdTarifLltt);

            Nama.IsHitTestVisible = false;
            Nama.Focusable = false;

            Alamat.IsHitTestVisible = false;
            Alamat.Focusable = false;

            KelurahanGroup.IsHitTestVisible = false;
            KelurahanGroup.Focusable = false;

            RayonGroup.IsHitTestVisible = false;
            RayonGroup.Focusable = false;

            GolonganGroup.IsHitTestVisible = false;
            GolonganGroup.Focusable = false;
        }

        private void DialogFormView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_selectPelanggan)
            {
                _ = _viewModel.OpenSelectPelangganAsync();
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckForm();

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            _ = new RekeningNonAirDto();

            if (_viewModel.IsEdit)
            {
                _viewModel.FormData.IdNonAir = _viewModel.SelectedData.IdNonAir;
                _viewModel.FormData.IdPdam = _viewModel.SelectedData.IdPdam;
            }

            _viewModel.FormData.Nama = Nama.Text;
            _viewModel.FormData.Alamat = Alamat.Text;
            _viewModel.FormData.Keterangan = Keterangan.Text;
            _viewModel.FormData.NomorPelanggan = NomorPelanggan.Text;
            _viewModel.FormData.FlagAngsur = false;
            _viewModel.FormData.FlagManual = true;

            _viewModel.FormData.JenisPelanggan = JenisPelanggan.SelectedItem is JenisPelangganDto jenisPelanggan ? jenisPelanggan.NamaJenisPelanggan : null;
            _viewModel.FormData.IdJenisNonAir = JenisNonAir.SelectedItem is MasterJenisNonAirDto nonAir ? nonAir.IdJenisNonAir : null;
            _viewModel.FormData.IdGolongan = _viewModel.GolonganForm != null ? _viewModel.GolonganForm.IdGolongan : null;
            _viewModel.FormData.IdTarifLimbah = _viewModel.TarifLimbahForm != null ? _viewModel.TarifLimbahForm.IdTarifLimbah : null;
            _viewModel.FormData.IdTarifLltt = _viewModel.TarifLlttForm != null ? _viewModel.TarifLlttForm.IdTarifLltt : null;
            _viewModel.FormData.IdRayon = _viewModel.RayonForm != null ? _viewModel.RayonForm.IdRayon : null;
            _viewModel.FormData.IdKelurahan = _viewModel.KelurahanForm != null ? _viewModel.KelurahanForm.IdKelurahan : null;
            _viewModel.FormDataDetail = new ObservableCollection<RekeningNonAirDetailDto>();

            foreach (var item in _viewModel.JenisNonAirDetailList)
            {
                foreach (StackPanel elem in Canvas.Children)
                {
                    var childLabel = elem.Children[0] as TextBlock;
                    if (item.Parameter == childLabel.Text)
                    {
                        var childGrid = elem.Children[1] as Grid;
                        var childInput = childGrid.Children[0] as TextBox;
                        var valueSelected = childInput.Text;
                        _viewModel.FormDataDetail.Add(new RekeningNonAirDetailDto()
                        {
                            Parameter = item.Parameter,
                            PostBiaya = item.PostBiaya,
                            Value = DecimalValidationHelper.FormatStringIdToDecimal(valueSelected)
                        });
                    }
                }
            }

            _viewModel.FormData.TanggalMulaiTagih = DateTime.Now;
            _viewModel.FormData.Total = _viewModel.FormDataDetail.Count > 0 ? _viewModel.FormDataDetail.Sum(c => c.Value) : 0;
            _viewModel.OnSubmitFormCommand.Execute(null);
        }

        private void CheckForm()
        {
            BtnSubmit.IsEnabled = true;

            if (((JenisPelangganDto)JenisPelanggan.SelectedItem) != null)
            {
                if (((JenisPelangganDto)JenisPelanggan.SelectedItem).NamaJenisPelanggan == "Non Pelanggan")
                {
                    NomorPelangganGroup.Visibility = Visibility.Collapsed;
                    TarifContainer.Visibility = Visibility.Collapsed;

                    Nama.IsHitTestVisible = true;
                    Nama.Focusable = true;

                    Alamat.IsHitTestVisible = true;
                    Alamat.Focusable = true;

                    KelurahanGroup.IsHitTestVisible = true;
                    KelurahanGroup.Focusable = true;

                    RayonGroup.IsHitTestVisible = true;
                    RayonGroup.Focusable = true;
                }
                else
                {
                    NomorPelangganGroup.Visibility = Visibility.Visible;
                    TarifContainer.Visibility = Visibility.Visible;

                    Nama.IsHitTestVisible = false;
                    Nama.Focusable = false;

                    Alamat.IsHitTestVisible = false;
                    Alamat.Focusable = false;

                    KelurahanGroup.IsHitTestVisible = false;
                    KelurahanGroup.Focusable = false;

                    RayonGroup.IsHitTestVisible = false;
                    RayonGroup.Focusable = false;

                    GolonganGroup.IsHitTestVisible = false;
                    GolonganGroup.Focusable = false;
                }
            }

            GolonganGroup.Visibility = Visibility.Collapsed;
            TarifLimbahGroup.Visibility = Visibility.Collapsed;
            TarifLlttGroup.Visibility = Visibility.Collapsed;

            if (_viewModel.IsEdit)
            {
                if (_viewModel.SelectedData.IdPelangganAir != null)
                {
                    GolonganGroup.Visibility = Visibility.Visible;
                }
                else if (_viewModel.SelectedData.IdPelangganLimbah != null)
                {
                    TarifLimbahGroup.Visibility = Visibility.Visible;
                }
                else if (_viewModel.SelectedData.IdPelangganLltt != null)
                {
                    TarifLlttGroup.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (_viewModel.SelectedPelanggan != null)
                {
                    if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
                    {
                        GolonganGroup.Visibility = Visibility.Visible;
                    }
                    if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
                    {
                        TarifLimbahGroup.Visibility = Visibility.Visible;
                    }
                    if (_viewModel.SelectedPelanggan is MasterPelangganLlttDto)
                    {
                        TarifLlttGroup.Visibility = Visibility.Visible;
                    }

                }
            }

            if (string.IsNullOrWhiteSpace(Nama.Text))
            {
                BtnSubmit.IsEnabled = false;
            }
            if (string.IsNullOrWhiteSpace(Alamat.Text))
            {
                BtnSubmit.IsEnabled = false;
            }

            if (string.IsNullOrWhiteSpace(KodeKelurahan.Text))
            {
                BtnSubmit.IsEnabled = false;
            }
            if (string.IsNullOrWhiteSpace(KodeRayon.Text))
            {
                BtnSubmit.IsEnabled = false;
            }

            if (_viewModel.Total == 0)
            {
                BtnSubmit.IsEnabled = false;
            }

            if (_viewModel.SelectedPelanggan != null)
            {
                if (string.IsNullOrWhiteSpace(NomorPelanggan.Text))
                {
                    BtnSubmit.IsEnabled = false;
                }

                if (_viewModel.SelectedPelanggan is MasterPelangganAirDto)
                {
                    if (string.IsNullOrWhiteSpace(KodeGolongan.Text))
                    {
                        BtnSubmit.IsEnabled = false;
                    }
                }
                if (_viewModel.SelectedPelanggan is MasterPelangganLimbahDto)
                {
                    if (string.IsNullOrWhiteSpace(KodeTarifLimbah.Text))
                    {
                        BtnSubmit.IsEnabled = false;
                    }
                }
                if (_viewModel.SelectedPelanggan is MasterPelangganLlttDto)
                {
                    if (string.IsNullOrWhiteSpace(KodeTarifLltt.Text))
                    {
                        BtnSubmit.IsEnabled = false;
                    }
                }
            }
        }

        private void TagihanManual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel.IsEdit == false)
            {
                var obj = sender as ComboBox;
                var select = obj.SelectedItem as JenisPelangganDto;
                if (select != null && select.NamaJenisPelanggan == "Non Pelanggan")
                {
                    _viewModel.SelectedPelanggan = null;
                    _viewModel.FormData = new RekeningNonAirDto();
                    _viewModel.KelurahanForm = null;
                    _viewModel.RayonForm = null;
                    _viewModel.GolonganForm = null;
                    _viewModel.TarifLlttForm = null;
                    _viewModel.TarifLimbahForm = null;
                    _viewModel.PiutangNonAirList = null;
                }
            }

            CheckForm();
        }

        private void JenisNonAir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var nonAir = JenisNonAir.SelectedItem as MasterJenisNonAirDto;
            _viewModel.SelectedIdJenisNonAir = nonAir != null ? nonAir.IdJenisNonAir : null;
            _ = SetupAsync();
        }

        private void BtnCariPelanggan(object sender, RoutedEventArgs e)
        {
            _selectPelanggan = true;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private async Task SetupAsync()
        {
            Canvas.Children.Clear();
            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnLoadDetailJenisNonairCommand).ExecuteAsync(null));

            decimal biaya = 0;
            decimal ppn = 0;
            decimal total = 0;
            var idx = 0;

            StackPanel Temp;
            TextBlock ChildLabel;
            Grid ChildGrid;
            TextBox ChildInput;
            var tempBiaya = decimal.Zero;
            foreach (var defines in _viewModel.JenisNonAirDetailList)
            {
                Temp = Resources["TextBoxDecimalTemplate"] as StackPanel;
                ChildLabel = Temp.Children[0] as TextBlock;
                ChildLabel.Text = defines.Parameter;
                ChildGrid = Temp.Children[1] as Grid;
                ChildInput = ChildGrid.Children[0] as TextBox;
                ChildInput.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                ChildInput.GotFocus += DecimalValidationHelper.Object_GotFocus;
                ChildInput.LostFocus += DecimalValidationHelper.Object_LostFocus;
                ChildInput.TextChanged += ChildInput_BiayaChanged;
                ChildInput.Text = DecimalValidationHelper.FormatDecimalToStringId(defines.Biaya ?? decimal.Zero);
                ChildInput.IsEnabled = !(defines.IsLocked ?? false);
                tempBiaya = defines.Biaya ?? decimal.Zero;

                if (_viewModel.IsEdit)
                {
                    foreach (var item in _viewModel.FormDataDetail)
                    {
                        if (defines.Parameter == item.Parameter)
                        {
                            ChildInput.Text = DecimalValidationHelper.FormatDecimalToStringId(item.Value ?? decimal.Zero);
                            tempBiaya = item.Value ?? decimal.Zero;
                        }
                    }
                }

                Canvas.Children.Insert(idx, Temp);
                idx++;
                biaya += tempBiaya;
            }

            ppn = (decimal)0.0 * biaya;
            total = biaya + ppn;
            Biaya.Text = biaya.ToString("#,##0.##", new CultureInfo("id-ID"));
            Ppn.Text = ppn.ToString("#,##0.##", new CultureInfo("id-ID"));
            Total.Text = total.ToString("#,##0.##", CultureInfo.GetCultureInfo("id-ID"));
            _viewModel.Total = total;
            CheckForm();
        }

        private void ChildInput_BiayaChanged(object sender, TextChangedEventArgs e)
        {
            decimal biaya = 0;
            decimal ppn, total;
            foreach (var item in _viewModel.JenisNonAirDetailList)
            {
                if (item.IsLocked.HasValue && !item.IsLocked.Value)
                {
                    foreach (StackPanel elem in Canvas.Children)
                    {
                        var ChildLabel = elem.Children[0] as TextBlock;
                        if (ChildLabel.Text == item.Parameter)
                        {
                            var ChildGrid = elem.Children[1] as Grid;
                            var ChildInput = ChildGrid.Children[0] as TextBox;
                            biaya += DecimalValidationHelper.FormatStringIdToDecimal(ChildInput.Text);
                        }
                    }
                }
                else
                    biaya += item.Biaya ?? decimal.Zero;
            }
            ppn = (decimal)0.0 * biaya;
            total = biaya + ppn;
            Biaya.Text = biaya.ToString("#,##0.##", new CultureInfo("id-ID"));
            Ppn.Text = ppn.ToString("#,##0.##", new CultureInfo("id-ID"));
            Total.Text = total.ToString("#,##0.##", new CultureInfo("id-ID"));
            _viewModel.Total = total;

            CheckForm();
        }
    }
}
