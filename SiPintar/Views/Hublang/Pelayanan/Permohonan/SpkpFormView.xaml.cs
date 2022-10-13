using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Permohonan
{
    public partial class SpkpFormView : UserControl
    {
        private readonly PermohonanViewModel _viewmodel;
        private int _totalForm;
        private UIElement _formElement;
        private readonly List<int> SelectedPegawai = new List<int>();
        private readonly List<StackPanel> PetugasComponentList = new List<StackPanel>();

        public SpkpFormView(object dataContext)
        {
            InitializeComponent();
            _viewmodel = dataContext as PermohonanViewModel;
            DataContext = _viewmodel;
            BtnSubmit.IsEnabled = false;
            SetupPegawai();
            TanggalSpkp.SelectedDate = DateTime.Now;
            CheckSubmit();
            PreviewKeyUp += BppiFormView_PreviewKeyUp;
        }

        private void SetupPegawai()
        {
            PetugasComponentList.Add(FirstPegawai);
            var temp = _viewmodel.SelectedPegawai?.FirstOrDefault();
            if (temp != null)
            {
                var src = Petugas1.ItemsSource as ObservableCollection<MasterPegawaiDto>;
                Petugas1.SelectedItem = _viewmodel.PegawaiList.FirstOrDefault(x => x.IdPegawai == temp.IdPegawai);
            }
            if (_viewmodel.SelectedPegawai?.Count > 1)
            {
                var idx = -1;
                foreach (var item in _viewmodel.SelectedPegawai)
                {
                    idx++;
                    if (idx == 0)
                        continue;
                    else
                    {
                        AppendFormElement(_totalForm, true, _viewmodel.PegawaiList.FirstOrDefault(x => x.IdPegawai == item.IdPegawai));
                    }
                }
            }
        }

        private void AddPegawai_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppendFormElement(_totalForm, true);
                ContentForm.ScrollToBottom();
            }
            catch (Exception error)
            {
                Console.WriteLine("add btn: " + error);
            }
        }

        private void DeletePegawaiBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var current = sender as Button;
                var parent1 = (StackPanel)current.Parent;
                var parent2 = (StackPanel)parent1.Parent;
                var parent3 = (StackPanel)parent2.Parent;

                int index = parent3.Children.IndexOf(parent2);

                // delete in collection
                var optionList = (ComboBox)LogicalTreeHelper.FindLogicalNode(ContentWrapper.Children[index], $"OptionList");
                if (optionList != null && optionList.SelectedIndex >= 0)
                {
                    var currentData = SelectedPegawai[index];
                    var check = _viewmodel.SelectedPegawai.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(currentData));

                    if (check != null)
                        _viewmodel.SelectedPegawai.Remove(check);

                    SelectedPegawai.RemoveAt(index);
                }
                // end delete in collection
                PetugasComponentList.Remove(parent2);
                ContentWrapper.Children.RemoveAt(index);
                _totalForm--;

                // next form
                for (int i = index; i < ContentWrapper.Children.Count; i++)
                {
                    var title = (TextBlock)LogicalTreeHelper.FindLogicalNode(ContentWrapper.Children[i], "Title");
                    if (title != null)
                    {
                        title.Text = "Petugas Pelaksana " + (i + 2).ToString();
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("delete btn: " + error);
            }
        }

        private void AppendFormElement(int i, bool IsHidden = false, MasterPegawaiDto selectedItem = null)
        {
            var main = FindResource("PegawaiComponent") as StackPanel;
            _formElement = main;

            ContentWrapper.Children.Insert(i, _formElement);
            PetugasComponentList.Add(_formElement as StackPanel);
            var component = ContentWrapper.Children[i];

            // Assign Title
            var title = (TextBlock)LogicalTreeHelper.FindLogicalNode(component, "Title");
            if (title != null)
            {
                title.Text += " " + (i + 2).ToString();
            }

            // Assign Delete Button
            var delete = (Button)LogicalTreeHelper.FindLogicalNode(component, "DeletePegawaiBtn");
            if (delete != null)
            {
                delete.Tag += (i + 2).ToString();
                if (i + 1 > 1 && !IsHidden)
                    delete.Visibility = Visibility.Visible;
            }

            // Assign Option
            var option = (ComboBox)LogicalTreeHelper.FindLogicalNode(component, "OptionList");
            if (option != null)
            {
                option.Tag += (i + 2).ToString();
                if (_viewmodel.PegawaiList != null)
                {
                    option.IsEnabled = true;
                    option.ItemsSource = _viewmodel.PegawaiList;
                    option.DisplayMemberPath = "NamaPegawai";
                    option.SelectedItem = _viewmodel.PegawaiList.FirstOrDefault(x => x.IdPegawai == selectedItem?.IdPegawai);
                }
            }

            _totalForm = i + 1;
        }

        private void OptionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var Element = (ComboBox)sender;

                if (Element.SelectedIndex >= 0)
                {
                    var Value = Element.Items.GetItemAt(Element.SelectedIndex);
                    var Result = Value.GetType().GetProperty("IdPegawai").GetValue(Value, null).ToString();

                    if (Result != null)
                    {
                        SelectedPegawai.Add(Convert.ToInt32(Result));
                        var check = _viewmodel.SelectedPegawai.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(Result));
                        var current = _viewmodel.PegawaiList.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(Result));

                        if (check == null && current != null)
                            _viewmodel.SelectedPegawai.Add(current);
                    }

                    foreach (var item in e.RemovedItems)
                    {
                        var ID = item.GetType().GetProperty("IdPegawai").GetValue(item, null).ToString();

                        if (ID != null)
                        {
                            SelectedPegawai.Remove(Convert.ToInt32(ID));
                            var check = _viewmodel.SelectedPegawai.FirstOrDefault(i => i.IdPegawai == Convert.ToInt32(ID));

                            if (check != null)
                                _viewmodel.SelectedPegawai.Remove(check);
                        }
                    }

                    var parent = (Grid)Element.Parent;
                    var Overlay = (TextBlock)LogicalTreeHelper.FindLogicalNode(parent.Children[1], $"PlaceHolder");
                    if (Overlay != null)
                    {
                        Overlay.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("error change option: " + error);
            }
            CheckSubmit();
        }

        private void BppiFormView_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, this);
        }

        private void Tanggal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckSubmit();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var petugas = new List<ParamPetugasSpkDto>();
            var counter = 0;
            foreach (var item in PetugasComponentList)
            {
                ComboBox option = null;
                if (counter == 0)
                {
                    option = (ComboBox)LogicalTreeHelper.FindLogicalNode(item as UIElement, "Petugas1");
                }
                else
                {
                    option = (ComboBox)LogicalTreeHelper.FindLogicalNode(item as UIElement, "OptionList");
                }
                if (option != null && option.SelectedItem != null)
                {
                    petugas.Add(new ParamPetugasSpkDto() { IdPegawai = ((MasterPegawaiDto)option.SelectedItem).IdPegawai });
                }
                counter++;
            }

            var param = new Dictionary<string, dynamic>
            {
                { "IdPermohonan", _viewmodel.SelectedData.IdPermohonan },
                { "TanggalSpkp", TanggalSpkp.SelectedDate },
                { "TanggalSppb", TanggalSppb.SelectedDate },
                { "Petugas", petugas },
            };

            _ = ((AsyncCommandBase)_viewmodel.OnSubmitSpkpFormCommand).ExecuteAsync(param);
        }

        private void CheckSubmit()
        {
            #region petugas harus ada
            var petugas = new List<ParamPetugasSpkDto>();

            try
            {
                var counter = 0;
                foreach (var item in PetugasComponentList)
                {
                    ComboBox option = null;
                    if (counter == 0)
                    {
                        option = (ComboBox)LogicalTreeHelper.FindLogicalNode(item as UIElement, "Petugas1");
                    }
                    else
                    {
                        option = (ComboBox)LogicalTreeHelper.FindLogicalNode(item as UIElement, "OptionList");
                    }
                    if (option != null && option.SelectedItem != null)
                    {
                        petugas.Add(new ParamPetugasSpkDto() { IdPegawai = ((MasterPegawaiDto)option.SelectedItem).IdPegawai });
                    }
                    counter++;
                }
            }
            catch { }
            #endregion

            if (TanggalSpkp.SelectedDate != null && TanggalSppb.SelectedDate != null && petugas.Count > 0)
            {
                BtnSubmit.IsEnabled = true;
            }
            else
            {
                BtnSubmit.IsEnabled = false;
            }
        }
    }
}
