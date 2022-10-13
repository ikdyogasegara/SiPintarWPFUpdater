using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.Views;

namespace SiPintar.Commands.Main.Desktop
{
    public class OnOpenDesktopAppCommand : AsyncCommandBase
    {
        private readonly MainViewModel _parent;
        private readonly bool _isTest;

        private dynamic Window;
        private dynamic Context;

        public OnOpenDesktopAppCommand(MainViewModel parent, bool isTest = false)
        {
            _parent = parent;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var moduleName = parameter as string;

            if (!Application.Current.Resources.Contains("CURRENT_MODULE"))
                Application.Current.Resources.Add("CURRENT_MODULE", "ApiModule" + moduleName.Substring(0, 1).ToUpper() + moduleName.Substring(1).ToLower());
            else
                Application.Current.Resources["CURRENT_MODULE"] = "ApiModule" + moduleName.Substring(0, 1).ToUpper() + moduleName.Substring(1).ToLower();
            SetContext(moduleName);
            OpenApp(Window, Context, moduleName);
            OnStartApp(moduleName, Context);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void SetContext(string moduleName)
        {
            Window = null;
            Context = null;

            switch (moduleName)
            {
                case "bacameter":
                    Context = _parent.bacameter;
                    Window = !_isTest ? new BacameterView(Context) : null;
                    break;
                case "billing":
                    if (_parent.Navi != null)
                    {
                        _parent.Navi.BillingCurrentViewModel = null!;
                    }
                    Context = _parent.billing;
                    Window = !_isTest ? new BillingView(Context) : null;
                    break;
                case "hublang":
                    Context = _parent.hublang;
                    Window = !_isTest ? new HublangView(Context) : null;
                    break;
                case "loket":
                    Context = _parent.loket;
                    Window = !_isTest ? new LoketView(Context) : null;
                    break;
                case "distribusi":
                    Context = _parent.distribusi;
                    Window = !_isTest ? new DistribusiView(Context) : null;
                    break;
                case "perencanaan":
                    Context = _parent.perencanaan;
                    Window = !_isTest ? new PerencanaanView(Context) : null;
                    break;
                case "gudang":
                    if (_parent.Navi != null)
                    {
                        _parent.Navi.GudangCurrentViewModel = null!;
                    }
                    Context = _parent.gudang;
                    Window = !_isTest ? new GudangView(Context) : null;
                    break;
                case "personalia":
                    Context = _parent.personalia;
                    Window = !_isTest ? new PersonaliaView(Context) : null;
                    break;
                case "akuntansi":
                    if (_parent.Navi != null)
                    {
                        _parent.Navi.AkuntansiCurrentViewModel = null!;
                    }
                    Context = _parent.akuntansi;
                    Window = !_isTest ? new AkuntansiView(Context) : null;
                    break;
                case "laporan":
                    Context = _parent.laporan;
                    Window = !_isTest ? new LaporanView(Context) : null;
                    break;
                default:
                    break;
            }
        }

        [ExcludeFromCodeCoverage]
        private void OnStartApp(string moduleName, dynamic context)
        {
            if (context != null && !_isTest)
            {
                switch (moduleName)
                {
                    case "bacameter":
                        context.UpdateCurrentViewModelCommand.Execute(BacameterViewType.Supervisi);
                        context.UpdateState();
                        break;
                    case "billing":
                        context.UpdateState();
                        break;
                    case "hublang":
                        context.UpdateCurrentViewModelCommand.Execute(HublangViewType.Pelayanan);
                        context.UpdateState();
                        break;
                    case "loket":
                        context.UpdateCurrentViewModelCommand.Execute(LoketViewType.Tagihan);
                        context.UpdateState();
                        ((ViewModels.Loket.TagihanViewModel)context.CurrentViewModel).UpdatePage("Kolektif Air");
                        break;
                    case "distribusi":
                        context.UpdateCurrentViewModelCommand.Execute(DistribusiViewType.Distribusi);
                        context.UpdateState();
                        break;
                    case "perencanaan":
                        context.UpdateCurrentViewModelCommand.Execute(PerencanaanViewType.Perencanaan);
                        context.UpdateState();
                        break;
                    case "gudang":
                        context.UpdateState();
                        break;
                    case "personalia":
                        context.UpdateCurrentViewModelCommand.Execute(PersonaliaViewType.DataMaster);
                        context.UpdateState();
                        break;
                    case "akuntansi":
                        context.UpdateState();
                        break;
                    case "laporan":
                        context.UpdateCurrentViewModelCommand.Execute(LaporanViewType.Laporan);
                        context.UpdateState();
                        break;
                    default:
                        break;
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void OpenApp(dynamic Window, dynamic Context, string moduleName)
        {
            _ = Context;

            if (Window != null && !_isTest)
            {
                if (App.OpenedWindow.ContainsKey(moduleName))
                {
                    var obj = App.OpenedWindow[moduleName] as Window;
                    if (obj != null)
                    {
                        App.OpenedWindow.Remove(moduleName);
                        obj.Close();
                    }
                }

                Window.OnPrepare();
                Window.Show();

                _parent.IsMinimizeToTray = true;
                _parent.WindowState = WindowState.Minimized;

                App.OpenedWindow.Add(moduleName, Window);
            }
        }
    }
}
