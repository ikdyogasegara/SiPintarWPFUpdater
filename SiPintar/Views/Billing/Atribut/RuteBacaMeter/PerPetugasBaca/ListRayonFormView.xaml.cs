using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Views.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    public partial class ListRayonFormView : UserControl
    {
        private readonly PerPetugasBacaViewModel _viewModel;

        public ListRayonFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PerPetugasBacaViewModel;
            DataContext = _viewModel;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !_viewModel.IsLoadingForm)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
