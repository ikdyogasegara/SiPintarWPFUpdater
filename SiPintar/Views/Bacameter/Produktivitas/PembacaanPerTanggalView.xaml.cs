using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using SiPintar.ViewModels.Bacameter.Produktivitas;

namespace SiPintar.Views.Bacameter.Produktivitas
{
    public partial class PembacaanPerTanggalView : UserControl
    {
        public PembacaanPerTanggalView()
        {
            InitializeComponent();
        }
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
            var mi = e.Source as MenuItem;

            var dc = (PembacaanPerTanggalViewModel)SubLaporan.DataContext;

            switch (mi.Name)
            {
                case "KolomMenuItem":
                    dc.Plot = "Kolom";
                    break;
                case "BatangMenuItem":
                    dc.Plot = "Batang";
                    break;
                case "GarisMenuItem":
                    dc.Plot = "Garis";
                    break;
                case "LingkaranMenuItem":
                    dc.Plot = "Lingkaran";
                    break;
            }

            dc.LoadSeries();
        }

        public void Cetak()
        {
            string tempFileName = System.IO.Path.GetTempFileName();

            File.Delete(tempFileName);
            using var xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite);
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

        private void IsCetak(bool state = true)
        {
            if (state)
            {
                CetakAreaTitle.Visibility = Visibility.Visible;
                CetakArea.Margin = new Thickness(10, 20, 10, 20);
                TerbacaLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                TaksirLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                KelainanLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
            }
            else
            {
                CetakAreaTitle.Visibility = Visibility.Collapsed;
                CetakArea.Margin = new Thickness(0);
                TerbacaLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
                TaksirLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
                KelainanLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
            }
        }
    }
}
