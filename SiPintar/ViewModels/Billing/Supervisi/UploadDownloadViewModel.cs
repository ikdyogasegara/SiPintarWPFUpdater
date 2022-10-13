using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Billing.Supervisi.UploadDownload;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class UploadDownloadViewModel : ViewModelBase
    {
        public delegate void DisableCloseButtonAction(bool State);
        public event DisableCloseButtonAction DisableCloseEvent;
        private readonly bool _isTest;

        public UploadDownloadViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenConfirmationUploadCommand = new OnOpenConfirmationUploadCommand(this, isTest);
            OnOpenConfirmationDownloadCommand = new OnOpenConfirmationDownloadCommand(this, isTest);
            OnConfirmUploadCommand = new OnConfirmUploadCommand(this, restApi, isTest);
            OnConfirmDownloadCommand = new OnConfirmDownloadCommand(this, restApi, isTest);
            OnReUploadCommand = new OnReUploadCommand(this, restApi, isTest);
            _isTest = isTest;
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenConfirmationUploadCommand { get; }
        public ICommand OnOpenConfirmationDownloadCommand { get; }
        public ICommand OnConfirmUploadCommand { get; }
        public ICommand OnReUploadCommand { get; }
        public ICommand OnConfirmDownloadCommand { get; }

        private bool _tagihanAir = true;
        public bool TagihanAir
        {
            get { return _tagihanAir; }
            set
            {
                _tagihanAir = value;
                OnPropertyChanged();
            }
        }

        private bool _sinkronParameter;
        public bool SinkronParameter
        {
            get { return _sinkronParameter; }
            set
            {
                _sinkronParameter = value;
                OnPropertyChanged();
            }
        }

        private bool _tagihanLimbah = true;
        public bool TagihanLimbah
        {
            get { return _tagihanLimbah; }
            set
            {
                _tagihanLimbah = value;
                OnPropertyChanged();
            }
        }

        private bool _tagihanLltt = true;
        public bool TagihanLltt
        {
            get { return _tagihanLltt; }
            set
            {
                _tagihanLltt = value;
                OnPropertyChanged();
            }
        }

        private int _progressSinkron;
        public int ProgressSinkron
        {
            get { return _progressSinkron; }
            set
            {
                _progressSinkron = value;
                OnPropertyChanged();
            }
        }

        private bool _progressSinkronLoading;
        public bool ProgressSinkronLoading
        {
            get { return _progressSinkronLoading; }
            set
            {
                _progressSinkronLoading = value;
                OnPropertyChanged();
            }
        }

        private int _progressTagihanAir;
        public int ProgressTagihanAir
        {
            get { return _progressTagihanAir; }
            set
            {
                _progressTagihanAir = value;
                OnPropertyChanged();
            }
        }

        private int _progressTagihanLimbah;
        public int ProgressTagihanLimbah
        {
            get { return _progressTagihanLimbah; }
            set
            {
                _progressTagihanLimbah = value;
                OnPropertyChanged();
            }
        }

        private int _progressTagihanLltt;
        public int ProgressTagihanLltt
        {
            get { return _progressTagihanLltt; }
            set
            {
                _progressTagihanLltt = value;
                OnPropertyChanged();
            }
        }

        private string _labelProgressSinkron = "data sudah terbaru";
        public string LabelProgressSinkron
        {
            get { return _labelProgressSinkron; }
            set
            {
                _labelProgressSinkron = value;
                OnPropertyChanged();
            }
        }

        private string _labelProgressTagihanAir = "data sudah terbaru";
        public string LabelProgressTagihanAir
        {
            get { return _labelProgressTagihanAir; }
            set
            {
                _labelProgressTagihanAir = value;
                OnPropertyChanged();
            }
        }

        private string _labelProgressTagihanLimbah = "data sudah terbaru";
        public string LabelProgressTagihanLimbah
        {
            get { return _labelProgressTagihanLimbah; }
            set
            {
                _labelProgressTagihanLimbah = value;
                OnPropertyChanged();
            }
        }

        private string _labelProgressTagihanLltt = "data sudah terbaru";
        public string LabelProgressTagihanLltt
        {
            get { return _labelProgressTagihanLltt; }
            set
            {
                _labelProgressTagihanLltt = value;
                OnPropertyChanged();
            }
        }

        private int _totalFailed;
        public int TotalFailed
        {
            get { return _totalFailed; }
            set
            {
                _totalFailed = value;
                OnPropertyChanged();
            }
        }

        private bool _isUploadDone;
        public bool IsUploadDone
        {
            get { return _isUploadDone; }
            set
            {
                _isUploadDone = value;
                OnPropertyChanged();
            }
        }

        private bool _isUploading;
        public bool IsUploading
        {
            get { return _isUploading; }
            set
            {
                _isUploading = value;
                OnPropertyChanged();
                if (!_isTest)
                    Application.Current.Dispatcher.Invoke(() => { DisableCloseEvent?.Invoke(!value); });
            }
        }

        private bool _canReUpload;
        public bool CanReUpload
        {
            get { return _canReUpload; }
            set
            {
                _canReUpload = value;
                OnPropertyChanged();
            }
        }

        private bool _progressVis;
        public bool ProgressVis
        {
            get { return _progressVis; }
            set
            {
                _progressVis = value;
                OnPropertyChanged();
            }
        }

        public DataUpload DataTagihanAirUpload { get; set; } = new DataUpload();
        public DataUpload DataTagihanLimbahUpload { get; set; } = new DataUpload();
        public DataUpload DataTagihanLlttUpload { get; set; } = new DataUpload();
    }

    public class DataUpload
    {
        public int Success { get; set; }
        public List<dynamic> AllData { get; set; } = new List<dynamic>();
        public List<dynamic> FailedData { get; set; } = new List<dynamic>();
    }
}
