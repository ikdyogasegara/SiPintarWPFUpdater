using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Views.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca
{
    public partial class ListRayonFormView : UserControl
    {
        private readonly PetugasBacaViewModel ViewModel;

        public ListRayonFormView(object dataContext)
        {
            InitializeComponent();
            ViewModel = dataContext as PetugasBacaViewModel;
            DataContext = ViewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !ViewModel.IsLoadingForm)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        //private void Select1_Click(object sender = null, RoutedEventArgs e = null)
        //{
        //    var item = DataGridContent.SelectedItem;
        //    var _viewModel = (PetugasBacaViewModel)DataContext;

        //    _viewModel.SelectedRayon = item as MasterRayonDto;

        //    _ = ((AsyncCommandBase)_viewModel.OnSubmitAddRayonCommand).ExecuteAsync(null);
        //}

    }
}
