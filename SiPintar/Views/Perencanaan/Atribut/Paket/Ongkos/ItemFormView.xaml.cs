using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;

namespace SiPintar.Views.Perencanaan.Atribut.Paket.Ongkos
{
    public partial class ItemFormView : UserControl
    {
        private readonly PaketOngkosViewModel _viewModel;
        public ItemFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PaketOngkosViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Rincian Ongkos";

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddItemStep1Command).ExecuteAsync(null));
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _viewModel.NamaItemForm = Keyword.Text;
                _viewModel.OnSearchItemCommand.Execute(null);
            }
        }
    }
}
