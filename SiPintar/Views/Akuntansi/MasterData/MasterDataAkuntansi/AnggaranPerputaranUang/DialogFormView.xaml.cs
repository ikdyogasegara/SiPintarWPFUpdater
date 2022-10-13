using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly AnggaranPerputaranUangViewModel _viewModel;
        public DialogFormView(AnggaranPerputaranUangViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (AnggaranPerputaranUangViewModel)DataContext;

            InitView();
            CheckButton();
            InputValidation();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (RadioTahun.IsChecked == true)
            {
                if (string.IsNullOrEmpty(AnggaranTahunan.Text))
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else if (RadioBulan.IsChecked == true)
            {
                if (string.IsNullOrEmpty(Anggaran1.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran2.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran3.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran4.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran5.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran6.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran7.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran8.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran9.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran10.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran11.Text))
                    OkButton.IsEnabled = false;
                else if (string.IsNullOrEmpty(Anggaran12.Text))
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else
            {
                OkButton.IsEnabled = true;
            }

        }

        private void InitView()
        {
            var tahun = _viewModel.SelectedDataUraian.Tahun;
            var periode = _viewModel.MasterPeriodeAkuntansiList.Where(x => x.Tahun == tahun && x.FlagTutupBuku == true).Select(x => x.KodePeriode.ToString());

            if (periode.Any())
            {
                BtnSamakanNominal.Visibility = Visibility.Collapsed;
                foreach (var bulan in periode)
                {
                    if (bulan[4..] == "01")
                        Anggaran1.IsEnabled = false;

                    if (bulan[4..] == "02")
                        Anggaran2.IsEnabled = false;

                    if (bulan[4..] == "03")
                        Anggaran3.IsEnabled = false;

                    if (bulan[4..] == "04")
                        Anggaran4.IsEnabled = false;

                    if (bulan[4..] == "05")
                        Anggaran5.IsEnabled = false;

                    if (bulan[4..] == "06")
                        Anggaran6.IsEnabled = false;

                    if (bulan[4..] == "07")
                        Anggaran7.IsEnabled = false;

                    if (bulan[4..] == "08")
                        Anggaran8.IsEnabled = false;

                    if (bulan[4..] == "09")
                        Anggaran9.IsEnabled = false;

                    if (bulan[4..] == "10")
                        Anggaran10.IsEnabled = false;

                    if (bulan[4..] == "11")
                        Anggaran11.IsEnabled = false;

                    if (bulan[4..] == "12")
                        Anggaran12.IsEnabled = false;
                }
            }
            else
            {
                BtnSamakanNominal.Visibility = Visibility.Visible;
            }

            RadioTahun.IsEnabled = periode is null;
            RadioTahun.IsChecked = periode is null;
            RadioBulan.IsChecked = periode is not null;

            RadioTahun.Content = $"Tahunan ({_viewModel.SelectedDataUraian.Tahun})";
            RadioBulan.Content = $"Bulanan (Jan {_viewModel.SelectedDataUraian.Tahun} - Des {_viewModel.SelectedDataUraian.Tahun})";

            _viewModel.AnggaranPerputaranUangForm.TotalAnggaran = _viewModel.AnggaranPerputaranUangForm.Anggaran1 + _viewModel.AnggaranPerputaranUangForm.Anggaran2 + _viewModel.AnggaranPerputaranUangForm.Anggaran3
                + _viewModel.AnggaranPerputaranUangForm.Anggaran4 + _viewModel.AnggaranPerputaranUangForm.Anggaran5 + _viewModel.AnggaranPerputaranUangForm.Anggaran6
                + _viewModel.AnggaranPerputaranUangForm.Anggaran7 + _viewModel.AnggaranPerputaranUangForm.Anggaran8 + _viewModel.AnggaranPerputaranUangForm.Anggaran9
                + _viewModel.AnggaranPerputaranUangForm.Anggaran10 + _viewModel.AnggaranPerputaranUangForm.Anggaran11 + _viewModel.AnggaranPerputaranUangForm.Anggaran12;

        }

        private void RadioTahun_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxGroupTahun.Visibility = Visibility.Visible;
            TextBoxGroupBulan.Visibility = Visibility.Collapsed;
            TotalBorder.Visibility = Visibility.Collapsed;
            CheckButton();
        }

        private void RadioBulan_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxGroupTahun.Visibility = Visibility.Collapsed;
            TextBoxGroupBulan.Visibility = Visibility.Visible;
            TotalBorder.Visibility = Visibility.Visible;
            CheckButton();
        }


        #region TextBox Changed
        private void Anggaran1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran4_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran5_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran6_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran7_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran8_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran9_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran10_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran11_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        private void Anggaran12_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountTotal();
        }
        #endregion

        private void CountTotal()
        {
            var total = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran1.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran2.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran3.Text)
                + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran4.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran5.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran6.Text)
                + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran7.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran8.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran9.Text)
                + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran10.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran11.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran12.Text);

            TextBlockTotal.Text = DecimalValidationHelper.FormatDecimalToStringId(total);
        }

        private void SamakanNominal_Click(object sender, RoutedEventArgs e)
        {
            Anggaran2.Text = Anggaran1.Text;
            Anggaran3.Text = Anggaran1.Text;
            Anggaran4.Text = Anggaran1.Text;
            Anggaran5.Text = Anggaran1.Text;
            Anggaran6.Text = Anggaran1.Text;
            Anggaran7.Text = Anggaran1.Text;
            Anggaran8.Text = Anggaran1.Text;
            Anggaran9.Text = Anggaran1.Text;
            Anggaran10.Text = Anggaran1.Text;
            Anggaran11.Text = Anggaran1.Text;
            Anggaran12.Text = Anggaran1.Text;
            CheckButton();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (RadioTahun.IsChecked == true)
            {
                var nominalPerBulan = DecimalValidationHelper.FormatStringIdToDecimal(AnggaranTahunan.Text) / 12;
                _viewModel.AnggaranPerputaranUangForm.Anggaran1 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran2 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran3 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran4 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran5 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran6 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran7 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran8 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran9 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran10 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran11 = nominalPerBulan;
                _viewModel.AnggaranPerputaranUangForm.Anggaran12 = nominalPerBulan;

            }
            else if (RadioBulan.IsChecked == true)
            {
                _viewModel.AnggaranPerputaranUangForm.Anggaran1 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran1.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran2 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran2.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran3 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran3.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran4 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran4.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran5 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran5.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran6 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran6.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran7 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran7.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran8 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran8.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran9 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran9.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran10 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran10.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran11 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran11.Text);
                _viewModel.AnggaranPerputaranUangForm.Anggaran12 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran12.Text);
            }

            _ = ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null);
        }


        private void InputValidation()
        {
            AnggaranTahunan.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            AnggaranTahunan.GotFocus += DecimalValidationHelper.Object_GotFocus;
            AnggaranTahunan.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran1.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran1.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran1.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran2.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran2.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran2.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran3.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran3.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran3.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran4.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran4.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran4.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran5.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran5.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran5.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran6.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran6.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran6.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran7.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran7.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran7.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran8.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran8.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran8.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran9.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran9.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran9.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran10.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran10.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran10.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran11.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran11.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran11.LostFocus += DecimalValidationHelper.Object_LostFocus;

            Anggaran12.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            Anggaran12.GotFocus += DecimalValidationHelper.Object_GotFocus;
            Anggaran12.LostFocus += DecimalValidationHelper.Object_LostFocus;

        }
    }
}
