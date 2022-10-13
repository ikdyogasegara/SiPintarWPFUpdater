using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using SiPintar.ViewModels.Bacameter.SistemKontrol.DistribusiPelanggan;

namespace SiPintar.Views.Bacameter.SistemKontrol.DistribusiPelanggan
{
    public partial class PerRayonView : UserControl
    {
        public PerRayonView()
        {
            InitializeComponent();
        }

        public void SetChartHeight(double height = 300)
        {
            ColumnChart.Height = height;
            BarChart.Height = height;
            LineChart.Height = height;
        }

        #region INTERACTION
        private void ResetZoom_Click(object sender, RoutedEventArgs e)
        {
            KolomX.MinValue = double.NaN;
            KolomX.MaxValue = double.NaN;
            KolomY.MinValue = double.NaN;
            KolomY.MaxValue = double.NaN;

            BatangX.MinValue = double.NaN;
            BatangX.MaxValue = double.NaN;
            BatangY.MinValue = double.NaN;
            BatangY.MaxValue = double.NaN;

            GarisX.MinValue = double.NaN;
            GarisX.MaxValue = double.NaN;
            GarisY.MinValue = double.NaN;
            GarisY.MaxValue = double.NaN;
        }

        private void PlotOption_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("PlotMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void PlotMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PerRayonViewModel)DataContext;
            if (_viewModel == null)
                return;

            var mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "KolomMenuItem":
                    _viewModel.Plot = "Kolom";
                    break;
                case "BatangMenuItem":
                    _viewModel.Plot = "Batang";
                    break;
                case "GarisMenuItem":
                    _viewModel.Plot = "Garis";
                    break;
                case "LingkaranMenuItem":
                    _viewModel.Plot = "Lingkaran";
                    break;
            }

            _viewModel.LoadSeries();
        }
        #endregion

        #region PRINT
        public void Cetak()
        {
            string tempFileName = System.IO.Path.GetTempFileName();

            File.Delete(tempFileName);
            using (var xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                IsCetak(true);
                writer.Write(CetakArea);
                IsCetak(false);

                //PrintPreviewWindow previewWindow = new PrintPreviewWindow
                //{
                //    Document = xpsDocument.GetFixedDocumentSequence()
                //};
                //previewWindow.ShowDialog();
            }
        }

        private void IsCetak(bool state = true)
        {
            if (state)
            {
                CetakAreaTitle.Visibility = Visibility.Visible;
                CetakArea.Margin = new Thickness(10, 20, 10, 20);
                ResetZoomWrapper.Visibility = Visibility.Collapsed;
                JadwalBacaLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
            }
            else
            {
                CetakAreaTitle.Visibility = Visibility.Collapsed;
                CetakArea.Margin = new Thickness(0);
                ResetZoomWrapper.Visibility = Visibility.Visible;
                JadwalBacaLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
            }
        }
        #endregion
    }
}
