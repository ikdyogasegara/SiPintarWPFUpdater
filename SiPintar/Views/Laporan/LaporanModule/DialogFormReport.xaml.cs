using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Laporan;

namespace SiPintar.Views.Laporan.LaporanModule
{
    /// <summary>
    /// Interaction logic for DialogFormReport.xaml
    /// </summary>
    public partial class DialogFormReport : UserControl
    {
        private readonly ICommand ShowReportCommand;
        private readonly ICommand OpenDesignerCommand;
        private readonly ReportModelDto Report;
        private readonly ObservableCollection<DataReportFilterList> ReportFilter;
        private readonly ObservableCollection<StackPanel> Filter = new ObservableCollection<StackPanel>();
        private readonly ObservableCollection<RadioButton> Sorter = new ObservableCollection<RadioButton>();

        public DialogFormReport(object dataContext, ReportModelDto _report, ObservableCollection<DataReportFilterList> _reportFilter, ICommand _showCommand, ICommand _openDesignerCommand = null)
        {
            InitializeComponent();
            DataContext = dataContext;
            ShowReportCommand = _showCommand;
            OpenDesignerCommand = _openDesignerCommand;
            Report = _report;
            ReportFilter = _reportFilter;
            Title.Text = Report.Name;
            SetupFilter();
            SetupSort();
            PreviewKeyUp += DialogFormReport_PreviewKeyUp;
        }

        private void DialogFormReport_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && DataContext is LaporanModuleViewModel vm && !vm.IsLoadingForm)
            {
                DialogHost.CloseDialogCommand.Execute(null, this);
            }
        }

        private void SetupSort()
        {
            Sorter.Clear();
            ContainerSort.Visibility = Report.Sorts.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            foreach (var item in Report.Sorts)
            {
                var Temp = Resources["TemplateSort"] as RadioButton;
                Temp.Tag = item.IdSort;
                Temp.Content = item.Name;
                CanvasSort.Children.Insert(CanvasSort.Children.Count, Temp);
                Sorter.Add(Temp);
            }
        }

        private void SetupFilter()
        {
            Filter.Clear();
            ContainerFilter.Visibility = Report.Params.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            foreach (var item in Report.Params)
            {
                StackPanel Temp = null;
                if (item.ParamType == "regular")
                {
                    Temp = SetupRegular($"{item.Name}", true, $"{(item.DataType == "date" || item.DataType == "datetime" ? "Pilih" : "Masukkan")} {item.Name}", false, item.DataType, item.IdParam, item.Required);
                }
                if (item.ParamType == "ratio")
                {
                    Temp = Resources["ListRatio"] as StackPanel;
                    ((CheckBox)Temp.Children[0]).Content = item.Name;
                    ((CheckBox)Temp.Children[0]).IsEnabled = !item.Required;
                    ((CheckBox)Temp.Children[0]).IsChecked = item.Required;
                    ((CheckBox)Temp.Children[0]).Tag = item.Required;
                    ((CheckBox)Temp.Children[0]).Checked += DialogFormView_Checked;
                    ((CheckBox)Temp.Children[0]).Unchecked += DialogFormView_Checked;
                    var _grid = Temp.Children[1] as Grid;
                    var _gridOperator = _grid.Children[0] as Grid;
                    var _cbOperator = _gridOperator.Children[0] as ComboBox;
                    _cbOperator.IsEnabled = item.Required;
                    _cbOperator.Tag = item.IdParam;
                    _cbOperator.ItemsSource = new ObservableCollection<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("=", "="),
                        new KeyValuePair<string, string>(">", ">"),
                        new KeyValuePair<string, string>("<", "<"),
                        new KeyValuePair<string, string>(">=", ">="),
                        new KeyValuePair<string, string>("<=", "<="),
                    };
                    _cbOperator.DisplayMemberPath = "Value";
                    _cbOperator.SelectionChanged += _comboBox_SelectionChanged;
                    var _gridValue = _grid.Children[1] as Grid;
                    var InputElement = _gridValue.Children[0] as TextBox;
                    InputElement.IsEnabled = item.Required;
                    InputElement.TextChanged += InputElement_TextChanged;
                    InputElement.Tag = item.IdParam;
                    if (item.DataType == "int")
                    {
                        InputElement.PreviewTextInput += IntegerValidationHelper.IntegerValidationTextBox;
                        InputElement.GotFocus += IntegerValidationHelper.Object_GotFocus;
                        InputElement.LostFocus += IntegerValidationHelper.Object_LostFocus;
                    }
                    if (item.DataType == "decimal")
                    {
                        InputElement.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                        InputElement.GotFocus += DecimalValidationHelper.Object_GotFocus;
                        InputElement.LostFocus += DecimalValidationHelper.Object_LostFocus;
                    }
                    var PlaceholderElemen = _gridValue.Children[1] as TextBlock;
                    PlaceholderElemen.Text = $"Masukkan {item.Name}";
                }
                if (item.ParamType == "list" || item.ParamType == "custom_list")
                {


                    Temp = Resources["ListTemplate"] as StackPanel;
                    ((CheckBox)Temp.Children[0]).Content = item.Name;
                    ((CheckBox)Temp.Children[0]).IsEnabled = !item.Required;
                    ((CheckBox)Temp.Children[0]).IsChecked = item.Required;
                    ((CheckBox)Temp.Children[0]).Checked += DialogFormView_Checked;
                    ((CheckBox)Temp.Children[0]).Unchecked += DialogFormView_Checked;
                    var _grid = Temp.Children[1] as Grid;
                    var _comboBox = _grid.Children[0] as ComboBox;
                    _comboBox.IsEnabled = item.Required;
                    _comboBox.Tag = item.IdParam;

                    if (item.ParamType == "list")
                        _comboBox.ItemsSource = ReportFilter.FirstOrDefault(x => x.EndPoint == item.ListDataDetail.EndPoint)?.Data2;
                    else if (item.ParamType == "custom_list")
                        _comboBox.ItemsSource = ReportFilter.FirstOrDefault(x => x.EndPoint == $"report-filter-custom-{item.ListCustomDataDetail.Nama.ToLower()}")?.Data2;

                    _comboBox.DisplayMemberPath = "Value";
                    _comboBox.SelectionChanged += _comboBox_SelectionChanged;
                    var _placeholder = _grid.Children[1] as TextBlock;
                    _placeholder.Text = $"Pilih {item.Name}";
                }
                if (item.ParamType == "range")
                {
                    Temp = Resources["RangeTemplate"] as StackPanel;
                    Temp.Tag = item.DataType;
                    ((CheckBox)Temp.Children[0]).Content = item.Name;
                    ((CheckBox)Temp.Children[0]).IsEnabled = !item.Required;
                    ((CheckBox)Temp.Children[0]).IsChecked = item.Required;
                    ((CheckBox)Temp.Children[0]).Checked += ChkElemenRange_Checked;
                    ((CheckBox)Temp.Children[0]).Unchecked += ChkElemenRange_Checked;
                    ((CheckBox)Temp.Children[0]).Tag = item.Required;
                    var _grid = Temp.Children[1] as Grid;
                    foreach (var itemKolom in _grid.Children)
                    {
                        if (((Grid)itemKolom).Name == "KolomKiri")
                        {
                            var _gridKiri = itemKolom as Grid;
                            StackPanel tempKiri = null;

                            if (item.DataType == "date")
                            {
                                tempKiri = SetupDatePicker("", false, "Tanggal Awal", true, item.IdParam, required: item.Required);
                            }
                            else if (item.DataType == "datetime")
                            {
                                tempKiri = SetupDateTimePicker("", false, "Tanggal Awal", true, item.IdParam, required: item.Required);
                            }
                            else
                            {
                                tempKiri = SetupRegular("", false, $"{item.Name} Awal", true, item.DataType, item.IdParam, required: item.Required);
                            }
                            tempKiri.Margin = new Thickness(0, 0, 10, 0);
                            _gridKiri.Children.Insert(_gridKiri.Children.Count, tempKiri);
                        }
                        if (((Grid)itemKolom).Name == "KolomKanan")
                        {
                            var _gridKanan = itemKolom as Grid;
                            StackPanel tempKanan = null;
                            if (item.DataType == "date")
                            {
                                tempKanan = SetupDatePicker("", false, "Tanggal Akhir", true, item.IdParam, required: item.Required); ;
                            }
                            else if (item.DataType == "datetime")
                            {
                                tempKanan = SetupDateTimePicker("", false, "Tanggal Akhir", true, item.IdParam, required: item.Required); ;
                            }
                            else
                            {
                                tempKanan = SetupRegular("", false, $"{item.Name} Akhir", true, item.DataType, item.IdParam, required: item.Required);
                            }
                            tempKanan.Margin = new Thickness(10, 0, 0, 0);
                            _gridKanan.Children.Insert(_gridKanan.Children.Count, tempKanan);
                        }
                    }
                }
                if (Temp != null)
                {
                    Temp.Name = $"Param{item.IdParam}";
                    CanvasFilter.Children.Insert(CanvasFilter.Children.Count, Temp);
                    Filter.Add(Temp);
                }
            }
        }

        private StackPanel SetupDateTimePicker(string Label = null, bool LabelVis = true, string Placeholder = null, bool IsRange = false, int? Tag = null, bool required = false)
        {
            var Response = Resources["DateTimePickerTemplate"] as StackPanel;

            var ChkElemen = Response.Children[0] as CheckBox;
            ChkElemen.IsEnabled = !required;
            ChkElemen.IsChecked = required;
            ChkElemen.Tag = required;
            ChkElemen.Content = Label;
            ChkElemen.Visibility = LabelVis ? Visibility.Visible : Visibility.Collapsed;
            if (!IsRange)
            {
                ChkElemen.Checked += ChkElemenDateTime_Checked;
                ChkElemen.Unchecked += ChkElemenDateTime_Checked;
            }

            var Sp = Response.Children[1] as StackPanel;
            var BorderElement = Sp.Children[0] as Border;
            var GridElemen = BorderElement.Child as Grid;
            var DpElemen = GridElemen.Children[0] as DatePicker;
            if (IsRange)
            {
                DpElemen.Width = 165;
            }
            else
            {
                DpElemen.Width = 336;
            }
            DpElemen.Tag = Tag;
            DpElemen.SelectedDateChanged += DpElemen_SelectedDateChanged;
            DpElemen.IsEnabled = required;
            var DpPlaceholder = GridElemen.Children[1] as TextBlock;
            DpPlaceholder.Text = Placeholder;

            var BorderElementTime = Sp.Children[1] as Border;
            var GridElemenTime = BorderElementTime.Child as Grid;
            var TimeElemen = GridElemenTime.Children[0] as TimePicker;
            TimeElemen.SelectedTimeChanged += TimeElemen_SelectedTimeChanged;
            TimeElemen.IsEnabled = required;
            if (!IsRange)
            {
                TimeElemen.Width = 160;
            }

            return Response;
        }

        private void TimeElemen_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var Tp = sender as TimePicker;
            var _grid = Tp.Parent as Grid;
            if (_grid != null && _grid.Children.Count > 1)
            {
                var txt = _grid.Children[1] as TextBlock;
                if (txt != null)
                {
                    txt.Visibility = Tp.SelectedTime is null ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private StackPanel SetupDatePicker(string Label = null, bool LabelVis = true, string Placeholder = null, bool IsRange = false, int? Tag = null, bool required = false)
        {
            var Response = Resources["DatePickerTemplate"] as StackPanel;
            var ChkElemen = Response.Children[0] as CheckBox;
            ChkElemen.IsEnabled = !required;
            ChkElemen.IsChecked = required;
            ChkElemen.Tag = required;
            ChkElemen.Content = Label;
            ChkElemen.Visibility = LabelVis ? Visibility.Visible : Visibility.Collapsed;
            if (!IsRange)
            {
                ChkElemen.Checked += ChkElemenDate_Checked;
                ChkElemen.Unchecked += ChkElemenDate_Checked;
            }

            var BorderElement = Response.Children[1] as Border;
            var GridElemen = BorderElement.Child as Grid;
            var DpElemen = GridElemen.Children[0] as DatePicker;
            DpElemen.Tag = Tag;
            DpElemen.SelectedDateChanged += DpElemen_SelectedDateChanged;
            var DpPlaceholder = GridElemen.Children[1] as TextBlock;
            DpPlaceholder.Text = Placeholder;
            DpElemen.IsEnabled = required;
            return Response;
        }

        private void ChkElemenDate_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            var req = (chk.Tag as bool?) ?? false;
            var _parent = chk.Parent as StackPanel;
            if (_parent != null && _parent.Children.Count > 1)
            {
                var BorderElemen = _parent.Children[1] as Border;
                var _grid = BorderElemen.Child as Grid;
                if (_grid != null && _grid.Children.Count > 0)
                {
                    var DpELemen = _grid.Children[0] as DatePicker;
                    if (DpELemen != null)
                    {
                        DpELemen.IsEnabled = (chk.IsChecked ?? false) || req;
                        if (!DpELemen.IsEnabled)
                        {
                            DpELemen.SelectedDate = null;
                        }
                    }
                }
            }
        }

        private void ChkElemenDateTime_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            var req = (chk.Tag as bool?) ?? false;
            var _parent = chk.Parent as StackPanel;
            if (_parent != null && _parent.Children.Count > 1)
            {
                var Sp = _parent.Children[1] as StackPanel;

                var BorderElemen = Sp.Children[0] as Border;
                var _grid = BorderElemen.Child as Grid;
                if (_grid != null && _grid.Children.Count > 0)
                {
                    var DpELemen = _grid.Children[0] as DatePicker;
                    if (DpELemen != null)
                    {
                        DpELemen.IsEnabled = (chk.IsChecked ?? false) || req;
                        if (!DpELemen.IsEnabled)
                        {
                            DpELemen.SelectedDate = null;
                        }
                    }
                }

                var BorderElemenTime = Sp.Children[1] as Border;
                var _gridTime = BorderElemenTime.Child as Grid;
                if (_gridTime != null && _gridTime.Children.Count > 0)
                {
                    var TimeELemen = _gridTime.Children[0] as TimePicker;
                    if (TimeELemen != null)
                    {
                        TimeELemen.IsEnabled = (chk.IsChecked ?? false) || req;
                        if (!TimeELemen.IsEnabled)
                        {
                            TimeELemen.SelectedTime = null;
                        }
                    }
                }
            }
        }

        private void ChkElemenRange_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            var req = (chk.Tag as bool?) ?? false;
            var _parent = chk.Parent as StackPanel;

            var _grid = _parent.Children[1] as Grid;
            foreach (Grid itemKolom in _grid.Children)
            {
                var StkPanel = itemKolom.Children[itemKolom.Children.Count - 1] as StackPanel;
                if (StkPanel != null)
                {
                    if ((string)_parent.Tag == "date")
                    {
                        if (StkPanel != null && StkPanel.Children.Count > 1)
                        {
                            var BorderElemen = StkPanel.Children[1] as Border;
                            var _gridElemen = BorderElemen.Child as Grid;
                            if (_gridElemen != null && _gridElemen.Children.Count > 0)
                            {
                                var DpElemen = _gridElemen.Children[0] as DatePicker;
                                if (DpElemen != null)
                                {
                                    DpElemen.IsEnabled = (chk.IsChecked ?? false) || req;
                                    if (!DpElemen.IsEnabled)
                                    {
                                        DpElemen.SelectedDate = null;
                                    }
                                }
                            }
                        }
                    }
                    else if ((string)_parent.Tag == "periode")
                    {
                        if (StkPanel != null && StkPanel.Children.Count > 1)
                        {
                            var _gridBorder = StkPanel.Children[1] as Grid;
                            var _gridBln = _gridBorder.Children[0] as Grid;
                            var _gridThn = _gridBorder.Children[1] as Grid;
                            if (_gridBln != null && _gridThn != null)
                            {
                                var cbxBln = _gridBln.Children[0] as ComboBox;
                                var cbxThn = _gridThn.Children[0] as ComboBox;
                                if (cbxBln != null && cbxThn != null)
                                {
                                    cbxBln.IsEnabled = (chk.IsChecked ?? false) || req;
                                    cbxThn.IsEnabled = (chk.IsChecked ?? false) || req;
                                    if (!cbxBln.IsEnabled && !cbxThn.IsEnabled)
                                    {
                                        cbxBln.SelectedItem = ((ObservableCollection<KeyValuePair<string, string>>)cbxBln.ItemsSource).FirstOrDefault(x => Convert.ToInt32(x.Key) == DateTime.Now.Month);
                                        cbxThn.SelectedItem = ((ObservableCollection<KeyValuePair<int, string>>)cbxThn.ItemsSource).FirstOrDefault(x => x.Key == DateTime.Now.Year); ;
                                    }
                                }
                            }
                        }
                    }
                    else if ((string)_parent.Tag == "datetime")
                    {
                        if (StkPanel != null && StkPanel.Children.Count > 1)
                        {
                            var Sp = StkPanel.Children[1] as StackPanel;
                            var BorderElement = Sp.Children[0] as Border;
                            var GridElemen = BorderElement.Child as Grid;
                            var DpElemen = GridElemen.Children[0] as DatePicker;

                            var BorderElementTime = Sp.Children[1] as Border;
                            var GridElemenTime = BorderElementTime.Child as Grid;
                            var TimeElemen = GridElemenTime.Children[0] as TimePicker;

                            if (DpElemen != null && TimeElemen != null)
                            {
                                DpElemen.IsEnabled = (chk.IsChecked ?? false) || req;
                                TimeElemen.IsEnabled = (chk.IsChecked ?? false) || req;
                                if (!DpElemen.IsEnabled)
                                {
                                    DpElemen.SelectedDate = null;
                                }
                                if (!TimeElemen.IsEnabled)
                                {
                                    TimeElemen.SelectedTime = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (StkPanel != null && StkPanel.Children.Count > 1)
                        {
                            var _gridElemen = StkPanel.Children[1] as Grid;
                            if (_gridElemen != null && _gridElemen.Children.Count > 0)
                            {
                                var InputELemen = _gridElemen.Children[0] as TextBox;
                                if (InputELemen != null)
                                {
                                    InputELemen.IsEnabled = (chk.IsChecked ?? false) || req;
                                    if (!InputELemen.IsEnabled)
                                    {
                                        InputELemen.Text = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DpElemen_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var Dp = sender as DatePicker;
            var _grid = Dp.Parent as Grid;
            if (_grid != null && _grid.Children.Count > 1)
            {
                var txt = _grid.Children[1] as TextBlock;
                if (txt != null)
                {
                    txt.Visibility = Dp.SelectedDate is null ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private StackPanel SetupRegular(string Label = null, bool LabelVis = true, string Placeholder = null, bool IsRange = false, string type = "string", int? Tag = null, bool required = false)
        {
            if (type == "date")
            {
                return SetupDatePicker(Label, LabelVis, Placeholder, IsRange, Tag, required);
            }
            else if (type == "datetime")
            {
                return SetupDateTimePicker(Label, LabelVis, Placeholder, IsRange, Tag, required);
            }
            else
            {
                StackPanel Response;
                if (type == "periode")
                {
                    Response = Resources["PeriodeTemplate"] as StackPanel;
                    var LabelElemen = Response.Children[0] as CheckBox;
                    LabelElemen.IsEnabled = !required;
                    LabelElemen.IsChecked = required;
                    LabelElemen.Tag = required;
                    LabelElemen.Content = Label;
                    LabelElemen.Visibility = LabelVis ? Visibility.Visible : Visibility.Collapsed;
                    LabelElemen.Checked += DialogFormView_CheckedPeriode;
                    LabelElemen.Unchecked += DialogFormView_CheckedPeriode;

                    var GridElemen = Response.Children[1] as Grid;
                    var gridBln = GridElemen.Children[0] as Grid;
                    var cbxBln = gridBln.Children[0] as ComboBox;
                    cbxBln.Tag = Tag;
                    cbxBln.ItemsSource = new ObservableCollection<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("01", "Januari"),
                        new KeyValuePair<string, string>("02", "Februari"),
                        new KeyValuePair<string, string>("03", "Maret"),
                        new KeyValuePair<string, string>("04", "April"),
                        new KeyValuePair<string, string>("05", "Mei"),
                        new KeyValuePair<string, string>("06", "Juni"),
                        new KeyValuePair<string, string>("07", "Juli"),
                        new KeyValuePair<string, string>("08", "Agustus"),
                        new KeyValuePair<string, string>("09", "September"),
                        new KeyValuePair<string, string>("10", "Oktober"),
                        new KeyValuePair<string, string>("11", "November"),
                        new KeyValuePair<string, string>("12", "Desember"),
                    };
                    cbxBln.SelectedItem = ((ObservableCollection<KeyValuePair<string, string>>)cbxBln.ItemsSource).FirstOrDefault(x => Convert.ToString(x.Key) == DateTime.Now.ToString("MM"));
                    cbxBln.DisplayMemberPath = "Value";

                    var periodeList = ReportFilter.FirstOrDefault(x => x.EndPoint == "master-periode-rekening");
                    var minTahun = int.Parse(periodeList.Data1.Min(x => x.Value)[0..^2]);
                    var maxTahun = int.Parse(periodeList.Data1.Max(x => x.Value)[0..^2]);


                    var currentTahun = DateTime.Now.Year;
                    var dataTahun = new ObservableCollection<KeyValuePair<int, string>>();
                    for (var i = minTahun; i <= maxTahun; i++)
                    {
                        dataTahun.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                    var gridThn = GridElemen.Children[1] as Grid;
                    var cbxThn = gridThn.Children[0] as ComboBox;
                    cbxThn.Tag = Tag;
                    cbxThn.ItemsSource = dataTahun;
                    cbxThn.SelectedItem = dataTahun.FirstOrDefault(x => x.Key == currentTahun);
                    cbxThn.DisplayMemberPath = "Value";
                    cbxThn.IsEnabled = required;
                    cbxBln.IsEnabled = required;
                }
                else if (type == "tahun")
                {
                    Response = Resources["ListTemplate"] as StackPanel;
                    ((CheckBox)Response.Children[0]).Content = Label;
                    ((CheckBox)Response.Children[0]).IsEnabled = !required;
                    ((CheckBox)Response.Children[0]).IsChecked = required;
                    ((CheckBox)Response.Children[0]).Checked += DialogFormView_Checked;
                    ((CheckBox)Response.Children[0]).Unchecked += DialogFormView_Checked;
                    var _grid = Response.Children[1] as Grid;
                    var _comboBox = _grid.Children[0] as ComboBox;
                    _comboBox.IsEnabled = required;
                    _comboBox.Tag = Tag;

                    var periodeList = ReportFilter.FirstOrDefault(x => x.EndPoint == "master-periode-rekening");
                    var minTahun = int.Parse(periodeList.Data1.Min(x => x.Value)[0..^2]);
                    var maxTahun = int.Parse(periodeList.Data1.Max(x => x.Value)[0..^2]);

                    var dataTahun = new ObservableCollection<KeyValuePair<int, string>>();
                    for (var i = maxTahun; i >= minTahun; i--)
                    {
                        dataTahun.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }

                    _comboBox.ItemsSource = dataTahun;
                    _comboBox.DisplayMemberPath = "Value";
                    _comboBox.SelectionChanged += _comboBox_SelectionChanged;
                    var _placeholder = _grid.Children[1] as TextBlock;
                    _placeholder.Text = $"Pilih {Label}";
                }
                else
                {
                    Response = Resources["RegularTemplate"] as StackPanel;

                    var LabelElemen = Response.Children[0] as CheckBox;
                    LabelElemen.IsEnabled = !required;
                    LabelElemen.IsChecked = required;
                    LabelElemen.Tag = required;
                    LabelElemen.Content = Label;
                    LabelElemen.Visibility = LabelVis ? Visibility.Visible : Visibility.Collapsed;
                    if (!IsRange)
                    {
                        LabelElemen.Checked += LabelElemen_Checked;
                        LabelElemen.Unchecked += LabelElemen_Checked;
                    }

                    var GridElemen = Response.Children[1] as Grid;

                    var InputElement = GridElemen.Children[0] as TextBox;
                    InputElement.TextChanged += InputElement_TextChanged;
                    InputElement.Tag = Tag;
                    InputElement.IsEnabled = required;
                    if (type == "int")
                    {
                        InputElement.PreviewTextInput += IntegerValidationHelper.IntegerValidationTextBox;
                        InputElement.GotFocus += IntegerValidationHelper.Object_GotFocus;
                        InputElement.LostFocus += IntegerValidationHelper.Object_LostFocus;
                    }
                    if (type == "decimal")
                    {
                        InputElement.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
                        InputElement.GotFocus += DecimalValidationHelper.Object_GotFocus;
                        InputElement.LostFocus += DecimalValidationHelper.Object_LostFocus;
                    }

                    var PlaceholderElemen = GridElemen.Children[1] as TextBlock;
                    PlaceholderElemen.Text = Placeholder;
                }

                return Response;
            }
        }

        private void LabelElemen_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            var req = (chk.Tag as bool?) ?? false;
            var _parent = chk.Parent as StackPanel;
            if (_parent != null && _parent.Children.Count > 1)
            {
                var _grid = _parent.Children[1] as Grid;
                if (_grid != null && _grid.Children.Count > 0)
                {
                    var txtELemen = _grid.Children[0] as TextBox;
                    if (txtELemen != null)
                    {
                        txtELemen.IsEnabled = (chk.IsChecked ?? false) || req;
                        if (!txtELemen.IsEnabled)
                        {
                            txtELemen.Text = null;
                        }
                    }
                }
            }
        }

        private void InputElement_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Obj = sender as TextBox;
            var _grid = Obj.Parent as Grid;
            if (_grid != null && _grid.Children.Count > 1)
            {
                var txt = _grid.Children[1] as TextBlock;
                if (txt != null)
                {
                    txt.Visibility = string.IsNullOrWhiteSpace(Obj.Text) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void DialogFormView_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            var req = (chk.Tag as bool?) ?? false;
            var _parent = chk.Parent as StackPanel;
            if (_parent != null && _parent.Children.Count > 1)
            {
                var _grid = _parent.Children[1] as Grid;
                if (_grid != null && _grid.Children.Count > 0)
                {
                    var cmb = _grid.Children[0] as ComboBox;
                    if (cmb != null)
                    {
                        cmb.IsEnabled = (chk.IsChecked ?? false) || req;
                        if (!cmb.IsEnabled)
                        {
                            cmb.SelectedItem = null;
                        }
                    }

                    //case ratio
                    var gridOp = _grid.Children[0] as Grid;
                    if (gridOp != null)
                    {
                        var cmbOp = gridOp.Children[0] as ComboBox;
                        if (cmbOp != null)
                        {
                            cmbOp.IsEnabled = (chk.IsChecked ?? false) || req;
                            if (!cmbOp.IsEnabled)
                            {
                                cmbOp.SelectedItem = null;
                            }
                        }
                    }
                    var gridValue = _grid.Children[1] as Grid;
                    if (gridValue != null)
                    {
                        var textVal = gridValue.Children[0] as TextBox;
                        if (textVal != null)
                        {
                            textVal.IsEnabled = (chk.IsChecked ?? false) || req;
                            if (!textVal.IsEnabled)
                            {
                                textVal.Text = null;
                            }
                        }

                        //case periode
                        var cbVal = gridValue.Children[0] as ComboBox;
                        if (cbVal != null)
                        {
                            cbVal.IsEnabled = (chk.IsChecked ?? false) || req;
                            if (!cbVal.IsEnabled)
                            {
                                cbVal.SelectedItem = null;
                            }
                        }
                    }

                }
            }
        }

        private void DialogFormView_CheckedPeriode(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            var req = (chk.Tag as bool?) ?? false;
            var _parent = chk.Parent as StackPanel;
            if (_parent != null && _parent.Children.Count > 1)
            {
                var _grid = _parent.Children[1] as Grid;
                if (_grid != null && _grid.Children.Count > 0)
                {
                    var gridBln = _grid.Children[0] as Grid;
                    if (gridBln != null)
                    {
                        var cbxBln = gridBln.Children[0] as ComboBox;
                        if (cbxBln != null)
                        {
                            cbxBln.IsEnabled = (chk.IsChecked ?? false) || req;
                            if (!cbxBln.IsEnabled)
                            {
                                cbxBln.SelectedItem = ((ObservableCollection<KeyValuePair<string, string>>)cbxBln.ItemsSource).FirstOrDefault(x => Convert.ToString(x.Key) == Convert.ToString(DateTime.Now.Month)); ;
                            }
                        }
                    }
                    var gridThn = _grid.Children[1] as Grid;
                    if (gridThn != null)
                    {

                        var cbThn = gridThn.Children[0] as ComboBox;
                        if (cbThn != null)
                        {
                            cbThn.IsEnabled = (chk.IsChecked ?? false) || req;
                            if (!cbThn.IsEnabled)
                            {
                                cbThn.SelectedItem = ((ObservableCollection<KeyValuePair<int, string>>)cbThn.ItemsSource).FirstOrDefault(x => x.Key == DateTime.Now.Year); ;
                            }
                        }
                    }

                }
            }
        }

        private void _comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Obj = sender as ComboBox;
            if (Obj.SelectedItem != null)
            {
                var _grid = Obj.Parent as Grid;
                if (_grid != null && _grid.Children.Count > 1)
                {
                    var txt = _grid.Children[1] as TextBlock;
                    if (txt != null)
                    {
                        txt.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                var _grid = Obj.Parent as Grid;
                if (_grid != null && _grid.Children.Count > 1)
                {
                    var txt = _grid.Children[1] as TextBlock;
                    if (txt != null)
                    {
                        txt.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var filterValue = new List<ReportModelParamWpf>();

            foreach (StackPanel Sp in Filter)
            {
                var temp = Report.Params.FirstOrDefault(x => $"Param{x.IdParam}" == Sp.Name);
                var CheckStatus = Sp.Children[0] as CheckBox;
                if (CheckStatus.IsChecked ?? false)
                {
                    if (temp.ParamType == "ratio")
                    {
                        var _border = Sp.Children[1] as Grid;
                        var _gridOp = _border.Children[0] as Grid;
                        var _gridVal = _border.Children[1] as Grid;
                        var cbxOp = _gridOp.Children[0] as ComboBox;
                        var txtVal = _gridVal.Children[0] as TextBox;
                        if (cbxOp.SelectedItem != null && !string.IsNullOrWhiteSpace(txtVal.Text))
                        {
                            filterValue.Add(new ReportModelParamWpf()
                            {
                                IdParam = temp.IdParam,
                                Name = temp.Name,
                                ParamType = temp.ParamType,
                                DataType = temp.DataType,
                                Value1 = ((KeyValuePair<string, string>)cbxOp.SelectedItem).Key,
                                Value2 = temp.DataType switch
                                {
                                    "int" => IntegerValidationHelper.FormatStringIdToInteger(txtVal.Text),
                                    "decimal" => DecimalValidationHelper.FormatStringIdToDecimal(txtVal.Text),
                                    _ => txtVal.Text
                                }
                            });
                        }
                    }
                    if (temp.ParamType == "regular")
                    {
                        if (temp.DataType == "date")
                        {
                            var _border = Sp.Children[1] as Border;
                            var _grid = _border.Child as Grid;
                            var _input = _grid.Children[0] as DatePicker;
                            if (_input.SelectedDate != null)
                            {
                                filterValue.Add(new ReportModelParamWpf()
                                {
                                    IdParam = temp.IdParam,
                                    Name = temp.Name,
                                    ParamType = temp.ParamType,
                                    DataType = temp.DataType,
                                    Value1 = _input.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss").Replace(".", ":")
                                });
                            }
                        }
                        else if (temp.DataType == "periode")
                        {
                            var _grid = Sp.Children[1] as Grid;
                            var gridBln = _grid.Children[0] as Grid;
                            var gridThn = _grid.Children[1] as Grid;
                            var cbxBln = gridBln.Children[0] as ComboBox;
                            var cbxThn = gridThn.Children[0] as ComboBox;
                            if (cbxBln.SelectedItem != null && cbxThn.SelectedItem != null)
                            {
                                var bln = ((KeyValuePair<string, string>)cbxBln.SelectedItem).Key;
                                var thn = ((KeyValuePair<int, string>)cbxThn.SelectedItem).Key;
                                var periode = Convert.ToInt32($"{thn}{bln}");
                                filterValue.Add(new ReportModelParamWpf()
                                {
                                    IdParam = temp.IdParam,
                                    Name = temp.Name,
                                    ParamType = temp.ParamType,
                                    DataType = temp.DataType,
                                    Value1 = periode
                                });
                            }
                        }
                        else if (temp.DataType == "datetime")
                        {
                            var SpIn = Sp.Children[1] as StackPanel;
                            var BorderElement = SpIn.Children[0] as Border;
                            var GridElemen = BorderElement.Child as Grid;
                            var DpElemen = GridElemen.Children[0] as DatePicker;

                            var BorderElementTime = SpIn.Children[1] as Border;
                            var GridElemenTime = BorderElementTime.Child as Grid;
                            var TimeElemen = GridElemenTime.Children[0] as TimePicker;

                            if (DpElemen.SelectedDate != null && TimeElemen.SelectedTime != null)
                            {
                                filterValue.Add(new ReportModelParamWpf()
                                {
                                    IdParam = temp.IdParam,
                                    Name = temp.Name,
                                    ParamType = temp.ParamType,
                                    DataType = temp.DataType,
                                    Value1 = new DateTime(
                                        DpElemen.SelectedDate.Value.Year,
                                        DpElemen.SelectedDate.Value.Month,
                                        DpElemen.SelectedDate.Value.Day,
                                        TimeElemen.SelectedTime.Value.Hour,
                                        TimeElemen.SelectedTime.Value.Minute,
                                        TimeElemen.SelectedTime.Value.Second
                                    ).ToString("yyyy-MM-dd HH:mm:ss").Replace(".", ":")
                                });
                            }
                        }
                        else if (temp.DataType == "tahun")
                        {
                            var _grid = Sp.Children[1] as Grid;
                            var _input = _grid.Children[0] as ComboBox;
                            if (_input.SelectedItem != null)
                            {
                                var Data = _input.SelectedItem as KeyValuePair<int, string>?;
                                if (Data.HasValue)
                                {
                                    filterValue.Add(new ReportModelParamWpf()
                                    {
                                        IdParam = temp.IdParam,
                                        Name = temp.Name,
                                        ParamType = temp.ParamType,
                                        DataType = temp.DataType,
                                        Value1 = Data.Value.Key
                                    });
                                }
                            }
                        }
                        else
                        {
                            var _grid = Sp.Children[1] as Grid;
                            var _input = _grid.Children[0] as TextBox;
                            if (!string.IsNullOrWhiteSpace(_input.Text))
                            {
                                filterValue.Add(new ReportModelParamWpf()
                                {
                                    IdParam = temp.IdParam,
                                    Name = temp.Name,
                                    ParamType = temp.ParamType,
                                    DataType = temp.DataType,
                                    Value1 = temp.DataType switch
                                    {
                                        "int" => IntegerValidationHelper.FormatStringIdToInteger(_input.Text),
                                        "decimal" => DecimalValidationHelper.FormatStringIdToDecimal(_input.Text),
                                        _ => _input.Text
                                    }
                                });
                            }
                        }
                        continue;
                    }
                    if (temp.ParamType == "list")
                    {
                        var _grid = Sp.Children[1] as Grid;
                        var _input = _grid.Children[0] as ComboBox;
                        if (_input.SelectedItem != null)
                        {
                            var Data = _input.SelectedItem as KeyValuePair<int, string>?;
                            var DataFilter = ReportFilter.FirstOrDefault(x => x.EndPoint == temp.ListDataDetail.EndPoint);
                            var FilterSelect1 = DataFilter?.Data1?.FirstOrDefault(x => x.Key == Data.Value.Key);
                            var FilterSelect2 = DataFilter?.Data2?.FirstOrDefault(x => x.Key == Data.Value.Key);
                            if (Data.HasValue)
                            {
                                filterValue.Add(new ReportModelParamWpf()
                                {
                                    IdParam = temp.IdParam,
                                    Name = temp.Name,
                                    ParamType = temp.ParamType,
                                    DataType = temp.DataType,
                                    Value1 = Data.Value.Key,
                                    ListDataDetail = temp.ListDataDetail,
                                    ListDisplay1 = FilterSelect1?.Value ?? "",
                                    ListDisplay2 = FilterSelect2?.Value ?? ""
                                });


                            }
                        }
                        continue;
                    }

                    if (temp.ParamType == "custom_list")
                    {
                        var _grid = Sp.Children[1] as Grid;
                        var _input = _grid.Children[0] as ComboBox;
                        if (_input.SelectedItem != null)
                        {
                            var Data = _input.SelectedItem as KeyValuePair<int, string>?;
                            var DataFilter = ReportFilter.FirstOrDefault(x => x.EndPoint == $"report-filter-custom-{temp.ListCustomDataDetail.Nama.ToLower()}");
                            var FilterSelect1 = DataFilter?.Data1?.FirstOrDefault(x => x.Key == Data.Value.Key);
                            var FilterSelect2 = DataFilter?.Data2?.FirstOrDefault(x => x.Key == Data.Value.Key);
                            if (Data.HasValue)
                            {
                                filterValue.Add(new ReportModelParamWpf()
                                {
                                    IdParam = temp.IdParam,
                                    Name = temp.Name,
                                    ParamType = temp.ParamType,
                                    DataType = temp.DataType,
                                    Value1 = FilterSelect1?.Value ?? "",
                                    ListCustomDataDetail = temp.ListCustomDataDetail,
                                    ListDisplay1 = FilterSelect1?.Value ?? "",
                                    ListDisplay2 = FilterSelect2?.Value ?? ""
                                });
                            }
                        }
                        continue;
                    }

                    if (temp.ParamType == "range")
                    {
                        var _grid = Sp.Children[1] as Grid;
                        var ParamAdded = new ReportModelParamWpf()
                        {
                            IdParam = temp.IdParam,
                            Name = temp.Name,
                            ParamType = temp.ParamType,
                            DataType = temp.DataType,
                        };
                        foreach (var itemKolom in _grid.Children)
                        {
                            var _gridInner = itemKolom as Grid;
                            var tempInnerSp = _gridInner.Children[0] as StackPanel;
                            if (tempInnerSp != null)
                            {
                                if (temp.DataType == "date")
                                {
                                    var _border = tempInnerSp.Children[1] as Border;
                                    var _gridSub = _border.Child as Grid;
                                    var _input = _gridSub.Children[0] as DatePicker;
                                    if (_input.SelectedDate != null)
                                    {
                                        if (_gridInner.Name == "KolomKiri")
                                        {
                                            ParamAdded.Value1 = _input.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.GetCultureInfo("id-ID")).Replace(".", ":");
                                        }
                                        else
                                        {
                                            ParamAdded.Value2 = _input.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.GetCultureInfo("id-ID")).Replace(".", ":");
                                        }
                                    }
                                }
                                else if (temp.DataType == "periode")
                                {
                                    var _gridSub = tempInnerSp.Children[1] as Grid;
                                    var _gridBln = _gridSub.Children[0] as Grid;
                                    var _gridThn = _gridSub.Children[1] as Grid;
                                    if (_gridBln != null && _gridThn != null)
                                    {
                                        var cbxBln = _gridBln.Children[0] as ComboBox;
                                        var cbxThn = _gridThn.Children[0] as ComboBox;
                                        if (cbxBln != null && cbxThn != null && cbxBln.SelectedItem != null && cbxThn.SelectedItem != null)
                                        {
                                            if (_gridInner.Name == "KolomKiri")
                                            {
                                                ParamAdded.Value1 = Convert.ToInt32($"{((KeyValuePair<int, string>)cbxThn.SelectedItem).Key}{((KeyValuePair<string, string>)cbxBln.SelectedItem).Key}");
                                            }
                                            else
                                            {
                                                ParamAdded.Value2 = Convert.ToInt32($"{((KeyValuePair<int, string>)cbxThn.SelectedItem).Key}{((KeyValuePair<string, string>)cbxBln.SelectedItem).Key}");
                                            }
                                        }
                                    }
                                }
                                else if (temp.DataType == "datetime")
                                {
                                    var SpIn = tempInnerSp.Children[1] as StackPanel;
                                    var BorderElement = SpIn.Children[0] as Border;
                                    var GridElemen = BorderElement.Child as Grid;
                                    var DpElemen = GridElemen.Children[0] as DatePicker;

                                    var BorderElementTime = SpIn.Children[1] as Border;
                                    var GridElemenTime = BorderElementTime.Child as Grid;
                                    var TimeElemen = GridElemenTime.Children[0] as TimePicker;

                                    if (DpElemen.SelectedDate != null && TimeElemen.SelectedTime != null)
                                    {
                                        if (_gridInner.Name == "KolomKiri")
                                        {
                                            ParamAdded.Value1 = new DateTime(
                                                DpElemen.SelectedDate.Value.Year,
                                                DpElemen.SelectedDate.Value.Month,
                                                DpElemen.SelectedDate.Value.Day,
                                                TimeElemen.SelectedTime.Value.Hour,
                                                TimeElemen.SelectedTime.Value.Minute,
                                                TimeElemen.SelectedTime.Value.Second
                                            ).ToString("yyyy-MM-dd HH:mm:ss").Replace(".", ":");
                                        }
                                        else
                                        {
                                            ParamAdded.Value2 = new DateTime(
                                                DpElemen.SelectedDate.Value.Year,
                                                DpElemen.SelectedDate.Value.Month,
                                                DpElemen.SelectedDate.Value.Day,
                                                TimeElemen.SelectedTime.Value.Hour,
                                                TimeElemen.SelectedTime.Value.Minute,
                                                TimeElemen.SelectedTime.Value.Second
                                            ).ToString("yyyy-MM-dd HH:mm:ss").Replace(".", ":");
                                        }
                                    }
                                }
                                else
                                {
                                    var _gridSub = tempInnerSp.Children[1] as Grid;
                                    var _input = _gridSub.Children[0] as TextBox;
                                    if (!string.IsNullOrWhiteSpace(_input.Text))
                                    {
                                        if (_gridInner.Name == "KolomKiri")
                                        {
                                            ParamAdded.Value1 = temp.DataType switch
                                            {
                                                "int" => IntegerValidationHelper.FormatStringIdToInteger(_input.Text),
                                                "decimal" => DecimalValidationHelper.FormatStringIdToDecimal(_input.Text),
                                                _ => _input.Text
                                            };
                                        }
                                        else
                                        {
                                            ParamAdded.Value2 = temp.DataType switch
                                            {
                                                "int" => IntegerValidationHelper.FormatStringIdToInteger(_input.Text),
                                                "decimal" => DecimalValidationHelper.FormatStringIdToDecimal(_input.Text),
                                                _ => _input.Text
                                            };
                                        }
                                    }
                                }
                            }
                        }
                        filterValue.Add(ParamAdded);
                        continue;
                    }
                }
            }

            var paramTidakLengkap = false;
            foreach (var item in Report.Params)
            {
                if (item.Required)
                {
                    paramTidakLengkap = filterValue.FirstOrDefault(x => x.IdParam == item.IdParam) is null
                        || filterValue.FirstOrDefault(x => x.IdParam == item.IdParam)?.Value1 is null;
                    if (item.ParamType == "range" || item.ParamType == "ratio")
                    {
                        paramTidakLengkap = filterValue.FirstOrDefault(x => x.IdParam == item.IdParam)?.Value1 is null
                            || filterValue.FirstOrDefault(x => x.IdParam == item.IdParam)?.Value2 is null;
                    }
                }
            }

            if (paramTidakLengkap)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "DialogFormReportDialog",
                    "Tampilkan Report",
                    message: "Filter tidak lengkap!",
                    "warning",
                    "OK",
                     moduleName: "main"
                );
                return;
            }

            int? idReportSort = null;
            foreach (var item in Sorter)
            {
                if (item.IsChecked ?? false)
                {
                    idReportSort = item.Tag as int?;
                }
            }

            var Param = new ParamMasterReportDataWpf()
            {
                IdModel = Report.IdModel,
                Filters = filterValue,
                IdSort = idReportSort
            };
            _ = ((AsyncCommandBase)ShowReportCommand).ExecuteAsync(Param);
        }

        private void BtnOpenDesigner_Click(object sender, RoutedEventArgs e)
        {
            if (OpenDesignerCommand != null)
            {
                OpenDesignerCommand.Execute(Report);
            }
        }
    }
}
