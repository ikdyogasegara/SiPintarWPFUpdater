using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using UserControl = System.Windows.Controls.UserControl;

namespace SiPintar.Views.Loket.Setoran
{
    public partial class FormSetoranDialog : UserControl
    {
        private SetoranViewModel _viewModel;

        public FormSetoranDialog(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SetoranViewModel)DataContext;
            CheckButton();

            InputValidation();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            SetImage();
        }

        private void SetImage()
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.ResiFormPath))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(_viewModel.ResiFormPath);
                image.EndInit();
                imageSetoran.Source = image;
            }
        }

        private void InputValidation()
        {
            JumlahSetoran.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            JumlahSetoran.GotFocus += DecimalValidationHelper.Object_GotFocus;
            JumlahSetoran.LostFocus += DecimalValidationHelper.Object_LostFocus;
        }

        private void Close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = (SetoranViewModel)DataContext;

            if (_viewModel != null)
                _viewModel.OnSubmitSetoranCommand.Execute(null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckButton();

        private void CheckButton()
        {
            if (TglSetor.SelectedDate == null ||
                string.IsNullOrEmpty(JumlahSetoran.Text) ||
                Bank.SelectedItem == null)
                OkButton.IsEnabled = false;
            else
                OkButton.IsEnabled = true;


            //imageSetoran.Source = new BitmapImage(_viewModel.ResiFormURI);
        }

        private void PilihFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JPEG File|*.jpg|Bitmap File|*.bmp|PNG File|*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _viewModel.ResiFormPath = openFileDialog.FileName;
                SetImage();
            }
        }
    }
}
