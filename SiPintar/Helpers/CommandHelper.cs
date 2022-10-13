using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class CommandHelper
    {
        public static Task ExecuteCommandAsync(bool isTest, ICommand cmd, bool dispatcher = false)
        {
            if (cmd != null && !isTest)
            {
                if (dispatcher)
                    Application.Current.Dispatcher.Invoke(() => cmd.Execute(null));
                else
                    cmd.Execute(null);
            }
            return Task.FromResult(true);
        }
    }
}
