using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Elasticsearch.Net;
using Newtonsoft.Json.Linq;
using SiPintar.Utilities;

namespace SiPintar.Helpers.TableHelper
{
    [ExcludeFromCodeCoverage]
    public class TableHelper : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private readonly bool _isTest;

        private KeyValuePair<int, string> _pageSize;
        public KeyValuePair<int, string> PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged();
                CurrentPage = 1;
                RefreshPageCommand.Execute(null);
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNextButtonEnabled));
                OnPropertyChanged(nameof(IsLastButtonEnabled));
                OnPropertyChanged(nameof(IsPreviousButtonEnabled));
                OnPropertyChanged(nameof(IsFirstButtonEnabled));
                OnPropertyChanged(nameof(IsLeftPageNumberFillerVisible));
                OnPropertyChanged(nameof(IsLeftPageNumberActive));
                OnPropertyChanged(nameof(IsMiddlePageNumberVisible));
                OnPropertyChanged(nameof(IsRightPageNumberActive));
                OnPropertyChanged(nameof(IsRightPageNumberVisible));
                OnPropertyChanged(nameof(IsRightPageNumberFillerVisible));
            }
        }

        private int _totalPage;
        public int TotalPage
        {
            get => _totalPage;
            set
            {
                _totalPage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNextButtonEnabled));
                OnPropertyChanged(nameof(IsLastButtonEnabled));
                OnPropertyChanged(nameof(IsPreviousButtonEnabled));
                OnPropertyChanged(nameof(IsFirstButtonEnabled));
                OnPropertyChanged(nameof(IsLeftPageNumberFillerVisible));
                OnPropertyChanged(nameof(IsLeftPageNumberActive));
                OnPropertyChanged(nameof(IsMiddlePageNumberVisible));
                OnPropertyChanged(nameof(IsRightPageNumberActive));
                OnPropertyChanged(nameof(IsRightPageNumberVisible));
                OnPropertyChanged(nameof(IsRightPageNumberFillerVisible));
            }
        }

        private long _totalRecord;
        public long TotalRecord
        {
            get => _totalRecord;
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<dynamic>? _data;
        public ObservableCollection<dynamic>? Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        public ObservableCollection<T>? GetList<T>()
        {
            if (_isTest)
            {
                return Activator.CreateInstance<ObservableCollection<T>>();
            }
            return Data as ObservableCollection<T>;
        }

        private dynamic? _selectedData;
        public dynamic? SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        public T? GetSelected<T>()
        {
            if (_isTest)
            {
                return Activator.CreateInstance<T>();
            }
            return ((JObject)SelectedData!).ToObject<T>();
        }

        public ICommand NextPageCommand { get; }
        public ICommand PrevPageCommand { get; }
        public ICommand FirstPageCommand { get; }
        public ICommand LastPageCommand { get; }
        public ICommand RefreshPageCommand { get; }

        #region Pagination
        public bool IsNextButtonEnabled { get => CurrentPage < TotalPage; }
        public bool IsLastButtonEnabled { get => CurrentPage < TotalPage; }
        public bool IsPreviousButtonEnabled { get => CurrentPage > 1; }
        public bool IsFirstButtonEnabled { get => CurrentPage > 1; }
        public bool IsLeftPageNumberFillerVisible { get => CurrentPage > 1; }
        public bool IsLeftPageNumberActive { get => CurrentPage is 1; }
        public bool IsMiddlePageNumberVisible { get => CurrentPage > 1 && CurrentPage < TotalPage; }
        public bool IsRightPageNumberActive { get => CurrentPage != 1 && CurrentPage == TotalPage; }
        public bool IsRightPageNumberVisible { get => TotalPage is not 1; }
        public bool IsRightPageNumberFillerVisible { get => CurrentPage < TotalPage; }
        #endregion

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
        public bool IsEmpty { get => !_isLoading && Data != null && Data.Count == 0; }

        public TableHelper(string url, IRestApiClientModel restApi, string moduleName, Func<Dictionary<string, dynamic>>? getFilter = null, bool isTest = false)
        {
            _isTest = isTest;
            PropertyChanged = null!;
            _isLoading = false;
            _currentPage = 1;
            _totalPage = 1;
            _pageSize = ConstantaPageSize.PageSizeList[1];

            GetFilter = getFilter;

            PrevPageCommand = new OnPageChangeCommand(HttpMethod.GET, url, restApi, GetParameter("prev"), CallbackCommand(moduleName));
            FirstPageCommand = new OnPageChangeCommand(HttpMethod.GET, url, restApi, GetParameter("first"), CallbackCommand(moduleName));
            NextPageCommand = new OnPageChangeCommand(HttpMethod.GET, url, restApi, GetParameter("next"), CallbackCommand(moduleName));
            LastPageCommand = new OnPageChangeCommand(HttpMethod.GET, url, restApi, GetParameter("last"), CallbackCommand(moduleName));
            RefreshPageCommand = new OnPageChangeCommand(HttpMethod.GET, url, restApi, GetParameter("refresh"), CallbackCommand(moduleName));
        }
        private Action<RestApiResponse> CallbackCommand(string moduleName) => (res) =>
        {
            if (!res.IsError)
            {
                if (res.Data.Status)
                {
                    Data = res.Data.Data != null ? res.Data.Data.ToObject<ObservableCollection<dynamic>>() : new();
                    TotalPage = res.Data.TotalPage == 0 ? 1 : res.Data.TotalPage;
                    TotalRecord = res.Data.Record;
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", res.Data?.Ui_msg!, moduleName);
                    Data = new();
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", res.Error.Message, moduleName);
            }
            IsLoading = false;
        };
        private Func<Dictionary<string, dynamic>> GetParameter(string type)
        {
            return () =>
            {
                var res = GetFilter?.Invoke() ?? new Dictionary<string, dynamic>();
                IsLoading = true;
                Data = null;
                res.Add("PageSize", PageSize.Key);
                CurrentPage = type switch
                {
                    "first" => 1,
                    "prev" => CurrentPage == 1 ? CurrentPage : (CurrentPage - 1),
                    "next" => CurrentPage == TotalPage ? CurrentPage : (CurrentPage + 1),
                    "last" => TotalPage,
                    _ => CurrentPage
                };
                res.Add("CurrentPage", CurrentPage);
                return res;
            };
        }

        private Func<Dictionary<string, dynamic>>? GetFilter { get; }
    }

    [ExcludeFromCodeCoverage]
    public static class ConstantaPageSize
    {
        public static ObservableCollection<KeyValuePair<int, string>> PageSizeList
        {
            get => new ObservableCollection<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(10, "10"),
                new KeyValuePair<int, string>(20, "20"),
                new KeyValuePair<int, string>(50, "50"),
                new KeyValuePair<int, string>(100, "100"),
                new KeyValuePair<int, string>(1000000, "Semua")
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class OnPageChangeCommand : ICommand
    {
        private bool _isExecuting { get; set; }

        #region required
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return !_isExecuting;
        }
        public void Execute(object? parameter)
        {
            _isExecuting = true;
            _ = ExecuteAsync();
            _isExecuting = false;
        }
        #endregion

        private readonly HttpMethod _method;
        private readonly string _url;
        private readonly IRestApiClientModel _restApi;

        private Action<RestApiResponse> CallBack { get; set; }
        private Func<Dictionary<string, dynamic>> Paramter { get; set; }

        public OnPageChangeCommand(HttpMethod method, string url, IRestApiClientModel restApi, Func<Dictionary<string, dynamic>> parameter, Action<RestApiResponse> callBack)
        {
            _method = method;
            _url = url;
            _isExecuting = false;
            _restApi = restApi;
            CallBack = callBack;
            Paramter = parameter;
        }

        public async Task ExecuteAsync()
        {
            var res = _method switch
            {
                HttpMethod.POST => await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_url}", Paramter()),
                HttpMethod.PATCH => await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/{_url}", null!, Paramter()),
                HttpMethod.DELETE => await _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/{_url}", Paramter()),
                _ => await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_url}", Paramter())
            };
            CallBack.Invoke(res);
        }
    }

    public enum HttpMethod
    {
        GET, POST, PATCH, DELETE
    }
}
