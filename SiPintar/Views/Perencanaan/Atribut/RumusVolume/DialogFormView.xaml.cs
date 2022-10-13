using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Views.Perencanaan.Atribut.RumusVolume
{
    public partial class DialogFormView : UserControl
    {
        private readonly RumusVolumeViewModel _viewModel;
        public DialogFormView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RumusVolumeViewModel)DataContext;

            Title.Text = _viewModel.IsEdit ? "Koreksi " : "Tambah ";
            Title.Text += "Rumus Volume";

            CheckButton();
            DisableFormOnLoad();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);

            RevalidateData();

            if (_viewModel.IsEdit)
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitEditCommand).ExecuteAsync(null));
            else
                _ = Task.Run(() => ((AsyncCommandBase)_viewModel.OnSubmitAddCommand).ExecuteAsync(null));
        }

        private void CheckButton()
        {
            if (OkButton != null)
            {
                if (NamaOngkos.SelectedItem == null || KodeOngkos.SelectedItem == null ||
                    string.IsNullOrEmpty(BB1.Text) || string.IsNullOrEmpty(BA1.Text) || string.IsNullOrEmpty(VOL1.Text) ||
                    string.IsNullOrEmpty(BB2.Text) || string.IsNullOrEmpty(BA2.Text) || string.IsNullOrEmpty(VOL2.Text) ||
                    string.IsNullOrEmpty(BB3.Text) || string.IsNullOrEmpty(BA3.Text) || string.IsNullOrEmpty(VOL3.Text) ||
                    string.IsNullOrEmpty(BB4.Text) || string.IsNullOrEmpty(BA4.Text) || string.IsNullOrEmpty(VOL4.Text) ||
                    string.IsNullOrEmpty(BB5.Text) || string.IsNullOrEmpty(BA5.Text) || string.IsNullOrEmpty(VOL5.Text) ||
                    (Rumus1.IsChecked != true && Rumus2.IsChecked != true && Rumus3.IsChecked != true && Rumus4.IsChecked != true && Rumus5.IsChecked != true)
                )
                    OkButton.IsEnabled = false;
                else
                    OkButton.IsEnabled = true;
            }
        }

        private void InputValidation()
        {
            BB1.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BB1.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BB1.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BA1.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BA1.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BA1.LostFocus += DecimalValidationHelper.Object_LostFocus;

            VOL1.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            VOL1.GotFocus += DecimalValidationHelper.Object_GotFocus;
            VOL1.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BB2.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BB2.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BB2.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BA2.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BA2.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BA2.LostFocus += DecimalValidationHelper.Object_LostFocus;

            VOL2.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            VOL2.GotFocus += DecimalValidationHelper.Object_GotFocus;
            VOL2.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BB3.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BB3.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BB3.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BA3.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BA3.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BA3.LostFocus += DecimalValidationHelper.Object_LostFocus;

            VOL3.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            VOL3.GotFocus += DecimalValidationHelper.Object_GotFocus;
            VOL3.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BB4.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BB4.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BB4.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BA4.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BA4.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BA4.LostFocus += DecimalValidationHelper.Object_LostFocus;

            VOL4.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            VOL4.GotFocus += DecimalValidationHelper.Object_GotFocus;
            VOL4.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BB5.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BB5.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BB5.LostFocus += DecimalValidationHelper.Object_LostFocus;

            BA5.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            BA5.GotFocus += DecimalValidationHelper.Object_GotFocus;
            BA5.LostFocus += DecimalValidationHelper.Object_LostFocus;

            VOL5.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            VOL5.GotFocus += DecimalValidationHelper.Object_GotFocus;
            VOL5.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Ongkos_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void DisableFormOnLoad()
        {
            if (!_viewModel.IsEdit)
            {
                BB2.IsEnabled = false;
                BA2.IsEnabled = false;
                VOL2.IsEnabled = false;
                BB3.IsEnabled = false;
                BA3.IsEnabled = false;
                VOL3.IsEnabled = false;
                BB4.IsEnabled = false;
                BA4.IsEnabled = false;
                VOL4.IsEnabled = false;
                BB5.IsEnabled = false;
                BA5.IsEnabled = false;
                VOL5.IsEnabled = false;
            }
        }

        private void CheckForm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_viewModel == null || _viewModel.IsEdit)
                return;

            var current = ((TextBox)sender).Name;
            var value = ((TextBox)sender).Text;

            switch (current)
            {
                case "BB1":
                    if (BA1 != null && VOL1 != null)
                    {
                        if (_viewModel.IsBatas1Active)
                        {
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(BA1.Text) && decimal.Parse(BA1.Text) < decimal.Parse(value))
                                BA1.Text = (decimal.Parse(value) + 1).ToString();

                            BA1.IsEnabled = true;
                            VOL1.IsEnabled = true;
                        }

                        _viewModel.FormData.Bb1 = decimal.Parse(BB1.Text);
                        _viewModel.FormData.Ba1 = decimal.Parse(BA1.Text);
                    }
                    break;
                case "BA1":
                    if (BB2 != null && BA2 != null && VOL1 != null)
                    {
                        if (_viewModel.IsBatas2Active)
                        {
                            BB2.Text = BA1.Text;
                            BA2.IsEnabled = true;
                        }

                        VOL1.IsEnabled = true;

                        _viewModel.FormData.Ba1 = decimal.Parse(BA1.Text);
                        _viewModel.FormData.Bb2 = decimal.Parse(BB2.Text);
                    }
                    break;
                case "BB2":
                    if (BA2 != null && VOL2 != null)
                    {
                        if (_viewModel.IsBatas2Active)
                        {
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(BA2.Text) && decimal.Parse(BA2.Text) < decimal.Parse(value))
                                BA2.Text = (decimal.Parse(value) + 1).ToString();

                            BA2.IsEnabled = true;
                            VOL2.IsEnabled = true;
                        }
                        else
                        {
                            BA2.Text = "0";
                        }

                        _viewModel.FormData.Bb2 = decimal.Parse(BB2.Text);
                        _viewModel.FormData.Ba2 = decimal.Parse(BA2.Text);
                    }
                    break;
                case "BA2":
                    if (BB3 != null && BA3 != null && VOL2 != null)
                    {
                        if (_viewModel.IsBatas3Active)
                        {
                            BB3.Text = BA2.Text;
                            BA3.IsEnabled = true;
                        }

                        VOL2.IsEnabled = true;

                        _viewModel.FormData.Ba2 = decimal.Parse(BA2.Text);
                        _viewModel.FormData.Bb3 = decimal.Parse(BB3.Text);
                    }
                    break;
                case "BB3":
                    if (BA3 != null && VOL3 != null)
                    {
                        if (_viewModel.IsBatas3Active)
                        {
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(BA3.Text) && decimal.Parse(BA3.Text) < decimal.Parse(value))
                                BA3.Text = (decimal.Parse(value) + 1).ToString();

                            BA3.IsEnabled = true;
                            VOL3.IsEnabled = true;
                        }
                        else
                        {
                            BA3.Text = "0";
                        }

                        _viewModel.FormData.Bb3 = decimal.Parse(BB3.Text);
                        _viewModel.FormData.Ba3 = decimal.Parse(BA3.Text);
                    }
                    break;
                case "BA3":
                    if (BB4 != null && BA4 != null && VOL3 != null)
                    {
                        if (_viewModel.IsBatas4Active)
                        {
                            BB4.Text = BA3.Text;
                            BA4.IsEnabled = true;
                        }

                        VOL3.IsEnabled = true;

                        _viewModel.FormData.Ba3 = decimal.Parse(BA3.Text);
                        _viewModel.FormData.Bb4 = decimal.Parse(BB4.Text);
                    }
                    break;
                case "BB4":
                    if (BA4 != null && VOL4 != null)
                    {
                        if (_viewModel.IsBatas4Active)
                        {
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(BA4.Text) && decimal.Parse(BA4.Text) < decimal.Parse(value))
                                BA4.Text = (decimal.Parse(value) + 1).ToString();

                            BA4.IsEnabled = true;
                            VOL4.IsEnabled = true;
                        }
                        else
                        {
                            BA4.Text = "0";
                        }

                        _viewModel.FormData.Bb4 = decimal.Parse(BB4.Text);
                        _viewModel.FormData.Ba4 = decimal.Parse(BA4.Text);
                    }
                    break;
                case "BA4":
                    if (BB5 != null && BA5 != null && VOL4 != null)
                    {
                        if (_viewModel.IsBatas5Active)
                        {
                            BB5.Text = BA4.Text;
                            BA5.IsEnabled = true;
                        }

                        VOL4.IsEnabled = true;

                        _viewModel.FormData.Ba4 = decimal.Parse(BA4.Text);
                        _viewModel.FormData.Bb5 = decimal.Parse(BB5.Text);
                    }
                    break;
                case "BB5":
                    if (BA5 != null && VOL5 != null)
                    {
                        if (_viewModel.IsBatas5Active)
                        {
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(BA5.Text) && decimal.Parse(BA5.Text) < decimal.Parse(value))
                                BA5.Text = (decimal.Parse(value) + 1).ToString();

                            BA5.IsEnabled = true;
                            VOL5.IsEnabled = true;
                        }
                        else
                        {
                            BA5.Text = "0";
                        }

                        _viewModel.FormData.Bb5 = decimal.Parse(BB5.Text);
                        _viewModel.FormData.Ba5 = decimal.Parse(BA5.Text);
                    }
                    break;
                case "BA5":
                    if (VOL5 != null)
                    {
                        if (_viewModel.IsBatas5Active)
                        {
                            VOL5.IsEnabled = true;
                        }

                        _viewModel.FormData.Ba5 = decimal.Parse(BA5.Text);
                    }
                    break;
                default:
                    break;
            }

            CheckButton();
        }

        private void RevalidateData()
        {
            _viewModel.FormData.Bb1 = !string.IsNullOrEmpty(BB1.Text) ? decimal.Parse(BB1.Text) : 0;
            _viewModel.FormData.Ba1 = !string.IsNullOrEmpty(BA1.Text) ? decimal.Parse(BA1.Text) : 0;
            _viewModel.FormData.Volum1 = !string.IsNullOrEmpty(VOL1.Text) ? decimal.Parse(VOL1.Text) : 0;
            _viewModel.FormData.Bb2 = !string.IsNullOrEmpty(BB2.Text) ? decimal.Parse(BB2.Text) : 0;
            _viewModel.FormData.Ba2 = !string.IsNullOrEmpty(BA2.Text) ? decimal.Parse(BA2.Text) : 0;
            _viewModel.FormData.Volum2 = !string.IsNullOrEmpty(VOL2.Text) ? decimal.Parse(VOL2.Text) : 0;
            _viewModel.FormData.Bb3 = !string.IsNullOrEmpty(BB3.Text) ? decimal.Parse(BB3.Text) : 0;
            _viewModel.FormData.Ba3 = !string.IsNullOrEmpty(BA3.Text) ? decimal.Parse(BA3.Text) : 0;
            _viewModel.FormData.Volum3 = !string.IsNullOrEmpty(VOL3.Text) ? decimal.Parse(VOL3.Text) : 0;
            _viewModel.FormData.Bb4 = !string.IsNullOrEmpty(BB4.Text) ? decimal.Parse(BB4.Text) : 0;
            _viewModel.FormData.Ba4 = !string.IsNullOrEmpty(BA4.Text) ? decimal.Parse(BA4.Text) : 0;
            _viewModel.FormData.Volum4 = !string.IsNullOrEmpty(VOL4.Text) ? decimal.Parse(VOL4.Text) : 0;
            _viewModel.FormData.Bb5 = !string.IsNullOrEmpty(BB5.Text) ? decimal.Parse(BB5.Text) : 0;
            _viewModel.FormData.Ba5 = !string.IsNullOrEmpty(BA5.Text) ? decimal.Parse(BA5.Text) : 0;
            _viewModel.FormData.Volum5 = !string.IsNullOrEmpty(VOL5.Text) ? decimal.Parse(VOL5.Text) : 0;
        }

        private void Rumus_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((CheckBox)sender).Name;
            var isChecked = ((CheckBox)sender).IsChecked == true;

            switch (current)
            {
                case "Rumus1":
                    BA1.IsEnabled = isChecked;
                    VOL1.IsEnabled = isChecked;
                    break;
                case "Rumus2":
                    BA2.IsEnabled = isChecked;
                    VOL2.IsEnabled = isChecked;
                    break;
                case "Rumus3":
                    BA3.IsEnabled = isChecked;
                    VOL3.IsEnabled = isChecked;
                    break;
                case "Rumus4":
                    BA4.IsEnabled = isChecked;
                    VOL4.IsEnabled = isChecked;
                    break;
                case "Rumus5":
                    BA5.IsEnabled = isChecked;
                    VOL5.IsEnabled = isChecked;
                    break;
                default:
                    break;
            }

            CheckButton();
        }
    }
}
