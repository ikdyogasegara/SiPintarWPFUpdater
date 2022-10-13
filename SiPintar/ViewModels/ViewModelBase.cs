using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;

namespace SiPintar.ViewModels
{
    public delegate TViewModel CreateViewModel<out TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //Error Handling Cetak
        public bool ErrorHandlingCetak(string identifier = "RootDialog", string module = "main", string title = null, string msg = null, bool isTest = false)
        {
            _ = DialogHelper.ShowDialogGlobalViewAsync(isTest, true,
                identifier, title, msg,
                "error", "OK", moduleName: module);
            return true;
        }

        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public bool UpdateProgress(int val, bool isTest = false)
        {
            AppDispatcherHelper.Run(isTest, () => Progress = val);
            return true;
        }
    }

    public class ParamPermohonanListWpf
    {
        public string Name { get; set; }
        public List<ParamPermohonanWpf> Data1 { get; set; }
        public List<ParamPermohonanWpf> Data2 { get; set; }
    }

    public class ParamPermohonanWpf
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class DataReportFilterList
    {
        public string EndPoint { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> Data1 { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> Data2 { get; set; }
    }

    public class ParamMasterReportDataWpf
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdModel { get; set; }
        public List<ReportModelParamWpf> Filters { get; set; } = new List<ReportModelParamWpf>();
        public int? IdSort { get; set; }
    }

    public class ReportModelParamWpf
    {
        public int IdPdam { get; set; }
        public int IdModel { get; set; }
        public int IdParam { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string ParamType { get; set; }
        public int? IdListData { get; set; }
        public int? IdListCustomData { get; set; }
        public dynamic Value1 { get; set; }
        public dynamic Value2 { get; set; }
        public bool Required { get; set; }
        public MasterTipePermohonanConfigListDataDto ListDataDetail { get; set; }
        public ReportFilterCustomDto ListCustomDataDetail { get; set; }
        public string ListDisplay1 { get; set; }
        public string ListDisplay2 { get; set; }
    }
}
