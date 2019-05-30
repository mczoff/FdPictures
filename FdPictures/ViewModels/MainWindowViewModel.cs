using FdPictures.Core.Helpers;
using FdPictures.Core.Utils;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FdPictures.ViewModels
{
    public class MainWindowViewModel
        : ObservableObject
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private ScreenshotMaker _screenshotMaker;

        public MainWindowViewModel()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

            _screenshotMaker = new ScreenshotMaker();
        }

        BitmapSource _sourceImage;
        public BitmapSource SourceImage
        {
            get => _sourceImage;
            set => this.SetProperty(ref this._sourceImage, value);
        }

        WindowState _windowState;
        public WindowState WindowState
        {
            get => _windowState;
            set => this.SetProperty(ref this._windowState, value);
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
                return;

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                var screenBitmapSource = _screenshotMaker.CopyScreen();

                WindowState = WindowState.Maximized;

                SourceImage = screenBitmapSource;

                e.Handled = true;
            }
        }
    }
}
