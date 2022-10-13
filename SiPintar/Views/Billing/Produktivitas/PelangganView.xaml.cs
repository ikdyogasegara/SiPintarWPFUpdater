using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using SiPintar.ViewModels.Billing.Produktivitas;

namespace SiPintar.Views.Billing.Produktivitas
{
    public partial class PelangganView : UserControl
    {
        public PelangganView()
        {
            InitializeComponent();
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

            var dc = (PelangganViewModel)SubLaporan.DataContext;

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
                PelangganLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                TerbacaLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                FotoMeterLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                FotoRumahLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                VideoLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                GpsLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
            }
            else
            {
                CetakAreaTitle.Visibility = Visibility.Collapsed;
                CetakArea.Margin = new Thickness(0);
                PelangganLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
                TerbacaLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
                FotoMeterLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                FotoRumahLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                VideoLabelCheckBox.Style = FindResource("LabelCheckBoxPrint") as Style;
                GpsLabelCheckBox.Style = FindResource("LabelCheckBox") as Style;
            }
        }
    }
}
