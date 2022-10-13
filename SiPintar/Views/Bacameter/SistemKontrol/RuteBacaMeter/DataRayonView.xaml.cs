using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Views.Bacameter.SistemKontrol.RuteBacaMeter
{
    public partial class DataRayonView : UserControl
    {
        public DataRayonView()
        {
            InitializeComponent();
        }

        private void Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var _viewModel = (DataRayonViewModel)DataContext;

            if (_viewModel == null)
                return;

            switch (e.Key)
            {
                case Key.Return:
                    {
                        if (!string.IsNullOrWhiteSpace(Search.Text))
                        {
                            _viewModel.SearchKeywordForm = Search.Text;
                            _viewModel.GetDataListCommand.Execute(null);
                        }
                    }
                    break;
                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                _viewModel.SearchKeywordForm = null;
                _viewModel.GetDataListCommand.Execute(null);
            }
        }

        private void DataGridContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridContent_Loaded(object sender, RoutedEventArgs e)
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                //dpd.AddValueChanged(DataGridContent, UpdatedSource);
            }
        }

        private void UpdatedSource(object sender, EventArgs e)
        {

        }
    }
}
