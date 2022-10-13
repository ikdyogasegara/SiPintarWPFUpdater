using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan3
{
    /// <summary>
    /// Interaction logic for DialogFormView.xaml
    /// </summary>
    public partial class DialogFormView : UserControl
    {
        private readonly KelompokKodePerkiraan3ViewModel _viewModel;
        public DialogFormView(KelompokKodePerkiraan3ViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (KelompokKodePerkiraan3ViewModel)DataContext;

            Title.Text = ((KelompokKodePerkiraan3ViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Koreksi ";
            Title.Text += "Kode Perkiraan (XX.YY.ZZ)";
            OkButton.Content = ((KelompokKodePerkiraan3ViewModel)DataContext).Parent.IsAdd ? "Tambah " : "Simpan ";

            CheckButton();

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
            if (_viewModel.Parent.SelectedDataPerkiraan1?.IdPerkiraan1.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedDataPerkiraan2?.IdPerkiraan2.HasValue == false)
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(KodePerkiraan3.Text))
                OkButton.IsEnabled = false;
            else if (string.IsNullOrEmpty(NamaPerkiraan3.Text))
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedAkunEtap?.IdAkunEtap.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedPenyusutanAktiva?.IdGolAktiva.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedJenisVoucher?.IdJenisVoucher.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedHarianKas?.IdLhkMaster.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedPerputaranUang?.IdPerputaranUangMaster.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedEkuitasMaster?.IdEkuitasMaster.HasValue == false)
                OkButton.IsEnabled = false;
            else if (_viewModel.Parent.SelectedLabaRugiMaster?.IdGroupLabaRugi.HasValue == false)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.Parent.SelectedDataPerkiraan2 != null)
            {
                var count = (_viewModel.DataList.Count(x => x.KodePerkiraan2 == _viewModel.Parent.SelectedDataPerkiraan2.KodePerkiraan2) + 1).ToString();
                KodePerkiraan3.Text = count.Length < 2 ? "0" + count : count;
                CheckButton();
            }
        }

        private void KelompokPerkiraan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel.Parent.SelectedDataPerkiraan1 != null)
                _viewModel.Parent.DataPerkiraan2List = new ObservableCollection<MasterPerkiraan2Dto>(MasterListGlobal.MasterPerkiraan2.Where(x => x.IdPerkiraan1 == _viewModel.Parent.SelectedDataPerkiraan1.IdPerkiraan1));

            CheckButton();
        }

        private void OkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var data = new ParamMasterPerkiraan3Dto();
            if (!_viewModel.Parent.IsAdd)
            {
                data.IdPerkiraan3 = _viewModel.SelectedData.IdPerkiraan3;
                data.IdPdam = _viewModel.SelectedData.IdPdam;
            }

            data.KodePerkiraan3 = _viewModel.Parent.SelectedDataPerkiraan2.KodePerkiraan2 + "." + KodePerkiraan3.Text;
            data.NamaPerkiraan3 = _viewModel.FormPerkiraan.NamaPerkiraan3;
            data.IdPerkiraan2 = _viewModel.Parent.SelectedDataPerkiraan2.IdPerkiraan2;
            data.IdGolAktiva = _viewModel.Parent.SelectedPenyusutanAktiva.IdGolAktiva;
            data.IdJenisVoucher = _viewModel.Parent.SelectedJenisVoucher.IdJenisVoucher;
            data.IdAkunEtap = _viewModel.Parent.SelectedAkunEtap.IdAkunEtap;
            data.IdLHKMaster = _viewModel.Parent.SelectedHarianKas.IdLhkMaster;
            data.IdLPUMaster = _viewModel.Parent.SelectedPerputaranUang.IdPerputaranUangMaster;
            data.IdEkuitasMaster = _viewModel.Parent.SelectedEkuitasMaster.IdEkuitasMaster;
            data.IdLabaRugiMaster = _viewModel.Parent.SelectedLabaRugiMaster.IdGroupLabaRugi;

            _viewModel.OnSubmitFormCommand.Execute(data);

        }

        private void KodeGolonganAktiva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }
    }
}
