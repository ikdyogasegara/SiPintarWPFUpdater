using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public class TextBoxHelper
    {
        private bool _autoFocus = true;

        public void SelectAllFirstFocusTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox obj)
            {
                if (!string.IsNullOrWhiteSpace(obj.Text) && _autoFocus)
                {
                    obj.SelectAll();
                    _autoFocus = false;
                }
            }
        }
    }
}
