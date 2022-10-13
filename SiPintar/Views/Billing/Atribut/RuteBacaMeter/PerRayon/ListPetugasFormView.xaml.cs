using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Views.Billing.Atribut.RuteBacaMeter.PerRayon
{
    public partial class ListPetugasFormView : UserControl
    {
        private readonly PerRayonViewModel _viewModel;

        public ListPetugasFormView(object dataContext)
        {
            InitializeComponent();
            _viewModel = dataContext as PerRayonViewModel;
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
