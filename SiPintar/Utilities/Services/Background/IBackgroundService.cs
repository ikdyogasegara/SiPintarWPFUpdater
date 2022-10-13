using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SiPintar.Utilities.Services.Background
{
    public interface IBackgroundService
    {
        public string Status { get; }
        public ObservableCollection<Task> ProcessList { get; set; }
        public bool AnyRunProcess();
        public void RunInBackground(Task t);
    }

    [ExcludeFromCodeCoverage]
    public class BackgroundProcess
    {
        public bool IsRunning { get; set; }
    }
}
