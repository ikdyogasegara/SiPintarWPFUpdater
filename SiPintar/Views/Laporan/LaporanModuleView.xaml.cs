using System.Linq;
using System.Windows.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using SiPintar.Commands;
using SiPintar.ViewModels.Laporan;

namespace SiPintar.Views.Laporan
{
    /// <summary>
    /// Interaction logic for LaporanModuleView.xaml
    /// </summary>
    public partial class LaporanModuleView : UserControl
    {
        public LaporanModuleView()
        {
            InitializeComponent();
            DataContextChanged += LaporanModuleView_DataContextChanged;
        }

        private void LaporanModuleView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var _viewModel = (LaporanModuleViewModel)DataContext;
            _viewModel.IdModule = null;
            _viewModel.SelectedModule = null;
            _viewModel.NavigationItems = _viewModel.GetNavigationItems();
            if (_viewModel != null)
            {
                _viewModel.RenderReportItemEvent += RenderReportItem;
            }
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var _viewModel = (LaporanModuleViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;

            if (_viewModel != null && nav != null)
                _viewModel.UpdateModule(nav.Label.ToLower());
        }

        private void RenderReportItem()
        {
            var vm = (LaporanModuleViewModel)DataContext;
            if (vm == null)
            {
                return;
            }

            Container.Children.Clear();
            if (vm.DataReport?.Count == 0)
            {
                vm.IsLoading = false;
                return;
            }
            foreach (var MGroup in vm.DataReportGroup)
            {
                if (!vm.DataReport.Where(x => x.IdMainGroup == MGroup.IdReportMainGroup).Any())
                {
                    continue;
                }
                var TempCard = Resources["CardTemplate"] as MaterialDesignThemes.Wpf.Card;
                var GrpBox = TempCard.Content as GroupBox;
                GrpBox.Header = MGroup.Nama;
                var StkPanel = GrpBox.Content as StackPanel;
                foreach (var SGroup in MGroup.SubGroup.OrderBy(x => x.Nama))
                {
                    if (!vm.DataReport.Where(x => x.IdMainGroup == MGroup.IdReportMainGroup && x.IdSubGroup == SGroup.IdReportSubGroup).Any())
                    {
                        continue;
                    }

                    var TxtSub = Resources["SubGroup"] as TextBlock;
                    TxtSub.Text = SGroup.Nama;
                    StkPanel.Children.Insert(StkPanel.Children.Count, TxtSub);
                    Button LastItem = null;
                    foreach (var reportItem in vm.DataReport.Where(x => x.IdMainGroup == MGroup.IdReportMainGroup && x.IdSubGroup == SGroup.IdReportSubGroup).ToList())
                    {
                        var Temp = Resources["ListItem"] as Button;
                        Temp.Tag = reportItem.IdModel;
                        var Label = Temp.Content as TextBlock;
                        Label.Text = $"{reportItem.Name}";
                        //Label.Text = $"{reportItem.Name} {(reportItem.ContohDataField == null ? "(Error!! Hub Vendor )" : "")}";
                        //Temp.IsEnabled = reportItem.ContohDataField != null;
                        //if (reportItem.ContohDataField == null)
                        //{
                        //    Temp.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                        //}
                        StkPanel.Children.Insert(StkPanel.Children.Count, Temp);
                        LastItem = Temp;
                    }
                    if (LastItem != null)
                    {
                        LastItem.Margin = new System.Windows.Thickness(0, 8, 0, 15);
                    }
                }
                Container.Children.Insert(Container.Children.Count, TempCard);
            }
            vm.IsLoading = false;
        }

        private void ItemClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (LaporanModuleViewModel)DataContext;
            _ = ((AsyncCommandBase)vm.OnOpenItemCommand).ExecuteAsync(((Button)sender).Tag);
        }
    }
}
