using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public partial class StanMeterFormView : UserControl
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private bool _isProccess;
        private bool _isHanyaAbonemen;

        public StanMeterFormView(object dataContext)
        {
            _isProccess = true;
            InitializeComponent();
            _viewModel = dataContext as UsulanKoreksiViewModel;
            DataContext = _viewModel;

            CekStanLalu.IsEnabled = true;
            CekStanKini.IsEnabled = true;
            CekStanAngkat.IsEnabled = true;
            CekPakai.IsEnabled = true;
            _viewModel.IsPakaiChecked = false;
            _viewModel.IsStanKiniChecked = true;
            _viewModel.IsStanLaluChecked = false;
            _viewModel.IsStanAngkatChecked = false;

            if (_viewModel.SelectedPiutangAir.IsInputKoreksiWpf == true)
            {
                _viewModel.IsNewFotoBukti1 = _viewModel.SelectedPiutangAir.IsNewFotoBukti1;
                _viewModel.IsNewFotoBukti2 = _viewModel.SelectedPiutangAir.IsNewFotoBukti2;
                _viewModel.IsNewFotoBukti3 = _viewModel.SelectedPiutangAir.IsNewFotoBukti3;
                _viewModel.FotoBukti1Uri = _viewModel.SelectedPiutangAir.FotoBukti1Uri;
                _viewModel.FotoBukti2Uri = _viewModel.SelectedPiutangAir.FotoBukti2Uri;
                _viewModel.FotoBukti3Uri = _viewModel.SelectedPiutangAir.FotoBukti3Uri;
                _viewModel.IsFotoBukti1FormChecked = _viewModel.SelectedPiutangAir.FotoBukti1Uri != null;
                _viewModel.IsFotoBukti2FormChecked = _viewModel.SelectedPiutangAir.FotoBukti1Uri != null;
                _viewModel.IsFotoBukti3FormChecked = _viewModel.SelectedPiutangAir.FotoBukti1Uri != null;

                _viewModel.KoreksiForm = new RekeningAirPiutangWpf()
                {
                    StanLalu_Usulan = _viewModel.SelectedPiutangAir.StanLalu_Usulan,
                    StanSkrg_Usulan = _viewModel.SelectedPiutangAir.StanSkrg_Usulan,
                    StanAngkat_Usulan = _viewModel.SelectedPiutangAir.StanAngkat_Usulan,
                    Pakai_Usulan = _viewModel.SelectedPiutangAir.Pakai_Usulan,
                };

                StanLalu.Text = _viewModel.SelectedPiutangAir.StanLalu_Usulan.ToString();
                StanSkrg.Text = _viewModel.SelectedPiutangAir.StanSkrg_Usulan.ToString();
                StanAngkat.Text = _viewModel.SelectedPiutangAir.StanAngkat_Usulan.ToString();
                Pakai.Text = _viewModel.SelectedPiutangAir.Pakai_Usulan.ToString();
            }
            else
            {
                _viewModel.IsNewFotoBukti1 = false;
                _viewModel.IsNewFotoBukti2 = false;
                _viewModel.IsNewFotoBukti3 = false;
                _viewModel.FotoBukti1Uri = null;
                _viewModel.FotoBukti2Uri = null;
                _viewModel.FotoBukti3Uri = null;
                _viewModel.IsFotoBukti1FormChecked = false;
                _viewModel.IsFotoBukti2FormChecked = false;
                _viewModel.IsFotoBukti3FormChecked = false;

                _viewModel.KoreksiForm = new RekeningAirPiutangWpf()
                {
                    StanLalu_Usulan = _viewModel.SelectedPiutangAir.StanLalu,
                    StanSkrg_Usulan = _viewModel.SelectedPiutangAir.StanSkrg,
                    StanAngkat_Usulan = _viewModel.SelectedPiutangAir.StanAngkat,
                    Pakai_Usulan = _viewModel.SelectedPiutangAir.Pakai,
                };

                StanLalu.Text = _viewModel.SelectedPiutangAir.StanLalu.ToString();
                StanSkrg.Text = _viewModel.SelectedPiutangAir.StanSkrg.ToString();
                StanAngkat.Text = _viewModel.SelectedPiutangAir.StanAngkat.ToString();
                Pakai.Text = _viewModel.SelectedPiutangAir.Pakai.ToString();
            }

            CheckButton();
            InputValidation();
            _isProccess = false;
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
            }

            KalkulasiRekening();
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

            _viewModel.KoreksiForm = new RekeningAirPiutangWpf()
            {
                StanLalu_Usulan = stanlalu,
                StanSkrg_Usulan = stanskrg,
                StanAngkat_Usulan = stanAngkat,
                Pakai_Usulan = pakai,
                FlagHanyaAbonemen = _isHanyaAbonemen
            };

            if (CekStanLalu.IsChecked == false) StanLalu.Text = stanlalu.ToString();
            if (CekStanKini.IsChecked == false) StanSkrg.Text = stanskrg.ToString();
            if (CekStanAngkat.IsChecked == false) StanAngkat.Text = stanAngkat.ToString();
            if (CekPakai.IsChecked == false) Pakai.Text = pakai.ToString();

            _isProccess = false;

        }

        private void KalkulasiRekening()
        {
            _viewModel.HitungKoreksi = new RekeningAirDto();
            _viewModel.HitungKoreksiDetail = new RekeningAirDetailDto();

            var golongan = _viewModel.Parent.GolonganList.FirstOrDefault(c => c.IdGolongan == _viewModel.SelectedPiutangAir.IdGolongan);
            var diameter = _viewModel.Parent.DiameterList.FirstOrDefault(c => c.IdDiameter == _viewModel.SelectedPiutangAir.IdDiameter);
            var administrasiLain = _viewModel.Parent.AdministrasiLainList.FirstOrDefault(c => c.IdAdministrasiLain == _viewModel.SelectedPiutangAir.IdAdministrasiLain);
            var pemeliharaanLain = _viewModel.Parent.PemeliharaanLainList.FirstOrDefault(c => c.IdPemeliharaanLain == _viewModel.SelectedPiutangAir.IdPemeliharaanLain);
            var retribusiLain = _viewModel.Parent.RetribusiLainList.FirstOrDefault(c => c.IdRetribusiLain == _viewModel.SelectedPiutangAir.IdRetribusiLain);
            var meterai = _viewModel.Parent.MeteraiList.Where(c => c.KodePeriodeMulaiBerlaku <= _viewModel.SelectedPiutangAir.KodePeriode).OrderByDescending(c => c.KodePeriodeMulaiBerlaku).FirstOrDefault();
            var status = _viewModel.Parent.StatusList.FirstOrDefault(c => c.IdStatus == _viewModel.SelectedPiutangAir.IdStatus);

            var result = KalkulasiRekeningAirHelper.Hitung(
                _viewModel.KoreksiForm.Pakai_Usulan,
                golongan,
                diameter,
                administrasiLain,
                pemeliharaanLain,
                retribusiLain,
                meterai,
                status,
                _viewModel.SelectedPiutangAir.IdFlag,
                _viewModel.SelectedPiutangAir.TglMulaiDenda1,
                _viewModel.SelectedPiutangAir.TglMulaiDenda2,
                _viewModel.SelectedPiutangAir.TglMulaiDenda3,
                _viewModel.SelectedPiutangAir.TglMulaiDenda4,
                _viewModel.SelectedPiutangAir.TglMulaiDendaPerBulan,
                false,
                _isHanyaAbonemen);

            _viewModel.HitungKoreksi = new RekeningAirDto()
            {
                Pakai = result.Pakai,
                PakaiKalkulasi = result.PakaiKalkulasi,
                BiayaPemakaian = result.BiayaPemakaian,
                Administrasi = result.Administrasi,
                Pemeliharaan = result.Pemeliharaan,
                Retribusi = result.Retribusi,
                Pelayanan = result.Pelayanan,
                AirLimbah = result.AirLimbah,
                DendaPakai0 = result.DendaPakai0,
                AdministrasiLain = result.AdministrasiLain,
                PemeliharaanLain = result.PemeliharaanLain,
                RetribusiLain = result.RetribusiLain,
                Ppn = result.Ppn,
                Meterai = result.Meterai,
                Rekair = result.Rekair,
                Denda = result.Denda,
                Total = result.Total,
                FlagMinimumPakai = result.FlagMinimumPakai,
            };

            _viewModel.HitungKoreksiDetail = new RekeningAirDetailDto()
            {
                Blok1 = result.Blok1,
                Blok2 = result.Blok2,
                Blok3 = result.Blok3,
                Blok4 = result.Blok4,
                Blok5 = result.Blok5,
                Prog1 = result.Prog1,
                Prog2 = result.Prog2,
                Prog3 = result.Prog3,
                Prog4 = result.Prog4,
                Prog5 = result.Prog5,
            };
        }

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(StanLalu.Text) || string.IsNullOrEmpty(StanSkrg.Text) || string.IsNullOrEmpty(StanAngkat.Text) || string.IsNullOrEmpty(Pakai.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.PiutangAirList == null || _viewModel.SelectedPiutangAir == null || _viewModel.KoreksiForm == null || _viewModel.HitungKoreksi == null)
                return;

            _viewModel.SelectedPiutangAir.FlagHanyaAbonemenWpf = _viewModel.KoreksiForm.FlagHanyaAbonemen;
            _viewModel.SelectedPiutangAir.StanLalu_UsulanWpf = _viewModel.KoreksiForm.StanLalu_Usulan;
            _viewModel.SelectedPiutangAir.StanSkrg_UsulanWpf = _viewModel.KoreksiForm.StanSkrg_Usulan;
            _viewModel.SelectedPiutangAir.StanAngkat_UsulanWpf = _viewModel.KoreksiForm.StanAngkat_Usulan;
            _viewModel.SelectedPiutangAir.Pakai_UsulanWpf = _viewModel.KoreksiForm.Pakai_Usulan;
            _viewModel.SelectedPiutangAir.BiayaPemakaian_UsulanWpf = _viewModel.HitungKoreksi.BiayaPemakaian;
            _viewModel.SelectedPiutangAir.Administrasi_UsulanWpf = _viewModel.HitungKoreksi.Administrasi;
            _viewModel.SelectedPiutangAir.Pemeliharaan_UsulanWpf = _viewModel.HitungKoreksi.Pemeliharaan;
            _viewModel.SelectedPiutangAir.Retribusi_UsulanWpf = _viewModel.HitungKoreksi.Retribusi;
            _viewModel.SelectedPiutangAir.Pelayanan_UsulanWpf = _viewModel.HitungKoreksi.Pelayanan;
            _viewModel.SelectedPiutangAir.AirLimbah_UsulanWpf = _viewModel.HitungKoreksi.AirLimbah;
            _viewModel.SelectedPiutangAir.DendaPakai0_UsulanWpf = _viewModel.HitungKoreksi.DendaPakai0;
            _viewModel.SelectedPiutangAir.AdministrasiLain_UsulanWpf = _viewModel.HitungKoreksi.AdministrasiLain;
            _viewModel.SelectedPiutangAir.PemeliharaanLain_UsulanWpf = _viewModel.HitungKoreksi.PemeliharaanLain;
            _viewModel.SelectedPiutangAir.RetribusiLain_UsulanWpf = _viewModel.HitungKoreksi.RetribusiLain;
            _viewModel.SelectedPiutangAir.Meterai_UsulanWpf = _viewModel.HitungKoreksi.Meterai;
            _viewModel.SelectedPiutangAir.Ppn_UsulanWpf = _viewModel.HitungKoreksi.Ppn;
            _viewModel.SelectedPiutangAir.Rekair_UsulanWpf = _viewModel.HitungKoreksi.Rekair;
            _viewModel.SelectedPiutangAir.Denda_UsulanWpf = _viewModel.HitungKoreksi.Denda;
            _viewModel.SelectedPiutangAir.Total_UsulanWpf = _viewModel.HitungKoreksi.Total;

            _viewModel.SelectedPiutangAir.FotoBukti1Uri = _viewModel.FotoBukti1Uri;
            _viewModel.SelectedPiutangAir.IsNewFotoBukti1 = _viewModel.IsNewFotoBukti1;
            _viewModel.SelectedPiutangAir.FotoBukti2Uri = _viewModel.FotoBukti2Uri;
            _viewModel.SelectedPiutangAir.IsNewFotoBukti2 = _viewModel.IsNewFotoBukti2;
            _viewModel.SelectedPiutangAir.FotoBukti3Uri = _viewModel.FotoBukti3Uri;
            _viewModel.SelectedPiutangAir.IsNewFotoBukti3 = _viewModel.IsNewFotoBukti3;

            _viewModel.SelectedPiutangAir.IsInputKoreksiWpf = true;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Tipe_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
                return;

            if (_viewModel != null)
            {
                var current = ((RadioButton)sender).Name;

                _isHanyaAbonemen = current == "HanyaAbonemen";

                CekStanLalu.IsEnabled = true;
                CekStanKini.IsEnabled = true;
                CekStanAngkat.IsEnabled = true;
                CekPakai.IsEnabled = true;
                _viewModel.IsPakaiChecked = false;
                _viewModel.IsStanKiniChecked = true;
                _viewModel.IsStanLaluChecked = false;
                _viewModel.IsStanAngkatChecked = false;

                _viewModel.KoreksiForm = new RekeningAirPiutangWpf()
                {
                    StanLalu_Usulan = _viewModel.SelectedPiutangAir.StanLalu,
                    StanSkrg_Usulan = _viewModel.SelectedPiutangAir.StanSkrg,
                    StanAngkat_Usulan = _viewModel.SelectedPiutangAir.StanAngkat,
                    Pakai_Usulan = _viewModel.SelectedPiutangAir.Pakai,
                };

                StanLalu.Text = _viewModel.SelectedPiutangAir.StanLalu.ToString();
                StanSkrg.Text = _viewModel.SelectedPiutangAir.StanSkrg.ToString();
                StanAngkat.Text = _viewModel.SelectedPiutangAir.StanAngkat.ToString();
                Pakai.Text = _viewModel.SelectedPiutangAir.Pakai.ToString();
                KalkulasiRekening();
            }

            CheckButton();
        }

        private void BtnOnBrowseFileBukti1Command_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti1");
            }
        }

        private void BtnOnBrowseFileBukti2Command_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti2");
            }
        }

        private void BtnOnBrowseFileBukti3Command_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UsulanKoreksiViewModel viewModel)
            {
                viewModel.OnBrowseFileBuktiCommand.Execute("foto_bukti3");
            }
        }
    }
}
