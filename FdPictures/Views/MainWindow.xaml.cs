using FdPictures.Core;
using FdPictures.Core.Params;
using FdPictures.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FdPictures
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool mouseDown = false; // Set to 'true' when mouse is held down.
        Point mouseDownPos; // The point where the mouse button was clicked down.

        int stepNumber = 0;

        

        public MainWindow()
        {
            InitializeComponent();

            //IFdPicturesContext context = new FdPicturesContext();
            //context.Transform(new BitmapImage(new Uri(@"E:\\test.png")), new TransformParams { HRatio = 10 , WRatio = 10 });
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stepNumber++;

            // Capture and track the mouse.
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();

            if(stepNumber % 2 == 0)
            {
                // Initial placement of the drag selection box.         
                Canvas.SetLeft(selectionBox1, mouseDownPos.X);
                Canvas.SetTop(selectionBox1, mouseDownPos.Y);
                selectionBox1.Width = 0;
                selectionBox1.Height = 0;

                // Make the drag selection box visible.
                selectionBox1.Visibility = Visibility.Visible;
            }
            else
            {
                // Initial placement of the drag selection box.         
                Canvas.SetLeft(selectionBox2, mouseDownPos.X);
                Canvas.SetTop(selectionBox2, mouseDownPos.Y);
                selectionBox2.Width = 0;
                selectionBox2.Height = 0;

                // Make the drag selection box visible.
                selectionBox2.Visibility = Visibility.Visible;
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Release the mouse capture and stop tracking it.
            mouseDown = false;
            theGrid.ReleaseMouseCapture();

            Point mouseUpPos = e.GetPosition(theGrid);

            if (stepNumber % 2 == 0)
            {
                (this.DataContext as MainWindowViewModel).FirstRectangle = Tuple.Create(mouseDownPos, mouseUpPos);
            }
            else
            {
                (this.DataContext as MainWindowViewModel).SecondRectangle = Tuple.Create(mouseDownPos, mouseUpPos);
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                // When the mouse is held down, reposition the drag selection box.

                Point mousePos = e.GetPosition(theGrid);

                if (stepNumber % 2 == 0)
                {
                    if (mouseDownPos.X < mousePos.X)
                    {
                        Canvas.SetLeft(selectionBox1, mouseDownPos.X);
                        selectionBox1.Width = mousePos.X - mouseDownPos.X;
                    }
                    else
                    {
                        Canvas.SetLeft(selectionBox1, mousePos.X);
                        selectionBox1.Width = mouseDownPos.X - mousePos.X;
                    }

                    if (mouseDownPos.Y < mousePos.Y)
                    {
                        Canvas.SetTop(selectionBox1, mouseDownPos.Y);
                        selectionBox1.Height = mousePos.Y - mouseDownPos.Y;
                    }
                    else
                    {
                        Canvas.SetTop(selectionBox1, mousePos.Y);
                        selectionBox1.Height = mouseDownPos.Y - mousePos.Y;
                    }
                }
                else
                {
                    if (mouseDownPos.X < mousePos.X)
                    {
                        Canvas.SetLeft(selectionBox2, mouseDownPos.X);
                        selectionBox2.Width = mousePos.X - mouseDownPos.X;
                    }
                    else
                    {
                        Canvas.SetLeft(selectionBox2, mousePos.X);
                        selectionBox2.Width = mouseDownPos.X - mousePos.X;
                    }

                    if (mouseDownPos.Y < mousePos.Y)
                    {
                        Canvas.SetTop(selectionBox2, mouseDownPos.Y);
                        selectionBox2.Height = mousePos.Y - mouseDownPos.Y;
                    }
                    else
                    {
                        Canvas.SetTop(selectionBox2, mousePos.Y);
                        selectionBox2.Height = mouseDownPos.Y - mousePos.Y;
                    }
                }
            }
        }
    }
}
