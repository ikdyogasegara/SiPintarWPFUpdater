using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.Posting
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly PostingViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PostingViewModel)DataContext;
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            Notifikasi();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }


        private void ComboPeriode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notifikasi();
        }

        private void Notifikasi()
        {
            var note = _viewModel.RiwayatPosting.Where(x => x.IdPeriode == _viewModel.SelectedDataPeriode?.IdPeriode);
            if (note.Any())
                notif.Text = $@"Periode {_viewModel.SelectedDataPeriode.NamaPeriode} sudah diposting. Untuk aktivitas posting ulang, Mohon isi catatan";
            else
                notif.Text = "";
        }

        private void PostingRekeningAir_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingRekeningAir = true;
        }

        private void PostingRekeningAir_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingRekeningAir = false;
        }

        private void PostingRekeningLimbah_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingRekeningLimbah = true;
        }

        private void PostingRekeningLimbah_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingRekeningLimbah = false;
        }

        private void PostingRekeningLltt_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingRekeningLltt = true;
        }

        private void PostingRekeningLltt_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingRekeningLltt = false;
        }

        private void PostingPelangganAir_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingPelangganAir = true;
        }

        private void PostingPelangganAir_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingPelangganAir = false;
        }

        private void PostingPelangganLimbah_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingPelangganLimbah = true;
        }

        private void PostingPelangganLimbah_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingPelangganLimbah = false;
        }

        private void PostingPelangganLltt_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingPelangganLltt = true;
        }

        private void PostingPelangganLltt_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.IsPostingPelangganLltt = false;
        }

    }
}
