using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private bool isInit;

        public DialogFormView(SaldoAwalPerkiraanViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SaldoAwalPerkiraanViewModel)DataContext;


            if (_viewModel.SelectedData.PerkiraanWpf == "perkiraan3" && _viewModel.SelectedData.DebetAwal != 0)
            {
                SaldoKredit.IsEnabled = false;
                SaldoDebit.IsEnabled = true;
                isInit = true;
            }
            else if (_viewModel.SelectedData.PerkiraanWpf == "perkiraan3" && _viewModel.SelectedData.KreditAwal != 0)
            {
                SaldoDebit.IsEnabled = false;
                SaldoKredit.IsEnabled = true;
                isInit = true;
            }

            CheckButton();
            InputValidation();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void TglSaldo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(SaldoDebit.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(SaldoKredit.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(TglSaldo.Text))
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;


            if (SaldoDebit.Text != "0" && SaldoKredit.Text == "0" && !isInit)
            {
                SaldoKredit.Text = "0";
                SaldoKredit.IsEnabled = false;
                SaldoDebit.IsEnabled = true;
            }
            else if (SaldoKredit.Text != "0" && SaldoDebit.Text == "0" && !isInit)
            {
                SaldoDebit.Text = "0";
                SaldoDebit.IsEnabled = false;
                SaldoKredit.IsEnabled = true;
            }
            else if (SaldoDebit.Text == "0" && SaldoKredit.Text == "0" && !isInit)
            {
                SaldoDebit.IsEnabled = true;
                SaldoKredit.IsEnabled = true;
            }

            isInit = false;
            InputValidation();
        }

        private void InputValidation()
        {
            SaldoDebit.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            SaldoDebit.GotFocus += DecimalValidationHelper.Object_GotFocus;
            SaldoDebit.LostFocus += DecimalValidationHelper.Object_LostFocus;

            SaldoKredit.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            SaldoKredit.GotFocus += DecimalValidationHelper.Object_GotFocus;
            SaldoKredit.LostFocus += DecimalValidationHelper.Object_LostFocus;

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FormPerkiraan.DebetAwal = DecimalValidationHelper.FormatStringIdToDecimal(SaldoDebit.Text);
            _viewModel.FormPerkiraan.KreditAwal = DecimalValidationHelper.FormatStringIdToDecimal(SaldoKredit.Text);
            _viewModel.FormPerkiraan.TglSaldo = TglSaldo.SelectedDate;

            //_ = ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null);
            _ = ((AsyncCommandBase)_viewModel.OnOpenConfirmEditCommand).ExecuteAsync(null);
        }

    }
}
