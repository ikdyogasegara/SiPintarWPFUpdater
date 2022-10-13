using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningL2T2
{
    public partial class TerbitkanPelangganView : UserControl
    {
        private readonly RekeningL2T2ViewModel _viewModel;

        public TerbitkanPelangganView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningL2T2ViewModel)dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var SelectedData = new List<int>();
            foreach (var item in _viewModel.TerbitkanPelangganList)
                if (item.IsSelected) SelectedData.Add((int)item.IdPelangganLltt);

            var PelangganList = SelectedData.ToArray();

            if (PelangganList.Length == 0)
                return;

            DialogHost.CloseDialogCommand.Execute(null, null);
            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitTerbitkanPelangganCommand).ExecuteAsync(null));
        }
    }
}
