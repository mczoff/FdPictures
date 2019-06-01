using FdPictures.Core;
using FdPictures.Core.Helpers;
using FdPictures.Core.Params;
using FdPictures.Core.Utils;
using FdPictures.Utils;
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

        public Tuple<Point, Point> FirstRectangle { get; set; }
        public Tuple<Point, Point> SecondRectangle { get; set; }

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

        ICommand _scanCommand;
        public ICommand ScanCommand
            => _scanCommand ?? (_scanCommand = new RelayCommand(
                _ =>
                {
                    IFdPicturesContext fdPicturesContext = new FdPicturesContext();

                    fdPicturesContext.Transform(_sourceImage, new TransformParams
                    {
                        FirstRectangle = new Int32Rect((int)FirstRectangle.Item1.X, (int)FirstRectangle.Item1.Y, (int)(FirstRectangle.Item2.X - FirstRectangle.Item1.X), (int)(FirstRectangle.Item2.Y - FirstRectangle.Item1.Y)),
                        SecondRectangle = new Int32Rect((int)SecondRectangle.Item1.X, (int)SecondRectangle.Item1.Y, (int)(SecondRectangle.Item2.X - SecondRectangle.Item1.X), (int)(SecondRectangle.Item2.Y - SecondRectangle.Item1.Y)),
                    });
                }));
    }
}
