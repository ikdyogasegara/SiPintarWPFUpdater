using System.Windows;
using System.Windows.Controls;

namespace SiPintar.Views.Hublang.Atribut
{
    /// <summary>
    /// Interaction logic for JenisPengaduanView.xaml
    /// </summary>
    public partial class TipePermohonanView : UserControl
    {
        private static bool ShowFilter = true; // filter section flag
        private static bool TempShowFilter = true;

        //public static bool ShowFilter { get => showFilter; set => showFilter = value; }

        public TipePermohonanView()
        {
            InitializeComponent();
        }

        #region == Show/Hide Filter Section
        public void ShowHideFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            TriggerShowFilter();

            if (ShowFilter)
            {
                FilterOptionLabel.Text = "Tutup Filter";
                BtnFilter.Visibility = Visibility.Collapsed;
                FilterColumn.Width = GridLength.Auto;
                FilterMaterial.Visibility = Visibility.Visible;
            }
            else
            {
                FilterOptionLabel.Text = "Filter";
                BtnFilter.Visibility = Visibility.Visible;
                FilterColumn.Width = GridLength.Auto;
                FilterMaterial.Visibility = Visibility.Collapsed;
            }
        }
        public static void TriggerShowFilter()
        {
            TempShowFilter = ShowFilter;
            ShowFilter = !ShowFilter;
        }
        #endregion
    }
}
