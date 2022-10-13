using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Views.Billing.Atribut.RuteBacaMeter
{
    public partial class PerPetugasBacaView : UserControl
    {
        public PerPetugasBacaView()
        {
            InitializeComponent();
        }

        private void Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var viewModel = (PerPetugasBacaViewModel)DataContext;

            if (viewModel == null)
                return;

            switch (e.Key)
            {
                case Key.Return:
                    {
                        if (!string.IsNullOrWhiteSpace(Search.Text))
                        {
                            viewModel.SearchKeywordForm = Search.Text;
                            viewModel.GetDataListCommand.Execute(null);
                        }
                    }
                    break;
                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(Search.Text))
            {
                viewModel.SearchKeywordForm = null;
                viewModel.GetDataListCommand.Execute(null);
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
