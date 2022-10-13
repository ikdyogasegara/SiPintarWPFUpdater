﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Commands.Hublang.Onboarding
{
    public class OnClosePageCommand : AsyncCommandBase
    {
        private readonly OnboardingViewModel _viewModel;
        private readonly bool _isTest;

        public OnClosePageCommand(OnboardingViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.CurrentPageName = null;
                _viewModel.IsOverlayActive = false;

                await LiteDBExtension.SetToAppConfigAsync("OnBoardHublangFlag", "1");
                DialogHelper.CloseDialog(_isTest, true, "HublangRootDialog");
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
