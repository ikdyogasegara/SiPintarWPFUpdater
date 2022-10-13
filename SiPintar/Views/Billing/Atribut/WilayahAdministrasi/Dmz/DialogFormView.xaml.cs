﻿using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;

namespace SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Dmz
{
    public partial class DialogFormView : UserControl
    {
        private readonly DmzViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DmzViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "District Meter Zona";

            CheckButton();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            PreviewKeyUp += new KeyEventHandler(HandleEnter);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.KodeForm = Kode.Text;
                _viewModel.NamaForm = Nama.Text;

                Submit_Click(null, null);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (!string.IsNullOrEmpty(Kode.Text) && !string.IsNullOrEmpty(Nama.Text) && notif.Text == "")
                OkButton.IsEnabled = true;
            else
                OkButton.IsEnabled = false;
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string notifikasi = _viewModel.DmzList.FirstOrDefault(i => i.KodeDmz == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "";

            if (notifikasi != "" && !_viewModel.IsEdit)
                notif.Visibility = Visibility.Visible;
            else
                notif.Visibility = Visibility.Collapsed;
        }

        private void Nama_TextChanged(object sender, TextChangedEventArgs e)
        {
            string notifikasi = _viewModel.DmzList.FirstOrDefault(i => i.NamaDmz == Nama.Text) != null ? notif.Text = "Nama telah digunakan" : notif.Text = "";

            if (notifikasi != "" && !_viewModel.IsEdit)
                notif.Visibility = Visibility.Visible;
            else
                notif.Visibility = Visibility.Collapsed;
        }
    }
}