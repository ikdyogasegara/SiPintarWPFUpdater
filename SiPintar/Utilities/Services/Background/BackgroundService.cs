using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SiPintar.Utilities.Services.Background
{
    [ExcludeFromCodeCoverage]
    public class BackgroundService : IBackgroundService, INotifyPropertyChanged
    {
        public string Status
        {
            get
            {
                return ProcessList?.Count > 0 && ProcessList.FirstOrDefault(x => x.Status == TaskStatus.Running) != null ? "Running Background Proses" : "Ready";
            }
        }
        public ObservableCollection<Task> ProcessList { get; set; } = new ObservableCollection<Task>();
        public bool AnyRunProcess()
        {
            return ProcessList?.Count > 0 && ProcessList.FirstOrDefault(x => x.Status == TaskStatus.Running) != null;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void RunInBackground(Task t)
        {
            ProcessList.Add(t);
        }
    }
}
