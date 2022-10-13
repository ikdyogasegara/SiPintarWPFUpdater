﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Views.Billing.Atribut.Kelainan
{
    public partial class DialogFormView : UserControl
    {
        private readonly KelainanViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            _viewModel = (KelainanViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Data Kelainan";

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
            if ((e.Key == Key.Enter) &&
                !string.IsNullOrEmpty(Kode.Text) && !string.IsNullOrEmpty(Nama.Text) &&
                JenisKelainan.SelectedItem != null && !string.IsNullOrEmpty(Posisi.Text) &&
                Status.SelectedItem != null && notif.Text == ""
            )
            {
                _viewModel.FormData.KodeKelainan = Kode.Text;
                _viewModel.FormData.Kelainan = Nama.Text;
                _viewModel.FormData.JenisKelainan = ((KeyValuePair<int, string>)JenisKelainan.SelectedItem).Value;
                _viewModel.FormData.Deskripsi = Deskripsi.Text;
                _viewModel.FormData.Posisi = Convert.ToInt32(Posisi.Text);
                _viewModel.FormData.Status = ((KeyValuePair<int, string>)Status.SelectedItem).Key == 1;

                Submit_Click(null, null);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditFormCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddFormCommand).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (string.IsNullOrEmpty(Kode.Text) || string.IsNullOrEmpty(Nama.Text) ||
                JenisKelainan.SelectedItem == null || string.IsNullOrEmpty(Posisi.Text) ||
                Status.SelectedItem == null || notif.Text != "")
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            CheckButton();
        }

        private void Kode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_viewModel.IsEdit)
            {
                notif.Text = "";
                return;
            }

            string notifikasi = null;

            notifikasi = _viewModel.DataList.FirstOrDefault(i => i.KodeKelainan == Kode.Text) != null ? notif.Text = "Kode telah digunakan" : notif.Text = "";

            if (notifikasi != "")
                notif.Visibility = Visibility.Visible;
            else
                notif.Visibility = Visibility.Collapsed;
        }
    }
}
