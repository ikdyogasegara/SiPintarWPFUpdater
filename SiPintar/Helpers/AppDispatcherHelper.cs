using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class AppDispatcherHelper
    {
        public delegate void Action();
        public static void Run(bool? isTest = false, Action actRun = null)
        {
            if (isTest.HasValue && !isTest.Value && actRun != null)
            {
                _ = Application.Current.Dispatcher.InvokeAsync(() => actRun());
            }
        }
    }
}
