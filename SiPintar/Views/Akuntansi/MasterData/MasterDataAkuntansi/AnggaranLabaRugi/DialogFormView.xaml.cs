using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly AnggaranLabaRugiViewModel _viewModel;
        public DialogFormView(AnggaranLabaRugiViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (AnggaranLabaRugiViewModel)DataContext;

            InitView();
            CheckButton();
            InputValidation();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void InitView()
        {
            CheckTahun.IsChecked = true;
            CheckBulan.IsChecked = false;
            CheckTahun.Content = $"Tahunan ({_viewModel.SelectedDataUraian.Tahun})";
            CheckBulan.Content = $"Bulanan (Jan {_viewModel.SelectedDataUraian.Tahun} - Des {_viewModel.SelectedDataUraian.Tahun})";
            SamakanNominal.Visibility = Visibility.Visible;
            SamakanNominalTutupBuku.Visibility = Visibility.Collapsed;

            if (_viewModel.PeriodeTutupBukuList.Where(x => x.Tahun == _viewModel.SelectedDataUraian.Tahun).Any())
            {
                CheckTahun.IsChecked = false;
                CheckTahun.IsHitTestVisible = false;
                CheckBulan.IsChecked = true;
                SamakanNominal.Visibility = Visibility.Collapsed;
                SamakanNominalTutupBuku.Visibility = Visibility.Visible;

                var periodeList = _viewModel.PeriodeTutupBukuList.Where(x => x.Tahun == _viewModel.SelectedDataUraian.Tahun).Select(x => x.KodePeriode.ToString()![^2..]);
                foreach (var item in periodeList)
                {
                    switch (item)
                    {
                        case "01":
                            Anggaran1.IsEnabled = false;
                            break;
                        case "02":
                            Anggaran2.IsEnabled = false;
                            break;
                        case "03":
                            Anggaran3.IsEnabled = false;
                            break;
                        case "04":
                            Anggaran4.IsEnabled = false;
                            break;
                        case "05":
                            Anggaran5.IsEnabled = false;
                            break;
                        case "06":
                            Anggaran6.IsEnabled = false;
                            break;
                        case "07":
                            Anggaran7.IsEnabled = false;
                            break;
                        case "08":
                            Anggaran8.IsEnabled = false;
                            break;
                        case "09":
                            Anggaran9.IsEnabled = false;
                            break;
                        case "10":
                            Anggaran10.IsEnabled = false;
                            break;
                        case "11":
                            Anggaran11.IsEnabled = false;
                            break;
                        case "12":
                            Anggaran12.IsEnabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (CheckTahun.IsChecked ?? false)
            {
                if (string.IsNullOrEmpty(AnggaranTahunan.Text))
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
            else if (CheckBulan.IsChecked ?? false)
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
                OkButton.IsEnabled = false;
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsPerTahun)
            {
                var anggaranPerBulan = DecimalValidationHelper.FormatStringIdToDecimal(AnggaranTahunan.Text) / 12;
                _viewModel.AnggaranLabaRugiForm.Anggaran1 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran2 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran3 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran4 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran5 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran6 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran7 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran8 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran9 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran10 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran11 = anggaranPerBulan;
                _viewModel.AnggaranLabaRugiForm.Anggaran12 = anggaranPerBulan;
            }
            else
            {
                _viewModel.AnggaranLabaRugiForm.Anggaran1 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran1.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran2 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran2.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran3 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran3.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran4 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran4.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran5 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran5.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran6 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran6.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran7 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran7.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran8 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran8.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran9 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran9.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran10 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran10.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran11 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran11.Text);
                _viewModel.AnggaranLabaRugiForm.Anggaran12 = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran12.Text);
            }

            _ = ((AsyncCommandBase)_viewModel.OnSubmitFormCommand).ExecuteAsync(null);
        }

        private void CheckTahun_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.IsPerTahun = true;
            DialogGrid.Width = 450;
            TextBoxGroupTahun.Visibility = Visibility.Visible;
            TextBoxGroupBulan.Visibility = Visibility.Collapsed;
            TotalBorder.Visibility = Visibility.Collapsed;
            CheckButton();
        }

        private void CheckBulan_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.IsPerTahun = false;
            DialogGrid.Width = 700;
            TextBoxGroupTahun.Visibility = Visibility.Collapsed;
            TextBoxGroupBulan.Visibility = Visibility.Visible;
            TotalBorder.Visibility = Visibility.Visible;
            CheckButton();
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

        private void CountTotal()
        {
            var total = DecimalValidationHelper.FormatStringIdToDecimal(Anggaran1.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran2.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran3.Text)
                + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran4.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran5.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran6.Text)
                + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran7.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran8.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran9.Text)
                + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran10.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran11.Text) + DecimalValidationHelper.FormatStringIdToDecimal(Anggaran12.Text);

            TextBlockTotal.Text = DecimalValidationHelper.FormatDecimalToStringId(total);
        }
    }
}
