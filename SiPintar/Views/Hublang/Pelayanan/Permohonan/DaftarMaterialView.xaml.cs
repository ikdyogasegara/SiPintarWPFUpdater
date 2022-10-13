using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class DaftarMaterialView : UserControl
    {
        private readonly PermohonanViewModel Vm;
        private readonly bool? _flagBiayaDibebankanKePdam;
        private readonly bool? _flagDialihkanKeVendor;

        public DaftarMaterialView(object dataContext, bool flagBiayaDibebankanKePdam = false, bool flagDialihkanKeVendor = false)
        {
            InitializeComponent();
            Vm = dataContext as PermohonanViewModel;
            DataContext = Vm;
            _flagBiayaDibebankanKePdam = flagBiayaDibebankanKePdam;
            _flagDialihkanKeVendor = flagDialihkanKeVendor;

            NamaBarang.Text = "";
            KodeBarang.Text = "";
            PreviewKeyUp += DaftarMaterialView_PreviewKeyUp;

            Vm.CurrentPageMaterialOngkos = 1;
            Vm.LimitDataMaterialOngkos = new KeyValuePair<int, string>(10, "10");
        }

        private void DaftarMaterialView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(null, null);
            }
        }

        private void PilihMaterial_Click(object sender, RoutedEventArgs e)
        {
            if ((Vm.SelectedPaketRabPipaPersil != null || Vm.SelectedPaketRabPipaDistribusi != null) && Vm.SelectedDaftarMaterial != null)
            {
                _ = DialogHost.Show(new TambahMaterialView(Vm, _flagBiayaDibebankanKePdam, _flagDialihkanKeVendor), "InnerSecondGlobalRootDialog");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "currentPage", Vm.CurrentPageMaterialOngkos },
                { "pageSize", Vm.LimitDataMaterialOngkos.Key },
                { "NamaMaterial", NamaBarang.Text },
                { "KodeMaterial", KodeBarang.Text },
            };

            _ = ((AsyncCommandBase)Vm.OnCariMaterialCommand).ExecuteAsync(param);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            Vm.CurrentPageMaterialOngkos--;
            Button_Click(null, null);
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            Vm.CurrentPageMaterialOngkos++;
            Button_Click(null, null);
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_Click(null, null);
        }
    }
}
