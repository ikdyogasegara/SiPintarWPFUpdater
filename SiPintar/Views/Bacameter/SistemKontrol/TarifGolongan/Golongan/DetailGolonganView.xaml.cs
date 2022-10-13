﻿using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Golongan
{
    /// <summary>
    /// Interaction logic for DetailGolonganView.xaml
    /// </summary>
    public partial class DetailGolonganView : UserControl
    {
        public DetailGolonganView(GolonganViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
