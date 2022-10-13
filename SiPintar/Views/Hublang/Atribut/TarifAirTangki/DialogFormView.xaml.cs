using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Views.Hublang.Atribut.TarifAirTangki
{
    /// <summary>
    /// Interaction logic for DialogFormAddView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private readonly TarifAirTangkiViewModel ViewModel;

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as TarifAirTangkiViewModel;
            DataContext = ViewModel;

            if (ViewModel.IsEdit)
            {
                Title.Text = "Koreksi Tarif Tangki Air";
                BtnSubmit.Content = "Simpan";
                BtnSubmit.IsEnabled = true;
                var item = ViewModel.JenisNonAir.Where(x => x.IdJenisNonAir == ViewModel.SelectedData.IdJenisNonAir).FirstOrDefault();
                if (item != null)
                    JenisNonAir.SelectedItem = item;
            }
            else
                SetupAdd();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            CheckButton();
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoading)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var obj = sender as TextBox;
            if (obj.Name == "BatasAtas1")
            {
                BatasAtas1.Text = string.IsNullOrWhiteSpace(BatasAtas1.Text) ? "0" : BatasAtas1.Text;
                BatasBawah2.Text = Convert.ToDecimal(BatasAtas1.Text.Replace(".", "")).ToString();
            }
            if (obj.Name == "BatasAtas2")
            {
                BatasAtas2.Text = string.IsNullOrWhiteSpace(BatasAtas2.Text) ? "0" : BatasAtas2.Text;
                BatasBawah3.Text = Convert.ToDecimal(BatasAtas2.Text.Replace(".", "")).ToString();
            }
            if (obj.Name == "BatasAtas3")
            {
                BatasAtas3.Text = string.IsNullOrWhiteSpace(BatasAtas3.Text) ? "0" : BatasAtas3.Text;
                BatasBawah4.Text = Convert.ToDecimal(BatasAtas3.Text.Replace(".", "")).ToString();
            }
            if (obj.Name == "BatasAtas4")
            {
                BatasAtas4.Text = string.IsNullOrWhiteSpace(BatasAtas4.Text) ? "0" : BatasAtas4.Text;
                BatasBawah5.Text = Convert.ToDecimal(BatasAtas4.Text.Replace(".", "")).ToString();
            }
            CheckButton();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetupAdd()
        {
            ViewModel.SelectedData = new MasterTarifTangkiDto()
            {
                Bb2 = 0,
                Bb3 = 0,
                Bb4 = 0,
                Bb5 = 0
            };
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var Data = new MasterTarifTangkiDto();
            if (ViewModel.IsEdit)
            {
                Data.IdTarifTangki = ViewModel.SelectedData.IdTarifTangki;
                Data.IdPdam = ViewModel.SelectedData.IdPdam;
            }
            Data.KodeTarifTangki = KodeTarif.Text;
            Data.NamaTarifTangki = NamaTarif.Text;
            Data.Satuan = Satuan.Text;
            Data.Kategori = Kategori.Text;
            Data.BiayaAir = Convert.ToDecimal(BiayaAir.Text.Replace(".", ""));
            Data.BiayaAdministrasi = Convert.ToDecimal(BiayaAdministrasi.Text.Replace(".", ""));
            Data.BiayaOperasional = Convert.ToDecimal(BiayaOperasional.Text.Replace(".", ""));
            Data.BiayaTransport = Convert.ToDecimal(BiayaTransportasi.Text.Replace(".", ""));
            Data.Ppn = Convert.ToDecimal(Ppn.Text.Replace(".", ""));
            Data.Bb1 = Convert.ToDecimal(BatasBawah1.Text.Replace(".", ""));
            Data.Bb2 = Convert.ToDecimal(BatasBawah2.Text.Replace(".", ""));
            Data.Bb3 = Convert.ToDecimal(BatasBawah3.Text.Replace(".", ""));
            Data.Bb4 = Convert.ToDecimal(BatasBawah4.Text.Replace(".", ""));
            Data.Bb5 = Convert.ToDecimal(BatasBawah5.Text.Replace(".", ""));
            Data.Ba1 = Convert.ToDecimal(BatasAtas1.Text.Replace(".", ""));
            Data.Ba2 = Convert.ToDecimal(BatasAtas1.Text.Replace(".", ""));
            Data.Ba3 = Convert.ToDecimal(BatasAtas1.Text.Replace(".", ""));
            Data.Ba4 = Convert.ToDecimal(BatasAtas1.Text.Replace(".", ""));
            Data.Ba5 = Convert.ToDecimal(BatasAtas1.Text.Replace(".", ""));
            Data.T1 = Convert.ToDecimal(Tarif1.Text.Replace(".", ""));
            Data.T2 = Convert.ToDecimal(Tarif1.Text.Replace(".", ""));
            Data.T3 = Convert.ToDecimal(Tarif1.Text.Replace(".", ""));
            Data.T4 = Convert.ToDecimal(Tarif1.Text.Replace(".", ""));
            Data.T5 = Convert.ToDecimal(Tarif1.Text.Replace(".", ""));
            var Jns = JenisNonAir.SelectedItem as MasterJenisNonAirDto;
            Data.IdJenisNonAir = Jns != null ? Jns.IdJenisNonAir : null;

            _ = Task.Run(() => ((AsyncCommandBase)ViewModel.OnSubmitFormCommand).ExecuteAsync(Data));
        }

        private void CheckButton()
        {
            BtnSubmit.IsEnabled = true;
            if (string.IsNullOrWhiteSpace(KodeTarif.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(NamaTarif.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Satuan.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Kategori.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BiayaAir.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BiayaAdministrasi.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BiayaOperasional.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BiayaTransportasi.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Ppn.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasAtas1.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasAtas2.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasAtas3.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasAtas4.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasAtas5.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasBawah1.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasBawah2.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasBawah3.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasBawah4.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(BatasBawah5.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Tarif1.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Tarif2.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Tarif3.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Tarif4.Text))
                BtnSubmit.IsEnabled = false;
            if (string.IsNullOrWhiteSpace(Tarif5.Text))
                BtnSubmit.IsEnabled = false;
            if (JenisNonAir.SelectedItem == null)
                BtnSubmit.IsEnabled = false;
        }

        private void Text_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            CheckButton();
        }

        private void JenisNonAir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
