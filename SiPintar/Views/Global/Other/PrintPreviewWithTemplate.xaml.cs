using System;
using System.Windows;
using System.Windows.Input;
using FastReport;

namespace SiPintar.Views.Global.Other
{
    public partial class PrintPreviewWithTemplate : Window, IDisposable
    {
        private readonly FastReport.Preview.PreviewControl prew = new FastReport.Preview.PreviewControl();

        public PrintPreviewWithTemplate()
        {
            InitializeComponent();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        public void ShowPage(Report report)
        {
            DefineWindowSize();
            //report.Preview
            report.Preview = prew;
            report.Prepare();
            report.ShowPrepared();
            FormContent.Child = prew;
            foreach (var item in prew.ToolBar.Items)
            {
                if (item is FastReport.DevComponents.DotNetBar.ButtonItem && ((FastReport.DevComponents.DotNetBar.ButtonItem)item).Name == "btnClose")
                {
                    ((FastReport.DevComponents.DotNetBar.ButtonItem)item).Click += Item_Click;
                }
                if (item is FastReport.DevComponents.DotNetBar.ButtonItem && (
                    ((FastReport.DevComponents.DotNetBar.ButtonItem)item).Name == "btnEdit" || ((FastReport.DevComponents.DotNetBar.ButtonItem)item).Name == "btnOpen"))
                {
                    ((FastReport.DevComponents.DotNetBar.ButtonItem)item).Visible = false;
                }
            }
            ShowDialog();
        }

        private void Item_Click(object sender, EventArgs e) => Close();

        private void DefineWindowSize()
        {
            var ScreenHeight = SystemParameters.FullPrimaryScreenHeight;
            var ScreenWidth = SystemParameters.FullPrimaryScreenWidth;

            Height = ScreenHeight * 0.8;
            Width = (ScreenWidth * 0.8) >= 1280 ? 1280 : (ScreenWidth * 0.8);
            MinHeight = Height;
            MinWidth = Width;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
