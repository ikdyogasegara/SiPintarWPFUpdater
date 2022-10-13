using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class KoreksiHasilBacaView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;
        private bool _isProccess;
        private readonly bool? _oldCheckTaksasi;

        public KoreksiHasilBacaView(object dataContext)
        {
            _isProccess = true;
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

            _oldCheckTaksasi = _viewModel.KoreksiForm.Taksasi;

            if (_viewModel.IsTaksasiPemakaian == true)
            {
                Title.Text = "Taksasi Pemakaian";
                CekTaksasi.IsEnabled = false;
                CekStanLalu.IsEnabled = false;
                CekStanKini.IsEnabled = false;
                CekStanAngkat.IsEnabled = false;
                CekPakai.IsEnabled = false;
                _viewModel.IsPakaiChecked = true;
                _viewModel.IsStanKiniChecked = false;
                _viewModel.IsStanLaluChecked = false;
                _viewModel.IsStanAngkatChecked = false;
            }
            else
            {
                Title.Text = "Koreksi Hasil Baca";
                CekTaksasi.IsEnabled = true;
                CekStanLalu.IsEnabled = true;
                CekStanKini.IsEnabled = true;
                CekStanAngkat.IsEnabled = true;
                CekPakai.IsEnabled = true;
                _viewModel.IsPakaiChecked = false;
                _viewModel.IsStanKiniChecked = true;
                _viewModel.IsStanLaluChecked = false;
                _viewModel.IsStanAngkatChecked = false;
            }

            if (_viewModel.KoreksiForm.StanAngkat > 0)
            {
                CekStanAngkat.IsEnabled = true;
                _viewModel.IsStanAngkatChecked = true;
            }

            StanSkrg.TextChanged += new TextBoxHelper().SelectAllFirstFocusTextChanged;
            StanLalu.Text = _viewModel.KoreksiForm.StanLalu.ToString();
            StanSkrg.Text = _viewModel.KoreksiForm.StanSkrg.ToString();
            StanAngkat.Text = _viewModel.KoreksiForm.StanAngkat.ToString();
            Pakai.Text = _viewModel.KoreksiForm.Pakai.ToString();

            _viewModel.KoreksiSelectedKelainan = _viewModel.KelainanList?.FirstOrDefault(i => i.Kelainan == _viewModel.KoreksiForm.Kelainan);
            _viewModel.IsKelainanChecked = _viewModel.KoreksiSelectedKelainan != null;
            _viewModel.KoreksiSelectedPetugasBaca = _viewModel.PetugasBacaList?.FirstOrDefault(i => i.PetugasBaca == _viewModel.KoreksiForm.PetugasBaca);
            _viewModel.IsPetugasBacaChecked = _viewModel.KoreksiSelectedPetugasBaca != null;

            CheckButton();
            PreviewKeyUp += new KeyEventHandler(HandleKey);
            InputValidation();
            _isProccess = false;
            StanSkrg.Focus();
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

            _viewModel.KoreksiForm.PetugasBaca = CekPetugasBaca.IsChecked == true && !string.IsNullOrWhiteSpace(PetugasBaca.Text) ? PetugasBaca.Text : "";
            _viewModel.KoreksiForm.Kelainan = CekKelainan.IsChecked == true && !string.IsNullOrWhiteSpace(Kelainan.Text) ? Kelainan.Text : "";
            _viewModel.KoreksiForm.Taksasi = CekTaksasi.IsChecked == true;

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
            _ = ((AsyncCommandBase)_viewModel.OnSubmitKoreksiCommand).ExecuteAsync(body);

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

            // if (_viewModel.KoreksiForm.Taksir == true)
            //     TaksirButton.IsEnabled = false;
            // else
            //     TaksirButton.IsEnabled = true;
        }

        private void InputValidation()
        {
            StanSkrg.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            StanSkrg.GotFocus += DecimalValidationHelper.Object_GotFocus;
            StanSkrg.LostFocus += DecimalValidationHelper.Object_LostFocus;

            StanLalu.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            StanLalu.GotFocus += DecimalValidationHelper.Object_GotFocus;
            StanLalu.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Pakai.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Pakai.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Pakai.LostFocus += DecimalValidationHelper.Object_LostFocus;

            StanAngkat.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            StanAngkat.GotFocus += DecimalValidationHelper.Object_GotFocus;
            StanAngkat.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        public void HitungStan(int param)
        {
            _isProccess = true;
            var stanlalu = Convert.ToInt32(StanLalu.Text.Replace(".", ""));
            var stanskrg = Convert.ToInt32(StanSkrg.Text.Replace(".", ""));
            var stanAngkat = Convert.ToInt32(StanAngkat.Text.Replace(".", ""));
            var pakai = Convert.ToInt32(Pakai.Text.Replace(".", ""));

            if (CekTaksasi.IsChecked == false)
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
                        pakai = stanAngkat - stanlalu + stanskrg;
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
        }

        private void CekTaksasi_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.KoreksiForm.Taksasi = true;
            if (_oldCheckTaksasi != _viewModel.KoreksiForm.Taksasi)
            {
                CekTaksasi.IsEnabled = false;
            }
        }

        private void CekTaksasi_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.KoreksiForm.Taksasi = false;
            if (CekPakai.IsChecked == true)
            {
                if (!_isProccess)
                    HitungStan(2);
            }
            else
            {
                if (!_isProccess)
                    HitungStan(1);
            }
        }

        private void StanLalu_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CekStanLalu.IsChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                if (!_isProccess)
                    HitungStan(1);
            }
        }

        private void StanSkrg_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CekStanKini.IsChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                if (!_isProccess)
                    HitungStan(1);
            }
        }

        private void StanAngkat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CekStanAngkat.IsChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                                                          && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                if (!_isProccess)
                    HitungStan(1);
            }
        }

        private void Pakai_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CekPakai.IsChecked == true && !string.IsNullOrWhiteSpace(StanLalu.Text) && !string.IsNullOrWhiteSpace(StanSkrg.Text)
                                                          && !string.IsNullOrWhiteSpace(StanAngkat.Text) && !string.IsNullOrWhiteSpace(Pakai.Text))
            {
                if (!_isProccess)
                    HitungStan(2);

                if (Convert.ToInt32(StanLalu.Text) > Convert.ToInt32(StanSkrg.Text) && CekTaksasi.IsChecked == false)
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

            KalkulasiRekening();
        }

        private void KalkulasiRekening()
        {
            _ = ((AsyncCommandBase)_viewModel.OnCalCulationKoreksiCommand).ExecuteAsync(null);

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
    }
}
