using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class Step3AddFormView : UserControl
    {
        public Step3AddFormView()
        {
            InitializeComponent();
            InputValidation();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is PendaftaranViewModel viewModel)
            {
                BiayaPendaftaran.Text = DecimalValidationHelper.FormatDecimalToStringId(viewModel.BiayaPendaftaranForm);
                BiayaFormulir.Text = DecimalValidationHelper.FormatDecimalToStringId(viewModel.BiayaFormulirForm);
                BiayaOpname.Text = DecimalValidationHelper.FormatDecimalToStringId(viewModel.BiayaOpnameForm);
                BiayaJaminan.Text = DecimalValidationHelper.FormatDecimalToStringId(viewModel.BiayaJaminanForm);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var viewModel = (PendaftaranViewModel)DataContext;
            var current = ((TextBox)sender).Name;

            switch (current)
            {
                case "BiayaPendaftaran":
                    viewModel.BiayaPendaftaranForm = DecimalValidationHelper.FormatStringIdToDecimal(((TextBox)sender).Text);
                    viewModel.CalculateBiayaCommand.Execute("pendaftaran");
                    break;
                case "BiayaOpname":
                    viewModel.BiayaOpnameForm = DecimalValidationHelper.FormatStringIdToDecimal(((TextBox)sender).Text);
                    viewModel.CalculateBiayaCommand.Execute("opname");
                    break;
                case "BiayaFormulir":
                    viewModel.BiayaFormulirForm = DecimalValidationHelper.FormatStringIdToDecimal(((TextBox)sender).Text);
                    viewModel.CalculateBiayaCommand.Execute("formulir");
                    break;
                case "BiayaJaminan":
                    viewModel.BiayaJaminanForm = DecimalValidationHelper.FormatStringIdToDecimal(((TextBox)sender).Text);
                    viewModel.CalculateBiayaCommand.Execute("jaminan");
                    break;
                default:
                    break;
            }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void KodePair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InputValidation()
        {
            BiayaPendaftaran.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BiayaPendaftaran.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BiayaPendaftaran.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BiayaOpname.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BiayaOpname.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BiayaOpname.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BiayaFormulir.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BiayaFormulir.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BiayaFormulir.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BiayaJaminan.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BiayaJaminan.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BiayaJaminan.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }
    }
}
