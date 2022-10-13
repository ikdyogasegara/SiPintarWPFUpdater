using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Loket.Angsuran;


namespace SiPintar.Views.Loket.Angsuran.DetailAngsuran
{
    /// <summary>
    /// Interaction logic for KoreksiSemuaTanggalMulaiTagihView.xaml
    /// </summary>
    public partial class KoreksiSemuaTanggalMulaiTagihView : UserControl
    {
        private readonly DetailAngsuranViewModel Vm;
        private readonly List<DatePicker> DaftarTanggal = new List<DatePicker>();
        public KoreksiSemuaTanggalMulaiTagihView(object dataContext)
        {
            InitializeComponent();
            Vm = dataContext as DetailAngsuranViewModel;
            DataContext = Vm;
            SetUp();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void SetUp()
        {
            var index = 0;
            foreach (var item in Vm.DetailAngsuranList)
            {
                var Temp = Resources["DatePickerTemplate"] as StackPanel;
                var ChildLabel = Temp.Children[0] as TextBlock;
                ChildLabel.Text = $"Tanggal Mulai Tagih {item.Termin}";
                var ChildBorder = Temp.Children[1] as Border;
                var ChildGrid = ChildBorder.Child as Grid;
                var ChildDatePicker = ChildGrid.Children[0] as DatePicker;
                ChildDatePicker.SelectedDate = item.TglMulaiTagih;
                ChildDatePicker.Tag = item.Id;
                DaftarTanggal.Add(ChildDatePicker);
                Container.Children.Insert(index, Temp);
                index++;
            }

        }

        private void YesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var Data = new List<ParamRekeningAngsuranRekeningNonAirTglMulaiTagihListDto>();
            foreach (var item in DaftarTanggal)
            {
                var id = item.Tag as int?;
                if (id.HasValue && item.SelectedDate != null)
                {
                    Data.Add(new ParamRekeningAngsuranRekeningNonAirTglMulaiTagihListDto()
                    {
                        Id = id,
                        TglMulaiTagih = item.SelectedDate
                    });
                }
            }
            Vm.OnSubmitSemuaTglMulaiTagihCommand.Execute(Data);
        }
    }
}
