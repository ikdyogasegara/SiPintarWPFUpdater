using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    /// <summary>
    /// Interaction logic for SetRekeningNormalView.xaml
    /// </summary>
    public partial class SetRekeningNormalView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;
        public SetRekeningNormalView(RekeningAirViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;
            Title.Text = ((RekeningAirViewModel)DataContext).TanpaDenda ? "Konfirmasi Set Tanpa Denda " : "Konfirmasi Set Rekening Normal";
            Msg.Text = $"Apakah Anda yakin akan set rekening {(((RekeningAirViewModel)DataContext).TanpaDenda ? "tanpa denda" : "normal")} bulan";
            PreviewKeyUp += SetRekeningNormalView_PreviewKeyUp;
        }

        private void SetRekeningNormalView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
    }
}
