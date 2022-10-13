using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Akuntansi.Pengaturan;

namespace SiPintar.Views.Akuntansi.Pengaturan.SettingTtd
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly SettingTtdViewModel _viewModel;
        public DialogFormView(SettingTtdViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SettingTtdViewModel)DataContext;

            Title.Text = ((SettingTtdViewModel)DataContext).IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Setting TTD";
            OkButton.Content = ((SettingTtdViewModel)DataContext).IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

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

        }
    }
}
