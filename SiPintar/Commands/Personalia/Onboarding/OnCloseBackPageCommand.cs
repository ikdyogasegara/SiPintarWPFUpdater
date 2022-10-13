using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Personalia;

namespace SiPintar.Commands.Personalia.Onboarding
{
    public class OnCloseBackPageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;

        public OnCloseBackPageCommand(OnboardingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.OpenPageCommand.Execute(_viewModel.TempCurrentPageName);

                DialogHost.Close("PersonaliaRootDialog");
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message.ToString());
                Debug.WriteLine(e);
            }

            await Task.FromResult(true);
        }
    }
}
